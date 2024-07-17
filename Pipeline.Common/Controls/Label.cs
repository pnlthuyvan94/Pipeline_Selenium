using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Common.Controls
{
    public class Label : BaseControl
    {
        public Label(FindType findType, string valueToFind) : base(findType, valueToFind) { }
        public Label(IWebElement webElement) : base(webElement) { }
        public Label(Row row) : base(row) { }
        public Label(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }
        public Label(string findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }
        private ref IWebElement _wrappedControl => ref this.wrappedControl;
        public void Click(bool isCaptured = true)
        {
            try { this.GetWrappedControl(); } catch { }
            CommonHelper.click_element(_wrappedControl, isCaptured);
        }

    }
}
