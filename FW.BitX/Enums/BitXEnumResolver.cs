using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Enums
{
	public static class BitXEnumResolver
	{
		public static string GetStringForType(Enums.BitXType type)
		{
			switch (type)
			{
				case BitXType.ASK: return "ASK";
				case BitXType.BID: return "BID";
				default:
					throw new Exception("Invalid type");
			}
		}

		public static string GetStringForPair(Enums.BitXPair pair)
		{
			switch (pair)
			{
				case BitXPair.XBTZAR: return "XBTZAR";
				default:
					throw new Exception("Invalid pair");
			}
		}

	}
}
