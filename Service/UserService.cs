using System.ComponentModel.DataAnnotations;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interface;

namespace Service;

public class UserService(IUserRepository userRepo)
{

    public async Task Insert(String username, String password, UserRole role, UserRole loggedInUserRole)
    {
        if (loggedInUserRole is UserRole.SuperUser or UserRole.Admin)
        {
            var user = new User
            {
                UserName = username,
                Password = password,
                Role = role
            };
            
            await userRepo.Insert(user);
        }
        else
        {
            throw new ValidationException("Invalid role");
        }
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
        return await userRepo.GetAll();
    }

}
