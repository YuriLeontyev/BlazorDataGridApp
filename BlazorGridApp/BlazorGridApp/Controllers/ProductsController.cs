using BlazorGridApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace BlazorGridApp.Controllers;

public class ProductsController : ODataController
{
    [HttpGet]
    public IActionResult Get(ODataQueryOptions<Product> options)
    {
        var query = GetAllProducts().AsQueryable();
        var settings = new ODataQuerySettings { EnsureStableOrdering = false };

        IQueryable filtered = query;
        if (options.Filter  != null) filtered = options.Filter.ApplyTo(filtered, settings);
        if (options.OrderBy != null) filtered = options.OrderBy.ApplyTo(filtered, settings);

        var totalCount = filtered.Cast<Product>().Count();
        var skip  = options.Skip?.Value ?? 0;
        var top   = options.Top?.Value  ?? 10;
        var items = filtered.Cast<Product>().Skip(skip).Take(top).ToList();

        return Ok(new PagedResult<Product> { Items = items, TotalCount = totalCount });
    }

    private static List<Product> GetAllProducts() =>
    [
        new Product { Id = 1,  Name = "iPhone 15 Pro",            Description = "Latest Apple flagship smartphone with A17 Pro chip",  IsActive = true,  Category = "Mobile Phones",   Cost = 999.99m,   ReleaseDate = new DateOnly(2023, 9, 22) },
        new Product { Id = 2,  Name = "Samsung Galaxy S24",       Description = "Premium Android smartphone with AI features",         IsActive = true,  Category = "Mobile Phones",   Cost = 799.99m,   ReleaseDate = new DateOnly(2024, 1, 17) },
        new Product { Id = 3,  Name = "MacBook Pro 16\"",         Description = "Powerful laptop with M3 Max processor",               IsActive = true,  Category = "Electronics",     Cost = 2499.00m,  ReleaseDate = new DateOnly(2023, 11, 7) },
        new Product { Id = 4,  Name = "Dell XPS 15",              Description = "High-performance Windows laptop",                    IsActive = true,  Category = "Electronics",     Cost = 1349.99m,  ReleaseDate = new DateOnly(2023, 3, 15) },
        new Product { Id = 5,  Name = "iPad Air",                 Description = "Versatile tablet for work and play",                 IsActive = true,  Category = "Tablets",         Cost = 599.00m,   ReleaseDate = new DateOnly(2024, 3, 8) },
        new Product { Id = 6,  Name = "Sony WH-1000XM5",          Description = "Premium noise-cancelling headphones",                IsActive = true,  Category = "Audio",           Cost = 349.99m,   ReleaseDate = new DateOnly(2022, 5, 20) },
        new Product { Id = 7,  Name = "Google Pixel 8",           Description = "Google's flagship phone with advanced AI",           IsActive = false, Category = "Mobile Phones",   Cost = 699.00m,   ReleaseDate = new DateOnly(2023, 10, 4) },
        new Product { Id = 8,  Name = "AirPods Pro",              Description = "Wireless earbuds with active noise cancellation",    IsActive = true,  Category = "Audio",           Cost = 249.00m,   ReleaseDate = new DateOnly(2023, 9, 22) },
        new Product { Id = 9,  Name = "Samsung 4K TV 65\"",       Description = "QLED smart TV with HDR",                            IsActive = true,  Category = "Electronics",     Cost = 1299.99m,  ReleaseDate = new DateOnly(2023, 2, 10) },
        new Product { Id = 10, Name = "PlayStation 5",            Description = "Next-gen gaming console",                           IsActive = true,  Category = "Gaming",          Cost = 499.99m,   ReleaseDate = new DateOnly(2020, 11, 12) },
        new Product { Id = 11, Name = "Xbox Series X",            Description = "Microsoft's powerful gaming console",               IsActive = true,  Category = "Gaming",          Cost = 499.99m,   ReleaseDate = new DateOnly(2020, 11, 10) },
        new Product { Id = 12, Name = "Nintendo Switch OLED",     Description = "Portable gaming console with vibrant display",       IsActive = true,  Category = "Gaming",          Cost = 349.99m,   ReleaseDate = new DateOnly(2021, 10, 8) },
        new Product { Id = 13, Name = "Canon EOS R5",             Description = "Professional mirrorless camera",                    IsActive = false, Category = "Cameras",         Cost = 3899.00m,  ReleaseDate = new DateOnly(2020, 7, 30) },
        new Product { Id = 14, Name = "GoPro Hero 12",            Description = "Action camera for adventures",                      IsActive = true,  Category = "Cameras",         Cost = 399.99m,   ReleaseDate = new DateOnly(2023, 9, 13) },
        new Product { Id = 15, Name = "Kindle Paperwhite",        Description = "E-reader with glare-free display",                  IsActive = true,  Category = "Electronics",     Cost = 139.99m,   ReleaseDate = new DateOnly(2023, 10, 11) },
        new Product { Id = 16, Name = "Apple Watch Series 9",     Description = "Smartwatch with health monitoring",                 IsActive = true,  Category = "Wearables",       Cost = 399.00m,   ReleaseDate = new DateOnly(2023, 9, 22) },
        new Product { Id = 17, Name = "Fitbit Charge 6",          Description = "Fitness tracker with heart rate monitoring",         IsActive = true,  Category = "Wearables",       Cost = 159.95m,   ReleaseDate = new DateOnly(2023, 9, 28) },
        new Product { Id = 18, Name = "Bose SoundLink",           Description = "Portable Bluetooth speaker",                        IsActive = true,  Category = "Audio",           Cost = 149.00m,   ReleaseDate = new DateOnly(2022, 8, 18) },
        new Product { Id = 19, Name = "Logitech MX Master 3S",    Description = "Ergonomic wireless mouse",                          IsActive = true,  Category = "Accessories",     Cost = 99.99m,    ReleaseDate = new DateOnly(2022, 5, 25) },
        new Product { Id = 20, Name = "Mechanical Keyboard RGB",  Description = "Gaming keyboard with customizable lighting",         IsActive = false, Category = "Accessories",     Cost = 129.99m,   ReleaseDate = new DateOnly(2021, 6, 14) },
        new Product { Id = 21, Name = "LG UltraWide Monitor",     Description = "34-inch curved display for productivity",           IsActive = true,  Category = "Electronics",     Cost = 799.99m,   ReleaseDate = new DateOnly(2022, 11, 3) },
        new Product { Id = 22, Name = "Anker PowerBank",          Description = "20000mAh portable charger",                         IsActive = true,  Category = "Accessories",     Cost = 55.99m,    ReleaseDate = new DateOnly(2023, 4, 19) },
        new Product { Id = 23, Name = "Ring Video Doorbell",      Description = "Smart doorbell with HD video",                      IsActive = true,  Category = "Smart Home",      Cost = 99.99m,    ReleaseDate = new DateOnly(2023, 1, 25) },
        new Product { Id = 24, Name = "Nest Thermostat",          Description = "Smart temperature control system",                  IsActive = true,  Category = "Smart Home",      Cost = 129.99m,   ReleaseDate = new DateOnly(2022, 10, 12) },
        new Product { Id = 25, Name = "Dyson V15 Vacuum",         Description = "Cordless stick vacuum with laser detection",         IsActive = false, Category = "Home Appliances", Cost = 749.99m,   ReleaseDate = new DateOnly(2021, 3, 24) },
        new Product { Id = 26, Name = "Instant Pot Duo",          Description = "Multi-functional pressure cooker",                  IsActive = true,  Category = "Home Appliances", Cost = 89.95m,    ReleaseDate = new DateOnly(2020, 9, 5) },
        new Product { Id = 27, Name = "Oculus Quest 3",           Description = "VR headset for immersive experiences",              IsActive = true,  Category = "Gaming",          Cost = 499.99m,   ReleaseDate = new DateOnly(2023, 10, 10) },
        new Product { Id = 28, Name = "DJI Mini 3 Pro",           Description = "Compact drone with 4K camera",                      IsActive = true,  Category = "Cameras",         Cost = 759.00m,   ReleaseDate = new DateOnly(2022, 5, 10) },
        new Product { Id = 29, Name = "Roku Streaming Stick",     Description = "4K streaming device",                               IsActive = true,  Category = "Electronics",     Cost = 49.99m,    ReleaseDate = new DateOnly(2023, 8, 15) },
        new Product { Id = 30, Name = "Philips Hue Starter Kit",  Description = "Smart lighting system with color control",           IsActive = true,  Category = "Smart Home",      Cost = 199.99m,   ReleaseDate = new DateOnly(2022, 3, 30) }
    ];
}
