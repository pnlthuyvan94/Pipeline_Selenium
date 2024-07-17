using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades.VendorAssignments
{
    public partial class VendorAssignmentsPage : DashboardContentPage<VendorAssignmentsPage>
    {
        protected Button BackToTrades_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbBackToTrades");

        protected DropdownList Divisions_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlDivisions");
        protected ListItem Communities_list => new ListItem(FindElementHelper.FindElements(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcCommunities_DropDown']/div[1]/ul/li"));
        protected Button LoadVendors_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbViewDivisionVendors");
        protected Grid VendorAssignments_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgTradesToVendors_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]");

        protected Grid VendorAssignmentsHeader_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgTradesToVendors_ctl00_Header", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTradesToVendors']/div[1]");

        protected Button CommunitiesArrow_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcCommunities_Arrow']");
    }
}
