using ShopperApp.Models;
using System.Net.Http.Json;

namespace ShopperApp.Services;

public class AuthApiService
{
    private readonly HttpClient client;
    public AuthApiService(HttpClient client) => this.client = client;

    public async Task<string> CreateTokenAsync(LoginModel model)
    {
        var response = await client.PostAsJsonAsync("api/token/create", model);

        var token = await response.Content.ReadFromJsonAsync<string>();

        return token;
    }
}

