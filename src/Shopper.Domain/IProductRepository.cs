using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Domain;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllAsync();   
}
