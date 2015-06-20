using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.Utils
{
	public class UnixTime
	{
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		public static DateTime FromUnixTimeUTC(long unixtime)
		{
			var result = UnixEpoch.AddMilliseconds(unixtime);
			return result;
		}

	}
}
