using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// curl -u keyid:keysecret https://api.mybitx.com/api/1/balance

	public class BitX_Balances_QueryResponse
	{
		public BitX_Balance[] balance { get; set; }
	}
}


