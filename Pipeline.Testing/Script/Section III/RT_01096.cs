using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.CostCategory;
using Pipeline.Testing.Based;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class E04_RT_01096 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private CostCategoryData categoryData;

        [SetUp]
        public void GetTestData()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Purchasing/CostCodes/CostCategories.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0: navigate to Cost Category page.</b></font>");
            CostCategoryPage.Instance.SelectMenu(MenuItems.PURCHASING).SelectItem(PurchasingMenu.CostCategories);

            categoryData = new CostCategoryData()
            {
                Name = "RT-QA_Cost Category",
                Description = "Cost Category Test",
                CostType = "Labor"
            };

            CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, categoryData.Name);
            if (CostCategoryPage.Instance.IsItemInGrid("Name", categoryData.Name) is true)
            {
                // Delete the data before creating a new one with same name
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Delete the data before creating a new one with name.</b></font>");
                CostCategoryPage.Instance.DeleteCostCategoryByName(categoryData.Name);
            }
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void E04_Purchasing_AddCostCategory()
        {
            // Step 2: Populate all values
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Create a new Category.</b></font>");
            CostCategoryPage.Instance.CreateCostCategory(categoryData);
        }
        #endregion


        [TearDown]
        public void DeleteData()
        {
            // Delete Cost Category
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Delete Category.</b></font>");
            CostCategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, categoryData.Name);
            if (CostCategoryPage.Instance.IsItemInGrid("Name", categoryData.Name) is true)
            {
                // Create a new Cost Category if it doesn't exist
                CostCategoryPage.Instance.DeleteCostCategoryByName(categoryData.Name);
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Cost Category '{categoryData.Name}' to delete.</font>");
        }
    }
}
