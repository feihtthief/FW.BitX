using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Local
{
    public class OrderBook
    {
        public string Currency { get; set; }
        public DateTime TimeStamp { get; set; }

        public List<OrderBookEntry> Asks { get; private set; }
        public List<OrderBookEntry> Bids { get; private set; }

        public OrderBook()
        {
            this.Asks = new List<OrderBookEntry>();
            this.Bids = new List<OrderBookEntry>();
        }
    }
}
