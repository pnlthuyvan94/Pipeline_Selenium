using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail
{
    public partial class CustomOptionDetailPage
    {
        /// <summary>
        /// Verify head title and id of new manufacturer 
        /// </summary>
        /// <param name="CustomOptionName"></param>
        /// <returns></returns>
        public bool IsSaveCustomOptionSuccessful(string CustomOptionName)
        {
            System.Threading.Thread.Sleep(1000);
            if (SubHeadTitle().Equals(CustomOptionName))
                return true;
            else
            {
                ExtentReportsHelper.LogInformation("Current screen.");
                return !CurrentURL.EndsWith("did=0");
            }
        }

        /// <summary>
        /// Verify the CustomOption save data correct or not
        /// </summary>
        /// <param name="CustomOptionData"></param>
        /// <returns></returns>
        public bool IsSaveCustomOptionData(CustomOptionData CustomOptionData)
        {
            if (CustomOptionData.Code != COCode_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {CustomOptionData.Code}.Actual result: {COCode_txt.GetValue()}");
                return false;
            }
            if (CustomOptionData.Description != CODescription_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {CustomOptionData.Description}.Actual result: {CODescription_txt.GetValue()}");
                return false;
            }
            if (CustomOptionData.Structural != Structural_chk.IsChecked)
            {
                var expected = "check";
                if (!CustomOptionData.Structural) expected = "uncheck";
                var actual = "check";
                if (!Structural_chk.IsChecked) actual = "uncheck";

                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {expected}.Actual result: {actual}");
                return false;
            }
            if (Math.Round(CustomOptionData.Price, 2).ToString("0.00") != Price_txt.GetValue() && CustomOptionData.Price.ToString() != Price_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: {CustomOptionData.Price}.Actual result: {Price_txt.GetValue()}");
                return false;
            }
            return true;
        }
    }
}
