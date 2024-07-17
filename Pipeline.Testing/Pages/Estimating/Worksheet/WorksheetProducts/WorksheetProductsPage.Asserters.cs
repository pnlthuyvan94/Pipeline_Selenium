using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Products;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetProducts
{
    public partial class WorksheetProductsPage
    {
        /*
         * Verify head title and id of new manufacturer
         */
        
        public bool IsSaveWorksheetSuccessful(string worksheetName)
        {
            System.Threading.Thread.Sleep(1000);
            return SubHeadTitle().Equals(worksheetName) && !CurrentURL.EndsWith("hid=0");
        }

        public void VerifyWorksheetBOMValues(string productAttr, ProductData productData, string productValue, bool toTracePage)
        {
            int columnIndex = WorksheetBOMProduct_Grid.GetColumnHeaderIndexByName(productAttr);           
            Label ResultLbl = new Label(FindType.XPath, $"//a[contains(.,'{productData.Name}')]/ancestor::tr[@id = 'ctl00_CPH_Content_rgReport_ctl00_ctl05_Detail10_ctl06_Detail10__0:0_0:0_0']/child::td[{columnIndex + 1}]");
            string resultValue = ResultLbl.GetText().ToString();
            Label TotalQtyBtn = new Label(FindType.XPath, $"//a[contains(.,'{productData.Name}')]/ancestor::tr[1]//td/a[contains(.,'{productData.Quantities}')]");
            Label ProductNameBtn = new Label(FindType.XPath, $"//a[contains(.,'{productData.Name}')]");
            if ((TotalQtyBtn.IsDisplayed(false) || ProductNameBtn.IsDisplayed(false)) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{productData.Name}'</font> ");
            }
            else
            {
                ExtentReportsHelper.LogPass( $"<font color='green'>Product with name '{productData.Name} is shown'</font>");
            }
            if (productValue == resultValue)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>{productAttr} field of produtc is shown matching!'</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail( $"<font color='red'>{productAttr} field of product field is not shown matching!'</font>");
            }

            if (toTracePage == true)
            {
                TotalQtyBtn.Click();
            }
        }


        public bool IsWorksheetProductQuantityInGrid(string columnName, string value)
        {
            int columI = 0;
            bool flag = false;
            ListItem listItem = new ListItem(FindElementHelper.FindElements(FindType.XPath, "//thead/tr[2]/th"));
            for (int i = 0; i < listItem.GetAllItems().Count; i++)
            {
                if (listItem.GetAllItems()[i].Text.Trim() == columnName)
                {
                    columI = i + 1;
                    break;
                }
            }

            ListItem listItem1 = new ListItem(FindElementHelper.FindElements(FindType.XPath, $"//table[@id = 'ctl00_CPH_Content_rgWorksheetProducts_ctl00']/child::tbody[1]/tr/td[{columI}]"));
            for (int i = 0; i < listItem1.GetAllItems().Count; i++)
            {
                if (listItem1.GetAllItems()[i].Text.Trim() == value)
                {
                    flag = true;
                    return true;

                }
            }
            return flag;

        }

    }
}
