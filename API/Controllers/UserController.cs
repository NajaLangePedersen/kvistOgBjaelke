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
    [Authorize(Roles = "SuperUser,Admin")]
    [HttpPut(nameof(Update))]
    public async Task<IActionResult> Update(UpdateRequestDTO dto)
    {
        var role = User.FindFirst(ClaimTypes.Role)?.Value;
        if (!Enum.TryParse<UserRole>(role, out var actorRole))
            return Forbid();

        var result = await service.Update(dto, actorRole);

        return result switch
        {
            OperationResult.Success => NoContent(),
            OperationResult.NotFound => NotFound(),
            _ => Forbid()
        };
    }
    
    [HttpGet(nameof(GetUsers))]
    public async Task<List<User>> GetUsers()
    {
        return await service.GetAll();
    }

    [HttpGet(nameof(GetById))]
    public async Task<IActionResult> GetById(string id)
    {
        var user = await service.GetById(id);

        if (user is null)
            return NotFound();
        
        return Ok(new
        { 
            user.UserId,
            user.UserName,
            user.Role
        });
    }

    [HttpPost(nameof(CreateUser))]
    public async Task CreateUser(String username, String password, UserRole role, UserRole loggedInUserRole)
    {
        await service.Insert(username, password, role, loggedInUserRole);
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
            OperationResult.Success => NoContent(), // deleted success
            OperationResult.NotFound => NotFound(), // user does not exist
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