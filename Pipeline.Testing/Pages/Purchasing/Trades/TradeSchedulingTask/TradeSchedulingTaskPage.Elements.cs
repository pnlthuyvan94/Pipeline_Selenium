using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask.AddSchedulingTaskToTrade;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Purchasing.Trades.SchedulingTask
{
    public partial class TradeSchedulingTaskPage : DetailsContentPage<TradeSchedulingTaskPage>
    {
        public readonly IQueryable<Row> MetaData;
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\BuildingTradeParams.xlsx";
        public TradeSchedulingTaskPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("BuildingTrade_OR");
        }
        public AddSchedulingTaskToTradeModal AddSchedulingTaskToTradeModal { get; private set; }
        private Row _grid => ExcelFactory.GetRow(MetaData, 21);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 23);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected Grid SchedulingTask_Grid => new Grid(_grid, _gridLoading);

        private Row _add => ExcelFactory.GetRow(MetaData, 22);
        protected Button ShowModal_btn => new Button(_add);
    }

}
