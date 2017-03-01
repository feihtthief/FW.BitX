using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// https://api.mybitx.com/api/1/trades?pair=XBTZAR

	public class BitX_MarketTrade
	{
		public long timestamp { get; set; }
		public string price { get; set; }
		public string volume { get; set; }
		public bool is_buy { get; set; }
	}
}
