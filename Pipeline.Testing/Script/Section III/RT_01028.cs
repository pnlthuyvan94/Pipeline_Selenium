using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Category;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B12_RT_01028 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        CategoryData CategoryData;
        [SetUp]
        public void GetTestData()
        {
            CategoryData = new CategoryData()
            {
                Name= "QA_USE-RT",
                Parent= "NONE"
            };
            ExtentReportsHelper.LogInformation("Delete Data Before Create New Data");
            //navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/Categories/Default.aspx
            CategoryPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Categories);

            CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, CategoryData.Name);
            if(CategoryPage.Instance.IsItemInGrid("Name", CategoryData.Name) is true)
            {
                CategoryPage.Instance.DeleteCategory(CategoryData.Name);
            }
        }
        [Test]
        [Category("Section_III")]
        public void B12_Estimating_AddProductCategory()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/Categories/Default.aspx
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CATEGORIES_URL);

            // Create new Category and verify it
            var isCreateSuccessful = CategoryPage.Instance.CreateNewCategory(CategoryData.Name, CategoryData.Parent);

            if (isCreateSuccessful)
            {
                // Step 6. Insert name to filter and click filter by Contain value
                CategoryPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, CategoryData.Name);

                if(CategoryPage.Instance.IsItemInGrid("Name", CategoryData.Name) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>New Category {CategoryData.Name} was not display on grid.</font>");
                }

                // 7. Select item and click deletete icon
                CategoryPage.Instance.DeleteCategory(CategoryData.Name);
            }

        }

        
    }
}
