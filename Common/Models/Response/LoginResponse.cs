using System;
namespace Common.Models.Response
{
	public class LoginResponse
	{
		public LoginResponse()
		{
		}

		public string Username { get; set; }

		public string Token { get; set; }

		public DateTime Expiry { get; set; }
	}
}

