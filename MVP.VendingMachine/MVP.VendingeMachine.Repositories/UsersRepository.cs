using MVP.VendingeMachine.Repositories.Interfaces;
using MVP.VendingMachine.DataModel;
using MVP.VendingMachine.DataModel.Models;

namespace MVP.VendingeMachine.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly DataContext _dataContext;

    public UsersRepository(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool UpdateUser(UserModel user)
    {
        _dataContext.Users.Update(user);

        return SaveAll();
    }

    private bool SaveAll()
    {
        return _dataContext.SaveChanges() > 0;
    }
}

