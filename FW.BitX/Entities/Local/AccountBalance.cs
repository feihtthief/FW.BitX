using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
	public class AccountBalance
	{
		public string AccountID { get; set; }
		public string Asset { get; set; }
		public decimal Balance { get; set; }
		public decimal Reserved { get; set; }
		public decimal Unconfirmed { get; set; }
		public string Name { get; set; }
	}
}