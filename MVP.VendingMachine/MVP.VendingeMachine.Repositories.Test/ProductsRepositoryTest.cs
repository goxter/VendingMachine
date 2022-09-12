using Bogus;
using Microsoft.EntityFrameworkCore;
using Moq;
using MVP.VendingeMachine.Repositories.Interfaces;
using MVP.VendingMachine.DataModel;
using MVP.VendingMachine.DataModel.Models;
using System.Security.Claims;

namespace MVP.VendingeMachine.Repositories.Test;

[TestClass]
public class ProductsRepositoryTest
{
    private Faker<ProductModel> _productFaker;

    private ProductsRepository _productsRepository;

    [TestInitialize]
    public void TestInitialize()
    {
        SetDataFakers();
    }

    [TestMethod]
    public void GetAllProductsTest()
    {
        var expectedProduct = _productFaker.Generate();

        var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "ProductsTest")
                .Options;

        using (var context = new DataContext(options))
        {
            context.Products.Add(expectedProduct);
            context.SaveChanges();
        }

        using (var context = new DataContext(options))
        {
            _productsRepository = new ProductsRepository(context);
            var actualData = _productsRepository.GetAllProducts();
            var actualProduct = actualData.First();

            Assert.AreEqual(expectedProduct.Id, actualProduct.Id);
            Assert.AreEqual(expectedProduct.SellerId, actualProduct.SellerId);
            Assert.AreEqual(expectedProduct.ProductName, actualProduct.ProductName);
            Assert.AreEqual(expectedProduct.AmmountAvailable, actualProduct.AmmountAvailable);
            Assert.AreEqual(expectedProduct.Cost, actualProduct.Cost);
        }
    }

    private void SetDataFakers()
    {
        _productFaker = new Faker<ProductModel>()
            .RuleFor(u => u.Id, f => f.Random.Guid())
            .RuleFor(u => u.SellerId, f => f.Random.Guid().ToString())
            .RuleFor(u => u.ProductName, f => f.Random.String())
            .RuleFor(u => u.AmmountAvailable, f => f.Random.Int(0))
            .RuleFor(u => u.Cost, f => f.Random.CollectionItem(new int[] { 5, 10, 20, 50, 100 }));
    }
}
