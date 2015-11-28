using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	public class BitX_OrderTrade
	{
		public string price { get; set; }
		public long timestamp { get; set; }
		public string volume { get; set; }
	}
}
