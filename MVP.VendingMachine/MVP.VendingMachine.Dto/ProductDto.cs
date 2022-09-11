using System.Runtime.Serialization;


namespace MVP.VendingMachine.Dto;

[DataContract]
public class ProductDto
{
    [DataMember(Name = "id")]
    public Guid Id { get; set; }

    [DataMember(Name = "availableAmount")]
    public int AvailableAmount { get; set; }

    [DataMember(Name = "name")]
    public string Name { get; set; }

    [DataMember(Name = "price")]
    public int Price { get; set; }
}

