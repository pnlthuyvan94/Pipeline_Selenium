using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail
{
    public partial class WorksheetDetailPage
    {
        public WorksheetDetailPage EnterWorksheetName(string name)
        {
            WorksheetName_txt.SetText(name);
            return this;
        }

        public WorksheetDetailPage EnterWorksheetCode(string code)
        {
            WorksheetCode_txt.SetText(code);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            PageLoad();
            var loadingGif = FindElementHelper.FindElement(Common.Enums.FindType.XPath, "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]", 1);
            var timeout = 0;
            while (loadingGif != null && timeout < 100)
            {
                System.Threading.Thread.Sleep(100);
                timeout++;
            }
            PageLoad();

        }

    }
}
