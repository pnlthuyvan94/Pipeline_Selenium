using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Common.Pages
{
    // Apply Singleton Stuff
    public abstract class DashboardContentPage<TPage> : ToolbarMenuSection
        where TPage : DashboardContentPage<TPage>, new()
    {
        private static readonly Lazy<TPage> _lazyPage = new Lazy<TPage>(() => new TPage());
        public static TPage Instance => _lazyPage.Value;

        SpecificControls HeaderPane => new SpecificControls(FindType.XPath, "//*/section/article/header[./a[ @data-original-title='Add']]");

        protected DashboardContentPage() : base() { }

        /// <summary>
        /// Left navigation on details content, You should input exact item name which you want to navigate
        /// </summary>
        /// <param name="itemName"></param>
        public void LeftMenuNavigation(string itemName, bool isCapture = true, bool isOpenNewTab = false)
        {
            CommonHelper.ScrollToBeginOfPage();

            var leftNavMenu = FindElementHelper.FindElement(FindType.XPath, "//section[@class='sidebar aside' or @class='sidebar']");
            // Using Xpath to find the element
            // ItemName is the Name which display on the left navigation bar 
            var _xPath = "./ul/li/a[text()='" + itemName + "']";

            if (itemName != null && itemName.Length != 0)
            {
                try
                {
                    var item = leftNavMenu.FindElement(By.XPath(_xPath));
                    string text = item.Text == "" ? item.GetAttribute("value") : item.Text;
                    if (isCapture)
                        ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"Element <font color ='green'><b>{text}</b></font> has been clicked after capturing the screenshot");
                    //item.Click();

                    if (isOpenNewTab)
                        CommonHelper.OpenLinkInNewTab(item.GetAttribute("href"));
                    else
                        item.Click();

                    BasePage.JQueryLoad();
                    System.Threading.Thread.Sleep(667);
                }
                catch (NoSuchElementException)
                {
                    throw new NoSuchElementException(string.Format("Could not find item with name - {0} - on your Left Menu.", itemName));
                }

            }

        }

        /// <summary>
        /// Get item on header pane by Name
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public IWebElement GetItemOnHeader(string itemName)
        {
            CommonHelper.ScrollToBeginOfPage();

            var itemToClick = HeaderPane.FindElement(FindType.XPath, "a[text()='" + itemName + "']");
            if (itemToClick is null)
                return null;
            return itemToClick;
        }

        /// <summary>
        /// Get item on header by default item provide
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public IWebElement GetItemOnHeader(DashboardContentItems itemName)
        {
            CommonHelper.ScrollToBeginOfPage();

            string add = $".//a[ @data-original-title='Add' or @class='button add'] | //a[ @data-original-title='Add' or @class='button add'] | //a[ contains(@data-original-title, 'Create New')]";
            switch (itemName)
            {
                case DashboardContentItems.Add:
                    var Add = FindElementHelper.FindElement(FindType.XPath, add);
                    return Add;
                case DashboardContentItems.Utilities:
                    var Utils = FindElementHelper.FindElement(FindType.XPath, "//a[ @data-original-title='Utilities' or text()='Util']");
                    return Utils;
                case DashboardContentItems.BulkActions:
                    var BulkActions = FindElementHelper.FindElement(FindType.XPath, "//a[@id='bulk-actions']");
                    if(BulkActions == null)
                        BulkActions = FindElementHelper.FindElement(FindType.XPath, "//a[@id='bulk-actions1']");
                    return BulkActions;
                case DashboardContentItems.Back:
                    var Back = FindElementHelper.FindElement(FindType.XPath, "//a[text()='Back to Folder']");
                    return Back;
                case DashboardContentItems.ChangeStatus:
                    var ChangeStatus = FindElementHelper.FindElement(FindType.XPath, "//a[text()='Change Status']");
                    return ChangeStatus;
                default:
                    return GetItemOnHeader(itemName.ToString("g"));
            }
        }
    }
}
