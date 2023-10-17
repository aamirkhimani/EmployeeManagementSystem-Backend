using Common.Models;
using Repository;
using Services.Interface;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
	{
		private readonly IRepository<Employee> _employeeRepository;

        public EmployeeService(IRepository<Employee> employeeRepository)
		{
			_employeeRepository = employeeRepository;
        }

		public async Task<List<Employee>> GetEmployees()
		{
			try
			{
				var employees = await _employeeRepository.GetAll();

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
				var employee = await _employeeRepository.GetById(id);

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
				await _employeeRepository.AddAsync(employee);
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
				await _employeeRepository.UpdateAsync(employee);
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
				await _employeeRepository.DeleteAsync(id);
				return true;
			}
			catch(Exception ex)
			{
				return false;
			}
		}

    }
}

