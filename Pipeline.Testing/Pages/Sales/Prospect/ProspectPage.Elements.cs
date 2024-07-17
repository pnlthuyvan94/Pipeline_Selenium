using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Sales.Prospect
{
    public partial class ProspectPage : DashboardContentPage<ProspectPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Sales\ProspectParams.xlsx";
        public IQueryable<Row> MetaData { get; set; }

        public ProspectPage() 
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            MetaData = ExcelHelper.GetAllRows("Prospect_OR");
        }

        private Row _grid => ExcelFactory.GetRow(MetaData, 1);
        private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 2);
        private string _gridLoading => _loadingGifRow[BaseConstants.ValueToFind];

        protected IGrid Prospect_Grid => new Grid(_grid, _gridLoading);
    }
}
