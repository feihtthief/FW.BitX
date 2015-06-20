using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FW.Utils
{
	public class RestResponse
	{
		public HttpStatusCode StatusCode { get; set; }
		public string ResponseContent { get; set; }
	}
}
