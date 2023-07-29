using ECommerce.API.Entities;
using ECommerce.Data.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Data
{
    public class ProductsWithTypesAndBrandsSpecification : BaseSpecification<Product>
    {
        public ProductsWithTypesAndBrandsSpecification()
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

        }
       
        public ProductsWithTypesAndBrandsSpecification(ProductSpecParams productSpecParams)
            :base(
                   x => 
                    (string.IsNullOrEmpty(productSpecParams.Search) || x.Name.ToLower().Contains(productSpecParams.Search)) && 
                    (!productSpecParams.Price.HasValue || x.Price <= productSpecParams.Price) &&
                    (!productSpecParams.BrandId.HasValue || x.ProductBrandId == productSpecParams.BrandId) &&
                    (!productSpecParams.TypeId.HasValue || x.ProductTypeId == productSpecParams.TypeId)
                 )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(x => x.Name);
            ApplyPaging(productSpecParams.PageSize * (productSpecParams.PageIndex - 1), productSpecParams.PageSize);
            


            if(!string.IsNullOrEmpty(productSpecParams.Sort))
            {
                switch (productSpecParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;

                }
            }

        }
        public ProductsWithTypesAndBrandsSpecification(Expression<Func<Product, bool>> expression):base(expression)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
