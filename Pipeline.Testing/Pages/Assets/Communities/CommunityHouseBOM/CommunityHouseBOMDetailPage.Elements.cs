using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityHouseBOM
{
    public partial class CommunityHouseBOMDetailPage : DetailsContentPage<CommunityHouseBOMDetailPage>
    {
        protected Button BomGeneration_btn => new Button(FindType.XPath, "//*[@id='bomtools']");
        protected Button GenerateAll_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbGenerateAll']");
        protected static string loadingIcon_Xpath => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]";
        protected IGrid QuantitiesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSuperOptions_ctl00']", loadingIcon_Xpath);
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypUtils']");
        protected CheckBox CheckAll_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_lsbBuildingPhase']//input[@class='rlbCheckAllItemsCheckBox']");
        protected Textbox BuildingPhaseExport_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBuildingPhase']");
        protected Button ExportSelectedHouse_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbExportSelectedHouseBOM']");
        protected Button ExportAllHouse_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbExportAllHouseBOM']");
        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='export-modal']//a[@class='close']");
        protected IGrid HouseBOMDetailPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSuperOptions_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
        protected Button AllHouseName_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSuperOptions_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");
    }

}
