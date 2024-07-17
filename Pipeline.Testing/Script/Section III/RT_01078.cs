using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionSelection;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A07_RT_01078 : BaseTestScript
    {
        bool Flag = true;
        Row TestData;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        [Test]
        [Category("Section_III")]
        [Ignore("The Option Selection was hidden from Asset menu, so this test will skip.")]
        public void A07_Assets_AddAOptionSelection()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Selections/Selections.aspx
            OptionSelectionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelections);

            // Step 2: click on "+" Add button
            OptionSelectionPage.Instance.ClickAddToOptionSelectionModal();
            OptionSelectionPage.Instance.AddOptionSelectionModal.CheckCustomize(false);

            if (!OptionSelectionPage.Instance.AddOptionSelectionModal.IsDefaultValues)
            {
                ExtentReportsHelper.LogWarning("The modal of Add OptionSelection has unexpected default fields/values. Modal fields have been added, altered, or removed since last regression.");
            }

            Assert.That(OptionSelectionPage.Instance.AddOptionSelectionModal.IsModalDisplayed(), "Add OptionSelection modal is not displayed.");

            // Step 3: Populate all values
            TestData = ExcelFactory.GetRow(OptionSelectionPage.Instance.TestData_RT01078, 1);

            OptionSelectionPage.Instance.AddOptionSelectionModal
                                      .EnterOptionSelectionName(TestData["Option Selection Name"])
                                      .SelectOptionSelectionGroup(TestData["Option Selection Group"])
                                      .CheckCustomize("TRUE".Equals(TestData["Customizable"]) ? true : false);

            // Step 4. Select the 'Save' button on the modal;
            OptionSelectionPage.Instance.AddOptionSelectionModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = "Option Selection successfully added.";
            string _actualMessage = OptionSelectionPage.Instance.AddOptionSelectionModal.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
                ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
            else
                ExtentReportsHelper.LogWarning($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");

            OptionSelectionPage.Instance.AddOptionSelectionModal.CloseToastMessage();

            // Verify the new OptionSelection create successfully
            OptionSelectionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, TestData["Option Selection Name"]);

            System.Threading.Thread.Sleep(667);

            bool isFound = OptionSelectionPage.Instance.IsItemInGrid("Name", TestData["Option Selection Name"]);
            Assert.That(isFound, string.Format("New Option Selection \"{0} \" was not display on grid.", TestData["Option Selection Name"]));

            Assert.That(Flag, "There are some error while running this test. Please review the details as above.");
        }


        [TearDown]
        public void DeleteSelection()
        {
            OptionSelectionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionSelections);
            OptionSelectionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, TestData["Option Selection Name"]);

            System.Threading.Thread.Sleep(667);

            if (OptionSelectionPage.Instance.IsItemInGrid("Name", TestData["Option Selection Name"]))
            {
                // 5. Select the trash can to delete the new OptionSelection; 
                OptionSelectionPage.Instance.DeleteItemInGrid("Name", TestData["Option Selection Name"]);
                OptionSelectionPage.Instance.WaitGridLoad();
                if ("Selection successfully removed." == OptionSelectionPage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("Selection deleted successfully.");
                    OptionSelectionPage.Instance.CloseToastMessage();
                }
                else
                {
                    if (OptionSelectionPage.Instance.IsItemInGrid("Name", TestData["Option Selection Name"]))
                        ExtentReportsHelper.LogFail("Selection could not be deleted.");
                    else
                        ExtentReportsHelper.LogPass("Selection deleted successfully.");
                }
            }
        }

    }
}
