using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Costing.Vendor.BaseCosts.AddBaseCost;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Costing.Vendor.BaseCosts
{
    public partial class BaseCostPage
    {
        public void ClickAddNewBaseCost()
        {
            Add_btn.Click();
            AddBaseCostModal = new AddBaseCostModal();
            if(AddBaseCostModal.IsModalDisplayed())
            {
                ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>Add Cost modal is displayed.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Add Cost modal is not displayed.</b></font>");
            }
        }

        public void AddNewBaseCost(string product, string style, string materialCost, string laborCost)
        {
            AddBaseCostModal.SelectProduct(product)
                .SelectStyle(style)
                .EnterMaterialCost(materialCost)
                .EnterLaborCost(laborCost)
                .AddCost();

        }

        public void AddAllAvailableStyles(string product, string style, string materialCost, string laborCost)
        {
            AddBaseCostModal.SelectProduct(product)
                .SelectStyle(style)
                .EnterMaterialCost(materialCost)
                .EnterLaborCost(laborCost)
                .AddAllStyles();
        }

        public void DeleteItemInGrid(string productCode)
        {
            Costs_Grid.ClickDeleteItemInGrid("Code", productCode);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(_gridLoading, 1000);
        }

        public bool IsProductBaseCostInGrid(string columnName, string valueToFind)
        {
            return Costs_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        public void EditBaseCost(string productCode, string materialCost, string laborCost)
        {
            IWebElement webElement = driver.FindElement(By.XPath("//table[@id='ctl00_CPH_Content_rgCosts_ctl00']/thead/tr[2]/th[6]"));
            
            Costs_Grid.SelectGridRow("Code", productCode);
            Textbox materialCost_txt = new Textbox(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgCosts_ctl00']/tbody/tr/td/div/input[contains(@id,'MaterialCosts_txtMUnitPrice')]");
            materialCost_txt.Clear();    
            materialCost_txt.SetText(materialCost);
            webElement.Click();
            CommonHelper.CaptureScreen();

            Costs_Grid.SelectGridRow("Code", productCode);
            Textbox laborCost_txt = new Textbox(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgCosts_ctl00']/tbody/tr/td/div/input[contains(@id,'LaborCosts_txtLUnitPrice')]");
            laborCost_txt.Clear();
            laborCost_txt.SetText(laborCost);
            webElement.Click();
            CommonHelper.CaptureScreen();

          

            Button saveBtn = new Button(FindType.Xpath, "//table[@id='ctl00_CPH_Content_rgCosts_ctl00'" +
                "]/thead/tr[1]/td/table/tbody/tr/td/a[contains(@id,'SaveChangesButton')]");
            saveBtn.Click();
            CommonHelper.CaptureScreen();
            WaitingLoadingGifByXpath(_gridLoading, 1000);
            CommonHelper.CaptureScreen();
        }
        
    }
}
