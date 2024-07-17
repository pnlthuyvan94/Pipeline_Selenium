using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Costing.Vendor
{
    public partial class VendorPage
    {
        public bool VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage()
        {
            bool isDisplayed = SyncToBuildPro.WaitForElementIsVisible(5, false);
            if (isDisplayed)
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(SyncToBuildPro), "The Sync Vendors to BuildPro button is displayed on page");
            else
                ExtentReportsHelper.LogFail("The Sync Vendors to BuildPro button is <font color='green'><b>NOT</b></font> displayed on page");
            return isDisplayed;
        }

        public bool VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage()
        {
            bool isDisplayed = SyncToBuildPro.WaitForElementIsVisible(5, false);
            if (!isDisplayed)
                ExtentReportsHelper.LogPass("The Sync Vendors to BuildPro button is NOT displayed on page as expected");
            else
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SyncToBuildPro), "The Sync Vendors to BuildPro button is <font color='green'><b>displayed</b></font> on page");
            return !isDisplayed;
        }

        public bool VerifyTheVendorSyncedSuccessfully(VendorData data)
        {
            string xpath = $"//*[@id='ctl00_CPH_Content_BuildProSyncModal_autoGrid_rgResults_ctl00']/tbody/tr[./td[.='The Vendor \"{data.Name}\"  has been received by BuildPro.'] and ./td[.='{data.Code}']]";
            SpecificControls control = new SpecificControls(FindType.XPath, xpath);
            if (control.WaitForElementIsVisible(5, false))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(control), $"The Vendor <font color='green'><b><i>\"{data.Code} - {data.Name}\"</font></b></i> has been received by BuildPro.");
                return true;
            }
            ExtentReportsHelper.LogPass($"The Vendor <font color='green'><b><i>\"{data.Code} - {data.Name}\"</font></b></i> has NOT been received by BuildPro.");
            return false;
        }
    }
}
