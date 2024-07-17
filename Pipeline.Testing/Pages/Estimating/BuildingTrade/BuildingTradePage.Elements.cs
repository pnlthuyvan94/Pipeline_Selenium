using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BuildingTrade.AddBuildingTrade;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Estimating.BuildingTrade
{
    public partial class BuildingTradePage : DashboardContentPage<BuildingTradePage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Estimating\BuildingTradeParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }
        public AddBuildingTradeModal AddBuildingTradeModal { get; private set; }

        public BuildingTradePage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("BuildingTrade_OR");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 5);

        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 6);

        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid BuildingTrade_Grid => new Grid(_grid, _gridLoading);

    }
}
