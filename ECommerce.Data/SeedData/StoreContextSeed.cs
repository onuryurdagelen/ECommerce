using ECommerce.API.Entities;
using ECommerce.Data.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ECommerce.Data.SeedData
{
    public class StoreContextSeed
    {
        public static async Task SeedAsync(StoreContext context)
        {
            if(!context.ProductBrands.Any())
            {
                string currentDirectory = Directory.GetCurrentDirectory();
                string brandsData = File.ReadAllText("C:\\Users\\Administrator\\Desktop\\ECommerce\\ECommerce\\ECommerce.Data\\Infrastructure\\Data\\brands.json");

                List<ProductBrand> brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                context.ProductBrands.AddRange(brands);
            }
            if (!context.ProductTypes.Any())
            {
                string typesData = File.ReadAllText("C:\\Users\\Administrator\\Desktop\\ECommerce\\ECommerce\\ECommerce.Data\\Infrastructure\\Data\\types.json");

                List<ProductType> types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                context.ProductTypes.AddRange(types);
            }
            if (!context.Products.Any())
            {
                string productsData = File.ReadAllText("C:\\Users\\Administrator\\Desktop\\ECommerce\\ECommerce\\ECommerce.Data\\Infrastructure\\Data\\products.json");

                List<Product> products = JsonSerializer.Deserialize<List<Product>>(productsData);
                context.Products.AddRange(products);
            }
            if(context.ChangeTracker.HasChanges()) 
                await context.SaveChangesAsync();
        }
    }
}
