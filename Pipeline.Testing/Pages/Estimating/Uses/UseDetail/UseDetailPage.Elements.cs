using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Uses.UseDetail
{
    public partial class UseDetailPage : DetailsContentPage<UseDetailPage>
    {

        protected Label Titlt_lbl => new Label(FindType.XPath, "//h1[text()='Use']");

        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUseName']");

        protected Textbox Description_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUseDescription']");

        protected Textbox SortOrder_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUseSortOrder']");

        protected Button ManufacturerSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveUse']");

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlContent']/div[1]";

    }

}
