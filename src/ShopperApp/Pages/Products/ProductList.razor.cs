using Microsoft.AspNetCore.Components;
using Shopper.Domain;
using ShopperApp.Services;
using System.Net.Http.Json;

namespace ShopperApp.Pages.Products;

public partial class ProductList
{
    [Inject]
    public ProductApiService Api { get; set; }

    private IEnumerable<Product> products;

    protected override async Task OnInitializedAsync()
    {
        products = await Api.GetAll();
    }

    private void Edit(int id)
    {
        NavigationManager.NavigateTo($"/products/edit/{id}");
    }

}
