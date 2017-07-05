using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FW.BitX.Logic
{
	public class SimpleRpmGovernor : SimpleDelayGovernor
	{
		public SimpleRpmGovernor(int rpm)
			: base(WorkOutDelayforRPM(rpm))
		{
		}

		private static TimeSpan WorkOutDelayforRPM(int rpm)
		{
			if (rpm <= 0) { throw new ArgumentException("RPM has to be positive", "rpm"); }
			var delay = (int)(60m / rpm * 1000m);
			return TimeSpan.FromMilliseconds(delay);
		}

	}
}
