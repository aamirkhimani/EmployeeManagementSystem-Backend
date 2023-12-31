using System;
namespace Common
{
	public class Constants
	{
		public Constants()
		{
		}

		public static readonly string UsernameRegex = "^(?:(?!\\s)[\\w.+-]+@(?:\\w+\\.)+[a-zA-Z]{2,}|[\\w.+-]+)$";
		public static readonly string PasswordRegex = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
	}
}

