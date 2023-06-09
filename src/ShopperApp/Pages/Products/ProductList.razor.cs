﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Shopper.Domain;
using ShopperApp.Services;
using System.Net.Http.Json;

namespace ShopperApp.Pages.Products;

[Authorize]
public partial class ProductList
{
    [Inject]
    public ProductApiService Api { get; set; }

    private ICollection<Product> products;

    protected override async Task OnInitializedAsync()
    {
        products = await Api.GetAll();
    }

    private void Edit(int id)
    {
        NavigationManager.NavigateTo($"/products/edit/{id}");

    }

    private async Task SearchProducts(string searchText)
    {
        // TODO: search products by search text
    }

}
