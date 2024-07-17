using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Divisions
{
    public partial class DivisionPage : DashboardContentPage<DivisionPage>
    {
         protected IGrid DivisionPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgDivisions_ctl00']"
            , "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgDivisions']/div[1]");

    }
}
