using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Pathway.DesignerElement.AddElement;
using Pipeline.Testing.Pages.Pathway.DesignerElement.AddElementType;
using System.Linq;

namespace Pipeline.Testing.Pages.Pathway.DesignerElement
{
    public partial class DesignerElementPage : DashboardContentPage<DesignerElementPage>
    {
        public AddElementModal AddElementModal { get; private set; }
        public AddElementTypeModal AddElementTypeModal { get; private set; }
        public IQueryable<Row> TestData_RT01101 { get; private set; }
        public IQueryable<Row> MetaData { get; set; }
        public DesignerElementPage() : base()
        {
            string _pathExcelFile = BaseFolderURL + @"\DataInputFiles\Pathway\DesignElementParams.xlsx";
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("DesignElement_OR");
            TestData_RT01101 = ExcelHelper.GetAllRows("RT_01101");
        }


        private Row _grid => ExcelFactory.GetRow(MetaData, 11);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 12);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid ElementType_grid => new Grid(_grid, _gridLoading);


        private Row _grid2 => ExcelFactory.GetRow(MetaData, 13);
        private Row _loadingGifRow2 => ExcelFactory.GetRow(MetaData, 14);
        private string _gridLoading2 => _loadingGifRow2[BaseConstants.ValueToFind];

        protected IGrid DesignerElement_grid => new Grid(_grid2, _gridLoading2);
    }
}
