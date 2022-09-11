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
        _dataContext.Products.Add(product);

        return SaveAll();
    }

    public bool DeleteProduct(ProductModel product)
    {
        _dataContext.Products.Remove(product);

        return SaveAll();
    }

    public IEnumerable<ProductModel> GetAllProducts() =>    
        _dataContext.Products.AsNoTracking();

    public ProductModel GetProduct(Guid id) =>
        _dataContext.Products.Include(product => product.Seller).AsNoTracking().FirstOrDefault(product => product.Id == id);

    public ProductModel GetProductByName(string productName) =>
        _dataContext.Products.AsNoTracking().FirstOrDefault(product => product.ProductName == productName);

    public bool UpdateProduct(ProductModel product, ClaimsPrincipal user)
    {
        throw new NotImplementedException();
    }

    private bool SaveAll()
    {
        return _dataContext.SaveChanges() > 0;
    }
}
