using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// https://api.mybitx.com/api/1/ticker?pair=XBTZAR

	public class BitX_Ticker_QueryResponse
	{
		public string ask { get; set; }
		public long timestamp { get; set; }
		public string bid { get; set; }
		public string rolling_24_hour_volume { get; set; }
		public string last_trade { get; set; }
	}
}