using Infrastructure.Entities;
using Microsoft.AspNetCore.Mvc;
using Service;

namespace API.Controllers;

public class UserController(UserService service) : ControllerBase
{
    [HttpGet(nameof(GetUsers))]
    public Task<List<User>> GetUsers()
    {
        return service.GetAll();
    }
}