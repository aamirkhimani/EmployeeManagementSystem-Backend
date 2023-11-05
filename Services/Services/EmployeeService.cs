using Common.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interface;
using ILogger = Serilog.ILogger;


namespace Services.Services
{
    public class EmployeeService : IEmployeeService
	{
		private readonly ILogger _logger;
        private readonly IRepository<Employee> _repository;
        public readonly string source = nameof(EmployeeService);

        public EmployeeService(ILogger logger, IRepository<Employee> repository)
		{
			_logger = logger;
			_repository = repository;
        }

		public async Task<List<Employee>> GetEmployees()
		{
            string methodContext = $"{source}.{nameof(GetEmployees)}";

            try
			{
				var employees = await _repository.getIQueryableAsNoTracking<Employee>().Include(x => x.Department).ToListAsync();

				_logger.Information($"{methodContext}:	Fetched list of Employees from db: {employees?.Count}");

				return employees;
            }
			catch(Exception ex)
			{
				_logger.Error($"{methodContext}:	{ex.Message}");
				throw;
			}
		}

		public async Task<Employee> GetById(string id)
		{
			try
			{
				var employee = await _repository.getIQueryableAsNoTracking<Employee>().Include(i => i.Department).Where(x => x.Id == id).FirstOrDefaultAsync();

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
                employee.DepartmentId = employee.DepartmentId == string.Empty ? null : employee.DepartmentId;

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
				employee.DepartmentId = employee.DepartmentId == string.Empty ? null : employee.DepartmentId;

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

