using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetProducts
{
    public partial class WorksheetProductsPage : DetailsContentPage<WorksheetProductsPage>
    {
        protected DropdownList CommunityBOM_ddl => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlCommunities']");
        protected Button GenerateBOM_btn => new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbGenerateBOM']");
        protected Button GenerateEstimateBOM_btn => new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbGenerateBOMAndEstimate']");
        protected Button ExpandWorksheet => new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgReport_ctl00']/thead/tr/th/input[contains(@title,'Expand')]");
        protected Button ExpandSubWorksheet => new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgReport_ctl00']/tbody/tr[2]/td/table/thead/tr/th/input");

        protected IGrid WorksheetBOM_Grid => new Grid(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgReport_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
        protected IGrid WorksheetBOMProduct_Grid => new Grid(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgReport_ctl00_ctl05_Detail10_ctl06_Detail10']", string.Empty);


        protected Button AddNewQuantity_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddProducts']");
        protected Label AddProductQuantities_lbl => new Label(FindType.XPath, "//*[@class='rwTitle']");
        protected Button OpenBuildingPhase_btn => new Button(FindType.XPath, "//*[@id='rcbPhases_Arrow']");
        protected Button LoadProducts_btn => new Button(FindType.XPath, "//*[@id='Button1Panel']/a");
        protected IGrid ProductQuantitiesPage_Grid => new Grid(FindType.XPath, "//table[@id='rgProductsToAdd_ctl00']", "//*[@id='lp1rgProductsToAdd']/div[1]");
        protected Button SaveProductToAdd_btn => new Button(FindType.XPath, "//*[@id='lbSaveProductsToAdd']");
        protected Button CloseModal_btn => new Button(FindType.XPath, "//*[@class='rwCommandButton rwCloseButton']");
        protected IGrid WorksheetQuantities_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgWorksheetProducts_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheetProducts']/div[1]");
        protected Button WorksheetQuantitiesDelete_btn => new Button(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_rgWorksheetProducts_ctl00_ctl04_btnDelete']");
        protected Button CopyQuantities_btn => new Button(FindType.XPath, "//a[@id = 'ctl00_CPH_Content_lbCopyQuantities']");
        protected DropdownList CopyToPage_ddl => new DropdownList(FindType.XPath, "//select[@id = 'ctl00_CPH_Content_ddlViewType']");
        protected Textbox SelectPage_txt => new Textbox(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_tbFilter']");
        protected Button SaveHouseOptionToCopyQuantity_btn => new Button(FindType.XPath, "//a[@id = 'ctl00_CPH_Content_lbSendQty']");
        protected CheckBox CheckAll_WSQuantity_ckb => new CheckBox(FindType.XPath, "//input[@id = 'ctl00_CPH_Content_rgWorksheetProducts_ctl00_ctl02_ctl02_ClientSelectColumnSelectCheckBox']");
        protected Button Bulk_Action_WSQuantity_btn => new Button (FindType.XPath, "//a[@id = 'bulk-actions']");
        protected Label Delete_Selected_lbl => new Label(FindType.XPath, "//a[@id = 'ctl00_CPH_Content_lbDeleteSelectedProducts']");

        protected Label DeleteAllProduct_lbl => new Label(FindType.XPath, "//a[@id = 'ctl00_CPH_Content_lbDeleteAll']");



    }

}
