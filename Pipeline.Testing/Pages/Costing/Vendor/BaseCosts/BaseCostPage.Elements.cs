using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Costing.Vendor.BaseCosts.AddBaseCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.Vendor.BaseCosts
{
    public partial class BaseCostPage : DashboardContentPage<BaseCostPage>
    {
        public AddBaseCostModal AddBaseCostModal { get; private set; }
        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCosts']/div[1]";

        protected Grid Costs_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCosts_ctl00']", _gridLoading);
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_rgCosts_ctl00_ctl02_ctl00_SaveChangesIcon");
        protected Button Cancel_btn => new Button(FindType.Id, "ctl00_CPH_Content_rgCosts_ctl00_ctl02_ctl00_CancelChangesButton");
        protected Button HistoricCosting_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbHistoricCostings");
        protected Button Add_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbNew");
    }
}
