using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Units.UnitDetail.AddProductToUnit;

namespace Pipeline.Testing.Pages.Estimating.Units.UnitDetail
{
    public partial class UnitDetailPage
    {
        private UnitDetailPage EnterUnitName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }
        private UnitDetailPage EnteAbbreviation(string abbreviation)
        {
            Abbreviation_txt.SetText(abbreviation);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            // Loading grid
            WaitingLoadingGifByXpath(loadingIcon, 5);
        }

        public void UpdateUnit(UnitData data)
        {
            EnterUnitName(data.Name).EnteAbbreviation(data.Abbreviation).Save();
        }

        public void OpenAddProductToUnitModal()
        {
            AddProductToUnit_btn.Click();
            AddProductToUnitModal = new AddProductToUnitModal();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return ProductDetail_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            ProductDetail_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgProducts']/div[1]");
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            ProductDetail_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rgProducts']/div[1]");
            var expectedMessage = "Product successfully removed.";
            var actualMessage = UnitDetailPage.Instance.GetLastestToastMessage();

            if (actualMessage == expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Product {value} deleted successfully!");
                UnitDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                if (UnitDetailPage.Instance.IsItemInGrid("Product Name", value))
                    ExtentReportsHelper.LogFail($"Product {value} can't be deleted!");
                else
                    ExtentReportsHelper.LogPass($"Product {value} deleted successfully!");

            }
        }

        public void WaitGridLoad()
        {
            ProductDetail_Grid.WaitGridLoad();
        }
        public void CloseModal()
        {
            CloseModal_btn.Click();
        }

        public void CheckFunctionalProductModal()
        {
            OpenAddProductToUnitModal();

            //verify Product dropdown field
            Textbox ProductControl_Input = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Input']");
            if (ProductControl_Input.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product field is displayed</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product field is not display</font color>");
            }

            //Product field then verify the list of products that is shown   
            Button product_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Arrow']");
            product_btn.Click(false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']");

            IWebElement ProductControl_ArrrProduct = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']");
            if (ProductControl_ArrrProduct.Displayed)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product dropdown field is displayed</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product dropdown field is not display</font color>");
            }

            //The product description and distinguish the “Product Name” and “Product Description” by a dash “-” character
            Button Product_btn = new Button(FindType.XPath, $"(//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']//li/label)[1]");
            Product_btn.WaitForElementIsVisible(5);
            Product_btn.RefreshWrappedControl();
            if (Product_btn.IsDisplayed() && Product_btn.GetText().Contains("-") is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product Name field is contain {"-"} character </font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product Name field is not contain {"-"} character </font color>");
            }

            //Close Modal
            CloseModal();

        }
    }
}
