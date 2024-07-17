using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionSelectionGroup;
using Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A09_A_RT_01224 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private OptionSelectionGroupData oldData;
        private OptionSelectionGroupData newTestData;
        private readonly int numberOfOptionSelection = 2;
        private List<string> OptionSelectionList;

        // Pre-condition
        [SetUp]
        public void GetTestData()
        {
            oldData = new OptionSelectionGroupData()
            {
                Name = "RegressionTest_Opt_Group",
                SortOrder = "-1"
            };

            // Step 1: Navigate Assets > Option Selection Group and open Option Selection Group Detail page
            ExtentReportsHelper.LogInformation(" Step 1: Navigate Assets > Option Selection Group and open Option Selection Group Detail page.");
            OptionSelectionGroupPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelectionGroups);

            // Step 2: Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(" Step 2: Insert name to filter and click filter by Contain value");
            OptionSelectionGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);

            bool isFoundOldItem = OptionSelectionGroupPage.Instance.IsItemInGrid("Name", oldData.Name);
            if (!isFoundOldItem)
            {
                // create data
                // Step 2: click on "+" Add button
                OptionSelectionGroupPage.Instance.ClickAddToOptionSelectionGroupModal();

                Assert.That(OptionSelectionGroupPage.Instance.AddOptionSelectionGroup.IsModalDisplayed(), "Add Option Selection group modal is not displayed.");

                OptionSelectionGroupPage.Instance.AddOptionSelectionGroup
                                          .EnterOptionSelectionGroupName(oldData.Name)
                                          .EnterSortOrder(oldData.SortOrder);

                // Step 4. Select the 'Save' button on the modal;
                OptionSelectionGroupPage.Instance.AddOptionSelectionGroup.Save();
                string _expectedMessage = "Selection Group successfully added.";
                string _actualMessage = OptionSelectionGroupPage.Instance.AddOptionSelectionGroup.GetLastestToastMessage();
                if (_expectedMessage.Equals(_actualMessage))
                {
                    ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                    OptionSelectionGroupPage.Instance.CloseToastMessage();
                }
                else if (!string.IsNullOrEmpty(_actualMessage))
                    ExtentReportsHelper.LogFail($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");

                // Close modal
                //OptionSelectionGroupPage.Instance.AddOptionSelectionGroup.CloseModal();
                OptionSelectionGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, oldData.Name);
            }

            newTestData = new OptionSelectionGroupData()
            {
                Name = "RegressionTest_Opt_Group_Update",
                SortOrder = "-11"
            };

            // Step 3: Select filter item to open detail page
            ExtentReportsHelper.LogInformation(" Step 3: Select filter item to open detail page");
            OptionSelectionGroupPage.Instance.SelectItemInGrid("Name", oldData.Name);
        }


        [Test]
        [Category("Section_IV")]
        [Ignore("The Option Selection Group was removed from Asset menu so this test sript will be ignored.")]
        public void A09_A_Assets_DetailPage_OptionSelectionGroup_OptionSelectionGroupDetails()
        {
          

            // Verify open Option Selection Group detail page display correcly
            if (OptionSelectionGroupDetailPage.Instance.IsTitlOptionSelectionGroup(oldData.Name) is true)
                ExtentReportsHelper.LogPass($"The Option Selection Group {oldData.Name} displays sucessfully with URL: {OptionSelectionGroupDetailPage.Instance.CurrentURL}");
            else
                ExtentReportsHelper.LogFail($"The Option Selection Group {oldData.Name} displays with incorrect sub header.");

            // Update Selection Group detail
            ExtentReportsHelper.LogInformation(" Step 4: Update information of Option Selection Group detail page.");
            UpdateSelectionGroup();

            // Step 5: Add Option Selection to Option Selection Group
            ExtentReportsHelper.LogInformation(" Step 5: Add Option Selection to Option Selection group.");
            AddOptionSelectionToOptionSelectionGroup();
        }

        private void AddOptionSelectionToOptionSelectionGroup()
        {
            OptionSelectionGroupDetailPage.Instance.AddOptionSelectionToGroup();
            OptionSelectionList = OptionSelectionGroupDetailPage.Instance.AddOptionSelectionModal.GetSpecifiedItemInOptionSelectionList(numberOfOptionSelection);

            if (!OptionSelectionGroupDetailPage.Instance.AddOptionSelectionModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Option Selection to Option Selection Group\" modal doesn't display.");
                return;
            }

            // Select 2 first items from the list
            ExtentReportsHelper.LogInformation(" Step 6-7: Click Add Selection Option button and verify in the grid view.");
            OptionSelectionGroupDetailPage.Instance.AddOptionSelectionModal.AddOptionSelectionToGroup(numberOfOptionSelection);
            OptionSelectionGroupDetailPage.Instance.WaitGridLoad();

            var expectedMessage = "Selections were successfully added.";
            var actualMessage = OptionSelectionGroupDetailPage.Instance.GetLastestToastMessage();

            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add Option Selection to Option Selection Group unsuccessfully. Actual messsage: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add Option Selection to Option Selection Group successfully.");
            }
            //OptionSelectionGroupDetailPage.Instance.AddOptionSelectionModal.CloseModal();

            // Verify  new items which is added to the grid view
            ExtentReportsHelper.LogInformation("Filter new Option Selection Group in the grid view.");
            foreach (string item in OptionSelectionList)
            {

                // Insert name to filter and click filter by Contain value
                OptionSelectionGroupDetailPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, item);

                bool isFoundItem = OptionSelectionGroupDetailPage.Instance.IsItemInGrid("Name", item);
                if (!isFoundItem)
                {
                    ExtentReportsHelper.LogFail($"The Option Selection Group\"{item} \" was not displayed on the grid.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"The Option Selection Group \"{item}\" was found on the grid view then delete it.");
                    RemoveSelectionFromSelectionGroup(item);
                }
            }

        }

        private void RemoveSelectionFromSelectionGroup(string selectionName)
        {
            ExtentReportsHelper.LogInformation($" Step 8: Delete Option Selection {selectionName} out ot Selection Group.");

            OptionSelectionGroupDetailPage.Instance.DeleteItemInGrid("Name", selectionName);
            OptionSelectionGroupDetailPage.Instance.WaitGridLoad();

            var expectedMessage = "Selection Group successfully removed.";
            var actualMessage = OptionSelectionGroupDetailPage.Instance.GetLastestToastMessage();

            if (actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Selection Group {selectionName} deleted successfully!");
                OptionSelectionGroupDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (OptionSelectionGroupDetailPage.Instance.IsItemInGrid("Name", selectionName))
                    ExtentReportsHelper.LogFail($"Selection Group {selectionName} can't be deleted!");
                else
                    ExtentReportsHelper.LogPass($"Selection Group {selectionName} deleted successfully!");

            }
        }

        private void UpdateSelectionGroup()
        {
            OptionSelectionGroupDetailPage.Instance.UpdateOptionSelectionGroup(newTestData);

            var expectedMessage = "Option Selection Group successfully updated.";
            var actualMessage = OptionSelectionGroupDetailPage.Instance.GetLastestToastMessage();
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
            }
            else if (expectedMessage == actualMessage)
            {
                ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");
                if (OptionSelectionGroupDetailPage.Instance.IsUpdateDataCorrectly(newTestData))
                    ExtentReportsHelper.LogPass("The updated data displays correctly in the grid view.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
            }
            OptionSelectionGroupDetailPage.Instance.CloseToastMessage();
        }

        private void FilterOptionSelectionGroupOnGrid()
        {
            // Step 4: Verify the filter Selection Group
            OptionSelectionGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, newTestData.Name);
            if (OptionSelectionGroupPage.Instance.IsItemInGrid("Name", newTestData.Name))
            {
                ExtentReportsHelper.LogPass("New Selection Group is filtered successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail("New Selection Group is filtered unsuccessfully");
            }
        }

        [TearDown]
        public void DeleteSelectionGroup()
        {
            // Back to Selection Group page and delete item
            ExtentReportsHelper.LogInformation($" Step 9: Back to Option Selection Group page and delete group {newTestData.Name}.");
            OptionSelectionGroupPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelectionGroups);
            ExtentReportsHelper.LogInformation($"Filter Selection Group {newTestData.Name} in the grid then delete it.");
            FilterOptionSelectionGroupOnGrid();

            // 5. Select the trash can to delete the new Selection Group; 
            OptionSelectionGroupPage.Instance.DeleteItemInGrid("Name", newTestData.Name);
            OptionSelectionGroupPage.Instance.WaitGridLoad();
            string expectedMsg = "Selection Group successfully removed.";
            string actualMsg = OptionSelectionGroupPage.Instance.GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("Selection Group deleted successfully.");
                OptionSelectionGroupPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(actualMsg))
                ExtentReportsHelper.LogFail("Selection Group could not be deleted.");

        }
    }
}
