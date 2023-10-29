using Common.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interface;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
	{
		private readonly IRepository<Employee> _repository;

        public EmployeeService(IRepository<Employee> repository)
		{
			_repository = repository;
        }

		public async Task<List<Employee>> GetEmployees()
		{
			try
			{
				var employees = await _repository.getIQueryableAsNoTracking<Employee>().Include(x => x.Department).ToListAsync();

				return employees;
            }
			catch(Exception ex)
			{
				throw;
			}
		}

		public async Task<Employee> GetById(string id)
		{
			try
			{
				var employee = await _repository.getIQueryableAsNoTracking<Employee>().Where(x => x.Id == id).FirstOrDefaultAsync();

				return employee;
			}
			catch(Exception ex)
			{
				throw;
			}
		}

		public async Task<bool> AddEmployee(Employee employee)
		{
			try
			{
				await _repository.AddAsync(employee);
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public async Task<bool> UpdateEmployee(Employee employee)
		{
			try
			{
				await _repository.UpdateAsync(employee);
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

		public async Task<bool> DeleteEmployee(string id)
		{
			try
			{
				await _repository.DeleteAsync(id);
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

    }
}

