using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;

namespace Pipeline.Testing.Script.Section_V
{
    public class A01_RT_01290 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_V);
        }

        [SetUp] // Pre-condition
        public void SetUp()
        {
            //old_TestData = ExcelFactory.GetRow(HouseDetailPage.Instance.TestData_RT01073, 1);
            // Go to default page
            // Step 1: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Communities/Default.aspx
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value -- This will cause an exception when the loading icon is no longer visible (this is expected)
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, "RT-QA_TestBOM");

            // Select filter item to open House detail page
            HousePage.Instance.SelectItemInGridWithTextContains("Name", "RT-QA_TestBOM");

        }

        [Test]
        [Category("Section_V")]
        public void A01_Generate_HouseBOM()
        {
            HouseDetailPage.Instance.LeftMenuNavigation("House BOM");
        }
    }
}
