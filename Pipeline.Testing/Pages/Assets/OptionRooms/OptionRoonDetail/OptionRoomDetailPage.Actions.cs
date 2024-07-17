using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail.AddOptionsToOptionRoom;

namespace Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail
{
    public partial class OptionRoomDetailPage
    {
        private OptionRoomDetailPage EnterOptionRoomName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }

        private OptionRoomDetailPage EnterSortOrder(string sortOrder)
        {
            SortOrder_txt.SetText(sortOrder);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            // Loading grid
            WaitingLoadingGifByXpath(loadingIcon, 5);
        }

        public void UpdateOptionRoom(OptionRoomData data)
        {
            EnterOptionRoomName(data.Name).EnterSortOrder(data.SortOrder).Save();
        }

        public void OpenAddOptionToOptionRoomModal()
        {
            AddOptionToRoom_btn.Click();
            AddOptionToRoomModal = new AddOptionToRoomModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Options_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Options_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Options_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            Options_Grid.WaitGridLoad();
        }
    }
}
