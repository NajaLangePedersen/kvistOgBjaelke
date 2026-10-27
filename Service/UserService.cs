using Infrastructure;
using Infrastructure.Entities;
using Infrastructure.Repositories.Interface;
using Service.DTOs;
using Service.Security;

namespace Service;
public class UserService(IUserRepository userRepo, IPasswordHasher hasher)
{

    public async Task Insert(CreateUserDTO dto, UserRole actorRole)
    {
        if (string.IsNullOrWhiteSpace(dto.username) || string.IsNullOrWhiteSpace(dto.password))
            throw new ValidationException("Username and password are required");

        // The actor may only create roles they are allowed to manage
        if (!CanManage(actorRole, dto.role))
            throw new ForbiddenException("You are not allowed to create this role");

        await userRepo.Insert(new User
        {
            UserId = Guid.NewGuid().ToString(),
            UserName = dto.username,
            Password = hasher.HashAndSaltPassword(dto.password),
            Role = dto.role
        });
    }

    public async Task<User?> GetById(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            throw new ValidationException("Id cant be empty");
        return await userRepo.GetById(id) ??
               throw new NotFoundException("User not found");
    }

    public async Task Update(UpdateRequestDTO dto, UserRole actorRole)
    {
        var existing = await userRepo.GetById(dto.UserId)
                       ?? throw new NotFoundException("User not found");
        
        // Must be allowed to manage the current role...
        if (!CanManage(actorRole, existing.Role))
            throw new ForbiddenException("You are not allowed to update this user");

        
        if (dto.Role is not null && !CanManage(actorRole, dto.Role.Value))
            throw new ForbiddenException("You are not allowed to assign this role");

        if (!string.IsNullOrWhiteSpace(dto.UserName))
            existing.UserName = dto.UserName;               // was dto.UserName = dto.UserName

        if (!string.IsNullOrWhiteSpace(dto.NewPassword))
            existing.Password = hasher.HashAndSaltPassword(dto.NewPassword); // was written to the dto

        if (dto.Role is not null)
            existing.Role = dto.Role.Value;

        await userRepo.Update(existing);
    }
    
    public async Task Delete(string targetId, UserRole actorRole)
    {
        var target = await userRepo.GetById(targetId)
                     ?? throw new NotFoundException("User not found");

        if (!CanManage(actorRole, target.Role))
            throw new ForbiddenException("You are not allowed to delete this user");

        await userRepo.Delete(target);
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
