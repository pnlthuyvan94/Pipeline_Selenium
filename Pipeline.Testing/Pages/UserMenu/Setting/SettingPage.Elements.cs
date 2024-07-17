using LinqToExcel;
using Pipeline.Common.Constants;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.User.AddUser;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.UserMenu.Setting
{
    public partial class SettingPage : DashboardContentPage<SettingPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\UserMenu\UserParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }
        public IQueryable<Row> TestData_Data_User_Input { get; set; }

        public AddUserDetailPage AddUserDetail { get; private set; }

        public SettingPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("User_OR");
            TestData_Data_User_Input = ExcelHelper.GetAllRows("Data_Input");
        }

        protected Label EstimatingSetting_Title => new Label(FindType.XPath, "//h1[contains(text(),'Estimating Settings')]");
       // protected DropdownList View_ddl => new DropdownList(FindType.XPath, "//*[contains(@name,'ddlViewType')]");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave']");
        ////*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave"]/div[1]
        protected CheckBox ShowCategoryonAddProductSubcomponentModal_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_chkShowSubcomponentCategory']");


    }
}
