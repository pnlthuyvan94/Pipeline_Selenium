using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;


namespace Pipeline.Testing.Script.Section_III
{
    public class E08_A_PIPE_32999 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E08A";
        private const string NewBuildingGroupCode = "E08A";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E08A";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_E08A";
        private const string NewBuildingPhaseCode = "E08A";

        [SetUp]
        public void Setup()
        {
            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };

            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }
        }

        [Test]
        public void E08_A_Purchasing_Add_Building_Phase()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Check if Building Phase already exists.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Click Add New Building Phase Modal.</b></font>");
            BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
            if (BuildingPhasePage.Instance.AddBuildingPhaseModal.IsModalDisplayed())
                ExtentReportsHelper.LogPass($"<font color='green'><b>Add Building Phase modal is displayed.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='green'><b>Add Building Phase modal is not displayed.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Enter New Building Phase Modal details.</b></font>");
            BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhase.Code)
                                          .EnterPhaseName(newBuildingPhase.Name)
                                          .EnterAbbName(newBuildingPhase.AbbName)
                                          .EnterDescription(newBuildingPhase.Description);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase.BuildingGroup);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
            BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableYes();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Click Save button.</b></font>");
            BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();

            string _expectedMessage = "Building Phase " + NewBuildingPhaseCode + " " + NewBuildingPhaseName + " added successfully!";
            string _actualMessage = BuildingPhasePage.Instance.AddBuildingPhaseModal.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is displayed as expected. Actual results: " + _actualMessage);
            }
            else
            {
                if (BuildingPhasePage.Instance.IsItemInGrid("Code", NewBuildingPhaseCode) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"Building Phase " + NewBuildingPhaseCode + " " + NewBuildingPhaseName + "is created successfully");
                }
                else
                {
                    ExtentReportsHelper.LogWarning($"<font color = 'red'>Failed to create Building Phase {NewBuildingPhaseCode} {NewBuildingPhaseName}</font>");
                }
            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Close modal.</b></font>");
            BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.6: Check if new building phase is displayed on the grid.</b></font>");
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", NewBuildingPhaseCode) is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>New building phase is displayed on the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='green'><b>New building phase is not displayed on the grid.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Verify if new Building Phase is also visible from in Estimating > Building Phase page.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>New building phase is displayed on the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='green'><b>New building phase is not displayed on the grid.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Go back to Purchasing > Building Phase page and delete the new BP.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
                CommonHelper.RefreshPage();
                if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is false)
                    ExtentReportsHelper.LogPass($"<font color='green'><b>New building phase is deleted in Purchasing > Building Phase page..</b></font>");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Verify if new Building Phase is also deleted from in Estimating > Building Phase page.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is false)
                ExtentReportsHelper.LogPass($"<font color='green'><b>New building phase is deleted in Estimating > Building Phase page..</b></font>");
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Tear down test data.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Delete Building Group.</b></font>");
            DeleteBuildingGroup();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2 Delete Building Phase.</b></font>");
            DeleteBuildingPhase();
        }

        private void DeleteBuildingGroup()
        {
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is true)
            {
                BuildingGroupPage.Instance.DeleteItemInGrid("Name", NewBuildingGroupName);
                BuildingGroupPage.Instance.WaitGridLoad();
            }
        }
        private void DeleteBuildingPhase()
        {
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, NewBuildingPhaseCode);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", NewBuildingPhaseCode) is true)
            {
                BuildingPhasePage.Instance.ClickItemInGrid("Code", NewBuildingPhaseCode);
                BuildingPhasePage.Instance.DeleteItemInGrid("Code", NewBuildingPhaseCode);
                BuildingPhasePage.Instance.WaitGridLoad();
            }
        }
    }
}
