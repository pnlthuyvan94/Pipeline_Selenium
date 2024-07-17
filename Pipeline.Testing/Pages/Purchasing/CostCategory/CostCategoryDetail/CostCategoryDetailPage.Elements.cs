using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Purchasing.CostCategory.CostCategoryDetail
{
    public partial class CostCategoryDetailPage : DetailsContentPage<CostCategoryDetailPage>
    {
        /******************* Cost Category detail page *******************/
        protected Label CostCategoryTitle_lbl => new Label(FindType.XPath, "//header[@class = 'card-header clearfix']/h1[text()='Cost Category']");
        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddccName']");
        protected Textbox Description_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddccDescription']");
        protected DropdownList CostType_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCcTypes']");
        protected Button SaveCostCategory_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");


        /******************* Building Phase *******************/

        private readonly string loading_Xpath = "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgCostCategoryInfo']";
        protected IGrid BuildingPhase_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCostCategoryInfo_ctl00']", loading_Xpath);
        protected Button AddBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAdd']");
        protected Label AddBuildingPhase_lbl => new Label(FindType.XPath, "//*[@id='cc-modal']/section/header/h1[text()='Add Building Phase(s) To Cost Category']");
        protected Button SaveBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertBP']");

    }
}
