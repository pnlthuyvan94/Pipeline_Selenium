using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders.Variance;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders
{
    public partial class CreatePurchaseOrdersPage
    {
        public void CheckStatusOfPOHasBeenCreatedInBuldingPhase(string BuildingPhase)
        {
            Button BuildingPhase_chk = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']//td/a[contains(text(),'{BuildingPhase}')]//ancestor::tr//td[@class='cbPhaseCheck']//input");
            if (BuildingPhase_chk.IsDisplayed())
            {
                ExtentReportsHelper.LogInformation($"PO is not created in BuildingPhase Name: {BuildingPhase}");
                BuildingPhase_chk.Click();
                CreatePO_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhasesView']/div[1]");
                ConfirmDialog(ConfirmType.OK);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 1000);
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(BuildingPhase_chk), $"PO is has Been created in BuildingPhase Name: {BuildingPhase}");
            }
        }

        public void CreatePOForSelectedInBP(string BuildingPhase, bool hasPriceChange)
        {
            Button BuildingPhase_chk = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']//td/a[contains(text(),'{BuildingPhase}')]//ancestor::tr//td[@class='cbPhaseCheck']//input");
            if (BuildingPhase_chk.IsDisplayed())
            {
                ExtentReportsHelper.LogInformation($"PO is not created in BuildingPhase Name: {BuildingPhase}");
                BuildingPhase_chk.Click();
                CreatePO_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhasesView']/div[1]");
                if (hasPriceChange)
                {
                    DropdownList priceChange_dll = new DropdownList(FindType.Id, "ctl00_CPH_Content_rptPOSelected_ctl00_ddlCostCategories");
                    priceChange_dll.SelectItemByIndexAndGetValue(1, true);
                    CreatePO_ConfirmChanges_btn.Click();
                    System.Threading.Thread.Sleep(5000);
                }
                //else
                //    ConfirmDialog(ConfirmType.OK);

                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 1000);
            }
            else
            {
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(BuildingPhase_chk), $"PO is has Been created in BuildingPhase Name: {BuildingPhase}");
            }
        }

        public void VerifyVarianceModalForAllSelectedViewType(string BuildingPhase)
        {
            ViewVarianceModalForSelectedViewType(BuildingPhase, "Release Group");
            ViewVarianceModalForSelectedViewType(BuildingPhase, "Building Group");
            ViewVarianceModalForSelectedViewType(BuildingPhase, "Phase");
            RefreshPage();
        }

        public void ViewVarianceModalForSelectedViewType(string BuildingPhase, string ViewType)
        {
            VarianceModal = new VarianceModal();
            switch (ViewType)
            {
                case "Phase":
                    ExtentReportsHelper.LogInformation($"Set Selected View Type to Phase View.");
                    SetDefaultView("Phase");
                    System.Threading.Thread.Sleep(5000);
                    CollapseAllGrid();
                    System.Threading.Thread.Sleep(5000);
                    Button BuildingPhase_chk = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']//td/a[contains(text(),'{BuildingPhase}')]//ancestor::tr//td[@class='cbPhaseCheck']//input");
                    if (BuildingPhase_chk.IsDisplayed())
                    {
                        BuildingPhase_chk.Click();
                        CreatePO_btn.Click();
                        WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhasesView']/div[1]");
                        CommonHelper.CaptureScreen();
                        System.Threading.Thread.Sleep(7000);
                        if (VarianceModal.IsModalDisplayed() is true)
                        {
                            ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>\"Variance\" modal is displayed in Phase View Type.</b></font>");
                            VarianceModal.Close();
                            System.Threading.Thread.Sleep(5000);
                        }
                        WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 1000);
                    }
                    break;
                case "Building Group":
                    ExtentReportsHelper.LogInformation($"Set Selected View Type to Building Group View.");
                    SetDefaultView("Building Group");
                    System.Threading.Thread.Sleep(5000);
                    CollapseAllGridBG();
                    System.Threading.Thread.Sleep(5000);
                    SelectAllBG_chk.SetCheck(true);
                    CreatePO_btn.Click();
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBuildingGroupsView']/div[1]");
                    CommonHelper.CaptureScreen();
                    System.Threading.Thread.Sleep(7000);
                    if (VarianceModal.IsModalDisplayed() is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>\"Variance\" modal is displayed in Building Group View Type.</b></font>");
                        VarianceModal.Close();
                        System.Threading.Thread.Sleep(5000);
                    }
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 1000);
                    break;
                case "Release Group":
                    ExtentReportsHelper.LogInformation($"Set Selected View Type to Release Group View.");
                    SetDefaultView("Release Group");
                    System.Threading.Thread.Sleep(5000);
                    CollapseAllGridRG();
                    System.Threading.Thread.Sleep(5000);
                    SelectAllRG_chk.SetCheck(true);
                    CreatePO_btn.Click();
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgReleaseGroupsView']/div[1]");
                    CommonHelper.CaptureScreen();
                    System.Threading.Thread.Sleep(7000);
                    if (VarianceModal.IsModalDisplayed() is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color ='green'><b>\"Variance\" modal is displayed in Release Group View Type.</b></font>");
                        VarianceModal.Close();
                        System.Threading.Thread.Sleep(5000);
                    }
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobs']/div[1]", 1000);
                    break;
                default:
                    ExtentReportsHelper.LogInformation(null, $"No View type is selected.");
                    break;
            }
        }


        public bool CanCreatePO(string BuildingPhase)
        {
            try
            {
                Button BuildingPhase_chk = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']//td/a[contains(text(),'{BuildingPhase}')]//ancestor::tr//td[@class='cbPhaseCheck']//input");
                return BuildingPhase_chk.IsClickable();
            }
            catch
            {
                return false;
            }
        }

        public void CheckIfBPHasPO(string BuildingPhase)
        {
            Button BuildingPhase_chk = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']//td/a[contains(text(),'{BuildingPhase}')]//ancestor::tr//td[@class='cbPhaseCheck']//input");
            if (BuildingPhase_chk.IsDisplayed())
                ExtentReportsHelper.LogInformation($"PO is not created in BuildingPhase Name: {BuildingPhase}");
            else
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(BuildingPhase_chk), $"PO is has Been created in BuildingPhase Name: {BuildingPhase}");
        }


        public bool IsItemInGrid(string columnName, string value)
        {
            return ViewPhase_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void CollapseAllGrid()
        {
            CommonHelper.CaptureScreen();
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgPhasesView_ctl00']/thead/tr/th/input";
            Button collapseOptionGridButton = new Button(FindType.XPath, xpath);
            collapseOptionGridButton.Click();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
        }

        public void CollapseAllGridBG()
        {
            CommonHelper.CaptureScreen();
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgBuildingGroupsView_ctl00']/thead/tr/th/input";
            Button collapseOptionGridButton = new Button(FindType.XPath, xpath);
            collapseOptionGridButton.Click();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
        }

        public void CollapseAllGridRG()
        {
            CommonHelper.CaptureScreen();
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgReleaseGroupsView_ctl00']/thead/tr/th/input";
            Button collapseOptionGridButton = new Button(FindType.XPath, xpath);
            collapseOptionGridButton.Click();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
        }

        public CreatePurchaseOrdersPage CheckJobInformationTooltip()
        {
            IWebElement webElement = driver.FindElement(By.XPath("//table[@id='ctl00_CPH_Content_rgPhasesView_ctl00']/tbody/tr[1]/td[7]/div"));
            try 
            {
                webElement.Click();
                System.Threading.Thread.Sleep(3000);
                CommonHelper.CaptureScreen();

                IWebElement svg = webElement.FindElement(By.XPath("//*[name()='svg' and @data-toggle='tooltip']"));
                if (svg.Displayed)
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'>There is a price change in budget</font color>");
                    string btnTitlet = svg.GetAttribute("data-original-title");
                    Actions act = new Actions(driver);
                    act.MoveToElement(svg);

                    if (btnTitlet == "There has been no Budget Created.")
                    {
                        ExtentReportsHelper.LogPass($"<font color ='green'>Tooltip display: There has been no Budget Created</font color>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogPass($"<font color ='green'>Tooltip display: There’s been changes to this budget. Create a purchase order for this building phase to see the changes.</font color>");
                    }
                }
                else
                {
                    ExtentReportsHelper.LogPass($"<font color ='green'>There's no price change in budget</font color>");
                }
            }
            catch 
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>There's no price change in budget</font color>");
            }
            return this;
        }

        public void SetDefaultView(string view)
        {
            ExtentReportsHelper.LogPass($"<font color ='green'>Selected View Type is " + view + "</font color>");
            DefaultView_ddl.SelectItem(view);
            System.Threading.Thread.Sleep(5000);
        }

        public void SelectOrUnSelectAllItems(bool status)
        {
            SelectAll_chk.SetCheck(status);
        }

        public void ValidateBudgetAmount(string expectedBudgetedAmount)
        {
            string xpath = "//table[@id = 'ctl00_CPH_Content_rgPhasesView_ctl00']/tbody/tr[1]/td[7]/div[2]/span/strong";
            string xpath2 = "//table[@id = 'ctl00_CPH_Content_rgPhasesView_ctl00']/tbody/tr[1]/td[7]/div[2]/span";
            SpecificControls control = new SpecificControls(FindType.XPath, xpath);
            string actualBudgetAmount;
            try
            {
                actualBudgetAmount = control.GetAttribute("innerHTML");
            }
            catch (Exception)
            {
                control = new SpecificControls(FindType.XPath, xpath2);
                actualBudgetAmount = control.GetAttribute("innerHTML");
            }
           
            if (expectedBudgetedAmount.Equals(actualBudgetAmount))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Budget Amount value of " + expectedBudgetedAmount + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The expected Budget Amount value of " + expectedBudgetedAmount + " is NOT displayed. Actual budget amount is " + actualBudgetAmount + ".</b></font>");
            }
        }

        public void ValidateUnorderedAmount(string expectedUnorderedAmount)
        {
            SpecificControls control = new SpecificControls(FindType.Id, "ctl00_CPH_Content_rgPhasesView_ctl00_ctl04_lblDollarsUnordered2");
            string actualUnorderedAmount = control.GetAttribute("innerHTML");
            if (expectedUnorderedAmount.Equals(actualUnorderedAmount))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Unordered Amount value of " + expectedUnorderedAmount + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The expected Unordered Amount value of " + expectedUnorderedAmount + " is NOT displayed. Actual Unordered amount is " + actualUnorderedAmount + ". </b></font>");
            }
        }
        public void ValidateOrderedAmount(string expectedOrderedAmount)
        {
            SpecificControls control = new SpecificControls(FindType.Id, "ctl00_CPH_Content_rgPhasesView_ctl00_ctl04_lblDollarsOrdered2");
            string actualOrderedAmount = control.GetAttribute("innerHTML");
            if (expectedOrderedAmount.Equals(actualOrderedAmount))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Ordered Amount value of " + expectedOrderedAmount + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The expected Ordered Amount value of " + expectedOrderedAmount + " is NOT displayed. The actual ordered amount is " + actualOrderedAmount + ". </b></font>");
            }
        }

    }
}
