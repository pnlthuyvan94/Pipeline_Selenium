using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionSelectionGroup;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A08_RT_01079 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        Row TestData;

        [Test]
        [Category("Section_III")]
        [Ignore("The Option Selection Group was hidden from Asset menu, so this test will skip.")]
        public void A08_Assets_AddAOptionSelectionGroup()
        {

            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Selections/Groups.aspx
            OptionSelectionGroupPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelectionGroups);

            // Step 2: click on "+" Add button
            OptionSelectionGroupPage.Instance.ClickAddToOptionSelectionGroupModal();

            Assert.That(OptionSelectionGroupPage.Instance.AddOptionSelectionGroup.IsModalDisplayed(), "Add Option Selection group modal is not displayed.");

            // Step 3: Populate all values

            OptionSelectionGroupPage.Instance.AddOptionSelectionGroup
                                      .EnterOptionSelectionGroupName("RegressionTest_Opt_Group")
                                      .EnterSortOrder("-1");


            // Step 4. Select the 'Save' button on the modal;
            OptionSelectionGroupPage.Instance.AddOptionSelectionGroup.Save();

            // Verify successful save and appropriate success message.
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
            OptionSelectionGroupPage.Instance.AddOptionSelectionGroup.CloseModal();

            // Verify the new OptionSelection create successfully
            string groupNameFromExcel = TestData["Option Selection group Name"];
            OptionSelectionGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, groupNameFromExcel);

            System.Threading.Thread.Sleep(667);

            bool isFound = OptionSelectionGroupPage.Instance.IsItemInGrid("Name", groupNameFromExcel);
            Assert.That(isFound, string.Format("New OptionSelection \"{0} \" was not display on grid.", TestData["Option Selection group Name"]));
        }

        [TearDown]
        public void CleanUp()
        {
            OptionSelectionGroupPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelectionGroups);
            OptionSelectionGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, TestData["Option Selection group Name"]);

            System.Threading.Thread.Sleep(667);

            if (OptionSelectionGroupPage.Instance.IsItemInGrid("Name", TestData["Option Selection group Name"]))
            {
                // 5. Select the trash can to delete the new Selection Group; 
                OptionSelectionGroupPage.Instance.DeleteItemInGrid("Name", TestData["Option Selection group Name"]);
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
}
