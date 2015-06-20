using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
	// todo: implement

	public class TransactionEntry
	{
		public int RowIndex { get; set; }
		public long BitXTimeStamp { get; set; }
		public DateTime TimeStampUTC { get; set; }
		public Decimal Balance { get; set; }
		public Decimal Available { get; set; }
		public Decimal BalanceDelta { get; set; }
		public Decimal AvailableDelta { get; set; }
		public string Currency { get; set; }
		public string Description { get; set; }

	}
}
