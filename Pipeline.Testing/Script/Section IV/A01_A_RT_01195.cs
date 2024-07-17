using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A01_A_RT_01195 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private DivisionData _division;
        private DivisionData _updatedDivision;

        [SetUp]
        public void CreateTestData()
        {
            // Step 1: navigate to this URL: http://beta.bimpipeline.com/Dashboard/Builder/Divisions/Default.aspx
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);

            // Step 2: Filter and select Division
            _division = new DivisionData()
            {
                Name = "R-QA Only Division Auto",
                Address = "3990 IN 38",
                City = "Lafayette",
                State = "IN",
                Zip = "47905",
                Description = "Create a new Division",
            };

            _updatedDivision = new DivisionData()
            {
                Name = "R-QA Only Division Auto_Update",
                Address = "3990 IN 38_Update",
                City = "Lafayette_Update",
                State = "IN_Update",
                Zip = "39903",
                Description = "Create a new Division_Update",
            };

            // Step 2.1: Filter and select Division
            // Delete item before creating a new one
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, _division.Name);
            if (DivisionPage.Instance.IsItemInGrid("Division", _division.Name) is true)
            {
                DivisionPage.Instance.DeleteDivision(_division);
            }

            //Create a new Divisions
            DivisionPage.Instance.CreateDivision(_division);

            // Back to list of Division and verify new item in grid view
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL);

            // Insert name to filter and click filter by Contain value
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, _division.Name);

            bool isFoundOldItem = DivisionPage.Instance.IsItemInGrid("Division", _division.Name);
            Assert.That(isFoundOldItem, string.Format($"New Division \"{_division.Name}\" was not display on grid."));
        }


        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A01_A_Assets_DetailPage_Division_DivisionDetail()
        {
            // Select filter item to open detail page
            DivisionPage.Instance.SelectItemInGrid("Division", _division.Name);

            //Verify the updated Division in header
            if (DivisionDetailPage.Instance.IsSaveDivisionSuccessful(_division.Name) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The updated Division '{_division.Name}' displays unsuccessfully on the Subtitle.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Division is displayed sucessfully with URL: {DivisionDetailPage.Instance.CurrentURL}</b></font>");


            // Step 3: Update division - Click 'Save' Button
            DivisionDetailPage.Instance.UpdateDivision(_updatedDivision);
            string _expectedUpdatedMessage = $"Division {_updatedDivision.Name} saved successfully!";
            string actualMsg = DivisionDetailPage.Instance.GetLastestToastMessage();
            if (_expectedUpdatedMessage.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Update division with name {_updatedDivision.Name} is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Could not update division with name {_updatedDivision.Name}.</font>");
                DivisionDetailPage.Instance.CloseToastMessage();
            }

            // Step 4. Verify new Division in the header
            //Verify the updated Division in header
            if (DivisionDetailPage.Instance.IsSaveDivisionSuccessful(_updatedDivision.Name) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Update Division unsuccessfully." +
                    $"<br>The updated Division '{_division.Name}' displays unsuccessfully on the Subtitle.</br></font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Division is Updated sucessfully with URL: {DivisionDetailPage.Instance.CurrentURL}</b></font>");


            // Step 5. Verify data saved successfully
            if (DivisionDetailPage.Instance.IsSaveDivisionData(_updatedDivision) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Division is updated sucessfully with valid data.</b></font>");


            // Step 6. Update with existing name
            // Re-update division with duplicate name: "CG Visions"
            DivisionDetailPage.Instance.EnterDivisionName("CG Visions").Save();

            string _expectedDuplicateMessage = $"Could not update division with name CG Visions.";
            string _actualMessage = DivisionDetailPage.Instance.GetLastestToastMessage();
            if (_expectedDuplicateMessage.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>The Division could not update with the existed name." +
                    "<br>The message is dispalyed as expected: " + _actualMessage);
                DivisionDetailPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The message does not as expected." +
                    $"<br>Actual results: {_actualMessage}." +
                    $"<br>Expected results: {_expectedDuplicateMessage}</br>/font>");
                DivisionDetailPage.Instance.CloseToastMessage();
            }

            // Step 7. Back to list of Division and verify new item in grid view
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL);

            // Insert name to filter and click filter by Contain value
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, _updatedDivision.Name);

            if (DivisionPage.Instance.IsItemInGrid("Division", _updatedDivision.Name) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Updated Division \"{_updatedDivision.Name} \" was not display on grid</font>");
        }
        #endregion

        [TearDown]
        public void DeleteData()
        {
            // Step 6. Back to Division page and delete it
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Back to Division page and delete it.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL);
            DivisionPage.Instance.DeleteDivision(_updatedDivision);
            DivisionPage.Instance.DeleteDivision(_division);
        }
    }
}
