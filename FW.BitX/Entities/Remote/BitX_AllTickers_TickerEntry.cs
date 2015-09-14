using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	public class BitX_AllTickers_TickerEntry
	{
		public long timestamp { get; set; }
		public string bid { get; set; }
		public string ask { get; set; }
		public string last_trade { get; set; }
		public string rolling_24_hour_volume { get; set; }
		public string pair { get; set; }
	}
}
