using Bogus;
using Shopper.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopper.Infrastructure.Fakers;

// dotnet add package Bogus
public class ProductFaker : Faker<Product>
{
    public ProductFaker()
    {
        UseSeed(0);
        StrictMode(true);
        RuleFor(p => p.Id, f => f.IndexFaker);
        RuleFor(p => p.Name, f => f.Commerce.ProductName());
        RuleFor(p=>p.Description, f => f.Commerce.ProductDescription());
        RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price()));
        RuleFor(p => p.Size, f => f.PickRandom<Size>().OrNull(f, 0.4f));

        Ignore(p => p.PriceDiscounted);
        Ignore(p => p.Image); 


    }
}
