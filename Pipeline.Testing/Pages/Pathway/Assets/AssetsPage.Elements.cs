using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Pathway.Assets.AssetsDetail;
using System.Linq;

namespace Pipeline.Testing.Pages.Pathway.Assets
{
    public partial class AssetsPage : DashboardContentPage<AssetsPage>
    {
        public IQueryable<Row> MetaData { get; set; }
        public IQueryable<Row> TestData_RT_01098 { get; set; }
        public AssetsDetailPage AssetsDetailPage { get; private set; }

        public AssetsPage() : base()
        {
            string _pathExcelFile = BaseFolderURL + @"\DataInputFiles\Pathway\AssetParams.xlsx";
            ExcelHelper = new ExcelFactory(_pathExcelFile);

            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("Assets_OR");
            TestData_RT_01098 = ExcelHelper.GetAllRows("RT_01098");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 7);

        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 8);

        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid Assets_Grid => new Grid(_grid, _gridLoading);

    }
}
