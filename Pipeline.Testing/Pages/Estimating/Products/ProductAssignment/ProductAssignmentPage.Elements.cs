using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductAssignment
{
    public partial class ProductAssignmentPage : DetailsContentPage<ProductAssignmentPage>
    {
        // Element of Product Assignment page 
        protected DropdownList View_ddl => new DropdownList(FindType.XPath, "//*[contains(@name,'ddlViewType')]");
        protected Button SavebtnConvertFrom => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbSaveSpecSet1')]");
        protected Button SavebtnConvertTo => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbSaveSpecSet2')]");
        protected Button AddbtnConvertFrom => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbNewSpecSet1')]");
        protected Button AddbtnConvertTo => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbNewSpecSet2')]");
        protected Button Accept_btn => new Button(FindType.XPath, "//tbody/tr/td/input[contains(@src,'accept')]");
        protected Label ConvertFromModal => new Label(FindType.XPath, "//h1[contains(text(),'Convert From')]");
        protected Label ConvertToModal => new Label(FindType.XPath, "//h1[contains(text(),'Convert To')]");
        protected IGrid ConvertFrom_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgConvertFrom_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]");
        protected IGrid ConvertTo_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgConvertTo_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl01']/div[1]");
        protected DropdownList CovertFromStyle_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOriginalStyle1']");
        protected DropdownList ConvertFromSpecSetGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSpecSetGroup1']");
        protected DropdownList ConvertFromSpecSet_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSpecSet1']");
        protected DropdownList ConvertFromUse_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOriginalNewUse1']");
        protected DropdownList ConvertFromBuildingGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingGroup1']");
        protected DropdownList ConvertFromBuildingPhase_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingPhase1']");
        protected DropdownList ConvertFromProduct_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlProduct1']");
        protected DropdownList ConvertFromNewStyle_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewStyle1']");
        protected DropdownList ConvertFromNewUse_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewUse1']");
        protected DropdownList ConvertFromCalculation_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCalculation1']");

        protected DropdownList CovertToSNewtyle_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewStyle2']");
        protected DropdownList ConvertToSpecSetGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSpecSetGroup2']");
        protected DropdownList ConvertToSpecSet_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSpecSet2']");
        protected DropdownList ConvertToNewUse_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOriginalNewUse2']");
        protected DropdownList ConvertToBuildingGroup_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingGroup2']");
        protected DropdownList ConvertToBuildingPhase_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingPhase2']");
        protected DropdownList ConvertToProduct_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlProduct2']");
        protected DropdownList ConvertToStyle_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOriginalStyle2']");
        protected DropdownList ConvertToUse_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewUse2']");
        protected DropdownList ConvertToCalculation_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCalculation2']");
    }
      
}
