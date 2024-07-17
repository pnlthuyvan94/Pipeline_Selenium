using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Purchasing.CostCategory;
using Pipeline.Testing.Pages.Purchasing.CostType;
using Pipeline.Testing.Pages.Purchasing.CostType.CostTypeDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class E04_A_PIPE_31199 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CostTypeData newCostTypeDataData;
        private CostTypeData newCostTypeDataData_Update;
        private string[] costCategoryList;
        private CostCategoryData newCategoryData;

        // Pre-condition
        [SetUp]
        public void SetUpData()
        {
            newCostTypeDataData = new CostTypeData()
            {
                Name = "QA_RT_Cost_Type_31199",
                Description = "QA_RT_Cost_Type_31199",
                TaxGroup = "NONE" // Default none
            };

            newCostTypeDataData_Update = new CostTypeData()
            {
                Name = "QA_RT_Cost_Type_31199_Update",
                Description = "QA_RT_Cost_Type_31199_Update",
                TaxGroup = "NONE" // Default none
            };

            newCategoryData = new CostCategoryData()
            {
                Name= "QA_RT_Automation_CostCategory_31199",
                Description= "Data for automation testing. Please don't EDIT/ DELETE it",
                CostType="NONE"
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Add new Cost Type data.</b></font>");
            CostTypePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostTypes);
            CostTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostTypeDataData.Name);
            if (CostTypePage.Instance.IsItemInGrid("Name", newCostTypeDataData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Create a new Cost Type data.</b></font>");
                CostTypePage.Instance.CreateCostType(newCostTypeDataData);
            }

            CostCategoryPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);
            CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newCategoryData.Name);
            if (CostCategoryPage.Instance.IsItemInGrid("Name", newCategoryData.Name) is false)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Create a new Cost Categories data.</b></font>");
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Create a new Cost Categories if it doesn't exist.</b></font>");
                CostCategoryPage.Instance.CreateCostCategory(newCategoryData);
            }
        }


        [Test]
        [Category("Section_IV")]
        public void E04_A_Purchasing_DetailPage_CostTypes_CostyType()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: On the Cost Types data page, click the Cost Type to which you would like to select.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Purchasing > CostTypes page.</b></font>");
            CostTypePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostTypes);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Select a Cost Type.</b></font>");
            CostTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostTypeDataData.Name);
            if (CostTypePage.Instance.IsItemInGrid("Name", newCostTypeDataData.Name) is true)
            {
                CostTypePage.Instance.SelectItemInGrid("Name", newCostTypeDataData.Name);
                System.Threading.Thread.Sleep(2000);
                CommonHelper.CaptureScreen();
                ExtentReportsHelper.LogPass("<font color='green'>Opened successfully the Cost Type page.</font>");
                System.Threading.Thread.Sleep(2000);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: On the Cost Type grid, edit fields of Cost Type and click the 'Save' button.</b></font>");
                CostTypeDetailPage.Instance.UpdateCostType(newCostTypeDataData_Update);

                if (CostTypeDetailPage.Instance.IsItemInGrid("Name", newCategoryData.Name) is false)
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: On the Cost Categories grid, click the '+' button to open the 'Add Cost Categories' modal.</b></font>");
                    CostTypeDetailPage.Instance.ClickAddCostCategory();
                    if (CostTypeDetailPage.Instance.IsAddCostCategoryDisplayed() is true)
                        ExtentReportsHelper.LogPass(null, "<font color = 'green'><b>Add Cost Categories modal displays correctly.<b></font>");
                    else
                        ExtentReportsHelper.LogFail("<font color = 'red'>Add Cost Categories modal doesn't display or the title isn't correct.</font>");

                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Assign Cost Categories to current Cost Type.</b></font>");
                    costCategoryList = new string[] { newCategoryData.Name };
                    CostTypeDetailPage.Instance.SelectCostCategoryByName(costCategoryList);
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5: Filter these assigned categories.</b></font>");
                foreach (var item in costCategoryList)
                {
                    if (CostTypeDetailPage.Instance.IsItemInGrid("Name", item) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Assigned Cost Categoris with name '{item}' to current Cost Type successfully.</b></font>");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color='red'>Can't find Cost Categories with name {item} on the grid view." +
                            $"<br>Failed to assign Cost Category '{item}' to current Cost Type.</font>");
                }

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1: Remove Cost Category from Cost Type.</b></font>");
                CostTypeDetailPage.Instance.RemoveCostCategoryFromCostType("Name", costCategoryList[0]);
            }
        }


        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2: Delete Cost Category.</b></font>");

            // Delete Cost Type
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.2: Delete Cost Type.</b></font>");
            CostTypePage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostTypes);
            CostTypePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCostTypeDataData_Update.Name);
            if (CostTypePage.Instance.IsItemInGrid("Name", newCostTypeDataData_Update.Name) is true)
            {
                CostTypePage.Instance.DeleteCostType(newCostTypeDataData_Update.Name);
            }

            // Delete Cost Category
            CostCategoryPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);
            CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newCategoryData.Name);
            if (CostCategoryPage.Instance.IsItemInGrid("Name", newCategoryData.Name) is true)
            {
                CostCategoryPage.Instance.DeleteCostCategoryByName(newCategoryData.Name);
            }

        }

    }
}
