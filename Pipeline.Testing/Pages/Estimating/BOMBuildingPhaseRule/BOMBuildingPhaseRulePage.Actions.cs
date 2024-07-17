using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule.AddBuildingPhaseRule;

namespace Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule
{
    public partial class BOMBuildingPhaseRulePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhaseRule_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingPhaseRule_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingPhaseRule_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitGridLoad();
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMPhaseRules']/div[1]");
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
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='options-modal']/section/header/a").Click();
            System.Threading.Thread.Sleep(500);
        }

        public void DeleteBOMBuildingPhaseRule(BOMBuildingPhaseRuleData _data)
        {
            DeleteItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMPhaseRules']/div[1]");
            string successfulMess = $"Successfully deleted BOM Building Phase Rule";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "BOM Building Phase Rule deleted successfully!");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Original Product Building Phase", _data.OriginalProductBuildingPhase))
                    ExtentReportsHelper.LogFail("BOM Building Phase Rule could not be deleted!");
                else
                    ExtentReportsHelper.LogPass(null, "BOM Building Phase Rule deleted successfully!");
            }
        }

        /// <summary>
        /// Get total number on the grid view
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return BuildingPhaseRule_Grid.GetTotalItems;
        }
        public void EditItemInGrid(string columnName, string value)
        {
            BuildingPhaseRule_Grid.ClickEditItemInGrid(columnName, value);
            WaitGridLoad();
        }
        public void UpdateNewSubcomponentBuildingPhase(string newSubBuildingPhase)
        {
            UpdateNewSubBuildingPhase_ddl.SelectItem(newSubBuildingPhase);
            UpdateNewSubBuildingPhase_btn.Click();
            WaitGridLoad();
            if (IsItemInGrid("New Subcomponent Building Phase", newSubBuildingPhase))
                ExtentReportsHelper.LogPass(null, "BOM Building Phase Rule updated successfully!");
            else
                ExtentReportsHelper.LogFail("BOM Building Phase Rule could not be updated!");

        }


        public void IsColumnHeaderIndexByName(string columnName)
        {
            BuildingPhaseRule_Grid.IsColumnHeaderIndexByName(columnName);
        }

        public void IsEditAndDeleteFirstItem()
        {
            BuildingPhaseRule_Grid.IsEditFirstItem();
            BuildingPhaseRule_Grid.IsDeleteFirstItem();
        }

        public void IsAddAndUtilitiesButton()
        {
            if (Add_btn.IsDisplayed() is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Add Button isn't displayed or title is incorrect.</font>");
            else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(Add_btn), "<font color ='green'><b>Add Button displayed successfully.</b></font>");

            if (Utilities_btn.IsDisplayed() is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Utilities Button isn't displayed or title is incorrect.</font>");
            else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(Utilities_btn), "<font color ='green'><b>Utilities Button displayed successfully.</b></font>");

        }
        public void DeleteItemSelectedInGrid(int ItemTotal)
        {
            for (int i = 0; i <= ItemTotal; i++)
            {
                CheckBox ItemSelected_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgBOMPhaseRules_ctl00__{i}']//input[@type='checkbox']");
                ItemSelected_chk.Check(true);
            }
            Button DeleteData_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbDeleteSelected']");
            BulkActions_btn.Click();
            DeleteData_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitGridLoad();
        }
    }

}
