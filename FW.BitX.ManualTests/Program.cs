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
		private static string _ApiKey = null;
		private static string _ApiSecret = null;

		static void Main(string[] args)
		{
			TryLoadSettings();

			var ac = new BitXClient(_ApiKey, _ApiSecret);
			var balances = ac.GetBalances();

			foreach (var balance in balances)
			{
				Console.WriteLine("=== Balance ===");
				Console.WriteLine("   AccountID : {0}", balance.AccountID);
				Console.WriteLine("       Asset : {0}", balance.Asset);
				Console.WriteLine("     Balance : {0}", balance.Balance);
				Console.WriteLine("    Reserved : {0}", balance.Reserved);
				Console.WriteLine(" Unconfirmed : {0}", balance.Unconfirmed);
				Console.WriteLine("        Name : {0}", balance.Name);
			}

			if (Over()) return;
			Console.WriteLine();

			foreach (var balance in balances)
			{
				Console.WriteLine("=== Transactions for account id {0} ({1}) ===", balance.AccountID, balance.Asset);
				var transactions = ac.GetTransactions(balance.AccountID, 1, 5);
				foreach (var transaction in transactions)
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
			}

			if (Over()) return;
			Console.WriteLine();

			var c = new BitXClient();
			DateTime startDT;
			DateTime endDT;

			var orderbook_A = c.GetOrderBookFromApi();
			Console.WriteLine("Asks: {0}", orderbook_A.Asks.Count());
			Console.WriteLine("Bids: {0}", orderbook_A.Bids.Count());
			Console.WriteLine("BitXTimeStamp: {0}", orderbook_A.BitXTimeStamp);
			Console.WriteLine("TimeStampUTC: {0}", orderbook_A.TimeStampUTC);
			Console.WriteLine();

			var orderbook_W = c.GetOrderBookFromWeb();
			Console.WriteLine("Asks: {0}", orderbook_W.Asks.Count());
			Console.WriteLine("Bids: {0}", orderbook_W.Bids.Count());
			Console.WriteLine("BitXTimeStamp: {0}", orderbook_W.BitXTimeStamp);
			Console.WriteLine("TimeStampUTC: {0}", orderbook_W.TimeStampUTC);
			Console.WriteLine();

			foreach (var ask in orderbook_W.Asks)
			{
				Console.WriteLine("A,{0},{1}", ask.Price, ask.Volume);
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
			Console.WriteLine("press <esc> to exit.");
			var k = Console.ReadKey(true);
			return k.Key == ConsoleKey.Escape;
		}

		private static void TryLoadSettings()
		{
			try
			{
				var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"c:\Configs\BitX\settings.json"));
				if (settings != null)
				{
					_ApiKey = settings.ApiKey ?? "";
					_ApiSecret = settings.ApiSecret ?? "";
				}
			}
			catch (Exception exc)
			{
				Console.WriteLine("Error reading config: {0}{1}", NL, exc);
			}
		}

	}
}
