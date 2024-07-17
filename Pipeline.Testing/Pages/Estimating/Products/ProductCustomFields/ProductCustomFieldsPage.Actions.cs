using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductCustomFields
{
    public partial class ProductCustomFieldsPage
    {

        public void ClickAdd_btn(string type)
        {
            Add_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddNew']/div[1]");
            System.Threading.Thread.Sleep(2000);
            //span[contains(text(),'Checkbox')]//..//input
            Button checkbox = new Button(FindType.XPath, "//span[contains(text(),'"+type+"')]//..//input");
            checkbox.Click();
            Insert_btn.Click();
            System.Threading.Thread.Sleep(5000);

        }
        public void Add_btn_Click()
        {
            Add_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddNew']/div[1]");
            System.Threading.Thread.Sleep(2000);
        }
        public void Close_ModalCustomFields_Click()
        {
            Close_btn.Click();
            System.Threading.Thread.Sleep(2000);
        }

        public void ClickRemove_btn(string type)
        {
            Remove_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbRemove']/div[1]");
            System.Threading.Thread.Sleep(2000);
            //span[contains(text(),'Checkbox')]//..//input
            Button checkbox = new Button(FindType.XPath, $"//*[contains(text(),'{type}')]//..//a");
            checkbox.Click();
            System.Threading.Thread.Sleep(1000);

        }
        public void Remove_btn_Click()
        {
            Remove_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbRemove']/div[1]");
            System.Threading.Thread.Sleep(2000);
        }

            public void ClickSave_btn()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave']/div[1]");
        }

        public void WaitConvertFromGridLoad()
        {
            ConvertFrom_Grid.WaitGridLoad();
        }

        public void EditConverFrom(string columnName, string valueToFind)
        {
            ConvertFrom_Grid.ClickEditItemInGrid( columnName, valueToFind);
            WaitConvertFromGridLoad();
            if (Accept_btn.WaitForElementIsVisible(10))
            {
                Accept_btn.Click();
                WaitConvertFromGridLoad();
            }
            else
                ExtentReportsHelper.LogInformation("Not accpet item be edited");      
        }

        public void DeleteConverFrom(string columnName, string valueToFind)
        {
            ConvertFrom_Grid.ClickDeleteItemInGrid (columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            ConvertFrom_Grid.WaitGridLoad();
           
        }

        public void WaitConvertToGridLoad()
        {
            ConvertTo_Grid.WaitGridLoad();
        }

        public void EditConverTo(string columnName, string valueToFind)
        {
            ConvertTo_Grid.ClickEditItemInGrid(columnName, valueToFind);
            WaitConvertFromGridLoad();
            if (Accept_btn.WaitForElementIsVisible(10))
            {
                Accept_btn.Click();
                WaitConvertToGridLoad();
            }
            else
                ExtentReportsHelper.LogInformation("Not accpet item be edited");
        }

        public void DeleteConverTo(string columnName, string valueToFind)
        {
            ConvertTo_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            ConvertTo_Grid.WaitGridLoad();

        }

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

      
        public float GetTotalItemOnStyle()
        {
            System.Threading.Thread.Sleep(1000);
            return ConvertTo_Grid.GetTotalItems;
        }
        public float GetTotalItemOnCatelogy()
        {
            CommonHelper.ScrollToBeginOfPage();
            System.Threading.Thread.Sleep(1000);
            return ConvertTo_Grid.GetTotalItems;
        }
    }

}
