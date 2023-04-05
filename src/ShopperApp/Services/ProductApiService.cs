using Shopper.Domain;
using System.IO;
using System.Net.Http.Json;
namespace ShopperApp.Services;

public class ProductApiService
{
    private readonly HttpClient client;
    public ProductApiService(HttpClient client) => this.client = client;
    public Task<ICollection<Product>> GetAll() => client.GetFromJsonAsync<ICollection<Product>>("api/products");
    public Task<Product> GetById(int id) => client.GetFromJsonAsync<Product>($"api/products/{id}");
    public Task Update(Product product) => client.PutAsJsonAsync<Product>($"api/products/{product.Id}", product);
    public Task Remove(int id) => client.DeleteAsync($"api/products/{id}");

    public async Task Upload(Stream stream)
    {
        using var formData = new MultipartFormDataContent();
        using var fileContent = new StreamContent(stream);
        formData.Add(fileContent, "file", "image.png");
        var response = await client.PostAsync("api/upload", formData);
    }
}


