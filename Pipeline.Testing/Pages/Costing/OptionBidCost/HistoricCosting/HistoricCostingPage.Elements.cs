using Pipeline.Common.Controls;
using Pipeline.Common.Enums;



namespace Pipeline.Testing.Pages.Costing.OptionBidCost.HistoricCosting
{
    public partial class HistoricCostingPage : OptionBidCostPage
    {
        public HistoricCostingPage() : base()
        {
        }
        private string _historicCostGrid => "//*[@id='ctl00_CPH_Content_rgCosts_ctl00']";

        protected Grid HistoricCost_Grid => new Grid(FindType.XPath,_historicCostGrid, "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCosts']/div[1]");

        protected Button BackToPrevious_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbBacktoPreviousPage']");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@title='Utilities']");
    }
}
