using MVP.VendingMachine.DataModel.Interfaces;

namespace MVP.VendingMachine.DataModel.Models;

public class BaseModel : IObject
{
    public Guid Id { get; set; }
}

