﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// https://api.mybitx.com/api/1/trades?pair=XBTZAR

	public class BitX_MarketTrade_QueryResponse
	{
		public BitX_MarketTrade[] trades { get; set; }
		public string currency { get; set; }
	}
}
