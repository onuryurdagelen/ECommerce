using ECommerce.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Abstracts
{
    public interface IProductRepository: IGenericRepository<Product>
    {
        Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync();

        Task<IReadOnlyList<ProductType>> GetProductTypesAsync();
        Task<IReadOnlyCollection<Product>> GetProductsAsync();
     }
}
