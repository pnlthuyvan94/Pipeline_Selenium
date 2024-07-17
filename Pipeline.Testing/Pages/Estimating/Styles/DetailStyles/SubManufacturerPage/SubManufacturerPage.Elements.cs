using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Estimating.Styles.DetailStyles.SubManufacturerPage
{
    public partial class SubmanufacturerPage : DetailsContentPage<SubmanufacturerPage>
    {
        protected Textbox SubManufacturerName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtMfgInsert']");

        protected Button SubManufacturerSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertMfg']");

        protected Button SubManufacturerClose_btn => new Button(FindType.XPath, "//*[@id='manuf - modal']/section/header/a");

        protected Label SubManufacturerTitle_lbl => new Label(FindType.XPath, "//h1[text()='Add Manufacturer']");
    }
}
