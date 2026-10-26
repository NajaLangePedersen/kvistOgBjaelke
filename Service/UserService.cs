using Infrastructure.Entities;
using Infrastructure.Repositories;

namespace Service;

public enum DeleteResult
{
    Success,
    NotFound,
    Forbidden,
}
public class UserService(IUserRepository userRepo)
{

    public Task Insert(User user)
    {
        throw new NotImplementedException();
    }

    public Task<User?> GetById(string id)
    {
        throw new NotImplementedException();
    }

    public Task Update(User user)
    {
        throw new NotImplementedException();
    }
    
    public async Task <DeleteResult> Delete(string targetId, UserRole actorRole)
    {
        var target = await userRepo.GetById(targetId);
        
        // Stops if the user does not exist
        if (target is null)
            return DeleteResult.NotFound;

        // Stop if the acting role is not allowed to delete the target user.
        if (!CanManage(actorRole, target.Role))
            return DeleteResult.Forbidden;
        
        await userRepo.Delete(target);
        return DeleteResult.Success;


    }

    public Task<List<User>> GetAll()
    {
        return userRepo.GetAll();
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
