using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Worksheet;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B15_RT_01142 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        WorksheetData WorksheetData;
        [SetUp]
        public void GetTestData()
        {
            WorksheetData = new WorksheetData()
            {
                Name = "QA_RT_Worksheet_Automation",
                Code = "159"
            };
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void B15_Estimating_AddAWorksheet()
        {
            // Step 1: navigate to this URL:  http://dev.bimpipeline.com/Dashboard/BuilderBom/Worksheets/Default.aspx
            WorksheetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);

            // Verify the created item and delete if it's existing
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", WorksheetData.Name);
            if (WorksheetPage.Instance.IsItemInGrid("Name", WorksheetData.Name) is true)
            {
                WorksheetPage.Instance.DeleteWorkSheet(WorksheetData.Name);
            }

            // Step 2: click on "+" Add button
            WorksheetPage.Instance.ClickAddWorksheetIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_WORKSHEET_URL;
            Assert.That(WorksheetDetailPage.Instance.IsPageDisplayed(expectedURL), "Worksheet detail page isn't displayed");

            // Step 3: Populate all values
            WorksheetDetailPage.Instance.EnterWorksheetName(WorksheetData.Name)
                .EnterWorksheetCode(WorksheetData.Code);

            // 4. Select the 'Save' button on the modal;
            WorksheetDetailPage.Instance.Save();

            // 5. Verify new Worksheet in header
            if (WorksheetDetailPage.Instance.IsSaveWorksheetSuccessful(WorksheetData.Name) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Create new Worksheet unsuccessfully.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Create successful Worksheet</b></font>");
            }

            // 6. Back to list of Worksheet and verify new item in grid view
            WorksheetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);

            // Insert name to filter and click filter by Contain value
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", WorksheetData.Name);
            if(WorksheetPage.Instance.IsItemInGrid("Name", WorksheetData.Name)is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>New Worksheet {WorksheetData.Name} was not display on grid.</font>");
            }
        }

        #endregion
        [TearDown]
        public void DeleteWorkSheet()
        {
            // 7. Select item and click deletete icon
            WorksheetPage.Instance.DeleteWorkSheet(WorksheetData.Name);
        }
    }
}
