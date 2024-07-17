using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Pathway.House;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class F01_RT_01097 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        [Test]
        [Category("Section_III")]
        [Ignore("Pathway menu was removed from Pipeline, so this test sript will be ignored.")]
        public void F01_Pathway_EditAHouse()
        {
            // Go to Pathway House
            HousePage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.Houses);

            Row TestData = ExcelFactory.GetRow(HousePage.Instance.TestData_RT01097, 1);

            PathwayHouseProperty houseProperty = new PathwayHouseProperty()
            {
                FloorPlan = bool.Parse(TestData["Floor Plan"]),
                Exterior = bool.Parse(TestData["Exterior Designer"]),
                Interior = bool.Parse(TestData["Interior Designer"]),
                Media = bool.Parse(TestData["Media Designer"])
            };

            // Filter the House in grid
            HousePage.Instance.EnterHouseNameToFilter("Name ", TestData["House Name"]);

            // edit
            HousePage.Instance.EditHouseProperty("Name ", TestData["House Name"])
                .UpdateHouseValue(TestData["House Name"], houseProperty)
                .UpdateValue();

            if ("Saved" == HousePage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass($"{TestData["House Name"]} updated successfully");
                HousePage.Instance.CloseToastMessage();
            }

            // Verify
            if (HousePage.Instance.IsCorrectHouseProperty(TestData["House Name"], houseProperty))
                ExtentReportsHelper.LogPass($"{TestData["House Name"]} updated successfully");
            else
                Assert.Fail($"{TestData["House Name"]} could not be updated or saved with invalid value.");

            // Reset value
            ExtentReportsHelper.LogInformation("------------------Reset value for next run -----------------");
            // edit
            houseProperty = new PathwayHouseProperty();
            HousePage.Instance.EditHouseProperty("Name ", TestData["House Name"])
                 .UpdateHouseValue(TestData["House Name"], houseProperty)
                 .UpdateValue();

            if ("Saved" == HousePage.Instance.GetLastestToastMessage())
            {
                HousePage.Instance.CloseToastMessage();
            }
        }

    }
}
