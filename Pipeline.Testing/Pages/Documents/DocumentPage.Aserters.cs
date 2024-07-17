using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Documents
{
    public partial class DocumentPage
    {
        public bool IsDocumentsPageDisplay()
        {
            if (SelectDocument_btn.IsNull())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"No display {SelectDocument_btn.Title} button");
                return false;
            }
            return true;
        }
        public string DocumentFileHref { get; private set; }
        public string IsFileHref(string filename)
        {
            string xpath = $"//table[contains(@id,'_rgDocuments')]/tbody/tr/td/span[text()='{filename}']//..//..//following-sibling::td[2]/div/a";
            Label item = new Label(FindType.XPath, xpath);

            System.Threading.Thread.Sleep(5000);
            if (item.IsNull())
                return null;
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"Item is displayed on screen");

            // wait to get href
            System.Threading.Thread.Sleep(2000);
            DocumentFileHref = item.GetAttribute("href");
            return DocumentFileHref;
        }

    }
}
