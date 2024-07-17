using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Estimating.Manufactures
{
    public partial class ManufacturerPage : DashboardContentPage<ManufacturerPage>
    {
        protected IGrid ManufacturerPage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgManufacturers_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgManufacturers']/div[1]");
    }
}
