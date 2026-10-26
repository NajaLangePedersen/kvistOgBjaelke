using Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Service;
using Service.DTOs;

namespace API.Controllers;

public class UserController(UserService service, IAuthenticationService authenticationService) : ControllerBase
{
    [HttpGet(nameof(GetUsers))]
    public Task<List<User>> GetUsers()
    {
        return service.GetAll();
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