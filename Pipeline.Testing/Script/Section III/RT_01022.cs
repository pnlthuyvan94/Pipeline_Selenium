using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B02_RT_01022 : BaseTestScript
    {
        bool Flag = true;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        BuildingGroupData oldData;

        [SetUp]
        public void GetTestData()
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
        public void B02_Estimating_AddABuildingGroup()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/BuildingGroups/Default.aspx
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            // File building group and delete it before creating a new one
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, oldData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", oldData.Code) is true)
            {
                // Delete it
                BuildingGroupPage.Instance.DeleteBuildingGroup(oldData);
            }

            // Step 2 - 3 - 4 - 5 click on "+" Add button and populate data
            BuildingGroupPage.Instance.AddNewBuildingGroup(oldData);

            // Verify the modal is displayed with default value ()
            if (!BuildingGroupPage.Instance.AddBuildingGroupModal.IsDefaultValues)
            {
                ExtentReportsHelper.LogWarning("The modal of Add Building Group is not displayed with default values.");
                Flag = false;
            }

            // Step 5. The blocked ability to add multiples of a single value (along with the appropriate toast message to indicate the failure)
            BuildingGroupPage.Instance.ClickAddToShowBuildingGroupModal();
            System.Threading.Thread.Sleep(2000);
            BuildingGroupPage.Instance.AddBuildingGroupModal
                                      .EnterBuildingGroupCode(oldData.Code)
                                      .EnterBuildingGroupName(oldData.Name)
                                      .EnterBuildingGroupDescription(oldData.Description)
                                      //.SelectTheBuildingTrade(TestData["Building Trade"])
                                      .Save();

            string _expectedMessage = $"Not able to create {oldData.Code} {oldData.Name}.";
            string _actualMessage = BuildingGroupPage.Instance.AddBuildingGroupModal.GetLastestToastMessage();
            if (_expectedMessage.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass($"The blocked ability to add multiples of a single value (along with the appropriate toast message to indicate the failure");
                BuildingGroupPage.Instance.CloseToastMessage();
            }
            else 
            {
                ExtentReportsHelper.LogFail($"The message is NOT display as expected.<br>Actual results: <i>{_actualMessage}</i>.<br>Expected Results:{_expectedMessage}");
                BuildingGroupPage.Instance.CloseToastMessage();
                Flag = false;
            }

            // Close modal
            BuildingGroupPage.Instance.AddBuildingGroupModal.CloseModal();

            // Verify the new Building Phase create successfully
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, oldData.Code);
            if(BuildingGroupPage.Instance.IsItemInGrid("Code", oldData.Code) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Building Group {oldData.Code} and {oldData.Name} was not display on grid.</font>");
            }

            if (Flag == false)
            {
                ExtentReportsHelper.LogFail("There are some error while running this test. Please review the details as above.");
            }
        }    
        
        [TearDown]
        public void DeleteBuildingGroup()
        {
            BuildingGroupPage.Instance.DeleteBuildingGroup(oldData);
        }
    }
}
