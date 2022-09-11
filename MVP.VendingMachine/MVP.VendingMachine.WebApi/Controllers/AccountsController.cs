using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVP.VendingeMachine.Repositories;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel.DtoMappers;
using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace MVP.VendingMachine.WebApi.Controllers;


[Route("api/accounts")]
[ApiController]
public class AccountsController : ControllerBase
{
    private readonly IAccountsService _accountService;
    public AccountsController(IAccountsService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("registration")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegistrationDto userForRegistration)
    {
        if (userForRegistration == null || !ModelState.IsValid)
            return BadRequest();

        var result = await _accountService.RegisterUser(userForRegistration);
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Description);
            return BadRequest(new RegistrationResponseDto { Errors = errors });
        }

        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserForAuthenticationDto userForAuthentication)
    {
        if (userForAuthentication == null || !ModelState.IsValid)
            return BadRequest();

        var token = await _accountService.LoginUser(userForAuthentication);
        if (token is null)        
            return Unauthorized(new AuthResponseDto { ErrorMessage = "Invalid Authentication" });
        

        return Ok(new AuthResponseDto { IsAuthSuccessfull = true, Token = token });
    }
}
