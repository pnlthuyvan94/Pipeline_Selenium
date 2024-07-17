using Pipeline.Common.Pages;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.BuilderMT
{
    public partial class BuilderMTPage : DetailsContentPage<BuilderMTPage>
    {
        private const string SAVE_PRODUCT_LOADING_ICON = "//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lblSaveProductPhaseMappingLoad']/div[1]";
        private const string BUILDERMT_PRODUCTS = "BuilderMT (Products)";
        private const string XPATH_BUILDINGPHASE_LIMIT_CODE_VALUE = "//Select[@id = 'ctl00_CPH_Content_ddlBuildingPhaseCodeLength']";
        private const string SAVE_BUTTON_ID = "ctl00_CPH_Content_lbSaveProductPhaseMapping";
        private const string BUILDINGPHASE_CODE_LENGTH_REQUIRED_ID = "ctl00_CPH_Content_ddlBuildingPhaseCodeLength";
        public void SelectGroupByParameter(bool value)
        {
            Button selectedItem;
            Button saveBuilderMTSetting = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave']");
            if (value is true)
                selectedItem = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbStatus_0']");
            else
                selectedItem = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rbStatus_1']");

            if (selectedItem.IsExisted(false) is true && saveBuilderMTSetting.IsDisplayed(false) is true)
            {
                selectedItem.Click();
                saveBuilderMTSetting.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlSettings']/div[1]");

            }
            else
                ExtentReportsHelper.LogWarning(null, $"Can't find 'Status' or 'Save' button to update.");
        }
        public void SetBuildingPhaseCodeLimit(int codeLimitValue)
        {            
            LeftMenuNavigation(BUILDERMT_PRODUCTS);
            CommonHelper.ScrollToElement(BUILDINGPHASE_CODE_LENGTH_REQUIRED_ID);
            DropdownList currentValueDropDownList = new DropdownList(FindType.XPath, XPATH_BUILDINGPHASE_LIMIT_CODE_VALUE);
            if (currentValueDropDownList.GetValue() == codeLimitValue.ToString())
            {
                return;
            }
            currentValueDropDownList.SelectItemByValue(codeLimitValue.ToString());
            CommonHelper.ScrollToElement(SAVE_BUTTON_ID);
            Button saveButton = new Button(FindType.Id, SAVE_BUTTON_ID);
            saveButton.Click();
            WaitingLoadingGifByXpath(SAVE_PRODUCT_LOADING_ICON);
        }
    }
}
