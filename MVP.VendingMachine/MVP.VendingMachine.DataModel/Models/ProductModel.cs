using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVP.VendingMachine.DataModel.Models;

public class ProductModel : BaseModel
{
    public int AmmountAvailable { get; set; }

    public int Cost { get; set; }

    public string ProductName { get; set; } = string.Empty;

    public UserModel Seller { get; set; }
}

