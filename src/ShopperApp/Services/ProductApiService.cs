using Shopper.Domain;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace ShopperApp.Services;

public class ProductApiService
{
    private readonly HttpClient client;
    public ProductApiService(HttpClient client) => this.client = client;
    public Task<IEnumerable<Product>> GetAll() => client.GetFromJsonAsync<IEnumerable<Product>>("api/products");
    public Task<Product> GetById(int id) => client.GetFromJsonAsync<Product>($"api/products/{id}");
    public Task Update(Product product) => client.PutAsJsonAsync<Product>($"api/products/{product.Id}", product);
}

