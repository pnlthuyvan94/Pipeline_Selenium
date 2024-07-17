using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.IO;
using System.Reflection;
using System.Text;
using static Pipeline.Testing.Script.Section_V.A5_A_PIPE_19165;

namespace Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail
{
    public partial class SpecSetDetailPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            SpecSet_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return SpecSet_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void OpenCreateSpecSetModal()
        {
            // Click add on the modal
            FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_lbAddNewSet").Click();
            // FindElementHelper.FindElement(FindType.Id, "ctl00_CPH_Content_lbNew").Click();
            System.Threading.Thread.Sleep(1000);
        }

        public void DeleteSpecSet(string item)
        {
            //span[text()='RT_SpecSet-Auto']//..//..//td//input[contains(@src,'delete')]
            CommonHelper.ScrollToBeginOfPage();
            FindElementHelper.FindElement(FindType.XPath, $"//span[text()='{item}']//..//..//td//input[contains(@src,'delete')]").Click();
            //SpecSet_Grid.ClickDeleteFirstItem();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
        }


        public void CreateNewSpecSet(string name)
        {
            Name_txt.SetText(name);
            Save_btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
            string expectedMsg = "Spec Set created!";
            string actualMsg = GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The spec set with name {name} </b> is created successfully.</b></font>");
                CloseToastMessage();
            }
            else
                ExtentReportsHelper.LogFail($"<font color='red'>The spec set is not create successfully.</font> Actual message: <i>{actualMsg}</i>");
        }

        public void CloseCreateSpecSetModal()
        {
            CloseModal_btn.Click();
            ModalTitle_lbl.WaitForElementIsInVisible(10);
        }

        public void ExpandAllSpecSet()
        {
            ExpandAll_btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
            AddNewProduct_Btn.WaitForElementIsVisible(10);
        }

        public void PerformInsertStyle()
        {
            PerformAddNewStyle_Btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
        }

        public void PerformInsertProduct()
        {
            PerformAddNewProduct_Btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
        }

        public SpecSetDetailPage ClickAddNewConversationStyle()
        {
            AddNewStyle_Btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage, 1000);
            return this;
        }

        public SpecSetDetailPage ClickAddNewProduct()
        {
            AddNewProduct_Btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage, 1000);
            return this;
        }

        public string SelectOriginalManufacture(string data)
        {
            if (OriManufacture_ddl.IsItemInList(data))
            {
                OriManufacture_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                OriManufacture_ddl.SelectItem(0);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(OriManufacture_ddl), $"Select OriginalManufacturer {OriManufacture_ddl.SelectedItemName} on the dropdown list.");
                return OriManufacture_ddl.SelectedItemName;
            }
        }

        public string SelectNewManufacture(string data)
        {
            if (NewManufacture_ddl.IsItemInList(data))
            {
                NewManufacture_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                NewManufacture_ddl.SelectItem(2);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(NewManufacture_ddl), $"Select NewManufacturer {NewManufacture_ddl.SelectedItemName} on the dropdown list.");
                return NewManufacture_ddl.SelectedItemName;
            }
        }

        public string SelectOriginalStyle(string data)
        {
            return OriStyle_ddl.SelectItemByValueOrIndex(data, 0);
        }
        public string SelectNewStyle(string data)
        {
            return NewStyle_ddl.SelectItemByValueOrIndex(data, 1);

        }
        public string SelectOriginalUse(string data)
        {
            return OriUse_ddl.SelectItemByValueOrIndex(data, 1);
        }
        public string SelectNewUse(string data)
        {
            return NewUse_ddl.SelectItemByValueOrIndex(data, 2);
        }
        public string SelectStyleCalculation(string data)
        {
            if(data == "NONE")
            {
                return StyleCalculation_ddl.SelectItemByValueOrIndex(data, 0);
            }    
            else
            {              

                return StyleCalculation_ddl.SelectItemByValueOrIndex(data, 1);
            }    
            
        }

        public void AddStyleConversion(SpecSetData specSetData)
        {
            ClickAddNewConversationStyle();
            SelectOriginalManufacture(specSetData.OriginalManufacture);
            SelectOriginalStyle(specSetData.OriginalStyle);
            SelectOriginalUse(specSetData.OriginalUse);
            SelectNewManufacture(specSetData.NewManufacture);
            SelectNewStyle(specSetData.NewStyle);
            SelectNewUse(specSetData.NewUse);
            SelectStyleCalculation(specSetData.StyleCalculation);
            PerformInsertStyle();
        }

        // Product
        public string SelectOriginalBuildingPhase(string data)
        {
            if (OriginalBuildingPhase_ddl.IsItemInList(data))
            {
                OriginalBuildingPhase_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                OriginalBuildingPhase_ddl.SelectItem(0);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(OriginalBuildingPhase_ddl), $"Select Original BuildingPhase {OriginalBuildingPhase_ddl.SelectedItemName} on the dropdown list.");
                return OriginalBuildingPhase_ddl.SelectedItemName;
            }
        }

        public string SelectNewBuildingPhase(string data)
        {
            if (NewBuildingPhase_ddl.IsItemInList(data))
            {
                NewBuildingPhase_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                NewBuildingPhase_ddl.SelectItem(1);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(NewBuildingPhase_ddl), $"Select New BuildingPhase {NewBuildingPhase_ddl.SelectedItemName} on the dropdown list.");
                return NewBuildingPhase_ddl.SelectedItemName;
            }
        }

        public string SelectOriginalProductStyle(string data)
        {
            return OriginalProductStyle_ddl.SelectItemByValueOrIndex(data, 0);

        }
        public void SelectOriginalProduct(string data)
        {
            Textbox OriginalProduct_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_radOriginalProduct_Input']");
            OriginalProduct_txt.SetText(data);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_radOriginalProduct_LoadingDiv']");
            Button OriginalProduc_btn = new Button(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_radOriginalProduct_DropDown']//li)[1]");
            OriginalProduc_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");

        }
        public void SelectNewProduct(string data)
        {
            Textbox newProduct_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_radNewProduct_Input']");
            newProduct_txt.SetText(data);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_radNewProduct_LoadingDiv']");
            Button originalProduc_btn = new Button(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_radNewProduct_DropDown']//li)[1]");
            originalProduc_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");

        }
        public string SelectOriginalProductUse(string data)
        {
            return OriProductUse_ddl.SelectItemByValueOrIndex(data, 0);
        }
        public string SelectNewProductUse(string data)
        {
            return NewProductUse_ddl.SelectItemByValueOrIndex(data, 1);
        }

        public string SelectNewProductStyle(string data)
        {
            return NewProductStyle_ddl.SelectItemByValueOrIndex(data, 1);
        }

        public string SelectProductCalculation(string data)
        {
            return ProductCalculation_ddl.SelectItemByValueOrIndex(data, 1);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specSetName">spec set name that assigned to community</param>
        /// <param name="columnName"> Here is the column name from xpath</param>
        /// <param name="valueToFind">value of column</param>
        public void IsIemOnProductConversionGrid(string specSetName, string columnName, string valueToFind)
        {
            string productConversion_Xpath = $"//span[contains(text(), '{specSetName}')]//ancestor::tbody/tr[2]//table[./caption[text() = 'Product Conversions']]//span[contains(@id, 'lbl{columnName}') and text() = '{valueToFind}']";
            IWebElement productConversion = driver.FindElement(By.XPath($"{productConversion_Xpath}"));

            if (productConversion.Displayed is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>{columnName} with value {valueToFind} displayed correctly on the Product Conversion grid.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find {columnName} with value {valueToFind} on the Product Conversion grid.</font>");

        }

        public bool IsIemOnStyleConversionGrid(string specSetName, string columnName, string valueToFind)
        {
            // Column Name shoud be one of following value: OriginalMfg_Name, OriginalStyle_Name, OriginalUse, NewMfg_Name, NewStyle_Name, NewUse, Calculation
            string findItem_Xpath = $"//span[contains(text(), '{specSetName}')]//ancestor::tbody/tr[2]//table[./caption[text() = 'Style Conversions']]//span[contains(@id, 'lbl{columnName}') and text() = '{valueToFind}']";

            Label StyleConversion = new Label(FindType.XPath, findItem_Xpath);
            return StyleConversion.IsDisplayed();
        }

        public void IsItemProductOnProductConversionGrid(string specSetName, string product)
        {
            string product_Xpath = $"//span[contains(text(), '{specSetName}')]//ancestor::tbody/tr[2]//table[./caption[text() = 'Product Conversions']]//a[contains(text(),'{product}')]";
            IWebElement productConversion = driver.FindElement(By.XPath($"{product_Xpath}"));

            if (productConversion.Displayed is true)
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Product with value {product} displayed correctly on the Product Conversion grid.</b></font>");
            else
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find The Product with value {product} on the Product Conversion grid.</font>");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="specSetName">spec set name that assigned to community</param>
        /// <param name="columnName"> Here is the column name from xpath</param>
        /// <param name="valueToFind">value of column</param>
        public bool IsItemOnCommunityGrid(string specSetName, string communityCode)
        {
            // "ColumnName" should be one of following value: SpecSets_Name, Communities_Name, Communities_Code
            string findItem_Xpath = $"//table[contains(@id, 'rgSets_Communities')]//tr[.//span[contains(@id, 'lblCommunities_Code') and text() = '{communityCode}'] and .//span[contains(@id, 'lblProductSpecSets_Name') and text() = '{specSetName}']]";

            Label community = new Label(FindType.XPath, findItem_Xpath);
            return community.IsDisplayed();

        }

        /// <summary>
        /// </summary>
        /// <param name="specSetName">spec set name that assigned to community</param>
        /// <param name="columnName"> Here is the column name from xpath</param>
        /// <param name="optionName">value of column</param>
        public bool IsItemOnOptionGrid(string specSetName, string optionName)
        {
            string findItem_Xpath = $"//tr[contains(@id, 'Content_rgSets_Options')][.//span[contains(@id,'_rgSets_Options') and text() = '{optionName}'] and .//span[contains(@id, '_lblProductSpecSets_Name') and contains(text(),'{specSetName}') ]]";

            Label option = new Label(FindType.XPath, findItem_Xpath);
            return option.IsDisplayed();
        }

        /// <summary>
        /// Delete first row of Style Conversion 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueToFind"></param>
        public void DeleteStyleConversionFirtRow()
        {
            string loadingIcon = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']";
            Grid StyleConversionGrid = new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11']", loadingIcon);
            StyleConversionGrid.ClickDeleteFirstItem();
            WaitingLoadingGifByXpath(loadingIcon);
        }

        /// <summary>
        /// Add Style Conversion
        /// </summary>
        /// <param name="SpecSetData"></param>
        public void AddStyleConversion(StyleConversion specSetData)
        {
            ClickAddNewConversationStyle();
            SelectOriginalManufacture(specSetData.OriginalManu);
            SelectOriginalStyle(specSetData.OriginalStyle);
            SelectOriginalUse(specSetData.OriginalUse);
            SelectNewManufacture(specSetData.NewManu);
            SelectNewStyle(specSetData.NewStyle);
            SelectNewUse(specSetData.NewUse);
            SelectStyleCalculation(specSetData.Calculation);
            PerformInsertStyle();
        }
        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="attributeName"></param>
        /// <param name="Item_Name"></param>
        /// <param name="SpecSet_Name"></param>
        /// <param name="Community_Name"></param>
        public void AddNameItemAndCheckItemInGrid(string attributeName, string item_Name, string SpecSet_Name, string Community_Name)
        {
            string expectedMsg;
            string addNewItem;
            string attributeName_SelectXpath;
            string modalloading;
            string AddAtributeNameToSpecSet_xpath;
            string save_Xpath;
            string ItemName_Xpath;
            string specSetGrid_Xpath;
            switch (attributeName)
            {
                case "Divisions":
                    addNewItem = "//*[@id='ctl00_CPH_Content_lbOpenDivisionModal']";
                    attributeName_SelectXpath = "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlAddDivisionsPanel']/select";
                    modalloading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlAddDivisions']/div[1]";
                    AddAtributeNameToSpecSet_xpath = "//*[@id='ctl00_CPH_Content_ddlAddDivisionSpecSet']";
                    save_Xpath = "//*[@id='ctl00_CPH_Content_lbSaveAddDivision']";
                    expectedMsg = "Division Spec Set succesfully added!";
                    ItemName_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Divisions_ctl00__0']//span[contains(text(),'{item_Name}')]";
                    specSetGrid_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Divisions_ctl00__0']//span[contains(text(),'{SpecSet_Name}')]";
                    break;

                case "Communities":
                    addNewItem = "//*[@id='ctl00_CPH_Content_lbNew_Communities']";
                    attributeName_SelectXpath = "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlNewCommunityPanel']/select";
                    modalloading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewCommunity']/div[1]";
                    AddAtributeNameToSpecSet_xpath = "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlNewSpecSetsPanel']/select";
                    save_Xpath = "//*[@id='ctl00_CPH_Content_lbSaveContent']";
                    expectedMsg = "Community Spec Set added!";
                    ItemName_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Communities_ctl00__0']//span[contains(text(),'{SpecSet_Name}')]//ancestor::tr//td[2]/span";
                    specSetGrid_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Communities_ctl00__0']//span[contains(text(),'{SpecSet_Name}')]";
                    break;

                case "Houses":
                    addNewItem = "//*[@id='ctl00_CPH_Content_lbNew_Houses']";
                    attributeName_SelectXpath = "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlNewHousePanel']/select";
                    modalloading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewHouse']/div[1]";
                    AddAtributeNameToSpecSet_xpath = "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlNewSpecSets_HousesPanel']/select";
                    save_Xpath = "//*[@id='ctl00_CPH_Content_lbSaveContent_Houses']";
                    expectedMsg = "House Spec Set was added.";
                    ItemName_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Houses_ctl00__0']//span[contains(text(),'{SpecSet_Name}')]//ancestor::tr//td[4]/span";
                    specSetGrid_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Houses_ctl00__0']//span[contains(text(),'{SpecSet_Name}')]";
                    break;

                case "Jobs":
                    addNewItem = "//*[@id='ctl00_CPH_Content_lbNew_Jobs']";
                    attributeName_SelectXpath = "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlNewJobPanel']/select";
                    modalloading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewJob']/div[1]";
                    AddAtributeNameToSpecSet_xpath = "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlNewSpecSets_JobsPanel']/select";
                    save_Xpath = "//*[@id='ctl00_CPH_Content_lbSaveContent_Jobs']";
                    expectedMsg = "Job Spec Set was added.";
                    ItemName_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Jobs_ctl00__0']//span[contains(text(),'{item_Name}')]";
                    specSetGrid_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Jobs_ctl00__0']//span[contains(text(),'{SpecSet_Name}')]";
                    break;

                case "Options":
                    addNewItem = "//*[@id='ctl00_CPH_Content_lbNew']";
                    attributeName_SelectXpath = "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_pnlAjaxNewOptionPanel']//select";
                    modalloading = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAjaxNewOption']/div[1]";
                    AddAtributeNameToSpecSet_xpath = "//*[@id='ctl00_CPH_Content_ddlNewSpecSets_Options']";
                    save_Xpath = "//*[@id='ctl00_CPH_Content_lbSaveContent_Options']";
                    expectedMsg = "Option Spec Set added!";
                    ItemName_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Options_ctl00__0']//span[contains(text(),'{item_Name}')]";
                    specSetGrid_Xpath = $"//*[@id='ctl00_CPH_Content_rgSets_Options_ctl00__0']//span[contains(text(),'{SpecSet_Name}')]";
                    break;
                default:
                    ExtentReportsHelper.LogFail("<font color='red'>The Attribute Name is not found</font>");
                    return;
            }
            //Click Add Modal
            Button AddNew_btn = new Button(FindType.XPath, addNewItem);
            AddNew_btn.Click();
            System.Threading.Thread.Sleep(1000);
            if (attributeName == "Houses")
            {
                DropdownList CommunityItem_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewCommunity_Houses']");
                CommunityItem_ddl.SelectItem(Community_Name);
                WaitingLoadingGifByXpath(modalloading, 2000);
            }
            else if (attributeName == "Jobs")
            {
                DropdownList CommunityItem_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_ddlCommunitiesPanel']/select");
                CommunityItem_ddl.SelectItem(Community_Name);
                WaitingLoadingGifByXpath(modalloading, 2000);
            }

            DropdownList ItemAttributeName_select = new DropdownList(FindType.XPath, attributeName_SelectXpath);
            ItemAttributeName_select.SelectItem(item_Name);
            WaitingLoadingGifByXpath(modalloading, 2000);
            DropdownList AddAttributeNameToSpecSet_ddl = new DropdownList(FindType.XPath, AddAtributeNameToSpecSet_xpath);
            AddAttributeNameToSpecSet_ddl.SelectItem(SpecSet_Name);
            Button Save_btn = new Button(FindType.XPath, save_Xpath);
            Save_btn.Click();
            WaitingLoadingGifByXpath(modalloading);
            string actualMsg = GetLastestToastMessage(2);
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"The {attributeName} with name <b>{item_Name}</b> is added successfully.");
                CloseToastMessage();
                Label ItemName_lbl = new Label(FindType.XPath, ItemName_Xpath);
                Label SpecSetInGrid_lbl = new Label(FindType.XPath, specSetGrid_Xpath);
                ItemName_lbl.WaitForElementIsVisible(5);
                if (ItemName_lbl.IsDisplayed(false) && item_Name.EndsWith(ItemName_lbl.GetText()) && SpecSetInGrid_lbl.GetText().Equals(SpecSet_Name))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The {ItemName_lbl.GetText()} Name in grid is displayed correctly with SpecSet Name: {SpecSetInGrid_lbl.GetText()}.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The {ItemName_lbl.GetText()} Name in grid is displayed wrong with SpecSet Name: {SpecSetInGrid_lbl.GetText()}.</font>");
                }
            }
            else
                ExtentReportsHelper.LogFail($"The {item_Name} is not create successfully. Actual message: <i>{actualMsg}</i>");
        }

        public void EditNameItemAndCheckItemInGrid(string attributeName, string columnNameToFind, string valueToFind, string valueSpecSetToUpdate)
        {
            string inputXpath;
            string update_Btn;
            IGrid currentGrid;
            string expectedMsg;
            string xpathAfterUpdate;
            switch (attributeName)
            {
                case "Divisions":
                    inputXpath = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_DivisionsPanel']//span[contains(text(),'{valueToFind}')]//ancestor::tr//select";
                    update_Btn = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_DivisionsPanel']//span[contains(text(),'{valueToFind}')]//ancestor::tr//td[3]/input[@title='Update']";
                    currentGrid = Divisions_Grid;
                    expectedMsg = "Spec Set updated!";
                    xpathAfterUpdate = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_DivisionsPanel']//span[contains(text(),'{valueSpecSetToUpdate}')]";
                    break;

                case "Communities":
                    inputXpath = $"//*[@id='ctl00_CPH_Content_rgSets_Communities']//span[contains(text(),'{valueToFind}')]//ancestor::tr//select";
                    update_Btn = $"//*[@id='ctl00_CPH_Content_rgSets_Communities']//span[contains(text(),'{valueToFind}')]//ancestor::tr//td[4]/input[@title='Update']";
                    currentGrid = Communities_Grid;
                    expectedMsg = "Spec Set updated!";
                    xpathAfterUpdate = $"//*[@id='ctl00_CPH_Content_rgSets_Communities']//span[contains(text(),'{valueSpecSetToUpdate}')]";
                    break;

                case "Houses":
                    inputXpath = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_HousesPanel']//span[contains(text(),'{valueToFind}')]//ancestor::tr//select";
                    update_Btn = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_HousesPanel']//span[contains(text(),'{valueToFind}')]//ancestor::tr//td[6]/input[@title='Update']";
                    currentGrid = Houses_Grid;
                    expectedMsg = "Spec Set updated!";
                    xpathAfterUpdate = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_HousesPanel']//span[contains(text(),'{valueSpecSetToUpdate}')]";
                    break;

                case "Jobs":
                    inputXpath = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_JobsPanel']//span[contains(text(),'{valueToFind}')]//ancestor::tr//select";
                    update_Btn = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_JobsPanel']//span[contains(text(),'{valueToFind}')]//ancestor::tr//td[3]/input[@title='Update']";
                    currentGrid = Jobs_Grid;
                    expectedMsg = "Spec Set updated!";
                    xpathAfterUpdate = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_JobsPanel']//span[contains(text(),'{valueSpecSetToUpdate}')]";
                    break;

                case "Options":
                    inputXpath = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_OptionsPanel']//span[contains(text(),'{valueToFind}')]//ancestor::tr//select";
                    update_Btn = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_OptionsPanel']//span[contains(text(),'{valueToFind}')]//ancestor::tr//td[4]/input[@title='Update']";
                    currentGrid = Options_Grid;
                    expectedMsg = "Spec Set updated!";
                    xpathAfterUpdate = $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgSets_OptionsPanel']//span[contains(text(),'{valueSpecSetToUpdate}')]";
                    break;

                default:
                    ExtentReportsHelper.LogFail("<font color='red'>The Attribute Name is not found</font>");
                    return;
            }

            currentGrid.ClickEditItemInGrid(columnNameToFind, valueToFind);
            currentGrid.WaitGridLoad();
            DropdownList edit_select = new DropdownList(FindType.XPath, inputXpath);
            if (edit_select.IsNull())
                ExtentReportsHelper.LogFail($"The field with column name <b>{columnNameToFind}</b> and value <b>{valueToFind}</b> is NOT have the edit textbox in grid</b>.");
            else
            {
                edit_select.SelectItem(valueSpecSetToUpdate);
                Button Update_Btn = new Button(FindType.XPath, update_Btn);
                Update_Btn.Click();
                currentGrid.WaitGridLoad();
                System.Threading.Thread.Sleep(3000);
            }

            if (expectedMsg.Equals(GetLastestToastMessage()))
            {
                ExtentReportsHelper.LogPass($"SpecSet was successfully updated.");
            }
            Label editAfterUpdatedata_txt = new Label(FindType.XPath, xpathAfterUpdate);
            if (editAfterUpdatedata_txt.GetText().Equals(valueSpecSetToUpdate))
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The {attributeName} with column name <b>{columnNameToFind}</b> and value <b>{valueSpecSetToUpdate}</b> is updated successfully in grid.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The {attributeName} with column name<b>{columnNameToFind}</b> and value <b>{valueSpecSetToUpdate}</b> is NOT update in grid.</font>");
            }

        }

        public void DeleteNameItemAndCheckItemInGrid(string attributeName, string columnName, string value)
        {
            IGrid currentGrid;
            string loading_Xpath;
            switch (attributeName)
            {
                case "Divisions":
                    currentGrid = Divisions_Grid;
                    loading_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Divisions']";
                    break;

                case "Communities":
                    currentGrid = Communities_Grid;
                    loading_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Communities']";
                    break;

                case "Houses":
                    currentGrid = Houses_Grid;
                    loading_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Houses']";
                    break;

                case "Jobs":
                    currentGrid = Jobs_Grid;
                    loading_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Jobs']";
                    break;

                case "Options":
                    currentGrid = Options_Grid;
                    loading_Xpath = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets_Options']";
                    break;
                default:
                    ExtentReportsHelper.LogFail("<font color='red'>The Attribute Name is not found</font>");
                    return;
            }
            if (currentGrid.IsItemOnCurrentPage(columnName, value) is true)
            {
                currentGrid.ClickDeleteItemInGrid(columnName, value);
                ConfirmDialog(ConfirmType.OK);
                WaitingLoadingGifByXpath(loading_Xpath);
                // Expected: delete successful
                string actualMsg = GetLastestToastMessage();
                string expectedDeleteSuccessfulMess = "Spec Set removed!";
                if (expectedDeleteSuccessfulMess.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The {value} removed successfully from {attributeName}!</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The {value} could not be deleted from {attributeName}!</font> Actual message: {actualMsg}");
                }
            }
        }

        public string SelectProductOriginalCategory(string data)
        {
            if (OriginalCategory_ddl.IsItemInList(data))
            {
                OriginalCategory_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                OriginalCategory_ddl.SelectItem(1);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(OriginalCategory_ddl), $"Select Original Category {OriginalCategory_ddl.SelectedItemName} on the dropdown list.");
                return OriginalCategory_ddl.SelectedItemName;
            }
        }

        public string SelectNewProductCategory(string data)
        {
            if (NewCategory_ddl.IsItemInList(data))
            {
                NewCategory_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                NewCategory_ddl.SelectItem(1);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(NewCategory_ddl), $"Select New Category {NewCategory_ddl.SelectedItemName} on the dropdown list.");
                return NewCategory_ddl.SelectedItemName;
            }
        }

        public void AddProductConversionWithoutCategory(SpecSetData specSetData)
        {
            ClickAddNewProduct();
            SelectOriginalBuildingPhase(specSetData.OriginalPhase);
            SelectOriginalProduct(specSetData.OriginalProduct);
            SelectOriginalProductStyle(specSetData.OriginalProductStyle);
            SelectOriginalProductUse(specSetData.OriginalProductUse);
            SelectNewBuildingPhase(specSetData.NewPhase);
            SelectNewProduct(specSetData.NewProduct);
            SelectNewProductStyle(specSetData.NewProductStyle);
            SelectNewProductUse(specSetData.NewProductUse);
            SelectProductCalculation(specSetData.ProductCalculation);
            PerformInsertProduct();
        }

        public void AddProductConversionWithCategory(SpecSetData specSetData)
        {
            ClickAddNewProduct();
            SelectOriginalBuildingPhase(specSetData.OriginalPhase);
            SelectProductOriginalCategory(specSetData.OriginalCategory);
            SelectOriginalProduct(specSetData.OriginalProduct);
            SelectOriginalProductStyle(specSetData.OriginalProductStyle);
            SelectOriginalProductUse(specSetData.OriginalProductUse);
            SelectNewBuildingPhase(specSetData.NewPhase);
            SelectNewProductCategory(specSetData.NewCategory);
            SelectNewProduct(specSetData.NewProduct);
            SelectNewProductStyle(specSetData.NewProductStyle);
            SelectNewProductUse(specSetData.NewProductUse);
            SelectProductCalculation(specSetData.ProductCalculation);
            PerformInsertProduct();
        }

        public SpecSetDetailPage EditItemProductConversionsInGrid(string originalproduct)
        {
            Button EditItem_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets']//a[contains(text(),'{originalproduct}')]//ancestor::tr/td[6]");
            EditItem_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            return this;
        }

        public SpecSetDetailPage Update_SelectOriginalBuildingPhase(string data)
        {
            UpdateOriginalBuildingPhase_ddl.SelectItem(data, false, false);
            WaitingLoadingGifByXpath(_loadingOnDetailPage, 3);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(UpdateOriginalBuildingPhase_ddl), $"Select {data} on the dropdown list.");
            return this;
        }

        public SpecSetDetailPage Update_SelectOriginalProduct(string data)
        {
            Textbox updateOriginalProduct_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_radOriginalProduct_Input']");
            updateOriginalProduct_txt.SetText(data);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_radOriginalProduct_LoadingDiv']");
            Button updateOriginalProduc_btn = new Button(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_radOriginalProduct_DropDown']//li)[1]");
            updateOriginalProduc_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            return this;
        }
        public SpecSetDetailPage Update_SelectOriginalProductStyle(string data)
        {
            UpdateOriginalProductStyle_ddl.SelectItem(data);
            return this;

        }

        public SpecSetDetailPage Update_SelectOriginalProductUse(string data)
        {
            UpdateOriProductUse_ddl.SelectItem(data);
            return this;
        }
        public SpecSetDetailPage Update_SelectNewBuildingPhase(string data)
        {
            UpdateNewBuildingPhase_ddl.SelectItem(data, false, false);
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(UpdateNewBuildingPhase_ddl), $"Select {data} on the dropdown list.");
            return this;
        }

        public SpecSetDetailPage Update_SelectNewProduct(string data)
        {
            Textbox updateNewProduct_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_radNewProduct_Input']");
            updateNewProduct_txt.SetText(data);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_radNewProduct_LoadingDiv']");
            Button updateNewProduc_btn = new Button(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl04_radNewProduct_DropDown']//li)[1]");
            updateNewProduc_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            return this;
        }

        public SpecSetDetailPage Update_SelectNewProductStyle(string data)
        {
            UpdateNewProductStyle_ddl.SelectItem(data);
            return this;
        }

        public SpecSetDetailPage Update_SelectNewProductUse(string data)
        {
            UpdateNewProductUse_ddl.SelectItem(data);
            return this;
        }

        public SpecSetDetailPage Update_SelectProductCalculation(string data)
        {
            UpdateProductCalculation_ddl.SelectItem(data);
            return this;
        }

        public void UpdateProduct()
        {
            UpdateProduct_Btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
        }

        public void UpdateProductConversion(SpecSetData specSetData)
        {
            Update_SelectOriginalBuildingPhase(specSetData.OriginalPhase)
           .Update_SelectOriginalProduct(specSetData.OriginalProduct)
           .Update_SelectOriginalProductStyle(specSetData.OriginalProductStyle)
           .Update_SelectOriginalProductUse(specSetData.OriginalProductUse)
           .Update_SelectNewBuildingPhase(specSetData.NewPhase)
           .Update_SelectNewProduct(specSetData.NewProduct)
           .Update_SelectNewProductStyle(specSetData.NewProductStyle)
           .Update_SelectNewProductUse(specSetData.NewProductUse)
           .Update_SelectProductCalculation(specSetData.ProductCalculation)
           .UpdateProduct();
            string expectedMsgUpdatedProduct = "Product Conversion Updated";
            string actualMsgUpdatedProduct = SpecSetDetailPage.Instance.GetLastestToastMessage();
            if (expectedMsgUpdatedProduct.Equals(actualMsgUpdatedProduct))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Product Conversion is updated successfully.</b></font>");
                SpecSetDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Product Conversion is not updateed successfully. Actual message: <i>{actualMsgUpdatedProduct}</i></font>");
            }
        }
        public void ExpandAndCollapseSpecSet(string data, string type)
        {
            Button ExpandSpecSet_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets_ctl00']//span[contains(text(),'{data}')]//ancestor::tr/td/input[@title='{type}']");
            ExpandSpecSet_btn.Click();
        }


        public SpecSetDetailPage ClickAddNewProduct2()
        {
            AddNewProduct_Btn2.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage, 1000);
            return this;
        }


        public string SelectOriginalManufacture2(string data)
        {
            if (OriManufacture_ddl.IsItemInList(data))
            {
                OriManufacture_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                OriManufacture_ddl.SelectItem(0);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(OriManufacture_ddl), $"Select OriginalManufacturer {OriManufacture_ddl.SelectedItemName} on the dropdown list.");
                return OriManufacture_ddl.SelectedItemName;
            }
        }

        public string SelectNewManufacture2(string data)
        {
            if (NewManufacture_ddl.IsItemInList(data))
            {
                NewManufacture_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                NewManufacture_ddl.SelectItem(2);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(NewManufacture_ddl), $"Select NewManufacturer {NewManufacture_ddl.SelectedItemName} on the dropdown list.");
                return NewManufacture_ddl.SelectedItemName;
            }
        }

        public string SelectOriginalStyle2(string data)
        {
            return OriStyle_ddl.SelectItemByValueOrIndex(data, 0);
        }
        public string SelectNewStyle2(string data)
        {
            return NewStyle_ddl.SelectItemByValueOrIndex(data, 1);

        }
        public string SelectOriginalUse2(string data)
        {
            return OriUse_ddl.SelectItemByValueOrIndex(data, 1);
        }
        public string SelectNewUse2(string data)
        {
            return NewUse_ddl.SelectItemByValueOrIndex(data, 2);
        }
        public string SelectStyleCalculation2(string data)
        {
            return StyleCalculation_ddl.SelectItemByValueOrIndex(data, 0);
        }

        // Product
        public string SelectOriginalBuildingPhase2(string data)
        {
            if (OriginalBuildingPhase2_ddl.IsItemInList(data))
            {
                OriginalBuildingPhase2_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                OriginalBuildingPhase2_ddl.SelectItem(0);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(OriginalBuildingPhase2_ddl), $"Select Original BuildingPhase {OriginalBuildingPhase2_ddl.SelectedItemName} on the dropdown list.");
                return OriginalBuildingPhase2_ddl.SelectedItemName;
            }
        }

        public string SelectNewBuildingPhase2(string data)
        {
            if (NewBuildingPhase2_ddl.IsItemInList(data))
            {
                NewBuildingPhase2_ddl.SelectItem(data, true);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                return data;
            }
            else
            {
                NewBuildingPhase2_ddl.SelectItem(1);
                WaitingLoadingGifByXpath(_loadingOnDetailPage);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(NewBuildingPhase_ddl), $"Select New BuildingPhase {NewBuildingPhase_ddl.SelectedItemName} on the dropdown list.");
                return NewBuildingPhase2_ddl.SelectedItemName;
            }
        }

        public string SelectOriginalProductStyle2(string data)
        {
            return OriginalProductStyle2_ddl.SelectItemByValueOrIndex(data, 0);

        }
        public void SelectOriginalProduct2(string data)
        {
            Textbox OriginalProduct_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_radOriginalProduct_Input']");
            OriginalProduct_txt.SetText(data);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_radNewProduct_LoadingDiv']");
            Button OriginalProduc_btn = new Button(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_radOriginalProduct_DropDown']//li)[1]");
            OriginalProduc_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
        }
        public void SelectNewProduct2(string data)
        {
            Textbox NewProduct_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_radNewProduct_Input']");
            NewProduct_txt.SetText(data);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail10_ctl02_ctl04_radNewProduct_LoadingDiv']");
            Button OriginalProduc_btn = new Button(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rgSets_ctl00_ctl07_Detail20_ctl02_ctl04_radNewProduct_DropDown']//li)[1]");
            OriginalProduc_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
        }
        public string SelectOriginalProductUse2(string data)
        {
            return OriProductUse2_ddl.SelectItemByValueOrIndex(data, 0);
        }
        public string SelectNewProductUse2(string data)
        {
            return NewProductUse2_ddl.SelectItemByValueOrIndex(data, 1);
        }

        public string SelectNewProductStyle2(string data)
        {
            return NewProductStyle2_ddl.SelectItemByValueOrIndex(data, 1);
        }

        public string SelectProductCalculation2(string data)
        {
            return ProductCalculation2_ddl.SelectItemByValueOrIndex(data, 1);
        }
        public void PerformInsertProduct2()
        {
            PerformAddNewProduct2_Btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
        }

        public void AddProductConversionWithoutCategoryInSpecSet2(SpecSetData specSetData)
        {
            ClickAddNewProduct2();
            SelectOriginalBuildingPhase2(specSetData.OriginalPhase);
            SelectOriginalProduct2(specSetData.OriginalProduct);
            SelectOriginalProductStyle2(specSetData.OriginalProductStyle);
            SelectOriginalProductUse2(specSetData.OriginalProductUse);
            SelectNewBuildingPhase2(specSetData.NewPhase);
            SelectNewProduct2(specSetData.NewProduct);
            SelectNewProductStyle2(specSetData.NewProductStyle);
            SelectNewProductUse2(specSetData.NewProductUse);
            SelectProductCalculation2(specSetData.ProductCalculation);
            PerformInsertProduct2();
        }

        public void EditItemOnSpecSetGrid(string columnNameToFind, string valueToFind, string valueToUpdate)
        {
            SpecSet_Grid.ClickEditItemInGrid(columnNameToFind, valueToFind);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            string inputXpath = $"//*[@id='ctl00_CPH_Content_rgSets_ctl00']//input[@value='{valueToFind}']";
            Textbox update_txt = new Textbox(FindType.XPath, inputXpath);
            if (update_txt.IsNull())
                ExtentReportsHelper.LogFail($"The field with column name <b>{columnNameToFind}</b> and value <b>{valueToFind}</b> is NOT have the edit textbox in grid</b>.");
            else
            {
                update_txt.SetText(valueToUpdate);
                Button Update_Btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets_ctl00']//input[@value='{valueToFind}']//ancestor::tr//td[5]//input[@title='Update']");
                Update_Btn.Click();
                SpecSet_Grid.WaitGridLoad();
            }
            string expectedMsg = "SpecSet updated";
            string actualMsg = GetLastestToastMessage();
            if (expectedMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"SpecSet was successfully updated.");
            }
            string xpathAfterUpdate = $"//*[@id='ctl00_CPH_Content_rgSets_ctl00']//span[contains(text(),'{valueToUpdate}')]";
            Label editAfterUpdatedata_txt = new Label(FindType.XPath, xpathAfterUpdate);
            if (editAfterUpdatedata_txt.GetText().Equals(valueToUpdate))
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The field with column name <b>{columnNameToFind}</b> and value <b>{valueToFind}</b> is updated successfully in grid</b></font>.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>SpecSet with column name<b>{columnNameToFind}</b> and value <b>{valueToFind}</b> is NOT update in grid</font>.");
            }
            CloseToastMessage();
        }
        public void DeleteItemOnProductConversionsInGrid(string originalproduct)
        {
            Button DeleteItem_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets']//a[contains(text(),'{originalproduct}')]//ancestor::tr/td[7]/input");
            DeleteItem_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            string actualDeleteProductMsg = GetLastestToastMessage();
            string expectedDeleteProductSuccessfulMess = "Spec Set removed!";
            if (expectedDeleteProductSuccessfulMess.Equals(actualDeleteProductMsg))
            {
                ExtentReportsHelper.LogPass($"The Product Conversion removed successfully!");
            }
            else if (!string.IsNullOrEmpty(actualDeleteProductMsg))
            {
                ExtentReportsHelper.LogFail($"The Product Conversion could not be deleted! Actual message: {actualDeleteProductMsg}");
            }
        }
        public void DeleteItemOnStyleConversionsInGrid(string originalstyle)
        {
            Button DeleteItem_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets']//table[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11']//span[contains(text(),'{originalstyle}')]//ancestor::tr/td/input[@title='Delete']");
            DeleteItem_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
        }

        public SpecSetDetailPage EditItemOnStyleConversionsInGridWithoutProductConversion(string originalstyle)
        {
            Button EditItem_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets']//table[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11']//span[contains(text(),'{originalstyle}')]//ancestor::tr[2]//td[6][@class='grideditcolumn']/input");
            EditItem_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            return this;
        }

        public SpecSetDetailPage EditItemOnStyleConversionsInGrid(string originalstyle)
        {
            Button EditItem_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets']//table[@id='ctl00_CPH_Content_rgSets_ctl00_ctl05_Detail11']//span[contains(text(),'{originalstyle}')]//ancestor::tr[1]//td[6][@class='grideditcolumn']/input");
            EditItem_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSets']/div[1]");
            return this;
        }

        public SpecSetDetailPage Update_SelectOriginalManufacture(string data)
        {
            UpdateOriManufactureStyle_ddl.SelectItem(data, false, false);
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(UpdateOriManufactureStyle_ddl), $"Select {data} on the dropdown list.");
            return this;
        }

        public SpecSetDetailPage Update_SelectOriginalStyle(string data)
        {
            UpdateOriStyle_ddl.SelectItem(data);
            return this;
        }
        public SpecSetDetailPage Update_SelectOriginalUse(string data)
        {
            UpdateOriUseStyle_ddl.SelectItem(data);
            return this;
        }

        public SpecSetDetailPage Update_SelectNewManufacture(string data)
        {
            UpdateNewManufactureStyle_ddl.SelectItem(data, false, false);
            WaitingLoadingGifByXpath(_loadingOnDetailPage, 3);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(UpdateNewManufactureStyle_ddl), $"Select {data} on the dropdown list.");
            return this;
        }

        public SpecSetDetailPage Update_SelectNewStyle(string data)
        {
            UpdateNewStyle_ddl.SelectItem(data);
            return this;
        }

        public SpecSetDetailPage Update_SelectNewUse(string data)
        {
            UpdateNewUseStyle_ddl.SelectItem(data);
            return this;
        }

        public SpecSetDetailPage Update_SelectStyleCalculation(string data)
        {
            UpdateStyleCalculation_ddl.SelectItem(data);
            return this;
        }

        public void UpdateStyle()
        {
            UpdateStyle_Btn.Click();
            WaitingLoadingGifByXpath(_loadingOnDetailPage);
        }

        public void UpdateStyleConversion(SpecSetData specSetData)
        {
            Update_SelectOriginalManufacture(specSetData.OriginalManufacture)
                .Update_SelectOriginalStyle(specSetData.OriginalStyle)
                .Update_SelectOriginalUse(specSetData.OriginalUse)
                .Update_SelectNewManufacture(specSetData.NewManufacture)
                .Update_SelectNewStyle(specSetData.NewStyle)
                .Update_SelectNewUse(specSetData.NewUse)
                .Update_SelectStyleCalculation(specSetData.StyleCalculation)
                .UpdateStyle();
        }


        /// <summary>
        /// Import Building Phase to Vendor Building Phase
        /// </summary>
        /// <param name="importTitle"></param>
        /// <param name="importFileDir"></param>
        public string ImportFile(string importTitle, string importFileDir)
        {
            string textboxUpload_Xpath, importButtion_Xpath, message_Xpath;
            switch (importTitle)
            {
                case "Spec Sets Product Import":
                    textboxUpload_Xpath = "//*[@id='ctl00_CPH_Content_fuImportSpecSets']";
                    importButtion_Xpath = "//*[@id='ctl00_CPH_Content_lbImportSpecSets']";
                    message_Xpath = "//*[@id='ctl00_CPH_Content_lblImportSpecSets']";
                    break;

                default:
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>There is no upload grid with title {importTitle}.</font>");
                    return string.Empty;
            }

            // Upload file to corect grid
            Textbox Upload_txt = new Textbox(FindType.XPath, textboxUpload_Xpath);
            Button ProductImport_btn = new Button(FindType.XPath, importButtion_Xpath);

            // Get upload file location
            string fileLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + importFileDir;

            // Upload
            Upload_txt.SendKeysWithoutClear(fileLocation);
            System.Threading.Thread.Sleep(500);
            ProductImport_btn.Click(false);
            PageLoad();

            // Get message
            Label message = new Label(FindType.XPath, message_Xpath);

            // Verify message
            //IWebElement message = FindElementHelper.FindElement(FindType.XPath, message_Xpath);
            string expectedMess = "Import complete.";
            if (message.IsDisplayed() is false || message.GetAttribute("value") == expectedMess)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import message isn't same as the expectation.</font>" +
                    $"<br>The expected message: {expectedMess}");
            }
            else
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Import {importTitle} to current ProductConversions successfuly.</b></font>");
            }
            return message.IsDisplayed() ? message.GetText() : string.Empty;
        }

        /// <summary>
        /// Import/ Export Product Conversion attributes from Utilities menu
        /// </summary>
        /// <param name="item"></param>
        /// <param name="ProductConversionsExportName"></param>
        public void ImportExporFromMoreMenu(string item, string specSetGroup)
        {
            string ProductConversionsExportName = "Pipeline_SpecSetProducts_" + specSetGroup;
            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();
            switch (item)
            {
                case "Export Product Conversions CSV":
                    SelectItemInUtiliestButton(item, true);
                    ExportFile("CSV", ProductConversionsExportName);
                    break;

                case "Export Product Conversions Excel":
                    SelectItemInUtiliestButton(item, true);
                    ExportFile("Excel", ProductConversionsExportName);
                    break;

                case "Import":
                    SelectItemInUtiliestButton(item, true);
                    break;

                default:
                    ExtentReportsHelper.LogInformation("Not found Import/Export items");
                    return;
            }
        }


        /// <summary>
        /// Export Product in SpecSet to CSV/ Excel file
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportAttributeName"></param>
        public void ExportFile(string exportType, string exportAttributeName)
        {
            string extention;
            //Resolve file extension before filesystem check
            if (exportType == "CSV")
                extention = "CSV";
            else
                extention = "XLSX";

            string fileName = $"{exportAttributeName}.{extention.ToLower()}";
            string fullFilePath = CommonHelper.GetFullFilePath("Download\\" + fileName);

            System.Threading.Thread.Sleep(3000);

            // Verify the download file exists on the default saved file location or not
            if (File.Exists(fullFilePath))
            {
                ExtentReportsHelper.LogPass(null, $"The export file <font color='green'><b>'{fileName}'</b></font> file downloaded successfully and existed on the file system.");
                string content = File.ReadAllText(fullFilePath, Encoding.UTF8);
                if (string.IsNullOrEmpty(content) is true)
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Can't read the <b>'{fileName}'</b> file on location: <b>{CommonHelper.GetFullFilePath("Download")}</b>" +
                   $"<br>The export file is empty.</font>");
            }
            else
                ExtentReportsHelper.LogFail(null, $"<font color='red'>Can't find the '{fileName}' file on location: <b>{CommonHelper.GetFullFilePath("Download")}</b>" +
                    $"<br>Failed to export <font color='red'><b>'{fileName}'</b></font> file.");

            // Remove focus from Utilities panel to continue following export steps
            Utilities_btn.Click(false);
        }


        public void ImportInvalidData(string importGridTitlte, string fullFilePath, string expectedFailedData)
        {
            string actualMess = ImportFile(importGridTitlte, fullFilePath);

            if (expectedFailedData.ToLower().Contains(actualMess.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file was failed to import and the toast message indicated failure.</b></font>");

        }
    }
}
