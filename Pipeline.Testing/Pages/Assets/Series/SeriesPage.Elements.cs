using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Assets.Series.AddSeries;


namespace Pipeline.Testing.Pages.Assets.Series
{
    public partial class SeriesPage : DashboardContentPage<SeriesPage>
    {
        public AddSeriesModal AddSeriesModal { get; private set; }

        //public IQueryable<Row> MetaData { get; set; }
        public SeriesPage() : base()
        {
        }

        protected static string _loadingGifRow => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSeries']/div[1]";
        protected IGrid Series_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgSeries_ctl00", _loadingGifRow);

        protected Textbox EditSeriesName_txt => new Textbox(FindType.XPath, "//*[contains(@id,'txtNameEdit')]");
        protected Textbox EditCode_txt => new Textbox(FindType.XPath, "//*[contains(@id,'txtCodeEdit')]");
        protected Textbox EditDescription_txt => new Textbox(FindType.XPath, "//*[contains(@id,'txtDescriptionEdit')]");
        protected Button UpdateSeries_btn => new Button(FindType.XPath, "//*[contains(@id,'UpdateButton') and @title='Update']");
        protected Button CancelSeries_btn => new Button(FindType.XPath, "//*[contains(@id,'CancelButton') and @title='Cancel']");
    }
}
