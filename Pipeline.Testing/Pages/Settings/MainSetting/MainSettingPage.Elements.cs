using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.MainSetting
{
    public partial class MainSettingPage : DetailsContentPage<MainSettingPage>
    {
        protected Textbox TransferSeparation_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtTransferSeparationCharacter");
        protected Button Save_btn => new Button(FindType.XPath, "//section[@class = 'subhead']/a[@id='ctl00_CPH_Content_lbSave']");

    }
}
