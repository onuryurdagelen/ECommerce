using ECommerce.API.Entities;
using ECommerce.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Data
{
    public class ProductWithFiltersForCountSpecification:BaseSpecification<Product>
    {
        public ProductWithFiltersForCountSpecification(ProductSpecParams productSpecParams)
            :base(x =>
                     (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) &&
                    (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                    (!productSpecParams.TypeId.HasValue || x.ProductBrandId == productSpecParams.TypeId))
        {
            
        }
    }
}
