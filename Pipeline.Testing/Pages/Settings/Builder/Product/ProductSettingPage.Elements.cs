using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.Builder.Product
{
    public partial class ProductSettingPage : DetailsContentPage<ProductSettingPage>
    {
        //public readonly string _pathExcelFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\Settings\CustomField\CustomFieldParams.xlsx";

        // Object repository
        const string Id_ProductCustomField = "ctl00_CPH_Content_rgCustomFields_ctl00";
        const string Xp_LoadingGifProductCustomField = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFields']/div[1]";
        const string Id_AddProductCustomField = "ctl00_CPH_Content_lbAddCustomFields";
        const string Id_ApplyDefault = "ctl00_CPH_Content_lbApply";
        const string Xp_TitleProductCustomFieldSection = "//*[@id='aspnetForm']/div[3]/section[2]/div/article/section[2]/div[2]/article/section/article/header/h1";

        //public IQueryable<Row> TestData { get; set; }

        public ProductSettingPage() : base()
        {
            //ExcelHelper = new ExcelFactory(_pathExcelFile);
            //// Sheet contains repository of Dashboard
            //TestData = ExcelHelper.GetAllRows("SettingCustomField");
        }

        // Controls
        protected Label ProductCustomFieldTitle_lbl => new Label(FindType.XPath, Xp_TitleProductCustomFieldSection);
        protected IGrid ProductCustomField_Grid => new Grid(FindType.Id, Id_ProductCustomField, Xp_LoadingGifProductCustomField);
        protected Button Add_btn => new Button(FindType.Id, Id_AddProductCustomField);
        protected Button ApplyDefault_Btn => new Button(FindType.Id, Id_ApplyDefault);

    }
}
