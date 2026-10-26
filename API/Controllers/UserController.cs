using System.Security.Claims;
using Infrastructure;
using Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTOs;

namespace API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(UserService service, IAuthenticationService authenticationService) : ControllerBase
{
    [Authorize(Roles = "SuperUser,Admin")]
    [HttpPut(nameof(Update))]
    public async Task<IActionResult> Update(UpdateRequestDTO request)
    {
        await service.Update(request, GetActorRole());
        return NoContent();
    }
    
    [HttpGet(nameof(GetUsers))]
    public async Task<List<User>> GetUsers()
    {
        return await service.GetAll();
    }
    
    
    [Authorize]
    [HttpGet(nameof(GetById))]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await service.GetById(id);
        return Ok(new { 
            user.UserId,
            user.UserName,
            user.Role
        });
    }

    [Authorize(Roles = "SuperUser,Admin")]
    [HttpPost(nameof(CreateUser))]
    public async Task<IActionResult> CreateUser(string username, string password, UserRole role)
    {
        await service.Insert(username, password, role, GetActorRole());
        return NoContent();
    }

    [Authorize(Roles = "SuperUser,Admin")]
    [HttpDelete(nameof(Delete))]
    public async Task<IActionResult> Delete(string id)
    {
        await service.Delete(id, GetActorRole());
        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDTO>> Login(
        LoginRequestDTO request)
    {
        string? token = await authenticationService.Login(
            request.Username,
            request.Password);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(new LoginResponseDTO
        {
            Token = token
        });
    }
    
    // reads the role from the token
    private UserRole GetActorRole()
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value; // reads claim from the token
        return Enum.TryParse<UserRole>(role, out var actorRole) // makes the text admin to UserRole.Admin
            ? actorRole
            : throw new ForbiddenException("Missing or invalid role");
    }
    
}