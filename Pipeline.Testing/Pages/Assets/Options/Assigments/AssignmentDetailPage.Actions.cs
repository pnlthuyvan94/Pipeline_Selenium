using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Assets.Options.Assigments
{
    public partial class AssignmentDetailPage
    {
        public void SwitchAssignmentView(AssigmentView viewBy)
        {
            if (!AssigmentViewBy_ddl.SelectedItemName.Equals(viewBy.ToString("g")))
            {
                AssigmentViewBy_ddl.SelectItem(viewBy.ToString("g"), false, false);
                WaitingLoadingGifByXpath("//*[contains(@id,'ctl00_CPH_Content_LoadingPanel')]/div[1]");
            }
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(AssigmentViewBy_ddl), $"The page was selected item <font color='green'><b>{viewBy:g}</b></font> from the Dropdownlist.");
        }

        public bool VerifyItemInPage(AssigmentView viewBy)
        {
            ExtentReportsHelper.LogInformation($"============  Verify the item on <b>{viewBy:g}</b> page displayed enough or not.==============");
            switch (viewBy)
            {
                case AssigmentView.Estimating:
                    return VerifyItemExistByName(GetListTitleByView(AssigmentView.Estimating).ToArray());
                case AssigmentView.Job:
                    return VerifyItemExistByName(GetListTitleByView(AssigmentView.Job).ToArray());
                case AssigmentView.BOM:
                    return VerifyItemExistByName(GetListTitleByView(AssigmentView.BOM).ToArray());
                case AssigmentView.Costing:
                    return VerifyItemExistByName(GetListTitleByView(AssigmentView.Costing).ToArray());
                case AssigmentView.Purchasing:
                    return VerifyItemExistByName(GetListTitleByView(AssigmentView.Purchasing).ToArray());
                //case AssigmentView.Pathway:
                    //return VerifyItemExistByName(GetListTitleByView(AssigmentView.Pathway).ToArray());
                case AssigmentView.Integrations:
                    return VerifyItemExistByName(GetListTitleByView(AssigmentView.Integrations).ToArray());
                default:
                    return VerifyItemExistByName(GetListTitleByView(AssigmentView.Builder).ToArray());
            }
        }

        private IList<string> GetListTitleByView(AssigmentView viewBy)
        {
            IList<string> items;
            switch (viewBy)
            {
                case AssigmentView.Estimating:
                    items = new List<string>() { "Subcomponents", "Spec Sets" };
                    return items;
                case AssigmentView.Job:
                    items = new List<string>() { "Active Jobs", "Closed Jobs" };
                    return items;
                case AssigmentView.BOM:
                    items = new List<string>() {"House BOMs", "Global Option BOMs", "Custom Option BOMs", "Worksheet BOMs", "Active Job BOMs", "Closed Job BOMs", "Import Comparison Groups - Houses", "House BOM Imports", "House BOM Syncs", "Job BOM Syncs" };
                    return items;
                case AssigmentView.Costing:
                    items = new List<string>() { "Option/Building Phase Bid Costs", "Option/Building Phase Bid Costs - Houses", "Option/Building Phase Bid Costs - Communities", "Option/Building Phase Bid Costs - Houses in Community", "Option/Building Phase Bid Costs - Jobs" };
                    return items;
                case AssigmentView.Purchasing:
                    items = new List<string>() { "Job Budgets", "Job Purchase Orders", "Job Variance Estimates"};
                    return items;
                //case AssigmentView.Pathway:
                   // items = new List<string>() { "Sales Options", "Designer Elements", "Scenarios", "Job Contracts" };
                   // return items;
                case AssigmentView.Integrations:
                    items = new List<string>() { "WMS Option Mappings", "WMS Bid Products", "HomeFront Bid Products" };
                    return items;
                default:
                    items = new List<string>() { "Houses", "Communities", "Houses in Communities", "Product Quantity Options", "Option Condition Mappings", "Option Selections" };
                    return items;
            }
        }

        private bool VerifyItemExistByName(params string[] names)
        {
            bool isPassed = true;
            foreach (var name in names)
            {
                var a = FindElementHelper.FindElement(FindType.XPath, $"//header/h1[text()='{name}']|//h1/span[.='{name}']|//header/h1/a[text()='{name}']");
                if (a is null)
                {
                    ExtentReportsHelper.LogFail($"The title with name <b><font color='green'>{name}</font></b> is not exist on current screen.");
                    isPassed = false;
                }
                else
                {
                    CommonHelper.MoveToElement(a);
                    ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(a), $"The title with name <b><font color='green'>{name}</font></b> is displayed on current screen.");
                }
            }
            return isPassed;
        }

        /// <summary>
        /// Add Houses to current Option
        /// </summary>
        public void AddHousesToOption(params string[] names)
        {
            Actions action = new Actions(driver);
            action.KeyDown(Keys.Control).Build().Perform();
            foreach (var name in names)
            {
                var item = FindElementHelper.FindElement(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbHouses']/div/ul/li[contains(.,'{name}')]");
                if (item is null)
                    ExtentReportsHelper.LogFail($"The item with name <b><font color='green'>{name}</font></b> could not be found on the list.");
                else
                {
                    item.Click();
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"The item with name <b><font color='green'>{name}</font></b> is selected on the list.");
                }
            }
            action.KeyUp(Keys.Control).Build().Perform();
            InsertHouseToOption_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rlbHouses']/div[1]");
        }

        public AssignmentDetailPage ClickAddHouseToShowModal()
        {
            AddHouse_btn.Click();
            if (!AddHouseModalTitle_lbl.WaitForElementIsVisible(5))
                ExtentReportsHelper.LogFail("The 'Add House' Modal is NOT display after 5s.");
            return this;
        }

        public void CloseAddHouseModal()
        {
            CloseAddHouseModal_btn.Click();
            if (!AddHouseModalTitle_lbl.WaitForElementIsInVisible(5))
                ExtentReportsHelper.LogFail("The 'Add House' Modal is NOT hide after 5s.");
        }

        /// <summary>
        /// Add communities to current option
        /// </summary>
        public void AddCommunityToOption(params string[] names)
        {
            Actions action = new Actions(driver);
            action.KeyDown(Keys.Control).Build().Perform();
            foreach (var name in names)
            {
                var item = FindElementHelper.FindElement(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbCommunities']/div/ul/li[contains(.,'{name}')]");
                if (item is null)
                    ExtentReportsHelper.LogFail($"The item with name <b><font color='green'>{name}</font></b> could not be found on the list.");
                else
                {
                    item.Click();
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"The item with name <b><font color='green'>{name}</font></b> is selected on the list.");
                }
            }
            action.KeyUp(Keys.Control).Build().Perform();
            InsertCommunitToOption_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rlbCommunities']/div[1]");
        }

        public AssignmentDetailPage ClickAddCommunityToShowModal()
        {
            AddCommunity_Btn.Click();
            if (!AddCommunityTitleModal_lbl.WaitForElementIsVisible(5))
                ExtentReportsHelper.LogFail("The 'Add Communities' Modal is NOT display after 5s.");
            return this;
        }

        public void CloseAddCommunityModal()
        {
            CloseAddCommunity_Btn.Click();
            if (!AddCommunityTitleModal_lbl.WaitForElementIsInVisible(5))
                ExtentReportsHelper.LogFail("The 'Add Communities' Modal is NOT hide after 5s.");
        }

        /// <summary>
        /// Add Product Quantities to current Option
        /// </summary>
        public void AddProductQuantityToOption(params string[] names)
        {
            Actions action = new Actions(driver);
            action.KeyDown(Keys.Control).Build().Perform();
            foreach (var name in names)
            {
                var item = FindElementHelper.FindElement(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbAddPQCO']/div/ul/li[contains(.,'{name}')]");
                if (item is null)
                    ExtentReportsHelper.LogFail($"The item with name <b><font color='green'>{name}</font></b> could not be found on the list.");
                else
                {
                    item.Click();
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"The item with name <b><font color='green'>{name}</font></b> is selected on the list.");
                }
            }
            action.KeyUp(Keys.Control).Build().Perform();
            InsertProductToOption_Btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgChildOptions']/div[1]");
        }

        public AssignmentDetailPage ClickAddProductQuantityOptionToShowModal()
        {
            AddProduct_Btn.Click();
            if (!AddProductTitleModal_lbl.WaitForElementIsVisible(5))
                ExtentReportsHelper.LogFail("The 'Select Product Quantity Option(s)' Modal is NOT display after 5s.");
            return this;
        }

        public void CloseAddProductQuantityOptionModal()
        {
            CloseProductModal_Btn.Click();
            if (!AddProductTitleModal_lbl.WaitForElementIsInVisible(5))
                ExtentReportsHelper.LogFail("The 'Select Product Quantity Option(s)' Modal is NOT hide after 5s.");
        }

        /// <summary>
        /// Remove houses from this option
        /// </summary>
        public void RemoveHouseFromOption()
        {

        }

        /// <summary>
        /// Remove Community from this Option
        /// </summary>
        public void RemoveCommunityFromOption()
        {

        }

        /// <summary>
        /// Remove Product from this Option
        /// </summary>
        public void RemoveProductFromOption()
        {

        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            AssignmentHouse_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            System.Threading.Thread.Sleep(2000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]");
        }


        public bool IsItemInHouseGrid(string columnName, string valueToFind)
        {
            return AssignmentHouse_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }


        public bool IsItemInCommunityGrid(string columnName, string valueToFind)
        {
            return AssignmentCommunity_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }

        public bool IsItemInProductGrid(string columnName, string valueToFind)
        {
            return AssignmentProduct_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }

        public void DeleteItemInHouseGrid(string columnName, string valueToFind)
        {
            AssignmentHouse_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgHouses']/div[1]");
            //AssignmentHouse_Grid.WaitGridLoad();
        }

        public void DeleteItemInCommunityGrid(string columnName, string valueToFind)
        {
            AssignmentCommunity_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunities']/div[1]");

           // AssignmentCommunity_Grid.WaitGridLoad();
        }

        public void DeleteItemInProductGrid(string columnName, string valueToFind)
        {
            AssignmentProduct_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgChildOptions']/div[1]");
            //AssignmentProduct_Grid.WaitGridLoad();
        }
    }

}
