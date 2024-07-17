using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.UserMenu.Setting
{
    public partial class SettingPage
    {
       public void ShowCategoryonAddProductSubcomponentModalCheckbox_Action(bool condition = true)
        {
            ShowCategoryonAddProductSubcomponentModalCheckbox(condition);
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave']/div[1]");
            if (condition && ShowCategoryonAddProductSubcomponentModal_chk.IsChecked)
            {
                ExtentReportsHelper.LogInformation("Show Category on Add Product Subcomponent Modal Checkbox is checked");
            }
            else
                ExtentReportsHelper.LogInformation("Show Category on Add Product Subcomponent Modal Checkbox is unchecked");
        }
    }
}
