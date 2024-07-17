using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.Builder.Lot
{
    public partial class LotSettingPage
    {
        public bool IsItemDisplayOnScreen(string lotStatusName)
        {
            int pageSize = LotStatus_Grid.GetTotalPages;
            for (int i = 1; i <= pageSize; i++)
            {
                if (LotStatus_Grid.IsItemOnCurrentPage("Lot Status", lotStatusName))
                    return true;
                if (i != pageSize)
                    LotStatus_Grid.NavigateToPage(i + 1);
            }
            return false;
        }

        public bool IsLotStatusExist(string lotStatusName)
        {
            CommonHelper.MoveToElement(AvailableDropdowListOnSystemStatus_ddl, true);
            return AvailableDropdowListOnSystemStatus_ddl.IsItemInList(lotStatusName);
        }
        public void ChangePageSize(int PageSize)
        {
            LotStatus_Grid.ChangePageSize(PageSize);
            LotStatus_Grid.WaitGridLoad();
        }
    }

}
