using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Purchasing.ReleaseGroup.ReleaseGroupDetail
{
    public partial class ReleaseGroupDetailPage : DetailsContentPage<ReleaseGroupDetailPage>
    {
        /******************* Release Group detail page *******************/
        protected Label ReleaseGroupTitle_lbl => new Label(FindType.XPath, "//header[@class = 'card-header clearfix']/h1[text()='Release Group']");
        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddRGName']");
        protected Textbox Description_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddRGDescription']");
        protected Textbox SortOrder_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddRGSortOrder']");
        protected Button SaveReleaseGroup_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");


        /******************* Building Phase *******************/

        private readonly string loading_Xpath = "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgReleaseGroupInfo']";
        protected IGrid BuildingPhase_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReleaseGroupInfo_ctl00']", loading_Xpath);
        protected Button AddBuildingPhase_btn => new Button(FindType.XPath, "//*[@class='button add']");
        protected Label AddBuildingPhase_lbl => new Label(FindType.XPath, "//*[@id='rg-modal']/section/header/h1[text()='Add Building Phase(s) To Release Group']");
        protected Button SaveBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertBP']");

    }
}
