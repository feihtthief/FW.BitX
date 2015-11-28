using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	public class BitX_GetOrder_QueryResponse
	{
		public string order_id { get; set; }
		public long creation_timestamp { get; set; }
		public int expiration_timestamp { get; set; }
		public string type { get; set; }
		public string state { get; set; }
		public string limit_price { get; set; }
		public string limit_volume { get; set; }
		public string @base { get; set; }
		public string counter { get; set; }
		public string fee_base { get; set; }
		public string fee_counter { get; set; }
		public BitX_OrderTrade[] trades { get; set; }
	}
}
