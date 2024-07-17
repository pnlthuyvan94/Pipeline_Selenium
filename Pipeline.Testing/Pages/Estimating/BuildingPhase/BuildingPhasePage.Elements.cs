using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddBuildingPhase;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase
{
    public partial class BuildingPhasePage : DashboardContentPage<BuildingPhasePage>
    {
        private readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Estimating\BuildingPhaseParams.xlsx";
        public AddBuildingPhaseModal AddBuildingPhaseModal { get; private set; }
        public IQueryable<Row> TestData_RT01025;
        protected static IQueryable<Row> MetaData { get; set; }
        public BuildingPhasePage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("BuildingPhase_OR");
            TestData_RT01025 = ExcelHelper.GetAllRows("RT_01025");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 13);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 14);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];
        protected Button SyncToBuildPro => new Button(FindType.Id, "ctl00_CPH_Content_lbStartSync");
        protected IGrid BuildingPhase_Grid => new Grid(_grid, _gridLoading);
        protected Button StartSyncToBuildPro_Btn => new Button(FindType.Id, "ctl00_CPH_Content_BuildProSyncModal_lbBuildProIntegrationSync");
        protected Button BulkActionsButton => new Button(FindType.XPath,
           "//a[@id = 'bulk-actions' and @title = 'Bulk Actions']");
        protected Button DeleteSelectedButton => new Button(FindType.XPath,
            "//a[@id = 'bulk-actions' and @title = 'Bulk Actions']//following::li/a[@id = 'ctl00_CPH_Content_lbDeleteSelectedPhases']");
    }
}
