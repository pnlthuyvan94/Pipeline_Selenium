using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.Options.AddOption;
using System.IO;

namespace Pipeline.Testing.Pages.Assets.Options
{
    public partial class OptionPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionPage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            //WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return OptionPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public float IsNumberItemInGrid()
        {
            return OptionPage_Grid.GetTotalItems;
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            OptionPage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitGridLoad();
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptions']/div[1]");
        }

        public void ClickAddToOpenCreateOptionModal()
        {
            //PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddOptionModal = new AddOptionModal();
        }

        public void CloseModal()
        {
            Button close = new Button(FindType.XPath, "//*[@id='addoption-modal']/section/header/a");
            close.Click();
            System.Threading.Thread.Sleep(800);
        }

        public void SelectItemInGrid(int columIndex, int rowIndex)
        {
            OptionPage_Grid.ClickItemInGrid(columIndex, rowIndex);
            PageLoad();
        }

        public void SelectItemInGrid(string columnName, string valueToFind)
        {
            OptionPage_Grid.ClickItemInGrid(columnName, valueToFind);
            PageLoad();
        }

        public void SelectItemInGridWithTextContains(string columnName, string valueToFind)
        {
            OptionPage_Grid.ClickItemInGridWithTextContains(columnName, valueToFind);
            PageLoad();
        }

        public void CreateNewOption(OptionData _option)
        {
            ClickAddToOpenCreateOptionModal();

            if (AddOptionModal.IsModalDisplayed() is false)
                ExtentReportsHelper.LogFail($"Could not open Create Option modal or the title is incorrect.");

            // Create Option - Click 'Save' Button
            AddOptionModal.AddOption(_option);
            string _expectedMessage = $"Option Number is duplicated.";
            string actualMsg = GetLastestToastMessage();
            if (_expectedMessage.Equals(actualMsg))
            {
                ExtentReportsHelper.LogFail($"Could not create Option with name { _option.Name} and Number {_option.Number}.");
            }
            // Step 4. Verify new Option in header
            PageLoad();
            System.Threading.Thread.Sleep(5000);
        }

        public void DeleteOption(string optionName)
        {
            // 7. Select item and click deletete icon
            FilterItemInGrid("Name", GridFilterOperator.Contains, optionName);
            WaitGridLoad();
            System.Threading.Thread.Sleep(2000);

            if (IsItemInGrid("Name", optionName) is true)
            {
                DeleteItemInGrid("Name", optionName);
                WaitGridLoad();
                string successfulMess = $"Option {optionName} deleted successfully!";
                if (successfulMess == GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Option {optionName} deleted successfully!</b></font>");
                }
                else
                {
                    if (IsItemInGrid("Code", optionName))
                        ExtentReportsHelper.LogWarning(null, $"Option {optionName} could not be deleted - Possible constraint preventing deletion.");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Option {optionName} deleted successfully!</b></font>");
                }
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"Can't find option with name {optionName} to delete.");
            }
        }

        /// <summary>
        /// Get Option Name by column and row
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public string GetOptionNameByIndexAndColumn(string columnName, int rowIndex)
        {
            IWebElement option = OptionPage_Grid.GetItemByRowAndColumn(columnName, rowIndex);
            return option != null ? option.Text : string.Empty;
        }

        /// <summary>
        /// Get total number on the grid view
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return OptionPage_Grid.GetTotalItems;
        }

        /// <summary>
        /// Click on (...) more item to select import/export function
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isCaptured"></param>
        public void SelectItemInUtiliestButton(string item, bool isCaptured = true)
        {
            try
            {
                Button moreItem = new Button(FindType.XPath, "//*[@data-original-title='Utilities']");
                moreItem.Click();
                Button Export_btn = new Button(FindType.XPath, "//*[@data-target='#OptionsExportModal']");
                Export_btn.Click();

                string itemXpath = $"//*[@id='exportOption']";
                DropdownList itemNeedToClick = new DropdownList(FindType.XPath, itemXpath);
                CommonHelper.WaitUntilElementVisible(5, itemXpath, false);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(itemNeedToClick),
                        $"Click <font color='green'><b><i>{item:g}</i></b></font> button.");
                itemNeedToClick.SelectItem(item);
                System.Threading.Thread.Sleep(3000);
                Button Continue_btn = new Button(FindType.XPath, "//*[@id='exportContinue']");
                Continue_btn.Click();
                Button CloseModal_btn = new Button(FindType.XPath, "//*[@class='card-header']/h1[text()='Options Export']/following-sibling::a");
                CloseModal_btn.Click();
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {item} on Utilities menu"));
            }
        }

        /// <summary>
        /// Download basline file to compare
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        public void DownloadBaseLineFile(string exportType, string exportFileName)
        {
            // Download baseline file to report folder
            SelectItemInUtiliestButton(exportType, false);
            System.Threading.Thread.Sleep(3000);
            // Verify and move it to baseline folder
            ValidationEngine.DownloadBaseLineFile(exportType, exportFileName);
        }

        /// <summary>
        /// Export file from More menu
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        /// <param name="expectedTotalNumber"></param>
        public void ExportFile(string exportType, string exportFileName, int expectedTotalNumber, string expectedExportTitle)
        {
            bool isCaptured = false;
            TableType format;
            if (exportType.ToLower().Contains("csv"))
                format = TableType.CSV;
            else if (exportType.ToLower().Contains("excel"))
                format = TableType.XLSX;
            else
                format = TableType.XML;

            string fileName = ValidationEngine.GetFullExportFileName(exportFileName, format);
            string filePath = CommonHelper.GetFullDownLoadFilePath(fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            // Download file
            SelectItemInUtiliestButton(exportType, isCaptured);

            System.Threading.Thread.Sleep(3000);

            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(exportType, exportFileName, expectedExportTitle, expectedTotalNumber);
        }
    }

}
