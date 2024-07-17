using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail.AddBuildingPhaseToBuildingGroup;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail
{
    public partial class BuildingGroupDetailPage
    {
        private BuildingGroupDetailPage EnterBuildingGroupName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                BuildingGroupName_txt.SetText(name);
            return this;
        }

        private BuildingGroupDetailPage EnterBuildingGroupCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
                BuildingGroupCode_txt.SetText(code);
            return this;
        }

        private BuildingGroupDetailPage EnterBuildingGroupDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                BuildingGropDescription_txt.SetText(description);
            return this;
        }

        private void Save()
        {
            Save_btn.Click();
        }

        public BuildingGroupDetailPage UpdateBuildingGroup(BuildingGroupData data)
        {
            EnterBuildingGroupCode(data.Code).EnterBuildingGroupName(data.Name).EnterBuildingGroupDescription(data.Description).Save();
            return this;
        }

        public BuildingGroupDetailPage AddBuildingPhaseToGroup()
        {
            AddBuildingPhase_btn.Click();
            AddBuildingGroupModal = new AddPhaseToGroupModal();
            return this;
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lp1ctl00_CPH_Content_rlbBuildingPhase']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingGroupDetail_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingGroupDetail_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingGroupDetail_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void AddPhaseToGroup(int numberOfBuildingPhase)
        {
            AddBuildingPhaseToGroup();
            List<string> buildingGroupList = AddBuildingGroupModal.GetSpecifiedItemInPhaseList(numberOfBuildingPhase);

            if (!AddBuildingGroupModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Building Phases to Building Group\" modal doesn't display or title is incorrect.");
            }

            // Select 2 first items from the list
            ExtentReportsHelper.LogInformation(" Step 3: Click Add phase button and verify in the grid view.");
            AddBuildingGroupModal.AddPhaseToGroup(numberOfBuildingPhase);
            WaitGridLoad();

            var expectedMessage = "Building Phase(s) were successfully added.";
            var actualMessage = GetLastestToastMessage();

            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add building phase to building group unsuccessfully. Actual messsage: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add building phase to building group successfully.");
            }
            //BuildingGroupDetailPage.Instance.AddBuildingGroupModal.CloseModal();

            // Verify  new items which is added to the grid view
            ExtentReportsHelper.LogInformation("Filter new building phase in the grid view.");
            foreach (string item in buildingGroupList)
            {
                var builidingPhaseCode = item.Split('-')[0];

                // Insert name to filter and click filter by Contain value
                FilterItemInGrid("Code", GridFilterOperator.Contains, builidingPhaseCode);

                bool isFoundItem = IsItemInGrid("Code", builidingPhaseCode);
                if (!isFoundItem)
                {
                    ExtentReportsHelper.LogFail($"The Building phase \"{item} \" was not display on the grid.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"The building phase \"{item}\" was found on the grid view then delete it.");
                    RemovePhaseFromGroup(item);
                }
            }

        }
        public void RemovePhaseFromGroup(string buildingPhase)
        {
            ExtentReportsHelper.LogInformation("Delete builidng phase out ot building group.");

            var builidingPhaseCode = buildingPhase.Split('-')[0];
            DeleteItemInGrid("Code", builidingPhaseCode);
            WaitGridLoad();

            var expectedMessage = "Building Phase successfully removed.";
            var actualMessage = GetLastestToastMessage();

            if (actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Building phase {buildingPhase} deleted successfully!");
                BuildingGroupPage.Instance.CloseToastMessage();
            }
            else
            {
                if (BuildingGroupPage.Instance.IsItemInGrid("Code", builidingPhaseCode))
                    ExtentReportsHelper.LogFail($"Building phase {buildingPhase} can't be deleted!");
                else
                    ExtentReportsHelper.LogPass($"Building phase {buildingPhase} deleted successfully!");

            }
        }

    }
}
