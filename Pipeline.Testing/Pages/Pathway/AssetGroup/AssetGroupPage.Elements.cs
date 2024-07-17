using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Pathway.AssetGroup.AddAssetGroup;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Pathway.AssetGroup
{
    public partial class AssetGroupPage : DashboardContentPage<AssetGroupPage>
    {
        private readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Pathway\AssetGroupParams.xlsx";
        public AddAssetGroupModal AddAssetGroupModal { get; private set; }

        public IQueryable<Row> MetaData { get; set; }
        public AssetGroupPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("AssetGroup_OR");
        }


        private Row _grid => ExcelFactory.GetRow(MetaData, 5);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 6);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid CostCategory_Grid => new Grid(_grid, _gridLoading);
    }
}
