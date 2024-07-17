using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Common.Controls
{
    public class RadioButton : BaseControl
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public RadioButton(FindType findType, string valueToFind) : base(findType, valueToFind) { }
        public RadioButton(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }
        public RadioButton(Row row) : base(row) { }
        public RadioButton(IWebElement element) : base(element) { }

        private ref IWebElement _wrappedControl => ref this.wrappedControl;

        public void Click(bool isCapture = true) {
            try { this.GetWrappedControl(); } catch { }
            CommonHelper.click_element(_wrappedControl, isCapture);
        }

        

    }
}
