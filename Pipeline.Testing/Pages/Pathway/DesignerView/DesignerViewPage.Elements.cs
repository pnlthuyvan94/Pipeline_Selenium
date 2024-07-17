using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Testing.Pages.Pathway.DesignerView.AddDesignerView;

namespace Pipeline.Testing.Pages.Pathway.DesignerView
{
    public partial class DesignerViewPage : DashboardContentPage<DesignerViewPage>
    {

        public AddDesignerViewModal AddDesignerViewModal { get; private set; }

        public static IQueryable<Row> MetaData { get; set; }
        public DesignerViewPage() : base()
        {
            string _pathExcelFile = BaseFolderURL + @"\DataInputFiles\Pathway\DesignerViewParams.xlsx";
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("DesignerView");
        }


        private Row _grid => ExcelFactory.GetRow(MetaData, 6);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 7);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid CostCategory_Grid => new Grid(_grid, _gridLoading);
    }
}
