using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using System;
using OpenQA.Selenium.Support.UI;
using LinqToExcel;
using Pipeline.Common.Constants;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Common.Utils
{
    public class FindElementHelper
    {
        private readonly static log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static readonly Lazy<FindElementHelper> _lazy = new Lazy<FindElementHelper>(() => new FindElementHelper());
        public static FindElementHelper Instance() { return _lazy.Value; }

        private int timeoutSeconds = 5;
        private DefaultWait<IWebDriver> wait;

        #region "For this project only"
        /// <summary>
        /// Using "Row" to find element with "Row" contains 2 column "Field Find Type" and "Value To Find"
        /// </summary>
        /// <param name="row"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IWebElement FindElement(Row row, int timeoutInSeconds = 5)
        {
            FindType findElementType;
            switch (row[BaseConstants.FindType])
            {
                case "Id":
                    findElementType = FindType.Id;
                    break;
                default:
                    findElementType = FindType.XPath;
                    break;
            }
            return FindElement(findElementType, row[BaseConstants.ValueToFind], timeoutInSeconds);
        }

        /// <summary>
        /// Using "Row" to find elements with "Row" contains 2 column "Field Find Type" and "Value To Find"
        /// </summary>
        /// <param name="row"></param>
        /// <param name="timeoutInSeconds"></param>
        /// <returns></returns>
        public IEnumerable<IWebElement> FindElements(Row row, int timeoutInSeconds = 5)
        {
            FindType findElementType;
            switch (row[BaseConstants.FindType])
            {
                case "Id":
                    findElementType = FindType.Id;
                    break;
                default:
                    findElementType = FindType.XPath;
                    break;
            }
            return FindElements(findElementType, row[BaseConstants.ValueToFind], timeoutInSeconds);
        }
        #endregion

        public void SetTimeoutSeconds(int timeoutSeconds)
        {
            this.timeoutSeconds = timeoutSeconds;
        }

        private void SetWait(int timeoutSeconds)
        {
            SetTimeoutSeconds(timeoutSeconds);
            if (this.timeoutSeconds != 5)
            {
                wait = new DefaultWait<IWebDriver>(BaseValues.DriverSession)
                {
                    Timeout = TimeSpan.FromSeconds(timeoutSeconds),
                    PollingInterval = TimeSpan.FromMilliseconds(100)
                };
                wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
                //wait.Message = "Element to be searched not found";
            }
        }

        internal void SetDefaultWait()
        {
            wait = new DefaultWait<IWebDriver>(BaseValues.DriverSession)
            {
                Timeout = TimeSpan.FromSeconds(5),
                PollingInterval = TimeSpan.FromMilliseconds(250)
            };
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            //wait.Message = "Element to be searched not found";
        }

        public IWebElement FindElement(FindType findElementType, string valueToFind, int timeoutSeconds)
        {
            SetWait(timeoutSeconds);
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElement(By.Name(valueToFind));
                case FindType.XPath:
                    return FindElement(By.XPath(valueToFind));
                case FindType.Id:
                    return FindElement(By.Id(valueToFind));
                default:
                    return null;
            }
        }

        public IWebElement FindElement(FindType findElementType, string valueToFind)
        {
            SetWait(8);
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElement(By.Name(valueToFind));
                case FindType.XPath:
                    return FindElement(By.XPath(valueToFind));
                case FindType.Id:
                    return FindElement(By.Id(valueToFind));
                default:
                    return null;
            }
        }

        //TODO: implement later
        internal IWebElement FindElement(IWebElement element, FindType findElementType, string valueToFind)
        {
            SetWait(5);
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElement(element, By.Name(valueToFind));
                case FindType.XPath:
                    return FindElement(element, By.XPath(valueToFind));
                case FindType.Id:
                    return FindElement(element, By.Id(valueToFind));
                default:
                    return null;
            }
        }

        //TODO: implement later
        internal IList<IWebElement> FindElements(IWebElement element, FindType findElementType, string valueToFind)
        {
            SetWait(5);
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElements(element, By.Name(valueToFind));
                case FindType.XPath:
                    return FindElements(element, By.XPath(valueToFind));
                case FindType.Id:
                    return FindElements(element, By.Id(valueToFind));
                default:
                    return null;
            }
        }

        public IList<IWebElement> FindElements(FindType findElementType, string valueToFind, int timeoutSeconds)
        {
            SetWait(timeoutSeconds);
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElements(By.Name(valueToFind));
                case FindType.XPath:
                    return FindElements(By.XPath(valueToFind));
                case FindType.Id:
                    return FindElements(By.Id(valueToFind));
                default:
                    return null;
            }
        }

        public IList<IWebElement> FindElements(FindType findElementType, string valueToFind)
        {
            SetWait(5);
            switch (findElementType)
            {
                case FindType.Name:
                    return FindElements(By.Name(valueToFind));
                case FindType.XPath:
                    return FindElements(By.XPath(valueToFind));
                case FindType.Id:
                    return FindElements(By.Id(valueToFind));
                default:
                    return null;
            }
        }

        private IWebElement FindElement(IWebElement element, By by)
        {
            if (element is null)
            {
                Log.Error("The parent element is null so the child element could not be found.");
                throw new NoSuchElementException("The parent element is null so the child element could not be found.");
            }
            try
            {
                return wait.Until(x => element.FindElement(by));
            }
            catch (TimeoutException e)
            {
                Log.Warn($"The element could not be found after {timeoutSeconds} seconds. " + e.Message);
                return null;
            }
        }

        private IWebElement FindElement(By by)
        {
            try
            {
                return wait.Until(x => x.FindElement(by));
            }
            catch (WebDriverTimeoutException e)
            {
                Log.Warn($"The element could not be found after {timeoutSeconds} seconds. " + e.Message);
                return null;
            }
        }

        private IList<IWebElement> FindElements(IWebElement element, By by)
        {
            try
            {
                return wait.Until(x => element.FindElements(by)).ToList();
            }
            catch (WebDriverTimeoutException e)
            {
                Log.Warn($"The elements could not be found after {timeoutSeconds} seconds. " + e.Message);
                return null;
            }
        }

        private IList<IWebElement> FindElements(By by)
        {
            try
            {
                return wait.Until(x => x.FindElements(by)).ToList();
            }
            catch (WebDriverTimeoutException e)
            {
                Log.Warn($"The elements could not be found after {timeoutSeconds} seconds. " + e.Message);
                return null;
            }
        }
    }
}
