using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.Purchasing
{
    public partial class PurchasingPage
    {
        public void Check_HideZeroCostItems(bool checkbox)
        {
            HideZeroCostItems_chk.SetCheck(checkbox);
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave']/div[1]");
            string actualMessage = GetLastestToastMessage();
            string expectMessage = "Settings Saved";
            if (actualMessage.Equals(expectMessage))
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Setting is saved successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Setting is saved unsuccessfully.</font>");
            }
        }

        public void SetDefaultJobBudgetView(string view)
        {
            DefaultJobBudgetView_ddl.SelectItem(view);
            Save_btn.Click();
            System.Threading.Thread.Sleep(3000);
        }
    }
}
