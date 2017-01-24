using FW.BitX.Entities.Local;
using FW.BitX.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FW.BitX.TestApp
{
	public partial class MainForm : Form
	{
		private static readonly string NL = Environment.NewLine;

		private List<Func<Trade, string>> TradeMap;

		public MainForm()
		{
			InitializeComponent();
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			TradeMap = BuildTradesMap(lvTrades);
			TryLoadSettings();
		}

		private void TryLoadSettings()
		{
			try
			{
				var settings = JsonConvert.DeserializeObject<Settings>(File.ReadAllText(@"c:\Configs\BitX\settings.json"));
				if (settings != null)
				{
					txtApiKey.Text = settings.ApiKey ?? "";
					txtApiSecret.Text = settings.ApiSecret ?? "";
				}
			}
			catch (Exception exc)
			{
				Notice(String.Format("Error reading config: {0}{1}", NL, exc));
			}
		}

		private void Notice(string message)
		{
			lblMessages.Text = message;
		}

		private List<Func<Trade, string>> BuildTradesMap(ListView listview)
		{
			var result = new List<Func<Trade, string>>();
			foreach (ColumnHeader item in listview.Columns)
			{
				switch (item.Text)
				{
					case "TimeStamp": result.Add(new Func<Trade, string>(trade => { return trade.TimeStampUTC.ToLocalTime().ToString(); })); break;
					case "Price": result.Add(new Func<Trade, string>(trade => { return trade.Price.ToString(); })); break;
					case "Volume": result.Add(new Func<Trade, string>(trade => { return trade.Volume.ToString(); })); break;
					default:
						result.Add(new Func<Trade, string>(trade => { return item.Text + "???"; }));
						break;
				}
			}
			return result;
		}

		private void btnGetTrades_Click(object sender, EventArgs e)
		{
			var client = new BitXClient();
			var tradeInfo = client.GetTradesFromApi(BitXPair.XBTZAR);
			if (tradeInfo.OK) { 
				PopulateListView(lvTrades, tradeInfo.PayloadResponse.Trades, TradeMap);
			}
			//lvTrades.BeginUpdate();
			//try
			//{
			//    lvTrades.Items.Clear();
			//    foreach (var trade in tradeInfo.Trades)
			//    {
			//        var item = lvTrades.Items.Add("");
			//        item.SubItems.Add(trade.TimeStamp.ToString());
			//        item.SubItems.Add(trade.Price.ToString());
			//        item.SubItems.Add(trade.Volume.ToString());
			//    }
			//}
			//finally
			//{
			//    lvTrades.EndUpdate();
			//}
		}

		private void PopulateListView<T>(ListView listView, List<T> list, List<Func<T, string>> map)
		{
			listView.BeginUpdate();
			try
			{
				listView.Items.Clear();
				foreach (var itemx in list)
				{
					var newListViewItem = listView.Items.Add(map[0](itemx));
					for (int i = 1; i < map.Count; i++)
					{
						newListViewItem.SubItems.Add(map[i](itemx));
					}
				}
			}
			finally
			{
				listView.EndUpdate();
			}
			foreach (ColumnHeader col in listView.Columns)
			{
				col.AutoResize(ColumnHeaderAutoResizeStyle.ColumnContent);
			}
		}

		private void btnGetTransactions_Click(object sender, EventArgs e)
		{
			try
			{
				var client = new BitXClient(txtApiKey.Text, txtApiSecret.Text);
				client.GetBalances();
			}
			catch (Exception exc)
			{
				Notice(String.Format("Error reading config: {0}{1}", NL, exc));
			}
		}

	}
}
