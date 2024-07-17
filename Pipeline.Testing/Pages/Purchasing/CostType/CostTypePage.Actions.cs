using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.CostType.AddCostType;

namespace Pipeline.Testing.Pages.Purchasing.CostType
{
    public partial class CostTypePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CostType_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(_gridLoading, 500);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CostType_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            CostType_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            CostType_Grid.ClickItemInGrid(columnName, value);
        }

        public void WaitGridLoad()
        {
            CostType_Grid.WaitGridLoad();
        }

        public void ClickAddToOpenCreateCostTypeModal()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddCostTypeModal = new AddCostTypeModal();
            System.Threading.Thread.Sleep(500);
        }

        /// <summary>
        /// Create a new Cost Type
        /// </summary>
        /// <param name="data"></param>
        public void CreateCostType(CostTypeData data)
        {
            ClickAddToOpenCreateCostTypeModal();
            if (AddCostTypeModal.IsModalDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Could not open Create Cost Type modal or the title is incorrect</font>.");

            // Create Cost Type - Click 'Save' Button
            AddCostTypeModal.AddCostType(data);

            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = $"Cost Code Type {data.Name} created successfully!";
            if (!string.IsNullOrEmpty(_actualMessage) &&_actualMessage != _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Could not Create Cost Type with name <b>{ data.Name}</b>.</font>");
                CloseToastMessage();
            }
            else
            {
                FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (IsItemInGrid("Name", data.Name) is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Create Cost Type with name {data.Name} successfully.</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, $"<font color='red'></font>Can't find Cost Type with name {data.Name} on the grid view." +
                        $"<br>Failed to create Cost Type.</br></font>");
            }
        }

        /// <summary>
        /// Delete Cost Type by Name
        /// </summary>
        /// <param name="costTypeName"></param>
        public void DeleteCostType(string costTypeName)
        {
            DeleteItemInGrid("Name", costTypeName);
            WaitGridLoad();

            string successfulMess = $"Cost Code Type {costTypeName} deleted successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Cost Type deleted successfully!</b></font>");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Name", costTypeName))
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Cost Type '{costTypeName}' could not be deleted!</font>");
                else
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Cost Type '{costTypeName}' deleted successfully!</b></font>");
            }
        }

    }

}
