using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Common.Controls
{
    public class CheckBox : BaseControl
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public CheckBox(FindType findType, string valueToFind) : base(findType, valueToFind) { }
        public CheckBox(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds) { }
        public CheckBox(Row row) : base(row) { }
        public CheckBox(IWebElement element) : base(element) { }

        public void Check(bool isCapture = true) {
            if (CommonHelper.is_valid(this.GetWrappedControl()) && !this.GetWrappedControl().Selected) {
                if (isCapture)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(this.GetWrappedControl()), "Checkbox will be selected by click after screenshotting the element...");
                CommonHelper.click_element(this.GetWrappedControl(), isCapture);
            } else {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Checkbox was unable to be selected because the element is invalid or already selected");
            }
        }

        public void UnCheck(bool isCapture = true) {
            if (CommonHelper.is_valid(this.GetWrappedControl()) && this.GetWrappedControl().Selected) {
                if (isCapture)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(this.GetWrappedControl()), "Checkbox will be deselected by click after screenshotting the element...");
                CommonHelper.click_element(this.GetWrappedControl());
            } else {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Checkbox was unable to be deselected because the element is invalid or already unselected");
            }
        }

        public bool IsChecked
        {
            get
            {
                return GetWrappedControl().Selected;
            }
        }

        public void SetCheck(bool checkedValue, bool isCapture = true)
        {
            if (checkedValue)
            {
                Check(isCapture);
            }
            else
                UnCheck(isCapture);
        }

    }
}
