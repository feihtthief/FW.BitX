using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
	public class TickerInfo
	{
		public decimal Ask { get; set; }
		public long BitXTimeStamp { get; set; }
		public DateTime TimeStampUTC { get; set; }
		public decimal Bid { get; set; }
		public decimal Rolling24HourVolume { get; set; }
		public decimal LastTrade { get; set; }
	}
}
