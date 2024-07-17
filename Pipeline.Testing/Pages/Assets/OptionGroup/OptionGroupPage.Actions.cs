using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.OptionGroup.AddOptionGroup;
using Pipeline.Common.Export;

namespace Pipeline.Testing.Pages.Assets.OptionGroup
{
    public partial class OptionGroupPage
    {
        public void ClickAddToOptionGroupModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddOptionGroup = new AddOptionGroupModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return OptionGroup_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            OptionGroup_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionGroups']", 2000);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            OptionGroup_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionGroups']/div[1]");
        }

        public void SelectItemInGrid(string columnName, string valueToFind)
        {
            OptionGroup_Grid.ClickItemInGrid(columnName, valueToFind);
            PageLoad();
        }

        /// <summary>
        /// Delete option group
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="value"></param>
        public void DeleteOptionGroup(string columnName, string value)
        {
            DeleteItemInGrid(columnName, value);
            if ("New option group deleted successfully." == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("New option group deleted successfully.");
            }
            else
            {
                if (IsItemInGrid(columnName, value))
                    ExtentReportsHelper.LogFail("New option group could not be deleted.");
                else
                    ExtentReportsHelper.LogPass("New option group deleted successfully.");
            }
        }

        /// <summary>
        /// Get total item on the grid view
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return OptionGroup_Grid.GetTotalItems;
        }

        /// <summary>
        /// Export file from More menu
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        /// <param name="expectedTotalNumber"></param>
        public void ExportFile(string exportType, string exportFileName, int expectedTotalNumber)
        {
            bool isCaptured = false;
            // Download file
            SelectItemInUtiliestButton(exportType, isCaptured);
            System.Threading.Thread.Sleep(2000);

            // Verify Download File
            string expectedTitle = ExportTitleFileConstant.OPTION_GROUP_TITLE;
            ValidationEngine.ValidateExportFile(exportType, exportFileName, expectedTitle, expectedTotalNumber);
        }

        
    }
}
