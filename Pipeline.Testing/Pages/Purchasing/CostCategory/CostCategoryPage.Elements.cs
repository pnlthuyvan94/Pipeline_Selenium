using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Purchasing.CostCategory.AddCostCategory;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.CostCategory
{
    public partial class CostCategoryPage : DashboardContentPage<CostCategoryPage>
    {
        //private readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\PurChasing\CostCategoryParams.xlsx";
        public AddCostCategoryPage AddCostCategoryModal { get; private set; }

        //public IQueryable<Row> MetaData { get; set; }
        //public IQueryable<Row> TestData_RT01096;

        public CostCategoryPage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("CostCategory");
            //TestData_RT01096 = ExcelHelper.GetAllRows("RT_01096");
        }


        //private Row _grid => ExcelFactory.GetRow(MetaData, 7);
        //private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 8);
        private string _gridLoading => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgCostCategories']/div[1]";

        protected IGrid CostCategory_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgCostCategories_ctl00", _gridLoading);
    }
}
