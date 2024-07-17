using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Purchasing.CostCategory;
using Pipeline.Testing.Pages.Purchasing.CostCategory.CostCategoryDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class E05_A_PIPE_18708 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private string[] buildingPhaseList;

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_E05A";
        private const string NewBuildingGroupCode = "E05A";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_E05A";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_E05A";
        private const string NewBuildingPhaseCode = "E05A";

        private CostCategoryData newCostCategory;
        private const string NewCostCategoryName = "RT_QA_New_CostCategory_E05A";


        

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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new Cost Category.</b></font>");
            newCostCategory = new CostCategoryData()
            {
                Name = NewCostCategoryName,
                Description = NewCostCategoryName,
                CostType = "NONE"
            };
            CostCategoryPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);
            CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newCostCategory.Name);
            if (CostCategoryPage.Instance.IsItemInGrid("Name", newCostCategory.Name) is false)
            {
                CostCategoryPage.Instance.CreateCostCategory(newCostCategory);
            }
        }

        #region"Test case"

        [Test]
        [Category("Section_IV")]
        public void E05_A_Purchasing_DetailPage_CostCategories_CostCategory()
        {
            buildingPhaseList = new string[] { NewBuildingPhaseCode + "-" + NewBuildingPhaseName };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Update Cost Category with new value.</b></font>");
            CostCategoryPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);
            CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newCostCategory.Name);
            if (CostCategoryPage.Instance.IsItemInGrid("Name", newCostCategory.Name) is true)
            {
                CostCategoryPage.Instance.SelectItemInGrid("Name", newCostCategory.Name);
                newCostCategory.Name = newCostCategory.Name + "_updated";
                CostCategoryDetailPage.Instance.UpdateCostCategory(newCostCategory);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Assign Building Phases to current Cost Category.</b></font>");
                CostCategoryDetailPage.Instance.ClickAddBuildingPhase();
                if(CostCategoryDetailPage.Instance.IsAddBuildingPhaseDisplayed() is true)
                    ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Add Building Phases modal displays correctly.<b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color = 'red'>Add Building Phases modal doesn't display or the title isn't correct.</font>");

                CostCategoryDetailPage.Instance.SelectBuildingPhaseByName(buildingPhaseList);
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Filter these assigned Building Phases.</b></font>");
                foreach(var item in buildingPhaseList)
                {
                    if (CostCategoryDetailPage.Instance.IsItemInGrid("Name", item) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assigned Building Phase with name '{item}' to current Cost Category successfully.</b></font>");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color='red'>Can't find Building Phases with name {item} on the grid view." +
                            $"<br>Failed to assign Building Phase '{item}' to current Cost Category.</font>");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Remove Building Phase from Cost Category.</b></font>");
                CostCategoryDetailPage.Instance.RemoveAllBuildingPhase();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Add Cost Category in Building Phase detail page.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.ClickItemInGrid("Code", NewBuildingPhaseCode);
                BuildingPhaseDetailPage.Instance.SelectCostCategory(newCostCategory.Name, 1);
                BuildingPhaseDetailPage.Instance.Save();

                CostCategoryPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);
                CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCategory.Name);
                if (CostCategoryPage.Instance.IsItemInGrid("Name", newCostCategory.Name) is true)
                {
                    CostCategoryPage.Instance.SelectItemInGrid("Name", newCostCategory.Name);
                    if (CostCategoryDetailPage.Instance.IsItemInGrid("Name", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assigned Building Phase with name '{NewBuildingPhaseCode + "-" + NewBuildingPhaseName}' to current Cost Category successfully.</b></font>");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color='red'>Can't find Building Phases with name {NewBuildingPhaseCode + "-" + NewBuildingPhaseName} on the grid view." +
                            $"<br>Failed to assign Building Phase '{NewBuildingPhaseCode + "-" + NewBuildingPhaseName}' to current Cost Category.</font>");
                }
            }
        }
        
        #endregion

        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1: Remove ALL Building Phase from Cost Category.</b></font>");
            CostCategoryPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);
            CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCategory.Name);
            if (CostCategoryPage.Instance.IsItemInGrid("Name", newCostCategory.Name) is true)
            {
                CostCategoryPage.Instance.SelectItemInGrid("Name", newCostCategory.Name);
                CostCategoryDetailPage.Instance.RemoveAllBuildingPhase();
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2: Back to Cost Category default page and delete it.</b></font>");
            CostCategoryPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);
            CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostCategory.Name);
            if (CostCategoryPage.Instance.IsItemInGrid("Name", newCostCategory.Name) is true)
            {
                CostCategoryPage.Instance.DeleteCostCategoryByName(newCostCategory.Name);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.3 Delete building phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", NewBuildingPhaseName);
            }

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
