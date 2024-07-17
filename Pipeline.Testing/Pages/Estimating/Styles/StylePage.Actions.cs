using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Styles.DetailStyles;

namespace Pipeline.Testing.Pages.Estimating.Styles
{
    public partial class StylePage
    {
        public void ClickAddStyleIcon()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return StylePage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            StylePage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitGridLoad();
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyles']/div[1]");
        }

        public StylePage EnterStyleNameToFilter(string columnName, string styleName)
        {
            StylePage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, styleName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyles']/div[1]");
            return this;
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            StylePage_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public string SelectItemInGridByOrder(string columnName, int order)
        {
            string StyleXpath = $"//*[@id='ctl00_CPH_Content_rgStyles_ctl00']/tbody/tr[{order}]/td/a[contains(@id, 'hypStyleName')]";
            IWebElement style = FindElementHelper.FindElement(FindType.XPath, StyleXpath);

            if (style.Displayed)
            {
                string styleName = style.Text;
                StylePage_Grid.ClickItemInGrid(columnName, styleName);
                PageLoad();
                return styleName;
            }
            ExtentReportsHelper.LogInformation("There are no Style in the grid view.");
            return string.Empty;
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            StylePage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyles']/div[1]");
        }

        /// <summary>
        ///  Create a new Style
        /// </summary>
        public void CreateNewStyle(StyleData data)
        {
            ClickAddStyleIcon();

            string expectedURl = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_STYLE_URL;
            if (StyleDetailPage.Instance.IsPageDisplayed(expectedURl) is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Style detail page isn't displayed." +
                    $"<br>Expected URL: {expectedURl}" +
                    $"Actual URL: {CurrentURL}</font>");
            else
                ExtentReportsHelper.LogPass("<font color ='green'><b>Style detail page displayed successfully.</b></font>");

            StyleDetailPage.Instance.CreateStyle(data);
            StyleDetailPage.Instance.Save();
        }
        public void DeleteStyle(StyleData data)
        {
            // Insert name to filter and click filter by Contain value
            EnterStyleNameToFilter("Name", data.Name);
            bool isFound = IsItemInGrid("Name", data.Name);
            if (isFound)
            {
                // 5. Select the trash can to delete the new Selection Group; 
                DeleteItemInGrid("Name", data.Name);
                WaitGridLoad();
                if ("Style was deleted!" == StylePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass($"Product Style {data.Name} deleted successfully.");
                }
                else
                {
                    if (IsItemInGrid("Name", data.Name))
                        ExtentReportsHelper.LogFail($"Product Style {data.Name} could not be deleted.");
                    else
                        ExtentReportsHelper.LogPass($"Product Style {data.Name} deleted successfully.");
                }
            }
        }
    }
}
