using Infrastructure.Entities;
using Infrastructure.Repositories.Interface;
using LinqToDB;
using LinqToDB.Async;
using SQLitePCL;

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

    public async Task Delete(User user)
    {
        await dc.DeleteAsync(user);
    }

    public async Task<List<User>> GetAll()
    {
        return await dc.Users.ToListAsync();
    }

    public async Task<User?> GetByUsername(string username)
    {
        return await dc.Users.FirstOrDefaultAsync(u => u.UserName == username);
    }
}