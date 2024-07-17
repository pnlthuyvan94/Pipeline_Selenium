using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_IV
{
    public class E07_PIPE_34948 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E07";
        private const string NewBuildingGroupCode = "RTE7";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E07";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_E07";
        private const string NewBuildingPhaseCode = "RTE7";

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
        public void E07_Purchasing_AddEditBuildingPhase()
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
            if(BuildingPhasePage.Instance.AddBuildingPhaseModal.IsModalDisplayed())
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.7: Go to Building Phase Detail page.</b></font>");
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName);
            BuildingPhasePage.Instance.ClickItemInGrid("Name", NewBuildingPhaseName);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.8: Update Building Phase details.</b></font>");
            BuildingPhaseDetailPage.Instance.EnterPhaseName(NewBuildingPhaseName + "_updated")
                .EnterAbbName(NewBuildingPhaseName + "_updated")
                .EnterDescription("Updated Description");

            BuildingPhaseDetailPage.Instance.SelectBuildingGroup("", 2);
            BuildingPhaseDetailPage.Instance.SelectType("", 2);
            BuildingPhaseDetailPage.Instance.SelectParent("", 2);
            BuildingPhaseDetailPage.Instance.SelectTaskForPayment("Test", 2);
            BuildingPhaseDetailPage.Instance.SelectTaskForPO("Test", 2);
            BuildingPhaseDetailPage.Instance.SelectReleaseGroup("", 2);
            BuildingPhaseDetailPage.Instance.SelectCostCode("", 2);
            BuildingPhaseDetailPage.Instance.SelectCostCategory("", 2);
            BuildingPhaseDetailPage.Instance.SelectPOView("", 2);
            BuildingPhaseDetailPage.Instance.IsBudgetOnly(true);
            BuildingPhaseDetailPage.Instance.EnterPercentBilled("50");
            BuildingPhaseDetailPage.Instance.ClickTaxableNo();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.9: Save updated Building Phase details.</b></font>");
            BuildingPhaseDetailPage.Instance.Save();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.10: Add Product to Building Phase.</b></font>");
            BuildingPhaseDetailPage.Instance.ClickAddProductToPhaseModal();
            //Use first product in the dropdown
            //BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SelectProduct("QA_RT_Product01_Auto", 1);
            System.Threading.Thread.Sleep(3000);
            BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SelectTaxStatus("", 1);
            System.Threading.Thread.Sleep(3000);
            BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.SetDefault(true);
            BuildingPhaseDetailPage.Instance.AddProductToPhaseModal.Save();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.11: Add Vendor to Building Phase.</b></font>");
            System.Threading.Thread.Sleep(3000);
            BuildingPhaseDetailPage.Instance.ClickAddVendorToPhaseModal();
            BuildingPhaseDetailPage.Instance.AddVendorToPhaseModal.SelectVendor("", 1);
            System.Threading.Thread.Sleep(3000);
            BuildingPhaseDetailPage.Instance.AddVendorToPhaseModal.Save();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.12: Go back to Building Phase Default page.</b></font>");
            BuildingPhaseDetailPage.Instance.BackToPreviousPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.13: Check if Building Phase Name is updated.</b></font>");
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewBuildingPhaseName + "_updated");

            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName + "_updated") is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>Building phase is updated and displayed on the grid.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='green'><b>Building phase is not updated and not displayed on the grid..</b></font>");
        }

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Tear down test data.</b></font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Delete Building Group.</b></font>");
            //DeleteBuildingGroup();
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
                BuildingPhasePage.Instance.DeleteItemInGrid("Code", NewBuildingPhaseCode);
                BuildingPhasePage.Instance.WaitGridLoad();
            }
        }

    }
}
