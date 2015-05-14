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
            var ti = c.GetTrades();
            
            Console.WriteLine(ti.Trades.Count());
            
            foreach (var item in ti.Trades)
            {
                Console.WriteLine("{0,-20} | {1,20:n8} | {2,20} | {3,20:n8}", item.TimeStamp, item.Volume, item.Price, item.Volume * item.Price);
            }

            
            //////var webReq = WebRequest.Create("https://api.mybitx.com/api/1/trades?pair=XBTZAR");
            //////var webResp = webReq.GetResponse();
            //////var sr = new StreamReader(webResp.GetResponseStream());
            //////var respString = sr.ReadToEnd();
            //////Console.WriteLine(respString);
            //////var resp = JsonConvert.DeserializeObject<BitX_TradeQueryResponse>(respString);
            //////Console.WriteLine(resp.trades.Count());

        }
    }
}
