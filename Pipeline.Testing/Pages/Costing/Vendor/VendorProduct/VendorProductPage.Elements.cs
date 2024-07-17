using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Costing.Vendor.VendorProduct
{
    public partial class VendorProductPage : DetailsContentPage<VendorProductPage>
    {
        protected IGrid VendorProductBuildingPhaseTable => new Grid(FindType.Id, "ctl00_CPH_Content_rgBuildingPhases_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']/div[1]");
        protected Label Product_lbl => new Label(FindType.XPath, "//header[@class='card-header clearfix']/h1");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypUtils']");
    }   

}
