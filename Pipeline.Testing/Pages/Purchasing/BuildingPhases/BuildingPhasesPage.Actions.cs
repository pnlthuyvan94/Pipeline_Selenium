using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.BuildingPhases
{
    public partial class BuildingPhasesPage
    {
        public void ClickItemInGrid(string columnName, string value)
        {
            BuildingPhase_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingPhase_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhase_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']/div[1]");
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingPhase_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhases']/div[1]");
        }

        public void WaitGridLoad()
        {
            BuildingPhase_Grid.WaitGridLoad();
        }


        public void ClickEditItemInGrid(string columnName, string value)
        {
            BuildingPhase_Grid.ClickEditItemInGrid(columnName, value);
            PageLoad();
        }

    }
}
