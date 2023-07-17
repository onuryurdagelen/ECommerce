using ECommerce.API.Entities;
using ECommerce.Data.Specifications;
using ECommerce.Entity.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Data
{
    public class SpecificationEvaluator<T> where T :BaseEntity
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery,ISpecification<T> spec)
        {
            var query = inputQuery as IQueryable<T>;

            if(spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            if(spec.Includes.Count > 0)
                query = spec.Includes.Aggregate(query,(current,include) => current.Include(include));

            return query;
        }
    }
}
