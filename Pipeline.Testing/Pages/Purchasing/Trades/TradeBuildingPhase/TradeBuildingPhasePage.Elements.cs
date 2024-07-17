using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase.AddPhaseToTrade;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Purchasing.Trades.TradeBuildingPhase
{
    public partial class TradeBuildingPhasePage : DetailsContentPage<TradeBuildingPhasePage>
    {
        public readonly IQueryable<Row> MetaData;
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\BuildingTradeParams.xlsx";
        public TradeBuildingPhasePage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("BuildingTrade_OR");
        }

        public AddPhaseToTradeModal AddPhaseToTradeModal { get; private set; }

        private Row _grid => ExcelFactory.GetRow(MetaData, 15);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 17);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected Grid BuildingPhase_Grid => new Grid(_grid, _gridLoading);

        private Row _add => ExcelFactory.GetRow(MetaData, 16);
        protected Button ShowModal_btn => new Button(_add);
        protected CheckBox SelectAll_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_rgPhases_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox");
        protected Button BulkActions_btn => new Button(FindType.XPath, "//*[@id='bulk-actions1']");
        protected Button RemoveSelectedPhases_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbDeleteSelectedBuildingPhase");
    }

}
