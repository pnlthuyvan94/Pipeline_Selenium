
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeDetail
{
    public partial class TradeDetailPage
    {
        public TradeDetailPage EnterTradeName(string name)
        {
            TradeName_txt.SetText(name);
            return this;
        }

        public TradeDetailPage EnterTradeCode(string code)
        {
            TradeCode_txt.SetText(code);
            return this;
        }

        public TradeDetailPage EnterTradeDescription(string description)
        {
            TradeDescription_txt.SetText(description);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            System.Threading.Thread.Sleep(1000);
        }
        public string GetTradeName()
        {
            return TradeName_txt.GetValue();
        }
    }
}
