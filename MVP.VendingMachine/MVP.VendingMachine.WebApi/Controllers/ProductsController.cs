using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVP.VendingeMachine.Repositories;
using MVP.VendingeMachine.Services;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using System.Data;

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

        if (_productService.AddProduct(product, HttpContext.User))
            return StatusCode(201);

        return BadRequest();
    }        

    [HttpPut]
    [Authorize(Roles = "Seller")]
    public IActionResult UpdateProduct([FromBody] ProductDto product)
    {
        if (product is null || !ModelState.IsValid)
            return BadRequest();

        if (_productService.UpdateProduct(product, HttpContext.User))
            return Ok();

        return BadRequest();
    }

    [HttpDelete]
    [Authorize(Roles = "Seller")]
    public IActionResult DeleteProduct(Guid id)
    {
        //if (id is null)
        //    return BadRequest();

        if (_productService.DeleteProduct(id, HttpContext.User))
            return Ok();

        return BadRequest();
    }

}

