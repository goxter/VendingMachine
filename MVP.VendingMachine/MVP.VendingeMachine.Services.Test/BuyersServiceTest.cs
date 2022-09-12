using Bogus;
using Microsoft.AspNetCore.Identity;
using Moq;
using MVP.VendingeMachine.Repositories.Interfaces;
using MVP.VendingeMachine.Services.Interfaces;
using MVP.VendingMachine.DataModel.Models;
using System.Security.Claims;

namespace MVP.VendingeMachine.Services.Test;

[TestClass]
public class BuyersServiceTest
{
    private static MockRepository _mockRepository;
    private static Mock<IUsersRepository> _userRepository;
    private static Mock<UserManager<UserModel>> _userManager;
    private Faker<UserModel> _userFaker;
    private Faker<ClaimsPrincipal> _claimsFaker;

    private BuyersService _buyersService;


    [TestInitialize]
    public void TestInitialize()
    {
        _mockRepository = new MockRepository(MockBehavior.Default);
        _userRepository = _mockRepository.Create<IUsersRepository>();
        _userManager = GetMockUserManager();

        SetDataFakers();

        _buyersService = new BuyersService(_userRepository.Object, _userManager.Object);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _userRepository.Reset();
        _userManager.Reset();
    }

    [TestMethod]
    public void DepositTest_Succesfully()
    {
        var user = _userFaker.Generate();

        _userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(user));
        _userRepository.Setup(x => x.UpdateUser(It.IsAny<UserModel>())).Returns(true);

        var result = _buyersService.Deposit(100, _claimsFaker.Generate()).Result;

        Assert.IsTrue(result);
    }

    [TestMethod]
    public void DepositTest_Fails()
    {
        var user = _userFaker.Generate();

        _userManager.Setup(x => x.GetUserAsync(It.IsAny<ClaimsPrincipal>())).Returns(Task.FromResult(user));
        _userRepository.Setup(x => x.UpdateUser(It.IsAny<UserModel>())).Returns(false);

        var result = _buyersService.Deposit(100, _claimsFaker.Generate()).Result;

        Assert.IsFalse(result);
    }

    private void SetDataFakers()
    {
        _claimsFaker = new Faker<ClaimsPrincipal>();

        _userFaker = new Faker<UserModel>()
            .RuleFor(u => u.Id, f => f.Random.Guid().ToString())
            .RuleFor(u => u.Email, f => f.Person.Email)
            .RuleFor(u => u.Deposit, f => f.Random.CollectionItem(new int[] {5, 10, 20, 50, 100 }))
            .RuleFor(u => u.UserName, f => f.Person.UserName);
    }

    private Mock<UserManager<UserModel>> GetMockUserManager()
    {
        var userStoreMock = new Mock<IUserStore<UserModel>>();
        return new Mock<UserManager<UserModel>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
    }
}

