using Pipeline.Common.Enums;
using Pipeline.Testing.Pages.Estimating.BuildingPhaseType.AddType;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhaseType
{
    public partial class BuildingPhaseTypePage 
    {
        public void ClickAddToShowBuildingPhaseTypeModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddTypeModal = new AddTypeModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingPhaseType_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhaseType_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhaseTypes']/div[1]");
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingPhaseType_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            string loadingXPath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhaseTypes']/div[1]";
            WaitingLoadingGifByXpath(loadingXPath);
            //BuildingPhaseType_Grid.WaitGridLoad();
        }

        public void ClickEditItemInGrid(string columnName, string value)
        {
            BuildingPhaseType_Grid.ClickEditItemInGrid(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhaseTypes']/div[1]");
        }

        public void Update_BuildingPhaseTypes(string newBuildingPhaseType)
        {
            BuildingPhaseTypeUpdate_txt.SetText(newBuildingPhaseType);
            BuildingPhaseTypeUpdate_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingPhaseTypes']/div[1]");

        }
    }
}
