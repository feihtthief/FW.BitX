using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FW.BitX.Entities.Remote
{
    // https://api.mybitx.com/api/1/trades?pair=XBTZAR

    public class BitX_TradeQueryResponse
    {
        public BitX_Trade[] trades { get; set; }
        public string currency { get; set; }
    }
}
