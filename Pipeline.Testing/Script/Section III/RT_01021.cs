using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Units;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B11_RT_01021 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        UnitData data;

        [SetUp]
        public void GetData()
        {
            // Step 3: Populate all values

            data = new UnitData()
            {
                Name = "Data using for test automation testing",
                Abbreviation = "QA_Unit",
                Message= "Unit created successfully!"
            };
        }

        [Test]
        [Category("Section_III")]
        public void B11_Estimating_AddUnit()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/Products/Units.aspx
            UnitPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Units);

            // Step 1.1: Filter and delete item before creating a new one
            UnitPage.Instance.DeleteUnit(data);

            // Step 2: click on "+" Add button
            UnitPage.Instance.ClickAddToShowUnitModal();

            if(UnitPage.Instance.AddUnitModal.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Add Unit modal is not displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Add Unit modal is displayed</b></font>");
            }

            // Create new Unit
            UnitPage.Instance.CreateUnitAndVerify(data.Abbreviation, data.Name, data.Message);

            // Verify the new unit create successfully
            //UnitPage.Instance.FilterItemInGrid("Abbreviation", GridFilterOperator.EqualTo, data.Abbreviation);
            //System.Threading.Thread.Sleep(3000);
            //bool isFound = UnitPage.Instance.IsItemInGrid("Abbreviation", data.Abbreviation);
            //Assert.That(isFound, string.Format("New Unit \"{0} \" was not display on grid.", data.Abbreviation));
        }


        [TearDown]
        public void DeleteUnit()
        {
            // Step 1: Navigate to Estimate menu > Units
            ExtentReportsHelper.LogInformation($"Back to Unit default page and delete item {data.Abbreviation}");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_UNITS_URL);
            UnitPage.Instance.DeleteUnit(data);
        }
    }
}
