using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionRooms;
using Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail;
using Pipeline.Testing.Pages.Estimating.Units;
using Pipeline.Testing.Script.Section_III;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A07_RT_01222 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private OptionRoomData oldData;
        private OptionRoomData newValidTestData;
        private List<string> optionList;
        private readonly int numberOfProduct = 3;
        bool isUpdatedRoomSuccessful = false;


        [SetUp]
        public void GetData()
        {

            oldData = new OptionRoomData()
            {
                Name = "R-QA Only Option Room Auto",
                SortOrder = "1"
            };

            newValidTestData = new OptionRoomData()
            {
                Name = "R-QA Only Option Room Auto_Update",
                SortOrder = "10"
            };

            OptionRoomPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionRooms);
            ExtentReportsHelper.LogInformation($"Filter old Option Room /'{oldData.Name}/' in the grid view.");
            OptionRoomPage.Instance.FilterItemInGrid("Option Room Name", GridFilterOperator.EqualTo, oldData.Name);

            if (!OptionRoomPage.Instance.IsItemInGrid("Option Room Name", oldData.Name))
            {
                // Create new Option Room
                A10_RT_01156.CreateOptionRoom(oldData);
            }
        }

        [Test]
        [Category("Section_IV")]
        [Ignore("The Option Room was removed from Asset menu so this test sript will be ignored.")]
        public void A07_A_Assets_DetailPage_OptionRoom_OptionRoomDetails()
        {
            // Step 1.1: Navigate to Estimate menu > Units
            ExtentReportsHelper.LogInformation(null, "Step 1.1: Navigate to Estimate menu > Units");

            var _optionRoomUrl = "http://dev.bimpipeline.com/Dashboard/Builder/Options/OptionRooms.aspx".ToString().Replace("dev",
                ConfigurationManager.GetValue("ApplicationDomain"));
            Assert.That(UnitPage.Instance.IsPageDisplayed(_optionRoomUrl), "Option Room isn't displayed");

            // Step 1.2: Select item to open Option Room detail page
            ExtentReportsHelper.LogInformation($" Step 1.2: Select {oldData.Name} item to open Option Room detail page.");
            OptionRoomPage.Instance.SelectItemInGrid("Option Room Name", oldData.Name);

            // Verify open Option Room detail page display correcly
            if (OptionRoomDetailPage.Instance.IsDisplayDataCorrectly(oldData) is true)
                ExtentReportsHelper.LogPass($"The Option Room detail page of item: {oldData.Name} displays correctly.");

            // Step 2: Add Option to Option Room
            ExtentReportsHelper.LogInformation(" Step 2: Add Option to Option Room.");
            AddOptionToOptionRoom();

            // Step 3: Update Style detail page with valid data
            ExtentReportsHelper.LogInformation(" Step 3: Update Option Room detail page.");
            UpdateOptionRoom(newValidTestData);

            // Step 4: Verify options added to room
            ExtentReportsHelper.LogInformation(" Step 4 + 5: Verify options added to room and delete it.");
            FilterNewOptions();
        }

        private bool UpdateOptionRoom(OptionRoomData newTestData)
        {
            string expectedMessage = "New Option Room updated successfully.";
            OptionRoomDetailPage.Instance.UpdateOptionRoom(newTestData);

            var actualMessage = OptionRoomDetailPage.Instance.GetLastestToastMessage();
            if (actualMessage == string.Empty)
            {
                ExtentReportsHelper.LogInformation($"Can't get toast message after 10s");
            }
            else if (expectedMessage == actualMessage)
            {
                ExtentReportsHelper.LogPass("Update successfully. The toast message same as expected.");
                isUpdatedRoomSuccessful = true;
                if (OptionRoomDetailPage.Instance.IsDisplayDataCorrectly(newTestData))
                {
                    ExtentReportsHelper.LogPass("The updated data displays or reset correctly in the grid view.");
                }
            }
            else
            {
                ExtentReportsHelper.LogFail($"Toast message must be same as expected. Expected: {expectedMessage}");
            }
            return isUpdatedRoomSuccessful;
        }

        private void AddOptionToOptionRoom()
        {
            OptionRoomDetailPage.Instance.OpenAddOptionToOptionRoomModal();
            optionList = OptionRoomDetailPage.Instance.AddOptionToRoomModal.GetSpecifiedItemInOptionList(numberOfProduct);

            if (!OptionRoomDetailPage.Instance.AddOptionToRoomModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Option to Option Room\" modal doesn't display.");
                return;
            }

            // Select 3 first items from the list
            ExtentReportsHelper.LogInformation("Select Option in the list and click save button.");
            OptionRoomDetailPage.Instance.AddOptionToRoomModal.AddOptionToRoom(optionList);

            var expectedMessage = "Option were successfully added.";
            var actualMessage = OptionRoomDetailPage.Instance.GetLastestToastMessage();

            if (!string.IsNullOrEmpty(actualMessage) && actualMessage != expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Add Option to Option Room unsuccessfully. Actual messsage: {actualMessage}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Add Product to Unit successfully.");
            }
           // OptionRoomDetailPage.Instance.AddOptionToRoomModal.CloseModal();

            
        }

        private void FilterNewOptions()
        {
            // Verify  new items which is added to the grid view
            ExtentReportsHelper.LogInformation("Filter added options in the grid view.");

            bool isFoundItem = false;
            string optionName = string.Empty;
            foreach (string option in optionList)
            {
                if (option.Contains("-"))
                {
                    var itemList = option.Split('-');
                    var index = 0;
                    while (index < itemList.Length)
                    {
                        OptionRoomDetailPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, itemList[index].Trim());
                        isFoundItem = OptionRoomDetailPage.Instance.IsItemInGrid("Name", itemList[index].Trim());

                        if (isFoundItem)
                        {
                            optionName = itemList[index].Trim();
                            break;
                        }
                        index++;
                    }
                }
                else
                {
                    // Insert name to filter and click filter by Contain value
                    OptionRoomDetailPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, option);
                    isFoundItem = OptionRoomDetailPage.Instance.IsItemInGrid("Name", option);
                    optionName = option;
                }

                if (!isFoundItem)
                {
                    ExtentReportsHelper.LogFail($"The Option \"{optionName} \" was not display on the grid.");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"The Option \"{optionName}\" was found on the grid view then delete it.");
                    RemoveOptionFromRoom(optionName);
                }
            }
        }

        private void RemoveOptionFromRoom(string optionName)
        {
            ExtentReportsHelper.LogInformation( $"Delete option {optionName} out of Option Room.");

            OptionRoomDetailPage.Instance.DeleteItemInGrid("Name", optionName);
            OptionRoomDetailPage.Instance.WaitGridLoad();

            var expectedMessage = "Option successfully removed.";
            var actualMessage = OptionRoomDetailPage.Instance.GetLastestToastMessage();

            if (actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Option: /'{optionName}/' deleted successfully!");
                OptionRoomDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (OptionRoomDetailPage.Instance.IsItemInGrid("Name", optionName))
                    ExtentReportsHelper.LogFail($"Option: /'{optionName}/' can't be deleted!");
                else
                    ExtentReportsHelper.LogPass($"Option: /'{optionName}/' deleted successfully!");
            }
        }

        [TearDown]
        public void DeleteOptionRoom()
        {
            // Back to Option Room Page and delete
            OptionRoomPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionRooms);
            string deleteOptionRoomName;
            if (isUpdatedRoomSuccessful)
            {
                deleteOptionRoomName = newValidTestData.Name;
            }
            else
            {
                deleteOptionRoomName = oldData.Name;
            }
            ExtentReportsHelper.LogInformation($"Filter old Option Room /'{deleteOptionRoomName}/' in the grid view.");
            OptionRoomPage.Instance.FilterItemInGrid("Option Room Name", GridFilterOperator.EqualTo, deleteOptionRoomName);
            A10_RT_01156.DeleteOptionRoom(deleteOptionRoomName);
        }
    }
}
