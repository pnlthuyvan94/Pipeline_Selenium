using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Purchasing.ReleaseGroup.AddReleaseGroup;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Purchasing.ReleaseGroup
{
    public partial class ReleaseGroupPage : DashboardContentPage<ReleaseGroupPage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Purchasing\ReleaseGroupParams.xlsx";
        //public IQueryable<Row> TestData_RT_01094;
        //protected  IQueryable<Row> MetaData { get; set; }
        public AddReleaseGroupModal AddReleaseGroupModal { get; private set; }

        public ReleaseGroupPage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            //// Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("ReleaseGroup_OR");
            //TestData_RT_01094 = ExcelHelper.GetAllRows("RT_01094");
        }

        //private Row _grid => ExcelFactory.GetRow(MetaData, 6);

        //private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 7);

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgReleaseGroups']/div[1]";

        protected IGrid ReleaseGroup_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReleaseGroups_ctl00']", _gridLoading);

    }
}
