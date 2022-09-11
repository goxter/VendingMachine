using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVP.VendingeMachine.Services;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.Dto;
using System.Data;

namespace MVP.VendingMachine.WebApi.Controllers;

[Route("api/buyer")]
public class BuyersController : Controller
{
    private readonly int[] _validAmount = {5, 10, 20, 50, 100 };
    private readonly IBuyersService _buyerService;

    public BuyersController(IBuyersService buyerService)
    {
        _buyerService = buyerService;
    }

    [HttpPost("deposit")]
    [Authorize(Roles = "Buyer")]
    public async Task<IActionResult> Deposit(int depositAmount)
    {
        if (!_validAmount.Contains(depositAmount))
            return BadRequest("You can deposit only 5, 10, 20, 50 and 100 cent coins.");

        await _buyerService.Deposit(depositAmount, HttpContext.User);

        return Ok();
    }

    [HttpPost("reset")]
    [Authorize(Roles = "Buyer")]
    public async Task<IActionResult> ResetDeposit()
    {
        await _buyerService.ResetDeposit(HttpContext.User);

        return Ok();
    }
}

