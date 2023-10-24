using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Common.Models
{
	public class Employee
	{
		public string Id { get; set; } = Guid.NewGuid().ToString();

		[Required]
		public string FirstName { get; set; }

		[Required]
        public string LastName { get; set; }

		[Required]
		public string Gender { get; set; }

		[Required]
		public DateTime DateOfBirth { get; set; }

		[Required]
		public string SocialSecurityNumber { get; set; }

		[Required]
		public string InsuranceNumber { get; set; }

		[Required]
		public string Address { get; set; }

        //[Required]
		//public string Country { get; set; }

		[Required]
		public string City { get; set; }

		//[Required]
		//public string State { get; set; }

		[Required]
		public string Zip { get; set; }

		[Required]
		public string PhoneNumber { get; set; }


		public string DepartmentId { get; set; }

		public bool IsActive { get; set; } = true;

		public bool IsDeleted { get; set; } = false;

		public DateTime Created { get; set; }

		public DateTime Modified { get; set; }

		public Employee()
		{
		}
	}
}

