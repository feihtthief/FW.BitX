using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Logic
{
	public class NoDelayGovernor : IGovernor
	{
		public void WaitTurn()
		{
			return;
		}
	}
}
