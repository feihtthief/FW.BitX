using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Enums
{
	public static class BitXEnumResolver
	{
		public static string GetStringForTransactionType(Enums.BitXTransactionType type)
		{
			switch (type)
			{
				case BitXTransactionType.ASK: return "ASK";
				case BitXTransactionType.BID: return "BID";
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

		public static Enums.BitXPair GetPairForString(string pairString)
		{
			switch (pairString)
			{
				case "XBTZAR": return BitXPair.XBTZAR;
				default:
					throw new Exception("Invalid or unknown pair string");
			}
		}

	}
}
