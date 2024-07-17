using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Calculations.CalculationModal;

namespace Pipeline.Testing.Pages.Estimating.Calculations
{
    public partial class CalculationPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Calculation_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCalcs']/div[1]", 2000);

        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Calculation_Grid.IsItemOnCurrentPage(columnName, value);
        }

        private void DeleteItemInGrid(string columnName, string value)
        {
            Calculation_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCalcs']/div[1]");
        }

        public void WaitGridLoad()
        {
            Calculation_Grid.WaitGridLoad();
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            Calculation_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        private void ClickAddToOpenCalculationModal()
        {
            //PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddCalculationModal = new AddCalculationModal();
        }

        public void CreateNewCalculation(CalculationData data)
        {
            // Click on "+" Add button
            ClickAddToOpenCalculationModal();
            if (!AddCalculationModal.IsModalDisplayed())
                ExtentReportsHelper.LogFail(null, "<font color = 'red'>Add Calculation modal is not displayed or title modal is incorrect.</font>");

            // Create Building Phase Rule - Click 'Save' Button
            AddCalculationModal
                           .EnterDescription(data.Description)
                           .EnterCalculation(data.Calculation)
                           .Save();

            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = "Calculation added successfully.";
            if (_actualMessage != _expectedMessage && !string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"<font color = 'red'>Could not create Calculation with description {data.Description} and calculation {data.Calculation}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass($"<font color = 'green'><b>Create Calculation with description {data.Description} and calculation {data.Calculation} sucessfully.</b>/font>");
            }
            CloseToastMessage();
        }

        public void DeleteCalculation(CalculationData calculationData)
        {
            DeleteItemInGrid("Description", calculationData.Description);
            //WaitGridLoad();

            string successfulMess = $"Calculation deleted successfully.";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("<font color = 'green'><b>Calculation deleted successfully!</b></font>");
                CloseToastMessage();
            }
            else
            {
                //Delete Calculation again
                CommonHelper.RefreshPage();
                FilterItemInGrid("Description", GridFilterOperator.EqualTo, calculationData.Description);
                DeleteItemInGrid("Description", calculationData.Description);
                if (IsItemInGrid("Description", calculationData.Description))
                    ExtentReportsHelper.LogFail("<font color = 'red'>Calculation could not be deleted!</font>");
                else
                    ExtentReportsHelper.LogPass("<font color = 'green'><b>Calculation deleted successfully!</b></font>");
            }
        }

    }

}
