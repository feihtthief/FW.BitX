using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// https://api.mybitx.com/api/1/trades?pair=XBTZAR

	// todo: xxx rename to BitX_MarketTrade
	public class BitX_Trade
	{
		public long timestamp { get; set; }
		public string price { get; set; }
		public string volume { get; set; }
		public bool is_buy { get; set; }
	}
}
