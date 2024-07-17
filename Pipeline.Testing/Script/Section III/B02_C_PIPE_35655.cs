using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingGroup.BuildingGroupDetail;

namespace Pipeline.Testing.Script.Section_III
{
    class B02_C_PIPE_35655 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        BuildingGroupData oldData;
        private readonly string BUILDINGPHASEGROUP_NAME_DEFAULT = "R-QA Only Building Group Auto";
        private int numberOfBuildingPhase = 2;
        [SetUp]
        public void GetData()
        {
            oldData = new BuildingGroupData()
            {
                Name = "R-QA Only Building Group Auto",
                Code = "622",
                Description = "Regression QA Only Test Group created by Automation"
            };
        }
        [Test]
        [Category("Section_III")]
        public void B02_C_Estimating_BuildingGroups_Should_not_allow_to_create_edit_BuildingGroup_without_Code()
        {
            //Step 1: Access https://pipeline-dev45.sstsandbox.com/develop/Dashboard/default.aspx using your credentials
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1: Access https://pipeline-dev45.sstsandbox.com/develop/Dashboard/default.aspx using your credentials.</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            // File building group and delete it before creating a new one
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, oldData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", oldData.Code) is true)
            {
                // Delete it
                BuildingGroupPage.Instance.DeleteBuildingGroup(oldData);
            }
            //Step 2: Navigate to Estimating > Building Groups page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2: Navigate to Estimating > Building Groups page..</font>");
            //Step 3: Click the  icon and verify if Add Building Group modal is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3: Click the  icon and verify if Add Building Group modal is displayed.</font>");
            BuildingGroupPage.Instance.ClickAddToShowBuildingGroupModal();
            if(BuildingGroupPage.Instance.AddBuildingGroupModal.IsModalDisplayed() is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Add Building Group modal isn't displayed or title is incorrect." +
                    $"<br>Expected title: 'Add Building Group'</font>");
            else
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Add Building Group modal displayed successfully.</b></font>");

            //Step 4: Input a value only on the “Name” field and click on Save button. Verify if there is an error validation not allowing to save a building group that do not have a code
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4: Input a value only on the “Name” field and click on Save button. Verify if there is an error validation not allowing to save a building group that do not have a code.</font>");
            BuildingGroupPage.Instance.AddBuildingGroupModal
                                      .EnterBuildingGroupName(oldData.Name)
                                      .Save();

            string _expectedMessage = $"Not able to create {oldData.Name}.";
            string _actualMessage = BuildingGroupPage.Instance.AddBuildingGroupModal.GetLastestToastMessage();
            if (_expectedMessage.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass($"The message is displayed as expected.");
                BuildingGroupPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"The message is NOT display as expected.<br>Actual results: <i>{_actualMessage}</i>.<br>Expected Results:{_expectedMessage}");
                BuildingGroupPage.Instance.CloseToastMessage();
            }

            //Step 5: Input a value on “Code” field and click on Save button. Verify if there a success toast message and the new building group is included in the general list of building groups
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5: Input a value on “Code” field and click on Save button. Verify if there a success toast message and the new building group is included in the general list of building groups.</font>");
            BuildingGroupPage.Instance.AddBuildingGroupModal
                             .EnterBuildingGroupName(oldData.Code)
                             .Save();

             _expectedMessage = $"Not able to create {oldData.Code}.";
             _actualMessage = BuildingGroupPage.Instance.AddBuildingGroupModal.GetLastestToastMessage();
            if (_expectedMessage.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass($"The message is displayed as expected.");
                BuildingGroupPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"The message is NOT display as expected.<br>Actual results: <i>{_actualMessage}</i>.<br>Expected Results:{_expectedMessage}");
                BuildingGroupPage.Instance.CloseToastMessage();
            }

            // Close modal
            BuildingGroupPage.Instance.AddBuildingGroupModal.CloseModal();
            //Step 6: Click on that newly created building group and click the icon under Building Phases pane. Verify if Add Building Phases to Building Group modal is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 6: Click on that newly created building group and click the icon under Building Phases pane. Verify if Add Building Phases to Building Group modal is displayed.</font>");
            BuildingGroupPage.Instance.AddNewBuildingGroup(oldData);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, oldData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", oldData.Code) is true)
            {
                
                BuildingGroupPage.Instance.SelectItemInGrid("Code", oldData.Code);
                // Verify open building group detail page display correcly
                if (BuildingGroupDetailPage.Instance.IsTitleBuildingGroup(oldData.Code + "-" + oldData.Name) is true)
                    ExtentReportsHelper.LogPass($"The building group {oldData.Name} displays sucessfully with URL: {BuildingGroupDetailPage.Instance.CurrentURL}");
                else
                    ExtentReportsHelper.LogFail($"The building group {oldData.Name} displays with incorrect sub header.");

            }

            //Step 7: Select 1 or multiple building phases and verify if there is a success toast message that is displayed on the page: Building Phase(s) were successfully added. Verify if all the selected building phases are displayed in Building Phases pane
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 7: Select 1 or multiple building phases and verify if there is a success toast message that is displayed on the page: Building Phase(s) were successfully added. Verify if all the selected building phases are displayed in Building Phases panel.</font>");
            //Add Building Phase to Building Group
            ExtentReportsHelper.LogInformation("Add building phase to building group. ");
            BuildingGroupDetailPage.Instance.AddPhaseToGroup(numberOfBuildingPhase);

        }
    }
}
