using FW.BitX.Entities.Local;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FW.BitX.TestApp
{
    public partial class MainForm : Form
    {
        private BitXClient client = new BitXClient();

        private List<Func<Trade, string>> TradeMap;

        public MainForm()
        {
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            TradeMap = BuildTradesMap(lvTrades);
        }

        private List<Func<Trade, string>> BuildTradesMap(ListView listview)
        {
            var result = new List<Func<Trade, string>>();
            foreach (ColumnHeader item in listview.Columns)
            {
                switch (item.Text)
                {
                    case "TimeStamp": result.Add(new Func<Trade, string>(trade => { return trade.TimeStamp.ToLocalTime().ToString(); })); break;
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
            var tradeInfo = client.GetTrades();
            PopulateListView(lvTrades, tradeInfo.Trades, TradeMap);
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

    }
}
