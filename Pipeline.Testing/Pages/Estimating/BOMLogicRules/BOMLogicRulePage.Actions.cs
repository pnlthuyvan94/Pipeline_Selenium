

using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BOMLogicRules.AddBOMLogicRule;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules
{
    public partial class BOMLogicRulePage
    {
        public void ClickAddToShowBOMLogicRuleModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddBOMLogicRuleModal = new AddBOMLogicRuleModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BOMLogicRule_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BOMLogicRule_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMLogicRules']", 1000);
        }
        public void DeleteItemInGrid(string columnName, string value)
        {
            BOMLogicRule_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }
        public void WaitGridLoad()
        {
            BOMLogicRule_Grid.WaitGridLoad();
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            BOMLogicRule_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }
        public void CreateNewBOMLogicRule(BOMLogicRuleData oldData)
        {
            //Click ON + 
            ClickAddToShowBOMLogicRuleModal();
            AddBOMLogicRuleModal = new AddBOMLogicRuleModal();
            AddBOMLogicRuleModal.EnterRuleName(oldData.RuleName);
            AddBOMLogicRuleModal.EnterRuleDescription(oldData.RuleDescription);
            AddBOMLogicRuleModal.EnterSortOrder(oldData.SortOrder);
            AddBOMLogicRuleModal.SelectExecutionType(oldData.Execution);
            AddBOMLogicRuleModal.Save();
            string Actual_message = GetLastestToastMessage();
            string Expected_message = $"{oldData.RuleName} was created successfully";
            if (Actual_message == Expected_message)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b> BOM Logic Rule with name {oldData.RuleName} was created successfully </b></font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'> BOM Logic Rule with name {oldData.RuleName} is not create successfully</font color>");
            }
        }
        public void DeleteBOMLogicRule(BOMLogicRuleData oldData)
        {
            DeleteItemInGrid("Rule Name", oldData.RuleName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMLogicRules']/div[1]");

            string expectedMsg = $"{oldData.RuleName} was deleted successfully";
            string actualMsg = GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("BOM Logic Rule deleted successfully!");
                CloseToastMessage();
            }
            else 
            {
                if (IsItemInGrid("Rule Name", oldData.RuleName))
                    ExtentReportsHelper.LogFail("BOM Logic Rule could not be deleted!");
                else
                    ExtentReportsHelper.LogPass(null, "BOM Logic Rule deleted successfully!");
            }
        }

    }
}
