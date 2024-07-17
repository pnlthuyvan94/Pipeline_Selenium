using LinqToExcel;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Pipeline.Common.BaseClass
{
    public abstract class BaseControl : BasePage
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected FindElementHelper findElementHelper;
        protected int defaultTimeout = 8;
        protected int overrideTime = 8;
        protected FindType FindType { get; private set; }
        protected string ValueToFind { get; private set; }
        protected bool isDropdownChild { get; set; }

        protected IWebElement wrappedControl;

        private WebDriverWait wait;

        protected void SetDefaultWait()
        {
            wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(6));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            //wait.Message = "Element to be searched not found";
        }
        private void SetUpBaseControl(IWebElement wrappedControl, string findType, string valueToFind, int timeoutSeconds)
        {
            SetDefaultWait();
            this.overrideTime = timeoutSeconds;
            if (driver == null)
                this.driver = BaseValues.DriverSession;
            if (findElementHelper == null)
                this.findElementHelper = FindElementHelper.Instance();
            switch (findType)
            {
                case "Name":
                    this.FindType = FindType.Name;
                    break;
                case "XPath":
                    this.FindType = FindType.XPath;
                    break;
                case "Id":
                    this.FindType = FindType.Id;
                    break;
                default:
                    this.FindType = FindType.XPath;
                    break;
            }
            this.ValueToFind = valueToFind;
            if (wrappedControl != null)
                this.wrappedControl = wrappedControl;
        }

        public void CaptureAndLog(string message) => ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(this), message);
        

        protected BaseControl(IWebElement wrappedControl, FindType findType, string valueToFind)
        {
            SetUpBaseControl(wrappedControl, findType.ToString("g"), valueToFind, 5);
        }

        protected BaseControl(Row row)
        {
            SetUpBaseControl(null, row[BaseConstants.FindType], row[BaseConstants.ValueToFind], 5);
        }

        protected BaseControl(Row row, bool is_dropdown_item)
        {
            //isDropdownChild = is_dropdown_item;
            SetUpBaseControl(null, row[BaseConstants.FindType], row[BaseConstants.ValueToFind], 5);
        }

        protected BaseControl(string findType, string valueToFind)
        {
            SetUpBaseControl(null, findType, valueToFind, 5);
        }

        protected BaseControl(string findType, string valueToFind, int timeout)
        {
            SetUpBaseControl(null, findType, valueToFind, timeout);
        }

        protected BaseControl(FindType findType, string valueToFind)
        {
            SetUpBaseControl(null, findType.ToString("g"), valueToFind, 5);
        }

        protected BaseControl(FindType findType, string valueToFind, int timeoutSeconds)
        {
            SetUpBaseControl(null, findType.ToString("g"), valueToFind, timeoutSeconds);
        }

        protected BaseControl(IWebElement control)
        {
            wrappedControl = control;
            ValueToFind = GetXpath(control, "");
            SetUpBaseControl(control, "XPath", ValueToFind, 5);
        }

        protected BaseControl(IWebElement control, bool is_dropdown_item)
        {
            //isDropdownChild = is_dropdown_item;
            wrappedControl = control;
            ValueToFind = GetXpath(control, "");
            SetUpBaseControl(control, "XPath", ValueToFind, 5);
        }

        public IWebElement RefreshWrappedControl()
        {
            if (this.defaultTimeout != this.overrideTime)
                wrappedControl = findElementHelper.FindElement(this.FindType, this.ValueToFind, this.overrideTime);
            else
                wrappedControl = findElementHelper.FindElement(this.FindType, this.ValueToFind);
            return wrappedControl;
        }

        public IWebElement GetWrappedControl()
        {
            if (this.wrappedControl == null)
                if (this.defaultTimeout != this.overrideTime)
                    wrappedControl = findElementHelper.FindElement(this.FindType, this.ValueToFind, this.overrideTime);
                else
                    wrappedControl = findElementHelper.FindElement(this.FindType, this.ValueToFind);
            return wrappedControl;
        }

        public string GetAttribute(string attributeName)
        {
            return GetWrappedControl().GetAttribute(attributeName);
        }


        /// <summary>
        /// Verified the element existed on this screen and log the information
        /// </summary>
        /// <returns>existed or not</returns>
        public bool IsExisted(bool isCaptured = true)
        {
            Log.Info("Verified the element existed on this screen and log the information");
            if (GetWrappedControl() != null)
            {
                CommonHelper.MoveToElement(GetWrappedControl(), true);
                Log.Info($"Getting Exist property and capture screen of control [{FindType.ToString("g")} : {ValueToFind}]");
                if (isCaptured)
                    this.CaptureAndLog($"The element <font color='green'><b>{this.GetText()}</b></font> is <font color='green'><b>existed</b></font> on screen");
                return true;
            }
            else
            {
                Debug.WriteLine($"The element does not exist on screen - This may be expected");
                return false;
            }
        }

        public bool IsEnabled()
        {
            try
            {
                Log.Info($"Getting Enable property of control [{FindType.ToString("g")} : {ValueToFind}]");
                return GetWrappedControl().Enabled;
            }
            catch (NoSuchElementException e)
            {
                Log.Error($"The control does not exist. [{FindType.ToString("g")} : {ValueToFind}]. " + e.Message);
                throw new NoSuchElementException($"The control does not exist. [{FindType.ToString("g")} : {ValueToFind}]");
            }
            catch (StaleElementReferenceException e)
            {
                Log.Error($"The control is out of date, it should refresh before use. [{FindType.ToString("g")} : {ValueToFind}]. " + e.Message);
                throw new StaleElementReferenceException($"The control is out of date, it should refresh before use. [{FindType.ToString("g")} : {ValueToFind}]");
            }
        }

        /// <summary>
        /// Verified the element displayed on this screen and log the information
        /// </summary>
        /// <returns>displayed or not</returns>
        public bool IsDisplayed(bool isCaptured = true)
        {
            Log.Info("Verified the element displayed on this screen and log the information");
            if (GetWrappedControl() != null)
            {
                CommonHelper.MoveToElement(GetWrappedControl(), true);

                try
                {
                    if (GetWrappedControl().Displayed)
                    {
                        if (isCaptured)
                            this.CaptureAndLog($"The element <font color='green'><b>{this.GetText()}</b></font> is <font color='green'><b>displayed</b></font> on screen");
                        return true;
                    }
                    else
                    {
                        if (isCaptured)
                            this.CaptureAndLog($"The element <font color='green'><b>{this.GetText()}</b></font> is <font color='red'><b>NOT display</b></font> on screen");
                        return false;
                    }
                }
                catch (StaleElementReferenceException exc)
                {
                    Debug.WriteLine($"The element is not displayed - This may be expected");
                    return false;
                }
            }
            else
            {
                Debug.WriteLine($"The element is not displayed - This may be expected");
                return false;
            }
        }

        public string GetText()
        {
            try
            {
                Log.Info($"Getting Text property of control [{FindType.ToString("g")} : {ValueToFind}]");
                return GetWrappedControl().Text;
            }
            catch (NoSuchElementException e)
            {
                Log.Error($"The control does not exist. [{FindType.ToString("g")} : {ValueToFind}]. " + e);
                throw new NoSuchElementException($"The control does not exist. [{FindType.ToString("g")} : {ValueToFind}]");
            }
            catch (StaleElementReferenceException e)
            {
                Log.Warn($"The control is out of date, it should refresh before use. [{FindType.ToString("g")} : {ValueToFind}]. " + e);
                throw new StaleElementReferenceException($"The control is out of date, it should refresh before use. [{FindType.ToString("g")} : {ValueToFind}]");
            }
        }

        internal string GetTextWithoutCapture()
        {
            try
            {
                return GetWrappedControl().Text;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public string GetValue()
        {
            try
            {
                Log.Info($"Getting value property of control [{FindType.ToString("g")} : {ValueToFind}]");
                return GetWrappedControl().GetAttribute("value");
            }
            catch (NoSuchElementException e)
            {
                Log.Error($"The control does not exist. [{FindType.ToString("g")} : {ValueToFind}]. " + e);
                throw new NoSuchElementException($"The control does not exist. [{FindType.ToString("g")} : {ValueToFind}]");
            }
            catch (StaleElementReferenceException e)
            {
                Log.Error($"The control is out of date, it should refresh before use. [{FindType.ToString("g")} : {ValueToFind}]. " + e);
                throw new StaleElementReferenceException($"The control is out of date, it should refresh before use. [{FindType.ToString("g")} : {ValueToFind}]");
            }
        }

        public bool WaitUntilExist(int timeout)
        {
            this.wrappedControl = findElementHelper.FindElement(this.FindType, this.ValueToFind, timeout);
            if (this.wrappedControl != null)
                return true;
            return false;
        }

        public bool WaitForElementIsVisible(int timeout, bool captureAndLog = true)
        {
            Log.Info($"Waiting for the element [{FindType.ToString("g")} : {ValueToFind}] exist after {timeout} seconds............");

            wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            try
            {
                IWebElement element;
                switch (FindType)
                {
                    case FindType.Id:
                        element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id(ValueToFind)));
                        break;
                    case FindType.Name:
                        element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name(ValueToFind)));
                        break;
                    case FindType.XPath:
                        element = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath(ValueToFind)));
                        break;
                    default:
                        element = null;
                        break;
                }
                if (captureAndLog)
                {
                    this.CaptureAndLog($"The element <font color='green'><b>{element.Text}</b></font> is <font color='green'><b>visible</b></font> on screen");
                }
                return element.Displayed;
            }
            catch (WebDriverTimeoutException e)
            {
                Log.Warn($"The element [{FindType.ToString("g")} : {ValueToFind}] does not exist after {timeout} seconds. " + e.Message);
                return false;
            }
        }

        public bool WaitForElementIsInVisible(int timeout, bool captureAndLog = true)
        {
            Log.Info($"Waiting for the element [{FindType.ToString("g")} : {ValueToFind}] hide after {timeout} seconds............");

            wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(timeout));
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            try
            {
                bool hide = false;
                switch (FindType)
                {
                    case FindType.Id:
                        hide = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Id(ValueToFind)));
                        break;
                    case FindType.Name:
                        hide = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.Name(ValueToFind)));
                        break;
                    case FindType.XPath:
                        hide = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath(ValueToFind)));
                        break;
                    default:
                        hide = false;
                        break;
                }
                if (captureAndLog)
                    ExtentReportsHelper.LogInformation($"Waiting for the element [{ FindType.ToString("g")} : { ValueToFind}] hide after { timeout} seconds");
                return hide;
            }
            catch (WebDriverTimeoutException e)
            {
                Log.Warn($"The element [{FindType.ToString("g")} : {ValueToFind}] does not exist after {timeout} seconds. " + e.Message);
                return false;
            }
        }

        protected void CaptureStepAndLogInfo(IWebElement control, string message = "")
        {
            if (control is null)
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), message);
            else
            {
                if (BaseValues.IsCaptureEverything)
                {
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), message);
                }
                else
                {
                    this.CaptureAndLog(message);
                }
            }
        }

        public bool IsNull()
        {
            return GetWrappedControl() is null ? true : false;
        }

        /// <summary>
        /// Get the attribute of the value with attribute name
        /// </summary>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public string GetAttribute(AttributeType attributeName)
        {
            return GetWrappedControl().GetAttribute(attributeName.ToString());
        }

        public void SetAttribute(string attName, string attValue)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            CaptureAndLog($"This control is set attribute <font color='green'><b>{attName}</b></font> with value <font color='green'><b>{attValue}</b></font>.");
            executor.ExecuteScript("arguments[0].setAttribute(arguments[1], arguments[2]);",
                    wrappedControl, attName, attValue);
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// Hover mouse on the element
        /// </summary>
        public void HoverMouse()
        {
            Actions action = new Actions(BaseValues.DriverSession);
            action.MoveToElement(GetWrappedControl()).Perform();
            //CaptureAndLog(string.Format(BaseConstants.HoverMouseMessage));
        }

        /// <summary>
        /// Hover mouse on the element and do not capture this screen
        /// </summary>
        internal void HoverMouseWithoutCapture()
        {
            Actions action = new Actions(driver);
            action.MoveToElement(GetWrappedControl()).Perform();
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// Click at the element and using javascript
        /// </summary>
        public void JavaScriptClick(bool isCaptured = true)
        {
            CommonHelper.MoveToElement(this, true);
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            if (isCaptured)
            {
                //string text = GetText() == "" ? GetAttribute("value") : GetText();
                //CaptureAndLog($"Element will be Javascript clicked after capturing the screenshot");
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), $"Element will be Javascript clicked after capturing the screenshot");
            }
            executor.ExecuteScript("arguments[0].click();", GetWrappedControl());
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// Click at the element with coordinate
        /// </summary>
        public void CoordinateClick()
        {
            Actions action = new Actions(BaseValues.DriverSession);
            string text = GetText() == "" ? GetAttribute("value") : GetText();
            CaptureAndLog($"Element <font color ='green'><b>{text}</b></font> will be clicked after capturing the screenshot");
            CaptureAndLog(string.Format(BaseConstants.LeftClickMessage));
            CommonHelper.MoveToElement(GetWrappedControl(), true);
            action.MoveToElement(GetWrappedControl()).Click().Build().Perform();
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// Execute the javascript for this controls. Format "arguments[0].click()", WrappedControl;
        /// </summary>
        public object JavaScriptExecutor(string script, BaseControl WrappedControl)
        {
            IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            CaptureAndLog(string.Format(BaseConstants.JavaScriptMessage));
            return executor.ExecuteScript(script, WrappedControl);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsClickable()
        {
            try
            {
                WebDriverWait wait = new WebDriverWait(BaseValues.DriverSession, TimeSpan.FromSeconds(8));
                wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(GetWrappedControl()));
                return true;
            }
            catch (NoSuchElementException e)
            {
                throw new WebDriverException(e.Message);
            }
            catch (WebDriverException)
            {
                return false;
            }
        }

        public string GetXpath(IWebElement childElement, string current)
        {
            if (childElement is null)
                return null;
            string childTag = childElement.TagName;
            if (childTag.Equals("html"))
            {
                return "/html[1]" + current;
            }
            IWebElement parentElement = childElement.FindElement(By.XPath(".."));
            List<IWebElement> childrenElements = parentElement.FindElements(By.XPath("*")).ToList();
            int count = 0;
            for (int i = 0; i < childrenElements.Count; i++)
            {
                IWebElement childrenElement = childrenElements[i];
                string childrenElementTag = childrenElement.TagName;
                if (childTag.Equals(childrenElementTag))
                {
                    count++;
                }
                if (childElement.Equals(childrenElement))
                {
                    return GetXpath(parentElement, "/" + childTag + "[" + count + "]" + current);
                }
            }
            return null;
        }

        public IWebElement GetParentElement(bool is_from_dropdown)
        {
            IWebElement parentElement;
            if (is_from_dropdown)
                parentElement = this.GetWrappedControl().FindElement(By.XPath("../.."));
            else
                parentElement = this.GetWrappedControl().FindElement(By.XPath(".."));

            if (parentElement != null)
                return parentElement;

            return this.GetWrappedControl();
        }

        /// <summary>
        /// Update the current value to find of this control to new value to find
        /// </summary>
        /// <param name="newValueToFind"></param>
        public void UpdateValueToFind(string newValueToFind)
        {
            this.ValueToFind = newValueToFind;
        }

        /// <summary>
        /// Update the current find type to new findtype
        /// </summary>
        /// <param name="newFindType"></param>
        public void UpdateFindType(FindType newFindType)
        {
            this.FindType = newFindType;
        }
    }
}
