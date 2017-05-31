using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Logic
{
	public class RestClient
	{
		private string _Username;
		private string _Password;
		private IGovernor _Governor;

		public RestClient() : this(null, null, null) { }
		static int last = Environment.TickCount;

		public RestClient(string username, string password, IGovernor governor)
		{
			this._Username = username;
			this._Password = password;
			this._Governor = (governor != null)
				? governor
				: new NoDelayGovernor()
			;
		}

		public RestResponse ExecuteRequest(string url, string data)
		{
			var start = Environment.TickCount;
			_Governor.WaitTurn();
			var end = Environment.TickCount;
			var diff = end - start;
			var diffTotal = end - last;
			last = end;
			Console.ForegroundColor = ConsoleColor.DarkGreen;
			Console.WriteLine("Delay from governor: {0,60:N0} ms", diff);
			Console.WriteLine("Total Delay:         {0,60:N0} ms", diffTotal);
			Console.ForegroundColor = ConsoleColor.DarkCyan;
			Console.WriteLine("URL: {0}", url);
			Console.ResetColor();
			var result = new RestResponse();
			try
			{
				// TODO: rate-throttle/govern here-ish
				var webReq = HttpWebRequest.Create(url);
				if (false
					|| (!string.IsNullOrWhiteSpace(_Username))
					|| (!string.IsNullOrWhiteSpace(_Password))
					)
				{
					// === !!! mrh? Works, but not ideal !!! too tired , too late in the night ===
					string _auth = string.Format("{0}:{1}", _Username, _Password);
					string _enc = Convert.ToBase64String(Encoding.ASCII.GetBytes(_auth));
					string _cred = string.Format("{0} {1}", "Basic", _enc);
					webReq.Headers[HttpRequestHeader.Authorization] = _cred;
				}
				if (!string.IsNullOrEmpty(data))
				{
					var bytes = Encoding.UTF8.GetBytes(data);
					webReq.ContentType = "application/x-www-form-urlencoded";
					webReq.Method = WebRequestMethods.Http.Post;
					webReq.GetRequestStream().Write(bytes, 0, bytes.Length);
				}
				var webResponse = (HttpWebResponse)webReq.GetResponse();
				result.StatusCode = webResponse.StatusCode;
				result.ResponseContent = GetResponseContentFromWebResponse(webResponse);
				result.OK = result.StatusCode == HttpStatusCode.OK;
			}
			catch (Exception exc)
			{
				result.OK = false; // todo: implement all the way back to caller
				result.Exception = exc;
				var webException = exc as WebException;
				if (webException != null)
				{
					var webResponse = webException.Response as HttpWebResponse;
					if (webResponse != null)
					{
						result.ResponseContent = TryGetResponseContentFromWebResponse(webResponse);
						result.StatusCode = webResponse.StatusCode;
					}
				}
			}
			return result;
		}

		internal string TryGetResponseContentFromWebResponse(WebResponse webResponse)
		{
			try
			{
				return GetResponseContentFromWebResponse(webResponse);
			}
			catch { }
			return null;
		}

		internal string GetResponseContentFromWebResponse(WebResponse webResponse)
		{
			var sr = new StreamReader(webResponse.GetResponseStream());
			var respString = sr.ReadToEnd();
			return respString;
		}
	}
}
