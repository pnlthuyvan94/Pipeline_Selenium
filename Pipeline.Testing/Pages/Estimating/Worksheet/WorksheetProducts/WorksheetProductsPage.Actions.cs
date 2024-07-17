using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetProducts
{
    public partial class WorksheetProductsPage
    {
       
        public void SelectCommunityBOM(string community)
        {
            CommunityBOM_ddl.SelectItem(community, true, false);
            //WorksheetBOM_Grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]", 2000);
        }
        public void Click_GenerateBOM()
        {
            GenerateBOM_btn.Click();
            //WorksheetBOM_Grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
        }
        
        public string Click_GenerateBOMEstimate()
        {
            GenerateEstimateBOM_btn.Click();            
            //WorksheetBOM_Grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
            return GetLastestToastMessage();
        }
        public void Click_ExpandWorksheet()
        {
            if (ExpandWorksheet.IsDisplayed())
            {
                ExpandWorksheet.Click();
                System.Threading.Thread.Sleep(1000);
            }
            else
                ExtentReportsHelper.LogInformation("Can not expand BOM worksheet");

        }
        public void Click_ExpandSubWorksheet()
        {
            if (ExpandSubWorksheet.IsDisplayed())
            {
                ExpandSubWorksheet.Click();
                System.Threading.Thread.Sleep(1000);
            }
            else
                ExtentReportsHelper.LogInformation("Not have Sub Worksheet");

        }
        public bool IsAddQuantitiesDisplayed()
        {
            AddProductQuantities_lbl.WaitForElementIsVisible(5);
            return AddProductQuantities_lbl != null && AddProductQuantities_lbl.IsDisplayed() is true;
        }

        public List<string> AddQuantitiesInGrid(WorksheetProductsData WorksheetQuantitiesData)
        {
            var listUse = new List<string>();
            AddNewQuantity_btn.Click();
            if (IsAddQuantitiesDisplayed() is true)
            {
                int Count = 0;
                //Switch To IFrame
                string Iframe = "RadWindow1";
                CommonHelper.SwitchFrame(Iframe);
                //Config Option, Source , Building Phase into Product
                OpenBuildingPhase_btn.Click();
                foreach (string item in WorksheetQuantitiesData.BuildingPhase)
                {
                    CheckBox BuildingPhase_chk = new CheckBox(FindType.XPath, $"//*[@id='rcbPhases_DropDown']//ul[@class='rcbList']//li//label[contains(text(),'{item}')]/input");
                    BuildingPhase_chk.Check(true);
                }
                LoadProducts_btn.Click();
                System.Threading.Thread.Sleep(4000);

                //Add Product Quantities And Quantity into Building Phase
                foreach (string product in WorksheetQuantitiesData.Products)
                {
                    ProductQuantitiesPage_Grid.FilterByColumn("Product Name", GridFilterOperator.Contains, product);
                    WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");
                    CheckBox Product_chk = new CheckBox(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]//ancestor::tr//input[@type='checkbox']");
                    Textbox Quantity_txt = new Textbox(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]//ancestor::tr//input[@type='text']");
                    Button ExpandUse_btn = new Button(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]/../following-sibling::td/select");
                    ProductQuantitiesPage_Grid.FilterByColumn("Style", GridFilterOperator.Contains, WorksheetQuantitiesData.Style);
                    WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");

                  
                    if (WorksheetQuantitiesData.Use != "NONE")
                    {
                        ExpandUse_btn.Click();
                        IWebElement IndexUse_ddl = FindElementHelper.FindElement(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]/../following-sibling::td/select");
                        Button getUse_btn = new Button(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]/../following-sibling::td/select/option[2]");
                        IndexUse_ddl.SendKeys(Keys.ArrowDown);
                        IndexUse_ddl.SendKeys(Keys.Enter);
                        listUse.Add(getUse_btn.GetText());
                    }
                    Quantity_txt.SetText(WorksheetQuantitiesData.Quantity[Count]);          
                    Product_chk.Check(true);
                    //No Filter Product And Style Data In Grid
                    ProductQuantitiesPage_Grid.FilterByColumn("Style", GridFilterOperator.NoFilter, string.Empty);
                    WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");
                    ProductQuantitiesPage_Grid.FilterByColumn("Product Name", GridFilterOperator.NoFilter, string.Empty);
                    WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");
                    Count++;
                }

                SaveProductToAdd_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='lp1lbSaveProductsToAdd']/div[1]");
                CommonHelper.SwitchToDefaultContent();
                CloseModal_btn.Click();
            }

            return listUse;
        }
        public bool IsItemInGrid(string columnName, string value)
        {
            return WorksheetQuantities_Grid.IsItemOnCurrentPage(columnName, value);
        }
        public void EditItemInGrid(string product)
        {
            Button editProduct_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgWorksheetProducts_ctl00']//td/a[contains(text(),'{product}')]//ancestor::tr//input[@alt='Edit']");
            editProduct_btn.Click();
            System.Threading.Thread.Sleep(3000);
        }
        public void UpdateItemInGrid(string use, string quantity)
        {
            // Select Use
            DropdownList use_ddl = new DropdownList(FindType.XPath, "//*[contains(@id,'ddlEditUse')]");
            if (!use_ddl.IsNull() && use != string.Empty)
                use_ddl.SelectItem(use, true);

            // Set Quantity
            Textbox quantity_txt = new Textbox(FindType.XPath, "//*[contains(@id,'txtProductsToWorksheets_Quantity')]");
            if (!quantity_txt.IsNull() && Convert.ToDouble(quantity) > 0)
                quantity_txt.SetText(quantity.ToString());

            CheckBox ClickAll_chk = new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgWorksheetProducts_ctl00_ctl02_ctl02_ClientSelectColumnSelectCheckBox']");
            ClickAll_chk.SetCheck(true);
            // Click save button
            Button add_btn = new Button(FindType.XPath, "//*[contains(@id,'ctl00_CPH_Content_rgWorksheetProducts_ctl00_ctl03_ctl01_SaveChangesIcon')]");
            add_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheetProducts']/div[1]");
        }

        public bool IsUseInGrid(string use)
        {

            System.Threading.Thread.Sleep(1000);
            Textbox Use_txt = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgWorksheetProducts_ctl00']//div/span[contains(@id,'lblUse') and contains(text(),'{use}')]");
            Use_txt.RefreshWrappedControl();
            if (Use_txt.GetText().Equals(use) is true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }     

        public void FilterItemInGrid(string columnName, GridFilterOperator GridFilterOperator, string valueToFilter)
        {
            WorksheetQuantities_Grid.FilterByColumn(columnName, GridFilterOperator, valueToFilter);
            WaitingLoadingGifByXpath("//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheetProducts']/div[1]");

        }

        public void DeleteItemInGrid(string columnName, string valueToFind)
        {
            WorksheetQuantities_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
        }
        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheetProducts']/div[1]");

        }

        //public void DeleteItemInGrid(string columName, string valueToDelete)
        //{            
        //    WorksheetQuantities_Grid.ClickDeleteItemInGrid(columName, valueToDelete);
        //    ConfirmDialog(ConfirmType.OK);
        //    WaitingLoadingGifByXpath("//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheetProducts']/div[@class = 'raDiv']");
        //}

        public WorksheetProductsPage CopyProductQuantitiesTo(string objectPage, string selectName)
        {
            switch (objectPage)
            {
                case "Options":
                case "Custom Options":
                case "Worksheets":
                    {
                        //Click Copy Quantities button 
                        CopyQuantities_btn.Click();
                        CopyToPage_ddl.SelectItem(objectPage, true, false);
                        WaitingLoadingGifByXpath("//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]");
                        SelectPage_txt.SetText(selectName);
                        ListItem listItem = new ListItem(FindElementHelper.FindElements(FindType.XPath, "//ul[@class = 'rlbList']//li[not(@style = 'display: none;')]"));
                        for (int i = 0; i < listItem.GetAllItems().Count; i++)
                        {
                            if (listItem.GetAllItems()[i].Text.Trim() == selectName)
                            {
                                CheckBox exactValue = new CheckBox(FindType.XPath, $"//ul[@class = 'rlbList']/li[not(@style = 'display: none;')][{i + 1}]//input");
                                exactValue.SetCheck(true);
                            }
                        }
                        Button copyQuantities_btn = new Button(FindType.XPath, "//a[contains(@id, 'ctl00_CPH_Content_lbCopy') and contains(.,'Copy Quantities to')]");
                        copyQuantities_btn.Click();
                        WaitingLoadingGifByXpath("//div[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgWorksheetProducts']/div[1]");
                        break;
                    }

                //"selectName" is combined by "HouseName + OptionName"
                case "House Options":
                    {
                        string HouseName = selectName.Split(';')[0];
                        string OptionName = selectName.Split(';')[1];
                        //Click Copy Quantities button 
                        CopyQuantities_btn.Click();
                        CopyToPage_ddl.SelectItem(objectPage, true, false);
                        WaitingLoadingGifByXpath("//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlViewType']/div[1]");
                        //
                        Textbox HouseInput_txb = new Textbox(FindType.XPath, "//input[contains(@value,'Search House Name..') and not(contains(@id, 'Client'))]");
                        Textbox OptionInput_txb = new Textbox(FindType.XPath, "//input[contains(@value,'Search Option Name..') and not(contains(@id, 'Client'))]");
                        //For House
                        HouseInput_txb.SetText(HouseName);
                        ListItem listItemHouse = new ListItem(FindElementHelper.FindElements(FindType.XPath, "//div[@id= 'ctl00_CPH_Content_rlbHouses']//ul[@class = 'rlbList']/li[not(@style = 'display: none;')]"));
                        for (int i = 0; i < listItemHouse.GetAllItems().Count; i++)
                        {
                            if (listItemHouse.GetAllItems()[i].Text.Trim() == HouseName)
                            {
                                CheckBox exactValue = new CheckBox(FindType.XPath, $"//div[@id = 'ctl00_CPH_Content_rlbHouses']//ul[@class = 'rlbList']/li[not(@style = 'display: none;')][{i + 1}]//input");
                                exactValue.SetCheck(true);
                            }
                        }
                        //For Option 
                        OptionInput_txb.SetText(OptionName);
                        ListItem listItemOp = new ListItem(FindElementHelper.FindElements(FindType.XPath, "//div[@id= 'ctl00_CPH_Content_rlbOptions']//ul[@class = 'rlbList']/li[not(@style = 'display: none;')]"));
                        for (int i = 0; i < listItemOp.GetAllItems().Count; i++)
                        {
                            if (listItemOp.GetAllItems()[i].Text.Trim() == OptionName)
                            {
                                CheckBox exactValue = new CheckBox(FindType.XPath, $"//div[@id = 'ctl00_CPH_Content_rlbOptions']//ul[@class = 'rlbList']/li[not(@style = 'display: none;')][{i + 1}]//input");
                                exactValue.SetCheck(true);
                            }
                        }
                        //Click "Save button
                        SaveHouseOptionToCopyQuantity_btn.Click();
                        break;
                    }
                default:
                    {
                        ExtentReportsHelper.LogWarning("Name of Page is not correct");
                        break;
                    }
            }

            return this;

        }

        public WorksheetProductsPage DeleteBulkActionInWSQuantitiesGrid(string DeleteKind)
        {
            CheckAll_WSQuantity_ckb.SetCheck(true);
            Bulk_Action_WSQuantity_btn.Click();
            if(DeleteKind.ToLower() == "delete selected")
            {
                Delete_Selected_lbl.Click();
            }    
            if(DeleteKind.ToLower() == "delete all products")
            {
                DeleteAllProduct_lbl.Click();
            }    
            ConfirmDialog(ConfirmType.OK);
            return this;
        }

        public bool IsItemInReportGrid(string columnName, string valueToFind)
        {
            return WorksheetBOM_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        
        public void FilterItemOnReportGrid(string columnName, GridFilterOperator GridFilterOperator, string valueToFilter)
        {
            WorksheetBOM_Grid.FilterByColumn(columnName, GridFilterOperator, valueToFilter);
            WaitingLoadingGifByXpath("//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgReport']/div[1]");

        }
    }
}
