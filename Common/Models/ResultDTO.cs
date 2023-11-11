using System;
using System.Net;
using static System.Net.WebRequestMethods;

namespace Common.Models
{
	public class ResultDTO
	{
		public ResultDTO()
		{
		}

		public bool IsSuccessful { get; set; }

		public string Message { get; set; }

		public object Data { get; set; }

		public HttpStatusCode StatusCode { get; set; }
	}
}

