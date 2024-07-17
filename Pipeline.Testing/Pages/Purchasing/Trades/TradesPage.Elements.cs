using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.Trades.AddTrade;
using Pipeline.Testing.Pages.Purchasing.Trades.EditTrade;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Purchasing.Trades
{
    public partial class TradesPage : DashboardContentPage<TradesPage>
    {
        public AddTradeModal AddTradeModal { get; private set; }
        public EditTradeModal EditTradeModal { get; private set; }
        public TradesExportModal TradesExportModal { get; private set; }
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\BuildingTradeParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }

        public TradesPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("BuildingTrade_OR");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 5);

        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 6);

        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected Grid Trades_Grid => new Grid(_grid, _gridLoading);

        private Row _vendorAssignments => ExcelFactory.GetRow(MetaData, 32);
        protected Button VendorAssignments_btn => new Button(_vendorAssignments);
    }
}
