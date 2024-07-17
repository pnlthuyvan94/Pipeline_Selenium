using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.Role.AddRole;
using System.IO;
using System.Linq;
using System.Reflection;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.UserMenu.Role
{
    public partial class RolePage : DashboardContentPage<RolePage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\UserMenu\RoleParams.xlsx";

        //public IQueryable<Row> MetaData { get; set; }

        //public IQueryable<Row> TestData_RT01107 { get; set; }

        public AddRoleModal AddRoleModal { get; private set; }

        public RolePage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            //MetaData = ExcelHelper.GetAllRows("Role_OR");
            //TestData_RT01107 = ExcelHelper.GetAllRows("RT_01107");
        }

        //private Row _grid => ExcelFactory.GetRow(MetaData, 5);

        //private Row _loadingGifRow => ExcelFactory.GetRow(MetaData, 6);

        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgRoles']/div[1]";

        protected IGrid Role_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgRoles_ctl00", _gridLoading);

    }
}
