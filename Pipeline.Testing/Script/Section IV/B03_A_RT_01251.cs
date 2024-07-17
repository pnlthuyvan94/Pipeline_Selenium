using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.StyleImportRules;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Styles.DetailStyles;

namespace Pipeline.Testing.Script.Section_IV
{
    public class B03_A_RT_01251 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        StyleImportRulesData StyleImportRulesData;
        StyleImportRulesData getStyleImportRulesData;

        // Pre-condition
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

        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void B03_A_Estimating_DetailPage_Styles_StyleImportRule()
        {
            // Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/Styles/Default.aspx
            // Step 1: Navigate Estimating > Styles and open Styles Detail page
            ExtentReportsHelper.LogInformation(" Step 1: Navigate Estimating > Styles and open Styles Detail page.");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);

            // Select filter item to open detail page
            string selectedStyle = StylePage.Instance.SelectItemInGridByOrder("Name", 1);

            if (string.IsNullOrEmpty(selectedStyle))
            {
                return;
            }

            // Verify open Stype detail page display correcly
            if (StyleDetailPage.Instance.IsSubHeaderCorrect(selectedStyle) is true)
                ExtentReportsHelper.LogPass($"the style detail page of item: {selectedStyle} displays correctly.");
            else
                ExtentReportsHelper.LogFail($"the style detail page of item:{selectedStyle} displays with incorrect sub header/ title.");

            // Step 2: Click Style Import Rules from left navigation
            ExtentReportsHelper.LogInformation(" Step 2: Click Style Import Rules from left navigation.");
            StyleDetailPage.Instance.LeftMenuNavigation("Style Import Rules");

            if (StyleImportRulesPage.Instance.DisplayCorrectStyleImportRuke(selectedStyle) is true)
                ExtentReportsHelper.LogPass($"The Style Import Rule page of selected item: {selectedStyle} displays correctly.");
            else
                ExtentReportsHelper.LogFail($"The Style Import Rule page of selected item: {selectedStyle} displays with incorrect sub header/ title.");

            // Step 3: Open Style Import Rule page from ESTIMATING menu
            ExtentReportsHelper.LogInformation(" Step 3: Open Style Import Rule page from ESTIMATING menu.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_IMPORT_RULE_URL);

            if (StyleImportRulesPage.Instance.DisplayCorrectStyleImportRuke(string.Empty, true) is true)
                ExtentReportsHelper.LogPass($"The Style Import Rule page displays correctly.");
            else
                ExtentReportsHelper.LogFail($"The Style Import Rule page displays with incorrect sub header/ title.");

            // Change page size before verifing the item on the grid
            StyleImportRulesPage.Instance.ChangePageSize(50);

            // Find all style import rule with name same as with the created one. Delete it
            while (StyleImportRulesPage.Instance.IsItemInGrid("Default", StyleImportRulesData.DefaultStyle) is true)
            {
                StyleImportRulesPage.Instance.DeleteStyleRule(StyleImportRulesData.DefaultStyle, StyleImportRulesData.Styles);
            }

            // Step 4 - 5: Add new Style Import Rule
            ExtentReportsHelper.LogInformation(" Step 4 - 5: Add new Style Import Rule.");
            getStyleImportRulesData = StyleImportRulesPage.Instance.AddNewStyleRule(StyleImportRulesData);

            // Step 6: Switch to another Style and verify new rule is displayed in all Styles
            ExtentReportsHelper.LogInformation($"Step 6: Switch to another Style and verify new rule is displayed in all Styles.");
            VerifyNewRuleOnAnotherStyle(1, getStyleImportRulesData.DefaultStyle);

            // Step 7: Edit Style import rule
            ExtentReportsHelper.LogInformation($" Step 7: Update current rule with new Style.");
            StyleImportRulesPage.Instance.EditItemInGrid(getStyleImportRulesData.DefaultStyle);
        }

        private void VerifyNewRuleOnAnotherStyle(int index, string newRule)
        {
            StyleImportRulesPage.Instance.SwitchToStyle(index);
            //Change page size
            StyleImportRulesPage.Instance.ChangePageSize(50);
            bool isFound = StyleImportRulesPage.Instance.IsItemInGrid("Default", newRule);

            if (isFound)
                ExtentReportsHelper.LogPass($"New Style Import Rule with Style /'{newRule}/' displays correctly with style: {StyleImportRulesPage.Instance.SubHeadTitle()}.");
            else
                ExtentReportsHelper.LogFail($"New Style Import Rule with Style /'{newRule}/' doesn't display on current page.");
        }

        [TearDown]
        public void DeleteStyleImportRule()
        {
            StyleImportRulesPage.Instance.DeleteStyleRule(getStyleImportRulesData.DefaultStyle, getStyleImportRulesData.Styles);
        }
        #endregion
    }
}
