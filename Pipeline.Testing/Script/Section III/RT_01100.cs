using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Pathway.DesignerView.AddDesignerView;
using Pipeline.Testing.Pages.Pathway.DesignerView;
using Pipeline.Testing.Based;
using System;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class F05_RT_01100 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        #region"Test case"
        [Test]
        [Category("Section_III")]
        [Ignore("Pathway menu was removed from Pipeline, so this test sript will be ignored.")]
        public void F05_Pathway_AddDesignerView()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/eHome/Designer/Views.aspx
            DesignerViewPage.Instance.SelectMenu(MenuItems.PATHWAY).SelectItem(PathWayMenu.DesignerViews);

            // Step 2: click on "+" Add button
            DesignerViewPage.Instance.ClickAddToShowDesignerViewModal();

            System.Threading.Thread.Sleep(2500);

            Assert.That(DesignerViewPage.Instance.AddDesignerViewModal.IsModalDisplayed(), "Add Designer View modal is not displayed.");

            // Step 3: Populate all values to Add modal
            Row TestData = ExcelFactory.GetRow(DesignerViewPage.Instance.AddDesignerViewModal.TestData_RT01100, 1);
            string _expectedCreatedMessage = "View successfully added.";
            CreateDesignView(TestData["View Name"], TestData["View Location"], _expectedCreatedMessage, false);

            try
            {
                DesignerViewPage.Instance.AddDesignerViewModal.CloseModal();
            } catch (Exception ex)
            {
                ExtentReportsHelper.LogWarning("Unable to close modal - is it already closed?");
            }

            System.Threading.Thread.Sleep(5000);

            DesignerViewPage.Instance.ClickAddToShowDesignerViewModal();

            System.Threading.Thread.Sleep(2500);

            // Create with a duplicate name by click Save on Add modal again
            string _expectedDuplicatedMessage = "View could not be added.";
            CreateDesignView(TestData["View Name"], TestData["View Location"], _expectedDuplicatedMessage, false);

            try
            {
                DesignerViewPage.Instance.AddDesignerViewModal.CloseModal();
            }
            catch (Exception ex)
            {
                ExtentReportsHelper.LogWarning("Unable to close modal - is it already closed?");
            }

            // Step 4: Verify the new Designer View create successfully
            DesignerViewPage.Instance.FilterItemInGrid("View Name", GridFilterOperator.EqualTo, TestData["View Name"]);
            bool isFound = DesignerViewPage.Instance.IsItemInGrid("View Name", TestData["View Name"]);
            Assert.That(isFound, string.Format("New Designer View \"{0} \" was not display on grid.", TestData["View Name"]));

            // Step 5: Delete Designer View
            // Select the trash can to delete the new Building Phase; 
            // select OK to confirm; verify successful delete and appropriate success message.
            DesignerViewPage.Instance.DeleteItemInGrid("View Name", TestData["View Name"]);

            string expectedMess = "View successfully removed.";
            if (expectedMess == DesignerViewPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Designer View deleted successfully!");
                DesignerViewPage.Instance.CloseToastMessage();
            }
            else
            {
                if (DesignerViewPage.Instance.IsItemInGrid("View Name", TestData["View Name"]))
                    ExtentReportsHelper.LogFail("Designer View could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Designer View deleted successfully!");
            }
        }


        public void CreateDesignView(string view, string location, string expectedMessage, bool isDuplicate)
        {
            DesignerViewPage.Instance.AddDesignerViewModal.EnterViewName(view).EnterViewLocation(location);
            DesignerViewPage.Instance.AddDesignerViewModal.Save();
            string actualMessage = DesignerViewPage.Instance.GetLastestToastMessage();

            // Verify successful save and appropriate success message.

            if (expectedMessage == actualMessage && !isDuplicate)
            {
                ExtentReportsHelper.LogPass("Create successful Designer View. The mesage is same as expected. Actual results: " + actualMessage);
                DesignerViewPage.Instance.CloseToastMessage();
            }
            else if (expectedMessage == actualMessage && isDuplicate)
            {
                ExtentReportsHelper.LogPass("Can't create duplicate Designer View. The mesage is same as expected. Actual results: " + actualMessage);
                DesignerViewPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"Could not Create Designer View with name {view} and location {location}.");
                Assert.Fail($"Could not create Designer View. The mesage isn't same as expected. The expected: {expectedMessage},  The actual: {actualMessage}");
            }
        }

        #endregion
    }
}
