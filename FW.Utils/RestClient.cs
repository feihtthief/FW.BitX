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
        public RestResponse ExecuteRequest(string url, string data)
        {
            var result = new RestResponse();
            try
            {
                var webReq = HttpWebRequest.Create(url);
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
