using Infrastructure.Entities;
using LinqToDB;
using LinqToDB.Data;

namespace Infrastructure;

public class MyDataConnection(DataOptions<MyDataConnection> options) : DataConnection(options.Options)
{
    public ITable<User> Users => this.GetTable<User>();
}
