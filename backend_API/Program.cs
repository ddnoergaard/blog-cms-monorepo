using backend_API.Repositories;
using backend_API.Repositories.Interfaces;
using backend_API.Security;
using backend_API.Services;
using backend_API.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//REPOS
builder.Services.AddScoped<IUserRepo, UserRepo>();

//SERVICES
builder.Services.AddScoped<IUserService, UserService>();

//SECURITY
builder.Services.AddSingleton<PasswordHasher>();


builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
