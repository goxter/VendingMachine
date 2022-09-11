using Microsoft.AspNetCore.Identity;
using MVP.VendingeMachine.Repositories.Interfaces;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel.DtoMappers;
using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using System.Net.Http;
using System.Security.Claims;

namespace MVP.VendingeMachine.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;
    private readonly UserManager<UserModel> _userManager;

    public ProductsService(IProductsRepository productsRepository, UserManager<UserModel> userManager)
    {
        _productsRepository = productsRepository;
        _userManager = userManager;
    }

    public bool AddProduct(ProductDto product, ClaimsPrincipal user)
    {
        var existingProduct = _productsRepository.GetProductByName(product.Name);
        var id = _userManager.GetUserId(user);

        if (existingProduct is null)
            return _productsRepository.AddProduct(product.ToModel(id));
        else
            throw new Exception("Product with same name already exists");

        return false;
    }

    public bool DeleteProduct(Guid id, ClaimsPrincipal user)
    {
        var product = _productsRepository.GetProduct(id);
        
        if (product.Seller.Id == _userManager.GetUserId(user))
            return _productsRepository.DeleteProduct(product);

        return false;
    }

    public ProductDto[] GetProducts() =>    
        _productsRepository.GetAllProducts().Select(p => p.ToDto()).ToArray();

    public bool UpdateProduct(ProductDto product, ClaimsPrincipal user)
    {
        if (product is null)
            return false;

        var productToUpdate = _productsRepository.GetProduct(product.Id);

        if (productToUpdate is null)
            return false;

        if (productToUpdate.Seller.Id == _userManager.GetUserId(user))
            return _productsRepository.UpdateProduct(productToUpdate, user);

        return false;
    }
}

