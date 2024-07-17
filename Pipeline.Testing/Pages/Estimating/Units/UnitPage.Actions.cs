using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Units.AddUnit;

namespace Pipeline.Testing.Pages.Estimating.Units
{
    public partial class UnitPage
    {
        public void ClickAddToShowUnitModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddUnitModal = new AddUnitPage();
            AddUnitModal.IsModalDisplayed();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Unit_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Unit_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Unit_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductUnitTypes']/div[1]");
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            Unit_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public void DeleteUnit(UnitData data)
        {
            ExtentReportsHelper.LogInformation($"Filter new item {data.Name} in the grid view.");
            FilterItemInGrid("Abbreviation", GridFilterOperator.Contains, data.Abbreviation);

            if (IsItemInGrid("Abbreviation", data.Abbreviation))
            {
                // select OK to confirm; verify successful delete and appropriate success message.
                DeleteItemInGrid("Abbreviation", data.Abbreviation);
                WaitGridLoad();
                if ("Unit deleted successfully!" == GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Unit {data.Abbreviation} deleted successfully!</b></font>");
                }
                else
                {
                    if (IsItemInGrid("Abbreviation", data.Abbreviation) is true)
                        ExtentReportsHelper.LogFail($"<font color ='red'>Unit could not be deleted!</font>");
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>Unit {data.Abbreviation} deleted successfully!</b></font>");
                }
            }
            else
                ExtentReportsHelper.LogInformation(null, $"Can't find Unit {data.Abbreviation} to delete.");
        }

        /// <summary>
        /// Create new Unit and verify it
        /// </summary>
        public void CreateUnitAndVerify(string abbName, string name, string expectedMessage)
        {
            // Load simple data from excel and add to model
            AddUnitModal.AddAbbreviation(abbName).AddName(name);

            // 4. Select the 'Save' button on the modal;
            AddUnitModal.Save();
            string _actualMessage = GetLastestToastMessage();
            // Verify successful save and appropriate success message.

            if (expectedMessage.Equals(_actualMessage))
                ExtentReportsHelper.LogPass("Create successful unit. The mesage is same as expected. Actual results: " + _actualMessage);
            else
                ExtentReportsHelper.LogFail("The message isn't as expected. Actual results: " + _actualMessage);
            CloseToastMessage();
        }
    }
}
