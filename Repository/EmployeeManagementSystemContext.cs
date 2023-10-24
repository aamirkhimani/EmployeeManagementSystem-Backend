using System;
using Common.Models;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
	public class EmployeeManagementSystemContext : DbContext
	{
		public EmployeeManagementSystemContext(DbContextOptions<EmployeeManagementSystemContext> options): base(options)
		{

		}

		public DbSet<Employee> Employees { get; set; }

		public DbSet<Department> Departments { get; set; }
	}
}

