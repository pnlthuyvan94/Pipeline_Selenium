using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.OptionRooms.AddOptionRoom;

namespace Pipeline.Testing.Pages.Assets.OptionRooms
{
    public partial class OptionRoomPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionRoomPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return OptionRoomPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            OptionRoomPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            OptionRoomPage_Grid.WaitGridLoad();
        }

        public void ClickAddToOpenCreateOptionRoomModal()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddOptionRoomModal = new AddOptionRoomModal();
            System.Threading.Thread.Sleep(500);
        }

        public void SelectItemInGrid(string columnName, string valueToFind)
        {
            OptionRoomPage_Grid.ClickItemInGrid(columnName, valueToFind);
            PageLoad();
        }
    }

}
