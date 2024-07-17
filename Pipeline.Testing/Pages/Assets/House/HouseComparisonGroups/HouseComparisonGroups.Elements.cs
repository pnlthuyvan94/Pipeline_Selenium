

using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.HouseComparisonGroups
{
    public partial class HouseComparisonGroups : DetailsContentPage<HouseComparisonGroups>
    {

        protected Label NoImportComparion_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//tr[@class='rtlR wrapword']//div[contains(text(),'No Import Comparison Groups to display.')]");
        protected Button SortOption_btn => new Button(FindType.XPath, "//a[@title='Click here to sort' and contains(text(),'Option')]");

        protected IGrid ComparisonGroups_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rtlComparisonGroups']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlComparisonGroups']/div[1]");
    }
}
