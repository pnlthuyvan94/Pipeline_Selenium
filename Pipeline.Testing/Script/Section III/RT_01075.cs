using NUnit.Framework;
using NUnit.Framework.Legacy;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionGroup;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A05_RT_01075 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        OptionData _option;
        OptionGroupData optionGroupData;

        [SetUp]
        public void CreateTestData()
        {
            var optionType = new List<bool>()
            {
                false, false, false
            };

            _option = new OptionData()
            {
                Name = "R-QA Only Option Auto",
                Number = "13579",
                SquareFootage = 10000,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "QA_RT_OPTION GROUP_AUTOMATION",
                OptionRoom = string.Empty,
                CostGroup = "All Rooms",
                OptionType = "Design",
                Price = 100.00,
                Types = optionType
            };

            optionGroupData = new OptionGroupData()
            {
                Name = "QA_RT_OPTION GROUP_AUTOMATION",
                SortOrder = "0"
            };

            // Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Options/Default.aspx
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            BasePage.PageLoad();

            //click on "+" Add button
            OptionPage.Instance.ClickAddToOpenCreateOptionModal();
            if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed or the title is incorrect.</font>");
            if (OptionPage.Instance.AddOptionModal.IsOptionGroupInList(_option.OptionGroup) == true)
            {
                ExtentReportsHelper.LogInformation($"The Series Data with Name {_option.OptionGroup} is exited in List");
                OptionPage.Instance.AddOptionModal.CloseModal();
            }

            else
            {
                OptionPage.Instance.AddOptionModal.CloseModal();
                // Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Options/OptionGroups.aspx
                OptionGroupPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionGroups);

                OptionGroupPage.Instance.FilterItemInGrid("Option Group Name", GridFilterOperator.EqualTo, optionGroupData.Name);
                if (OptionGroupPage.Instance.IsItemInGrid("Option Group Name", optionGroupData.Name) is true)
                {

                    ExtentReportsHelper.LogInformation($"The Option Data with Name {optionGroupData.Name} is exited in List");
                }
                else
                {
                    //click on "+" Add button
                    OptionGroupPage.Instance.ClickAddToOptionGroupModal();

                    Assert.That(OptionGroupPage.Instance.AddOptionGroup.IsModalDisplayed(), "Add Option group modal is not displayed.");

                    //Populate all values
                    OptionGroupPage.Instance.AddOptionGroup
                                              .EnterOptionGroupName(optionGroupData.Name)
                                              .EnterSortOrder(optionGroupData.SortOrder);

                    // Save
                    OptionGroupPage.Instance.AddOptionGroup.Save();

                    // Verify successful save and appropriate success message.
                    string _expectedMessage = "New option group added successfully.";
                    string _actualMessage = OptionGroupPage.Instance.AddOptionGroup.GetLastestToastMessage();
                    if (_expectedMessage == _actualMessage)
                        ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                    else
                        ExtentReportsHelper.LogWarning($"The message does not as expected. <br>Actual results: {_actualMessage}<br>Expected results: {_expectedMessage} ");

                    OptionGroupPage.Instance.CloseToastMessage();
                }
            }

        }

        [Test]
        [Category("Section_III")]
        public void A05_Assets_AddAnOption()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Options/Default.aspx
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            BasePage.PageLoad();

            // Step 1.1: Filter the created option number and delete if it's existing
            ExtentReportsHelper.LogInformation(null, $"Filter the created option number '{_option.Name}' and delete if it's existing.");
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, _option.Number);
            if (OptionPage.Instance.IsItemInGrid("Number", _option.Number) is true)
            {
                // Delete before creating a new one
                OptionPage.Instance.DeleteItemInGrid("Number", _option.Number);
            }

            // Create Option 
            OptionPage.Instance.CreateNewOption(_option);

            string _expectedMessage = $"Option Number is duplicated.";
            string actualMsg = OptionPage.Instance.GetLastestToastMessage();
            if (_expectedMessage.Equals(actualMsg))
            {
                ExtentReportsHelper.LogFail($"Could not create Option with name { _option.Name} and Number {_option.Number}.");
                OptionPage.Instance.CloseToastMessage();
                OptionPage.Instance.CloseModal();
                //Assert.Fail($"Could not create Option.");
            }

            // Step 4. Verify new Option in header
            BasePage.PageLoad();
            if (OptionDetailPage.Instance.IsSaveOptionSuccessful(_option.Name) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Create new Option unsuccessfully.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Option is created sucessfully with URL: { OptionDetailPage.Instance.CurrentURL}");
            }

            // Step 5. Verify data saved successfully
            OptionDetailPage.Instance.IsSaveOptionData(_option);


            // Step 5. Create with existing name
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.ClickAddToOpenCreateOptionModal();
            if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Create Option modal is not displayed");
            }

            // Re-create division - Click 'Save' Button
            OptionPage.Instance.AddOptionModal.AddOption(_option);

            string _actualMessage = OptionPage.Instance.GetLastestToastMessage();
            ClassicAssert.AreEqual(_expectedMessage, _actualMessage, $"The message does not as expected. Actual results: {_actualMessage}. Expected results: {_expectedMessage} ");
            ExtentReportsHelper.LogPass("The Option could not create with the existed number. The message is dispalyed as expected: " + _actualMessage);

            OptionPage.Instance.CloseToastMessage();

            // Close the modal
            OptionPage.Instance.CloseModal();

        }

        [TearDown]
        public void CleanUpData()
        {
            // 7. Select item and click deletete icon
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name))
            {
                OptionPage.Instance.DeleteItemInGrid("Name", _option.Name);
                string successfulMess = $"Option {_option.Name} deleted successfully!";
                string actualResults = OptionPage.Instance.GetLastestToastMessage();
                if (successfulMess.Equals(actualResults))
                {
                    ExtentReportsHelper.LogPass("Option deleted successfully!");
                    OptionPage.Instance.CloseToastMessage();
                }
                else if (!string.IsNullOrEmpty(actualResults))
                {
                    ExtentReportsHelper.LogFail("Option could not be deleted!");
                }
                else
                {
                    ExtentReportsHelper.LogFail("Option deletion message was not as expected: " + actualResults);
                }
            }
        }
    }
}
