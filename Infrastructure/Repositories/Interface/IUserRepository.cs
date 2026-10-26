using Infrastructure.Entities;

namespace Infrastructure.Repositories.Interface;

public interface IUserRepository 
{
    Task Insert(User user);
    Task<User?> GetById(string id);
    Task Update(User user);
    Task Delete(User user);
    Task<List<User>> GetAll();
}