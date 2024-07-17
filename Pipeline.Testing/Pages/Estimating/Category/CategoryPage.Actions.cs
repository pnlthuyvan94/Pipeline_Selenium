using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Category
{
    public partial class CategoryPage
    {
        public void ClickAddToShowCategoryModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddCategoryModal = new AddCategory.AddCategoryPage();
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Category_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCategories']", 2000);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return Category_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Category_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            Category_Grid.WaitGridLoad();
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            Category_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public bool CreateNewCategory(string categoryName, string parent)
        {
            bool isCreateSuccessful = false;

            // Step 2: click on "+" Add button
            ClickAddToShowCategoryModal();
            if (AddCategoryModal.IsModalDisplayed() is false)
                ExtentReportsHelper.LogFail("Create Category modal is not displayed.");

            // Step 3: Populate all values
            AddCategoryModal.AddName(categoryName)
                .SetCategoryParent(parent);

            // Create Option Room - Click 'Save' Button
            AddCategoryModal.Save();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCategories']");

            string _expectedMsg = $"Category {categoryName} created successfully!";
            string actualMsg = GetLastestToastMessage();
            if (_expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Create Category with name {categoryName} successfully.");
                CloseToastMessage();
                isCreateSuccessful = true;
            }
            else if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogFail($"Could not create Category with name {categoryName}.");
                isCreateSuccessful = false;
            }
            return isCreateSuccessful;

        }

        public void DeleteCategory(string categoryName)
        {
            // 7. Select item and click deletete icon
            DeleteItemInGrid("Name", categoryName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCategories']//div[1]");

            string successfulMess = $"Category {categoryName} deleted successfully!";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("Category deleted successfully!");
                CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Name", categoryName))
                    ExtentReportsHelper.LogFail("Category could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Category deleted successfully!");
            }
        }

        /// <summary>
        /// Get total item on the Category page
        /// </summary>
        /// <returns></returns>
        public int GetTotalNumberItem()
        {
            return Category_Grid.GetTotalItems;
        }

        public void RemoveFocus()
        {
            // Remove by get sub head title
            IWebElement categoryTile = FindElementHelper.FindElement(FindType.XPath, "//h1[text()='Categories']");
            categoryTile.Click();
        }
    }
}
