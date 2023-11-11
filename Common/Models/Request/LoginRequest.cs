using System;
namespace Common.Models.Request
{
	public class LoginRequest
	{
		public LoginRequest()
		{
		}

		public string Username { get; set; }

		public string Password { get; set; }
	}
}

