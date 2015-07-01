using FW.BitX.Entities.Local;
using FW.BitX.Entities.Remote;
using FW.BitX.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FW.BitX
{
	public class BitXClient
	{
		private const string BaseUrlApi = "https://api.mybitx.com/api/1/";
		private const string BaseUrlWeb = "https://bitx.co/ajax/1/";

		private string _ApiKey;
		private string _ApiSecret;

		public BitXClient()
		{
		}

		public BitXClient(string apiKey, string apiSecret)
		{
			this._ApiKey = apiKey;
			this._ApiSecret = apiSecret;
		}

		public void Fail()
		{
			// todo: remove. testing only. WIP
			var restClient = new RestClient();
			var restResponse = restClient.ExecuteRequest("http://localhost/thereisnosuchfile", null);
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
			var restResponse = restClient.ExecuteRequest(url, null);
			var payload = JsonConvert.DeserializeObject<BitX_OrderBook_QueryResponse>(restResponse.ResponseContent);
			var result = new OrderBook
			{
				Currency = payload.currency,
				BitXTimeStamp = payload.timestamp,
				TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(payload.timestamp),
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
			var restResponse = restClient.ExecuteRequest(url, null);
			var payload = JsonConvert.DeserializeObject<BitX_Trade_QueryResponse>(restResponse.ResponseContent);
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
					TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(item.timestamp),
				});
			}
			return result;
		}

		public List<AccountBalance> GetBalances()
		{
			return GetBalancesFromUrl(BaseUrlApi + "balance");
		}

		private List<AccountBalance> GetBalancesFromUrl(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, null);
			var payload = JsonConvert.DeserializeObject<BitX_Balances_QueryResponse>(restResponse.ResponseContent);
			var result = new List<AccountBalance>();
			foreach (var balance in payload.balance)
			{
				result.Add(new AccountBalance
				{
					AccountID = balance.account_id,
					Asset = balance.asset,
					Name = balance.name,
					Balance = Decimal.Parse(balance.balance),
					Reserved = Decimal.Parse(balance.reserved),
					Unconfirmed = Decimal.Parse(balance.unconfirmed),
				});
			}
			return result;
		}

		public List<TransactionEntry> GetTransactions(string accountID, int minRow, int maxRow)
		{
			return GetTransactionsFromUrl(BaseUrlApi + "accounts/"
				+ HttpUtility.UrlEncode(accountID) + "/transactions"
				+ "?min_row=" + HttpUtility.UrlEncode(minRow.ToString())
				+ "&max_row=" + HttpUtility.UrlEncode(maxRow.ToString())
			);
		}

		private List<TransactionEntry> GetTransactionsFromUrl(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, null);
			var payload = JsonConvert.DeserializeObject<BitX_Transactions_QueryResponse>(restResponse.ResponseContent);
			var result = new List<TransactionEntry>();
			foreach (var transaction in payload.transactions)
			{
				result.Add(new TransactionEntry
				{
					RowIndex = transaction.row_index,
					BitXTimeStamp = transaction.timestamp,
					TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(transaction.timestamp),
					Balance = (Decimal)transaction.balance,
					Available = (Decimal)transaction.available,
					BalanceDelta = (Decimal)transaction.balance_delta,
					AvailableDelta = (Decimal)transaction.available_delta,
					Currency = transaction.currency,
					Description = transaction.description,
				});
			}
			return result;
		}
	}
}
