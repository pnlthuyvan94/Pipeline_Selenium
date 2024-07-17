using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Purchasing.CostType.AddCostType;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.CostType
{
    public partial class CostTypePage : DashboardContentPage<CostTypePage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\PurChasing\CostTypeParams.xlsx";

        //public IQueryable<Row> MetaData { get; set; }
        //public IQueryable<Row> TestData_RT01095 { get; set; }
        public AddCostTypeModal AddCostTypeModal { get; private set; }

        public CostTypePage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("CostType_OR");
            //TestData_RT01095 = ExcelHelper.GetAllRows("RT_01095");
        }

        //private Row _grid => ExcelFactory.GetRow(MetaData, 6);

        //private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 7);

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgCostTypes']/div[1]";

        protected IGrid CostType_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCostTypes_ctl00']", _gridLoading);

    }
}
