using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.User.AddUser;
using System.IO;
using System.Linq;
using System.Reflection;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.UserMenu.User
{
    public partial class UserPage : DashboardContentPage<UserPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\UserMenu\UserParams.xlsx";

        //public IQueryable<Row> MetaData { get; set; }
        public IQueryable<Row> TestData_Data_User_Input { get; set; }

        public AddUserDetailPage AddUserDetail { get; private set; }

        public UserPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("User_OR");
            TestData_Data_User_Input = ExcelHelper.GetAllRows("Data_Input");
        }

        //private Row _grid => ExcelFactory.GetRow(MetaData, 20);

        //private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 21);

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUsers']/div[1]";

        //protected IGrid User_Grid => new Grid(_grid, _gridLoading);
        protected IGrid User_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgUsers_ctl00']", _gridLoading);

    }
}
