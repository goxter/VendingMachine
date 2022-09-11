using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MVP.VendingMachine.Dto
;

[DataContract]
public class ProductToBuyDto
{
    [DataMember(Name = "id")]
    public Guid Id { get; set; }

    [DataMember(Name = "amount")]
    public int Amount { get; set; }
}

