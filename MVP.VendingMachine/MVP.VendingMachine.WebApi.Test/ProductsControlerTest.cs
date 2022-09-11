using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.Dto;
using MVP.VendingMachine.WebApi.Controllers;

namespace MVP.VendingMachine.WebApi.Test;

public class ProductsControlerTest
{
    private readonly Mock<IProductsService> _mockRepo;
    private readonly ProductsController _controller;

    public ProductsControlerTest()
    {
        _mockRepo = new Mock<IProductsService>();
        _controller = new ProductsController(_mockRepo.Object);
    }

    [Fact]
    public void BuyProducts_Returns_Ok()
    {
        var productToBuy = new ProductToBuyDto
        {
            Id = new Guid("c0d7b241-763b-4037-8f66-d3ea5695487b"),
            Amount = 2
        };

        var result = _controller.BuyProducts(productToBuy);

        result.Should().BeOfType<OkResult>();
    }
}
