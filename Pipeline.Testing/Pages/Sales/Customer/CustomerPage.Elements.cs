using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Sales.Customer
{
    public partial class CustomerPage : DashboardContentPage<CustomerPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Sales\CustomerParams.xlsx";
        public IQueryable<Row> MetaData { get; set; }

        public CustomerPage() 
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            MetaData = ExcelHelper.GetAllRows("Customer_OR");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 1);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 2);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid Customer_Grid => new Grid(_grid, _gridLoading);
    }
}
