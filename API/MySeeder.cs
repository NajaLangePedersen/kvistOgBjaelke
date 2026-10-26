using Infrastructure;
using Infrastructure.Entities;
using LinqToDB;

namespace API;

public class MySeeder(MyDataConnection dc)
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
                    Password = "123",
                    Role = UserRole.Admin
                });
            }

    }
}