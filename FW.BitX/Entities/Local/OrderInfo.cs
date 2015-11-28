using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
	public class OrderInfo
	{
		public string OrderID { get; set; }

		public long BitXCreationTimestamp { get; set; }
		public DateTime CreationTimestampUTC { get; set; }

		public long BitXExpirationTimestamp { get; set; }
		public DateTime ExpirationTimestampUTC { get; set; }
		
		public string Type { get; set; }
		public string State { get; set; }
		public decimal LimitPrice { get; set; }
		public decimal LimitVolume { get; set; }
		public decimal Base { get; set; }
		public decimal Counter { get; set; }
		public decimal FeeBase { get; set; }
		public decimal FeeCounter { get; set; }
		public List<OrderTrade> Trades { get; set; }
	}
}
