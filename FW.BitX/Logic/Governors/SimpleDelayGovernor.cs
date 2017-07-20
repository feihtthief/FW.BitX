using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FW.BitX.Logic
{
	public class SimpleDelayGovernor : IGovernor
	{
		public int Step { get; private set; }
		public int NextTick { get; private set; }

		public SimpleDelayGovernor(TimeSpan delay)
		{
			if (delay.TotalMilliseconds<=0){ throw new ArgumentException("Delay has to be positive", "delay"); }
			this.Step = (int)(delay.TotalMilliseconds);
			//this.Step += 15; // because computers suck
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
