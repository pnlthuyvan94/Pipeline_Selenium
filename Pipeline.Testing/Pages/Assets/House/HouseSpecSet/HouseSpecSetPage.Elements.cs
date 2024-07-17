using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.HouseSpecSet
{
    public partial class HouseSpecSetPage : DetailsContentPage<HouseSpecSetPage>
    {
        protected IGrid HouseSpecSet_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00']", "");
        protected Label SpecSetsHeaderTitle_Lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets']");
    }
}
