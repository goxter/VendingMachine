using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingeMachine.Services.Interfaces;

public interface IBuyersService
{
    Task<bool> Deposit(int depositAmount, ClaimsPrincipal user);

    Task<bool> ResetDeposit(ClaimsPrincipal user);
}

