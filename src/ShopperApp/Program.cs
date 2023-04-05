using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopperApp;
using ShopperApp.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// dotnet add package Microsoft.Extensions.Http
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddHttpClient<ProductApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7127"));
builder.Services.AddHttpClient<AuthApiService>(sp => sp.BaseAddress = new Uri("https://localhost:7272"));

// dotnet add package Blazored.FluentValidation


// dotnet add packaged Blazored.LocalStorage
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddAuthorizationCore(options =>
{
    options.AddPolicy("GoldPartner", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("license", "gold");        
    });
});

builder.Services.AddScoped<JwtTokenAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<JwtTokenAuthenticationStateProvider>());

await builder.Build().RunAsync();
