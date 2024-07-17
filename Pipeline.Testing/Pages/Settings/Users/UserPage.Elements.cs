using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
namespace Pipeline.Testing.Pages.Settings.Users
{
    public partial class UserPage : DashboardContentPage<UserPage>
    {
        protected IGrid UserPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgUsers_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUsers']/div[1]");
    }
}
