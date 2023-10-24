﻿using System;
namespace Common.Models
{
	public class Department
	{
		public Department()
		{
		}

		public string id { get; set; }

		public string Name { get; set; }

		public string RPDept { get; set; }

		public virtual List<Employee> Employees { get; set; }
	}
}

