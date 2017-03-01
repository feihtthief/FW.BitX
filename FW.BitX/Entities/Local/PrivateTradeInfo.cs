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
		public List<PrivateTrade> Trades { get; set; }

		public PrivateTradeInfo()
		{
			this.Trades = new List<PrivateTrade>();
		}
	}
}
