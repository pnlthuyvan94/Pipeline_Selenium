using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.Estimating
{
    public partial class EstimatingPage
    {
        public void Check_Show_Subcomponent_Description(bool checkbox)
        {
            ShowSubcomponent_Description_chk.SetCheck(checkbox);
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave']/div[1]");

            string Save_expected = "Estimating Settings Updated";
            string Save_actual = GetLastestToastMessage();
            if (Save_expected.Equals(Save_actual))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Save information is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Save information is unsuccessfully. Actual message: <i>{Save_actual}</i></font>");
            }
        }
        public void Check_Show_Category_On_Product_Conversion(bool checkbox)
        {
            ShowCategory_ProductConversion_chk.SetCheck(checkbox);
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave']/div[1]");

            string Save_expected = "Estimating Settings Updated";
            string Save_actual = GetLastestToastMessage();
            if (Save_expected.Equals(Save_actual))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Save information is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Save information is unsuccessfully. Actual message: <i>{Save_actual}</i></font>");
            }
        }

        public void Check_Show_On_Add_Option_Product_Modal(bool checkbox)
        {
            ShowCategory_AddOptionProduct_chk.SetCheck(checkbox);
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave']/div[1]");

            string Save_expected = "Estimating Settings Updated";
            string Save_actual = GetLastestToastMessage();
            if (Save_expected.Equals(Save_actual))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Save information is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Save information is unsuccessfully. Actual message: <i>{Save_actual}</i></font>");
            }
        }
    }
}
