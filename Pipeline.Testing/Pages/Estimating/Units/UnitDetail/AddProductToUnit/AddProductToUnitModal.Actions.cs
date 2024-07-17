using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Units.UnitDetail.AddProductToUnit
{
    public partial class AddProductToUnitModal
    {
        public AddProductToUnitModal AddProductToUnit(IList<string> productList)
        {
            Button OpenArrow_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbProductId_Arrow']");
            OpenArrow_btn.Click();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']");
            foreach (string item in productList)
            {
                // Select Product
                IWebElement ProductValue_element = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Input']");
                ProductValue_element.SendKeys(Keys.Control + "A");
                ProductValue_element.SendKeys(Keys.Delete);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']");
                ProductValue_element.SendKeys(item);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']");

                CheckBox Product_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']//ul//li/label/em[contains(text(),'{item}')]//ancestor::ul//input");

                if (Product_chk.IsDisplayed() is true)
                {
                    Product_chk.SetCheck(true);
                }
            }
                Save();

            return this;
        }


        private void Save()
        {
            AddProductToUnit_btn.Click();
            // Loading grid
            WaitingLoadingGifByXpath(loadingIcon);
        }

        public void CloseModal()
        {
            Cancel_btn.Click();
        }

        public List<string> GetSpecifiedItemInProductList(int numberOfProduct)
        {
            return ListOfProduct_lst.GetSpecifiedItemsName(numberOfProduct);
        }
    }
}
