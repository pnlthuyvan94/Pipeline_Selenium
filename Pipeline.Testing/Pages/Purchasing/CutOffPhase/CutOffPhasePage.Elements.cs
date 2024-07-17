using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.CutoffPhase.AddCutoffPhase;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase
{
    public partial class CutoffPhasePage : DashboardContentPage<CutoffPhasePage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\CutoffPhaseParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }
        public AddCutoffPhaseModal AddCutoffPhaseModal { get; private set; }
        public IQueryable<Row> TestData_RT01159 { get; private set; }

        public CutoffPhasePage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("CutOffPhase_OR");
            TestData_RT01159 = ExcelHelper.GetAllRows("RT_01159");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 5);

        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 6);

        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid CutOffPhase_Grid => new Grid(_grid, _gridLoading);

        private Row _buildingPhase => ExcelFactory.GetRow(MetaData, 8);
        public Label BuildingPhases_lbl => new Label(_buildingPhase);
    }
}
