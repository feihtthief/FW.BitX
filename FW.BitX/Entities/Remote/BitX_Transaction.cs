using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// todo: implement

	public class BitX_Transaction
	{
		public int row_index { get; set; }
		public long timestamp { get; set; }
		public float balance { get; set; }
		public float available { get; set; }
		public float balance_delta { get; set; }
		public float available_delta { get; set; }
		public string currency { get; set; }
		public string description { get; set; }
	}
}

// todo: figure out

//public class Transaction
//{
//	public int row_index { get; set; }
//	public long timestamp { get; set; }
//	public float balance { get; set; }
//	public float available { get; set; }
//															public string account_id { get; set; }
//	public float balance_delta { get; set; }
//	public float available_delta { get; set; }
//	public string currency { get; set; }
//	public string description { get; set; }
//															public Details details { get; set; }
//}

//public class Details
//{
//}
