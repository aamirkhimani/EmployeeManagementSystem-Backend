using System;
using Common.Models;

namespace Services.Interface
{
	public interface IEmployeeService
	{
		Task<List<Employee>> GetEmployees();
		Task<Employee> GetById(string id);
		Task<bool> AddEmployee(Employee employee);
		Task<bool> UpdateEmployee(Employee employee);
		Task<bool> DeleteEmployee(string id);
    }
}

