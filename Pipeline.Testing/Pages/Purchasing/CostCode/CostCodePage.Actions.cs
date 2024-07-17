using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.CostCode.AddCostCode;

namespace Pipeline.Testing.Pages.Purchasing.CostCode
{
    public partial class CostCodePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CostCode_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCostCodes']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CostCode_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            CostCode_Grid.ClickItemInGrid(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            CostCode_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCostCodes']/div[1]");
        }

        public void OpenCostCodeModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddCostCodeModal = new AddCostCodeModal();
        }

        /// <summary>
        /// Create a new Cost Code
        /// </summary>
        /// <param name="data"></param>
        /// <param name="expectedMess"></param>
        /// <param name="isDuplicate"></param>
        public void AddCostCodes(CostCodeData data, string expectedMess, bool isDuplicate)
        {
            OpenCostCodeModal();
            if (AddCostCodeModal.IsModalDisplayed is false)
            {
                // Ignore this step if Add Cost Code modal doesn't display
                ExtentReportsHelper.LogFail("<font color = 'red'>Cost Code modal isn't displayed</font>");
                return;
            }

            // 3: Populate all values - Click 'Save' Button
            AddCostCodeModal.EnterName(data.Name).EnterDescription(data.Description).Save();

            // Verify message
            string actualDuplicateMessage = GetLastestToastMessage();
            if (!string.IsNullOrEmpty(actualDuplicateMessage))
            {
                if (isDuplicate && actualDuplicateMessage != expectedMess)
                {
                    ExtentReportsHelper.LogFail($"<font color = 'red'>Create new Cost Code with duplicate name '{data.Name}' successful. It should be fail." +
                        $"<br>Expected message: {expectedMess}" +
                        $"<br>Actual message: {actualDuplicateMessage}</br></font>");
                }
                else if (!isDuplicate && actualDuplicateMessage != expectedMess)
                {
                    ExtentReportsHelper.LogFail($"<font color = 'red'>Could not create new Cost Code with name '{data.Name}'." +
                        $"<br>Expected message: {expectedMess} " +
                        $"<br>Actual message: {actualDuplicateMessage}</br></font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Create new Cost Code with name {data.Name} successfully. / Can't create a new Cost Code with duplicate name successfully.</b></font>");
                    // if unstable create which will close Modal
                    if (actualDuplicateMessage == $"Not able to create cost code { data.Name} at this time.")
                    {
                        AddCostCodeModal.Close();
                    }
                }
            }
            else
            {
                ExtentReportsHelper.LogWarning($"<font color = 'yellow'>Don't display any message.</font>");
            }
        }

        /// <summary>
        /// Delete Cost Code by Name
        /// </summary>
        /// <param name="costCodeName"></param>
        public void DeleteCostCodesByName(string costCodeName)
        {
            // Select OK to confirm; verify successful delete and appropriate success message.
            DeleteItemInGrid("Name", costCodeName);

            string expectedMess = $"Cost Code {costCodeName} deleted successfully!";
            if (expectedMess == GetLastestToastMessage())
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Cost Code with name '{costCodeName}' deleted successfully!</b></font>");
            else
            {
                // Verify if the item is removed from the grid view
                if (IsItemInGrid("Name", costCodeName) is true)
                    ExtentReportsHelper.LogFail($"<font color = 'red'>Cost Code with name '{costCodeName}' could not be deleted!</font>");
                else
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Delete Cost Code with name '{costCodeName}' successfully!</b></font>");

            }
        }

    }

}
