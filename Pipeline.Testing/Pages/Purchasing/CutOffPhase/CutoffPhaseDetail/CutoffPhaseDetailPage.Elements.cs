using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase.CutoffPhaseDetail
{
    public partial class CutoffPhaseDetailPage : DetailsContentPage<CutoffPhaseDetailPage>
    {
        /******************* Cutoff Phase detail page *******************/
        protected Label CutoffPhaseTitle_lbl => new Label(FindType.XPath, "//header[@class = 'card-header clearfix']/h1[text()='Cutoff Phase']");
        protected Textbox Code_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddCode']");
        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddName']");
        protected Textbox SortOrder_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddSortOrder']");
        protected Button SaveCutoffPhase_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");
        protected Button Utilities_btn => new Button(FindType.XPath, "//*[@data-original-title='Utilities']");


        /******************* Building Phase *******************/
        private string buildingPhaseLoading_Xpath = "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgBuildingPhasesInfo']";
        protected IGrid BuildingPhase_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBuildingPhasesInfo_ctl00']", buildingPhaseLoading_Xpath);
        protected Button AddBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddBuildingPhases']");
        protected Label AddBuildingPhase_lbl => new Label(FindType.XPath, "//*[@id='rg-buildingPhases-modal']/section/header/h1[text()='Add Building Phases(s) To Cutoff Phase']");
        protected Button SaveBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertBuildingPhases']");


        /******************* Option Group *******************/
        private string optionGroupLoading_Xpath = "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgOptionGroupsInfo']";
        protected IGrid OptionGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptionGroupsInfo_ctl00']", optionGroupLoading_Xpath);
        protected Button AddOptionGroup_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddOptionGroup']");
        protected Label AddOptionGroup_lbl => new Label(FindType.XPath, "//*[@id='rg-option-group-modal']/section/header/h1[text()='Add Option Group(s) To Cutoff Phase']");
        protected Button SaveOptionGroup_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertOptionGroup']");

        /******************* Option *******************/
        private string optionLoading_Xpath = "//*[@id='//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgOptionsInfo']']";
        protected IGrid Option_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgOptionsInfo_ctl00']", optionLoading_Xpath);
        protected Button AddOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddOption']");
        protected Label AddOption_lbl => new Label(FindType.XPath, "//*[@id='options-modal']/section/header/h1[text()='Add Option(s) To Cutoff Phase']");
        protected Button SaveOption_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertOption']");

    }
}
