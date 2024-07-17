using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Settings.BuildPro
{
    public partial class BuildProPage : DetailsContentPage<BuildProPage>
    {
        public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Settings\SettingsParams.xlsx";

        public IQueryable<Row> MetaData { get; set; }
        public BuildProPage() : base()
        {
            ExcelHelper = new ExcelFactory(_pathExcelFile);
            // Sheet contains repository of Dashboard
            MetaData = ExcelHelper.GetAllRows("Settings_OR");
        }

        protected Textbox RootUri_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtBoxRootURI");

        protected Textbox UserName_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtBoxUsername");

        protected Textbox Password_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtBoxPassword");

        protected Textbox Company_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtBoxCompany");
        protected Textbox Division_Txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtBoxDivision");

        protected Button Running_Btn => new Button(FindType.Id, "ctl00_CPH_Content_rbStatus_0");

        protected Button Paused_Btn => new Button(FindType.Id, "ctl00_CPH_Content_rbStatus_1");

        protected Button TestConnection_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbTestConnection");

        protected Button Save_Btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSave");

        // Buildpro
        protected Textbox BP_Username_Txt => new Textbox(FindType.Id, "userName");
        protected Textbox BP_Password_Txt => new Textbox(FindType.Id, "pwd");
        protected Button BP_Login_Btn => new Button(FindType.Id, "LoginButton");

        // Org page
        protected Textbox BP_OrgNumber_Txt => new Textbox(FindType.Id, "org_num");
        protected Textbox BP_OrgName_Txt => new Textbox(FindType.Name, "org_name");
        protected DropdownList BP_OrgState_Txt => new DropdownList(FindType.Name, "bill_to_state_code");
        protected Textbox BP_OrgZip_Txt => new Textbox(FindType.Name, "bill_to_postal_code");

        // Cost Code
        protected Button BP_CGVisionCodeCost_Btn => new Button(FindType.XPath, "//a[.='CG Vision Codes']");

        // Vendor
        protected DropdownList BP_ListItem_ddl => new DropdownList(FindType.Id, "ctl00_ctl00_ctl00_CPH_Middle_CPH_Middle_CPH_Middle_ddlMaxRows");
        protected Button BP_SearchVendor_btn => new Button(FindType.Id, "showTasksButton");
        protected CheckBox ForceSignIn_chk => new CheckBox(FindType.Id, "force_signon");

        // Job
        protected DropdownList JobName_ddl => new DropdownList(FindType.Id, "jobDropDown");
        protected Button ShowTask_Btn => new Button(FindType.Id, "button_text");
        protected Button ExpandJobFilter => new Button(FindType.Id, "frExpand");
    }
}
