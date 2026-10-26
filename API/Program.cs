using API;
using Infrastructure;
using Infrastructure.Repositories;
using LinqToDB;
using Service;
using Service.Security;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=dev.db";
var options = new DataOptions().UseSQLite(connectionString);
var dataOptions = new DataOptions<MyDataConnection>(options);

builder.Services.AddScoped<MyDataConnection>(_ => new MyDataConnection(dataOptions));
builder.Services.AddScoped<IPasswordHasher, Argon2PasswordHasher>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<MySeeder>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddControllers();

builder.Services.AddOpenApiDocument(settings => settings.SchemaSettings.SchemaProcessors.Add(new RequireNotNullableSchemaProcessor()));

var app = builder.Build();

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(_ => true));

app.UseOpenApi();

app.UseSwaggerUi();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<MySeeder>();
    seeder.Seed();
}

app.MapControllers();

app.Run();
