using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BuildingGroup.AddBuildingGroup;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup
{
    public partial class BuildingGroupPage
    {
        public void ClickAddToShowBuildingGroupModal()
        {
            Add_btn.Click();
            AddBuildingGroupModal = new AddBuildingGroupModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return BuildingGroup_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingGroup_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingGroups']/div[1]", 3000);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            BuildingGroup_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingGroups']/div[1]");
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            BuildingGroup_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public void EditItemInGrid(string columnName, string value)
        {
            BuildingGroup_Grid.ClickEditItemInGrid(columnName, value);
            PageLoad();
        }

        public void DeleteBuildingGroup(BuildingGroupData oldData)
        {
            DeleteItemInGrid("Code", oldData.Code);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingGroups']");

            string expectedMsg = $"Building Group {oldData.Code} {oldData.Name} deleted successfully!";
            string actualMsg = GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("Building Group deleted successfully!");
                CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(actualMsg))
            {
                if (IsItemInGrid("Code", oldData.Code))
                    ExtentReportsHelper.LogFail("Building Group could not be deleted!");
                else
                    ExtentReportsHelper.LogPass(null, "Building Group deleted successfully!");
            }
        }

        /// <summary>
        /// Create a new Building Group
        /// </summary>
        /// <param name="data"></param>
        public void AddNewBuildingGroup(BuildingGroupData data)
        {
            ClickAddToShowBuildingGroupModal();

            if (AddBuildingGroupModal.IsModalDisplayed() is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Add Building Group modal isn't displayed or title is incorrect." +
                    $"<br>Expected title: 'Add Building Group'</font>");
            else
                ExtentReportsHelper.LogPass(null, "<font color ='green'><b>Add Building Group modal displayed successfully.</b></font>");

            AddBuildingGroupModal.EnterBuildingGroupCode(data.Code)
                .EnterBuildingGroupName(data.Name).EnterBuildingGroupDescription(data.Description);
            AddBuildingGroupModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = $"Building Group {data.Code} {data.Name} saved successfully!";
            string _actualMessage = GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Add new Building Group successfully. The message is displayed as expected.</b></font>");
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail("<font color ='red'>Failed to create new Building Group. The message is NOT display as expected." +
                    $"<br>Actual results: {_actualMessage}" +
                    $"<br>Expected Result: {_expectedMessage}</font>");
                CloseToastMessage();
            }
        }

        public void IsColumnHeaderIndexByName(string columnName)
        {
            BuildingGroup_Grid.IsColumnHeaderIndexByName(columnName);
        }
        public void IsEditAndDeleteFirstItem()
        {
            BuildingGroup_Grid.IsEditFirstItem();
            BuildingGroup_Grid.IsDeleteFirstItem();
        }

        public void IsAddAndUtilitiesButton()
        {
                if(Add_btn.IsDisplayed() is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Add Button isn't displayed or title is incorrect.</font>");
                else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(Add_btn), "<font color ='green'><b>Add Button displayed successfully.</b></font>");

            if (Utilities_btn.IsDisplayed() is false)
                ExtentReportsHelper.LogFail("<font color ='red'>Utilities Button isn't displayed or title is incorrect.</font>");
            else
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(Utilities_btn), "<font color ='green'><b>Utilities Button displayed successfully.</b></font>");

        }

        /// <summary>
        /// Get total number on the grid view
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return BuildingGroup_Grid.GetTotalItems;
        }
        public void DeleteItemSelectedInGrid(int ItemTotal)
        {
            for(int i = 0; i< ItemTotal; i++)
            {
                CheckBox ItemSelected_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgBuildingGroups_ctl00__{i}']//input[@type='checkbox']");
                ItemSelected_chk.Check(true);
            }
            Button DeleteData_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbDeleteBuildingGroups']");
            BulkActions_btn.Click();
            DeleteData_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitGridLoad();
        }
    }
}
