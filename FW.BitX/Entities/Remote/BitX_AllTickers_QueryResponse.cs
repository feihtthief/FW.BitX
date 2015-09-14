using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// https://api.mybitx.com/api/1/tickers

	public class BitX_AllTickers_QueryResponse
	{
		public BitX_AllTickers_TickerEntry[] tickers { get; set; }
	}



}