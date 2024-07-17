using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Purchasing.CostType.CostTypeDetail
{
    public partial class CostTypeDetailPage : DetailsContentPage<CostTypeDetailPage>
    {
        /******************* Cost Type detail page *******************/
        protected Label CostTypeTitle_lbl => new Label(FindType.XPath, "//header[@class = 'card-header clearfix']/h1[text()='Cost Type']");
        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddctName']");
        protected Textbox Description_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddctDescription']");
        protected DropdownList TaxGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTaxGroup']");
        protected Button SaveCostType_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");


        /******************* Cost Categories *******************/
        private readonly string loading_Xpath = "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgCostTypeInfo']";
        protected IGrid CostCategory_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCostTypeInfo_ctl00']", loading_Xpath);
        protected Button AddCostCategory_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAdd']");
        protected Button SaveCostCategory_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertCategory']");
        protected Label AddCostCategory_lbl => new Label(FindType.XPath, "//*[@id='ct-modal']/section/header/h1[text()='Add Cost Categories']");

    }
}
