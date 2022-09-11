using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using System.Security.Claims;

namespace MVP.VendingeMachine.Repositories.Interfaces;

public interface IProductsRepository
{
    IEnumerable<ProductModel> GetAllProducts();

    ProductModel GetProduct(Guid id);

    bool DeleteProduct(ProductModel product);

    bool AddProduct(ProductModel product);

    bool UpdateProduct(ProductModel product, ClaimsPrincipal user);
}

