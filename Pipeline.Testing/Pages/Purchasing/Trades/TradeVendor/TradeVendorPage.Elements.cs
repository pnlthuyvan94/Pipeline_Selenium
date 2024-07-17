using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor.AddVendorToTrade;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeVendor
{
    public partial class TradeVendorPage : DashboardContentPage<TradeVendorPage>
    {       
        public readonly IQueryable<Row> MetaData;
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\BuildingTradeParams.xlsx";
        public TradeVendorPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("BuildingTrade_OR");
        }
        public AddVendorToTradeModal AddVendorToTradeModal { get; private set; }

        private Row _grid => ExcelFactory.GetRow(MetaData, 18);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 20);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected Grid Vendor_Grid => new Grid(_grid, _gridLoading);

        private Row _add => ExcelFactory.GetRow(MetaData, 19);
        protected Button ShowModal_btn => new Button(_add);
        private Row _deleteSelected => ExcelFactory.GetRow(MetaData, 36);
        protected Button DeleteSelectedVendor_Btn => new Button(_deleteSelected);
        private Row _bulkAction => ExcelFactory.GetRow(MetaData, 37);
        protected Button BulkAction_Btn => new Button(_bulkAction);
        protected CheckBox SelectAll_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_rgVendors_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox");
        protected Button BulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions1']");
        protected Button RemoveSelectedVendors_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbDeleteSelectedVendors");
    }

}
