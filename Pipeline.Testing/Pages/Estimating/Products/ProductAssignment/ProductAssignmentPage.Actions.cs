using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductAssignment
{
    public partial class ProductAssignmentPage
    {

        public void WaitConvertFromGridLoad()
        {
            ConvertFrom_Grid.WaitGridLoad();
        }
        public ProductAssignmentPage SelectConvertFromStyle(string style)
        {
            CovertFromStyle_ddl.SelectItemByValueOrIndex(style, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(CovertFromStyle_ddl), $"Select ConvertFromStyle: {style}");
            return this;
        }
        public ProductAssignmentPage SelectConvertFromSpecSetGroup(string specsetgroup)
        {
            ConvertFromSpecSetGroup_ddl.SelectItemByValueOrIndex(specsetgroup, 0);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]", 4000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromSpecSetGroup_ddl), $"Select SpecSetGroup: {specsetgroup} in ConvertFrom Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertFromSpecSet(string specset)
        {
            ConvertFromSpecSet_ddl.SelectItemByValueOrIndex(specset, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromSpecSet_ddl), $"Select SpecSet: {specset} in ConvertFrom Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertFromUse(string use)
        {
            ConvertFromUse_ddl.SelectItemByValueOrIndex(use, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromUse_ddl), $"Select Use {use} in ConvertFrom Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertFromBuildingGroup(string buildinggroup)
        {
            ConvertFromBuildingGroup_ddl.SelectItemByValueOrIndex(buildinggroup, 1);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]", 4000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromBuildingGroup_ddl), $"Select BuildingGroup: {buildinggroup} in ConvertFrom Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertFromBuildingPhase(string buildingphase)
        {
            ConvertFromBuildingPhase_ddl.SelectItemByValueOrIndex(buildingphase, 1);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]", 4000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromBuildingPhase_ddl), $"Select BuildingPhase: {buildingphase} in ConvertFrom Modal");
            return this;
        }

        public ProductAssignmentPage SelectConvertFromProduct(string product)
        {
            ConvertFromProduct_ddl.SelectItemByValueOrIndex(product, 1);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]", 4000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromProduct_ddl), $"Select Product: {product} in ConvertFrom Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertFromNewStyle(string newstyle)
        {
            ConvertFromNewStyle_ddl.SelectItemByValueOrIndex(newstyle, 0);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]");
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromNewStyle_ddl), $"Select NewStyle: {newstyle} in ConvertFrom Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertFromNewUse(string newuse)
        {
            ConvertFromNewUse_ddl.SelectItemByValueOrIndex(newuse, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromNewUse_ddl), $"Select NewUse: {newuse} in ConvertFrom Modal");
            return this;
        }

        public ProductAssignmentPage SelectConvertFromCalculation(string calculation)
        {
            ConvertFromCalculation_ddl.SelectItemByValueOrIndex(calculation, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertFromCalculation_ddl), $"Select Calculation: {calculation} in ConvertFrom Modal");
            return this;
        }

        public ProductAssignmentPage SelectConvertToNewStyle(string newstyle)
        {
            CovertToSNewtyle_ddl.SelectItemByValueOrIndex(newstyle, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(CovertToSNewtyle_ddl), $"Select NewStyle: {newstyle} in  ConvertTo Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertToSpecSetGroup(string specsetgroup)
        {
            ConvertToSpecSetGroup_ddl.SelectItemByValueOrIndex(specsetgroup, 0);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl01']/div[1]", 4000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToSpecSetGroup_ddl), $"Select SpecSetGroup: {specsetgroup} in  ConvertTo Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertToSpecSet(string specset)
        {
            ConvertToSpecSet_ddl.SelectItemByValueOrIndex(specset, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToSpecSet_ddl), $"Select SpecSet: {specset} in  ConvertTo Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertToNewUse(string newuse)
        {
            ConvertToNewUse_ddl.SelectItemByValueOrIndex(newuse, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToNewUse_ddl), $"Select NewUse: {newuse} in  ConvertTo Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertToBuildingGroup(string buildinggroup)
        {
            ConvertToBuildingGroup_ddl.SelectItemByValueOrIndex(buildinggroup, 1);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl01']/div[1]", 4000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToBuildingGroup_ddl), $"Select BuildingGroup: {buildinggroup} in  ConvertTo Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertToBuildingPhase(string buildingphase)
        {
            ConvertToBuildingPhase_ddl.SelectItemByValueOrIndex(buildingphase, 1);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl01']/div[1]", 2000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToBuildingPhase_ddl), $"Select BuildingPhase: {buildingphase} in  ConvertTo Modal");
            return this;
        }

        public ProductAssignmentPage SelectConvertToProduct(string product)
        {
            ConvertToProduct_ddl.SelectItemByValueOrIndex(product, 1);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl01']/div[1]", 5000);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToProduct_ddl), $"Select Product: {product} in  ConvertTo Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertToStyle(string style)
        {
            ConvertToStyle_ddl.SelectItemByValueOrIndex(style, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToStyle_ddl), $"Select Style: {style} in  ConvertTo Modal");
            return this;
        }
        public ProductAssignmentPage SelectConvertToUse(string use)
        {
            ConvertToUse_ddl.SelectItemByValueOrIndex(use, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToUse_ddl), $"Select Use: {use} in  ConvertTo Modal");
            return this;
        }

        public ProductAssignmentPage SelectConvertToCalculation(string calculation)
        {
            ConvertToCalculation_ddl.SelectItemByValueOrIndex(calculation, 0);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ConvertToCalculation_ddl), $"Select Calculation: {calculation}i n  ConvertTo Modal");
            return this;
        }

        public void AddConvertFrom(ProductAssignmentData productAssignmentData)
        {
            SelectConvertFromStyle(productAssignmentData.ConvertFromStyle)
                   .SelectConvertFromSpecSetGroup(productAssignmentData.ConvertFromSpecSetGroup)
                   .SelectConvertFromSpecSet(productAssignmentData.ConvertFromSpetSet)
                   .SelectConvertFromUse(productAssignmentData.ConvertFromUse)
                   .SelectConvertFromBuildingGroup(productAssignmentData.ConverFromBuildingGroup)
                   .SelectConvertFromBuildingPhase(productAssignmentData.ConvertFromBuildingPhase)
                   .SelectConvertFromProduct(productAssignmentData.ConvertFromProduct)
                   .SelectConvertFromNewStyle(productAssignmentData.ConvertFromNewStyle)
                   .SelectConvertFromNewUse(productAssignmentData.ConvertFromNewUse)
                   .SelectConvertFromCalculation(productAssignmentData.ConvertFromCalculation);
                    SavebtnConvertFrom.Click();
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]");
        }
        public void AddConvertTo(ProductAssignmentData productAssignmentData)
        {
            SelectConvertToNewStyle(productAssignmentData.ConvertToNewStyle)
                   .SelectConvertToSpecSetGroup(productAssignmentData.ConvertToSpecSetGroup)
                   .SelectConvertToSpecSet(productAssignmentData.ConvertToSpecSet)
                   .SelectConvertToNewUse(productAssignmentData.ConvertToNewUse)
                   .SelectConvertToBuildingGroup(productAssignmentData.ConvertToBuildingGroup)
                   .SelectConvertToBuildingPhase(productAssignmentData.ConvertToBuildingPhase)
                   .SelectConvertToProduct(productAssignmentData.ConvertToProduct)
                   .SelectConvertToStyle(productAssignmentData.ConverToStyle)
                   .SelectConvertToUse(productAssignmentData.ConvertToUse)
                   .SelectConvertToCalculation(productAssignmentData.ConvertToCalculation);
                   SavebtnConvertTo.Click();
                   WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl01']/div[1]");
        }

        public void EditConverFrom(string columnName, string valueToFind)
        {
            ConvertFrom_Grid.ClickEditItemInGrid( columnName, valueToFind);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]");
            if (Accept_btn.WaitForElementIsVisible(10))
            {
                Accept_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl02']/div[1]");
            }
            else
                ExtentReportsHelper.LogInformation("Not accpet item be edited");      
        }

        public void DeleteConverFrom(string columnName, string valueToFind)
        {
            ConvertFrom_Grid.ClickDeleteItemInGrid (columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgConvertFrom']/div[1]");
           
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
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl01']/div[1]");

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
