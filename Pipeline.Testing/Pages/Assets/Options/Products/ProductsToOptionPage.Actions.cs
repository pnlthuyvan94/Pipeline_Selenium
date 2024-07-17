using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Estimating.Products;
using System;

namespace Pipeline.Testing.Pages.Assets.Options.Products
{
    public partial class ProductsToOptionPage
    {
        public void FilterOptionQuantitiesInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            optionQuantitiesGrid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]", 2000);
        }

        public void FilterOptionQuantitiesByDropDownInGrid(string columnName, string valueToFind)
        {
            string valueToFind_ListItem;
            if ("Building Phase" == columnName)
                valueToFind_ListItem = "//*[contains(@id, 'ddlBuildingPhases_DropDown')]/div/ul";
            else
                // If there are any column with drop down list, display value here
                valueToFind_ListItem = string.Empty;

            optionQuantitiesGrid.FilterByColumnDropDowwn(columnName, valueToFind_ListItem, valueToFind);
            WaitOptionQuantitiesLoadingIcon();
        }

        public bool IsOptionQuantitiesInGrid(string columnName, string value)
        {
            return optionQuantitiesGrid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            optionQuantitiesGrid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK, false);
        }

        public void WaitOptionQuantitiesLoadingIcon()
        {
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]", 2000);
        }

        public void AddOptionQuantities(OptionQuantitiesData optionPhaseData)
        {
            Button Add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNew']");
            if (Add_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Can't find Add new Option Quantities button. Stop this step.</font>");
                return;
            }
            Add_btn.Click(false);
            System.Threading.Thread.Sleep(5000);
            Label title_lbl = new Label(FindType.XPath, "//h1[text()='Add Product']");
            if (title_lbl.IsDisplayed(false) is false)
                ExtentReportsHelper.LogFail("<font color='red'>Can't Open Add Product modal or the title is incorrect.</font>");

            // Populate the data to modal and save
            PoppulateOptionQuantitiesToModal(optionPhaseData);
        }
        public void PoppulateOptionQuantitiesToModal(OptionQuantitiesData optionPhaseData)
        {
            // Select Building Phase
            DropdownList phase_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingPhases']");
            if (phase_ddl.IsDisplayed(false) is true && optionPhaseData.BuildingPhase != string.Empty)
            {
                phase_ddl.SelectItem(optionPhaseData.BuildingPhase, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewProduct']/div[1]", 7000);
            }
            /*
            // Select Category
            DropdownList category_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCategoryProduct']");
            if (category_ddl.IsDisplayed(false) is true && optionPhaseData.Category != string.Empty)
            {
                category_ddl.SelectItem(optionPhaseData.Category, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewProduct']/div[1]");
            }
            */
            // Select Product
            Button Product_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbProductId_Arrow']");
            Product_btn.Click();
            
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']", 6000);
            System.Threading.Thread.Sleep(5000);

            Button productCtrl = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']/div[1]/ul/li[contains(text(),'{optionPhaseData.ProductName}')]");
            productCtrl.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewProduct']/div[1]", 5000);

            
            // Select Style
            DropdownList style_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStyles']");
            style_ddl.WaitForElementIsVisible(5);
            if (!style_ddl.IsNull() && optionPhaseData.Style != string.Empty)
            {
                style_ddl.SelectItem(optionPhaseData.Style, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewProduct']/div[1]", 3000);
            }
            System.Threading.Thread.Sleep(5000);

            // Select Condition

            if (optionPhaseData.Condition == true)
            {
                Button selectCondition_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewDependentCondition']");
                selectCondition_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbNewDependentCondition']/div[1]",5000);
                DropdownList option_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddNewOption']");
                DropdownList operator_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddNewOperator']");
                Textbox finalCondition_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddNewDependentCondition']");
                Button conditionSave_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddDependentCondition']");
                option_ddl.SelectItem(optionPhaseData.OptionName);
                operator_ddl.SelectItem(optionPhaseData.Operator);
                if (!finalCondition_txt.IsNull() && optionPhaseData.FinalCondition != string.Empty)
                    finalCondition_txt.SetText(optionPhaseData.FinalCondition);
                conditionSave_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlCondition']/div[1]",5000);
            }

            // Select Use
            DropdownList use_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlUses']");
            if (!use_ddl.IsNull() && optionPhaseData.Use != string.Empty)
                use_ddl.SelectItem(optionPhaseData.Use, true);

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewProduct']/div[1]", 3000);
            System.Threading.Thread.Sleep(5000);
            // Set Quantity
            Textbox quantity_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtQuantity']");
            if (!quantity_txt.IsNull())
                quantity_txt.SetText(optionPhaseData.Quantity.ToString());

            System.Threading.Thread.Sleep(5000);
            // Select Source
            DropdownList source_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewSourceTypes']");
            if (!source_ddl.IsNull() && optionPhaseData.Source != string.Empty)
                source_ddl.SelectItem(optionPhaseData.Source, true);

            System.Threading.Thread.Sleep(5000);
            // Click save button
            Button add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");
            if (!add_btn.IsNull())
                add_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlContent']/div[1]");

            // Verify toast message
            string _expectedMessage = $"Option Product created successfully!";
            string actualMsg = GetLastestToastMessage();
            if (actualMsg == _expectedMessage)
            {
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Option Quantities with phase {optionPhaseData.BuildingPhase} added successfully!</b></font>");
            }
            else
            {
                if (IsOptionQuantitiesInGrid("Building Phase", optionPhaseData.BuildingPhase) is false)
                    ExtentReportsHelper.LogFail($"<font color = 'red'>Option quantities with phase {optionPhaseData.BuildingPhase} could not be added - Possible constraints preventing additional.</font>");
                else
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Option quantities with phase {optionPhaseData.BuildingPhase} added successfully!</b></font>");
            }

        }

        public bool IsHouseOptionQuantitiesInGrid(string columnName, string value)
        {
            return HouseOptionQuantitiesGrid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteAllOptionQuantities()
        {
            AllOptionQuantities_chk.Check();
            Button DeleteData_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbDeleteSelectedOptionProducts']");
            OptionBulkActions_btn.Click();
            DeleteData_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]");
        }
        public void DeleteAllHouseOptionQuantities()
        {
            AllHouseQuantities_chk.Check();
            Button DeleteData_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbDeleteSelectedQtys']");
            HouseBulkActions_btn.Click();
            DeleteData_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductsToHouses']/div[1]");
        }

        public void AddHouseOptionQuantities(OptionQuantitiesData optionPhaseData)
        {
            Button Add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewQuantity']");
            if (Add_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Can't find Add new Option Quantities button. Stop this step.</font>");
                return;
            }
            Add_btn.Click(false);

            Label title_lbl = new Label(FindType.XPath, "//h1[text()='Add Option House Product']");
            if (title_lbl.IsDisplayed(false) is false)
                ExtentReportsHelper.LogFail("<font color='red'>Can't Open Add Product modal or the title is incorrect.</font>");
            PoppulateHouseQuantitiesToModal(optionPhaseData);
        }
        public void PoppulateHouseQuantitiesToModal(OptionQuantitiesData optionPhaseData)
        {
            // Select Community
            DropdownList Community_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCommunitiesNewProduct']");
            if (Community_ddl.IsDisplayed(false) is true && optionPhaseData.Community != string.Empty)
            {
                Community_ddl.SelectItem(optionPhaseData.Community, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel1']/div[1]");
            }

            //Select House 
            DropdownList House_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewHouseProduct']");
            if (House_ddl.IsDisplayed(false) is true && optionPhaseData.House != string.Empty)
            {
                House_ddl.SelectItem(optionPhaseData.House, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel1']/div[1]");
            }

            // Select Building Phase
            DropdownList phase_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewBuildingPhases']");
            if (phase_ddl.IsDisplayed(false) is true && optionPhaseData.BuildingPhase != string.Empty)
            {
                phase_ddl.SelectItem(optionPhaseData.BuildingPhase, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel1']/div[1]");
            }
            /*
            // Select Category
            DropdownList category_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlHouseOptionProductCategory']");
            if (category_ddl.IsDisplayed(false) is true && optionPhaseData.Category != string.Empty)
            {
                category_ddl.SelectItem(optionPhaseData.Category, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel1']/div[1]");
            }
            */
            // Select Product
            Button product_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId2']/span/button");
            product_btn.Click(false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId2_LoadingDiv']");
            // Check if the dropdown panel doesn't display then clicking again.
            string productPanel_Xpath = "//*[@id='ctl00_CPH_Content_rcbProductId2_DropDown']/div/ul[@class='rcbList']";
            IWebElement ProductPanel = FindElementHelper.FindElement(FindType.XPath, productPanel_Xpath);
            if (ProductPanel.Enabled == false)
            {
                product_btn.JavaScriptClick(false);
                CommonHelper.WaitUntilElementVisible(5, productPanel_Xpath);
            }
            string productXpath = productPanel_Xpath + $"/li[contains(text(),'{optionPhaseData.ProductName}')]";

            Button value_btn = new Button(FindType.XPath, productXpath);
            IWebElement value_txt = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId2_Input']");
            System.Threading.Thread.Sleep(3000);
            if (value_btn.IsDisplayed() is false)
                ExtentReportsHelper.LogFail($"The Product <font color='red'><b>{optionPhaseData.ProductName + "-" + optionPhaseData.ProductDescription }</b></font> is not displayed on Product list.");
            else
            {
                value_txt.SendKeys(optionPhaseData.ProductName);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId2_LoadingDiv']",5000);
                value_btn.RefreshWrappedControl();
                value_btn.WaitForElementIsVisible(5);
                value_btn.Click();               
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel1']/div[1]");
                System.Threading.Thread.Sleep(5000);
            }

            // Select Style
            DropdownList style_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStyle']");
            if (!style_ddl.IsNull() && optionPhaseData.Style != string.Empty)
            {
                style_ddl.SelectItem(optionPhaseData.Style, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel1']/div[1]");
            }

            // Select Use
            DropdownList use_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlUse']");
            if (!use_ddl.IsNull() && optionPhaseData.Use != string.Empty)
                use_ddl.SelectItem(optionPhaseData.Use, true);
            string loadingIconXpathLatest = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_RadAjaxPanel1']/div[1]";
            //CommonHelper.WaitUntilElementVisible(6, loadingIconXpathLatest);
            WaitingLoadingGifByXpath(loadingIconXpathLatest, 5000);

            // Set Quantity           
            Textbox quantity_txt = new Textbox(FindType.XPath, "//input[@id='ctl00_CPH_Content_txtNewQuantity']");
            if (!quantity_txt.IsNull() && quantity_txt.IsDisplayed() == true && quantity_txt.IsClickable() == true)                
                quantity_txt.SetText(optionPhaseData.Quantity.ToString());


            // Select Source
            DropdownList source_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewSourceType']");
            if (!source_ddl.IsNull() && optionPhaseData.Source != string.Empty)
                source_ddl.SelectItem(optionPhaseData.Source, true);


            // Click save button           
            Button add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertNewQuantity']");
            if (!add_btn.IsNull())
                add_btn.Click();

            // Verify toast message
            string _expectedMessage = $"House Quantity created successfully!";
            string actualMsg = GetLastestToastMessage();
            if (actualMsg == _expectedMessage)
            {
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>House Option Quantities with phase {optionPhaseData.BuildingPhase} added successfully!</b></font>");
            }
            else
            {
                if (IsOptionQuantitiesInGrid("Building Phase", optionPhaseData.BuildingPhase) is false)
                    ExtentReportsHelper.LogFail($"<font color = 'red'>House Option quantities with phase {optionPhaseData.BuildingPhase} could not be added - Possible constraints preventing additional.</font>");
                else
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>House Option quantities with phase {optionPhaseData.BuildingPhase} added successfully!</b></font>");
            }
        
    }


    public void SelectCommunity(string selectedCommunity)
    {
        if (selectedCommunity != string.Empty)
        {
            Community_list.SelectItem(selectedCommunity);
            WaitingLoadingGifByXpath(loadingIcon_Xpath, 1000);
        }
    }

    public void GenerateGlobalBom(string community)
    {

        //CLick BOM generation button
        if (Generate_Bom_Button.IsDisplayed(false) is true)
        {
            // Click generate BOM button
            Generate_Bom_Button.Click();
            WaitingLoadingGifByXpath(loadingIcon_Xpath);
            System.Threading.Thread.Sleep(1000);

            string actualToastMess = GetLastestToastMessage();
            string expectedMess = "Generate a BOM for this global option successfully!";
            if (actualToastMess.Contains(expectedMess))
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Generate BOM successfully. Toast message's same as the expectation.</b></font>");
            else
                ExtentReportsHelper.LogFail(null, $"<font color='green'>Generate BOM unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                    $"<br>The expectation: {expectedMess}" +
                    $"<br>The actual result: {actualToastMess}");
        }
        else
        {
            ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>'BOM Generation'</b> button.</font>");
        }
    }

    public void FilterItemInGlobalBomQuantitiesGrid(string column, GridFilterOperator gridFilterOperator, string valueToFind)
    {
        GlobalBomQuantitiesGrid.FilterByColumn(column, gridFilterOperator, valueToFind);
        WaitingLoadingGifByXpath(loadingIcon_GlobalBom, 1000);
    }

    public bool IsItemInGlobalBomQuantitiesGrid(string column, string valueToFind)
    {
        return GlobalBomQuantitiesGrid.IsItemWithTextContainsOnCurrentPage(column, valueToFind);
    }
        public void EditItemInGrid(string columnName, string value)
        {
            optionQuantitiesGrid.ClickEditItemInGrid(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]");
        }
        public void UpdateItemInGrid(OptionQuantitiesData optionPhaseData)
        {

            // Select Style
            DropdownList style_ddl = new DropdownList(FindType.XPath, "//*[contains(@id,'ddlEditStyles')]");
            if (!style_ddl.IsNull() && optionPhaseData.Style != string.Empty)
            {
                style_ddl.SelectItem(optionPhaseData.Style, true);
            }

            // Select Use
            DropdownList use_ddl = new DropdownList(FindType.XPath, "//*[contains(@id,'ddlEditUses')]");
            if (!use_ddl.IsNull() && optionPhaseData.Use != string.Empty)
                use_ddl.SelectItem(optionPhaseData.Use, true);

            // Set Quantity
            Textbox quantity_txt = new Textbox(FindType.XPath, "//*[contains(@id,'txtProductsToOptions_Quantity')]");
            if (!quantity_txt.IsNull() && Convert.ToDouble(optionPhaseData.Quantity) > 0)
                quantity_txt.SetText(optionPhaseData.Quantity.ToString());


            // Select Source
            DropdownList source_ddl = new DropdownList(FindType.XPath, "//*[contains(@id,'dlSourceTypes')]");
            if (!source_ddl.IsNull() && optionPhaseData.Source != string.Empty)
                source_ddl.SelectItem(optionPhaseData.Source, true);


            // Click save button
            Button add_btn = new Button(FindType.XPath, "//*[contains(@id,'btnEFUpdate')]");
            if (!add_btn.IsNull())
                add_btn.Click();

            // Verify toast message
            string _expectedMessage = $"Option Product saved successfully!";
            string actualMsg = GetLastestToastMessage();
            if (actualMsg == _expectedMessage)
            {
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Option Quantities with phase {optionPhaseData.BuildingPhase} added successfully!</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color = 'red'>Option quantities with phase {optionPhaseData.BuildingPhase} could not be added - Possible constraints preventing additional.</font>");
            }
        }

        public void VerifyItemOnGlobalBomGrid(GlobalOptionBomQuantitesData expectedData, bool isVerifyWaste = true)
    {
        // Filter option name
        FilterItemInGlobalBomQuantitiesGrid("Option", GridFilterOperator.Contains, expectedData.optionName);

        if (IsItemInGlobalBomQuantitiesGrid("Option", expectedData.optionName) is false)
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2: Can't find Option with name <b>'{expectedData.optionName}'</b></font>");
            // ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name <b>'{expectedData.optionName}'</b>on the Global Bom grid view." +
            //   "<br>Failed to generate Global Option BOM.</font></br>");
            return;
        }

        // Verify Expand Option
        Button expandOption = new Button(FindType.XPath, $"//*[text()='{expectedData.optionName}']/ancestor::tr/td[@class='rgExpandCol']/input");
        if (expandOption.IsDisplayed(false) is false)
        {
            ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand Option</b> button on Global Bom grid view." +
                "<br>Failed to verify Global BOM.</font></br>");
            return;
        }

        expandOption.RefreshWrappedControl();
        expandOption.Click();

        foreach (ProductToOptionData item in expectedData.productToOption)
        {
            Button expandPhase = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");

            // Verify Building Phase
            if (expandPhase.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{item.BuildingPhase}'</b> on grid view." +
                    $"<br>Failed to verify Global Option BOM.</font></br>");
                return;
            }

            expandPhase.Click();

            //Verify Product
            // Verify each product quantities on current building phase
            foreach (ProductData expectedProduct in item.ProductList)
            {
                // Verify Product and total quantities
                Label product = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_pnlGlobalOptionBOMsPanel']//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                Label quanities = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_pnlGlobalOptionBOMsPanel']//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]/../following-sibling::td/a");
                Label waste = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_pnlGlobalOptionBOMsPanel']//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]/../following-sibling::td[7]");
                Label rounding = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_pnlGlobalOptionBOMsPanel']//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]/../following-sibling::td[8]");

                if (product.IsDisplayed(false) is false || quanities.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                        $"and Quantities <b>'{expectedProduct.Quantities}'</b> on Global Option BOM grid view." +
                        "<br>Failed to verify Global Option BOM.</font></br>");
                }
                else
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                           $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                    if (isVerifyWaste is true)
                    {
                        System.Threading.Thread.Sleep(1000);
                        if (waste.IsDisplayed(false) is true || rounding.IsDisplayed(false) is true)
                        {
                            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Waste '0.10' " +
                           $"and rounding '-0.10' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                        }
                    }
                }

            }

            // Collapse building phase
            expandPhase.Click(false);
        }
    }
        public void DeleteAllProductToOptionInHouseOptionQuantity()
        {
            CheckBox CheckAll = new CheckBox(FindType.XPath, "//h1[contains(.,'House Option Quantities')]/ancestor::article[1]/div//colgroup/following-sibling::thead/tr[1]/th[1]/input");
            CheckAll.Check();
            Button BulkAc = new Button(FindType.XPath, "//h1[contains(.,'House Option Quantities')]/preceding-sibling::div/a");
            BulkAc.Click();
            Button DeleteSelectedFiles = new Button(FindType.XPath, "//h1[contains(.,'House Option Quantities')]/preceding-sibling::div/ul/li/a[contains(.,'Delete Selected')]");
            DeleteSelectedFiles.Click();
            bool alertAppear = CommonHelper.WaitUntilAlertAppears(driver, TimeSpan.FromSeconds(10));
            if (alertAppear)
            {
                driver.SwitchTo().Alert().Accept();
            }
            else
            {
                return;
            }
        }

}
}


