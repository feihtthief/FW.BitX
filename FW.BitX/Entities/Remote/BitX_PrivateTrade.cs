using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	public class BitX_PrivateTrade
	{
		public string @base { get; set; }
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
