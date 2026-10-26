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

    public async Task<User?> GetById(string id)
    {
        return await dc.Users.FirstOrDefaultAsync(user => user.UserId == id);
    }

    public async Task Update(User user)
    {
        await dc.UpdateAsync(user);
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