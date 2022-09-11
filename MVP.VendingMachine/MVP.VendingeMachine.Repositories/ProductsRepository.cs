using Microsoft.EntityFrameworkCore;
using MVP.VendingeMachine.Repositories.Interfaces;
using MVP.VendingMachine.DataModel;
using MVP.VendingMachine.DataModel.Models;
using System.Security.Claims;

namespace MVP.VendingeMachine.Repositories;

public class ProductsRepository : IProductsRepository
{
    private readonly DataContext _dataContext;

    public ProductsRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool AddProduct(ProductModel product)
    {
        throw new NotImplementedException();
    }

    public bool DeleteProduct(ProductModel product)
    {
        _dataContext.Products.Remove(product);

        return SaveAll();
    }

    public IEnumerable<ProductModel> GetAllProducts() =>    
        _dataContext.Products.AsNoTracking();

    public ProductModel GetProduct(Guid id)
    {
        throw new NotImplementedException();
    }

    public bool UpdateProduct(ProductModel product, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }

    private bool SaveAll()
    {
        return _dataContext.SaveChanges() > 0;
    }
}
