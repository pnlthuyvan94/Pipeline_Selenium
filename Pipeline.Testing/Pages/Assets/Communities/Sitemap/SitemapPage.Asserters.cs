using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities.Sitemap
{
    public partial class SitemapPage
    {
        public bool IsUploadGridDisplay()
        {
            // True: The Choose File button is displayed and description is display correctly
            return !DisplayFileName_txt.IsNull() && Description_lbl.GetText().Contains("Upload a .pdf or an image");
        }

        public bool IsUploadFileSuccessful(string fileName)
        {
            string uploadFileName = UploadFileName123_lbl.GetText();
            // True: The upload file and remove button are displayed 
            return uploadFileName == fileName && !Remove_btn.IsNull();
        }

        public bool IsRemoveFileSuccessful()
        {
            Remove_btn.WaitForElementIsInVisible(5);

            string uploadFileName = UploadFileName123_lbl.GetText();
            // True: The upload file name is removed 
            return Remove_btn.IsNull() || uploadFileName == "No file assigned yet";
        }

        public bool IsSitemapPageDisplay()
        {
            // True: There are no files uploaded and don't display Choose File button
            string uploadFileName = UploadFileName123_lbl.GetText();

            if (uploadFileName != "No file assigned yet" || !DisplayFileName_txt.IsNull())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: The upload file name should be empty and the choose file button isn't displayes"
                    + $"<br>Actual result: Display upload file name:  {uploadFileName} or choose file button isn't empty.");
                return false;
            }
            return true;
        }
    }
}
