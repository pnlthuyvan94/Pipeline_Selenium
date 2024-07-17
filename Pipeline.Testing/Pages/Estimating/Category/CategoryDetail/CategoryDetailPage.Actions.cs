using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Category.CategoryDetail.AddProductToCategory;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Category.CategoryDetail
{
    public partial class CategoryDetailPage
    {
        private CategoryDetailPage EnterName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                CategoryName_txt.SetText(name);
            return this;
        }

        private CategoryDetailPage EnterParent(string parent)
        {
            if (!string.IsNullOrEmpty(parent))
                Parent_ddl.SelectItem(parent, false, false);
            return this;
        }

        private void Save()
        {
            Save_btn.Click();
        }

        public CategoryDetailPage UpdateCategory(CategoryData data)
        {
            EnterName(data.Name).EnterParent(data.Parent).Save();
            WaitingLoadingGifByXpath(loadingIcon);
            return this;
        }

        public CategoryDetailPage AddProductToCategory()
        {
            AddProduct_btn.Click();
            AddProductToCategoryModal = new AddProductToCategoryModal();
            return this;
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgProducts']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return AddProduct_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            AddProduct_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgProducts']", 2000);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            Button delete = new Button(FindType.XPath, $"//a[contains(text(),'{value}')]/parent::td//following-sibling::td/input[contains(@src,'delete')]");
            delete.Click();
            //AddProduct_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void DeleteAllProduct()
        {
            // Select all product
            AllCheckBox_cb.SetCheck(true);

            // Click Delete all item
            BulkAction_btn.Click();
            DeleteAll_btn.WaitForElementIsVisible(3);

            DeleteAll_btn.Click();
            ConfirmDialog(ConfirmType.OK);
        }

        public void RemoveAllProductFromCategory(List<string> productList)
        {
            // There are no product then don't need to delete it.
            if (AddProduct_Grid.GetTotalItems == 0)
            {
                ExtentReportsHelper.LogInformation($"There is no Products on the grid view to delete.");
                return;
            }

            // Click Bulk action
            DeleteAllProduct();
            WaitGridLoad();

            var expectedMessage = "selected Products removed from this Category.";
            var actualMessage = GetLastestToastMessage();

            if (actualMessage.EndsWith(expectedMessage))
            {
                ExtentReportsHelper.LogPass($"All products deleted successfully!");
                CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"The toast message isn't ended same as the expectation." +
                    $"<br>Expected message: {expectedMessage}" +
                    $"<br>Actual message: {actualMessage} </br>");

                if (productList != null && productList.Count != 0)
                {
                    foreach (string product in productList)
                    {
                        if (CategoryPage.Instance.IsItemInGrid("Product Name", product))
                            ExtentReportsHelper.LogFail($"Product {product} didn't delete from current Category.");
                    }
                }

            }
        }

        public int getNumberProductOnGrid()
        {
            return AddProduct_Grid.GetTotalItems;
        }

        /// <summary>
        /// Remove focus from More menu
        /// </summary>
        public void RemoveFocusFromMoreMenu()
        {
            // Remove by get sub head title
            IWebElement productTile = FindElementHelper.FindElement(FindType.XPath, "//h1[text()='Products']");
            productTile.Click();
        }


        public void TestFunctionalProduct(string value)
        {
            //verify Product dropdown field
            IWebElement ProductControl_Input = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_radProductControl_Input']");
            if (ProductControl_Input.Displayed)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product dropdown field is displayed</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='green'>Product dropdown field is not display</font color>");
            }

            //Product field then verify the list of products that is shown

            IWebElement ProductControl_ArrrProduct = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_radProductControl_Arrow']");
            if (ProductControl_ArrrProduct.Displayed)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product dropdown field is displayed</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='green'>Product dropdown field is not display</font color>");
            }

            //The product description and distinguish the “Product Name” and “Product Description” by a dash “-” character
            Button Product_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_radProductControl_DropDown']//ul//li/label/em[contains(text(),'{value}')]");
            ProductControl_Input.SendKeys(value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_radProductControl_LoadingDiv']", 3000);
            Product_btn.WaitForElementIsVisible(20);
            if (Product_btn.IsDisplayed() && Product_btn.GetText().Contains("-") is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product Name field is contain {"-"} character </font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product Name field is not contain {"-"} character </font color>");
            }

            //Show a tooltip that provides the full information when hovering over the item

            if (Product_btn.IsDisplayed() && Product_btn.GetText().Equals(value) is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product Name field is contain the full information</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product Name field is not contain the full information</font color>");
            }

            //Enter the value to filter product
            ProductControl_Input.SendKeys(value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_radProductControl_LoadingDiv']", 3000);
            if (Product_btn.IsDisplayed() is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product Name field is filtered successfully</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product Name field is not filter successfully/font color>");
            }

            //verify the list of products


        }
    }
}
