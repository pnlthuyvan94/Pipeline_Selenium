using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.StyleImportRules;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B08_RT_01083 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        StyleImportRulesData StyleImportRulesData;
        private string getNewDefaultStyle;
        private string getStyles;

        [SetUp]
        public void GetTestData()
        {
            StyleImportRulesData = new StyleImportRulesData()
            {
                Active = true,
                DefaultStyle = "GENERIC-RegressionTest_Auto_Style",
                Styles = "CONCEPT-THREE-BAR"
            };
        }

        [Test]
        [Category("Section_III")]
        public void B08_Estimating_AddStyleImportRule()
        {
            // Step 1: navigate to this URL:http://dev.bimpipeline.com/Dashboard/BuilderBom/Transfers/Imports/StyleImportRules.aspx
            StyleImportRulesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.StyleImportRules);

            // Change page size before verifing the item on the grid
            CommonHelper.ScrollToEndOfPage();
            StyleImportRulesPage.Instance.ChangePageSize(50);

            // Find all style import rule with name same as with the created one. Delete it
            while (StyleImportRulesPage.Instance.IsItemInGrid("Default", StyleImportRulesData.DefaultStyle) is true)
            {
                StyleImportRulesPage.Instance.DeleteStyleRule(StyleImportRulesData.DefaultStyle, StyleImportRulesData.Styles);
            }

            // Step 2: click on "+" Add button
            CommonHelper.ScrollToBeginOfPage();
            StyleImportRulesPage.Instance.ClickAddToOpenAddRuleModal();
            if (!StyleImportRulesPage.Instance.AddRuleModal.IsModalDisplayed)
            {
                ExtentReportsHelper.LogFail("Add Style Import Rule modal is not displayed.");
            }

            // Step 3: Populate all values
            // Create Style Import Rule - Click 'Save' Button            
            StyleImportRulesPage.Instance.AddRuleModal.IsActive(StyleImportRulesData.Active);
            //Select data in List. IF data is'nt in List then select item By index 
            getNewDefaultStyle = StyleImportRulesPage.Instance.AddRuleModal.SelectDefaultStyle(StyleImportRulesData.Styles);
            getStyles = StyleImportRulesPage.Instance.AddRuleModal.SelectStyles(StyleImportRulesData.Styles);

            string actualmessage = StyleImportRulesPage.Instance.AddRuleModal.Save();
            StyleImportRulesData.DefaultStyle = getNewDefaultStyle;
            StyleImportRulesData.Styles = getStyles;
            if (actualmessage.Equals("Style Import Rules added successfully!"))
            {
                ExtentReportsHelper.LogPass($"Create Style Import Rule with Default Style {StyleImportRulesData.DefaultStyle} and Styles {StyleImportRulesData.Styles}  is added sucessfully.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Create Style Import Rule with Default Style {StyleImportRulesData.DefaultStyle} and Styles {StyleImportRulesData.Styles} is not added sucessfully.");
            }

            //check item in grid
            if (StyleImportRulesPage.Instance.IsItemInGrid("Default", StyleImportRulesData.DefaultStyle) is true)
            {
                ExtentReportsHelper.LogPass($" Style Import Rule with Default Style {StyleImportRulesData.DefaultStyle} and Styles {StyleImportRulesData.Styles} was displayed in grid.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Style Import Rule with Default Style \"{StyleImportRulesData.DefaultStyle}\" and Styles \"{StyleImportRulesData.Styles}\" was not display on grid.");
            }

            //  Select item and click deletete icon
            StyleImportRulesPage.Instance.DeleteStyleRule(StyleImportRulesData.DefaultStyle, StyleImportRulesData.Styles);
        }
    }
}
