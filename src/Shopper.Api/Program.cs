using Bogus;
using Shopper.Domain;
using Shopper.Infrastructure;
using Shopper.Infrastructure.Fakers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<Faker<Product>, ProductFaker>();

builder.Services.AddSingleton<IEnumerable<Product>>(sp =>
{
    var faker = sp.GetRequiredService<Faker<Product>>();
    return faker.Generate(100);

    return new List<Product>()
    {
        new Product { Id = 1, Name = "Product 1" },
        new Product { Id = 2, Name = "Product 2" },
        new Product { Id = 3, Name = "Product 3" },
    };
});
builder.Services.AddSingleton<IProductRepository, InMemoryProductRepository>();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(
            new string[] { "https://localhost:7065", "http://localhost:5063" });
        policy.WithMethods(new string[] { "GET", "PUT", "DELETE" });
        policy.AllowAnyHeader();
    });
});



var app = builder.Build();

app.UseCors();

app.MapGet("/", () => "Hello World!");


// GET api/products

app.MapGet("/api/products", async (IProductRepository repository)
    => await repository.GetAllAsync());

app.MapGet("/api/products/{id:int}", async (int id, IProductRepository repository) => await repository.GetById(id));

app.MapPut("/api/products/{id:int}", async(Product product, IProductRepository repository) => await repository.UpdateAsync(product));

app.MapDelete("/api/products/{id:int}", async (int id, IProductRepository repository) => await repository.RemoveAsync(id));

app.Run();
