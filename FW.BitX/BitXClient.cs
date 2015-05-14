using FW.BitX.Entities.Local;
using FW.BitX.Entities.Remote;
using FW.Utils;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX
{
    public class BitXClient
    {
        private string _a;
        private string _b;

        public BitXClient()
        {
        }

        public BitXClient(string a, string b)
        {
            this._a = a;
            this._b = b;
        }

        public TradeInfo GetTrades()
        {
            var restClient = new RestClient();
            var restRepsonse = restClient.ExecuteRequest("https://api.mybitx.com/api/1/trades?pair=XBTZAR", null);
            var payload = JsonConvert.DeserializeObject<BitX_TradeQueryResponse>(restRepsonse.ResponseContent);
            var result = new TradeInfo
            {
                Currency = payload.currency,
            };
            foreach (var item in payload.trades)
            {
                result.Trades.Add(new Trade
                {
                    Price = Decimal.Parse(item.price),
                    Volume = Decimal.Parse(item.volume),
                    TimeStamp = UnixTime.FromUnixTime(item.timestamp),
                });
            }
            return result;

        }

    }
}
