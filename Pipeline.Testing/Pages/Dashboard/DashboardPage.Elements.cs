using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Dashboard
{
    public partial class DashboardPage : NormalPage<DashboardPage>
    {
        private readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\DashboardParams.xlsx";
        public DashboardPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("Dashboard_OR");
            TestData_RT01001 = ExcelHelper.GetAllRows("RT_01001");
            TestData_RT01003 = ExcelHelper.GetAllRows("RT_01003");
            TestData_RT01004 = ExcelHelper.GetAllRows("RT_01004");
            TestData_RT01005 = ExcelHelper.GetAllRows("RT_01005");
            TestData_RT01006 = ExcelHelper.GetAllRows("RT_01006");
            TestData_RT01007 = ExcelHelper.GetAllRows("RT_01007");
            TestData_RT01008 = ExcelHelper.GetAllRows("RT_01008");
            TestData_RT01011 = ExcelHelper.GetAllRows("RT_01011");
            TestData_RT01012 = ExcelHelper.GetAllRows("RT_01012");
            TestData_RT01013 = ExcelHelper.GetAllRows("RT_01013");
            TestData_RT01014 = ExcelHelper.GetAllRows("RT_01014");
            TestData_RT01015 = ExcelHelper.GetAllRows("RT_01015");
            TestData_RT01016 = ExcelHelper.GetAllRows("RT_01016");
            TestData_RT01017 = ExcelHelper.GetAllRows("RT_01017");
            TestData_RT01018 = ExcelHelper.GetAllRows("RT_01018");
            TestData_RT01019 = ExcelHelper.GetAllRows("RT_01019");
            TestData_RT01020 = ExcelHelper.GetAllRows("RT_01020");
            TestData_RT01071 = ExcelHelper.GetAllRows("RT_01071");


        }

        public IQueryable<Row> TestData_RT01001;
        public IQueryable<Row> TestData_RT01003;
        public IQueryable<Row> TestData_RT01004;
        public IQueryable<Row> TestData_RT01005;
        public IQueryable<Row> TestData_RT01006;
        public IQueryable<Row> TestData_RT01007;
        public IQueryable<Row> TestData_RT01008;
        public IQueryable<Row> TestData_RT01011;
        public IQueryable<Row> TestData_RT01012;
        public IQueryable<Row> TestData_RT01013;
        public IQueryable<Row> TestData_RT01014;
        public IQueryable<Row> TestData_RT01015;
        public IQueryable<Row> TestData_RT01016;
        public IQueryable<Row> TestData_RT01017;
        public IQueryable<Row> TestData_RT01018;
        public IQueryable<Row> TestData_RT01019;
        public IQueryable<Row> TestData_RT01020;
        public IQueryable<Row> TestData_RT01071;

        public IQueryable<Row> MetaData { get; set; }

        private Row _welcome { get { return ExcelFactory.GetRow(MetaData, 1); } }
        public Label Welcome_lbl => new Label(_welcome);

        #region "Dashboard Overview"

        private Row _jobs { get { return ExcelFactory.GetRow(MetaData, 2); } }
        public Button JOBS => new Button(_jobs);

        private Row _houses { get { return ExcelFactory.GetRow(MetaData, 3); } }
        public Button HOUSES => new Button(_houses);

        private Row _communities { get { return ExcelFactory.GetRow(MetaData, 4); } }
        public Button COMMUNITIES => new Button(_communities);

        private Row _options { get { return ExcelFactory.GetRow(MetaData, 5); } }
        public Button OPTIONS => new Button(_options);

        private Row _products { get { return ExcelFactory.GetRow(MetaData, 6); } }
        public Button PRODUCTS => new Button(_products);

        #endregion

        #region "Activity Feed"
        // Loading gif
        private Row _loadingGif { get { return ExcelFactory.GetRow(MetaData, 11); } }
        private SpecificControls _loadingGif_Activity => new SpecificControls(_loadingGif);

        private Row _sortButton_Activity { get { return ExcelFactory.GetRow(MetaData, 7); } }
        public Button SortActivity_Btn => new Button(_sortButton_Activity);

        private Row _sortMenu_Activity { get { return ExcelFactory.GetRow(MetaData, 8); } }
        //public DropdownMenu Activity_Menu => new DropdownMenu(_sortMenu_Activity);
        public DropdownMenu Activity_Menu => new DropdownMenu(FindType.XPath, "//*[@id='ctl00_CPH_Content_af1_pnlActivityFeed']/header/div[1]/ul");
       
        private Row _gridActivity { get { return ExcelFactory.GetRow(MetaData, 10); } }
        public SpecificControls Activity_Grid => new SpecificControls(_gridActivity);

        private Row _viewAllActivity { get { return ExcelFactory.GetRow(MetaData, 9); } }
        public Button ViewAllActivity_Btn => new Button(_viewAllActivity);

        #endregion

        #region "Recent Houses"
        // Loading gif
        private Row _loadingGif_Houses_Row { get { return ExcelFactory.GetRow(MetaData, 16); } }
        private SpecificControls _loadingGif_Houses => new SpecificControls(_loadingGif_Houses_Row);

        private Row _sortButton_Houses { get { return ExcelFactory.GetRow(MetaData, 12); } }
        public Button SortHouses_Btn => new Button(_sortButton_Houses);

        private Row _sortMenu_Houses { get { return ExcelFactory.GetRow(MetaData, 13); } }
        //public DropdownMenu SortHouses_Menu => new DropdownMenu(_sortMenu_Houses);
        public DropdownMenu SortHouses_Menu => new DropdownMenu(FindType.XPath, "//*[@id='recent-houses']//header/div/ul");

        private Row _gridHouses { get { return ExcelFactory.GetRow(MetaData, 15); } }
        public SpecificControls Houses_Grid => new SpecificControls(_gridHouses);

        private Row _viewAllHouses { get { return ExcelFactory.GetRow(MetaData, 14); } }
        public Button ViewAllHouses_Btn => new Button(_viewAllHouses);

        #endregion

        #region "Recent Active Jobs"
        // Loading gif
        private Row _loadingGifJobs_Row { get { return ExcelFactory.GetRow(MetaData, 21); } }
        private SpecificControls _loadingGif_Jobs => new SpecificControls(_loadingGifJobs_Row);

        private Row _sortButton_Jobs { get { return ExcelFactory.GetRow(MetaData, 17); } }
        public Button SortJobs_Btn => new Button(_sortButton_Jobs);

        private Row _sortMenu_Jobs { get { return ExcelFactory.GetRow(MetaData, 18); } }
        public DropdownMenu SortJobs_Menu => new DropdownMenu(_sortMenu_Jobs);

        private Row _gridJobs { get { return ExcelFactory.GetRow(MetaData, 20); } }
        private SpecificControls _jobs_Grid => new SpecificControls(_gridJobs);

        private Row _viewAllJobs { get { return ExcelFactory.GetRow(MetaData, 19); } }
        public Button ViewAllJobs_Btn => new Button(_viewAllJobs);

        #endregion

        #region "Recent Communities"
        // Loading gif
        private Row _loadingGifCommunities_Row { get { return ExcelFactory.GetRow(MetaData, 26); } }
        private SpecificControls _loadingGif_Communities => new SpecificControls(_loadingGifCommunities_Row);

        private Row _sortButton_Communities { get { return ExcelFactory.GetRow(MetaData, 22); } }
        public Button SortCommunities_Btn => new Button(_sortButton_Communities);

        private Row _sortMenu_Communities { get { return ExcelFactory.GetRow(MetaData, 23); } }
        public DropdownMenu SortCommunities_Menu => new DropdownMenu(_sortMenu_Communities);

        private Row _gridCommunities { get { return ExcelFactory.GetRow(MetaData, 25); } }
        public SpecificControls _communities_Grid => new SpecificControls(_gridCommunities);

        private Row _viewAllCommunities { get { return ExcelFactory.GetRow(MetaData, 24); } }
        public Button ViewAllCommunities_Btn => new Button(_viewAllCommunities);

        #endregion

        #region "Recent Options"
        private Row _loadingGifOptions_Row { get { return ExcelFactory.GetRow(MetaData, 31); } }
        private SpecificControls _loadingGif_Options => new SpecificControls(_loadingGifOptions_Row);

        private Row _sortButton_Options { get { return ExcelFactory.GetRow(MetaData, 27); } }
        public Button SortOptions_Btn => new Button(_sortButton_Options);

        private Row _sortMenu_Options { get { return ExcelFactory.GetRow(MetaData, 28); } }
        public DropdownMenu SortOptions_Menu => new DropdownMenu(_sortMenu_Options);

        private Row _gridOptions { get { return ExcelFactory.GetRow(MetaData, 30); } }
        private SpecificControls _options_Grid => new SpecificControls(_gridOptions);

        private Row _viewAllOptions { get { return ExcelFactory.GetRow(MetaData, 29); } }
        public Button ViewAllOptions_Btn => new Button(_viewAllOptions);
        #endregion

        #region "Recent Products"
        private Row _loadingGifProducts_Row { get { return ExcelFactory.GetRow(MetaData, 36); } }
        public SpecificControls LoadingGif_Products => new SpecificControls(_loadingGifProducts_Row);

        private Row _sortButton_Products { get { return ExcelFactory.GetRow(MetaData, 32); } }
        public Button SortProducts_Btn => new Button(_sortButton_Products);

        private Row _sortMenu_Products { get { return ExcelFactory.GetRow(MetaData, 33); } }
        public DropdownMenu SortProducts_Menu => new DropdownMenu(_sortMenu_Products);

        private Row _gridProducts { get { return ExcelFactory.GetRow(MetaData, 35); } }
        public SpecificControls Products_Grid => new SpecificControls(_gridProducts);

        private Row _viewAllProducts { get { return ExcelFactory.GetRow(MetaData, 34); } }
        public Button ViewAllProducts_Btn => new Button(_viewAllProducts);

        #endregion
    }
}
