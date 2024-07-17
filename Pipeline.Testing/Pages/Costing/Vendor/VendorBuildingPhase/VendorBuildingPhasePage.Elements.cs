using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase
{
    public partial class VendorBuildingPhasePage : DetailsContentPage<VendorBuildingPhasePage>
    {
        protected Button Add => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddBuildingPhase']");
        protected Button SaveBuildingPhase => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertBuildingPhase']");
        protected IGrid BuildingPhaseTable => new Grid(FindType.XPath, "//*[@id = 'ctl00_CPH_Content_rgPhases_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypUtils']");
        protected Button Close_btn => new Button(FindType.XPath, "//*[@class='close']");
    }   

}
