using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers;

public class UserController(UserService service) : ControllerBase
{
    [HttpGet(nameof(GetUsers))]
    public async Task<List<User>> GetUsers()
    {
        return await service.GetAll();
    }

    [HttpPost(nameof(CreateUser))]
    public async Task CreateUser(String username, String password, UserRole role, UserRole loggedInUserRole)
    {
        await service.Insert(username, password, role, loggedInUserRole);
    }
}