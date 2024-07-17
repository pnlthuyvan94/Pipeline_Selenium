using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail
{
    public partial class WorksheetDetailPage : DetailsContentPage<WorksheetDetailPage>
    {
        protected Textbox WorksheetName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtWSName']");

        protected Textbox WorksheetCode_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtWSCode']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

    }

}
