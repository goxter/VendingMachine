using Microsoft.AspNetCore.Identity;
using MVP.VendingMachine.Dto;
using System.IdentityModel.Tokens.Jwt;

namespace MVP.VendingeMachine.Services.Interfaces;

public interface IAccountsService
{    
    Task<IdentityResult> RegisterUser(UserRegistrationDto userForRegistration);

    Task<string> LoginUser(UserForAuthenticationDto userForAuthentication);
}

