using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Infrastructure.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Service.Security;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private const string Issuer = "Jwt:Issuer";
    private const string Audience = "Jwt:Audience";
    private readonly SymmetricSecurityKey _key = new(Encoding.UTF8.GetBytes(
        configuration["Jwt:Secret"] ?? throw new InvalidOperationException("JwtSecret is missing.")));

    public string GenerateToken(User user)
    {
        Claim[] claims =
        [
            new Claim(
                ClaimTypes.NameIdentifier,
                user.UserId),
            
            new Claim(
                ClaimTypes.Name,
                user.UserName),
            
            new Claim(
                ClaimTypes.Role,
                user.Role.ToString())
        ];
       

        var token = new JwtSecurityToken(
            issuer: Issuer,
            audience: Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: new SigningCredentials(_key, SecurityAlgorithms.HmacSha256));

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}