using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// curl -u keyid:keysecret https://api.mybitx.com/api/1/balance
	public class BitX_Balance
	{
		public string account_id { get; set; }
		public string asset { get; set; }
		public string balance { get; set; }
		public string reserved { get; set; }
		public string unconfirmed { get; set; }
		public string name { get; set; }
	}
}