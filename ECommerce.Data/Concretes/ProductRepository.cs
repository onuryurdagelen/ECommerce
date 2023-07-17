using ECommerce.API.Entities;
using ECommerce.Data.Abstracts;
using ECommerce.Data.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Concretes
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        private readonly StoreContext _context;

        public ProductRepository(StoreContext context):base(context)
        {
            _context = context;
        }

        public async Task<IReadOnlyList<ProductBrand>> GetProductBrandsAsync()
        {
            return await _context.ProductBrands.ToListAsync();
        }

        public async Task<IReadOnlyCollection<Product>> GetProductsAsync()
        {
            return await _context.Products
                 .Include(p => p.ProductType)
                 .Include(p => p.ProductBrand)
                 .ToListAsync();
        }

        public async Task<IReadOnlyList<ProductType>> GetProductTypesAsync()
        {
            return await _context.ProductTypes.ToListAsync();
        }
    }
}
