using API;
using Infrastructure;
using Infrastructure.Repositories;
using LinqToDB;
using Service;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=dev.db";
var options = new DataOptions().UseSQLite(connectionString);
var dataOptions = new DataOptions<MyDataConnection>(options);

builder.Services.AddScoped<MyDataConnection>(_ => new MyDataConnection(dataOptions));
builder.Services.AddScoped<UserService>();


builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddControllers();


var app = builder.Build();

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(_ => true));

app.UseOpenApi();

app.UseSwaggerUi();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<MySeeder>();
    seeder.Seed();
}

app.Run();
