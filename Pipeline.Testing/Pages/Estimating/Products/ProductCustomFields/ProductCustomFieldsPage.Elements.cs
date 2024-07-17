using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Estimating.Products.ProductCustomFields
{
    public partial class ProductCustomFieldsPage : DetailsContentPage<ProductCustomFieldsPage>
    {
        // Element of Product Assignment page 
        protected Button Add_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNew']");
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddNew"]/div[1]
        protected Button Insert_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsert']");
        protected Button Close_btn => new Button(FindType.XPath, "//a[contains(text(),'close')]");
        
        protected Button Remove_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbRemove']");
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbRemove"]/div[1]
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave']");
        //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave"]/div[1]



        protected Button SavebtnConvertFrom => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbSaveSpecSet1')]");
        protected Button SavebtnConvertTo => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbSaveSpecSet2')]");
        protected Button AddbtnConvertFrom => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbNewSpecSet1')]");
        protected Button AddbtnConvertTo => new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_lbNewSpecSet2')]");
        protected Button Accept_btn => new Button(FindType.XPath, "//tbody/tr/td/input[contains(@src,'accept')]");
        protected Label ConvertFromModal => new Label(FindType.XPath, "//h1[contains(text(),'Convert From')]");
        protected Label ConvertToModal => new Label(FindType.XPath, "//h1[contains(text(),'Convert To')]");
        protected IGrid ConvertFrom_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgConvertFrom_ctl00", "[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgConvertFrom']/div[1]");
        protected IGrid ConvertTo_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgConvertTo_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgConvertTo']/div[1]");
    }
      
}
