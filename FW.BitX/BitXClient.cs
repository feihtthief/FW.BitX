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

		// todo: rework all of these to return encapsulated response object to allow the calling app to know that the web server response code was a throttle response (503)
		// todo: implement PAIRS enum

		// todo: tickers
		// done: ticker(pair)
		// todo: accounts
		// todo: pending transactions
		// todo: list orders
		// todo: post order
		// todo: stop order
		// todo: get order?

		public ResponseWrapper<TickerList> GetTickerList()
		{
			return GetTickerListFromEndPoint(BaseUrlApi + "tickers");
		}

		private ResponseWrapper<TickerList> GetTickerListFromEndPoint(string url)
		{
			var restClient = new RestClient();
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<TickerList>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_AllTickers_QueryResponse>(responseContent);
				var payloadData = new TickerList
				{
					Tickers = new List<TickerListEntry>()
				};
				PopulateTickerListEntries(payloadData.Tickers, payload.tickers);
				return payloadData;
			});
			return result;
		}

		private void PopulateTickerListEntries(List<TickerListEntry> list, BitX_AllTickers_TickerEntry[] entries)
		{
			foreach (var item in entries)
			{
				list.Add(
					new TickerListEntry
					{
						BitXTimeStamp = item.timestamp,
						TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(item.timestamp),
						Ask = Decimal.Parse(item.ask),
						Bid = Decimal.Parse(item.bid),
						LastTrade = Decimal.Parse(item.last_trade),
						Rolling24HourVolume = Decimal.Parse(item.rolling_24_hour_volume),
						Pair = item.pair,
					}
				);
			}
		}

		public ResponseWrapper<TickerInfo> GetTickerInfoFromWeb()
		{
			return GetTickerInfoFromEndPoint(BaseUrlWeb + "ticker?pair=XBTZAR");
		}

		public ResponseWrapper<TickerInfo> GetTickerInfoFromApi()
		{
			return GetTickerInfoFromEndPoint(BaseUrlApi + "ticker?pair=XBTZAR");
		}

		private ResponseWrapper<TickerInfo> GetTickerInfoFromEndPoint(string url)
		{
			var restClient = new RestClient();
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<TickerInfo>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_Ticker_QueryResponse>(restResponse.ResponseContent);
				var payloadData = new TickerInfo
				{
					BitXTimeStamp = payload.timestamp,
					TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(payload.timestamp),
					Ask = Decimal.Parse(payload.ask),
					Bid = Decimal.Parse(payload.bid),
					LastTrade = Decimal.Parse(payload.last_trade),
					Rolling24HourVolume = Decimal.Parse(payload.rolling_24_hour_volume),
				};
				return payloadData;
			});
			return result;
		}

		public ResponseWrapper<OrderBook> GetOrderBookFromWeb()
		{
			return GetOrderBookFromEndpoint(BaseUrlWeb + "orderbook?pair=XBTZAR");
		}

		public ResponseWrapper<OrderBook> GetOrderBookFromApi()
		{
			return GetOrderBookFromEndpoint(BaseUrlApi + "orderbook?pair=XBTZAR");
		}

		private ResponseWrapper<OrderBook> GetOrderBookFromEndpoint(string url)
		{
			//var restClient = new RestClient();
			//var restResponse = restClient.ExecuteRequest(url, null);
			//var payload = JsonConvert.DeserializeObject<BitX_OrderBook_QueryResponse>(restResponse.ResponseContent);

			var restClient = new RestClient();
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<OrderBook>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_OrderBook_QueryResponse>(restResponse.ResponseContent);
				var payloadData = new OrderBook
					{
						Currency = payload.currency,
						BitXTimeStamp = payload.timestamp,
						TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(payload.timestamp),
					};
				PopulateOrderBookEntries(payloadData.Asks, payload.asks);
				PopulateOrderBookEntries(payloadData.Bids, payload.bids);
				return payloadData;
			});
			return result;

			//BitX_OrderBook_QueryResponse payload;
			//if (GetPayloadAnonymous(out payload, url))
			//{
			//	var result = new OrderBook
			//	{
			//		Currency = payload.currency,
			//		BitXTimeStamp = payload.timestamp,
			//		TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(payload.timestamp),
			//	};
			//	PopulateOrderBookEntries(result.Asks, payload.asks);
			//	PopulateOrderBookEntries(result.Bids, payload.bids);
			//	return result;
			//}
			//return null;
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

		public ResponseWrapper<TradeInfo> GetTradesFromWeb()
		{
			return GetTradesFromUrl(BaseUrlWeb + "trades?pair=XBTZAR");
		}

		public ResponseWrapper<TradeInfo> GetTradesFromApi()
		{
			return GetTradesFromUrl(BaseUrlApi + "trades?pair=XBTZAR");
		}

		private ResponseWrapper<TradeInfo> GetTradesFromUrl(string url)
		{
			//var restClient = new RestClient();
			//var restResponse = restClient.ExecuteRequest(url, null);
			//var payload = JsonConvert.DeserializeObject<BitX_Trade_QueryResponse>(restResponse.ResponseContent);

			var restClient = new RestClient();
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<TradeInfo>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_Trade_QueryResponse>(restResponse.ResponseContent);
				var payloadData = new TradeInfo
				{
					Currency = payload.currency,
				};
				foreach (var item in payload.trades)
				{
					payloadData.Trades.Add(new Trade
					{
						Price = Decimal.Parse(item.price),
						Volume = Decimal.Parse(item.volume),
						BitXTimeStamp = item.timestamp,
						TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(item.timestamp),
					});
				}
				return payloadData;
			});
			return result;

			//BitX_Trade_QueryResponse payload;
			//if (GetPayloadAnonymous(out payload, url))
			//{
			//	var result = new TradeInfo
			//	{
			//		Currency = payload.currency,
			//	};
			//	foreach (var item in payload.trades)
			//	{
			//		result.Trades.Add(new Trade
			//		{
			//			Price = Decimal.Parse(item.price),
			//			Volume = Decimal.Parse(item.volume),
			//			BitXTimeStamp = item.timestamp,
			//			TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(item.timestamp),
			//		});
			//	}
			//	return result;
			//}
			//return null;
		}

		public ResponseWrapper<List<AccountBalance>> GetBalances()
		{
			return GetBalancesFromUrl(BaseUrlApi + "balance");
		}

		private ResponseWrapper<List<AccountBalance>> GetBalancesFromUrl(string url)
		{
			//var restClient = new RestClient(_ApiKey, _ApiSecret);
			//var restResponse = restClient.ExecuteRequest(url, null);
			//var payload = JsonConvert.DeserializeObject<BitX_Balances_QueryResponse>(restResponse.ResponseContent);

			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<List<AccountBalance>>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_Balances_QueryResponse>(restResponse.ResponseContent);
				var payloadData = new List<AccountBalance>();
				foreach (var balance in payload.balance)
				{
					payloadData.Add(new AccountBalance
					{
						AccountID = balance.account_id,
						Asset = balance.asset,
						Name = balance.name,
						Balance = Decimal.Parse(balance.balance),
						Reserved = Decimal.Parse(balance.reserved),
						Unconfirmed = Decimal.Parse(balance.unconfirmed),
					});
				}
				return payloadData;
			});
			return result;

			//BitX_Balances_QueryResponse payload;
			//if (GetPayloadAuthenticated(out payload, url))
			//{
			//	var result = new List<AccountBalance>();
			//	foreach (var balance in payload.balance)
			//	{
			//		result.Add(new AccountBalance
			//		{
			//			AccountID = balance.account_id,
			//			Asset = balance.asset,
			//			Name = balance.name,
			//			Balance = Decimal.Parse(balance.balance),
			//			Reserved = Decimal.Parse(balance.reserved),
			//			Unconfirmed = Decimal.Parse(balance.unconfirmed),
			//		});
			//	}
			//	return result;
			//}
			//return null;
		}

		public ResponseWrapper<List<PendingTransactionEntry>> GetPendingTransactions(string accountID)
		{
			return GetPendingTransactionsFromUrl(BaseUrlApi + "accounts/"
				+ HttpUtility.UrlEncode(accountID) + "/pending"
			);
		}

		private ResponseWrapper<List<PendingTransactionEntry>> GetPendingTransactionsFromUrl(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<List<PendingTransactionEntry>>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_PendingTransactions_QueryResponse>(restResponse.ResponseContent);
				var payloadData = new List<PendingTransactionEntry>();
				foreach (var transaction in payload.pending)
				{
					payloadData.Add(new PendingTransactionEntry
					{
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
				return payloadData;
			});
			return result;
		}

		public ResponseWrapper<List<TransactionEntry>> GetTransactions(string accountID, int minRow, int maxRow)
		{
			return GetTransactionsFromUrl(BaseUrlApi + "accounts/"
				+ HttpUtility.UrlEncode(accountID) + "/transactions"
				+ "?min_row=" + HttpUtility.UrlEncode(minRow.ToString())
				+ "&max_row=" + HttpUtility.UrlEncode(maxRow.ToString())
			);
		}

		private ResponseWrapper<List<TransactionEntry>> GetTransactionsFromUrl(string url)
		{
			//var restClient = new RestClient(_ApiKey, _ApiSecret);
			//var restResponse = restClient.ExecuteRequest(url, null);
			//var payload = JsonConvert.DeserializeObject<BitX_Transactions_QueryResponse>(restResponse.ResponseContent);

			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<List<TransactionEntry>>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_Transactions_QueryResponse>(restResponse.ResponseContent);
				var payloadData = new List<TransactionEntry>();
				foreach (var transaction in payload.transactions)
				{
					payloadData.Add(new TransactionEntry
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
				return payloadData;
			});
			return result;

			//BitX_Transactions_QueryResponse payload;
			//if (GetPayloadAuthenticated(out payload, url))
			//{
			//	var result = new List<TransactionEntry>();
			//	foreach (var transaction in payload.transactions)
			//	{
			//		result.Add(new TransactionEntry
			//		{
			//			RowIndex = transaction.row_index,
			//			BitXTimeStamp = transaction.timestamp,
			//			TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(transaction.timestamp),
			//			Balance = (Decimal)transaction.balance,
			//			Available = (Decimal)transaction.available,
			//			BalanceDelta = (Decimal)transaction.balance_delta,
			//			AvailableDelta = (Decimal)transaction.available_delta,
			//			Currency = transaction.currency,
			//			Description = transaction.description,
			//		});
			//	}
			//	return result;
			//}
			//return null;
		}

		#region Old. Abandoned.

		private bool GetPayloadAnonymous<T>(out T payload, string url)
			where T : class
		{
			var restClient = new RestClient();
			return __GetPayload<T>(out payload, restClient, url);
		}

		private bool GetPayloadAuthenticated<T>(out T payload, string url)
			where T : class
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			return __GetPayload<T>(out payload, restClient, url);
		}

		// Prefixed with __ to help hint at the fact that this is not meant to be called directly, but rather through one of the other method
		private bool __GetPayload<T>(out T payload, RestClient restClient, string url)
			where T : class
		{
			payload = null;
			var restResponse = restClient.ExecuteRequest(url, null);
			if (restResponse.OK)
			{
				payload = JsonConvert.DeserializeObject<T>(restResponse.ResponseContent);
				return true;
			}
			return false;
		}

		#endregion
	}
}
