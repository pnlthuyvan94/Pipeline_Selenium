using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.Communities.CommunityVendor.AddVendor;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityVendor
{
    public partial class CommunityVendorPage : DashboardContentPage<CommunityVendorPage>
    {
        public CommunityVendorModal CommunityVendorModal { get; private set; }
        protected Button AddVendor_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddVendor");

         protected IGrid AllowedVendors_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgAllowVendors_ctl00"
            , "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgAllowVendors']/div[1]");

        protected IGrid VendorAssignments_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPhases_ctl00']"
            , "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");

    }
}
