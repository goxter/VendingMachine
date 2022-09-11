using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVP.VendingMachine.Dto;
using System.Data;

namespace MVP.VendingMachine.WebApi.Controllers;

public class BuyersController : Controller
{
    //[HttpPost]
    //[Authorize(Roles = "Buyer")]
    //[AllowAnonymous]
    //public ProductDto[] GetProducts() =>
    //    _productService.GetProducts();
}

