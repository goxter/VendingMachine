using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingMachine.DataModel.Models;

public class ProductModel : BaseModel
{
    public int AmmountAvailable { get; set; }

    public int Cost { get; set; }

    public string ProductName { get; set; } = string.Empty;

    [ForeignKey(nameof(Seller))]
    public string SellerId { get; set; }
    public UserModel Seller { get; set; }
}

