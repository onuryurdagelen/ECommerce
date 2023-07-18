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
        public ProductsWithTypesAndBrandsSpecification(int skip,int take)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            ApplyPaging(skip, take);
        }
        public ProductsWithTypesAndBrandsSpecification(string sort,int? brandId,int? typeId)
            :base(
                   x => 
                    (!brandId.HasValue || x.ProductBrandId == brandId) &&
                    (!typeId.HasValue || x.ProductBrandId == typeId)
                 )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
            AddOrderBy(x => x.Name);

            if(!string.IsNullOrEmpty(sort))
            {
                switch (sort)
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
