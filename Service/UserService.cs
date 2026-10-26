using System.ComponentModel.DataAnnotations;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interface;
using Service.DTOs;
using Service.Security;

namespace Service;

public enum OperationResult
{
    Success,
    NotFound,
    Forbidden,
}
public class UserService(IUserRepository userRepo, IPasswordHasher hasher)
{

    public async Task Insert(String username, String password, UserRole role, UserRole loggedInUserRole)
    {
        if (loggedInUserRole is UserRole.SuperUser or UserRole.Admin)
        {
            var user = new User
            {
                UserName = username,
                Password = password,
                Role = role
            };
            
            await userRepo.Insert(user);
        }
        else
        {
            throw new ValidationException("Invalid role");
        }
    }

    public async Task<User?> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ValidationException("Id cant be empty");
        return await userRepo.GetById(id);
    }

    public async Task <OperationResult> Update(UpdateRequestDTO dto, UserRole actorRole)
    {
        var existing = await userRepo.GetById(dto.UserId);

        if (existing is null)
            return OperationResult.NotFound;

        if (!CanManage(actorRole, existing.Role))
            return OperationResult.Forbidden;

        if (dto.Role is not null && !CanManage(actorRole, dto.Role.Value))
            return OperationResult.Forbidden;

        if (!string.IsNullOrWhiteSpace(dto.UserName))
            dto.UserName = dto.UserName;

        if (!string.IsNullOrWhiteSpace(dto.NewPassword))
            dto.NewPassword = hasher.HashAndSaltPassword(dto.NewPassword);

        if (dto.Role is not null)
            existing.Role = dto.Role.Value;

        await userRepo.Update(existing);
        return OperationResult.Success;
    }
    
    public async Task <OperationResult> Delete(string targetId, UserRole actorRole)
    {
        var target = await userRepo.GetById(targetId);
        
        // Stops if the user does not exist
        if (target is null)
            return OperationResult.NotFound;

        // Stop if the acting role is not allowed to delete the target user.
        if (!CanManage(actorRole, target.Role))
            return OperationResult.Forbidden;
        
        await userRepo.Delete(target);
        return OperationResult.Success;


    }

    public async Task<List<User>> GetAll()
    {
        return await userRepo.GetAll();
    }

    // Admin can delete all roles
    // Superadmin can only delete Users
    // User cannot dele anyone
    private static bool CanManage(UserRole actor, UserRole target) => actor switch
    {
        UserRole.Admin => true,
        UserRole.SuperUser => target == UserRole.User,
        _ => false
    };

}
