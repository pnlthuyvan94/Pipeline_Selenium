using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Settings.Builder.Lot
{
    public partial class LotSettingPage : DetailsContentPage<LotSettingPage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Settings\Builder\LotParams.xlsx";

        //public IQueryable<Row> MetaData { get; set; }
        //public IQueryable<Row> TestData { get; set; }

        public LotSettingPage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            //// Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("Lot_OR");
            //TestData = ExcelHelper.GetAllRows("RT_01228");
        }

        //private Row switchView => ExcelFactory.GetRow(MetaData, 1);
        protected DropdownList SwitchView_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlViewType");

        //private Row loading => ExcelFactory.GetRow(MetaData, 2);
        string loadingXpathWhenSwitch = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]";

        //private Row newLotStatus => ExcelFactory.GetRow(MetaData, 3);
        protected Button AddNewLotStatus_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlLot']/div[2]/div/article/section/article/header/span/a[2]");

        //private Row lotStatusGrid => ExcelFactory.GetRow(MetaData, 4);
        private string lotStatusLoading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStatuses']/div[1]";
        protected IGrid LotStatus_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgStatuses_ctl00", lotStatusLoading);

        //private Row availableDropdowListOnSystemStatus => ExcelFactory.GetRow(MetaData, 5);
        protected DropdownList AvailableDropdowListOnSystemStatus_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_dlAvailableLotStatus");

        //private Row modalTitle_Row => ExcelFactory.GetRow(MetaData, 7);
        protected Label TitleModal_lbl => new Label(FindType.XPath, "//*[@id='sg-modal']/section/header/h1");

        //private Row lotStatus => ExcelFactory.GetRow(MetaData, 8);
        protected Textbox LotStatus_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtAddSName");

        //private Row save => ExcelFactory.GetRow(MetaData, 9);
        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertStatus");

        //private Row closeModal => ExcelFactory.GetRow(MetaData, 10);
        protected Button CloseModal_Btn => new Button(FindType.XPath, "//*[@id='sg-modal']/section/header/a");

        protected Button AcceptChange_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgStatuses_ctl00']/tbody/tr/td[2]/input[contains(@src,'accept')]");
    }
}
