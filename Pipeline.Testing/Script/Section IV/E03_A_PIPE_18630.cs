using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Purchasing.CostCode;
using Pipeline.Testing.Pages.Purchasing.CostCode.CostCodeDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class E03_A_PIPE_18630 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        //private CostCodeData oldData;
        //private CostCodeData newTestData;
        private string[] buildingPhaseList;

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E03A";
        private const string NewBuildingGroupCode = "E03A";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E03A";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_E03A";
        private const string NewBuildingPhaseCode = "E03A";

        private CostCodeData newCostCode;
        private const string NewCostCodeName = "RT_QA_New_CostCode_E03A";

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }

            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhase.Code)
                                          .EnterPhaseName(newBuildingPhase.Name)
                                          .EnterAbbName(newBuildingPhase.AbbName)
                                          .EnterDescription(newBuildingPhase.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase.BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPayment("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectTaskForPO("TEST");
                BuildingPhasePage.Instance.AddBuildingPhaseModal.ClickTaxableYes();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new Cost Code.</b></font>");

            newCostCode = new CostCodeData()
            {
                Name = NewCostCodeName,
                Description = NewCostCodeName
            };
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);
            CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCode.Name);
            if (CostCodePage.Instance.IsItemInGrid("Name", newCostCode.Name) is false)
            {
                CostCodePage.Instance.AddCostCodes(newCostCode, "Cost Code " + newCostCode.Name + " created successfully!", false);
            }
        }


        [Test]
        [Category("Section_IV")]
        public void E03_A_Purchasing_DetailPage_CostCodes_CostCode()
        {
            buildingPhaseList = new string[] { NewBuildingPhaseCode + "-" + NewBuildingPhaseName };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Update Cost Code with new value.</b></font>");
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);
            CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCode.Name);
            if (CostCodePage.Instance.IsItemInGrid("Name", newCostCode.Name) is true)
            {
                CostCodePage.Instance.SelectItemInGrid("Name", newCostCode.Name);
                newCostCode.Name = NewCostCodeName + "_updated";
                CostCodeDetailPage.Instance.UpdateCostCode(newCostCode);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Assign Building Phases to current Cost Code.</b></font>");
                CostCodeDetailPage.Instance.ClickAddBuildingPhase();
                if (CostCodeDetailPage.Instance.IsAddBuildingPhaseDisplayed() is true)
                    ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Add Building Phases modal displays correctly.<b></font>");
                else
                    ExtentReportsHelper.LogFail("<font color = 'red'>Add Building Phases modal doesn't display or the title isn't correct.</font>");

                CostCodeDetailPage.Instance.SelectBuildingPhaseByName(buildingPhaseList);


                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Filter these assigned Building Phases.</b></font>");
                foreach (var item in buildingPhaseList)
                {
                    if (CostCodeDetailPage.Instance.IsItemInGrid("Name", item) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assigned Building Phase with name '{item}' to current Cost Code successfully.</b></font>");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color='red'>Can't find Building Phases with name {item} on the grid view." +
                            $"<br>Failed to assign Building Phase '{item}' to current Cost Code.</font>");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Remove Building Phase from Cost Code.</b></font>");
                CostCodeDetailPage.Instance.RemoveAllBuildingPhaseFromCostCode();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Add Cost Code in Building Phase detail page.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.ClickItemInGrid("Code", NewBuildingPhaseCode);
                BuildingPhaseDetailPage.Instance.SelectCostCode(newCostCode.Name, 1);
                BuildingPhaseDetailPage.Instance.Save();

                CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);
                CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCode.Name);
                if (CostCodePage.Instance.IsItemInGrid("Name", newCostCode.Name) is true)
                {
                    CostCodePage.Instance.SelectItemInGrid("Name", newCostCode.Name);
                    if (CostCodeDetailPage.Instance.IsItemInGrid("Name", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assigned Building Phase with name '{NewBuildingPhaseCode + "-" + NewBuildingPhaseName}' to current Cost Code successfully.</b></font>");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color='red'>Can't find Building Phases with name {NewBuildingPhaseCode + "-" + NewBuildingPhaseName} on the grid view." +
                            $"<br>Failed to assign Building Phase '{NewBuildingPhaseCode + "-" + NewBuildingPhaseName}' to current Cost Code.</font>");
                }

            }
        }


        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Remove ALL Building Phase from Cost Code.</b></font>");
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);
            CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCode.Name);
            if (CostCodePage.Instance.IsItemInGrid("Name", newCostCode.Name) is true)
            {
                CostCodePage.Instance.SelectItemInGrid("Name", newCostCode.Name);
                CostCodeDetailPage.Instance.RemoveAllBuildingPhaseFromCostCode();
            }
            

            // Delete Cost Code
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Back to Cost Type default page and delete it.</b></font>");
            CostCodePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCodes);

            // Delete the updated data
            CostCodePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCode.Name);
            if (CostCodePage.Instance.IsItemInGrid("Name", newCostCode.Name) is true)
            {
                // Create a new Cost Code if it doesn't exist
                CostCodePage.Instance.DeleteCostCodesByName(newCostCode.Name);
            }

            //delete bp
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3 Delete building phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
            }
            //delete bg
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.4 Delete building group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingGroupName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", NewBuildingGroupName) is true)
            {
                BuildingGroupPage.Instance.DeleteBuildingGroup(newBuildingGroup);
            }
        }

    }
}
