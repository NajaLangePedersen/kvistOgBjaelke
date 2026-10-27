using Infrastructure.Entities;

namespace Service.DTOs;

public class CreateUserDTO
{
    public string username { get; set; } = "";
    public string password { get; set; } = "";
    public UserRole role { get; set; }
}