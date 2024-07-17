using NUnit.Framework;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.CustomField
{
    public partial class CustomFieldModal
    {
        public bool VerifyTitleFieldWithMessage(string expected)
        {
            ExtentReportsHelper.LogInformation(null, "<b>***Verify the validation message of Title field.***</b>");
            Save_btn.Click();
            if (TitleErrorField.GetText().Equals(expected))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(TitleErrorField), $"The error of Title message is displayed as expected.");
                return true;
            }
            ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(TitleErrorField), $"The error of Title message is NOT displayed as expected.<br><b>Actual </b>{TitleErrorField.GetText()}<br><b>Expected: </b>{expected}");
            return false;
        }

        public bool VerifyTagFieldWithMessage(string expected)
        {
            ExtentReportsHelper.LogInformation(null, "<b>***Verify the validation message of Tag field.***</b>");
            Save_btn.Click();
            if (TagErrorField.GetText().Equals(expected))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(TagErrorField), "The error of Tag message is displayed as expected.");
                return true;
            }
            ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(TagErrorField), $"The error of Tag message is NOT displayed as expected.<br><b>Actual </b>{TitleErrorField.GetText()}<br><b>Expected: </b>{expected}");
            return false;
        }

        /// <summary>
        /// Verify the Custom Field is not allow to create duplicate Tag and Title
        /// </summary>
        /// <param name="data"></param>
        /// <param name="grid"></param>
        public void VerifyCannotCreateWithDuplicateValue(CustomFieldData data, IGrid grid, string LoadingGrid_xpath)
        {
            ExtentReportsHelper.LogInformation(null, "<b>***Verify the Custom Field does not allow CREATE with duplicate value.***</b>");
            CreateNewCustomField(data,LoadingGrid_xpath);
            grid.WaitGridLoad();
            string actualMsg = GetLastestToastMessage();
            if (!actualMsg.Equals("Custom Field Type could not be added. Title and Tag Value must be unique."))
            {
                ExtentReportsHelper.LogFail($"Able to create a Custom Field with duplicate Tag. Actual message: <i>{actualMsg}</i>");
                CloseToastMessage();
                //Assert.Fail();
                //CloseModal_btn.Click();
            }
            else
            {
                ExtentReportsHelper.LogPass("Custom Field Type could not be added as expectation.");
                CloseToastMessage();
                System.Threading.Thread.Sleep(2000);
            }
        }

        public void VerifyCannotUpdateWithDuplicateValue(string oldTitle, CustomFieldData data, IGrid grid, string LoadingGrid_Xpath)
        {
            ExtentReportsHelper.LogInformation(null, "<b>***Verify the Custom Field does not allow UPDATE with duplicate value.***</b>");
            UpdateCustomField(grid, LoadingGrid_Xpath, oldTitle, data);
            WaitingLoadingGifByXpath(LoadingGrid_Xpath);
            string actualMsg = GetLastestToastMessage();
            if (!actualMsg.Equals("Custom Field Type could not be added. Title and Tag Value must be unique."))
            {
                ExtentReportsHelper.LogFail($"Able to create a Custom Field with duplicate Tag. Actual message <i>{actualMsg}</i>");
                CloseToastMessage();
                //Assert.Fail();
               // CloseModal_btn.Click();
            }
            else
            {
                ExtentReportsHelper.LogPass("Custom Field Type could not be added as expectation.");
                CloseToastMessage();
                //CloseModal_btn.Click();
            }
        }

    }
}
