﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
	public class MarketTradeInfo
	{
		public string Currency { get; set; }
		public List<MarketTrade> Trades { get; private set; }

		public MarketTradeInfo()
		{
			this.Trades = new List<MarketTrade>();
		}

	}
}
