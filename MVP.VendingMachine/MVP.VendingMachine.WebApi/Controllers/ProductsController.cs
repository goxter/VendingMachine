using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.Dto;

namespace MVP.VendingMachine.WebApi.Controllers;

[Route("api/products")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsService _productService;
    public ProductsController(IProductsService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    [Authorize(Roles = "Buyer, Seller")]
    public ProductDto[] GetProducts() =>    
        _productService.GetProducts();

    [HttpPost]
    [Authorize(Roles = "Seller")]
    public IActionResult AddProduct([FromBody] ProductDto product)
    {
        if (product is null || !ModelState.IsValid)
            return BadRequest();

        if (product.Price % 5 != 0)
            return BadRequest("Price should be in multiples of 5");

        var result = _productService.AddProduct(product, HttpContext.User);
        if (result.IsSuccess)
            return StatusCode(201);

        return BadRequest(result.Message);
    }        

    [HttpPut]
    [Authorize(Roles = "Seller")]
    public IActionResult UpdateProduct([FromBody] ProductDto product)
    {
        if (product is null || !ModelState.IsValid)
            return BadRequest();

        if (product.Price % 5 != 0)
            return BadRequest("Price should be in multiples of 5");

        var result = _productService.UpdateProduct(product, HttpContext.User);
        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Message);
    }

    [HttpDelete]
    [Authorize(Roles = "Seller")]
    public IActionResult DeleteProduct([FromBody] Guid id)
    {
        var result = _productService.DeleteProduct(id, HttpContext.User);
        if (result.IsSuccess)
            return Ok();

        return BadRequest(result.Message);
    }

    [HttpPost("buy")]
    [Authorize(Roles = "Buyer")]
    public async Task<IActionResult> BuyProducts([FromBody]  ProductToBuyDto product)
    {
        await _productService.BuyProduct(product, HttpContext.User);

        return Ok();
    }
}

