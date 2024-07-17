using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Purchasing.CostCode.AddCostCode;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.CostCode
{
    public partial class CostCodePage : DashboardContentPage<CostCodePage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\CostCodeParams.xlsx";

        //public IQueryable<Row> MetaData { get; set; }

        //public IQueryable<Row> TestData_RT01229 { get; set; }

        public AddCostCodeModal AddCostCodeModal { get; private set; }

        public CostCodePage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            //// Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("CostCode_OR");
            //TestData_RT01229 = ExcelHelper.GetAllRows("RT_01229");
        }

        //private Row _grid => ExcelFactory.GetRow(MetaData, 6);

        //private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 7);

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgCostCodes']/div[1]";

        protected IGrid CostCode_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgCostCodes_ctl00", _gridLoading);

    }
}
