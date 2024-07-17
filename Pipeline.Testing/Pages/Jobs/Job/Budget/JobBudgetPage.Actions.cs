using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Products;

namespace Pipeline.Testing.Pages.Jobs.Job.Budget
{
    public partial class JobBudgetPage
    {
        public void CreateBudget()
        {
            SelectAllPhases_ckb.Check(true);
            System.Threading.Thread.Sleep(5000);

            CommonHelper.CaptureScreen();
            CreateBudgetForSelected_Btn.Click();
            //WaitingLoadingGifByXpath(loadingGrid_xpath);
            CommonHelper.CaptureScreen();

            // Get current toast message and verify it
            string actualToastMess = GetLastestToastMessage(50);
            string expectedToastMess = "selected Budget(s) created/updated successfully.";

            if (actualToastMess.Contains(expectedToastMess))
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Selected Budget(s) created/updated successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Failed to create Budget. The toast message isn't same as the expectation. \n " + actualToastMess + " </font>");
            }
        }

        public void ValidateEstimateAmount(string expectedEstimateAmount)
        {
            SpecificControls control = new SpecificControls(FindType.Id, "ctl00_CPH_Content_rgPhasesView_ctl00_ctl04_lblDollarsTotalBudgetedUnbudgeted2");
            string actualEstimateAmount = control.GetAttribute("innerHTML");
            if (expectedEstimateAmount.Equals(actualEstimateAmount))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Estimate Amount value of " + expectedEstimateAmount + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='green'><b>The expected Estimate Amount value of " + expectedEstimateAmount + " is NOT displayed. </b></font>");
            }
        }
        public void ValidateUnbudgetedAmount(string expectedUnbudgetedAmount)
        {
            SpecificControls control = new SpecificControls(FindType.Id, "ctl00_CPH_Content_rgPhasesView_ctl00_ctl04_lblDollarsUnbudgeted2");
            string actualUnbudgetedAmount = control.GetAttribute("innerHTML");
            if (expectedUnbudgetedAmount.Equals(actualUnbudgetedAmount))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Unbudgeted Amount value of " + expectedUnbudgetedAmount + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='green'><b>The expected Unbudgeted Amount value of " + expectedUnbudgetedAmount + " is NOT displayed. </b></font>");
            }
        }

        public void ValidateBudgetedAmount(string expectedBudgetAmount)
        {
            SpecificControls control = new SpecificControls(FindType.Id, "ctl00_CPH_Content_rgPhasesView_ctl00_ctl04_lblDollarsBudgeted2");
            string actualBudgetAmount = control.GetAttribute("innerHTML");
            if (expectedBudgetAmount.Equals(actualBudgetAmount))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected Budget Amount value of " + expectedBudgetAmount + " is displayed. </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='green'><b>The expected Budget Amount value of " + expectedBudgetAmount + " is NOT displayed. </b></font>");
            }
        }

        public void CheckLineItemCost(string productName, string itemType, string expectedUnitCosts)
        {
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgPhasesView_ctl00_ctl05_Detail10']/tbody/tr/td[text()='" + itemType + "']/following-sibling::td[4]";
            SpecificControls control = new SpecificControls(FindType.XPath, xpath);
            string actualUnitCost = control.GetAttribute("innerHTML");
            if(expectedUnitCosts.Equals(actualUnitCost))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The expected unit cost for " + productName + "'s " + itemType + " Line Item Type is displayed and is equal to " + actualUnitCost + "</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The unit cost for " + productName + "'s " + itemType + " Line Item Type is " + actualUnitCost + ".");
            }
        }

        public void SelectView(string view)
        {
            ViewBy_ddl.SelectItem(view, false);
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        public string GetDefaultBudgetView()
        {
            return ViewBy_ddl.SelectedItem.Text;
        }

        public bool IsColumnFoundInOptionsGrid(string columnName)
        {
            try
            {
                return BudgetViewByOptions_grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }

        public bool IsColumnFoundInOptionBPDetailsGrid(string columnName)
        {
            try
            {
                return BudgetViewByOptionsBPDetail_grid.IsColumnFoundInGrid(columnName);
            }
            catch(System.Exception e)
            {
                return false;
            }
        }

        public bool IsColumnFoundInOptionProductDetailsGrid(string columnName)
        {
            try
            {
                return BudgetViewByOptionsProductDetail_grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }
        public bool IsColumnFoundInOptionVendorDetailsGrid(string columnName)
        {
            try
            {
                return BudgetViewByOptionsVendorDetail_grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }

        public void CollapseAllGrid()
        {
            CommonHelper.CaptureScreen();
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgOptionsView_ctl00']/thead/tr/th/input";
            Button collapseOptionGridButton = new Button(FindType.XPath, xpath);
            collapseOptionGridButton.Click();

            xpath = $"//table[@id = 'ctl00_CPH_Content_rgOptionsView_ctl00_ctl05_Detail10']/thead/tr/th/input";
            Button collapseBPGridButton = new Button(FindType.XPath, xpath);
            collapseBPGridButton.Click();

            xpath = $"//table[@id = 'ctl00_CPH_Content_rgOptionsView_ctl00_ctl05_Detail10_ctl06_Detail10']/thead/tr/th/input";
            Button collapseProductGridButton = new Button(FindType.XPath, xpath);
            collapseProductGridButton.Click();
            CommonHelper.CaptureScreen();
        }

        public void CollapseAllBudgetGrid()
        {
            CommonHelper.CaptureScreen();
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgPhasesView_ctl00']/thead/tr/th/input";
            Button collapseOptionGridButton = new Button(FindType.XPath, xpath);
            collapseOptionGridButton.Click();
            System.Threading.Thread.Sleep(5000);
            CommonHelper.CaptureScreen();
        }

        public void ValidateOneTimeProductExistsOnOptionsView(string expectedProductName)
        {
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgOptionsView_ctl00']/thead/tr/th/input";
            Button collapseGridButton = new Button(FindType.XPath, xpath);
            collapseGridButton.Click();

            Button collapseProductGridButton = new Button(FindType.XPath, $"//table[@id = 'ctl00_CPH_Content_rgOptionsView_ctl00_ctl05_Detail10']/tbody/tr[1]/td[1]/input");
            collapseProductGridButton.Click();
            //CommonHelper.CaptureScreen();

            SpecificControls control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgOptionsView_ctl00']/tbody/tr[2]/td[2]/table/tbody/tr[2]/td[2]/table/tbody/tr[1]/td[3]");
            string actualProductName = control.GetAttribute("innerHTML");
            if (actualProductName == expectedProductName)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The one time product " + expectedProductName + " is displayed.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The one time product " + expectedProductName + " is NOT displayed..</b></font>");
            }

        }
        public void IsProductNameVisibleOnPhasesView(string expectedProductName)
        {
            string xpath = $"//table[@id = 'ctl00_CPH_Content_rgPhasesView_ctl00']/thead/tr/th/input";
            Button collapseGridButton = new Button(FindType.XPath, xpath);
            collapseGridButton.Click();

            //Button collapseProductGridButton = new Button(FindType.XPath, $"//table[@id = 'ctl00_CPH_Content_rgOptionsView_ctl00_ctl05_Detail10']/tbody/tr[1]/td[1]/input");
            //collapseProductGridButton.Click();
            CommonHelper.CaptureScreen();

            SpecificControls control = new SpecificControls(FindType.XPath, "//table[@id = 'ctl00_CPH_Content_rgPhasesView_ctl00']/tbody/tr[2]/td[2]/table/tbody/tr[1]/td[3]");
            string actualProductName = control.GetAttribute("innerHTML");
            if (actualProductName == expectedProductName)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The product " + expectedProductName + " is displayed.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'><b>The product " + expectedProductName + " is NOT displayed..</b></font>");
            }

        }

    }
}
