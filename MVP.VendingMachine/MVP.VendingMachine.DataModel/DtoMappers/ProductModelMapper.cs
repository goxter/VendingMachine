using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;

namespace MVP.VendingMachine.DataModel.DtoMappers;

public static class ProductModelMapper
{
    public static ProductDto ToDto(this ProductModel model) =>
        new()
        {
           Id = model.Id,
           AvailableAmount = model.AmmountAvailable,
           Name = model.ProductName,
           Price = model.Cost
        };

    public static ProductModel ToModel(this ProductDto model) =>
        new()
        {
            Id = model.Id,
            AmmountAvailable = model.AvailableAmount,
            ProductName = model.Name,
            Cost = model.Price
        };
}
