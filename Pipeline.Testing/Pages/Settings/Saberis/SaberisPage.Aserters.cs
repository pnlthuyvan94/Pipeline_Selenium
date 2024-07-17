using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.Saberis
{
    public partial class SaberisPage
    {
        public string ProcessedFileHref { get; private set; }
        public bool IsProcessedFileExisted(string filename)
        {
            string xpath = $"/html/body/div[2]/div/div/table/tbody/tr/td[5]/a[text()='{filename}']";
            Label item = new Label(FindType.XPath, xpath);
            if (item.IsNull())
                return false;
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"Item is displayed on screen");
            ProcessedFileHref = item.GetAttribute("href");
            return item.IsDisplayed();
        }

        public bool IsFileUploaded(string date, string viewBy)
        {
            string xpath = $"/html/body/div[2]/div/div/table/tbody/tr/td[./preceding-sibling::td[contains(text(),'{date}')] and position()=4]/a[contains(text(),'{viewBy}')]";
            Label item = new Label(FindType.XPath, xpath);
            if (item.IsNull())
                return false;
            if (item.GetText().Contains(viewBy))
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"Item is displayed on screen");
                ProcessedFileHref = item.GetAttribute("href");
                return true;
            }
            return false;
        }
    }
}
