using System;
namespace Repository
{
	public interface IRepository<T> where T : class
	{
        Task<List<T>> GetAll();
        Task<T> GetById(string id);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(string id);
    }
}

