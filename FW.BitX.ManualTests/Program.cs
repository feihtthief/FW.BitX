﻿using FW.BitX.Enums;
using FW.BitX.Logic;
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
	// Ugly manual testing program.

	class Program
	{
		private static readonly string NL = Environment.NewLine;
		private static Settings _Settings = null;
		private static BitXClient authorizedClient;
		private static BitXClient anonymousClient;

		static void Main(string[] args)
		{
			Init();

			//var placeLimitOrderResponse = authorizedClient.PostLimitOrder("XBTZAR", "BID", 0.001m, 15);
			// TODO: retest when I have some money again.
			//var placeLimitOrderResponse = authorizedClient.PostLimitOrder(BitXPair.XBTZAR, BitXTransactionType.BID, 0.001m, 15);

			var stopOrderID = "";
			if (!string.IsNullOrEmpty(stopOrderID))
			{
				var stopOrderRepsonse = authorizedClient.StopOrder(stopOrderID);
				if (stopOrderRepsonse.OK)
				{

				}
				else
				{
					Console.WriteLine("Authenticated StopOrder via API Failed: {0}", stopOrderRepsonse);
				}
			}

			var knownOrderID = "";
			if (!string.IsNullOrEmpty(knownOrderID))
			{
				var orderInfo = authorizedClient.GetOrderInfo(knownOrderID);
				if (orderInfo.OK)
				{

				}
				else
				{
					Console.WriteLine("Authenticated GetOrderInfo via API Failed: {0}", orderInfo);
				}
			}

			TestGetPrivateTrades();

			TestGetMarketTrades();
			TestGetMarketTradesWithSince();

			if (Over()) return;
			Console.WriteLine();

			var anon_TickerList_ViaApi = anonymousClient.GetTickerList();
			if (anon_TickerList_ViaApi.OK)
			{
				DumpTickerList(anon_TickerList_ViaApi.PayloadResponse);
			}
			else
			{
				Console.WriteLine("Anonymous GetTickerList via API Failed: {0}", anon_TickerList_ViaApi);
			}
			var authed_TickerList_ViaApi = authorizedClient.GetTickerList();
			if (authed_TickerList_ViaApi.OK)
			{
				DumpTickerList(authed_TickerList_ViaApi.PayloadResponse);
			}
			else
			{
				Console.WriteLine("Authenticated GetTickerList via API Failed: {0}", authed_TickerList_ViaApi);
			}

			if (Over()) return;
			Console.WriteLine();

			var anon_TickerInfo_ViaApi = anonymousClient.GetTickerInfoFromApi(BitXPair.XBTZAR);
			if (anon_TickerInfo_ViaApi.OK)
			{
				DumpTickerInfo(anon_TickerInfo_ViaApi.PayloadResponse);
			}
			else
			{
				Console.WriteLine("Anonymous GetTickerInfo via API Failed: {0}", anon_TickerInfo_ViaApi);
			}
			var anon_TickerInfo_ViaWeb = anonymousClient.GetTickerInfoFromWeb(BitXPair.XBTZAR);
			if (anon_TickerInfo_ViaWeb.OK)
			{
				DumpTickerInfo(anon_TickerInfo_ViaWeb.PayloadResponse);
			}
			else
			{
				Console.WriteLine("Anonymous GetTickerInfo via WEB Failed: {0}", anon_TickerInfo_ViaWeb);
			}

			var authed_TickerInfo_ViaApi = authorizedClient.GetTickerInfoFromApi(BitXPair.XBTZAR);
			if (authed_TickerInfo_ViaApi.OK)
			{
				DumpTickerInfo(authed_TickerInfo_ViaApi.PayloadResponse);
			}
			else
			{
				Console.WriteLine("Authenticated GetTickerInfo via API Failed: {0}", authed_TickerInfo_ViaApi);
			}
			var authed_TickerInfo_ViaWeb = authorizedClient.GetTickerInfoFromWeb(BitXPair.XBTZAR);
			if (authed_TickerInfo_ViaWeb.OK)
			{
				DumpTickerInfo(authed_TickerInfo_ViaWeb.PayloadResponse);
			}
			else
			{
				Console.WriteLine("Authenticated GetTickerInfo via WEB Failed: {0}", authed_TickerInfo_ViaWeb);
			}

			if (Over()) return;
			Console.WriteLine();

			authorizedClient.Fail(); // todo: remove. for testing only

			if (Over()) return;
			Console.WriteLine();

			var balances = authorizedClient.GetBalances();
			if (balances.OK)
			{
				foreach (var balance in balances.PayloadResponse)
				{
					DumpBalance(balance);
				}

				if (Over()) return;
				Console.WriteLine();

				foreach (var balance in balances.PayloadResponse)
				{
					Console.WriteLine("=== (Top 5) Transactions for account id {0} ({1}) ===", balance.AccountID, balance.Asset);
					var transactions = authorizedClient.GetTransactions(balance.AccountID, 1, 5);
					if (transactions.OK)
					{
						foreach (var transaction in transactions.PayloadResponse)
						{
							DumpTransaction(transaction);
						}
					}
					else
					{
						Console.WriteLine("Authenticated GetTransactions via API Failed: {0}", transactions);
					}
					Console.WriteLine("=== Pending Transactions for account id {0} ({1}) ===", balance.AccountID, balance.Asset);
					var pendingTransactions = authorizedClient.GetPendingTransactions(balance.AccountID);
					if (pendingTransactions.OK)
					{
						foreach (var pendingTransaction in pendingTransactions.PayloadResponse)
						{
							DumpPendingTransaction(pendingTransaction);
						}
					}
					else
					{
						Console.WriteLine("Authenticated GetPendingTransactions via API Failed: {0}", pendingTransactions);
					}
				}
			}
			else
			{
				Console.WriteLine("Authenticated GetBalances via API Failed: {0}", balances);
			}

			if (Over()) return;
			Console.WriteLine();

			var orderbook = anonymousClient.GetOrderBookFromApi(BitXPair.XBTZAR);
			if (orderbook.OK)
			{
				var sb = new StringBuilder();
				sb.AppendFormat("{0},{1},{2}", "Type", "Price", "Volume");
				sb.AppendLine();
				foreach (var item in orderbook.PayloadResponse.Asks)
				{
					sb.AppendFormat("{0},{1},{2}", "ASK", item.Price, item.Volume);
					sb.AppendLine();
				}
				foreach (var item in orderbook.PayloadResponse.Bids)
				{
					sb.AppendFormat("{0},{1},{2}", "BID", item.Price, item.Volume);
					sb.AppendLine();
				}
				File.WriteAllText(@"d:\temp\bitx\orderbook.csv", sb.ToString());
				Console.WriteLine("OrderBook Dumped to File.");
			}
			else
			{
				Console.WriteLine("Anonymous GetOrderBookFromApi via API Failed: {0}", orderbook);
			}

			if (Over()) return;
			Console.WriteLine();

			var orderbook_ViaApi = anonymousClient.GetOrderBookFromApi(BitXPair.XBTZAR);
			if (orderbook_ViaApi.OK)
			{
				DumpOrderBook(orderbook_ViaApi.PayloadResponse);
			}
			else
			{
				Console.WriteLine("Anonymous GetOrderBook via API Failed: {0}", orderbook_ViaApi);
			}
			Console.WriteLine();

			var orderbook_ViaWeb = anonymousClient.GetOrderBookFromWeb(BitXPair.XBTZAR);
			if (orderbook_ViaWeb.OK)
			{
				DumpOrderBook(orderbook_ViaWeb.PayloadResponse);
				Console.WriteLine();

				foreach (var ask in orderbook_ViaWeb.PayloadResponse.Asks)
				{
					Console.WriteLine("ASK,{0},{1}", ask.Price, ask.Volume);
				}
				foreach (var bid in orderbook_ViaWeb.PayloadResponse.Bids)
				{
					Console.WriteLine("BID,{0},{1}", bid.Price, bid.Volume);
				}
			}
			else
			{
				Console.WriteLine("Anonymous GetOrderBook via WEB Failed: {0}", orderbook_ViaWeb);
			}

			//////var webReq = WebRequest.Create("https://api.mybitx.com/api/1/trades?pair=XBTZAR");
			//////var webResp = webReq.GetResponse();
			//////var sr = new StreamReader(webResp.GetResponseStream());
			//////var respString = sr.ReadToEnd();
			//////Console.WriteLine(respString);
			//////var resp = JsonConvert.DeserializeObject<BitX_TradeQueryResponse>(respString);
			//////Console.WriteLine(resp.trades.Count());

			Console.WriteLine("End of the line");
			Console.ReadKey(true);

		}

		private static void TestGetPrivateTrades()
		{
			var privateTradeInfo = authorizedClient.GetPrivateTradesFromApi(BitXPair.XBTZAR);
			if (privateTradeInfo.OK)
			{
				foreach (var item in privateTradeInfo.PayloadResponse.Trades)
				{
					Console.WriteLine("{0}", item.OrderID);
				}
			}
			else
			{
				Console.WriteLine("Authenticated GetPrivateTradesFromApi via API Failed: {0}", privateTradeInfo);
			}
		}

		private static void TestGetMarketTradesWithSince()
		{
			//var since = BitXUnixTime.BitXUnixTimeFromDateTimeUTC(DateTime.Now.Date.AddDays(-10).ToUniversalTime());
			var since = BitXUnixTime.BitXUnixTimeFromDateTimeUTC(DateTime.Now.Date.ToUniversalTime());
			var ti = anonymousClient.GetMarketTradesFromApi(BitXPair.XBTZAR, since);
			var min = ti.PayloadResponse.Trades.Min(t => t.TimeStampUTC);
			var max = ti.PayloadResponse.Trades.Max(t => t.TimeStampUTC);
		}

		private static void TestGetMarketTrades()
		{
			Console.WriteLine("Trades via API");
			var startDT = DateTime.Now;
			var ti_A = anonymousClient.GetMarketTradesFromApi(BitXPair.XBTZAR);
			var endDT = DateTime.Now;
			if (ti_A.OK)
			{
				Console.WriteLine(ti_A.PayloadResponse.Trades.Count());
			}
			else
			{
				Console.WriteLine("Anonymous GetTrades via API Failed: {0}", ti_A);
			}
			Console.WriteLine("Duration: {0}", endDT - startDT);
			Console.WriteLine();

			//foreach (var item in trades_A.Trades)
			//{
			//    Console.WriteLine("{0,-20} | {1,20:n8} | {2,20} | {3,20:n8}", item.TimeStamp, item.Volume, item.Price, item.Volume * item.Price);
			//}


			Console.WriteLine("Trades via WEB");
			startDT = DateTime.Now;
			var ti_W = anonymousClient.GetMarketTradesFromWeb(BitXPair.XBTZAR);
			endDT = DateTime.Now;

			if (ti_W.OK)
			{
				Console.WriteLine(ti_W.PayloadResponse.Trades.Count());
			}
			else
			{
				Console.WriteLine("Anonymous GetTrades via Web Failed: {0}", ti_W);
			}
			Console.WriteLine("Duration: {0}", endDT - startDT);
			Console.WriteLine();

			//foreach (var item in trades_B.Trades)
			//{
			//    Console.WriteLine("{0,-20} | {1,20:n8} | {2,20} | {3,20:n8}", item.TimeStamp, item.Volume, item.Price, item.Volume * item.Price);
			//}

			if ((ti_A.OK) && (ti_W.OK))
			{
				if (ti_A.PayloadResponse.Trades.Count() != ti_W.PayloadResponse.Trades.Count())
				{
					Console.WriteLine("Count Mismatch");
				}
				var sameCount = 0;
				var diffCount = 0;
				var minDateTime = DateTime.MaxValue;
				var maxDateTime = DateTime.MinValue;
				for (int i = 0; i < Math.Min(ti_A.PayloadResponse.Trades.Count(), ti_W.PayloadResponse.Trades.Count()); i++)
				{
					var a = ti_A.PayloadResponse.Trades[i];
					var b = ti_W.PayloadResponse.Trades[i];
					var areSame = true
						&& (a.Price == b.Price)
						&& (a.TimeStampUTC == b.TimeStampUTC)
						&& (a.TimeStampUTC == b.TimeStampUTC)
						;
					if (areSame)
					{
						sameCount++;
					}
					else
					{
						diffCount++;
					}
					if (a.TimeStampUTC < minDateTime)
					{
						minDateTime = a.TimeStampUTC;
					}
					if (a.TimeStampUTC > maxDateTime)
					{
						maxDateTime = a.TimeStampUTC;
					}
				}
				Console.WriteLine("Same Count: {0}", sameCount);
				Console.WriteLine("Diff Count: {0}", diffCount);
				Console.WriteLine("Min TimeStamp (UTC): {0}", minDateTime);
				Console.WriteLine("Max TimeStamp (UTC): {0}", maxDateTime);
				Console.WriteLine("Min TimeStamp (Local): {0}", minDateTime.ToLocalTime());
				Console.WriteLine("Max TimeStamp (Local): {0}", maxDateTime.ToLocalTime());
				Console.WriteLine();
			}
		}

		private static void Init()
		{
			TryLoadSettings();
			InitClients();
		}

		private static void InitClients()
		{
			authorizedClient = new BitXClient(_Settings.ApiKey, _Settings.ApiSecret);
			anonymousClient = new BitXClient();
		}

		private static void DumpPendingTransaction(Entities.Local.PendingTransactionEntry pendingTransaction)
		{
			Console.WriteLine("=== Pending Transaction ===");
			Console.WriteLine("       Available : {0}", pendingTransaction.Available);
			Console.WriteLine(" Available Delta : {0}", pendingTransaction.AvailableDelta);
			Console.WriteLine("         Balance : {0}", pendingTransaction.Balance);
			Console.WriteLine("   Balance Delta : {0}", pendingTransaction.BalanceDelta);
			Console.WriteLine("        Currency : {0}", pendingTransaction.Currency);
			Console.WriteLine("     Description : {0}", pendingTransaction.Description);
			Console.WriteLine("   BitXTimeStamp : {0}", pendingTransaction.BitXTimeStamp);
			Console.WriteLine("    TimeStampUTC : {0}", pendingTransaction.TimeStampUTC);
		}

		private static void DumpTickerList(Entities.Local.TickerList tickerList)
		{
			Console.WriteLine("===== Ticker List =====");
			Console.WriteLine();
			foreach (var ticker in tickerList.Tickers)
			{
				Console.WriteLine(" ==== Ticker info for Pair: {0} ====", ticker.Pair);
				DumpTickerInfo(ticker, "  ");
				Console.WriteLine();
			}
		}

		private static void DumpOrderBook(Entities.Local.OrderBook orderbook_A)
		{
			Console.WriteLine("=== Order Book ===");
			Console.WriteLine("Asks: {0}", orderbook_A.Asks.Count());
			Console.WriteLine("Bids: {0}", orderbook_A.Bids.Count());
			Console.WriteLine("BitXTimeStamp: {0}", orderbook_A.BitXTimeStamp);
			Console.WriteLine("TimeStampUTC: {0}", orderbook_A.TimeStampUTC);
		}

		private static void DumpTransaction(Entities.Local.TransactionEntry transaction)
		{
			Console.WriteLine("=== Transaction ===");
			Console.WriteLine("        RowIndex : {0}", transaction.RowIndex);
			Console.WriteLine("       Available : {0}", transaction.Available);
			Console.WriteLine(" Available Delta : {0}", transaction.AvailableDelta);
			Console.WriteLine("         Balance : {0}", transaction.Balance);
			Console.WriteLine("   Balance Delta : {0}", transaction.BalanceDelta);
			Console.WriteLine("        Currency : {0}", transaction.Currency);
			Console.WriteLine("     Description : {0}", transaction.Description);
			Console.WriteLine("   BitXTimeStamp : {0}", transaction.BitXTimeStamp);
			Console.WriteLine("    TimeStampUTC : {0}", transaction.TimeStampUTC);
		}

		private static void DumpBalance(Entities.Local.AccountBalance balance)
		{
			Console.WriteLine("=== Balance ===");
			Console.WriteLine("   AccountID : {0}", balance.AccountID);
			Console.WriteLine("       Asset : {0}", balance.Asset);
			Console.WriteLine("     Balance : {0}", balance.Balance);
			Console.WriteLine("    Reserved : {0}", balance.Reserved);
			Console.WriteLine(" Unconfirmed : {0}", balance.Unconfirmed);
			Console.WriteLine("        Name : {0}", balance.Name);
		}

		private static void DumpTickerInfo(Entities.Local.TickerInfo tickerInfo, string headerPrefix = "")
		{
			Console.WriteLine("{0}=== Ticker Info ===", headerPrefix);
			Console.WriteLine("                 Ask : {0}", tickerInfo.Ask);
			Console.WriteLine("                 Bid : {0}", tickerInfo.Bid);
			Console.WriteLine("           LastTrade : {0}", tickerInfo.LastTrade);
			Console.WriteLine("        TimeStampUTC : {0}", tickerInfo.TimeStampUTC);
			Console.WriteLine("       BitXTimeStamp : {0}", tickerInfo.BitXTimeStamp);
			Console.WriteLine(" Rolling24HourVolume : {0}", tickerInfo.Rolling24HourVolume);
		}

		private static bool Over()
		{
			Console.WriteLine("press <esc> to exit.");
			var k = Console.ReadKey(true);
			return k.Key == ConsoleKey.Escape;
		}

		private static void TryLoadSettings()
		{
			try
			{
				_Settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"c:\Configs\BitX\settings.json"));
			}
			catch (Exception exc)
			{
				Console.WriteLine("Error reading config: {0}{1}", NL, exc);
			}
		}

	}
}
