using System;
using Microsoft.AspNetCore.Identity;

namespace Repository.Models
{
	public class ApplicationUser : IdentityUser
	{
		public ApplicationUser()
		{
		}

		public string Name { get; set; }
	}
}

