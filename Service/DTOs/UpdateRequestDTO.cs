using Infrastructure.Entities;
using LinqToDB.Mapping;

namespace Service.DTOs;

public class UpdateRequestDTO
{
    [NotNull] public string UserId { get; set;} = "";

    public string UserName { get; set; } = "";
    
    public string? NewPassword { get; set; }
    
    public UserRole? Role { get; set; }
}