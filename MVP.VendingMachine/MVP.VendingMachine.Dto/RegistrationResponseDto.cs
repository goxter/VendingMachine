using System.Runtime.Serialization;

namespace MVP.VendingMachine.Dto;

[DataContract]
public class RegistrationResponseDto
{
    [DataMember(Name = "isSuccessfulRegistration")]
    public bool IsSuccessfulRegistration { get; set; }

    [DataMember(Name = "errors")]
    public IEnumerable<string> Errors { get; set; }
}

