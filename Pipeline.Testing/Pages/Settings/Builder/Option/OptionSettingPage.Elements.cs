using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Pages.Settings.Builder.Option
{
    public partial class OptionSettingPage : DetailsContentPage<OptionSettingPage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Settings\CustomField\CustomFieldParams.xlsx";

        // Object repository
        const string Id_ViewModeDropdown = "ctl00_CPH_Content_ddlViewType";
        const string Xp_LoadingWhenSwitchView = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]";
        const string Id_OptionCustomField = "ctl00_CPH_Content_rgCustomFields_ctl00";
        const string Xp_LoadingGifOptionCustomField = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFields']/div[1]";
        const string Id_AddOptionCustomField = "ctl00_CPH_Content_lbAddCustomFields";
        const string Id_ApplyDefault = "ctl00_CPH_Content_lbApply";
        const string Xp_TitleOptionCustomFieldSection = "//*[@id='ctl00_CPH_Content_pnlOption']/div[2]/div/article/section/article/header/h1";
        const string Id_OptionDisplayFormat = "ctl00_CPH_Content_ddlOptionDisplayFormat";
        const string Id_SaveOptionSetting = "ctl00_CPH_Content_lbSaveOption";


        //public IQueryable<Row> TestData { get; set; }

        public OptionSettingPage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            //// Sheet contains repository of Dashboard
            //TestData = ExcelHelper.GetAllRows("SettingCustomField");
        }

        // Controls
        protected Label OptionCustomFieldTitle_lbl => new Label(FindType.XPath, Xp_TitleOptionCustomFieldSection);
        protected IGrid OptionCustomField_Grid => new Grid(FindType.Id, Id_OptionCustomField, Xp_LoadingGifOptionCustomField);
        protected Button Add_btn => new Button(FindType.Id, Id_AddOptionCustomField);
        protected Button ApplyDefault_Btn => new Button(FindType.Id, Id_ApplyDefault);
        protected DropdownList SwitchView_ddl => new DropdownList(FindType.Id, Id_ViewModeDropdown);
        protected DropdownList OptionDisplayFormat_ddl => new DropdownList(FindType.Id, Id_OptionDisplayFormat);
        protected Button SaveOptionSetting_btn => new Button(FindType.Id, Id_SaveOptionSetting);

    }
}
