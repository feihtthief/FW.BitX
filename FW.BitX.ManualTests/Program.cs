using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.ManualTests
{
    class Program
    {
        static void Main(string[] args)
        {
            var c = new BitXClient();
            DateTime startDT;
            DateTime endDT;

            var orderbook_A = c.GetOrderBookFromApi();
            Console.WriteLine("Asks: {0}", orderbook_A.Asks.Count());
            Console.WriteLine("Bids: {0}", orderbook_A.Bids.Count());
            Console.WriteLine("Bids: {0}", orderbook_A.TimeStamp);
            Console.WriteLine();

            var orderbook_W = c.GetOrderBookFromWeb();
            Console.WriteLine("Asks: {0}", orderbook_W.Asks.Count());
            Console.WriteLine("Bids: {0}", orderbook_W.Bids.Count());
            Console.WriteLine("Bids: {0}", orderbook_W.TimeStamp);
            Console.WriteLine();

            foreach (var ask in orderbook_W.Asks)
            {
                Console.WriteLine("A,{0},{1}",ask.Price,ask.Volume);
            }
            foreach (var bid in orderbook_W.Bids)
            {
                Console.WriteLine("B,{0},{1}", bid.Price, bid.Volume);
            }

            if (Over()) return;
            Console.WriteLine();

            startDT = DateTime.Now;
            var ti_A = c.GetTradesFromApi();
            endDT = DateTime.Now;

            Console.WriteLine(ti_A.Trades.Count());
            Console.WriteLine(endDT - startDT);
            Console.WriteLine();

            //foreach (var item in trades_A.Trades)
            //{
            //    Console.WriteLine("{0,-20} | {1,20:n8} | {2,20} | {3,20:n8}", item.TimeStamp, item.Volume, item.Price, item.Volume * item.Price);
            //}

            startDT = DateTime.Now;
            var ti_W = c.GetTradesFromWeb();
            endDT = DateTime.Now;

            Console.WriteLine(ti_W.Trades.Count());
            Console.WriteLine(endDT - startDT);
            Console.WriteLine();

            //foreach (var item in trades_B.Trades)
            //{
            //    Console.WriteLine("{0,-20} | {1,20:n8} | {2,20} | {3,20:n8}", item.TimeStamp, item.Volume, item.Price, item.Volume * item.Price);
            //}

            if (ti_A.Trades.Count() != ti_W.Trades.Count())
            {
                Console.WriteLine("Count Mismatch");
            }
            var sameCount = 0;
            var diffCount = 0;
            for (int i = 0; i < Math.Min(ti_A.Trades.Count(), ti_W.Trades.Count()); i++)
            {
                var a = ti_A.Trades[i];
                var b = ti_W.Trades[i];
                var areSame = true
                    && (a.Price == b.Price)
                    && (a.TimeStamp == b.TimeStamp)
                    && (a.TimeStamp == b.TimeStamp)
                    ;
                if (areSame)
                {
                    sameCount++;
                }
                else
                {
                    diffCount++;
                }
            }
            Console.WriteLine("Same Count: {0}", sameCount);
            Console.WriteLine("Diff Count: {0}", diffCount);
            Console.WriteLine();

            //////var webReq = WebRequest.Create("https://api.mybitx.com/api/1/trades?pair=XBTZAR");
            //////var webResp = webReq.GetResponse();
            //////var sr = new StreamReader(webResp.GetResponseStream());
            //////var respString = sr.ReadToEnd();
            //////Console.WriteLine(respString);
            //////var resp = JsonConvert.DeserializeObject<BitX_TradeQueryResponse>(respString);
            //////Console.WriteLine(resp.trades.Count());

        }

        private static bool Over()
        {
            var k = Console.ReadKey(true);
            return k.Key == ConsoleKey.Escape;
        }

    }
}
