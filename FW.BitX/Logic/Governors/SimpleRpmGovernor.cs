using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FW.BitX.Logic
{
	public class SimpleRpmGovernor : IGovernor
	{
		public int RPM { get; private set; }
		public int Step { get; private set; }
		public int NextTick { get; private set; }

		public SimpleRpmGovernor(int rpm)
		{
			if (rpm <= 0) { throw new ArgumentException("RPM has to be positive", "rpm"); }
			this.RPM = rpm;
			this.Step = (int)(60m / this.RPM * 1000m);
			this.Step += 100; // because computers suck
			this.NextTick = Environment.TickCount - Step;
		}

		public void WaitTurn()
		{
			var now = Environment.TickCount;
			var diff = NextTick - now;
			if (diff > 0)
			{
				Thread.Sleep(diff);
			}
			NextTick = Environment.TickCount + Step;
		}
	}
}
