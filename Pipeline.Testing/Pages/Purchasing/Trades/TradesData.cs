
using System.Windows.Forms;

namespace Pipeline.Testing.Pages.Purchasing.Trades
{
    public class TradesData
    {
        public TradesData()
        {
            TradeName = string.Empty;
            Code = string.Empty;
            TradeDescription = string.Empty;
            IsBuilderVendor = false;
            Vendor = string.Empty;
            BuilderVendor = string.Empty;
            BuildingPhases = string.Empty;
            SchedulingTasks = string.Empty;
        }

        public string TradeName { get; set; }
        public string Code { get; set; }
        public string TradeDescription { get; set; }
        public bool IsBuilderVendor { get; set; }
        public string Vendor { get; set; }
        public string BuilderVendor { get; set; }
        public string BuildingPhases { get; set; }
        public string SchedulingTasks { get; set; }
    }
}