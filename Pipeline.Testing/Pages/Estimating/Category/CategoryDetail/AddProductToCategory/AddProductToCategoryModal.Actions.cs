using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Category.CategoryDetail.AddProductToCategory
{
    public partial class AddProductToCategoryModal
    {
        public IList<string> GetListItem(string xpath)
        {
            IList<IWebElement> listElement = driver.FindElements(By.XPath($"{xpath}"));
            IList<string> listItem = new List<string>();
            foreach (var item in listElement)
            {
                string additem = item.Text;
                listItem.Add(additem);
            }
            return listItem;
        }

        public AddProductToCategoryModal AddProductToCategory(string BuildingPhase , string Product)
        {
            BuildingPhase_ddl.SelectItem(BuildingPhase);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rcbProductId']/div[1]");
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(BuildingPhase_ddl), $"The page was selected item <font color='green'><b>{BuildingPhase}</b></font> from the dropdown list.");


            Button Product_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Arrow']");
            Product_btn.Click();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']");
            System.Threading.Thread.Sleep(3000);

            Textbox Product_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Input']");
            Product_txt.SetText(Product);

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']");
            System.Threading.Thread.Sleep(3000);

            Button ProductName_btn = new Button(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']//li)[1]");
            ProductName_btn.WaitUntilExist(5);
            ProductName_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_lpctl00_CPH_Content_rlbProducts']/div[1]", 5000);
            Save();
            return this;
        }

        private void Save()
        {
            AddProduct_btn.Click();
        }

        public void CloseModal()
        {
            Cancel_btn.Click();
        }

        public List<string> GetSpecifiedItemInProductList(int numberOfProduct)
        {
            return ListOfPhase_lst.GetSpecifiedItemsName(numberOfProduct);
        }
    }
}
