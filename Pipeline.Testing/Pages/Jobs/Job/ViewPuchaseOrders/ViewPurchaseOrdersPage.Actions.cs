using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;


namespace Pipeline.Testing.Pages.Jobs.Job.ViewPuchaseOrders
{
    public partial class ViewPurchaseOrdersPage
    {

    /// <summary>
    /// Check Zero Items is show in Report
    /// </summary>
    /// <param name="BuildingPhase"></param>
    /// <param name="PO"></param>
        public void ViewPurchaseOrderInGridAndVerify(string BuildingPhase,string URL, string PO)
        {
            Button ExpandBuildingPhase_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']//span[contains(text(),'{BuildingPhase}')]//ancestor::tr/td[@class='rgExpandCol']/input");
            Button ExpandPO_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']//td[contains(text(),'{PO}')]//ancestor::tr/td[@class='rgExpandCol']/input");
            Button ViewPuchaseOrder_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']//td[contains(text(),'{PO}')]//../following-sibling::td/a[@class='viewModal']");
            ExpandBuildingPhase_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView']/div[1]",2000);
            ExpandPO_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView']/div[1]",2000);
            NoRecords_txt.WaitForElementIsVisible(5);
            if (NoRecords_txt.IsDisplayed())
            {
                ExtentReportsHelper.LogInformation("Zero Cost Items don't show in grid");
                ViewPuchaseOrder_btn.Click();
                PageLoad(30);
                CommonHelper.SwitchLastestTab();               
                VerifyViewPurchaseOrderReportIsDisplay(URL, PO);
                VerifyItemIsShowInReport();
            }
            else
            {
                ExtentReportsHelper.LogInformation("Zero Cost Items is show in grid");
                ViewPuchaseOrder_btn.Click();
                PageLoad(30);
                CommonHelper.SwitchLastestTab();
                VerifyViewPurchaseOrderReportIsDisplay(URL, PO);
                VerifyItemIsShowInReport();
            }
        }

        public void VerifyViewPurchaseOrderReportIsDisplay(string URL, string PO)
        {
            if(CurrentURL.Contains(URL))
            {
                CommonHelper.SwitchFrame("ctl00_CPH_Content_rvPurchaseOrderReportFrame");
                if (PurchaseOrder_lbl.IsDisplayed() && PurchaseOrder_lbl.GetText().Equals("Purchase Order " + PO))
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>The Purchase Order Report was displayed in Purchase Order report</b></font>");
                }
            }           

            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The Purchase Order Report wasn't display correct in Purchase Order report</font>");
            }
        }

        public void VerifyItemIsShowInReport()
        {
            if(Product1_txt.IsDisplayed() && Product2_txt.IsDisplayed())
            {
                ExtentReportsHelper.LogInformation("Zero Cost Items is show in report.");
            }
            else
            {
                ExtentReportsHelper.LogInformation("Zero Cost Items isn't show in report.");
            }
        }

        public void VerifyPOStatus(string buildingPhase, bool isSageRunning, string expectedStatus)
        {
            CommonHelper.RefreshPage();
            string poStatus = string.Empty;
            SelectView("Phase");
            System.Threading.Thread.Sleep(5000);
            FilterItemInGrid("Building Phase", GridFilterOperator.EqualTo, buildingPhase);
            System.Threading.Thread.Sleep(5000);
            //CommonHelper.CaptureScreen();
            if (IsItemInGrid("Building Phase", buildingPhase) is true)
            {
                string xpath = $"//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']/thead/tr/th/input";
                Button collapseGridButton = new Button(FindType.XPath, xpath);
                collapseGridButton.Click();
                CommonHelper.CaptureScreen();

                IWebElement statusElement = driver.FindElement(By.XPath("//table[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10']/tbody/tr/td[6]"));
                poStatus = statusElement.Text;
                if (poStatus == expectedStatus)
                    ExtentReportsHelper.LogPass(null, "<font color='green'><b>The PO Status is " + expectedStatus + ".</b></font>");
                else
                    ExtentReportsHelper.LogFail(null, "<font color='red'><b>The PO Status is " + poStatus + " but the expected is " + expectedStatus + ".</b></font>");

                if(isSageRunning)
                {
                    IWebElement labelElement = driver.FindElement(By.XPath("//table[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10']/tbody/tr/td[2]/label"));
                    
                    if (labelElement != null)
                    {
                        string svgElement = labelElement.GetAttribute("innerHTML");
                        if(!string.IsNullOrEmpty(svgElement) && svgElement.ToLower().Contains("sent to accounting"))
                            ExtentReportsHelper.LogPass(null, "<font color='lavender'><b>The sent to accounting icon is displayed.</b></font>");
                    }
                }
                else
                {
                    ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Sync to sage is disbled.</b></font>");
                }
            }
        }


        public bool IsItemInGrid(string columnName, string value)
        {
            return ViewPuchaseOrder_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            ViewPuchaseOrder_Grid.FilterByColumn(columnName, gridFilterOperator, value);
        }

        public void SelectView(string view)
        {
            ViewBy_ddl.SelectItem(view, false);
            System.Threading.Thread.Sleep(5000);
        }

        public void VerifyProductDetails(string expectedQuantity, string expectedUnitCost, string expectedSubtotal
            ,  string expectedTax, string expectedTotalCost)
        {
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10']/thead/tr/th/input";
            Button collapseDetailGridButton = new Button(FindType.XPath, xpath);
            collapseDetailGridButton.Click();

            SpecificControls control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10_ctl06_Detail10']/tbody/tr[2]/td[6]");
            string actualQuantity = control.GetAttribute("innerHTML");
            if (actualQuantity.Equals(expectedQuantity))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Quantity value of " + expectedQuantity + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The expected Quantity value of " + expectedQuantity + " is NOT displayed. </b></font>");
            }

            control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10_ctl06_Detail10']/tbody/tr[2]/td[7]");
            string actualUnitCost = control.GetAttribute("innerHTML");
            if (actualUnitCost.Equals(expectedUnitCost))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Unit Cost value of " + expectedUnitCost + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The expected Unit Cost value of " + expectedUnitCost + " is NOT displayed. </b></font>");
            }

            control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10_ctl06_Detail10']/tbody/tr[2]/td[8]");
            string actualSubTotal = control.GetAttribute("innerHTML");
            if (actualSubTotal.Equals(expectedSubtotal))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Sub Total value of " + expectedSubtotal + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The expected Sub Total value of " + expectedSubtotal + " is NOT displayed. </b></font>");
            }

            control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10_ctl06_Detail10']/tbody/tr[2]/td[9]");
            string actualTax = control.GetAttribute("innerHTML");
            if (actualTax.Equals(expectedTax))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Tax  value of " + expectedTax + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The expected Tax value of " + expectedTax + " is NOT displayed. </b></font>");
            }

            control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10_ctl06_Detail10']/tbody/tr[2]/td[10]");
            string actualTotalCost = control.GetAttribute("innerHTML");
            if (actualTotalCost.Equals(expectedTotalCost))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Total Cost value of " + expectedTotalCost + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The expected Total Cost value of " + expectedTotalCost + " is NOT displayed. </b></font>");
            }

        }

        public void ChangePOStatus(string buildingPhase, string newStatus)
        {
            FilterItemInGrid("Building Phase", GridFilterOperator.EqualTo, buildingPhase);
            System.Threading.Thread.Sleep(5000);
            if (IsItemInGrid("Building Phase", buildingPhase) is true)
            {
                string xpath = $"//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']/thead/tr/th/input";
                Button collapseGridButton = new Button(FindType.XPath, xpath);
                collapseGridButton.Click();

                //CommonHelper.CaptureScreen();
                Button PO_chk = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10']//tbody//tr[1]//td[2]//input");
                PO_chk.Click();
                ChangeStatus_btn.Click();
                System.Threading.Thread.Sleep(5000);
                ChangeStatusModal = new ChangeStatus.ChangeStatusModal();
                //CommonHelper.CaptureScreen();
                ChangeStatusModal.ChangeStatus(newStatus);
                //CommonHelper.CaptureScreen();
            }
        }

        public void ValidateOneTimeProductCount(string buildingPhase, string productName, int expectedProductCount)
        {
            FilterItemInGrid("Building Phase", GridFilterOperator.EqualTo, buildingPhase);
            System.Threading.Thread.Sleep(5000);
            if (IsItemInGrid("Building Phase", buildingPhase) is true)
            {
                string xpath = $"//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']/thead/tr/th/input";
                Button collapseGridButton = new Button(FindType.XPath, xpath);
                collapseGridButton.Click();

                Button collapseProductGridButton = new Button(FindType.XPath, $"//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00_ctl05_Detail10']/tbody/tr[1]/td[1]/input");
                collapseProductGridButton.Click();
                //CommonHelper.CaptureScreen();               

                var rows = driver.FindElements(By.XPath("//table[@id = 'ctl00_CPH_Content_rgJobPurchaseOrdersPhaseView_ctl00']/tbody/tr[2]/td[2]/table/tbody/tr[2]/td/table/tbody/tr"));
                int productCount = rows.Count;
                if (expectedProductCount == productCount)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The product count for " + productName + " is " + productCount + ".</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The product count for " + productName + " is " + productCount + ".</b></font>");
                }               
            }   
        }
    }
    
}
