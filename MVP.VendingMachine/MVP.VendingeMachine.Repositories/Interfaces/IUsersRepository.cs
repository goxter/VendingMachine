using MVP.VendingMachine.DataModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingeMachine.Repositories.Interfaces;

public interface IUsersRepository
{
    bool UpdateUser(UserModel user);
}

