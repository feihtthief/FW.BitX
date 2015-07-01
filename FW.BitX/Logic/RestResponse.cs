using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Logic
{
	public class RestResponse
	{
		public HttpStatusCode StatusCode { get; set; }
		public string ResponseContent { get; set; }
		public bool OK { get; set; }
		public Exception Exception { get; set; }
	}
}
