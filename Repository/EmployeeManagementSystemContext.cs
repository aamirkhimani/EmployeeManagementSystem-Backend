using System;
using Common.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Models;

namespace Repository
{
	public class EmployeeManagementSystemContext : IdentityDbContext<ApplicationUser>
	{
		public EmployeeManagementSystemContext(DbContextOptions<EmployeeManagementSystemContext> options): base(options)
		{

		}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }

        public DbSet<ApplicationUser> ApplicationUsers;

		public DbSet<Employee> Employees { get; set; }

		public DbSet<Department> Departments { get; set; }
	}
}

