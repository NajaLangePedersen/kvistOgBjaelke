using System.Security.Claims;
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
    [HttpGet(nameof(GetUsers))]
    public Task<List<User>> GetUsers()
    {
        return service.GetAll();
    }

    [Authorize(Roles = "SuperUser,Admin")]
    [HttpDelete(nameof(Delete))]
    public async Task<IActionResult> Delete(string id)
    {
        // Read the role of the logged-in user
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        
        // If the role is missing or invalid, deny it
        if (!Enum.TryParse<UserRole>(role, out var actorRole))
            return Forbid();

        var result = await service.Delete(id, actorRole);
        
        // Translate the service result into an HTTP response
        return result switch
        {
            DeleteResult.Success => NoContent(), // deleted success
            DeleteResult.NotFound => NotFound(), // user does not exist
            _ => Forbid() // not allowed

        };
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
}