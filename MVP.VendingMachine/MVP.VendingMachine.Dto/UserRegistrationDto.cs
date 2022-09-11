using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Xml.Linq;

namespace MVP.VendingMachine.Dto;

[DataContract]
public class UserRegistrationDto
{
    [DataMember(Name = "email")]
    [Required(ErrorMessage = "Email is required.")]
    public string Email { get; set; }

    [DataMember(Name = "password")]
    [Required(ErrorMessage = "Password is required")]
    public string Password { get; set; }

    [DataMember(Name = "confirmPassword")]
    [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
    public string ConfirmPassword { get; set; }
}
