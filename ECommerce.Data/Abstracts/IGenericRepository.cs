using ECommerce.API.Entities;
using ECommerce.Data.Specifications;
using ECommerce.Entity.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Data.Abstracts
{
    public interface IGenericRepository<T> where T : class, IEntity
    {
        Task<T> GetByIdAsync(int id);
        Task<IReadOnlyCollection<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> GetEntityWithSpec(ISpecification<T> spec);
        Task<int> CountAsync(ISpecification<T> spec);
        Task<IReadOnlyCollection<T>> ListAsync(ISpecification<T> spec);
        void Add(T entity);
        void Update(T entity); 
        void Delete(T entity);
    }
}
