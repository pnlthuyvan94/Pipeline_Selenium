using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule.AddBuildingPhaseRule;

namespace Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule
{
    public partial class QuantityBuildingPhaseRulePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhaseRule_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rapPhGrid']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingPhaseRule_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingPhaseRule_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rapPhGrid']/div[1]");
        }

        public void WaitGridLoad()
        {
            BuildingPhaseRule_Grid.WaitGridLoad();
        }

        public void ClickAddToOpenBuildingPhaseRuleModal()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddBuildingPhaseRuleModal = new AddBuildingPhaseRuleModal();
            System.Threading.Thread.Sleep(500);
        }

        public void CloseModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='houses']/header/a").Click();
            System.Threading.Thread.Sleep(500);
        }
    }

}
