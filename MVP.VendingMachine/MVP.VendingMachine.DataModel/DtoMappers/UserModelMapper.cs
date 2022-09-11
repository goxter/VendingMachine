using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingMachine.DataModel.DtoMappers;

public static class UserModelMapper
{
    public static UserModel ToModel(this UserRegistrationDto dto) =>
        new()
        {
            UserName = dto.Email,
            Email = dto.Email
        };
}

