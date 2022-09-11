using MVP.VendingMachine.Dto;
using System.Security.Claims;

namespace MVP.VendingeMachine.Services.Interfaces;

public interface IProductsService
{
    ProductDto[] GetProducts();

    bool AddProduct(ProductDto product, ClaimsPrincipal user);

    bool UpdateProduct(ProductDto product, ClaimsPrincipal user);

    bool DeleteProduct(Guid id, ClaimsPrincipal user);
}

