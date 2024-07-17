using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using OpenQA.Selenium.Interactions;

namespace Pipeline.Common.Controls
{
    public class Button : BaseControl
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static ref IWebDriver _driver => ref BaseValues.DriverSession;

        private ref IWebElement _wrappedControl => ref this.wrappedControl;

        public Button(FindType findType, string valueToFind) : base(findType, valueToFind) { }
        public Button(IWebElement element) : base(element) { }
        public Button(Row row) : base(row) { }
        public Button(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }

        public void Click(bool isCaptured = true) {
            try { this.GetWrappedControl(); } catch {}
            CommonHelper.click_element(_wrappedControl, isCaptured);
        }
    }
}
