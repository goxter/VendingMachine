using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingMachine.Dto;

[DataContract]
public class AuthResponseDto
{
    [DataMember(Name = "isAuthSuccessfull")]
    public bool IsAuthSuccessfull { get; set; }

    [DataMember(Name = "errorMessage")]
    public string ErrorMessage { get; set; }

    [DataMember(Name = "token")]
    public string Token { get; set; }
}

