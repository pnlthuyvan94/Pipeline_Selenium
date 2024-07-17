using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Communities.Products
{
    public partial class CommunityProductsPage : DashboardContentPage<CommunityProductsPage>
    {
        protected IGrid CommunityQuantitiesGrid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCommunityQuantities_ctl00']",
    "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunityQuantities']/div[1]");
        protected Button BulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions']");
        protected Button CloseModal_btn => new Button(FindType.XPath, "//*[@class='close']");
    }
}
