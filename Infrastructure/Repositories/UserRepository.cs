using Infrastructure.Entities;
using LinqToDB;
using LinqToDB.Async;
using SQLitePCL;

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

    public async Task Delete(User user)
    {
        await dc.DeleteAsync(user);
    }

    public Task<List<User>> GetAll()
    {
        return dc.Users.ToListAsync();
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await dc.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}