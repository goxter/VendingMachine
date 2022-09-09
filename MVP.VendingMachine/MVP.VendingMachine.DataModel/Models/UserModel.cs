using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingMachine.DataModel.Models;

public class UserModel : IdentityUser
{
    public int Deposit { get; set; }
}

