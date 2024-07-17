using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Series.AddSeries
{
    public partial class AddSeriesModal : SeriesPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='series-modal']/section/header/h1");
        protected Textbox SeriesName_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSeriesName");

        protected Textbox SeriesCode_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSeriesCode");

        protected Textbox SeriesDescription_txt
            => new Textbox(FindType.Id, "ctl00_CPH_Content_txtSeriesDescription");

        protected Button SeriesSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSeries']");

        protected Button SeriesClose_btn
           => new Button(FindType.XPath, "//*[@id='series-modal']/section/header/a");

        protected Label ModalName_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_rfv2']");

    }

}
