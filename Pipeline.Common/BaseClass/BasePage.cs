using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Common.BaseClass
{
    public abstract class BasePage
    {
        protected IWebDriver driver;
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        protected FindElementHelper FindElementHelper;
        protected ExcelFactory ExcelHelper { get; set; }

        protected string BaseFolderURL => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        protected string BaseDashboardUrl => BaseValues.DashboardURL;
        
        protected BasePage()
        {
            if (FindElementHelper == null)
                this.FindElementHelper = FindElementHelper.Instance();
            if (this.driver == null)
                this.driver = BaseValues.DriverSession;
            PageLoad();

            if (!(this is BaseControl) && this.driver != null)
                //Use CSS trick to ensure our page content emulates a minimum width of the provided emulated device width
                CommonHelper.FitViewportToContentByJavascript();
        }

        /// <summary>
        /// The timeout of pages can be overridden for especially purpose
        /// </summary>
        /// <param name="iSeconds"></param>
        // Wait until this page load completed (all elements on DOM is ready state)
        public static void PageLoad(int iSeconds = 30)
        {
            if (iSeconds < BaseValues.PageloadTimeouts) iSeconds = BaseValues.PageloadTimeouts;
            var wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(iSeconds));
            wait.Until((x) =>
                {
                    return ((IJavaScriptExecutor)BaseValues.DriverSession).ExecuteScript("return document.readyState").Equals("complete");
                });
        }

        /// <summary>
        /// Page waiting until all jQuery is completed loading
        /// </summary>
        /// <param name="iSeconds"></param>
        public static bool JQueryLoad(int iSeconds = 30)
        {
            bool isPageReady = true;

            if (iSeconds < BaseValues.PageloadTimeouts) iSeconds = BaseValues.PageloadTimeouts;
            try
            {
                var wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(iSeconds));
                wait.Until((d) =>
                {
                    try
                    {
                        //First check if Document is ready and if JQuery doesn't exist - if JQuery doesn't exist and Doc is ready, then the page is loaded
                        bool documentReady = (bool)((IJavaScriptExecutor)BaseValues.DriverSession).ExecuteScript("return document.readyState").Equals("complete");
                        bool JQueryExists = (bool)((IJavaScriptExecutor)BaseValues.DriverSession).ExecuteScript("return (typeof jQuery != 'undefined')");
                        if (documentReady && !JQueryExists) return true;

                        //If either JQuery does exist or the document isn't ready, then we need to wait for the jQuery.active flag to be 0, and then the Document readystate should be complete
                        bool JQueryStatus = (bool)((IJavaScriptExecutor)BaseValues.DriverSession).ExecuteScript("return jQuery.active==0");
                        return (documentReady && JQueryStatus);

                    } catch (NoSuchWindowException exception)
                    {
                        Console.WriteLine(exception.Message);
                        Log.Debug($"Window handle for driver session no longer exists. " + exception.Message);
                        return true;
                    }
                });
            }
            // If it have any unhandle alert displayed, throw new exception and break the test
            catch (UnhandledAlertException)
            {
                isPageReady = false;
                throw new UnhandledAlertException(string.Format("An unexpected alert is displayed on your page, the test is stopped. The alert is displayed with the message: \"{0}\"", BaseValues.DriverSession.SwitchTo().Alert().Text));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Log.Debug($"Waiting for JQuery to load timed out after {iSeconds} seconds. " + e.Message);
                isPageReady = false;
            }

            return isPageReady;
        }


        /// <summary>
        /// Get all controls with type toast message
        /// </summary>
        /// <returns>Return list control toast message</returns>
        public IList<IWebElement> GetListMessageControls()
        {
            var _listToastContent = FindElementHelper.FindElements(FindType.XPath, "/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper']", 5);
            return _listToastContent;
        }

        /// <summary>
        /// Get latest message
        /// </summary>
        public string GetLastestToastMessage(int timeout = 20)
        {
            Button item = new Button(FindType.XPath, "/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper' and position()=last()]");
            if (item.WaitForElementIsVisible(timeout, false))
                return item.GetText();
            else
                return string.Empty;
        }

        /// <summary>
        /// Get list message of Toast Message
        /// </summary>
        /// <returns>List message</returns>
        public IList<string> GetListMessage()
        {
            IList<string> listMessage = new List<string>();
            var _listContent = FindElementHelper.FindElement(FindType.XPath, "/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper']");
            // Get the latest toast messsage
            if (_listContent != null)
            {
                var ToastMessage = _listContent.FindElements(By.XPath("/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper']/div/p"));
                foreach (var item in ToastMessage)
                {
                    listMessage.Add(item.Text);
                }
                return listMessage;
            }
            // if the ToastMessage is not exist on this page, return null
            return null;
        }

        /// <summary>
        /// Use this function to handle dialog on the pages. 
        /// </summary>
        /// <param name="Action Name (OK/Cancel)"></param>
        public static void ConfirmDialog(ConfirmType confirm, bool isBackToMainPage = true)
        {
            switch (confirm)
            {
                case ConfirmType.OK:
                    {
                        try
                        {
                            BaseValues.DriverSession.SwitchTo().Alert().Accept();
                        }
                        catch (NoAlertPresentException)
                        {
                            throw new NoAlertPresentException(string.Format("Could not find any alert dialog on your browser"));
                        }
                        PageLoad();
                        break;
                    }
                default: // Cancel to close the dialog
                    try
                    {
                        BaseValues.DriverSession.SwitchTo().Alert().Dismiss();
                    }
                    catch (NoAlertPresentException)
                    {
                        throw new NoAlertPresentException(string.Format("Could not find any alert dialog on your browser"));
                    }
                    break;
            }
            if (isBackToMainPage is true)
            {
                // Back to main windows
                BaseValues.DriverSession.SwitchTo().Window(BaseValues.DriverSession.WindowHandles.First());
            }
        }

        /// <summary>
        /// Get title of current Page
        /// </summary>
        public string Title => BaseValues.DriverSession.Title;

        /// <summary>
        /// Get curent URL of page
        /// </summary>
        public string CurrentURL => BaseValues.DriverSession.Url;


        protected Button CloseDeleteModal_btn => new Button(FindType.XPath, "//*[@id='delete-modal']/section/header/a");

        public void CloseDeleteModal() {
            try { this.CloseDeleteModal_btn.JavaScriptClick(); } catch { }
        }

        /// <summary>
        /// Check status of page in waiting time is 5s
        /// </summary>
        /// <param name="expectedUrl"></param>
        public bool IsPageDisplayed(string expectedUrl)
        {
            Stopwatch timer = new Stopwatch();
            timer.Start();
            while (CurrentURL == null && timer.Elapsed.TotalSeconds < 5)
                System.Threading.Thread.Sleep(1000);
            return CurrentURL.Equals(expectedUrl);
        }

        /// <summary>
        /// Using to wait the loading gif by xpath
        /// </summary>
        public void WaitingLoadingGifByXpath(string xpath, int forceWait = 0)
        {
            WaitingLoadingGifByXpath(xpath, 60, forceWait);
        }

        public void WaitingLoadingGifByXpath(string xpath, int timeout, int forceWait = 0)
        {
            Label loading = new Label(FindType.XPath, xpath);
            loading.WaitUntilExist(3);
            loading.WaitForElementIsInVisible(timeout, false);
            if (forceWait > 0)
                System.Threading.Thread.Sleep(forceWait);
        }

        /// <summary>
        /// Click Toast message to close
        /// </summary>
        public void CloseToastMessage(int timeout = 5, bool isCaptured = true)
        {
            Button item = new Button(FindType.XPath, "/html/body/div[@class='toast-container toast-position-top-center']/div[@class = 'toast-item-wrapper' and position()=last()]");
            if (item.WaitForElementIsVisible(timeout, false))
                CommonHelper.ClickAtPosition(item, Margin.Right, 10, isCaptured);
        }

        /// <summary>
        /// Refresh current page
        /// </summary>
        public void RefreshPage()
        {
            driver.Navigate().Refresh();
            ExtentReportsHelper.LogInformation("Reload this page manually.");
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        public void BackToPreviousPage()
        {
            driver.Navigate().Back();
            ExtentReportsHelper.LogInformation("Back to previous page.");
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }
        public void NavigateURL(string url)
        {
            string fullURL;
            if (url.StartsWith("/"))
                fullURL = BaseValues.DashboardURL + url;
            else
                fullURL = BaseValues.DashboardURL + "/" + url;

            driver.Navigate().GoToUrl(fullURL);
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// Click on (...) more item to select import/export function
        /// </summary>
        /// <param name="item"></param>
        /// <param name="isCaptured"></param>
        public void SelectItemInUtiliestButton(string item, bool isCaptured = true)
        {
            Log.Debug($"Click '...'(Ultilities option) button to show the popup.");
            try
            {
                Button moreItem = new Button(FindType.XPath,"//*[@data-original-title='Utilities']");
                moreItem.Click();
                string itemXpath = $"//*[@data-original-title='Utilities']/following::a[.='{item}']";
                Button itemNeedToClick = new Button (FindType.XPath, itemXpath);
                CommonHelper.WaitUntilElementVisible(5, itemXpath, false);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(itemNeedToClick),
                        $"Click <font color='green'><b><i>{item:g}</i></b></font> button.");
                itemNeedToClick.Click();
                System.Threading.Thread.Sleep(3000);
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {item} on Utilities menu"));
            }
        }

        public void SelectItemInUtilitiesWorksheetProductQuantityGridButton(string item, bool isCaptured = true)
        {
            Log.Debug($"Click '...'(Ultilities option) button to show the popup.");
            try
            {
                Button moreItem = new Button(FindType.XPath, "//*[@data-original-title='Utilities']");
                moreItem.Click();
                Label itemMore = new Label(FindType.XPath, $"//*[@data-original-title='Utilities']/following::a[.='{item}']");

                itemMore.WaitForElementIsInVisible(5);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(itemMore),
                        $"Click <font color='green'><b><i>{item:g}</i></b></font> button.");
                itemMore.Click();
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {item} on Utilities menu"));
            }
        }

        public void SelectItemInUtilitiesButton2(string item, bool isCaptured = true, bool selectUtilities = true)
        {
            Log.Debug($"Click '...'(Ultilities option) button to show the popup.");
            try
            {
                if(selectUtilities)
                {
                    IWebElement moreItem = driver.FindElement(By.XPath("//*[@title='Utilities']"));
                    moreItem.Click();
                }                
                string itemXpath = $"//*[@title='Utilities']/following::a[.='{item}']";
                IWebElement itemNeedToClick = driver.FindElement(By.XPath(itemXpath));
                CommonHelper.WaitUntilElementVisible(5, itemXpath, false);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(itemNeedToClick),
                        $"Click <font color='green'><b><i>{item:g}</i></b></font> button.");
                itemNeedToClick.Click();
                System.Threading.Thread.Sleep(3000);
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {item} on Utilities menu"));
            }
        }
        public void ImportExporFromMoreMenu(string item)
        {
            switch (item)
            {
                case "Import":
                    SelectItemInUtiliestButton(UtilitiesMenu.Import, true);
                    System.Threading.Thread.Sleep(2000);
                    break;
                case "Export":
                    // TODO: Implement later
                    break;
                default:
                    ExtentReportsHelper.LogInformation("Not found Import/Export items");
                    break;
            }
        }

        /// <summary>
        /// Compare export file by Beyond Compare tool
        /// </summary>
        /// <param name="export"></param>
        /// <param name="format"></param>
        public void CompareExportFile(string exportFileName, TableType format)
        {
            ValidationEngine.GenerateReportFile(exportFileName, format);
            ValidationEngine.ValidateExportFileByBeyondCompare(exportFileName, format);
        }

        /// <summary>
        /// Download basline file to compare
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        public void DownloadBaseLineFile(string exportType, string exportFileName)
        {
            // Download baseline file to report folder
            if(exportFileName.Contains("Pipeline_Worksheets"))
            {
                SelectItemInUtilitiesWorksheetProductQuantityGridButton(exportType, false);
            }  
            else
            {
                SelectItemInUtiliestButton(exportType, false);
            }                
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
            if (exportFileName.Contains("Pipeline_Worksheets"))
            {
                SelectItemInUtilitiesWorksheetProductQuantityGridButton(exportType, isCaptured);
            }
            else
            {
                SelectItemInUtiliestButton(exportType, isCaptured);
            }
            
            System.Threading.Thread.Sleep(3000);

            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(exportType, exportFileName, expectedExportTitle, expectedTotalNumber);
        }

        /// <summary>
        /// Export file for Utilities button with different design
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportFileName"></param>
        /// <param name="expectedTotalNumber"></param>
        /// <param name="expectedExportTitle"></param>
        public void ExportFile2(string exportType, string exportFileName, int expectedTotalNumber, string expectedExportTitle, bool selectUtilities = true)
        {
            bool isCaptured = false;
            // Download file
            SelectItemInUtilitiesButton2(exportType, isCaptured, selectUtilities);
            System.Threading.Thread.Sleep(3000);
            // Don't verify total number and header if that's xml file
            if (exportType.ToLower().Contains("xml"))
                return;


            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(exportType, exportFileName, expectedExportTitle, expectedTotalNumber);
        }

        /// <summary>
        /// Open Import page from default page
        /// </summary>
        /// <param name="expectedUrl"></param>
        public void OpenImportPage(string expectedUrl)
        {
            // Click Ipmort button
            SelectItemInUtiliestButton("Import");

            // Verify url of import page
            if (CurrentURL.ToLower().Equals(BaseDashboardUrl.ToLower() + expectedUrl.ToLower()) is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Import page displays successfully with url: '{CurrentURL}'.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The current page isn't the expected import one." +
                    $"<br>Expected URL: {BaseDashboardUrl + expectedUrl}" +
                    $"<br>Actual URL: {CurrentURL}</br></font>");
        }

        public void OpenImportPage()
        {
            // Click Ipmort button
            SelectItemInUtiliestButton("Import");
        }

        public static string BaseApiUrl => BaseValues.BaseApiUrl;
        public static string BasePath => BaseValues.BasePath;

    }
}
