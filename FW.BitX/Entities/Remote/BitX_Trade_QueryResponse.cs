using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// https://api.mybitx.com/api/1/trades?pair=XBTZAR

	// todo: xxx rename to BitX_MarketTrade_QueryResponse
	public class BitX_Trade_QueryResponse
	{
		public BitX_Trade[] trades { get; set; }
		public string currency { get; set; }
	}
}
