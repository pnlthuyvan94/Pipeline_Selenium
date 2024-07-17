using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddProductToPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddVendorToPhase;

namespace Pipeline.Testing.Pages.Purchasing.BuildingPhase.BuildingPhaseDetail
{
    public partial class BuildingPhaseDetailPage
    {
        public void DeleteItemInVendorsGrid(string columnName, string value)
        {
            Vendors_grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendors']");
        }
        public bool IsItemInVendorGrid(string columnName, string value)
        {
            return Vendors_grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInVendorGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Vendors_grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgVendors']");
        }

        public void ClickAddVendorToPhaseModal()
        {
            AddVendorToPhaseModal = new AddVendorToPhaseModal();
            AddVendorToBuildingPhase_btn.Click();
        }

    }
}
