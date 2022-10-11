using Microsoft.AspNetCore.Identity;
using MVP.VendingeMachine.Repositories.Interfaces;
using MVP.VendingeMachine.Services.Helpers;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel;
using MVP.VendingMachine.DataModel.DtoMappers;
using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using System;
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

    public ResponseDto AddProduct(ProductDto product, ClaimsPrincipal user)
    {
        var existingProduct = _productsRepository.GetProductByName(product.Name);
        var id = _userManager.GetUserId(user);
        var result = new ResponseDto();
        if (existingProduct is null)
            result.IsSuccess = _productsRepository.AddProduct(product.ToModel(id));
        else
        {
            result.IsSuccess = false;
            result.Message = "Product with same name already exists.";
        }

        return result;
    }

    public async Task<ResponseDto> BuyProduct(ProductToBuyDto product, ClaimsPrincipal user)
    {
        var existingProduct = _productsRepository.GetProductForUpdate(product.Id);
        if (existingProduct is null)
            return new ResponseDto { IsSuccess = false, Message = "Product doesn't exist." };

        if(existingProduct.AmmountAvailable < product.Amount)
            return new ResponseDto { IsSuccess = false, Message = "The desired amount of the product is unavailable." };

        var buyer = await _userManager.GetUserAsync(user);

        if(buyer.Deposit < existingProduct.Cost*product.Amount)
            return new ResponseDto { IsSuccess = false, Message = "Insufficient funds." };

        using var transaction = _dataContext.Database.BeginTransaction();

        var result = new ResponseDto();
        try
        {
            existingProduct.AmmountAvailable -= product.Amount;
            _productsRepository.UpdateProduct(existingProduct);

            var finalPrice = product.Amount * existingProduct.Cost;
            buyer.Deposit -= finalPrice;
            _usersRepository.UpdateUser(buyer);

            transaction.Commit();

            var change = CoinChangeHelper.GetChange(buyer.Deposit);
            result.Message = $"You bought {existingProduct.ProductName} for {finalPrice}. Here is your change: {string.Join(", ", change)}";
            result.IsSuccess = true;
        }
        catch (Exception ex)
        {
            transaction.Rollback();
            result.IsSuccess = false;
            result.Message = "Something went wrong. Please contact system administrator.";
            //log exception throw ex;
        }

        return result;
    }

    public ResponseDto DeleteProduct(Guid id, ClaimsPrincipal user)
    {
        var product = _productsRepository.GetProduct(id);

        var result = new ResponseDto { IsSuccess = false};
        if (product.Seller.Id == _userManager.GetUserId(user))
            result.IsSuccess = _productsRepository.DeleteProduct(product);

        return result;
    }

    public ProductDto[] GetProducts() =>    
        _productsRepository.GetAllProducts().Select(p => p.ToDto()).ToArray();

    public ResponseDto UpdateProduct(ProductDto product, ClaimsPrincipal user)
    {
        var result = new ResponseDto { IsSuccess = false };
        if (product is null)
            return result;

        var productToUpdate = _productsRepository.GetProduct(product.Id);

        if (productToUpdate is null)
        {
            result.Message = "Product with specified id doesn't exists";
            return result;
        }

        if (productToUpdate.Seller.Id == _userManager.GetUserId(user))
        {
            productToUpdate.ProductName = product.Name;
            productToUpdate.AmmountAvailable = product.AvailableAmount;
            productToUpdate.Cost = product.Price;

            result.IsSuccess = _productsRepository.UpdateProduct(productToUpdate);
        }            

        return result;
    }
}

