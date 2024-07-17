using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Windows.Forms;

namespace Pipeline.Testing.Pages.UserMenu.Setting
{
    public partial class SettingPage
    {
        
        public void ShowCategoryonAddProductSubcomponentModalCheckbox(bool condition = true)
        {
            if (!condition)
            {
                ShowCategoryonAddProductSubcomponentModal_chk.UnCheck();
            }
            else
                ShowCategoryonAddProductSubcomponentModal_chk.Check();
        }

        public void SelectTransferSeparationCharacter(string character)
        {
            Textbox separator = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtTransferSeparationCharacter']");

            if(separator != null && character != string.Empty) {
                ExtentReportsHelper.LogInformation(null, $"Update 'Transfer Separation Character' with character '{character}'.");
                separator.SetText(character);
                Save_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave']/div[1]");
            } else
            {
                ExtentReportsHelper.LogWarning(null, $"Can't find 'Transfer Separation Character' to update or updated value is empty.");
            }
        }

    }

}
