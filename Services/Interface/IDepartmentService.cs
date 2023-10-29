using System;
using Common.Models;

namespace Services.Interface
{
	public interface IDepartmentService
	{
		Task<List<Department>> GetDepartments();
	}
}

