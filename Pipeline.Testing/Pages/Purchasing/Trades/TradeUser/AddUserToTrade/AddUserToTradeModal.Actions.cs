using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeUser.AddUserToTrade
{
    public partial class AddUserToTradeModal
    {
        public AddUserToTradeModal SelectUser(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                Users_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }

        public void Save()
        {
            Save_btn.Click(false);
        }
    }
}
