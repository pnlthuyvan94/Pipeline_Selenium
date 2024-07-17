using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Jobs.Job.Import
{
    public partial class JobImportPage : DashboardContentPage<JobImportPage>
    {
        protected Label Import_lbl => new Label(FindType.XPath, "//h1[text()='Import Job Quantities']");
        protected Button NoOptionSpecified_btn => new Button(FindType.XPath, "//label[text()='No Options Specified']");
        protected Button OptionSpecified_btn => new Button(FindType.XPath, "//label[text()='Options Specified']");

        protected Textbox Select_txt => new Textbox(FindType.XPath, "//*[contains(@id, 'ctl00_CPH_Content_RadAsyncUpload1file')]");
        protected Button ProcessFile_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbProcessFileAndConfiguration']");
        protected Button FinishImport_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbFinishJobImportProcess']");
        protected Button DeleteSelectedFile_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_lbDeletePanel']");
    }
}
