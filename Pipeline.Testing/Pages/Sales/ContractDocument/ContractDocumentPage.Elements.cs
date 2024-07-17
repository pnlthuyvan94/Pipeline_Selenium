using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Sales.ContractDocument.AddContractDocument;
using System.Linq;

namespace Pipeline.Testing.Pages.Sales.ContractDocument
{
    public partial class ContractDocumentPage : DashboardContentPage<ContractDocumentPage>
    {
        public IQueryable<Row> MetaData { get; set; }
        public IQueryable<Row> TestData_RT_01104 { get; set; }
        public ContractDocumentDetailPage ContractDocumentDetail { get; private set; }
        private string _pathUploadedFile;

        public ContractDocumentPage() : base()
        {
            _pathUploadedFile = BaseFolderURL + @"\DataInputFiles\Sales\SalesContract.doc";
            string _pathExcelFile = BaseFolderURL + @"\DataInputFiles\Sales\ContractDocumentParams.xlsx";
            ExcelHelper = new ExcelFactory(_pathExcelFile);

            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("ContractDocument_OR");
            TestData_RT_01104 = ExcelHelper.GetAllRows("RT_01104");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 7);

        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 8);

        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid ContractDocument_Grid => new Grid(_grid, _gridLoading);

    }
}
