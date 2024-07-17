using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Costing.Vendor.VendorCurrentEstimate
{
    public partial class VendorCurrentEstimatePage : DetailsContentPage<VendorCurrentEstimatePage>
    {
        protected Button ExpandAll_btn
             => new Button(FindType.XPath, "//input[@Title='Expand']");

        private string _loadingPage
            => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgReport']/div[1]";
    }

}
