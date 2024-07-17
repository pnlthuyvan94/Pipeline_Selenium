using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Common.Pages
{
    // Apply Singleton Stuff
    public class DetailsContentPage<TPage> : ToolbarMenuSection
        where TPage : DetailsContentPage<TPage>, new()
    {
        private static readonly Lazy<TPage> _lazyPage = new Lazy<TPage>(() => new TPage());
        public static TPage Instance => _lazyPage.Value;
        protected DetailsContentPage() : base() { }

        /// <summary>
        /// Left navigation on details content, You should input exact item name which you want to navigate
        /// </summary>
        /// <param name="itemName"></param>
        public void LeftMenuNavigation(string itemName, bool isCapture = true)
        {
            CommonHelper.ScrollToBeginOfPage();

            var leftNavMenu = FindElementHelper.FindElement(FindType.XPath, "//article/section[@class='sidebar']");
            // Using Xpath to find the element
            // ItemName is the Name which display on the left navigation bar 
            var _xPath = $"./ul/li/a[text()='{itemName}']";

            if (!string.IsNullOrEmpty(itemName))
            {
                try
                {
                    var item = leftNavMenu.FindElement(By.XPath(_xPath));

                    CommonHelper.MoveToElement(item, true);

                    string text = item.Text == "" ? item.GetAttribute("value") : item.Text;
                    if (isCapture)
                        ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"Element <font color ='green'><b>{text}</b></font> has been clicked after capturing the screenshot");
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

        // Get sub head title from breadscrumb bar

        public string SubHeadTitle()
        {
            string xpath = @"//*[@id='aspnetForm']/div/section[1]/ul/li/div/a[@data-toggle='dropdown']/span 
                             | //*[@id='aspnetForm']/div/section[1]/ul/li/a[@data-toggle='dropdown']/span
                             | //*[@id='aspnetForm']/div[3]/section/ul/li[2]/span/div/div/a[@data-toggle='dropdown']/span
                             | //*[@id='aspnetForm']/div/section[1]/ul/li[2]/div/div/a[@data-toggle='dropdown']/span
                             | //ul[contains(@class,'subhead-list')]/li[starts-with(@class,'subhead-list-item')]/a/span
                             | (//a[starts-with(@class,'subhead-list-item') and @data-toggle='dropdown'])[last()]";
            var titleName = FindElementHelper.FindElement(FindType.XPath, xpath);
            if (titleName is null)
                return string.Empty;
            return titleName.Text;
        }

        public bool IsPresentInLeftMenuNavigation(string itemName, bool isCapture = true)
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

                    return true;
                }
                catch (NoSuchElementException)
                {
                    return false;
                }

            }
            return false;
        }

    }
}
