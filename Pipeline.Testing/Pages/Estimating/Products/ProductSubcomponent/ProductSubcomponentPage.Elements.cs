using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent
{
    public partial class ProductSubcomponentPage : DetailsContentPage<ProductSubcomponentPage>
    {
        // Element of Product Subcomponent page 
        protected Label Subcomponent_Title => new Label(FindType.XPath, "//h1[contains(text(),'Subcomponents')]");
        protected DropdownList View_ddl => new DropdownList(FindType.XPath, "//*[contains(@name,'ddlViewType')]");
        protected Button AddSubcomponent_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddSubcomponent']");
        ///*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddSubcomponent"]/div[1]
        protected Button SaveSubcomponent_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveAddSubcomponent']");
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveAddSubcomponent"]/div[1]
        protected Button AssignShowProduct_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAssignShowProduct']");
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAssignShowProduct"]/div[1]
        protected Button CopySubcomponent_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCopySubcomponents']");
        protected DropdownList FilterSubComponent_ddl => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlProductBuildingPhaseFilter']");

        

        //----------- Add Product Subcomponent Modal ---------------//
        protected Label ProductSubcomponentModal => new Label(FindType.XPath, "//h1//span[contains(text(),'Add Product Subcomponent')]");
        
        protected Button Basic_btn => new Button(FindType.XPath, "//label[contains(text(),'Basic')]");
        protected Button Advanced_btn => new Button(FindType.XPath, "//label[contains(text(),'Advanced')]");
        protected DropdownList BuildingPhaseofProduct => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlAddParentBuildingPhase']");
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlAddParentBuildingPhase"]/div[1]
        protected DropdownList StylelofProduct => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlAddParentStyle']");
        protected DropdownList CatelogyOfSubcomponent => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlAddChildCategory']");
        protected Textbox ProductWithCategorySubcomponentName => new Textbox(FindType.XPath, "//input[@id='ctl00_CPH_Content_ddlProductWithCategory_Input']");
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlProductWithCategory"]/div[1]

        protected DropdownList BuildingPhaseWithCategory_ddl => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlBuildingPhaseWithCategory']");
        protected DropdownList ChildBuildingPhaseOfSubcomponent => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlAddChildBuildingPhase']");

        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlBuildingPhaseWithCategory"]/div[1]
        protected DropdownList StyleOfSubcomponent => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlAddChildProductStyle']");
        protected DropdownList ChildStyleOfSubcomponent => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlAddChildProductStyle']");
        protected DropdownList ChildUseOfSubcomponent_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddUse']");
        protected DropdownList UpdateChildUseOfSubcomponent_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlEditUse']");
        protected Button EnableOrDisableGird => new Button(FindType.XPath, "//div[@id='ctl00_CPH_Content_rtlSubcomponents']/table//tbody/tr[contains(@id,'ctl00_CPH_Content_rtlSubcomponents')]//td//input[@id='ctl00_CPH_Content_rtlSubcomponents_ctl02_ibEditDisabled']");
        protected DropdownList CalculationSubcomponent_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddCalculation']");
        protected Textbox OptionSubcomponent_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_tbFilter']");
        //Gird
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlSubcomponents"]/div[1]
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlSubcomponents"]/div[1]

        //--------------------- Save modal --------------------//

        protected Button SaveEditSubComponent => new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbSaveEditSubcomponent']");
       
        //*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveEditSubcomponent']/div[1]
        protected Label EditSubcomponentModal => new Label(FindType.XPath, "//h1//span[contains(text(),'Edit Subcomponent')]");
        protected DropdownList UseEditSubcomponent => new DropdownList(FindType.XPath, "//select[contains(@id,'ctl00_CPH_Content_ddlEditUse')]");

        protected Button SubcomponentBulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions1']");
        protected CheckBox AllSubcomponent_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rtlSubcomponents']//input[@class='chkAll-subcomponents']");
        //-------------------- Assign Product Modal -----------------//
        //div[@id='assign-showproduct-modal']//span[contains(text(),'004-HN-Phase-4')]//..//input
        protected Button AssignProductModal_Save_btn => new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbSaveAssignShowProduct']");
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveAssignShowProduct"]/div[1]

        //-------------------- Copy Subcomponent Modal -----------------//
        protected Button SelectiveCopy_btn => new Button(FindType.XPath, "//label[contains(text(),'Selective Copy')]");
        protected Button BatchCopy_btn => new Button(FindType.XPath, "//label[contains(text(),'Batch Copy')]");

        protected Button CopySubcomponentModal_Save_btn => new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbSaveCopySubcomponents']");
        protected DropdownList CopySubcomponentModal_BuildingPhaseTo_ddl => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlToBuildingPhase']");
        protected DropdownList CopySubcomponentModal_BuildingPhaseFrom_ddl => new DropdownList(FindType.XPath, "//select[@id='ctl00_CPH_Content_ddlFromBuildingPhase']");

        protected Button CopySubcomponentBatchCopyModal_Save_btn => new Button(FindType.XPath, "//a[@id='ctl00_CPH_Content_lbSaveCopySubcomponentsBatch']");


        //==============================================================//


        protected Button SavebtnConvertTo => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbSaveSpecSet2')]");
        protected Button AddbtnConvertFrom => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbNewSpecSet1')]");
        protected Button AddbtnConvertTo => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbNewSpecSet2')]");
        protected Button Accept_btn => new Button(FindType.XPath, "//tbody/tr/td/input[contains(@src,'accept')]");
        protected Label ConvertFromModal => new Label(FindType.XPath, "//h1[contains(text(),'Convert From')]");
        protected Label ConvertToModal => new Label(FindType.XPath, "//h1[contains(text(),'Convert To')]");
        protected IGrid ConvertFrom_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgConvertFrom_ctl00", "[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgConvertFrom']/div[1]");
        protected IGrid ConvertTo_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgConvertTo_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgConvertTo']/div[1]");
        protected Label NoSubcomponent => new Label(FindType.XPath, "//div[.='No subcomponents to display.']");

        protected Textbox UseSubcomponent_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rtlSubcomponents_ctl02_lblUse']");
        //Add Style & Add Manufacture
        protected Label AddStyleModalTitle_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlNewStyle']/p[contains(text(),'Add Manufacturer and Style')]");
        protected Textbox InputManufacturer_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewManufacturer']");
        protected Button InsertManufacturer_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertManufacturer']");

        protected Label ErrorManufacturer_lbl => new Label(FindType.XPath, "//*[@id='error-manufacturer']");
        protected Button CancelManufacturer_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCancelManufacturer']");

        protected Textbox InputStyle_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewStyle']");
        protected Button InsertStyle_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertStyle']");
        protected Label ErrorStyle_lbl => new Label(FindType.XPath, "//*[@id='error-style']");
        protected Button CancelStyle_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCancelStyle']");
        protected DropdownList SelectManufacture_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewManufacturer']");
        protected DropdownList SelectStyle_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewStyle']");

        protected Textbox ProductCode_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtProductCode']");
        protected Button SaveStyle_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbStyleSave']");

        protected Button CloseStyle_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbStyleClose']");

    }
      
}
