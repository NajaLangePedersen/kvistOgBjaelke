using API;
using Infrastructure;
using LinqToDB;


var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=dev.db";
var options = new DataOptions().UseSQLite(connectionString);
var dataOptions = new DataOptions<MyDataConnection>(options);

builder.Services.AddScoped<MyDataConnection>(_=>
    new MyDataConnection(dataOptions));

builder.Services.ConfigureDomainServices(
    builder.Configuration["JwtSecret"]
    ?? throw new InvalidOperationException("JwtSecret is missing"));
    
builder.Services.AddScoped<MySeeder>();


builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ProblemExceptionHandler>();

builder.Services.AddControllers();

var app = builder.Build();

app.UseExceptionHandler();

app.UseCors(config => config.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(_ => true));

app.UseOpenApi();

app.UseSwaggerUi();

using (var scope = app.Services.CreateScope())
{
    var seeder = scope.ServiceProvider.GetRequiredService<MySeeder>();
    seeder.Seed();
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
