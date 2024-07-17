using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.DocumentType.AddDocument;
using System.IO;
using System.Linq;
using System.Reflection;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.UserMenu.DocumentType
{
    public partial class DocumentTypePage : DashboardContentPage<DocumentTypePage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\UserMenu\DocumentParams.xlsx";

        //public IQueryable<Row> MetaData { get; set; }

        //public IQueryable<Row> TestData_RT01108 { get; set; }

        public AddDocumentType AddDocumentType { get; private set; }

        public DocumentTypePage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("DocumentType_OR");
            //TestData_RT01108 = ExcelHelper.GetAllRows("RT_01108");
        }

        //private Row _grid => ExcelFactory.GetRow(MetaData, 10);

        //private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 11);

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddEditType']/div[1]";

        protected IGrid DocumentType_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgTypes_ctl00", _gridLoading);

    }
}
