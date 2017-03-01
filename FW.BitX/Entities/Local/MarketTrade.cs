using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
	public class MarketTrade
	{
		public decimal Price { get; set; }
		public decimal Volume { get; set; }
		public long BitXTimeStamp { get; set; }
		public DateTime TimeStampUTC { get; set; }
		public bool IsBuy { get; set; }
	}
}
