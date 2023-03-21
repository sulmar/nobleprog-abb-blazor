using Shopper.Domain;

namespace Shopper.Infrastructure;

public class InMemoryProductRepository : IProductRepository
{
    private readonly IDictionary<int, Product> _products;

    public InMemoryProductRepository(IEnumerable<Product> products)
    {
        _products = products.ToDictionary(p => p.Id);
    }

    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<Product>>(_products.Values.ToList());
    }

    public Task<Product> GetById(int id)
    {
        return Task.FromResult(_products[id]);  
    }

    public Task RemoveAsync(int id)
    {
         _products.Remove(id);

        return Task.CompletedTask;
    }

    public async Task UpdateAsync(Product entity)
    {
        await RemoveAsync(entity.Id);
        _products[entity.Id] = entity;
    }
}