using Infrastructure.Entities;
using Infrastructure.Repositories;
using Service.Security;

namespace Service;

public class AuthenticationService(
    IUserRepository userRepository, 
    IPasswordHasher passwordHasher,
    ITokenService tokenService) : IAuthenticationService
{
    public async Task<string?> Login(
        string username,
        string password)
    {
        User? user = await userRepository.GetByUsername(username);

        if (user == null)
        {
            return null;
        }

        bool valid = passwordHasher.VerifyPassword(
            password,
            user.Password);

        if (!valid)
        {
            return null;
        }

        return tokenService.GenerateToken(user);
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}