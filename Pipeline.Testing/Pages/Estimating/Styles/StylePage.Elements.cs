using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Estimating.Styles
{
    public partial class StylePage : DashboardContentPage<StylePage>
    {
        protected IGrid StylePage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgStyles_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyles']/div[1]");
        protected Button AddStyle_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypAddNew']");
    }
}
