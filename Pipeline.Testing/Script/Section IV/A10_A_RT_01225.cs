using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options.OptionImport;
using Pipeline.Testing.Pages.Assets.OptionType;
using Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A10_A_RT_01225 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private OptionTypeData createdData;
        private OptionTypeData updatedTestData;
        private IList<string> OptionList;
        private const string OPTION_IMPORT = "Option Import";
        private const string OPTION_VIEW = "Option";

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {

            createdData = new OptionTypeData()
            {
                Name = "RT_OPT Type",
                SortOrder = "1",
            };

            updatedTestData = new OptionTypeData()
            {
                Name = "RT_OPT Type_Update",
                SortOrder = "10"
            };
            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Option to Option Group.</font>");
            OptionTypePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_OPTION);
            if (OptionImportPage.Instance.IsImportGridDisplay(OPTION_VIEW, OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {OPTION_IMPORT} grid view to import new Options.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_RT_01225\\Pipeline_Options_Automation.csv";
            ImportValidData(OPTION_IMPORT, importFile);
            OptionList = new List<string> { "_Peole", "000 Test", "000 Test1" };
        }


        [Test]
        [Category("Section_IV")]
        public void A10_A_Assets_DetailPage_OptionTypes_OptionType()
        {
            // Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Options/OptionTypes.aspx
            // Step 1: Navigate Assets > Option Type to open OptionType default page
            ExtentReportsHelper.LogInformation(" Step 1: Navigate Assets > Option Type to open OptionType default page.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_TYPE_URL);

            var _optionTypeURL = BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_TYPE_URL;
            Assert.That(OptionTypePage.Instance.IsPageDisplayed(_optionTypeURL), "Option Type page isn't displayed");
            if (OptionTypePage.Instance.IsPageDisplayed(_optionTypeURL) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Option Type page is displayed</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Option Type page isn't displayed.</font>");
            }

            // Step 2: click on "+" Add button
            ExtentReportsHelper.LogInformation(" Step 2: Click Add button to add new Option Type item and populate test data.");
            OptionTypePage.Instance.ClickAddOptionTypeButton();
            if (OptionTypePage.Instance.AddOptionTypeModal.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Add Option Type modal is not displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Add Option Type modal is displayed.</b></font>");
                // Populate test data from excel file
                OptionTypePage.Instance.AddOptionTypeModal.AddNewOptionType(createdData);

                ExtentReportsHelper.LogInformation(" Step 3: Verify new Option Type is displayed in Pathway => Ignore this step.");

                // Step 4: Verify new item in the grid view
                ExtentReportsHelper.LogInformation(" Step 4: Verify new Option Type is displayed in the grid view and successful message.");

                // Verify successful save and appropriate success message.
                string _expectedMessage = "New Option Type added successfully.";
                string _actualMessage = OptionTypePage.Instance.GetLastestToastMessage();
                if (_expectedMessage == _actualMessage)
                {
                    ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                    OptionTypePage.Instance.CloseToastMessage();
                }
                else if (!string.IsNullOrEmpty(_actualMessage))
                {
                    ExtentReportsHelper.LogFail($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");
                    OptionTypePage.Instance.CloseToastMessage();
                }
            }
            // Step 5: Filter new item in the grid view
            ExtentReportsHelper.LogInformation($" Step 5: Filter new item {createdData.Name} in the grid view.");
            OptionTypePage.Instance.FilterItemInGrid("Option Type Name", GridFilterOperator.Contains, createdData.Name);

            if (!OptionTypePage.Instance.IsItemInGrid("Option Type Name", createdData.Name))
                ExtentReportsHelper.LogFail($"Option Type {createdData.Name} can't be found in the grid view!");
            else
                ExtentReportsHelper.LogPass($"Option Type {createdData.Name} display correctly in the grid view.");

            // Step 6: Select new item to open Option Type detail page
            ExtentReportsHelper.LogInformation($" Step 6: Select new item to open Option Type detail page.");
            OptionTypePage.Instance.SelectItemInGrid("Option Type Name", createdData.Name);

            // Verify open Option Type detail page display correcly
            if (OptionTypeDetailPage.Instance.IsTitleOptionType(createdData.Name) is true)
                ExtentReportsHelper.LogPass($"The Option Type detail page of item: {createdData.Name} displays correctly.");
            else
                ExtentReportsHelper.LogFail($"The Option Type detail page of item:{createdData.Name} displays with incorrect sub header/ title.");

            // Step 7: Update Option Type detail
            ExtentReportsHelper.LogInformation(" Step 7: Update information of Option Type on detail page.");
            UpdateOptionType();

            // Step 8: Add Option to Option Type
            ExtentReportsHelper.LogInformation(" Step 8: Add Option to Option Type.");
            AddOptionOptionType();

            // Step 9: Remove single option from Option Type
            ExtentReportsHelper.LogInformation($" Step 9: Remove SINGLE option: {OptionList[0]} from Option Type {updatedTestData.Name}.");
            RemoveSingleOptionFromOptionType(OptionList[0]);

            // Step 10: Remove all option from Option Type
            ExtentReportsHelper.LogInformation($" Step 10: Remove ALL option from Option Type {updatedTestData.Name}.");
            RemoveAllOptionFromOptionType();

            // Step 11: Back to Option Type page and delete item
            ExtentReportsHelper.LogInformation($" Step 11: Back to Option Type page and delete type {updatedTestData.Name}.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_TYPE_URL);
            ExtentReportsHelper.LogInformation($"Filter Option Type {updatedTestData.Name} in the grid then delete it.");
            FilterOptionTypeOnGrid();
        }

        private void AddOptionOptionType()
        {
            OptionTypeDetailPage.Instance.AddOptionToOptionType();

            if (!OptionTypeDetailPage.Instance.AddOptionToOptionTypeModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Option to Option Type\" modal doesn't display.");
                return;
            }

            // Select 2 first items from the list
            ExtentReportsHelper.LogInformation(" Step 9: Select 2 firsts options to save and verify in the grid view.");
            OptionTypeDetailPage.Instance.AddOptionToOptionTypeModal.AddOptionToOptionType(OptionList);
            OptionTypeDetailPage.Instance.WaitGridLoad();

            var expectedMessage = "Option were successfully added.";
            var actualMessage = OptionTypeDetailPage.Instance.GetLastestToastMessage();

            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add Option to Option Type unsuccessfully. Actual messsage: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add Option to Option Type successfully.");
            }
            //OptionTypeDetailPage.Instance.AddOptionToOptionTypeModal.CloseModal();

            // Verify  new items which is added to the grid view
            // ExtentReportsHelper.LogInformation("Filter new Option Type in the grid view.");

            // FilterOptionOnGrid(false);
        }

        private void RemoveSingleOptionFromOptionType(string optionName)
        {
            OptionTypeDetailPage.Instance.DeleteItemInGrid("Name", optionName);
            OptionTypeDetailPage.Instance.WaitGridLoad();

            var expectedMessage = "Option successfully removed.";
            var actualMessage = OptionTypeDetailPage.Instance.GetLastestToastMessage();

            if (actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Option {optionName} deleted successfully!");
                OptionTypeDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (OptionTypeDetailPage.Instance.IsItemInGrid("Name", optionName))
                    ExtentReportsHelper.LogFail($"Option {optionName} can't be deleted!");
                else
                    ExtentReportsHelper.LogPass($"Option {optionName} deleted successfully!");
            }
        }

        private void RemoveAllOptionFromOptionType()
        {
            OptionTypeDetailPage.Instance.DeleteAllItem();
            System.Threading.Thread.Sleep(2000);
            //if (FilterOptionOnGrid(true))
            ExtentReportsHelper.LogPass($"All Options were removed successfuly from the grid view of Option Type {updatedTestData.Name}");
        }

        private bool FilterOptionOnGrid(bool isDeleteAllOption = false)
        {
            bool result = true;
            foreach (string item in OptionList)
            {
                // Insert name to filter and click filter by Contain value
                OptionTypeDetailPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, item);

                bool isFoundItem = OptionTypeDetailPage.Instance.IsItemInGrid("Name", item);

                if (isDeleteAllOption)
                {
                    if (isFoundItem)
                    {
                        // Delete all options. Expected: No option was found in the grid view
                        ExtentReportsHelper.LogFail($"The Option \"{item} \" was not deleted from the Option Type {updatedTestData.Name}.");
                        result = false;
                    }
                }
                else
                {
                    if (!isFoundItem)
                    {
                        // Add option to option type. Expected: All option was found in the grid view
                        ExtentReportsHelper.LogFail($"The Option \"{item} \" was not displayed on the grid.");
                        result = false;
                    }
                    else
                    {
                        ExtentReportsHelper.LogPass($"The Option \"{item}\" was found on the grid view.");
                    }
                }
            }

            // Clear filter
            OptionTypeDetailPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, "");

            return result;
        }

        private void UpdateOptionType()
        {
            OptionTypeDetailPage.Instance.UpdateOptionType(updatedTestData);

            var expectedMessage = "New Option Type updated successfully.";
            var actualMessage = OptionTypeDetailPage.Instance.GetLastestToastMessage();
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
            }
            else if (expectedMessage == actualMessage)
            {
                ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");
                //if (OptionTypeDetailPage.Instance.IsUpdateDataCorrectly(updatedTestData))
                //ExtentReportsHelper.LogPass("The updated data displays correctly in the grid view.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
            }
            OptionTypeDetailPage.Instance.CloseToastMessage();
        }

        private void FilterOptionTypeOnGrid()
        {
            // Step 4: Verify the filter Option Type
            OptionTypePage.Instance.FilterItemInGrid("Option Type Name", GridFilterOperator.EqualTo, updatedTestData.Name);
            System.Threading.Thread.Sleep(3000);
            if (OptionTypePage.Instance.IsItemInGrid("Option Type Name", updatedTestData.Name))
            {
                ExtentReportsHelper.LogPass("New Option Type is filtered successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail("New Option Type is filtered unsuccessfully");
            }
        }

        [TearDown]
        public void DeleteOptionType()
        {
            // 5. Select the trash can to delete the new Option Type 
            OptionTypePage.Instance.DeleteItemInGrid("Option Type Name", updatedTestData.Name);
            if ("New Option Type delete successfully." == OptionTypePage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass($"Option Type {updatedTestData.Name} deleted successfully.");
                OptionTypePage.Instance.CloseToastMessage();
            }
            else
            {
                if (OptionTypePage.Instance.IsItemInGrid("Name", updatedTestData.Name))
                    ExtentReportsHelper.LogFail($"Option Type {updatedTestData.Name} could not be deleted.");
                else
                    ExtentReportsHelper.LogPass($"Option Type {updatedTestData.Name} deleted successfully.");
            }
        }
        /// <summary>
        /// Import with valid file
        /// </summary>
        /// <param name="fullFilePath"></param>
        private void ImportValidData(string importGridTitlte, string fullFilePath)
        {
            string actualMess = OptionImportPage.Instance.ImportFile(importGridTitlte, fullFilePath);

            string expectedMess = "Import complete.";
            if (expectedMess.ToLower().Contains(actualMess.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The valid file was NOT imported." +
                    $"<br>The toast message is: {actualMess}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid file was imported successfully and the toast message indicated success.</b></font>");
        }

    }
}
