using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.Builder.Community
{
    public partial class CommunitySettingPage : DetailsContentPage<CommunitySettingPage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Settings\CustomField\CustomFieldParams.xlsx";

        // Object repository
        const string Id_ViewModeDropdown = "ctl00_CPH_Content_ddlViewType";
        const string Xp_LoadingWhenSwitchView = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]";
        const string Id_CommunityCustomField = "ctl00_CPH_Content_rgCustomFieldsToCommunity_ctl00";
        const string Xp_LoadingGifCommunityCustomField = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFieldsToCommunity']/div[1]";
        const string Id_AddCommunityCustomField = "ctl00_CPH_Content_lbAddCustomFieldsToCommunity";
        const string Id_ApplyDefault = "ctl00_CPH_Content_lbApplyToCommunity";
        const string Xp_TitleCommunityCustomFieldSection = "//*[@id='ctl00_CPH_Content_pnlCommunity']/div[2]/div/article/section/article/header/h1";

        //public IQueryable<Row> TestData { get; set; }

        public CommunitySettingPage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            //// Sheet contains repository of Dashboard
            //TestData = ExcelHelper.GetAllRows("SettingCustomField");
        }

        // Controls
        protected Label CommunityCustomFieldTitle_lbl => new Label(FindType.XPath, Xp_TitleCommunityCustomFieldSection);
        protected IGrid CommunityCustomField_Grid => new Grid(FindType.Id, Id_CommunityCustomField, Xp_LoadingGifCommunityCustomField);
        protected Button Add_btn => new Button(FindType.Id, Id_AddCommunityCustomField);
        protected Button ApplyDefault_Btn => new Button(FindType.Id, Id_ApplyDefault);
        protected DropdownList SwitchView_ddl => new DropdownList(FindType.Id, Id_ViewModeDropdown);

    }
}
