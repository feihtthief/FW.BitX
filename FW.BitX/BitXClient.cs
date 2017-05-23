using FW.BitX.Entities.Local;
using FW.BitX.Entities.Remote;
using FW.BitX.Enums;
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

		public BitXClient() : this(null, null) { }

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

		// done: rework all of these to return encapsulated response object to allow the calling app to know that the web server response code was a throttle response (503)
		// done: accounts (=balances)
		// done: tickers
		// done: pending transactions
		// done: list orders
		// done: post order
		// done: get order info

		// done: implement PAIRS enum
		// done: implement Ask/Buy enum

		// TODO: use PAIRS enum in all places where a pair can be provided

		// TODO: implement a basic rate throttler
		// TODO: implement an advanced rate throttler

		// done : stop order

		// TODO: xxx distinguish between :
		//   market trades  ( GET https://api.mybitx.com/api/1/trades     | curl https://api.mybitx.com/api/1/trades?pair=XBTZAR )
		//   private trades ( GET https://api.mybitx.com/api/1/listtrades | curl -u keyid:keysecret https://api.mybitx.com/api/1/listtrades?pair=XBTZAR )

		public ResponseWrapper<PostLimitOrderResponse> PostLimitOrder(Enums.BitXPair pair, Enums.BitXTransactionType transactionType, decimal volume, int price)
		{
			// done: enforce pair is something sane from valid pairs enum/const-class (like XBTZAR)
			// done: enforce type is something sane from valid type  enum/const-class (ie, BID or ASK)
			var pairStr = BitXEnumResolver.GetStringForPair(pair);
			var transactionTypeStr = BitXEnumResolver.GetStringForTransactionType(transactionType);
			var data = ""
				+ "pair=" + HttpUtility.UrlEncode(pairStr)
				+ "&type=" + HttpUtility.UrlEncode(transactionTypeStr)
				+ "&volume=" + HttpUtility.UrlEncode(volume.ToString())
				+ "&price=" + HttpUtility.UrlEncode(price.ToString("n0"))
			;
			return PostLimitOrderToEndpoint(BaseUrlApi + "postorder"
				, data
			);
		}

		private ResponseWrapper<PostLimitOrderResponse> PostLimitOrderToEndpoint(string url, string data)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, data);
			var result = new ResponseWrapper<PostLimitOrderResponse>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_PostLimitOrder_Response>(responseContent);
				var payloadData = new PostLimitOrderResponse
				{
					OrderID = payload.id,
				};
				return payloadData;
			});
			return result;
		}

		public ResponseWrapper<StopOrderResponse> StopOrder(string orderID)
		{
			var data = ""
				+ "order_id=" + HttpUtility.UrlEncode(orderID)
			;
			return PostStopOrderToEndpoint(BaseUrlApi + "stoporder"
				, data
				);
		}

		private ResponseWrapper<StopOrderResponse> PostStopOrderToEndpoint(string url, string data)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, data);
			var result = new ResponseWrapper<StopOrderResponse>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_StopOrder_Response>(responseContent);
				var payloadData = new StopOrderResponse
				{
					Success = payload.success,
				};
				return payloadData;
			});
			return result;
		}

		public ResponseWrapper<OrderInfo> GetOrderInfo(string orderID)
		{
			return GetOrderInfoEndPoint(BaseUrlApi + "orders/"
				+ HttpUtility.UrlEncode(orderID)
				);
		}

		private ResponseWrapper<OrderInfo> GetOrderInfoEndPoint(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<OrderInfo>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_GetOrder_QueryResponse>(responseContent);
				var payloadData = new OrderInfo
				{
					Base = Decimal.Parse(payload.@base),
					BitXCreationTimestamp = payload.creation_timestamp,
					CreationTimestampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(payload.creation_timestamp),
					BitXExpirationTimestamp = payload.expiration_timestamp,
					ExpirationTimestampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(payload.expiration_timestamp),
					Counter = Decimal.Parse(payload.counter),
					FeeBase = Decimal.Parse(payload.fee_base),
					FeeCounter = Decimal.Parse(payload.fee_counter),
					LimitPrice = Decimal.Parse(payload.limit_price),
					LimitVolume = Decimal.Parse(payload.limit_volume),
					OrderID = payload.order_id,
					State = payload.state,
					Type = payload.type,
					Trades = new List<OrderTrade>(),
				};
				if (payload.trades != null)
				{
					PopulateOrderInfoEntries(payloadData.Trades, payload.trades);
				}
				return payloadData;
			});
			return result;
		}

		private void PopulateOrderInfoEntries(List<OrderTrade> list, BitX_OrderTrade[] entries)
		{
			foreach (var item in entries)
			{
				list.Add(
					new OrderTrade
					{
						BitXTimeStamp = item.timestamp,
						TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(item.timestamp),
						Price = Decimal.Parse(item.price),
						Volume = Decimal.Parse(item.volume),
					}
				);
			}
		}

		public ResponseWrapper<TickerList> GetTickerList()
		{
			return GetTickerListFromEndPoint(BaseUrlApi + "tickers");
		}

		private ResponseWrapper<TickerList> GetTickerListFromEndPoint(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
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

		public ResponseWrapper<TickerInfo> GetTickerInfoFromWeb(Enums.BitXPair pair)
		{
			var pairStr = BitXEnumResolver.GetStringForPair(pair);
			return GetTickerInfoFromEndPoint(
			BaseUrlWeb
				+ "ticker"
				+ "?pair=" + HttpUtility.UrlEncode(pairStr)
			);
		}

		public ResponseWrapper<TickerInfo> GetTickerInfoFromApi(Enums.BitXPair pair)
		{
			var pairStr = BitXEnumResolver.GetStringForPair(pair);
			return GetTickerInfoFromEndPoint(
			BaseUrlApi
				+ "ticker"
				+ "?pair=" + HttpUtility.UrlEncode(pairStr)
			);
		}

		private ResponseWrapper<TickerInfo> GetTickerInfoFromEndPoint(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
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

		public ResponseWrapper<OrderBook> GetOrderBookFromWeb(Enums.BitXPair pair)
		{
			var pairStr = BitXEnumResolver.GetStringForPair(pair);
			return GetOrderBookFromEndpoint(
			BaseUrlWeb
				+ "orderbook"
				+ "?pair=" + HttpUtility.UrlEncode(pairStr)
			);
		}

		public ResponseWrapper<OrderBook> GetOrderBookFromApi(Enums.BitXPair pair)
		{
			var pairStr = BitXEnumResolver.GetStringForPair(pair);
			return GetOrderBookFromEndpoint(
			BaseUrlApi
				+ "orderbook"
				+ "?pair=" + HttpUtility.UrlEncode(pairStr)
			);
		}

		private ResponseWrapper<OrderBook> GetOrderBookFromEndpoint(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
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

		public ResponseWrapper<MarketTradeInfo> GetMarketTradesFromWeb(Enums.BitXPair pair, long? since = null)
		{
			var pairStr = BitXEnumResolver.GetStringForPair(pair);
			return GetMarketTradesFromUrl(
			BaseUrlWeb
				+ "trades"
				+ "?pair=" + HttpUtility.UrlEncode(pairStr)
				+ (since.HasValue ? "&since=" + HttpUtility.UrlEncode(since.Value.ToString()) : "")
			);
		}

		public ResponseWrapper<MarketTradeInfo> GetMarketTradesFromApi(Enums.BitXPair pair, long? since = null)
		{
			var pairStr = BitXEnumResolver.GetStringForPair(pair);
			return GetMarketTradesFromUrl(
			BaseUrlApi
				+ "trades"
				+ "?pair=" + HttpUtility.UrlEncode(pairStr)
				+ (since.HasValue ? "&since=" + HttpUtility.UrlEncode(since.Value.ToString()) : "")
			);
		}

		private ResponseWrapper<MarketTradeInfo> GetMarketTradesFromUrl(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<MarketTradeInfo>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_MarketTrade_QueryResponse>(restResponse.ResponseContent);
				var payloadData = new MarketTradeInfo
				{
					Currency = payload.currency,
				};
				foreach (var item in payload.trades)
				{
					payloadData.Trades.Add(new MarketTrade
					{
						Price = Decimal.Parse(item.price),
						Volume = Decimal.Parse(item.volume),
						BitXTimeStamp = item.timestamp,
						TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(item.timestamp),
						IsBuy = item.is_buy,
					});
				}
				return payloadData;
			});
			return result;
		}


		public ResponseWrapper<PrivateTradeInfo> GetPrivateTradesFromApi(Enums.BitXPair pair, long? since = null, int? limit = 100)
		{
			var pairStr = BitXEnumResolver.GetStringForPair(pair);
			return GetPrivateTradesFromUrl(
			BaseUrlApi
				+ "listtrades"
				+ "?pair=" + HttpUtility.UrlEncode(pairStr)
				+ (since.HasValue ? "&since=" + HttpUtility.UrlEncode(since.Value.ToString()) : "")
				+ (limit.HasValue ? "&limit=" + HttpUtility.UrlEncode(limit.Value.ToString()) : "")
			);
		}

		private ResponseWrapper<PrivateTradeInfo> GetPrivateTradesFromUrl(string url)
		{
			var restClient = new RestClient(_ApiKey, _ApiSecret);
			var restResponse = restClient.ExecuteRequest(url, null);
			var result = new ResponseWrapper<PrivateTradeInfo>(restResponse, (responseContent) =>
			{
				var payload = JsonConvert.DeserializeObject<BitX_PrivateTrade_QueryResponse>(restResponse.ResponseContent);
				var payloadData = new PrivateTradeInfo
				{
				};
				foreach (var item in payload.trades)
				{
					payloadData.Trades.Add(new PrivateTrade
					{
						Base = Decimal.Parse(item.@base),
						Counter = Decimal.Parse(item.counter),
						FeeBase = Decimal.Parse(item.fee_base),
						FeeCounter = Decimal.Parse(item.fee_counter),
						IsBuy = item.is_buy,
						OrderID = item.order_id,
						Pair = BitXEnumResolver.GetPairForString(item.pair),
						Price = Decimal.Parse(item.price),
						Volume = Decimal.Parse(item.volume),
						BitXTimeStamp = item.timestamp,
						TimeStampUTC = BitXUnixTime.DateTimeUTCFromBitXUnixTime(item.timestamp),
						Type = item.type,
					});
				}
				return payloadData;
			});
			return result;
		}

		public ResponseWrapper<List<AccountBalance>> GetBalances()
		{
			return GetBalancesFromUrl(BaseUrlApi + "balance");
		}

		private ResponseWrapper<List<AccountBalance>> GetBalancesFromUrl(string url)
		{
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
		}

	}
}
