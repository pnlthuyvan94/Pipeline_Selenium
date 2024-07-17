using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Pathway.House
{
    public partial class HousePage : DashboardContentPage<HousePage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Pathway\HouseParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }
        public IQueryable<Row> TestData_RT01097 { get; set; }
        public HousePage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            TestData_RT01097 = ExcelHelper.GetAllRows("RT_01097");
            MetaData = ExcelHelper.GetAllRows("House_OR");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 7);

        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 8);

        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid HousePage_Grid => new Grid(_grid, _gridLoading);

        private string _floorPlan => ExcelFactory.GetRow(MetaData, 1)["Value To Find"];
        private string _exterior => ExcelFactory.GetRow(MetaData, 2)["Value To Find"];
        private string _interior => ExcelFactory.GetRow(MetaData, 3)["Value To Find"];
        private string _media => ExcelFactory.GetRow(MetaData, 4)["Value To Find"];

        private Row _update => ExcelFactory.GetRow(MetaData, 5);
        protected Button Update_btn => new Button(_update);

    }
}
