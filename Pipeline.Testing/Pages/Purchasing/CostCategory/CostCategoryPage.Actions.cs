using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Purchasing.CostCategory.AddCostCategory;

namespace Pipeline.Testing.Pages.Purchasing.CostCategory
{
    public partial class CostCategoryPage
    {
        public void ClickAddToShowCategoryModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddCostCategoryModal = new AddCostCategoryPage();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            CostCategory_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgCostCategories']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CostCategory_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public void SelectItemInGrid(string columnName, string value)
        {
            CostCategory_Grid.ClickItemInGrid(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            CostCategory_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgCostCategories']/div[1]",2000);
        }

        public void WaitGridLoad()
        {
            CostCategory_Grid.WaitGridLoad();
        }

        /// <summary>
        /// Create new Cost Category and verify it
        /// </summary>
        public void CreateCostCategory(CostCategoryData data)
        {
            ClickAddToShowCategoryModal();

            if (AddCostCategoryModal.IsModalDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Could not open Create Cost Category modal or the title is incorrect</font>.");

            // Create Cost Category - Click 'Save' Button
            AddCostCategoryModal.AddName(data.Name).AddDescription(data.Description).EnterCostType(data.CostType);

            // 4. Select the 'Save' button on the modal;
            AddCostCategoryModal.Save();
            string _actualMessage = GetLastestToastMessage();
            string _expectedMessage = $"Cost Code Category {data.Name} created successfully!";

            if (!string.IsNullOrEmpty(_actualMessage) && _actualMessage != _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Could not Create Cost Category with name <b>{data.Name}</b>.</font>");
                CloseToastMessage();
            }
            else
            {
                FilterItemInGrid("Name", GridFilterOperator.Contains, data.Name);
                if (IsItemInGrid("Name", data.Name) is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Create Cost Category with name {data.Name} successfully.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'></font>Can't find Cost Category with name {data.Name} on the grid view." +
                        $"<br>Failed to create Cost Type.</br></font>");
            }
        }

        /// <summary>
        /// Delete Cost Category by Name
        /// </summary>
        /// <param name="costCategoryName"></param>
        public void DeleteCostCategoryByName(string costCategoryName)
        {
            DeleteItemInGrid("Name", costCategoryName);
            WaitGridLoad();

            string successfulMess = $"Cost Code Category {costCategoryName} deleted successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Cost Category '{costCategoryName}' deleted successfully!</b></font>");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Name", costCategoryName))
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Cost Type '{costCategoryName}' could not be deleted!</font>");
                else
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Cost Type '{costCategoryName}' deleted successfully!</b></font>");
            }
        }
    }
}
