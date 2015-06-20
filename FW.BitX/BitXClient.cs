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
		private const string BaseUrlApi = "https://api.mybitx.com/api/1/";
		private const string BaseUrlWeb = "https://bitx.co/ajax/1/";

		private string _Username;
		private string _Password;

		public BitXClient()
		{
		}

		public BitXClient(string username, string password)
		{
			this._Username = username;
			this._Password = password;
		}

		public OrderBook GetOrderBookFromWeb()
		{
			return GetOrderBookFromEndpoint(BaseUrlWeb + "orderbook?pair=XBTZAR");
		}

		public OrderBook GetOrderBookFromApi()
		{
			return GetOrderBookFromEndpoint(BaseUrlApi + "orderbook?pair=XBTZAR");
		}

		private OrderBook GetOrderBookFromEndpoint(string url)
		{
			var restClient = new RestClient();
			var restRepsonse = restClient.ExecuteRequest(url, null);
			var payload = JsonConvert.DeserializeObject<BitX_OrderBook_QueryResponse>(restRepsonse.ResponseContent);
			var result = new OrderBook
			{
				Currency = payload.currency,
				BitXTimeStamp = payload.timestamp,
				TimeStamp = UnixTime.FromUnixTimeUTC(payload.timestamp),
			};
			PopulateOrderBookEntries(result.Asks, payload.asks);
			PopulateOrderBookEntries(result.Bids, payload.bids);
			return result;
		}

		private void PopulateOrderBookEntries(List<OrderBookEntry> list, BitX_OrderBookEntry[] entries)
		{
			foreach (var item in entries)
			{
				list.Add(new OrderBookEntry
				{
					Price = Decimal.Parse(item.price),
					Volume = Decimal.Parse(item.volume),
				});
			}
		}

		public TradeInfo GetTradesFromWeb()
		{
			return GetTradesFromUrl(BaseUrlWeb + "trades?pair=XBTZAR");
		}

		public TradeInfo GetTradesFromApi()
		{
			return GetTradesFromUrl(BaseUrlApi + "trades?pair=XBTZAR");
		}

		private TradeInfo GetTradesFromUrl(string url)
		{
			var restClient = new RestClient();
			var restRepsonse = restClient.ExecuteRequest(url, null);
			var payload = JsonConvert.DeserializeObject<BitX_Trade_QueryResponse>(restRepsonse.ResponseContent);
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
					BitXTimeStamp = item.timestamp,
					TimeStamp = UnixTime.FromUnixTimeUTC(item.timestamp),
				});
			}
			return result;
		}

	}
}
