using System;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public interface IRepository<T> where T : class
	{
        IQueryable<TEntity> getIQueryableAsNoTracking<TEntity>() where TEntity : class;
        IQueryable<TEntity> getIQueryable<TEntity>() where TEntity : class;
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }
}

