using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Styles.DetailStyles
{
    public partial class StyleDetailPage : DetailsContentPage<StyleDetailPage>
    {
        protected Textbox Name_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected DropdownList Manufacturer_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlMfg']");

        protected Button AddManufacturer_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddMfg']");

        protected Textbox Url_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUrl']");

        protected Textbox Description_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDescription']");

        protected Button StyleSave_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

        string loadingIcon => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlContent']/div[1]";

        protected Button AddStyle_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypNew']");

    }

}
