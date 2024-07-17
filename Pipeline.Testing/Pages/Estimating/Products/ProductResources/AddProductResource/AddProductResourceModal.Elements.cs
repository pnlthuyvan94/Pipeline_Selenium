using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Estimating.Products.ProductResources.AddProductResource
{
    public partial class AddProductResourceModal : ProductResourcePage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//h1[text()='Add Resource']");

        protected Textbox Title_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtTitle']");

        protected Textbox Source_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_fuProductImage']");

        protected CheckBox PrimaryResource_ckb => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ckbAddIsMain']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertResource']");

        protected Button Close_btn => new Button(FindType.XPath, "//*[@id='addresource-modal']/div/article/section/article/header/a");
    }
}
