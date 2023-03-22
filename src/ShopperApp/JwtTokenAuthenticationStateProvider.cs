using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.IdentityModel.Tokens;
using ShopperApp.Models;
using ShopperApp.Services;
using System.Diagnostics.SymbolStore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShopperApp;

public class JwtTokenAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly AuthApiService Api;
    private readonly ILocalStorageService storageService;

    public JwtTokenAuthenticationStateProvider(AuthApiService authApiService,
        ILocalStorageService storageService)
    {
        this.Api = authApiService;
        this.storageService = storageService;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var state = new AuthenticationState(new System.Security.Claims.ClaimsPrincipal());

        var token = await storageService.GetItemAsStringAsync(TokenKey);

        // dotnet add package System.IdentityModel.Tokens.Jwt

        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            if (tokenHandler.CanReadToken(token))
            {
                var jwtToken = tokenHandler.ReadJwtToken(token);

                var secretKey = "your-256-bit-secret";
                var key = Encoding.ASCII.GetBytes(secretKey);

                try
                {
                    tokenHandler.ValidateToken(token, new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ClockSkew = TimeSpan.Zero,
                        ValidateLifetime = true
                    }, out var validatedToken);

                    var identity = new ClaimsIdentity(jwtToken.Claims, "JWT Token");
                    state = new AuthenticationState(new ClaimsPrincipal(identity));
                }
                catch (Exception ex)
                {
                    throw;
                    await LogoutAsync();
                }
            }
        }
       
      

        return state;
    }

    private const string TokenKey = "token";
    public async Task LoginAsync(LoginModel model)
    {
        var token = await Api.CreateTokenAsync(model);

        await storageService.SetItemAsStringAsync(TokenKey, token);

        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }

    public async Task LogoutAsync()
    {
        await storageService.RemoveItemAsync(TokenKey);
        NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
    }
}
