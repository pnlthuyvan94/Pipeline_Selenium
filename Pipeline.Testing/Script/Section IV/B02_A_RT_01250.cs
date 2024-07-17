using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class B02_A_RT_01250 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private BuildingGroupData oldData;
        private BuildingGroupData newTestData;
        private int numberOfBuildingPhase = 2;

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            oldData = new BuildingGroupData()
            {
                Name = "R-QA Only Building Group Auto",
                Code = "622",
                Description = "Regression QA Only Test Group created by Automation"
            };

            newTestData = new BuildingGroupData()
            {
                Name = "R-QA Only Building Group Auto_Update",
                Code = "622",
                Description = "Regression QA Only Test Group created by Automation_Update"
            };

            // Step 1: Navigate Estimating > Building Group and open Building Group Detail page
            ExtentReportsHelper.LogInformation(" Step 1: Navigate Estimating menu >  Building Group and open Building Group Detail page.");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            // Insert name BuildingGroupPage.Instance.AddNewBuildingGroup(oldData); filter and click filter by Contain value
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, oldData.Code);

            bool isFoundOldItem = BuildingGroupPage.Instance.IsItemInGrid("Code", oldData.Code);
            if (!isFoundOldItem)
            {
                // Step 2: click on "+" Add button
                BuildingGroupPage.Instance.AddNewBuildingGroup(oldData);

                // Close modal
                //BuildingGroupPage.Instance.AddBuildingGroupModal.CloseModal();

                // Verify the new Building Phase create successfully
                BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, oldData.Code);
            }
        }

        [Test]
        [Category("Section_IV")]
        public void B02_A_Estimating_DetailPage_BuildingGroup_BuildingGroupDetails()
        {
            // Select filter item to open detail page
            BuildingGroupPage.Instance.SelectItemInGrid("Code", oldData.Code);

            // Verify open building group detail page display correcly
            if (BuildingGroupDetailPage.Instance.IsTitleBuildingGroup(oldData.Code + "-" + oldData.Name) is true)
                ExtentReportsHelper.LogPass($"The building group {oldData.Name} displays sucessfully with URL: {BuildingGroupDetailPage.Instance.CurrentURL}");
            else
                ExtentReportsHelper.LogFail($"The building group {oldData.Name} displays with incorrect sub header.");

            // Step 2: Add Building Phase to Building Group
            ExtentReportsHelper.LogInformation(" Step 2: Add building phase to building group. ");
            BuildingGroupDetailPage.Instance.AddPhaseToGroup(numberOfBuildingPhase);

            // Update Building Group detail
            ExtentReportsHelper.LogInformation(" Step 5: Update information of Building Group detail page.");
            UpdateBuildingGroup();

            // Back to Building Group page and delete item
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);
            ExtentReportsHelper.LogInformation($"Filter Building Group {newTestData.Code} - {newTestData.Name} in the grid then delete it.");
            FilterBuildroupOnGrid();

        }


        private void UpdateBuildingGroup()
        {
            BuildingGroupDetailPage.Instance.UpdateBuildingGroup(newTestData);

            var expectedMessage = "Building Group updated successfully.";
            var actualMessage = BuildingGroupDetailPage.Instance.GetLastestToastMessage();
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
            }
            else if (expectedMessage == actualMessage)
            {
                ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");
                if (BuildingGroupDetailPage.Instance.IsUpdateDataCorrectly(newTestData))
                    ExtentReportsHelper.LogPass("The updated data displays correctly in the grid view.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
            }
            BuildingGroupDetailPage.Instance.CloseToastMessage();
        }

        private void FilterBuildroupOnGrid()
        {
            // Step 4: Verify the filter building group
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, newTestData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", newTestData.Code))
            {
                ExtentReportsHelper.LogPass("New building group is filtered successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail("New building group is filtered unsuccessfully");
            }
        }

        [TearDown]
        public void DeleteBuildingGroup()
        {
            // 5. Select the trash can to delete the new Building group; 
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, newTestData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", newTestData.Code) is true)
            {
                BuildingGroupPage.Instance.DeleteBuildingGroup(newTestData);
            }
        }
    }
}
