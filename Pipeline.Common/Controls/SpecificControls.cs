using LinqToExcel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.ObjectModel;

namespace Pipeline.Common.Controls
{
    public class SpecificControls : BaseControl
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ref IWebDriver _driver => ref BaseValues.DriverSession;

        private const int timeout = 3;
        public SpecificControls(Row row) : base(row) { GetWrappedControl(); }
        public SpecificControls(Row row, bool is_dropdown_item) : base(row, is_dropdown_item) { GetWrappedControl(); }

        public SpecificControls(IWebElement webElement) : base(webElement) { }

        public SpecificControls(IWebElement webElement, bool is_dropdown_item) : base(webElement, is_dropdown_item) { }

        public SpecificControls(FindType findType, string valueToFind) : base(findType, valueToFind)
        {
            SetDefaultWait();
            GetWrappedControl();
        }

        public SpecificControls(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds)
        {
            SetDefaultWait();
            GetWrappedControl();
        }

        public bool AlertExists() {
            IAlert alert = ExpectedConditions.AlertIsPresent().Invoke(_driver);
            return (alert != null);
        }

        public void Click(bool capture_screenshot = true) {
            try {
                IWebElement internal_element_obj = this.GetWrappedControl();
                if (!CommonHelper.is_valid(internal_element_obj)) {
                    Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] is null, disabled, or not visible");
                    throw new InvalidElementStateException("Web element is null, disabled, or not visible");
                }

                //Clear any existing input to prevent conflicting actions
                //((IActionExecutor)_driver).ResetInputState();
                //System.Threading.Thread.Sleep(100);

                //Scroll page to the center of the element
                CommonHelper.MoveToCenterOfElement(internal_element_obj, true);

                if (capture_screenshot) {
                    string text = GetText() == "" ? GetAttribute("value") : GetText();
                    CaptureAndLog($"Element <font color ='green'><b>{text}</b></font> will be clicked after capturing the screenshot");
                }

                //Build the input action for clicking the element
                IAction click_element = new Actions(_driver).Click(internal_element_obj).Build();
                click_element.Perform();

                //Clicking directly on an IWebElement is deprecated and can cause unexpected behavior
                //Use the Selenium Action classes instead like the above example
                //GetWrappedControl().Click();

                //BasePage.JQueryLoad();
                System.Threading.Thread.Sleep(500);
            } catch (Exception any_ex) {
                //try {
                //    System.Threading.Thread.Sleep(250);
                //    if (!AlertExists())
                //        CommonHelper.JavaScriptClick(this);
                //} catch (Exception js_ex) {
                string error = $"Encountered exception while attempting to click web element of a control - Exception #1: [ {any_ex.StackTrace} ]"; //, Exception #2: [ {js_ex.StackTrace} ];";
                    Console.WriteLine(error);
                    Log.Warn(error);
                    throw any_ex;
                //}
            }
        }

        #region "Find Element in Element"
        public IWebElement FindElement(FindType findElementType, string valueToFind, int timeoutInSeconds = timeout)
        {
            return FindElement(GetWrappedControl(), findElementType, valueToFind, timeoutInSeconds);
        }

        public IWebElement FindElement(string findElementType, string valueToFind, int timeoutInSeconds = timeout)
        {
            Enum.TryParse(findElementType, out FindType findType);
            return FindElement(GetWrappedControl(), findType, valueToFind, timeoutInSeconds);
        }

        private IWebElement FindElement(IWebElement control, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new DefaultWait<IWebDriver>(BaseValues.DriverSession)
                {
                    Timeout = TimeSpan.FromSeconds(timeoutInSeconds),
                    PollingInterval = TimeSpan.FromMilliseconds(250)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));

                try
                {
                    return wait.Until(d =>
                    {
                        try { return control.FindElement(by); }
                        catch { return null; }
                    });
                }
                catch (WebDriverTimeoutException) { return null; }
                catch (TimeoutException) { return null; }
            }
            return control.FindElement(by);
        }

        private IWebElement FindElement(IWebElement control, FindType findElementType, string valueToFind, int timeoutInSeconds = timeout)
        {
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElement(control, By.Name(valueToFind), timeoutInSeconds);
                case FindType.XPath:
                    return FindElement(control, By.XPath(valueToFind), timeoutInSeconds);
                case FindType.Id:
                    return FindElement(control, By.Id(valueToFind), timeoutInSeconds);
                case FindType.TagName:
                    return FindElement(control, By.TagName(valueToFind), timeoutInSeconds);
                default:
                    throw new NotSupportedException(string.Format("Find Element Type - {0} - is not supported for the method FindElement.", findElementType));
            }
        }

        #endregion

        #region "Find Elements in Element"
        public ReadOnlyCollection<IWebElement> FindElements(FindType findElementType, string valueToFind, int timeoutInSeconds = timeout)
        {
            return FindElements(GetWrappedControl(), findElementType, valueToFind, timeoutInSeconds);
        }

        public ReadOnlyCollection<IWebElement> FindElements(string findElementType, string valueToFind, int timeoutInSeconds = timeout)
        {
            Enum.TryParse(findElementType, out FindType findType);
            return FindElements(GetWrappedControl(), findType, valueToFind, timeoutInSeconds);
        }

        private ReadOnlyCollection<IWebElement> FindElements(IWebElement control, FindType findElementType, string valueToFind, int timeoutInSeconds = timeout)
        {
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElements(control, By.Name(valueToFind), timeoutInSeconds);
                case FindType.XPath:
                    return FindElements(control, By.XPath(valueToFind), timeoutInSeconds);
                case FindType.TagName:
                    return FindElements(control, By.TagName(valueToFind), timeoutInSeconds);
                case FindType.Id:
                    return FindElements(control, By.Id(valueToFind), timeoutInSeconds);
                default:
                    throw new NotSupportedException(string.Format("Find Element Type - {0} - is not supported for the method FindElements.", findElementType));
            }
        }

        private ReadOnlyCollection<IWebElement> FindElements(IWebElement control, By by, int timeoutInSeconds)
        {
            if (timeoutInSeconds > 0)
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                try
                {
                    return wait.Until(
                        drv => (control.FindElements(by).Count > 0) ? GetWrappedControl().FindElements(by) : null
                    );
                }
                
                catch (WebDriverTimeoutException) { return null; }
                catch (TimeoutException) { return null; }
            }
            var elements = GetWrappedControl().FindElements(by);
            return elements;
        }

        #endregion


    }
}
