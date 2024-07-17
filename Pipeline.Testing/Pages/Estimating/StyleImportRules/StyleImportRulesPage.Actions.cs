using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.StyleImportRules.AddRule;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.Estimating.StyleImportRules
{
    public partial class StyleImportRulesPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhaseRule_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingPhaseRule_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            IWebElement delete = FindElementHelper.FindElement(FindType.XPath, $"//span[text()='{value}']//..//../td/input[contains(@src,'delete')]");
            delete.Click();
           // BuildingPhaseRule_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            //BuildingPhaseRule_Grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_gvRules']/div[1]", 2000);
        }

        public void ClickAddToOpenAddRuleModal()
        {
            PageLoad();
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNew']").Click();
            AddRuleModal = new AddRuleModal();
        }

        public void CloseModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='options-modal']/section/article/header/a").Click();
            System.Threading.Thread.Sleep(500);
            CommonHelper.CaptureScreen();
        }

        public void SwitchToStyle(int index)
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblTitle']").Click();
            string styleXpath = $"//*[@class='dropdown-menu subhead-lateral']/li[{index}]";
            FindElementHelper.FindElement(FindType.XPath, styleXpath).Click();
            PageLoad();
        }

        public void EditItemInGrid(string defaultName)
        {
            // Click Edit button
            string editXpath = $"//*[@id='ctl00_CPH_Content_rgRules_ctl00']/tbody/tr[./td/span[contains(text(),'{defaultName}')]]/td/input[@alt='Edit']";
            FindElementHelper.FindElement(FindType.XPath, editXpath).Click();

            // Wait loading grid
            WaitGridLoad();

            // Verify Updated Style Grid displayed
            string selectedDefaultXpath = $"//*[@id='ctl00_CPH_Content_rgRules_ctl00']/tbody/tr/td/select[contains(@id,'ddlDefaultStyle') and ./option[@selected='selected' and contains(.,'{defaultName}')]]";
            string selectedStyletXpath = selectedDefaultXpath + "/../../td[select[contains(@id,'lboxStyles')]]/select";

            IWebElement rule = FindElementHelper.FindElement(FindType.XPath, selectedDefaultXpath);
            if (rule != null)
            {
                ExtentReportsHelper.LogPass($"Selected Default is displayed correctly.");
                DropdownList style = new DropdownList(FindType.XPath, selectedStyletXpath);

                if (style.IsDisplayed(false))
                {

                    style.SelectItem("carpet", true, false);

                    // Click Appy button
                    string applyXpath = selectedDefaultXpath + "/../../td/input[@alt='Update']";
                    FindElementHelper.FindElement(FindType.XPath, applyXpath).Click();

                    // Wait loading grid
                    WaitGridLoad();

                    var applyStyleXpath = $"//*[@id='ctl00_CPH_Content_rgRules_ctl00']/tbody/tr[./td/span[contains(text(),'{defaultName}')]]/td[./p[contains(text(),'{defaultName}')]]/p";
                    ListItem styleListApplyMode = new ListItem(FindElementHelper.FindElements(FindType.XPath, applyStyleXpath).ToList());
                    VerifyStyleList(styleListApplyMode.GetAllItemsName(), "carpet");
                }
            }
            else
            {
                ExtentReportsHelper.LogFail($"Selected Default is displayed incorrectly.");
            }
        }

        private void VerifyStyleList(IList<string> styles, params string[] styleList)
        {
            // Split list of Style and Manu => Get style only
            IList<string> styleNameList = new List <string>();
            foreach (string style in styles)
            {
                string styleName = style.Split('-').First();
                styleNameList.Add(styleName);
            }

            foreach (string item in styleList)
            {
                var result = styleNameList.Where(stringToCheck => stringToCheck.Contains(item)).ToList();
                if (result != null )
                {
                    ExtentReportsHelper.LogPass($"Style {item} display correctly in the grid view.");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"Style {item} doesn't display in the grid view.");
                }
            }         

        }

        public void ChangePageSize(int pageSize)
        {
            BuildingPhaseRule_Grid.ChangePageSize(pageSize);
            BuildingPhaseRule_Grid.WaitGridLoad();
        }

        public void DeleteStyleRule(string defaultStyle, string style)
        {
            DeleteItemInGrid("Default", defaultStyle);
            WaitGridLoad();
            System.Threading.Thread.Sleep(5000);

            if (IsEmptyGridView())
            {
                ExtentReportsHelper.LogPass(null, "Style Import Rule deleted successfully!");
            }
            else
            {
                // There is no toast message when deleting import style => verify item on the grid instead
                bool isFound = IsItemInGrid("Default", defaultStyle);
                if (isFound is true)
                    ExtentReportsHelper.LogInformation($"Style Import Rule with Default Style \"{defaultStyle} \" and Styles \"{style} \" still exists on the grid!");
                else
                    ExtentReportsHelper.LogPass(null, "Style Import Rule deleted successfully and not displayed in grid!");
            }
        }
      

        public StyleImportRulesData AddNewStyleRule(StyleImportRulesData StyleImportRulesData)
        {
            CommonHelper.ScrollToBeginOfPage();
            ClickAddToOpenAddRuleModal();
            if (!AddRuleModal.IsModalDisplayed)
            {
                ExtentReportsHelper.LogFail("Add Style Import Rule modal is not displayed.");
            }

            // Step 3: Populate all values
            // Create Style Import Rule - Click 'Save' Button
            AddRuleModal.IsActive(StyleImportRulesData.Active);
            StyleImportRulesData.DefaultStyle = AddRuleModal.SelectDefaultStyle(StyleImportRulesData.DefaultStyle);
            StyleImportRulesData.Styles = AddRuleModal.SelectStyles(StyleImportRulesData.Styles);
            string actualmessage = AddRuleModal.Save();
            StyleImportRulesData _getstyleImportRulesData = new StyleImportRulesData(StyleImportRulesData)
            {
                DefaultStyle = StyleImportRulesData.DefaultStyle,
                Styles = StyleImportRulesData.Styles
            };

            if (actualmessage.Equals("Style Import Rules added successfully!"))
            {
                ExtentReportsHelper.LogPass($"Create Style Import Rule with Default Style {StyleImportRulesData.DefaultStyle} and Styles {StyleImportRulesData.Styles}  is added sucessfully.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Create Style Import Rule with Default Style {StyleImportRulesData.DefaultStyle} and Styles {StyleImportRulesData.Styles} is not added sucessfully.");
            }

            //check item in grid
            if (IsItemInGrid("Default", StyleImportRulesData.DefaultStyle) is true)
            {
                ExtentReportsHelper.LogPass($" Style Import Rule with Default Style {StyleImportRulesData.DefaultStyle} and Styles {StyleImportRulesData.Styles} was displayed in grid.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Style Import Rule with Default Style \"{StyleImportRulesData.DefaultStyle}\" and Styles \"{StyleImportRulesData.Styles}\" was not display on grid.");
            }

            return _getstyleImportRulesData;

        }
    }

}
