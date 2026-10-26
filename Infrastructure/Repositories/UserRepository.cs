using Infrastructure.Entities;
using Infrastructure.Repositories.Interface;
using LinqToDB;
using LinqToDB.Async;

namespace Infrastructure.Repositories;

public class UserRepository(MyDataConnection dc) : IUserRepository
{
    public async Task Insert(User user)
    {
        await dc.InsertAsync(user);
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

    public async Task<List<User>> GetAll()
    {
        return await dc.Users.ToListAsync();
    }
}