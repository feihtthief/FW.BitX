using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
    public class TradeInfo
    {
        public string Currency { get; set; }
        public List<Trade> Trades { get; private set; }

        public TradeInfo()
        {
            this.Trades = new List<Trade>();
        }

    }
}
