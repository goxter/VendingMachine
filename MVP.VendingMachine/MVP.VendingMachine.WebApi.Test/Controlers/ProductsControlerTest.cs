using Bogus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MVP.VendingeMachine.Services;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel.Models;
using MVP.VendingMachine.Dto;
using MVP.VendingMachine.WebApi.Controllers;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;

namespace MVP.VendingMachine.WebApi.Test.Controlers;

[TestClass]
public class ProductsControlerTest
{
    private static MockRepository _mockRepository;
    private static Mock<IProductsService> _productsService;

    private Faker<ClaimsPrincipal> _claimsFaker;


    private ProductsController _productsController;

    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepository = new MockRepository(MockBehavior.Default);
        _productsService = _mockRepository.Create<IProductsService>();

        _productsController = new ProductsController(_productsService.Object);

        _claimsFaker = new Faker<ClaimsPrincipal>();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _mockRepository.VerifyAll();
        _productsService.Reset();
    }

    [TestMethod]
    public void BuyProductsTest()
    {
        _productsController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = _claimsFaker.Generate() }
        };

        _productsService.Setup(x => x.BuyProduct(It.IsAny<ProductToBuyDto>(), It.IsAny<ClaimsPrincipal>()))
            .Returns(Task.FromResult(true));

        var response = _productsController.BuyProducts(new ProductToBuyDto()).Result;
 
        Assert.IsInstanceOfType(response, typeof(Microsoft.AspNetCore.Mvc.OkResult));
    }

    [TestMethod]
    public void DeleteProductTest_Ok()
    {
        _productsController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = _claimsFaker.Generate() }
        };

        _productsService.Setup(x => x.DeleteProduct(It.IsAny<Guid>(), It.IsAny<ClaimsPrincipal>()))
            .Returns(true);

        var response = _productsController.DeleteProduct(new Guid());

        Assert.IsInstanceOfType(response, typeof(OkResult));
    }

    [TestMethod]
    public void DeleteProductTest_BadRequest()
    {
        _productsController.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = _claimsFaker.Generate() }
        };

        _productsService.Setup(x => x.DeleteProduct(It.IsAny<Guid>(), It.IsAny<ClaimsPrincipal>()))
            .Returns(false);

        var response = _productsController.DeleteProduct(new Guid());

        Assert.IsInstanceOfType(response, typeof(BadRequestResult));
    }
}

