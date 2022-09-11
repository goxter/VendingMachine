using Microsoft.AspNetCore.Identity;
using MVP.VendingeMachine.Repositories.Interfaces;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel;
using MVP.VendingMachine.DataModel.DtoMappers;
using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using System.Net.Http;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace MVP.VendingeMachine.Services;

public class ProductsService : IProductsService
{
    private readonly IProductsRepository _productsRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly UserManager<UserModel> _userManager;
    private readonly DataContext _dataContext;

    public ProductsService(IProductsRepository productsRepository, UserManager<UserModel> userManager, IUsersRepository usersRepository, DataContext dataContext)
    {
        _productsRepository = productsRepository;
        _userManager = userManager;
        _usersRepository = usersRepository;
        _dataContext = dataContext;
    }

    public bool AddProduct(ProductDto product, ClaimsPrincipal user)
    {
        var existingProduct = _productsRepository.GetProductByName(product.Name);
        var id = _userManager.GetUserId(user);

        if (existingProduct is null)
            return _productsRepository.AddProduct(product.ToModel(id));
        else
            throw new Exception("Product with same name already exists.");

        return false;
    }

    public async Task<bool> BuyProduct(ProductToBuyDto product, ClaimsPrincipal user)
    {
        var existingProduct = _productsRepository.GetProductForUpdate(product.Id);
        if(existingProduct is null)
            throw new Exception("Product doesn't exist.");

        if(existingProduct.AmmountAvailable < product.Amount)
            throw new Exception("The desired amount of the product is unavailable.");

        var buyer = await _userManager.GetUserAsync(user);

        if(buyer.Deposit < existingProduct.Cost*product.Amount)
            throw new Exception("Insufficient funds.");

        using var transaction = _dataContext.Database.BeginTransaction();

        try
        {
            existingProduct.AmmountAvailable -= product.Amount;
            _productsRepository.UpdateProduct(existingProduct);

            buyer.Deposit -= product.Amount*existingProduct.Cost;
            _usersRepository.UpdateUser(buyer);

            transaction.Commit();

            return true;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            throw ex;
        }

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
            throw new Exception("Product with specified id doesn't exists");

        if (productToUpdate.Seller.Id == _userManager.GetUserId(user))
        {
            productToUpdate.ProductName = product.Name;
            productToUpdate.AmmountAvailable = product.AvailableAmount;
            productToUpdate.Cost = product.Price;

            return _productsRepository.UpdateProduct(productToUpdate);
        }            

        return false;
    }
}

