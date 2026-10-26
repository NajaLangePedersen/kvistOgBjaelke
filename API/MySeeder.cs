using Infrastructure;
using Infrastructure.Entities;
using LinqToDB;
using Service.Security;

namespace API;

public class MySeeder(MyDataConnection dc, IPasswordHasher passwordHasher)
{
    public void Seed()
    {
        dc.CreateTable<User>(tableOptions: TableOptions.CreateIfNotExists);

        if (dc.Users.Count() == 0)
            for (int i = 1; i <= 3; i++)
            {
                dc.Insert(new User
                {
                    UserId = Guid.NewGuid().ToString(),
                    UserName = i.ToString(),
                    Password = passwordHasher.HashAndSaltPassword("123"),
                    Role = UserRole.Admin
                });
            }

    }
}