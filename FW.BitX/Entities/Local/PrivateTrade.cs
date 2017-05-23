using FW.BitX.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
	public class PrivateTrade
	{
		public decimal Base { get; set; }
		public decimal Counter { get; set; }
		public decimal FeeBase { get; set; }
		public decimal FeeCounter { get; set; }
		public bool IsBuy { get; set; }
		public string OrderID { get; set; }
		public BitXPair Pair { get; set; }
		public decimal Price { get; set; }
		public long BitXTimeStamp { get; set; }
		public DateTime TimeStampUTC { get; set; }
		public string Type { get; set; }
		public decimal Volume { get; set; }
	}
}
