using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.HouseEstimate
{
    public partial class HouseEstimateDetailPage : DetailsContentPage<HouseEstimateDetailPage>
    {
        protected DropdownList Community_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCommunities']");
        protected Button BomGeneration_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCleanAndGenerateAll']");
        protected Button QueueBomCosts_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbQueueBomCosts']");
        protected Button BomQueueCosts_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbQueueCosts']");
        protected static string loadingIcon_Xpath => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]";
        protected IGrid QuantitiesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReport_ctl00']", loadingIcon_Xpath);
        protected CheckBox Check_All_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReport_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");
    }

}
