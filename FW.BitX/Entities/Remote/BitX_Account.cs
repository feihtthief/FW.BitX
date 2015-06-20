using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
	// todo: implement

	public class BitX_Account
	{
		public string Id { get; set; }
		public string UserId { get; set; }
		public string Name { get; set; }
		public string Currency { get; set; }
		public DateTime CreatedAt { get; set; }
		public float Balance { get; set; }
		public float Available { get; set; }
		public int LastRowIndex { get; set; }
		public DateTime LastRowTimestamp { get; set; }
		public bool IsDefault { get; set; }
	}
}