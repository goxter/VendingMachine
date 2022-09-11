using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel.DtoMappers;
using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MVP.VendingeMachine.Services;

public class AccountsService : IAccountsService
{
    private readonly UserManager<UserModel> _userManager;
    private readonly IConfiguration _configuration;
    private readonly IConfigurationSection _jwtSettings;

    public AccountsService(UserManager<UserModel> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
        _jwtSettings = _configuration.GetSection("JwtSettings");
    }

    public async Task<string> LoginUser(UserForAuthenticationDto userForAuthentication)
    {
        var user = await _userManager.FindByNameAsync(userForAuthentication.Email);

        if (user == null || !await _userManager.CheckPasswordAsync(user, userForAuthentication.Password))
            return null;
        
        var signingCredentials = GetSigningCredentials();
        var claims = GetClaims(user);
        var tokenOptions = GenerateTokenOptions(signingCredentials, claims);        
        var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

        return token;
    }

    public async Task<IdentityResult> RegisterUser(UserRegistrationDto userForRegistration)
    {
        var user = userForRegistration.ToModel();
        var result = await _userManager.CreateAsync(user, userForRegistration.Password);
        await _userManager.AddToRoleAsync(user, "Buyer");

        return result;
    }

    private SigningCredentials GetSigningCredentials()
    {
        var key = Encoding.UTF8.GetBytes(_jwtSettings["securityKey"]);
        var secret = new SymmetricSecurityKey(key);

        return new SigningCredentials(secret, SecurityAlgorithms.HmacSha256);
    }
    private List<Claim> GetClaims(IdentityUser user)
    {
        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, user.Email)
    };

        return claims;
    }
    private JwtSecurityToken GenerateTokenOptions(SigningCredentials signingCredentials, List<Claim> claims)
    {
        var tokenOptions = new JwtSecurityToken(
            issuer: _jwtSettings["validIssuer"],
            audience: _jwtSettings["validAudience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_jwtSettings["expiryInMinutes"])),
            signingCredentials: signingCredentials);

        return tokenOptions;
    }
}
