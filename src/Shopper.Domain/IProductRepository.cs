using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Domain;



public interface IEntityRepository<T>
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetById(int id);
    Task UpdateAsync(T entity);
    Task RemoveAsync(int id);
}


public interface IProductRepository : IEntityRepository<Product>
{

}
