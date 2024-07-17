using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionProduct
{
    public partial class CustomOptionProduct : DetailsContentPage<CustomOptionProduct>
    {
        protected Button AddProduct_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNew']");

        protected Button CoppyProduct => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCopyQuantities']");

        protected DropdownList BuildPhase_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingPhases']");

        protected Textbox Product_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Input']");
        protected DropdownList Style_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStyles']");
        protected DropdownList Use_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlUses']");
        protected Textbox Quantity_dll => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtQuantity']");
        protected DropdownList Source_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewSourceTypes']");
        protected Button SaveProduct_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");
        protected Button Close_btn => new Button(FindType.XPath, "//div[@id='options-modal']//section/header/a[contains(text(),'close')]");
        protected Button CopyQuantities_btn => new Button(FindType.XPath, "//section/a[contains(text(),'Copy Quantities to Selected Option(s)')]");
        protected Button GenerateBOMEstimate_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbGenerateBOMAndEstimate']");
        protected Button GenerateBOM_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbGenerateBOM']");
        protected IGrid CustomOptionBOM_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReport_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");

        protected IGrid CustomOptionProduct_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProducts_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl00']/div[1]");
        protected Button CopyProductQuantities_btn => new Button(FindType.XPath, "//a[contains(text(),'Copy Product Quantities')]");
        protected Button CloseCopyProductQuantities_btn => new Button(FindType.XPath, "//h1[contains(text(),'Copy Product Quantities')]/following-sibling::a");
        protected DropdownList CommunityBOM_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCommunities']");
        protected Button ExpainItem_BOMGrid => new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgReport_ctl00']/descendant::input[1]");
        protected Button ExpainSubItem_BOMGrid => new Button(FindType.XPath, "//table[contains(@id,'ctl00_CPH_Content_rgReport_ctl00_ctl05_Detail10')]/descendant::input[1]");
        protected Button product_addproduct_expand_btn => new Button(FindType.XPath, "//button[@class='rcbActionButton']");
    }
}
