using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using System.IO;
using System.Reflection;
using Pipeline.Testing.Pages.Costing.OptionBidCost.AddOptionBidCost;
using Pipeline.Testing.Pages.Costing.OptionBidCost.AddJobOptionBidCost;
using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Costing.OptionBidCost.HistoricCosting;

namespace Pipeline.Testing.Pages.Costing.OptionBidCost
{
    public partial class OptionBidCostPage : DashboardContentPage<OptionBidCostPage>
    {
        public AddOptionBidCostModal AddOptionBidCostModal { get; private set; }
        public AddJobOptionBidCostModal AddJobOptionBidCostModal { get; private set; }
        public HistoricCostingPage HistoricCostingPage { get; private set; }
        public OptionBidCostPage() : base()
        {

        }

        protected DropdownList Vendor_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlVendors']");
        protected IGrid BidCost_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBidCosts_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBidCosts']/div[1]");
        protected IGrid JobBidCost_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgJobBidCosts_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobBidCosts']/div[1]");
        protected DropdownList Job_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlJobs']");
        protected Button AddBidCost_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewBidCost']");
        protected Button AddJobBidCost_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewJobBidCost']");
        protected Button TypeTier_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBidCosts_ctl00_ctl02_ctl02_ddlCostingTier_Arrow']");
        protected Button HistoricCosting_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbHistoricCostings']");
    }
}
