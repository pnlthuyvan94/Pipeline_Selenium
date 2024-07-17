using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.Builder.House
{
    public partial class HouseSettingPage : DetailsContentPage<HouseSettingPage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Settings\CustomField\CustomFieldParams.xlsx";

        // Object repository
        const string Id_ViewModeDropdown = "ctl00_CPH_Content_ddlViewType";
        const string Xp_LoadingWhenSwitchView = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]";
        const string Id_HouseCustomField = "ctl00_CPH_Content_rgCustomFieldsToHouse_ctl00";
        const string Xp_LoadingGifHouseCustomField = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFieldsToHouse']/div[1]";
        const string Id_AddHouseCustomField = "ctl00_CPH_Content_lbAddCustomFieldsToHouse";
        const string Id_ApplyDefault = "ctl00_CPH_Content_lbApplyToHouse";
        const string Xp_TitleHouseCustomFieldSection = "//*[@id='ctl00_CPH_Content_pnlHouse']/div[2]/div/article/section/article/header/h1";

        //public IQueryable<Row> TestData { get; set; }

        public HouseSettingPage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            //// Sheet contains repository of Dashboard
            //TestData = ExcelHelper.GetAllRows("SettingCustomField");
        }

        // Controls
        protected Label HouseCustomFieldTitle_lbl => new Label(FindType.XPath, Xp_TitleHouseCustomFieldSection);
        protected IGrid HouseCustomField_Grid => new Grid(FindType.Id, Id_HouseCustomField, Xp_LoadingGifHouseCustomField);
        protected Button Add_btn => new Button(FindType.Id, Id_AddHouseCustomField);
        protected Button ApplyDefault_Btn => new Button(FindType.Id, Id_ApplyDefault);
        protected DropdownList SwitchView_ddl => new DropdownList(FindType.Id, Id_ViewModeDropdown);

    }
}
