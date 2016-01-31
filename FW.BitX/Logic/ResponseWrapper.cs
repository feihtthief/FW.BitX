using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Logic
{
	public class ResponseWrapper<T>
	{

		public RestResponse RestResponse { get; private set; }
		public T PayloadResponse { get; internal set; }
		public bool OK { get; private set; }

		public ResponseWrapper(RestResponse restResponse, Func<string, T> payloadParser)
		{
			this.RestResponse = restResponse;
			this.OK = RestResponse.OK;
			if (this.OK)
			{
				PayloadResponse = payloadParser(RestResponse.ResponseContent);
			}
		}

		public override string ToString()
		{
			return String.Format("[{0}]", (int)RestResponse.StatusCode);
		}

#if DEBUG
		// For Unit Testing
		public ResponseWrapper(T internalResponse, RestResponse restResponse = null, bool ok = true)
		{
			this.RestResponse = restResponse;
			this.OK = (restResponse == null) ? ok : RestResponse.OK;
			if (this.OK)
			{
				PayloadResponse = internalResponse;
			}
		}
#endif

	}
}
