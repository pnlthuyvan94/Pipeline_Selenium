using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Common.Controls
{
    public class Image : BaseControl
    {
        public Image(FindType findType, string valueToFind) : base(findType, valueToFind) { }
        public Image(IWebElement webElement) : base(webElement) { }
        public Image(Row row) : base(row) { }
        public Image(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }
        public Image(string findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }
        private ref IWebElement _wrappedControl => ref this.wrappedControl;
        public void Click(bool isCaptured = true)
        {
            try { this.GetWrappedControl(); } catch { }
            CommonHelper.click_element(_wrappedControl, isCaptured);
        }

    }
}
