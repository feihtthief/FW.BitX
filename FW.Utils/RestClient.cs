using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FW.Utils
{
	public class RestClient
	{
		private string _Username;
		private string _Password;

		public RestClient() : this(null, null) { }

		public RestClient(string username, string password)
		{
			this._Username = username;
			this._Password = password;
		}

		public RestResponse ExecuteRequest(string url, string data)
		{
			var result = new RestResponse();
			try
			{
				var webReq = HttpWebRequest.Create(url);
				if (false
					|| (!string.IsNullOrWhiteSpace(_Username))
					|| (!string.IsNullOrWhiteSpace(_Password))
					)
				{
					//webReq.PreAuthenticate = true;
					//webReq.Credentials = new NetworkCredential(_Username, _Password);
					// === !!! mrh? Works, but not ideal !!! too tired , too late in the night ===
					string _auth = string.Format("{0}:{1}", _Username, _Password);
					string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
					string _cred = string.Format("{0} {1}", "Basic", _enc);
					webReq.Headers[HttpRequestHeader.Authorization] = _cred;
				}
				var webResp = (HttpWebResponse)webReq.GetResponse();
				var sr = new StreamReader(webResp.GetResponseStream());
				var respString = sr.ReadToEnd();
				result.StatusCode = webResp.StatusCode;
				result.ResponseContent = respString;
			}
			catch (Exception exc)
			{
				result.ResponseContent = String.Format("Exception: {0}", exc);
			}
			return result;
		}
	}
}
