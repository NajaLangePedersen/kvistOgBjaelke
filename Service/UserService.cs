using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Service;

public class UserService(IUserRepository userRepo)
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
        return userRepo.GetAll();
    }

}
