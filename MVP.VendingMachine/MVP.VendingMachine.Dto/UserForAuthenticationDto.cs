using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace MVP.VendingMachine.Dto;

[DataContract]
public class UserForAuthenticationDto
{
    [DataMember(Name = "email")]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    [DataMember(Name = "password")]
    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; }
}
