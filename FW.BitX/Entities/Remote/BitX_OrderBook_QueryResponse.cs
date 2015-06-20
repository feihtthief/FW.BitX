using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	public class BitX_OrderBook_QueryResponse
	{
		public long timestamp { get; set; }
		public BitX_OrderBookEntry[] asks { get; set; }
		public BitX_OrderBookEntry[] bids { get; set; }

		public string currency { get; set; }
	}

}
