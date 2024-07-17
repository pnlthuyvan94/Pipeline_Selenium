using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;

namespace Pipeline.Testing.Pages.Estimating.Uses
{
    public partial class UsesPage
    {
        public void ClickAddToUsesModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddUsesModal = new AddUsesModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Uses_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Uses_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Uses_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitGridLoad();
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUses']");
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            Uses_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        /// <summary>
        /// Get total item on the grid view
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return Uses_Grid.GetTotalItems;
        }

        /// <summary>
        /// Delete Use by Name
        /// </summary>
        public void DeleteUses(string name)
        {
            if (IsItemInGrid("Name", name))
            {
                // Select item and click deletete icon
                DeleteItemInGrid("Name", name);
                WaitGridLoad();

                string successfulMess = $"{name} successfully deleted.";
                if (successfulMess == GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Use deleted successfully!</b></font>");
                }
                else
                {
                    if (IsItemInGrid("Name", name) is false)
                        ExtentReportsHelper.LogPass("<font color='green'><b>Use could not be deleted!</b></font>");
                    else
                        ExtentReportsHelper.LogFail("<font color='red'>Use deleted successfully!</font>");

                }
            }
        }

        /// <summary>
        /// Create a new Use
        /// </summary>
        /// <param name="useData"></param>
        public void CreateNewUse(UsesData useData)
        {
            ClickAddToUsesModal();

            if (AddUsesModal.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Create Use modal is not displayed or title is incorrect!" +
                    "<br>Expected title: 'Add Use</font>");
            }

            // Create Option Room - Click 'Save' Button
            AddUsesModal.FillDataAndSave(useData);
            string actualMess = GetLastestToastMessage();
            string expectedMess = $"{useData.Name} successfully created.";

            if (actualMess == expectedMess)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Create Use with name { useData.Name}, description {useData.Description} and Sort Order {useData.SortOrder} successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Could not create Use with name { useData.Name}" +
                    $"<br>Expected message: {expectedMess}" +
                    $"<br>Actual message: {actualMess}.</br></font>");
                CloseToastMessage();
            }
        }
    }
}
