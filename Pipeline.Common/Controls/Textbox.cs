using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Common.Controls
{
    public class Textbox : BaseControl
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public Textbox(FindType findType, string valueToFind) : base(findType, valueToFind) { }
        public Textbox(IWebElement webElement) : base(webElement) { }
        public Textbox(Row row) : base(row) { }
        public Textbox(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }

        /// <summary>
        /// Append key to current textbox
        /// </summary>
        /// <param name="value"></param>
        public void AppendKeys(string value)
        {
            string message = $"Input value <font color='green'><b>'{value}'</b></font> to field <font color='green'><b>'{GetWrappedControl().TagName}'</b></font>.";
            try
            {
                Log.Info($"Send value {value} to the element [{FindType.ToString("g")}|{ValueToFind}]");

                CommonHelper.MoveToElement(GetWrappedControl(), true);

                GetWrappedControl().SendKeys(value);

                BasePage.JQueryLoad();
                System.Threading.Thread.Sleep(667);

                //CaptureAndLog(message);
            }
            catch (StaleElementReferenceException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] is out of date. " + e.Message);
                throw new StaleElementReferenceException("The element is out of date.");
            }
            catch (ElementNotInteractableException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] does not interactable. " + e.Message);
                throw new ElementNotInteractableException("The element does not interactable(hidden or disable or something ...)");
            }
            catch (NoSuchElementException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] does not exist on DOM. " + e.Message);
                throw new NoSuchElementException("The element could not be found.");
            }
        }

        public void Clear()
        {
            Log.Info($"Clear value to the element [{FindType.ToString("g")}|{ValueToFind}]");
            GetWrappedControl().Clear();
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
            //CaptureAndLog(string.Format(BaseConstants.ClearValueMessage));
        }

        internal void SetTextWithoutCapture(string value)
        {
            try
            {
                CommonHelper.MoveToElement(GetWrappedControl(), true);

                Log.Info($"Send value {value} to the element [{FindType.ToString("g")}|{ValueToFind}]");
                GetWrappedControl().Clear();
                System.Threading.Thread.Sleep(300);
                GetWrappedControl().SendKeys(value);
                System.Threading.Thread.Sleep(500);
            }
            catch (StaleElementReferenceException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] is out of date. " + e.Message);
                throw new StaleElementReferenceException("The element is out of date.");
            }
            catch (ElementNotInteractableException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] does not interactable. " + e.Message);
                throw new ElementNotInteractableException("The element does not interactable(hidden or disable or something ...)");
            }
            catch (NoSuchElementException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] does not exist on DOM. " + e.Message);
                throw new NoSuchElementException("The element could not be found.");
            }
        }

        public void SetText(string value)
        {
            try
            {
                CommonHelper.MoveToElement(GetWrappedControl(), true);

                string message = $"Input value <font color='green'><b>'{value}'</b></font> to field <font color='green'><b>'{GetWrappedControl().TagName}'</b></font>.";
                Log.Info($"Send value {value} to the element [{FindType.ToString("g")}|{ValueToFind}]");
                GetWrappedControl().Clear();
                System.Threading.Thread.Sleep(100);
                GetWrappedControl().SendKeys(value);
                System.Threading.Thread.Sleep(500);

                // Re-Send key again if miss.
                if (string.IsNullOrEmpty(GetWrappedControl().Text) && string.IsNullOrEmpty(GetWrappedControl().GetAttribute("value")))
                {
                    GetWrappedControl().Clear();
                    System.Threading.Thread.Sleep(100);
                    GetWrappedControl().SendKeys(value);
                    System.Threading.Thread.Sleep(200);
                }
                //CaptureAndLog(message);
            }
            catch (StaleElementReferenceException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] is out of date. " + e.Message);
                throw new StaleElementReferenceException("The element is out of date.");
            }
            catch (ElementNotInteractableException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] does not interactable. " + e.Message);
                throw new ElementNotInteractableException("The element does not interactable(hidden or disable or something ...)");
            }
            catch (NoSuchElementException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] does not exist on DOM. " + e.Message);
                throw new NoSuchElementException("The element could not be found.");
            }
        }

        public void SendKeysWithoutClear(string value)
        {
            try
            {
                CommonHelper.MoveToElement(GetWrappedControl(), true);

                string message = $"Input value <font color='green'><b>'{value}'</b></font> to field <font color='green'><b>'{GetWrappedControl().TagName}'</b></font>.";
                Log.Info($"Send value {value} to the element [{FindType.ToString("g")}|{ValueToFind}]");
                GetWrappedControl().SendKeys(value);
                //CaptureAndLog(message);
            }
            catch (StaleElementReferenceException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] is out of date. " + e.Message);
                throw new StaleElementReferenceException("The element is out of date.");
            }
            catch (ElementNotInteractableException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] does not interactable. " + e.Message);
                throw new ElementNotInteractableException("The element does not interactable(hidden or disable or something ...)");
            }
            catch (NoSuchElementException e)
            {
                Log.Error($"The element [{FindType.ToString("g")}|{ValueToFind}] does not exist on DOM. " + e.Message);
                throw new NoSuchElementException("The element could not be found.");
            }
        }
    }
}
