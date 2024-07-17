using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Common.Controls;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Assets.House;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityHouseBOM
{
    public partial class CommunityHouseBOMDetailPage
    {

        public void FilterItemInQuantitiesGrid(string name, GridFilterOperator gridFilterOperator, string valueToFind)
        {
            QuantitiesGrid.FilterByColumn(name, gridFilterOperator, valueToFind);
            WaitingLoadingGifByXpath(loadingIcon_Xpath);
        }

        public bool IsItemInQuantitiesGrid(string column, string valueToFind)
        {
            return QuantitiesGrid.IsItemWithTextContainsOnCurrentPage(column, valueToFind);
        }

        public int GetTotalNumberItem()
        {
            return QuantitiesGrid.GetTotalItems;
        }

        /// <summary>
        /// House BOM Generation
        /// </summary>
        /// <param name="community">Selected community name</param>
        public void GenerateHouseBOM()
        {
            // Click BOM Generation button
            if (BomGeneration_btn.IsDisplayed(false) is true)
            {
                // Click generate BOM button
               BomGeneration_btn.Click();
               if(GenerateAll_btn.IsDisplayed(false) is true)
                {
                    GenerateAll_btn.Click();
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");

                    string actualToastMess = GetLastestToastMessage();
                    string expectedMess = "Successfully processed selected BOM(s).";
                    if (actualToastMess.Contains(expectedMess))
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Generate BOM successfully. Toast message's same as the expectation.</b></font>");
                    else
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>Generate BOM unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                            $"<br>The expectation: {expectedMess}" +
                            $"<br>The actual result: {actualToastMess}");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>'BOM Generation'</b> button.</font>");
                }
            }
        }

        /// <summary>
        /// Verify Item On Community House BOM In Grid
        /// </summary>
        /// <param name="expectedData"></param>
        /// <param name="isVerifyQuantities"></param>
        public void VerifyItemOnHouseBOMGrid(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            // Filter option name
            FilterItemInQuantitiesGrid("House Name", GridFilterOperator.Contains, expectedData.houseName);

            if (IsItemInQuantitiesGrid("House Name", expectedData.houseName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name <b>'{expectedData.optionName}'</b>on the grid view." +
                    "<br>Failed to generate House BOM.</font></br>");
                return;
            }

            // Verify Expand House Name
            Button expandHouse = new Button(FindType.XPath, $"//*[text()='{expectedData.houseName}']/ancestor::tr/td[@class='rgExpandCol']/input");
            if (expandHouse.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand House Name</b> button on grid view." +
                    "<br>Failed to verify House BOM.</font></br>");
                return;
            }
            expandHouse.RefreshWrappedControl();
            expandHouse.Click();

            WaitingLoadingGifByXpath(loadingIcon_Xpath, 6000);

            // Verify Expand Option
            Button expandOption = new Button(FindType.XPath, $"//*[text()='{expectedData.houseName}']/ancestor::tr/td[@class='rgExpandCol']/input//ancestor::tr/./following-sibling::tr//td/span[contains(text(),'{expectedData.optionName}')]//ancestor::tr/./following-sibling::tr//*[contains(@id,'ctl00_CPH_Content_rgSuperOptions_ctl00')]//td/input[contains(@id, 'GECBtnExpandColumn') and contains(@title,'Expand')] | //*[text()='{expectedData.optionName}']/ancestor::tr/td[@class='rgExpandCol']/input");
            if (expandOption.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand Option</b> button on grid view." +
                    "<br>Failed to verify House BOM.</font></br>");
                return;
            }
            expandOption.RefreshWrappedControl();
            expandOption.Click();

            WaitingLoadingGifByXpath(loadingIcon_Xpath, 4000);
            if (expectedData.dependentCondition == string.Empty)
            {

                Button expandCondition = new Button(FindType.XPath, $"(//*[text()='{expectedData.optionName}' and contains(@id,'lblSuperOptionName')]/ancestor::tr/following::tbody/tr[@class='rgRow'])[1]//input[contains(@id, 'GECBtnExpandColumn') and contains(@title,'Expand')]");
                if (expectedData.productToOption.Count == 0)
                {
                    // Expectation: There are no items on the grid view. Verify there is NO EXPAND button next to '(No Condition)'
                    if (expandCondition.IsDisplayed(false) is false)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>There are no items on the grid view. Generate House BOM successfully.</b><font/>");
                    else
                        ExtentReportsHelper.LogFail($"<font color='red'>It should be NO items on the grid view. Failed to generate House BOM.<font/>");
                    return;
                }
                else
                {
                    // Expectation: Should display phase and product quantities after generating House BOM. Verify there is EXPAND button next to '(No Condition)'
                    if (expandCondition.IsDisplayed(false) is false)
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand All Condition</b> button on grid view." +
                            "<br>Failed to verify House BOM.</font></br>");
                        return;
                    }
                    expandCondition.RefreshWrappedControl();
                    expandCondition.Click();
                    WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);
                }


            }
            else
            {
                Button expandConditionWithCondition = new Button(FindType.XPath, $"//*[text()='{expectedData.dependentCondition}' and contains(@id,'lblOptionCondition')]/ancestor::tr/td[@class='rgExpandCol']/input");
                Label expandConditionWithCondition_lbl = new Label(FindType.XPath, $"//*[text()='{expectedData.dependentCondition}' and contains(@id,'lblOptionCondition')]");
                if (expectedData.productToOption.Count == 0)
                {
                    // Expectation: There are no items on the grid view. Verify there is NO EXPAND button next to '(No Condition)'
                    if (expandConditionWithCondition_lbl.IsDisplayed(false) && expandConditionWithCondition_lbl.GetText().Equals(expectedData.optionName) is false)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>There are no items on the grid view.</b><font/>");
                    else
                        ExtentReportsHelper.LogFail($"<font color='red'>It should be NO items on the grid view. Failed to generate House BOM.<font/>");
                    return;
                }
                else
                {
                    // Expectation: Should display phase and product quantities after generating House BOM. Verify there is EXPAND button next to '(No Condition)'
                    if (expandConditionWithCondition_lbl.IsDisplayed(false) && expandConditionWithCondition_lbl.GetText().Equals(expectedData.dependentCondition) is false)
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Condition With Condition Option {expectedData.dependentCondition}</b> button on grid view." +
                            "<br>Failed to verify House BOM.</font></br>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogPass($"<font color='green'> <b>Condition With Condition Option {expectedData.dependentCondition}</b> button on grid view.");
                    }
                    expandConditionWithCondition.RefreshWrappedControl();
                    expandConditionWithCondition.Click();
                    WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);
                }
            }

            foreach (ProductToOptionData item in expectedData.productToOption)
            {
                // Verify Building Phase
                Button expandPhase = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");
                if (expandPhase.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{item.BuildingPhase}'</b> on grid view." +
                        $"<br>Failed to verify House BOM.</font></br>");
                    return;
                }

                if (isVerifyQuantities is true)
                {
                    expandPhase.Click();
                    WaitingLoadingGifByXpath(loadingIcon_Xpath, 4000);
                    // Verify each product quantities on current building phase
                    foreach (ProductData expectedProduct in item.ProductList)
                    {
                        // Verify Product and total quantities
                        Label product = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                        Label quanities = new Label(FindType.XPath, $"//tr[contains(@id, 'ctl00_CPH_Content_rgSuperOptions')]/td[contains(.,'{expectedProduct.Name}')]/following-sibling::td/a[text() = '{expectedProduct.Quantities}']");

                        if (product.IsDisplayed(false) is false || quanities.IsDisplayed(false) is false)
                        {
                            ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                                $"and Quantities <b>'{expectedProduct.Quantities} and product {product} and quanities {quanities}'</b> on grid view." +
                                "<br>Failed to verify House BOM.</font></br> ");
                        }
                        else
                            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                                    $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                    }
                    // Collapse building phase
                    expandPhase.RefreshWrappedControl();
                    expandPhase.Click(false);
                    WaitingLoadingGifByXpath(loadingIcon_Xpath, 4000);
                }
            }
        }

        public void CloseModal()
        {
            Cancel_btn.Click();
        }

        /// <summary>
        /// Verify Item Community House BOM With Turn On Parameter 
        /// </summary>
        /// <param name="expectedData"></param>
        /// <param name="isVerifyQuantities"></param>
        public void VerifyItemOnHouseBOMGridWithParameter(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            // Filter option name
            FilterItemInQuantitiesGrid("House Name", GridFilterOperator.Contains, expectedData.houseName);

            if (IsItemInQuantitiesGrid("House Name", expectedData.houseName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name <b>'{expectedData.optionName}'</b>on the grid view." +
                    "<br>Failed to generate House BOM.</font></br>");
                return;
            }

            // Verify Expand House Name
            Button expandHouse = new Button(FindType.XPath, $"//*[text()='{expectedData.houseName}']/ancestor::tr/td[@class='rgExpandCol']/input");
            if (expandHouse.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand House Name</b> button on grid view." +
                    "<br>Failed to verify House BOM.</font></br>");
                return;
            }
            expandHouse.RefreshWrappedControl();
            expandHouse.Click();
            WaitingLoadingGifByXpath(loadingIcon_Xpath, 6000);

            // Verify Expand Option
            Button expandOption = new Button(FindType.XPath, $"//*[text()='{expectedData.optionName}']/ancestor::tr/td[@class='rgExpandCol']/input");
            if (expandOption.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand Option</b> button on grid view." +
                    "<br>Failed to verify House BOM.</font></br>");
                return;
            }
            expandOption.RefreshWrappedControl();
            expandOption.Click();
            WaitingLoadingGifByXpath(loadingIcon_Xpath, 6000);

            Button expandCondition = new Button(FindType.XPath, $"//*[text()='{expectedData.houseName}']/ancestor::tr/td[@class='rgExpandCol']/input//ancestor::tr/./following-sibling::tr//td/span[contains(text(),'{expectedData.optionName}')]//ancestor::tr/./following-sibling::tr//*[contains(@id,'ctl00_CPH_Content_rgSuperOptions_ctl00')]//td/input[contains(@id, 'GECBtnExpandColumn') and contains(@title,'Expand')]");
            if (expectedData.productToOption.Count == 0)
            {
                // Expectation: There are no items on the grid view. Verify there is NO EXPAND button next to '(No Condition)'
                if (expandCondition.IsDisplayed(false) is false)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>There are no items on the grid view. Generate House BOM successfully.</b><font/>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>It should be NO items on the grid view. Failed to generate House BOM.<font/>");
                return;
            }
            else
            {
                // Expectation: Should display phase and product quantities after generating House BOM. Verify there is EXPAND button next to '(No Condition)'
                if (expandCondition.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand All Condition</b> button on grid view." +
                        "<br>Failed to verify House BOM.</font></br>");
                    return;
                }
                expandCondition.RefreshWrappedControl();
                expandCondition.Click();
                WaitingLoadingGifByXpath(loadingIcon_Xpath, 6000);
            }
            foreach (ProductToOptionData item in expectedData.productToOption)
            {
                // Verify Building Phase
                Button expandPhase = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");
                if (expandPhase.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{item.BuildingPhase}'</b> on grid view." +
                        $"<br>Failed to verify House BOM.</font></br>");
                    return;
                }

                if (isVerifyQuantities is true)
                {
                    expandPhase.Click();
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]",5000);

                    foreach (ProductData expectedParameter in item.ParameterList)
                    {
                        // Verify Parameter 
                        Button expandParameter = new Button(FindType.XPath, $"//td[contains(text(), '{expectedParameter.Parameter}')]/../td[@class='rgExpandCol']");

                        Label parameter_txt = new Label(FindType.XPath, $"//table[@class='rgDetailTable rgClipCells']//td[contains(text(), '{expectedParameter.Parameter}')]");
                        if (parameter_txt.IsDisplayed(true) && expandParameter.IsDisplayed() is true)
                        {
                            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Parameter with name '{expectedParameter.Parameter}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                            expandParameter.Click();
                            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
                            // Verify each product quantities on current building phase
                            foreach (ProductData expectedProduct in item.ProductList)
                            {
                                // Verify Product and total quantities
                                Label product = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                                Label quanities = new Label(FindType.XPath, $"//tr[contains(@id, 'ctl00_CPH_Content_rgSuperOptions') and ./td[contains(.,'{expectedParameter.Parameter}')]]/following-sibling::tr/td[contains(.,'{expectedProduct.Name}')]//following-sibling::td/a[text() = '{expectedProduct.Quantities}']");

                                if (product.IsDisplayed(false) is true || quanities.IsDisplayed(false) is true)
                                {

                                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                                    $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                                }
                                else
                                {
                                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                                    $"and Quantities <b>'{expectedProduct.Quantities}'</b> on grid view." +
                                    "<br>Failed to verify House BOM.</font></br>");
                                }
                            }
                        }
                        else
                        {
                            ExtentReportsHelper.LogFail($"<font color='red'>Can't find Parameter with name <b>'{expectedParameter.Parameter}'</b></font>");
                        }
                        expandParameter.RefreshWrappedControl();
                        expandParameter.Click();
                        WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
                    }
                    expandPhase.RefreshWrappedControl();
                    expandPhase.Click(false);
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
                }

            }

        }

        /// <summary>
        /// Download Base Line Community House BOM
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        /// <param name="HouseType"></param>
        /// <param name="Buildingphase"></param>
        public void DownloadBaseLineHouseBOMFile(string exportType, string exportName, string HouseType, string Buildingphase)
        {
            // Download baseline file to report folder
            bool isCaptured = false;
            // Download file
            try
            {
                switch (HouseType)
                {
                    case "Export Selected Houses":
                        AllHouseName_btn.Click();
                        Utilities_btn.Click();
                        ExportSelectedHouse_btn.Click();
                        if (isCaptured)
                            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ExportSelectedHouse_btn),
                                $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                        WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlModal']/div[1]");
                        break;
                    case "Export All Houses":
                        Utilities_btn.Click();
                        ExportAllHouse_btn.Click();
                        if (isCaptured)
                            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ExportAllHouse_btn),
                                $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                        WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlModal']/div[1]");
                        break;
                    default:
                        ExtentReportsHelper.LogInformation("Not found Import/Export items");
                        break;
                }
              
                Button exportType_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbExport{exportType}']");
                if (Buildingphase == string.Empty)
                {
                    CheckAll_chk.SetCheck(true);
                    exportType_btn.Click();
                }
                else
                {
                    BuildingPhaseExport_txt.SetText(Buildingphase);
                    CheckAll_chk.SetCheck(true);
                    exportType_btn.Click();
                }
            }

            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {exportType} on Utilities menu"));
            }

            System.Threading.Thread.Sleep(5000);
            CloseModal();

            // Verify and move it to baseline folder
            ValidationEngine.DownloadBaseLineFile(exportType, exportName);
        }


        /// <summary>
        /// Export Community House BOM File With House Type
        /// </summary>
        /// <param name="exportType"></param>
        /// <param name="exportName"></param>
        /// <param name="expectedTotalNumber"></param>
        /// <param name="expectedExportTitle"></param>
        /// <param name="HouseType"></param>
        /// <param name="Buildingphase"></param>
        public void ExportHouseBOMFile(string exportType, string exportName, int expectedTotalNumber, string expectedExportTitle, string HouseType, string Buildingphase)
        {
            // Download baseline file to report folder
            bool isCaptured = false;
            // Download file
            try
            {
                Utilities_btn.Click();

                switch (HouseType)
                {
                    case "Export Selected Houses":
                        ExportSelectedHouse_btn.Click();
                        if (isCaptured)
                            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ExportSelectedHouse_btn),
                                $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                        WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlModal']/div[1]");
                        break;
                    case "Export All Houses":
                        ExportAllHouse_btn.Click();
                        if (isCaptured)
                            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ExportAllHouse_btn),
                                $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                        WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlModal']/div[1]");
                        break;
                    default:
                        ExtentReportsHelper.LogInformation("Not found Import/Export items");
                        break;
                }

                Button exportType_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbExport{exportType}']");
                if (Buildingphase == string.Empty)
                {
                    CheckAll_chk.SetCheck(true);
                    exportType_btn.Click();
                }
                else
                {
                    BuildingPhaseExport_txt.SetText(Buildingphase);
                    CheckAll_chk.SetCheck(true);
                    exportType_btn.Click();
                }
            }

            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {exportType} on Utilities menu"));
            }

            System.Threading.Thread.Sleep(5000);
            CloseModal();
            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(exportType, exportName, expectedExportTitle, expectedTotalNumber);
        }

        public void SelectCollection(string ViewType)
        {
            Button CollectionView_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbSubcollection{ViewType}']");
            CollectionView_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]",5000);
        }
    }
}

