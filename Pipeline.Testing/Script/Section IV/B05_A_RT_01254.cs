using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Uses;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;
using Pipeline.Testing.Pages.Estimating.Uses.UseDetail;
using Pipeline.Testing.Script.Section_III;

namespace Pipeline.Testing.Script.Section_IV
{
    public class B05_RT_01254 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private UsesData oldData;
        private UsesData newValidTestData;
        private UsesData newInvalidTestData;


        [SetUp]
        public void GetData()
        {
            oldData = new UsesData()
            {
                Name = "QA_USE-RT",
                SortOrder = "1",
                Description = "QA_USE-RT - Do not Modify"
            };

            newValidTestData = new UsesData()
            {
                Name = "QA_USE-RT_Update",
                SortOrder = "10",
                Description = "QA_USE-RT - Do not Modify_Update"
            };

            newInvalidTestData = new UsesData()
            {
                Name = string.Empty,
                SortOrder = "10",
                Description = "QA_USE-RT - Do not Modify_Update"
            };

            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            ExtentReportsHelper.LogInformation($"Filter new item {oldData.Name} in the grid view.");
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);
            if (!UsesPage.Instance.IsItemInGrid("Name", oldData.Name))
            {
                UsesPage.Instance.CreateNewUse(oldData);

                // Step 5. Close the Adding modal
                // UsesPage.Instance.AddUsesModal.CloseModal();

                // Step 6. Insert name to filter and click filter by Contain value
                UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);
            }
        }

        [Test]
        [Category("Section_IV")]
        public void B05_A_Estimating_DetailPage_Uses_UseDetails()
        {
            // Step 1: Navigate to Estimate menu > Use > Open page with URL .../Dashboard/Products/Uses/Default.aspx
            ExtentReportsHelper.LogInformation(" Step 1: Navigate to Estimate menu > Manufacturers > Open page with URL .../Dashboard/Products/Uses/Default.aspx.");

            var _usesURL = BaseDashboardUrl + BaseMenuUrls.VIEW_USES_URL;
            if (UsesPage.Instance.IsPageDisplayed(_usesURL) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Uses page is displayed.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Uses page isn't displayed.</font>");
            }
            // Select new item to open Uses detail page
            ExtentReportsHelper.LogInformation($" Step 2: Select item to open Uses detail page. Ignore all steps related to create new Uses.");
            UsesPage.Instance.SelectItemInGrid("Name", oldData.Name);

            // Verify open Manufacturer detail page display correcly
            if (UseDetailPage.Instance.IsDisplayDataCorrectly(oldData) is true)
                ExtentReportsHelper.LogPass($"The Use detail page of item: {oldData.Name} displays correctly.");
            else
                ExtentReportsHelper.LogFail($"The Use detail page of item:{oldData.Name} displays with incorrect sub header/ title.");

            // Step 3: Update Use detail page with valid data
            ExtentReportsHelper.LogInformation(" Step 3: Update Use detail page.");
            UpdateUse(newValidTestData);
            UpdateUse(newInvalidTestData, false);

        }

        private void UpdateUse(UsesData newTestData, bool isUpdateValidData = true)
        {
            string expectedMessage;

            if (isUpdateValidData)
            {
                ExtentReportsHelper.LogInformation(" Step 3.1: Update Use with valid data.");
                expectedMessage = $"{newValidTestData.Name} successfully saved.";
            }
            else
            {
                ExtentReportsHelper.LogInformation("Step 3.2: Update Use with empty name (invalid use).");
                expectedMessage = "Name can not be empty.";
            }
            UseDetailPage.Instance.UpdateUse(newTestData);


            var actualMessage = UseDetailPage.Instance.GetLastestToastMessage();
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
            }
            else if (expectedMessage == actualMessage)
            {
                if (isUpdateValidData)
                {
                    ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");
                    if (UseDetailPage.Instance.IsDisplayDataCorrectly(newValidTestData))
                        ExtentReportsHelper.LogPass("The updated data displays or reset correctly in the grid view.");
                }
                else
                {
                    ExtentReportsHelper.LogPass("Can't update. The toast message same as expected.");
                    UseDetailPage.Instance.CloseToastMessage();
                }
            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
            }
        }


        [TearDown]
        public void DeleteUse()
        {
            // Back to Use Default page to delete itm
            ExtentReportsHelper.LogInformation($" Step 4: Back to Use default page and delete item {newValidTestData.Name}");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_USES_URL);

            // Filter New item
            ExtentReportsHelper.LogInformation($"Filter new item {newValidTestData.Name} in the grid view.");
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newValidTestData.Name);
            System.Threading.Thread.Sleep(2000);

            if (UsesPage.Instance.IsItemInGrid("Name", newValidTestData.Name))
            {
                UsesPage.Instance.DeleteUses(newValidTestData.Name);
            }

            if (B10_RT_01084.isDuplicated)
            {
                ExtentReportsHelper.LogInformation("Delete the duplicated data.");
                UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, newValidTestData.Name);
                UsesPage.Instance.DeleteItemInGrid("Name", newValidTestData.Name);
            }
        }
    }
}
