using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Manufactures.ManufacturerDetail;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B09_RT_01027 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private ManufacturerData oldData;

        [SetUp]
        public void CreateTestData()
        {
            // Populate all values
            oldData = new ManufacturerData()
            {
                Name = "RT_Manufacturer-QA Only-Auto",
                Url = "https://strongtie.com",
                Description = "Regression Test Manufacturer-QA Only"
            };
        }
        [Test]
        [Category("Section_III")]
        public void B09_Estimating_AddAManufacturer()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/Manufacturers/Default.aspx
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            // Step 2 -3: click on "+" Add button anh populate all values
            ManufacturerPage.Instance.CreateNewManufacturer(oldData);


            // 5. Verify new manufacturer in header
            if(ManufacturerDetailPage.Instance.IsSaveManufactureSuccessful(oldData.Name) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Create new manufacturer unsuccessfully.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Create successful manufacturer</b></font>");
            }

            // 6. Back to list of manufaturer and verify new item in grid view
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);

            // Insert name to filter and click filter by Contain value
            ManufacturerPage.Instance.EnterManufaturerNameToFilter("Name", oldData.Name);
            if(ManufacturerPage.Instance.IsItemInGrid("Name", oldData.Name) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Manufacturer {oldData.Name} was not display on grid.</font>");
            }
        }

        [TearDown]
        public void DeleteManufacture()
        {
            // Back to Manufacturer Default page to delete itm
            ExtentReportsHelper.LogInformation($"Back to Manufacturer default page and delete item {oldData.Name}");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);

            // Filter New item
            ExtentReportsHelper.LogInformation($"Filter new item {oldData.Name} in the grid view.");
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", oldData.Name))
            {
                // 7. Select item and click deletete icon
                ManufacturerPage.Instance.DeleteManufacturer(oldData.Name);

            }
        }
    }
}
