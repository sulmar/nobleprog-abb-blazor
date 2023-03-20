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
}