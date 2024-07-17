using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.OptionGroup;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A06_RT_01076 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private OptionGroupData optionGroupData;

        [SetUp]
        public void GetData()
        {
            optionGroupData = new OptionGroupData()
            {
                Name = "QA_RT_OPTION GROUP AUTO TEST",
                SortOrder = "0"
                //CutoffPhase = "NONE" // Default none
            };

            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Options/OptionGroups.aspx
            OptionGroupPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionGroups);

            OptionGroupPage.Instance.FilterItemInGrid("Option Group Name", GridFilterOperator.EqualTo, optionGroupData.Name);
            while (OptionGroupPage.Instance.IsItemInGrid("Option Group Name", optionGroupData.Name) is true)
            {
                // Delete old data
                OptionGroupPage.Instance.DeleteOptionGroup("Option Group Name", optionGroupData.Name);
            }
        }

        [Test]
        [Category("Section_III")]
        public void A06_Assets_AddAOptionGroup()
        {
            // Step 2: click on "+" Add button
            OptionGroupPage.Instance.ClickAddToOptionGroupModal();

            Assert.That(OptionGroupPage.Instance.AddOptionGroup.IsModalDisplayed(), "Add Option group modal is not displayed.");

            // Step 3: Populate all values
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

            // Verify the new Option create successfully
            OptionGroupPage.Instance.FilterItemInGrid("Option Group Name", GridFilterOperator.EqualTo, optionGroupData.Name);
            bool isFound = OptionGroupPage.Instance.IsItemInGrid("Option Group Name", optionGroupData.Name);
            if (isFound is false)
                ExtentReportsHelper.LogFail($"<font color='red'>New Option '{optionGroupData.Name}' was not display on grid.</font></br>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>Option Group '{optionGroupData.Name}' displayed successfully on the grid view.</b></font></br>");
        }

        [TearDown]
        public void Delete()
        {
            OptionGroupPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.OptionGroups);
            OptionGroupPage.Instance.FilterItemInGrid("Option Group Name", GridFilterOperator.Contains, optionGroupData.Name);
            if (OptionGroupPage.Instance.IsItemInGrid("Option Group Name", optionGroupData.Name))
            {
                OptionGroupPage.Instance.DeleteOptionGroup("Option Group Name", optionGroupData.Name);
            }
        }
    }
}
