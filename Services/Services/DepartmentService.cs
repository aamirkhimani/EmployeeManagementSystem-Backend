using System;
using Common.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Services.Interface;

namespace Services.Services
{
	public class DepartmentService : IDepartmentService
	{
        private readonly IRepository<Department> _repository;

        public DepartmentService(IRepository<Department> repository)
		{
			_repository = repository;
		}

		public async Task<List<Department>> GetDepartments()
		{
			try
			{
				var departments = await _repository.getIQueryableAsNoTracking<Department>().ToListAsync();

				return departments;
			}
			catch(Exception ex)
			{
				throw;
			}
		}
	}
}

