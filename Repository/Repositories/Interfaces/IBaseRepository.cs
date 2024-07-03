using System;
using System.Linq.Expressions;
using Domain.Common;
using Microsoft.EntityFrameworkCore.Query;

namespace Repository.Repositories.Interfaces
{
	public interface IBaseRepository<T> where T : BaseEntity
    {
        Task CreateAsync(T entity);
        Task EditAsync(T entity);
        Task DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetById(int id);
        //IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includes);
        public IQueryable<T> Find(Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
        public IQueryable<T> FindBy(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    }
}

