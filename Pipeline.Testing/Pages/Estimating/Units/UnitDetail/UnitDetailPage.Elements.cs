using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.Units.UnitDetail.AddProductToUnit;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Units.UnitDetail
{
    public partial class UnitDetailPage : DetailsContentPage<UnitDetailPage>
    {
        public AddProductToUnitModal AddProductToUnitModal { get; private set; }

        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox Abbreviation_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtType']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");
        string loadingIcon => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_lbLoadingAnimation']/div[1]";


        // Product

        protected Button AddProductToUnit_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddProduct']");

        protected Label ProductTitle_lbl => new Label(FindType.XPath, "//h1[text()='Products']");

        protected IGrid ProductDetail_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProducts_ctl00']", "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rlbProducts']/div[1]");
        protected Button CloseModal_btn => new Button(FindType.XPath, "//*[@class='close']");

    }
}
