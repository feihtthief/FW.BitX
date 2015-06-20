using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// todo: implement

	public class BitX_Transactions_QueryResponse
	{
		public string id { get; set; }
		public BitX_Transaction[] transactions { get; set; }
	}
}
