

using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.Estimating
{
    public partial class EstimatingPage
    {
        public bool VerifySettingEstimatingPageIsDisplayed()
        {
            if (CurrentURL.EndsWith("/Dashboard/Products/Settings/Default.aspx"))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Setting Estimating page is displayed.</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Setting Estimating page is displayed wrong.</font>");
                return false;
            }
        }
    }
}
