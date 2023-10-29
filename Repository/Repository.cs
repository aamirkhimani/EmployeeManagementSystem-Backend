using System;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class Repository<T> : IRepository<T> where T : class
	{
        private readonly EmployeeManagementSystemContext _dbContext;
		private readonly DbSet<T> _dbSet;

        public Repository(EmployeeManagementSystemContext employeeManagementSystemContext)
		{
			_dbContext = employeeManagementSystemContext;
            _dbSet = employeeManagementSystemContext.Set<T>();

        }

        public IQueryable<TEntity> getIQueryableAsNoTracking<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }

        public IQueryable<TEntity> getIQueryable<TEntity>() where TEntity : class
        {
            return _dbContext.Set<TEntity>().AsTracking();
        }

        public async Task AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}

