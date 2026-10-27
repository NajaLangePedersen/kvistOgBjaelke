using Infrastructure.Entities;

namespace Service.DTOs;

public class CreateUserDTO
{
    public string username;
    public string password;
    public UserRole role;
}