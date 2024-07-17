using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.ReleaseGroup.AddReleaseGroup;

namespace Pipeline.Testing.Pages.Purchasing.ReleaseGroup
{
    public partial class ReleaseGroupPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            ReleaseGroup_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return ReleaseGroup_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            ReleaseGroup_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgReleaseGroups']/div[1]");
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            ReleaseGroup_Grid.ClickItemInGrid(columnName, value);
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgReleaseGroups']/div[1]", 10);
            //CutOffPhase_Grid.WaitGridLoad();
        }

        public void ClickAddToOpenReleaseGroupModal()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddReleaseGroupModal = new AddReleaseGroupModal();
        }

        public void CloseModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='rg-modal']/section/header/a").Click();
            System.Threading.Thread.Sleep(500);
        }

        /// <summary>
        /// Create a new Release Group
        /// </summary>
        /// <param name="data"></param>
        /// <param name="expectedMess"></param>
        /// <param name="isDuplicateChecking"></param>
        public void AddReleaseGroup(ReleaseGroupData data, string expectedMess, bool isDuplicateChecking)
        {
            ClickAddToOpenReleaseGroupModal();
            if (AddReleaseGroupModal.IsModalDisplayed() is false)
            {
                // Ignore this step if Add Release Group modal doesn't display
                ExtentReportsHelper.LogFail("<font color = 'red'>Release Group modal isn't displayed</font>");
                return;
            }

            // 3: Populate all values - Click 'Save' Button
            AddReleaseGroupModal.CreateNewReleaseGroup(data);

            // Verify message
            string actualDuplicateMessage = GetLastestToastMessage();
            if (!string.IsNullOrEmpty(actualDuplicateMessage))
            {
                if (isDuplicateChecking && actualDuplicateMessage != expectedMess)
                {
                    // Expectation: Failed to create with duplicate name
                    ExtentReportsHelper.LogFail($"<font color = 'red'>Create new Release Group with duplicate name '{data.Name}' successful. It should be fail." +
                        $"<br>Expected message: {expectedMess}" +
                        $"<br>Actual message: {actualDuplicateMessage}</br></font>");
                }
                else if (!isDuplicateChecking && actualDuplicateMessage != expectedMess)
                {
                    // Expectation: Create successfully
                    ExtentReportsHelper.LogFail($"<font color = 'red'>Could not create new Release Group with name '{data.Name}'." +
                        $"<br>Expected message: {expectedMess} " +
                        $"<br>Actual message: {actualDuplicateMessage}</br></font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Create new Release Group with name {data.Name} successfully. / Can't create a new Release Group with duplicate name successfully.</b></font>");
                    // if unstable create which will close Modal
                    if (actualDuplicateMessage == $"Not able to create Release Group{ data.Name} at this time.")
                    {
                        AddReleaseGroupModal.Close();
                    }
                }
            }
            else
            {
                ExtentReportsHelper.LogWarning($"<font color = 'yellow'>Don't display any message.</font>");
            }
        }

        /// <summary>
        /// Delete Release Group by Name
        /// </summary>
        /// <param name="releaseGroupName"></param>
        public void DeleteReleaseGroupByName(string releaseGroupName)
        {
            // Select OK to confirm; verify successful delete and appropriate success message.
            DeleteItemInGrid("Name", releaseGroupName);

            string expectedMess = "Release Group successfully removed.";
            if (expectedMess == GetLastestToastMessage())
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Release Group with name '{releaseGroupName}' deleted successfully!</b></font>");
            else
            {
                // Verify if the item is removed from the grid view
                if (IsItemInGrid("Name", releaseGroupName) is true)
                    ExtentReportsHelper.LogFail($"<font color = 'red'>Release Group with name '{releaseGroupName}' could not be deleted!</font>");
                else
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Delete Release Group with name '{releaseGroupName}' successfully!</b></font>");

            }
        }
    }

}
