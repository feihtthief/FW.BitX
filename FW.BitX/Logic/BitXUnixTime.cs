using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Logic
{

	public class BitXUnixTime
	{
		public static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

		public static DateTime DateTimeUTCFromBitXUnixTime(long unixtime)
		{
			var result = UnixEpoch.AddMilliseconds(unixtime);
			return result;
		}

		public static long BitXUnixTimeFromDateTimeUTC(DateTime dateTimeUTC)
		{
			var diff = (dateTimeUTC - UnixEpoch);
			var result = (long)diff.TotalMilliseconds;
			return result;
		}

	}
}
