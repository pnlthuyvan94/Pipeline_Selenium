using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Costing.CommunitySalesTax
{
    public partial class CommunitySalesTaxPage
    {
        public CommunitySalesTaxPage SelectCommunity(string community)
        {
            Community_ddl.SelectItem(community, true, false);
            WaitingLoadingGifByXpath(_gridLoading);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Community_ddl), $"Select community {community}.");
            return this;
        }

        public CommunitySalesTaxPage SelectTaxGroup(string taxGroup)
        {
            TaxGroup_ddl.SelectItem(taxGroup, true, false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlCommunityTaxGroup']/div[1]");
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(TaxGroup_ddl), $"Select Tax Group {taxGroup}.");
            return this;
        }

        public CommunitySalesTaxPage SelectBuildingPhase(string buildingPhaseName)
        {
            string findBuildingPhase = $"//*[@id='ctl00_CPH_Content_rgTaxes_ctl00_ctl02_ctl02_ddlBuildingPhases_DropDown']/div/ul/li[text()='{buildingPhaseName}']";
            BuildingPhaseArrow_btn.Click();

            // Wait until the phase displays and select it
            CommonHelper.WaitUntilElementVisible(5,findBuildingPhase);

            var findLocation = CommonHelper.VisibilityOfAllElementsLocatedBy(5, findBuildingPhase);
            if (findLocation is null) throw new System.Exception($"Could not find the building phase name : {buildingPhaseName}");
            else
                findLocation.Click();
            WaitingLoadingGifByXpath(_gridLoading, 2000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_rgTaxes_ctl00_ctl02_ctl02_ddlBuildingPhases_Input")), $"Select Tax Group {buildingPhaseName}.");
            return this;
        }

        public CommunitySalesTaxPage ClickEditItemInGrid(string columnName, string value)
        {
            CommunitySalesTaxPage_Grid.ClickEditItemInGrid(columnName, value);
            WaitingLoadingGifByXpath(_gridLoading);
            return this;
        }

        public string SelectTaxGroupOverride(string taxGroupOverride,int index)
        {
            return TaxGroupOverride_ddl.SelectItemByValueOrIndex(taxGroupOverride, index);
        }

        public void Save()
        {
            UpdateValue_btn.Click();
            WaitingLoadingGifByXpath(_gridLoading);
        }

    }
}