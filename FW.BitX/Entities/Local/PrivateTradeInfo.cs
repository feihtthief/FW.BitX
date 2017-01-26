using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
	// TODO: XXX implement this in remote bitx_type
	// TODO: XXX implement invocation
	public class PrivateTradeInfo
	{
		public List<PrivateTrade>[] Trades { get; set; }
	}

	public class PrivateTrade
	{
		public string _base { get; set; }
		public string counter { get; set; }
		public string fee_base { get; set; }
		public string fee_counter { get; set; }
		public bool is_buy { get; set; }
		public string order_id { get; set; }
		public string pair { get; set; }
		public string price { get; set; }
		public long timestamp { get; set; }
		public string type { get; set; }
		public string volume { get; set; }
	}
}
