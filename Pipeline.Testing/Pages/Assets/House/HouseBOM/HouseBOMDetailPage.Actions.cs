using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Common.Controls;
using Pipeline.Testing.Pages.Estimating.Products;
using System.Collections.Generic;
using System.IO;

namespace Pipeline.Testing.Pages.Assets.House.HouseBOM
{
    public partial class HouseBOMDetailPage
    {
        public void SelectCommunity(string selectedCommunity)
        {
            if (selectedCommunity != string.Empty)
            {
                Community_ddl.SelectItem(selectedCommunity);
                //WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);
                System.Threading.Thread.Sleep(2000);
            }
        }

        public void FilterItemInQuantitiesGrid(string name, GridFilterOperator gridFilterOperator, string valueToFind)
        {
            QuantitiesGrid.FilterByColumn(name, gridFilterOperator, valueToFind);
            WaitingLoadingGifByXpath(loadingIcon_Xpath, 3000);
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
        public void GenerateHouseBOM(string community)
        {
            // Select community
            SelectCommunity(community);

            // Click BOM Generation button
            if (BomGeneration_btn.IsDisplayed(false) is true)
            {
                // Click generate BOM button
                BomGeneration_btn.Click();
                WaitingLoadingGifByXpath(loadingIcon_Xpath);

                string actualToastMess = GetLastestToastMessage();
                string expectedMess = "Successfully processed selected BOM(s).";
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
        public void VerifyOptionOnHouseGrid(HouseQuantitiesData expectedData)
        {
            // Filter option name
            FilterItemInQuantitiesGrid("Option", GridFilterOperator.EqualTo, expectedData.optionName);

            if (IsItemInQuantitiesGrid("Option", expectedData.optionName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name <b>'{expectedData.optionName}'</b>on the grid view." +
                    "<br>Failed to generate House BOM.</font></br>");
                return;
            }

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
            WaitingLoadingGifByXpath(loadingIcon_Xpath);

            if (expectedData.dependentCondition == string.Empty)
            {

                Button expandCondition = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSuperOptions_ctl00']//input[contains(@id, 'GECBtnExpandColumn') and contains(@title,'Expand')]");
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
                Button expandConditionWithCondition = new Button(FindType.XPath, $"//*[text()='{expectedData.dependentCondition}'and contains(@id,'lblOptionCondition')]/ancestor::tr/td[@class='rgExpandCol']/input");
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
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand All Condition With Condition Option {expectedData.dependentCondition}</b> button on grid view." +
                            "<br>Failed to verify House BOM.</font></br>");
                        return;
                    }
                    expandConditionWithCondition.RefreshWrappedControl();
                    expandConditionWithCondition.Click();
                    WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);
                }
            }
        }
        public void VerifyItemWithStyleOnHouseBOMGrid(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            //Verify Option HouseBOM Grid
            VerifyOptionOnHouseGrid(expectedData);

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

                // Verify Phase Bid
                Label phaseBid = new Label(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]" +
                    $"/following-sibling::td/*[contains(@id, 'OpenSupplementalModal')]");
                if (phaseBid.IsExisted() is true && phaseBid.GetText() == item.PhaseBid)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Phase Bid column displays with correct value '{item.PhaseBid}' on grid view.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Phase Bid column SHOULD display with value <b>'{item.PhaseBid}'</b> on grid view." +
                        $"<br>Failed to display Phase Bid.</font></br>");

                if (isVerifyQuantities is true)
                {
                    expandPhase.Click();
                    System.Threading.Thread.Sleep(5000);
                    // Verify each product quantities on current building phase
                    foreach (ProductData expectedProduct in item.ProductList)
                    {
                        // Verify Product and total quantities
                        Label product = new Label(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]//ancestor::tr[contains(@id,'ctl00_CPH_Content_rgSuperOptions')]/following::tr//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                        Label style = new Label(FindType.XPath, $"//tr[contains(@id, 'ctl00_CPH_Content_rgSuperOptions')]/td[contains(.,'{expectedProduct.Name}')]/following-sibling::td/a[text() = '{expectedProduct.Style}']");
                        Label quanities = new Label(FindType.XPath, $"//tr[contains(@id, 'ctl00_CPH_Content_rgSuperOptions')]/td[contains(.,'{expectedProduct.Name}')]/following-sibling::td/a[text() = '{expectedProduct.Quantities}']");

                        if (product.IsDisplayed(false) is false || style.IsDisplayed(false) || quanities.IsDisplayed(false)  is false)
                        {
                            ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                                $"and Style <b>'{expectedProduct.Style} and product {product} and Style {style}'</b> on grid view." +
                                $"and Quantities <b>'{expectedProduct.Quantities} and product {product} and quanities {quanities}'</b> on grid view." +
                                "<br>Failed to verify House BOM.</font></br> ");
                        }
                        else
                            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                                    $"and Style <b>'{expectedProduct.Style} and product {product} and Style {style}'</b>" +
                                    $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                    }

                    // Collapse building phase
                    expandPhase.Click(false);
                }
            }
        }

        public void VerifyItemOnHouseBOMGrid(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            //Verify Option HouseBOM Grid
            VerifyOptionOnHouseGrid(expectedData);

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

                // Verify Phase Bid
                Label phaseBid = new Label(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]" +
                    $"/following-sibling::td/*[contains(@id, 'OpenSupplementalModal')]");
                if (phaseBid.IsExisted() is true && phaseBid.GetText() == item.PhaseBid)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Phase Bid column displays with correct value '{item.PhaseBid}' on grid view.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Phase Bid column SHOULD display with value <b>'{item.PhaseBid}'</b> on grid view." +
                        $"<br>Failed to display Phase Bid.</font></br>");

                if (isVerifyQuantities is true)
                {
                    expandPhase.Click();
                    WaitingLoadingGifByXpath(loadingIcon_Xpath);
                    // Verify each product quantities on current building phase
                    foreach (ProductData expectedProduct in item.ProductList)
                    {
                        // Verify Product and total quantities
                        Label product = new Label(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]//ancestor::tr[contains(@id,'ctl00_CPH_Content_rgSuperOptions')]/following::tr//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                        //Label quanities = new Label(FindType.XPath, $"//tr[contains(@id, 'ctl00_CPH_Content_rgSuperOptions')]/td[10]/a[text() = '{expectedProduct.Quantities}']");
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
                    expandPhase.RefreshWrappedControl();
                    // Collapse building phase
                    expandPhase.Click(false);
                    WaitingLoadingGifByXpath(loadingIcon_Xpath);
                }
            }
        }

        public void VerifySupplementalByPhase(HouseQuantitiesData expectedData)
        {
            foreach (ProductToOptionData item in expectedData.productToOption)
            {
                // Get Phase Bid by option and phase
                Label phaseBid = new Label(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]" +
                    $"/following-sibling::td/a[contains(@id, 'lbOpenSupplementalModal') and text() = '{item.PhaseBid}']");
                if (phaseBid.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Phase Bid value <b>'{item.PhaseBid}'</b> of phase '{item.BuildingPhase}' on grid view.</font>");
                    return;
                }
                phaseBid.JavaScriptClick();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]", 1000);


                // Verify the Supplemental Products modal
                Label supplemental_lbl = new Label(FindType.XPath, "//h1[text()='Supplemental Products']");
                if (supplemental_lbl.IsDisplayed(false) is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Supplemental Products modal displays correctly.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't open Supplemental Products modal or the title isn't correct.</font>");


                // Verify the supplemental products - hash code: on product list has only 1 product
                IWebElement productName = FindElementHelper.FindElement(FindType.XPath, $"//tr[contains(@id,'Content_rgSupplementalProducts')]/td/a[normalize-space() = '{item.ProductList[0].Name}']");
                if (productName != null && productName.Displayed is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{item.ProductList[0].Name}' displays correctly on Supplemental Products modal.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name '{item.ProductList[0].Name}' on Supplemental Products modal.</font>");


                // Verify the Style
                IWebElement style = FindElementHelper.FindElement(FindType.XPath, $"//tr[contains(@id, 'Content_rgSupplementalProducts')]/td[position() = 4 and normalize-space() = '{item.ProductList[0].Style}']");
                if (style != null && style.Displayed is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Style with name '{item.ProductList[0].Style}' displays correctly on Supplemental Products modal.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Style with name '{item.ProductList[0].Style}' on Supplemental Products modal.</font>");


                // Verify the Total Quantity
                IWebElement totalQuantity = FindElementHelper.FindElement(FindType.XPath, $"//tr[contains(@id, 'Content_rgSupplementalProducts')]/td[position() = 9 and normalize-space() = '{item.ProductList[0].Quantities}']");
                if (totalQuantity != null && totalQuantity.Displayed is true)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Total quantity with value '{item.ProductList[0].Quantities}' displays correctly on Supplemental Products modal.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Total quantity with value '{item.ProductList[0].Quantities}' on Supplemental Products modal.</font>");


                // Click Close button
                Button closeBtn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbCloseModal']");
                if (closeBtn.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Close button on Supplemental Products modal.</font>");
                    return;
                }
                closeBtn.Click();
            }
        }
        public void SelectAdvanceCommunity(string selectedCommunity)
        {
            if (selectedCommunity != string.Empty)
            {
                AdvanceCommunity_ddl.SelectItem(selectedCommunity);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rcOptions']/div[1]");
            }
        }
        public void GenerateAdvancedHouseBOM()
        {

            // Click BOM Generation button
            if (AdvanceBomGeneration_btn.IsDisplayed() is true)
            {
                // Click generate BOM button
                AdvanceBomGeneration_btn.Click(true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbCalculateAdvance']/div[1]");

                string actualToastMess = GetLastestToastMessage();
                string expectedMess = "Successfully processed selected BOM(s).";
                if (actualToastMess.Contains(expectedMess))
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Generate BOM successfully. Toast message's same as the expectation.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='green'>Generate BOM unsuccessfully. Toast message's NOT same as the expectation.</font>" +
                        $"<br>The expectation: {expectedMess}" +
                        $"<br>The actual result: {actualToastMess}");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>'BOM Generation'</b> button.</font>");
            }
        }
        public void CloseModal()
        {
            Cancel_btn.Click();
        }
        public void CloseAdvanceModal()
        {
            AdvanceCancel_btn.Click();
        }

        public void SelectCollection(string ViewType)
        {
            Button CollectionView_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbSubcollection{ViewType}']");
            CollectionView_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
        }

        public void VerifyItemOnHouseBOMGridWithParameter(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            // Filter option name
            FilterItemInQuantitiesGrid("Option", GridFilterOperator.EqualTo, expectedData.optionName);

            if (IsItemInQuantitiesGrid("Option", expectedData.optionName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name <b>'{expectedData.optionName}'</b>on the grid view." +
                    "<br>Failed to generate House BOM.</font></br>");
                return;
            }

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
            WaitingLoadingGifByXpath(loadingIcon_Xpath);

            Button expandCondition = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSuperOptions_ctl00']//input[contains(@id, 'GECBtnExpandColumn') and contains(@title,'Expand')]");
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
                WaitingLoadingGifByXpath(loadingIcon_Xpath, 1000);
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

                // Verify Phase Bid
                Label phaseBid = new Label(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]" +
                    $"/following-sibling::td/*[contains(@id, 'OpenSupplementalModal')]");
                if (phaseBid.IsExisted() is true && phaseBid.GetText() == item.PhaseBid)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Phase Bid column displays with correct value '{item.PhaseBid}' on grid view.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Phase Bid column SHOULD display with value <b>'{item.PhaseBid}'</b> on grid view." +
                        $"<br>Failed to display Phase Bid.</font></br>");

                if (isVerifyQuantities is true)
                {
                    foreach (ProductData expectedParameter in item.ParameterList)
                    {
                        expandPhase.Click();
                        WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
                        // Verify Parameter 
                        Button expandSWG = new Button(FindType.XPath, $"//td[contains(text(), '{expectedParameter.Parameter}')]/../td[@class='rgExpandCol']/input");

                        Label parameter_txt = new Label(FindType.XPath, $"//table[@class='rgDetailTable rgClipCells']//td[contains(text(), '{expectedParameter.Parameter}')]");
                        if (parameter_txt.IsDisplayed(true) && expandSWG.IsDisplayed() is true)
                        {

                            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Parameter with name '{expectedParameter.Parameter}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                            expandSWG.Click();
                            WaitingLoadingGifByXpath(loadingIcon_Xpath);

                            // Verify each product quantities on current building phase
                            foreach (ProductData expectedProduct in item.ProductList)
                            {
                                // Verify Product and total quantities
                                Label product = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                                Label quanities = new Label(FindType.XPath, $"//tr[contains(@id, 'ctl00_CPH_Content_rgSuperOptions')]/td[10]/*[text() = '{expectedProduct.Quantities}']");

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
                        //collapse expandSWG
                        expandSWG.RefreshWrappedControl();
                        expandSWG.Click();
                        WaitingLoadingGifByXpath(loadingIcon_Xpath);
                    }
                }
                expandPhase.RefreshWrappedControl();
                expandPhase.Click(false);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
            }

        }
        public void DownloadBaseLineHouseBOMFile(string exportType, string exportName, string Buildingphase)
        {
            // Download baseline file to report folder
            bool isCaptured = false;
            // Download file
            try
            {
                Button moreItem = new Button(FindType.XPath,"//*[@id='ctl00_CPH_Content_hypUtils']");
                moreItem.Click();
                Export_btn.WaitForElementIsVisible(10);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Export_btn),
                        $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                Export_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlModal']/div[1]");
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

                System.Threading.Thread.Sleep(5000);
                CloseModal();

                if (exportType.ToLower().Contains("xml"))
                {
                    System.Threading.Thread.Sleep(5000);
                    CloseModal();
                    return;
                }

            }

            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {exportType} on Utilities menu"));
            }

            // Verify and move it to baseline folder
            ValidationEngine.DownloadBaseLineFile(exportType, exportName);
        }

        public void DownloadBaseLineAdvanceHouseBOMFile(string exportType, string exportName, string Buildingphase)
        {
            // Download baseline file to report folder
            bool isCaptured = false;
            // Download file
            try
            {
                Utils_btn.Click();
                ExportView_btn.WaitForElementIsVisible(10);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ExportView_btn),
                        $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                ExportView_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnModalAdvance']/div[1]");
                Button exportType_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbExport{exportType}Advance']");
                if (Buildingphase == string.Empty)
                {
                    AdvanceCheckAll_chk.SetCheck(true);
                    exportType_btn.Click();
                }
                else
                {
                    AdvanceBuildingPhaseExport_txt.SetText(Buildingphase);
                    AdvanceCheckAll_chk.SetCheck(true);
                    exportType_btn.Click();
                }

                System.Threading.Thread.Sleep(5000);
                CloseAdvanceModal();

                if (exportType.ToLower().Contains("xml"))
                {
                    System.Threading.Thread.Sleep(5000);
                    CloseAdvanceModal();
                    return;
                }
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {exportType} on Utilities menu"));
            }

            // Verify and move it to baseline folder
            ValidationEngine.DownloadBaseLineFile(exportType, exportName);
        }

        public void ExportHouseBOMFile(string exportType, string exportName, int expectedTotalNumber, string expectedExportTitle, string Buildingphase)
        {
            bool isCaptured = false;
            // Download file
            try
            {
                Button moreItem = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypUtils']");
                moreItem.Click();
                Export_btn.WaitForElementIsVisible(10);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Export_btn),
                        $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                Export_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlModal']/div[1]");
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
                System.Threading.Thread.Sleep(5000);
                CloseModal();

                // Don't verify total number and header if that's xml file
                if (exportType.ToLower().Contains("xml"))
                {
                    System.Threading.Thread.Sleep(5000);
                    CloseModal();
                    return;
                }

            }

            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {exportType} on Utilities menu"));
            }

            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(exportType, exportName, expectedExportTitle, expectedTotalNumber);
        }

        public void ExportAdvanceHouseBOMFile(string exportType, string exportName, int expectedTotalNumber, string expectedExportTitle, string Buildingphase)
        {
            bool isCaptured = false;
            // Download file
            try
            {
                Utils_btn.Click();
                ExportView_btn.WaitForElementIsVisible(10);
                if (isCaptured)
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ExportView_btn),
                        $"Click <font color='green'><b><i>{exportType:g}</i></b></font> button.");
                ExportView_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnModalAdvance']/div[1]");
                Button exportType_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbExport{exportType}Advance']");
                if (Buildingphase == string.Empty)
                {
                    AdvanceCheckAll_chk.SetCheck(true);
                    exportType_btn.Click();
                }
                else
                {
                    AdvanceBuildingPhaseExport_txt.SetText(Buildingphase);
                    AdvanceCheckAll_chk.SetCheck(true);
                    exportType_btn.Click();
                }

                System.Threading.Thread.Sleep(5000);
                CloseAdvanceModal();

                if (exportType.ToLower().Contains("xml"))
                {
                    System.Threading.Thread.Sleep(5000);
                    CloseAdvanceModal();
                    return;
                }
            }
            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {exportType} on Utilities menu"));
            }

            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(exportType, exportName, expectedExportTitle, expectedTotalNumber);
        }

        public void ClickOnAdvancedHouseBOMView()
        {
            AdvancedHouseBOMView_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");

        }

        public void ClickOnBasicHouseBOMView()
        {
            BasicHouseBOMView_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
        }

        public void SelectOptions(IList<string> optionsName)
        {
            //Expand Option
            AdvanceOptions_btn.JavaScriptClick();
            foreach (string item in optionsName)
            {
                CheckBox options_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcOptions_DropDown']//li//label[contains(text(),'{item}')]/input");
                options_chk.SetCheck(true);
            }

            LoadSelectedProduct_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgAdvanceHouseBOMView']/div[1]");
        }

        public void CheckAllOptions()
        {
            //Expand Option
            AdvanceOptions_btn.JavaScriptClick();

            if (!CheckAllOption_btn.IsDisplayed())
            {
                AdvanceOptions_btn.JavaScriptClick();
                CheckAllOption_btn.WaitForElementIsVisible(5);
                CheckAllOption_btn.JavaScriptClick();
            }
            CheckAllOption_btn.JavaScriptClick();

        }

        public void LoadHouseAdvanceQuantities()
        {
            LoadSelectedProduct_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgAdvanceHouseBOMView']/div[1]");
        }

        public void ViewBOMtrace(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            // Filter option name
            FilterItemInQuantitiesGrid("Option", GridFilterOperator.Contains, expectedData.optionName);

            if (IsItemInQuantitiesGrid("Option", expectedData.optionName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name <b>'{expectedData.optionName}'</b>on the grid view." +
                    "<br>Failed to generate House BOM.</font></br>");
                return;
            }

            // Verify Expand Option
            Button expandOption = new Button(FindType.XPath, $"//*[text()='{expectedData.optionName}']/ancestor::tr/td[@class='rgExpandCol']/input");
            expandOption.RefreshWrappedControl();
            expandOption.Click();
            WaitingLoadingGifByXpath(loadingIcon_Xpath);
            Button expandCondition = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSuperOptions_ctl00']//input[contains(@id, 'GECBtnExpandColumn') and contains(@title,'Expand')]");
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
                expandCondition.RefreshWrappedControl();
                expandCondition.Click();
                WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);
            }
            foreach (ProductToOptionData item in expectedData.productToOption)
            {
                // Verify Building Phase
                Button expandPhase = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");

                if (isVerifyQuantities is true)
                {
                    expandPhase.Click();
                    WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);
                    if (Expand_SWG.IsDisplayed() is true)
                    {
                        Expand_SWG.Click();
                        WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);
                    }

                    // Verify each product quantities on current building phase
                    foreach (ProductData expectedProduct in item.ProductList)
                    {                       
                        if (expectedProduct.Use == string.Empty || expectedProduct.Use == "NONE")
                        {
                            // Expectation: There are no items on the grid view. Verify there is NO EXPAND button next to '(No Condition)'
                            if (expandCondition.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>There are no items on the grid view. Generate House BOM successfully.</b><font/>");
                            }                                
                            else
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>It should be NO items on the grid view. Failed to generate House BOM.<font/>");
                            }                                

                            Button quanities = new Button(FindType.XPath, $"//tr[contains(@id, 'ctl00_CPH_Content_rgSuperOptions')]/td[10]/*[text() = '{expectedProduct.Quantities}']");
                            //Click On Quantities to BOM Trace Page
                            quanities.Click();
                            PageLoad();
                            CommonHelper.SwitchLastestTab();
                            //Out BOM Products in BOM Trace
                            IWebElement moreItem = driver.FindElement(By.XPath("//*[@id='ctl00_CPH_Content_rorgSubIcon']"));
                            Label productBOMTrace = new Label(FindType.XPath, $"(//*[@class='sidebartotal']//p[@class='directproductlink']//a[contains(text(),'{expectedProduct.Name}')])[1]");
                            Button Total_quanitiesBOMTrace = new Button(FindType.XPath, $"//*[@class='sidebartotal']//p[@class='qty' and contains(text(),'{expectedProduct.Quantities}')]");
                            if (productBOMTrace.IsDisplayed(false) is false || Total_quanitiesBOMTrace.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                                    $"and Quantities  <b>'{expectedProduct.Quantities}'</b> in BOM Trace." +
                                    "<br>Failed to verify House BOM.</font></br>");
                            }
                            ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(moreItem), $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                                        $"and Quantities '{expectedProduct.Quantities}' display correctly Out BOM Products Of BOM Trace view.</b>");
                            CommonHelper.SwitchTab(0);

                        }
                        else
                        {
                            //Write actions on this use
                            Button expandProductItem = new Button(FindType.XPath, $"//a[contains(.,'{expectedProduct.Name}')]/parent::td/preceding-sibling::td/input");
                            expandProductItem.RefreshWrappedControl();
                            expandProductItem.Click();
                            WaitingLoadingGifByXpath(loadingIcon_Xpath, 2000);                            
                            Button TotalQtyUseBOMTrace = new Button(FindType.XPath, $"//td[contains(.,'{expectedProduct.Use}')]/following-sibling::td[a[contains(.,'{expectedProduct.Quantities}')]]");
                            Label UseName = new Label(FindType.XPath, $"//th[contains(.,'Use')]/ancestor::thead[1]/following-sibling::tbody/tr/td[contains(.,'{expectedProduct.Use}')]");
                            if (UseName.IsDisplayed(false) is false || TotalQtyUseBOMTrace.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find the USE with name <b>'{expectedProduct.Use}'</b> " +
                                    $"and Quantities  <b>'{expectedProduct.Quantities}'</b> in BOM Trace." +
                                    "<br>Failed to verify House BOM.</font></br>");
                            }
                            else
                            {
                                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(UseName), $"<font color='green'><b>Use is with name '{expectedProduct.Use}' " +
                                        $"and Quantities '{expectedProduct.Quantities}' display correctly Out BOM Products Of BOM Trace view.</b>");
                            }
                            TotalQtyUseBOMTrace.Click();
                            PageLoad();
                            //CommonHelper.SwitchLastestTab();
                        }
                    }
                }
            }
        }

        public void ViewModalFilteredAndVerifyFilterToInExport(string ViewType, string Community)
        {
            SelectCollection(ViewType);
            IWebElement moreItem = driver.FindElement(By.XPath("//*[@id='ctl00_CPH_Content_hypUtils']"));
            moreItem.Click();
            Export_btn.WaitForElementIsVisible(10);
            Export_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlModal']/div[1]");
            Label CommunityExport_lbl = new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblExportHouseBomTitle']");
            if (CommunityExport_lbl.IsDisplayed() && CommunityExport_lbl.GetText().Equals(Community))
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Community with name: {CommunityExport_lbl.GetText()} in Export Modal is displayed correctly.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"<font color='red'>The Community with name: {CommunityExport_lbl.GetText()} in Export Modal is displayed incorrectly.</font>");
            }

            Button FilteredIcon_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbExportSubcollection{char.ToUpper(ViewType[0]) + ViewType.Substring(1)}']");
            FilteredIcon_btn.WaitForElementIsVisible(5);
            switch (ViewType)
            {
                case "ALL":

                    if (FilteredIcon_btn.IsDisplayed() is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Export filtered by All is displayed correctly.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>The Export filtered by All is displayed incorrectly.</font>");
                    }
                    break;
                case "BASEAndElevations":
                    if (FilteredIcon_btn.IsDisplayed() is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Export filtered by Base + Elevations is displayed correctly.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>The Export filtered by Base + Elevations is displayed incorrectly.</font>");
                    }
                    break;
                case "ShowHouseOnly":
                    if (FilteredIcon_btn.IsDisplayed() is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Export filtered by House Icon is displayed correctly.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>The Export filtered by House Icon is displayed incorrectly.</font>");
                    }
                    break;
                case "ShowGlobalOnly":
                    if (FilteredIcon_btn.IsDisplayed() is true)
                    {
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Export filtered by Global Icon is displayed correctly.</b></font>");
                    }
                    else
                    {
                        ExtentReportsHelper.LogFail(null, $"<font color='red'>The Export filtered by Global Icon is displayed incorrectly.</font>");
                    }
                    break;
                default:
                    ExtentReportsHelper.LogInformation(null, $"<font><b>The Export filtered by All is displayed correctly.</b></font>");
                    break;
            }

            CloseModal();
        }

        public int GetTotalNumberHouseBOMDetailItem()
        {
            return HouseBOMDetailPage_Grid.GetTotalItems;
        }

        public void FilterItemInAdvanceQuantitiesGrid(string column, string valueToFind)
        {
            Textbox Option_txt = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgAdvanceHouseBOMView_ctl00_ctl02_ctl02_FilterTextBox_{column}']");
            Option_txt.SetText(valueToFind);
            Button Filter_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgAdvanceHouseBOMView_ctl00_ctl02_ctl02_Filter_{column}']");
            Button FilterType_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgAdvanceHouseBOMView_rfltMenu_detached']//span[contains(text(),'Contains')]");
            Filter_btn.Click();
            FilterType_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAdvance']/div[1]");
        }

        public bool IsItemInAdvanceQuantitiesGrid(string column, string valueToFind)
        {
            return AdvanceQuantities_Grid.IsItemWithTextContainsOnCurrentPage(column, valueToFind);
        }
        public int GetTotalNumberAdvanceHouseBOMItem()
        {
            return AdvanceQuantities_Grid.GetTotalItems;
        }
        public void ViewAdvancedBOM(string Product, string Quantities)
        {
            if (IsItemInAdvanceQuantitiesGrid("Total Qty", Quantities) is true)
            {
                AdvanceQuantities_Grid.ClickItemInGrid("Total Qty", Quantities);
                PageLoad();
                CommonHelper.SwitchLastestTab();
                //Out BOM Products in BOM Trace
                IWebElement moreItem = driver.FindElement(By.XPath("//*[@id='ctl00_CPH_Content_rorgSubIcon']"));
                Label productBOMTrace = new Label(FindType.XPath, $"(//*[@class='sidebartotal']//p[@class='directproductlink']//a[contains(text(),'{Product}')])[1]");
                Button Total_quanitiesBOMTrace = new Button(FindType.XPath, $"//*[@class='sidebartotal']//p[@class='qty' and contains(text(),'{Quantities}')]");
                if (productBOMTrace.IsDisplayed(false) is false || Total_quanitiesBOMTrace.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{Product}'</b> " +
                        $"and Quantities  <b>'{Quantities}'</b> in BOM Trace." +
                        "<br>Failed to verify House BOM.</font></br>");
                }
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(moreItem), $"<font color='green'><b>Product with name '{Product}' " +
                            $"and Quantities '{Quantities}' display correctly Out BOM Products Of BOM Trace view.</b>");
                CommonHelper.SwitchTab(0);
            }
        }
    }
}

