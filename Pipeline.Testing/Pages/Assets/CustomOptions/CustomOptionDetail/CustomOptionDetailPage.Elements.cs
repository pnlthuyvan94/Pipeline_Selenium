using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail
{
    public partial class CustomOptionDetailPage : DetailsContentPage<CustomOptionDetailPage>
    {
        protected Textbox COCode_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtName']");

        protected Textbox CODescription_txt =>
            new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDescription']");

        protected CheckBox Structural_chk
            => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ckbStructural']");

        protected Textbox Price_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtPrice']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");
    }
}
