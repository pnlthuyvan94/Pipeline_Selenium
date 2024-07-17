using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionRooms;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A10_RT_01156 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private OptionRoomData optionRoom;

        [SetUp]
        public void GetData()
        {
            optionRoom = new OptionRoomData()
            {
                Name = "R-QA Only Option Room Auto",
                SortOrder = "1",
            };
        }

        [Test]
        [Category("Section_III")]
        [Ignore("The Option Room was hidden from Asset menu, so this test will skip.")]
        public void A10_Assets_AddAnOptionRoom()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Options/OptionRooms.aspx
            OptionRoomPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionRooms);

            // Step 2 - 3: click on "+" Add button to add new Option Room
            CreateOptionRoom(optionRoom);

            // Step 4: click on "+" Add button again
            OptionRoomPage.Instance.ClickAddToOpenCreateOptionRoomModal();

            // Step 5. Create with existing name
            OptionRoomPage.Instance.AddOptionRoomModal.AddOptionRoom(optionRoom);
            var _expectedMessage = $"Option Room was not added.";
            var _actualMessage = OptionRoomPage.Instance.GetLastestToastMessage();
            bool isDuplicated = false;
            if (_actualMessage != _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Created duplicated Option Room with name { optionRoom.Name} and Sort Order {optionRoom.SortOrder}.");
                isDuplicated = true;
            }
            ExtentReportsHelper.LogPass("The Option could not create with the existed number. The message is dispalyed as expected: " + _actualMessage);
            OptionRoomPage.Instance.CloseToastMessage();

            DeleteOptionRoom(optionRoom.Name);

            if (isDuplicated)
            {
                ExtentReportsHelper.LogInformation("Delete the duplicated data.");
                OptionRoomPage.Instance.FilterItemInGrid("Option Room Name", GridFilterOperator.Contains, optionRoom.Name);
                OptionRoomPage.Instance.DeleteItemInGrid("Option Room Name", optionRoom.Name);
            }
        }

        public static void CreateOptionRoom(OptionRoomData optionRoom)
        {
            // Step 2: click on "+" Add button
            ExtentReportsHelper.LogInformation($"Create new Option Room with name /'{optionRoom.Name}/'.");
            OptionRoomPage.Instance.ClickAddToOpenCreateOptionRoomModal();
            Assert.That(OptionRoomPage.Instance.AddOptionRoomModal.IsModalDisplayed(), "Create Option Room modal is not displayed.");

            // Step 3: Create Option Room - Click 'Save' Button
            OptionRoomPage.Instance.AddOptionRoomModal.AddOptionRoom(optionRoom);
            string _actualMessage = OptionRoomPage.Instance.GetLastestToastMessage();
            string _expectedMessage = $"New Option Room added successfully.";
            if (_actualMessage == _expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Create Option Room with name { optionRoom.Name} and Sort Order {optionRoom.SortOrder} successfully.");
                OptionRoomPage.Instance.CloseToastMessage();
            }
            else
            {
                // Insert name to filter and click filter by Contain value
                OptionRoomPage.Instance.FilterItemInGrid("Option Room Name", GridFilterOperator.Contains, optionRoom.Name);

                bool isFound = OptionRoomPage.Instance.IsItemInGrid("Option Room Name", optionRoom.Name);
                Assert.That(isFound, string.Format($"Could not create Option Room with name { optionRoom.Name} and Sort Order {optionRoom.SortOrder}."));
            }
        }

        public static void DeleteOptionRoom(string optionName)
        {
            // 7. Select item and click deletete icon
            OptionRoomPage.Instance.DeleteItemInGrid("Option Room Name", optionName);
            OptionRoomPage.Instance.WaitGridLoad();
            string successfulMess = $"New Option Room deleted successfully.";

            if (successfulMess == OptionRoomPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Option Room deleted successfully!");
                OptionRoomPage.Instance.CloseToastMessage();
            }
            else
            {
                if (OptionRoomPage.Instance.IsItemInGrid("Option Room Name", optionName))
                    ExtentReportsHelper.LogFail("Option Room could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Option Room deleted successfully!");
            }
        }
    }
}
