using Infrastructure.Entities;
using LinqToDB.Async;

namespace Infrastructure.Repositories;

public class UserRepository(MyDataConnection dc) : IUserRepository
{
    public Task Insert(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task Update(User user)
    {
        throw new NotImplementedException();
    }

    public Task Delete(User user)
    {
        throw new NotImplementedException();
    }

    public Task<List<User>> GetAll()
    {
        return dc.Users.ToListAsync();
    }
}