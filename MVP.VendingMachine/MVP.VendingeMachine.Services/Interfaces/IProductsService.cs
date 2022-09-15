using MVP.VendingMachine.Dto;
using System.Security.Claims;

namespace MVP.VendingeMachine.Services.Interfaces;

public interface IProductsService
{
    ProductDto[] GetProducts();

    ResponseDto AddProduct(ProductDto product, ClaimsPrincipal user);

    ResponseDto UpdateProduct(ProductDto product, ClaimsPrincipal user);

    ResponseDto DeleteProduct(Guid id, ClaimsPrincipal user);

    Task<ResponseDto> BuyProduct(ProductToBuyDto product, ClaimsPrincipal user);
}

