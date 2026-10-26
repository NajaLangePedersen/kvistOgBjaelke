using LinqToDB.Mapping;

namespace Infrastructure.Entities;

public class User
{
    [PrimaryKey] public string UserId { get; set; } = "";
    [Column] public string UserName { get; set; } = "";
    [Column] public string Password { get; set; } = "";

    public UserRole Role { get; set; } = UserRole.User;

}