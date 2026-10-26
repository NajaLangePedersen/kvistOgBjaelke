using Infrastructure.Entities;

namespace Service.Security;

public interface ITokenService
{
    string GenerateToken(User user);
}