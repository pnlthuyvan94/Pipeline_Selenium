using AventStack.ExtentReports.Model;
using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Linq;

namespace Pipeline.Testing.Pages.Assets.House.Quantities
{
    public partial class HouseQuantitiesDetailPage
    {
        public void FilterByCommunity(string communityName)
        {
            if (communityName != string.Empty)
            {
                FilterCommunity_ddl.SelectItem(communityName);
                WaitingLoadingGifByXpath(loadingIcon_Xpath, 1000);
            }
        }

        public void FilterByDropDownColumn(string columnName, string valueToFind)
        {
            string columnXpath;
            string listItem;
            switch (columnName)
            {
                case "Option":
                    columnXpath = "//*[@id='ctl00_CPH_Content_rgProductsToHouses_ctl00_ctl02_ctl03_ddlOptions_Arrow']";
                    listItem = "//*[contains(@id, 'ddlOptions_DropDown')]/div/ul";
                    break;
                default:
                    // Building Phase
                    columnXpath = "//*[@id='ctl00_CPH_Content_rgProductsToHouses_ctl00_ctl02_ctl03_ddlBuildingPhases_Arrow']";
                    listItem = "//*[contains(@id, 'ddlBuildingPhases_DropDown')]/div/ul";
                    break;
            }
            try
            {
                // Get Column to filter
                Button expandListItems = new Button(FindType.XPath, columnXpath);
                expandListItems.Click(false);

                // Wait until the lis items display
                CommonHelper.WaitUntilElementVisible(10, listItem);

                // Get list of item
                var list = FindElementHelper.FindElements(FindType.XPath, listItem).Where(p => p.Displayed == true).FirstOrDefault();
                list.FindElements(By.XPath("./li")).Where(item => item.Text.StartsWith(valueToFind)).FirstOrDefault().Click();
                WaitingLoadingGifByXpath(loadingIcon_Xpath, 1000);
            }
            catch (Exception)
            {
                ExtentReportsHelper.LogFail($"<font color='yellow'>Can't find element <b>'{valueToFind}'</b> on columnd <b>'{columnName}'</b> to filter.</font>.");
            }
        }


        public bool IsItemInQuantitiesGrid(string column, string valueToFind)
        {
            return QuantitiesGrid.IsItemOnCurrentPage(column, valueToFind);
        }
        public void DeleteAllHouseQuantities(string DeleteType)
        {
         Button DeleteData_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lb{DeleteType}']");
         BulkActions_btn.Click();
         DeleteData_btn.Click();
         ConfirmDialog(ConfirmType.OK);
         WaitingLoadingGifByXpath(loadingIcon_Xpath);
        }

        public void DeleteSelectedHouseQuantities(string DeleteType)
        {
            CheckBox deletedall_chk = new CheckBox(FindType.XPath, "//*[@class='rgHeader rgCheck']/input");
            Button DeleteData_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lb{DeleteType}']");
            deletedall_chk.Check();
            BulkActions_btn.Click();
            DeleteData_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(loadingIcon_Xpath);
        }

        public void ClickEditItemInQuantitiesGrid(string BuildingPhase, string Product)
        {
            Button EditItem_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgProductsToHouses_ctl00']//span[contains(text(),'{BuildingPhase}')]//ancestor::td//./following-sibling::td/a[contains(text(),'{Product}')]//ancestor::td//./following-sibling::td//input[@title='Edit']");
            EditItem_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductsToHouses']/div[1]", 1000);
        }

        public void AddParameterInQuantitiesGrid(string parameter)
        {
            Label EditQuantities_Modal = new Label(FindType.XPath, "//*[@id='actionTitle']");
            Textbox Parameter_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtparameter']");
            Button SaveEdit_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave']");
            if (EditQuantities_Modal.IsDisplayed() && EditQuantities_Modal.GetText().Equals("Edit Quantities"))
            {
                Parameter_txt.SetText(parameter);
                SaveEdit_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductsToHouses']/div[1]");
            }
        }
		 public void ClickCopyQuantitiesToHouseOrOption()
        {
            CopyQtyHouseOption.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbCopyQty']/div[1]");
        }

        public void EditUseInQuantitiesGrid(string value)
        {
            Label EditQuantities_Modal = new Label(FindType.XPath, "//*[@id='actionTitle']");
            DropdownList Use_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlEditUse']");
            Button SaveEdit_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSave']");
            if (EditQuantities_Modal.IsDisplayed() && EditQuantities_Modal.GetText().Equals("Edit Quantities"))
            {
                Use_ddl.SelectItem(value);
                SaveEdit_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductsToHouses']/div[1]");
            }
        }
        public int GetTotalNumberItem()
        {
            return QuantitiesGrid.GetTotalItems;
        }
    }
}
