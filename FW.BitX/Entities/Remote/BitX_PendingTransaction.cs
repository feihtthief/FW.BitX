using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	public class BitX_PendingTransaction
	{
		public long timestamp { get; set; }
		public float balance { get; set; }
		public float available { get; set; }
		public float balance_delta { get; set; }
		public float available_delta { get; set; }
		public string currency { get; set; }
		public string description { get; set; }
	}
}
