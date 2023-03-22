using Auth.Api.Domain;
using Auth.Api.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<IUserIdentityRepository, FakeUserIdentityRepository>();
builder.Services.AddSingleton<ITokenService, JwtTokenService>();
builder.Services.AddSingleton<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            new string[] { "https://localhost:7065", "http://localhost:5063" });
        policy.WithMethods(new string[] { "POST" });
        policy.AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello Auth Api!");

// POST /api/token/create
app.MapPost("/api/token/create", (LoginModel model, 
    IAuthService authService,
    ITokenService tokenService) =>
{
    if (authService.TryAuthorize(model.Username, model.Password, out var identity))
    {
        var token = tokenService.Create(identity);

        return Results.Ok(token);
    }

    return Results.BadRequest(new { message = "Invalid username or password" });

});


app.Run();
