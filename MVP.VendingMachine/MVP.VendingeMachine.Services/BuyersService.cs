using Microsoft.AspNetCore.Identity;
using MVP.VendingeMachine.Repositories.Interfaces;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel.Models;
using System.Security.Claims;

namespace MVP.VendingeMachine.Services;

public class BuyersService : IBuyersService
{
    private readonly IUsersRepository _usersRepository;
    private readonly UserManager<UserModel> _userManager;

    public BuyersService(IUsersRepository usersRepository, UserManager<UserModel> userManager)
    {
        _usersRepository = usersRepository;
        _userManager = userManager;
    }

    public async Task<bool> Deposit(int depositAmount, ClaimsPrincipal user)
    {
        var buyer = await _userManager.GetUserAsync(user);
        buyer.Deposit += depositAmount;

        return _usersRepository.UpdateUser(buyer);
    }

    public async Task<bool> ResetDeposit(ClaimsPrincipal user)
    {
        var buyer = await _userManager.GetUserAsync(user);
        if (buyer.Deposit == 0)
            return true;

        buyer.Deposit = 0;

        return _usersRepository.UpdateUser(buyer);
    }
}

