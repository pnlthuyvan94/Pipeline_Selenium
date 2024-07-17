using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Estimating.Products;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Jobs.Job.Quantities
{
    public partial class JobQuantitiesPage
    {
        private const string XML_TYPE = "XML";
        private const string CAN_NOT_OPEN_EXPORT_JOB_QUANTITIES = "Can't open Export Job Quantities modal";
        private const string JOB_QUANTITIES_NO_PRODUCT = "Job Quantities displayed with no product";
        private const int DOWNLOAD_WAITING_TIME = 5000;
        private const int EXPORT_QUANTITIES_WAITING_TIME = 10;
        public const string ExportJobQuantityLabelBySource = "Export Job Quantities By Source in CSV or Excel";
        public const string ExportJobQuantityLabelBySourceXML = "Export Job Quantities By Source in XML";
        public const string NoProductQuantityLabelXpath = "//td[text() = 'No non-zero quantities were found for the selected Option.']";
        public const string ExpandAllPhaseBtnXpath = "//th[text()='Phases']/preceding-sibling::th/input";

        public bool IsExportJobQuantitiesLabelMatching(string expectedMessage)
        {
            Label exportJobQuantitiesLbl = new Label(FindType.XPath, $"//*[@class='card-header']" +
                $"/h1[contains(text(),'{ExportJobQuantityLabelBySource}')]");
            return exportJobQuantitiesLbl.GetText().Equals(expectedMessage);
        }

        public bool IsExportJobXMLQuantitiesLabelMatching(string expectedMessage)
        {
            Label exportJobQuantitiesLbl = new Label(FindType.XPath, $"//*[@class='card-header']" +
                $"/h1[contains(text(),'{ExportJobQuantityLabelBySourceXML}')]");
            return exportJobQuantitiesLbl.GetText().Equals(expectedMessage);
        }

        public static bool VerifyElementLabelDisplay(Label elementLbl)
        {
            return elementLbl.IsDisplayed(false);
        }

        /// <summary>
        /// Apply system quantities from House / Quantities to Job page
        /// </summary>
        /// <param name="selectedSources">Selected source to apply</param>
        public void ApplySystemQuantities(params string[] selectedSources)
        {
            if (ApplyQuantities_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color = red>Can't find 'Apply System Quantities' button on Job Quantities page.</font>");
                return;
            }
            ApplyQuantities_btn.Click(false);

            // Open apply quantities modal
            Label title = new Label(FindType.XPath, "//h1[text()='Select Sources to Apply Quantities']");
            if (title.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color = red>Can't open 'Select Sources to Apply Quantities' modal to apply system quantities.</font>");
                return;
            }

            CheckBox selectedSource;
            foreach (var item in selectedSources)
            {
                selectedSource = new CheckBox(FindType.XPath, $"//*[contains(@id, 'ctl00_CPH_Content_rlbBIMSources')]/div/ul/li/label/span[text()='{item}']/../input");
                if (selectedSource.IsNull())
                {
                    ExtentReportsHelper.LogFail($"The Source with name <font color='green'><b>{item}</b></font> is not displayed on 'Select Sources to Apply Quantities' modal.");
                }
                else
                {
                    selectedSource.Check();
                }
            }

            // Click apply button
            Button apply = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertSources']");
            if (apply.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color = red>Can't find 'Apply' button on the 'Select Source to Apply Quantities' modal.</font>");
                return;
            }
            apply.Click();

            // Wait loading
            CommonHelper.WaitUntilElementInvisible("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbOpenReconciledModal']/div[1]", 10, false);

            // Get current toast message and verify it
            string actualToastMess = GetLastestToastMessage();
            string expectedToastMess = "Successfully applied system quantities for the selected source(s).";

            if (actualToastMess.Equals(expectedToastMess))
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Apply Quantities successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Failed to Apply Quantities. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return QuantitiesPage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteQuantities(params string[] selectedSources)
        {
            if (DeleteQuantities_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color = red>Can't find 'Delete Quantities' button on Job Quantities page.</font>");
                return;
            }
            DeleteQuantities_btn.Click(false);

            // Open apply quantities modal
            Label title = new Label(FindType.XPath, "//h1[text()='Select Sources to Delete Quantities']");
            if (title.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color = red>Can't open 'Select Sources to Apply Quantities' modal to apply system quantities.</font>");
                return;
            }

            CheckBox selectedSource;
            foreach (var item in selectedSources)
            {
                selectedSource = new CheckBox(FindType.XPath, $"//*[contains(@id, 'ctl00_CPH_Content_rlbSourcesForDelete')]/div/ul/li/label/span[text()='{item}']/../input");
                if (selectedSource.IsNull())
                {
                    ExtentReportsHelper.LogFail($"The Source with name <font color='green'><b>{item}</b></font> is not displayed on 'Select Sources to Apply Quantities' modal.");
                }
                else
                {
                    selectedSource.Check();
                }
            }

            // Click apply button
            Button apply = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbDeleteAllConfigQuantities']");
            if (apply.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail(null, $"<font color = red>Can't find 'Apply' button on the 'Select Source to Delete Quantities' modal.</font>");
                return;
            }
            apply.Click();
            ConfirmDialog(ConfirmType.OK);

            // Wait loading
            CommonHelper.WaitUntilElementInvisible("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgOptionConfigurationQuantities']/div[1]", 10, false);

            // Get current toast message and verify it
            string actualToastMess = GetLastestToastMessage();
            string expectedToastMess = "Successfully deleted the quantities.";

            if (actualToastMess.Equals(expectedToastMess))
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Delete Quantities successfully.</b></font>");
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Failed to Delete Quantities. The toast message isn't same as the expectation." +
                    $"<br>The expected: {expectedToastMess}" +
                    $"<br>The actual: {actualToastMess}</font>");
            }
        }

        public bool IsAddQuantitiesDisplayed()
        {
            AddProductQuantities_lbl.WaitForElementIsVisible(5);
            return AddProductQuantities_lbl != null && AddProductQuantities_lbl.IsDisplayed() is true;
        }
        public void FillterItemsInProductQuantitiesGrid(string columnName, string value)
        {
            ProductQuantitiesPage_Grid.FilterByColumn(columnName, GridFilterOperator.Contains, value);
            WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");
        }

        /// <summary>
        /// Add Product Quantities into Quantities Grid
        /// </summary>
        /// <param name="JobQuantitiesData"></param>
        public void AddQuantitiesInGrid(JobQuantitiesData JobQuantitiesData)
        {
            AddNewQuantity_btn.Click();
            if (IsAddQuantitiesDisplayed() is true)
            {
                int Count = 0;
                //Switch To IFrame
                string Iframe = "RadWindow1";
                CommonHelper.SwitchFrame(Iframe);
                //Config Option, Source , Building Phase into Product
                Option_ddl.SelectItem(JobQuantitiesData.Option);
                Source_ddl.SelectItem(JobQuantitiesData.Source);
                OpenBuildingPhase_btn.Click();
                foreach (string item in JobQuantitiesData.BuildingPhase)
                {
                    CheckBox BuildingPhase_chk = new CheckBox(FindType.XPath, $"//*[@id='rcbPhases_DropDown']//ul[@class='rcbList']//li//label[contains(text(),'{item}')]/input");
                    BuildingPhase_chk.Check(true);
                }
                LoadProducts_btn.Click();
                System.Threading.Thread.Sleep(5000);

                //Add Product Quantities And Quantity into Building Phase
                foreach (string product in JobQuantitiesData.Products)
                {
                    ProductQuantitiesPage_Grid.FilterByColumn("Product Name", GridFilterOperator.Contains, product);
                    WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");
                    CheckBox Product_chk = new CheckBox(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]//ancestor::tr//input[@type='checkbox']");
                    Textbox Quantity_txt = new Textbox(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]//ancestor::tr//input[@type='text']");
                    ProductQuantitiesPage_Grid.FilterByColumn("Style", GridFilterOperator.Contains, JobQuantitiesData.Style);
                    WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");
                    Quantity_txt.SetText(JobQuantitiesData.Quantity[Count]);
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
                CloseModalBtn.Click();
            }

        }

        /// <summary>
        /// Add Product Quantities into Quantities Grid
        /// </summary>
        /// <param name="JobQuantitiesData"></param>
        public List<string> AddQuantitiesWithUseInGrid(JobQuantitiesData JobQuantitiesData)
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
                Option_ddl.SelectItem(JobQuantitiesData.Option);
                Source_ddl.SelectItem(JobQuantitiesData.Source);
                OpenBuildingPhase_btn.Click();
                foreach (string item in JobQuantitiesData.BuildingPhase)
                {
                    CheckBox BuildingPhase_chk = new CheckBox(FindType.XPath, $"//*[@id='rcbPhases_DropDown']//ul[@class='rcbList']//li//label[contains(text(),'{item}')]/input");
                    BuildingPhase_chk.Check(true);
                }
                LoadProducts_btn.Click();
                System.Threading.Thread.Sleep(4000);
                //Add Product Quantities And Quantity into Building Phase
                foreach (string product in JobQuantitiesData.Products)
                {

                    ProductQuantitiesPage_Grid.FilterByColumn("Product Name", GridFilterOperator.Contains, product);
                    WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");
                    CheckBox Product_chk = new CheckBox(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]//ancestor::tr//input[@type='checkbox']");
                    Textbox Quantity_txt = new Textbox(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]//ancestor::tr//input[@type='text']");
                    Button ExpandUse_btn = new Button(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]/../following-sibling::td/select");
                    ProductQuantitiesPage_Grid.FilterByColumn("Style", GridFilterOperator.Contains, JobQuantitiesData.Style);
                    WaitingLoadingGifByXpath("//*[@id='lp1rgProductsToAdd']/div[1]");
                    ExpandUse_btn.Click();
                    IWebElement IndexUse_ddl = FindElementHelper.FindElement(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]/../following-sibling::td/select");
                    Button getUse_btn = new Button(FindType.XPath, $"//*[@id='rgProductsToAdd_ctl00']//tbody//td//a[contains(text(),'{product}')]/../following-sibling::td/select/option[2]");
                    IndexUse_ddl.SendKeys(Keys.ArrowDown);
                    IndexUse_ddl.SendKeys(Keys.Enter);
                    listUse.Add(getUse_btn.GetText());
                    Quantity_txt.SetText(JobQuantitiesData.Quantity[Count]);
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
                CloseModalBtn.Click();
            }

            return listUse;
        }

        public void FilterItemInBOMGrid(string columnName, GridFilterOperator filterOperator, string value)
        {
            QuantitiesPage_Grid.FilterByColumn(columnName, filterOperator, value);
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        private void VerifyNoProductQuantityInJobQuantities(ProductToOptionData item, ProductData expectedProduct)
        {
            // Verify Product and total quantities
            Label productLbl = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
            Label quantitiesLbl = new Label(FindType.XPath, $"//tr[contains(@id, 'Content_rgOptionConfigurationQuantities')]/td[7]/*[text()='{expectedProduct.Quantities}']");

            if (VerifyElementLabelDisplay(productLbl) is false || VerifyElementLabelDisplay(quantitiesLbl) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                    $"and Quantities <b>'{expectedProduct.Quantities}'</b> on grid view." +
                    "<br>Failed to verify Job BOM.</font></br>");
            }
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                        $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
        }

        private void VerifyNoProductQuantityInJobQuantities(ProductToOptionData item)
        {
            Label noProductQuantityLbl = new Label(FindType.XPath, NoProductQuantityLabelXpath);
            if (VerifyElementLabelDisplay(noProductQuantityLbl) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find text with name .No non-zero quantities were found for the selected Option on grid view." +
                    "<br>Failed to verify Job BOM.</font></br>");
            }
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Text with name 'No non-zero quantities were found for the selected Option.' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
        }

        private void ExpandAllBuildingPhase()
        {
            Button expandAllPhaseBtn = new Button(FindType.XPath, ExpandAllPhaseBtnXpath);
            expandAllPhaseBtn.Click();
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        private void CheckAllBuildingPhaseName()
        {
            // Verify Building Phase
            Button expandAllPhaseBtn = new Button(FindType.XPath, ExpandAllPhaseBtnXpath);
            if (expandAllPhaseBtn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Expand All Phase button on grid view.</font>");
            }
        }

        private void ExpandBuildingPhaseAndVerifyItem(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            foreach (ProductToOptionData productToOptionItem in expectedData.productToOption)
            {
                if (isVerifyQuantities is false)
                {
                    CheckAllBuildingPhaseName();
                    ExpandAllBuildingPhase();
                    VerifyNoProductQuantityInJobQuantities(productToOptionItem);
                    // Collapse it
                    ExpandBuildingPhase(productToOptionItem.BuildingPhase);
                }
                CheckBuildingPhaseName(productToOptionItem.BuildingPhase);
                ExpandBuildingPhase(productToOptionItem.BuildingPhase);
                // Verify each product quantities on current building phase
                foreach (ProductData expectedProduct in productToOptionItem.ProductList)
                {
                    VerifyNoProductQuantityInJobQuantities(productToOptionItem, expectedProduct);
                }
                ExpandBuildingPhase(productToOptionItem.BuildingPhase);
            }
        }

        public void VerifyJobQuantitiesInGrid(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            FilterItemInBOMGrid("Option Name", GridFilterOperator.Contains, expectedData.optionName);
            CheckOptionName(expectedData.optionName);
            ExpandOption(expectedData);
            ExpandBuildingPhaseAndVerifyItem(expectedData, isVerifyQuantities);
            ExpandOrCollapseOption(expectedData.optionName);
        }

        private void CheckOptionName(string optionName)
        {
            if (IsItemInGrid("Option Name", optionName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name '{optionName}'on the BOM grid view." +
                    "<br>Failed to generate House BOM.</font></br>");
            }
        }

        private void ExpandOrCollapseOption(string optionName)
        {
            Button expandOptionBtn = new Button(FindType.XPath, $"//*[normalize-space(text())='{optionName}']/ancestor::tr/td[@class='rgExpandCol']/input");
            expandOptionBtn.RefreshWrappedControl();
            expandOptionBtn.Click();
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        private Button RetrieveExpandOptionButton(string optionName)
        {
            return new Button(FindType.XPath, $"//*[normalize-space(text())='{optionName}']" +
                $"/ancestor::tr/td[@class='rgExpandCol']/input");
        }

        private void ExpandOption(HouseQuantitiesData expectedData)
        {
            Button expandOptionBtn = RetrieveExpandOptionButton(expectedData.optionName);
            HandleProductToOptionCount(expectedData, expandOptionBtn);
            LogFailedToGetExpandOption(expandOptionBtn);
            expandOptionBtn.Click();
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        private void HandleProductToOptionCount(HouseQuantitiesData expectedData, Button expandOptionBtn)
        {
            if (expectedData.productToOption.Count != 0) return;
            CheckProductQuantityItem(expandOptionBtn);
        }

        private void CheckProductQuantityItem(Button expandOptionBtn)
        {
            if (!expandOptionBtn.IsDisplayed(false))
            {
                ExtentReportsHelper.LogPass($"<font color='green'>There is no product quantity item on the grid view." +
                                            "Successfully generated Job BOM.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Expand Option button should NOT be visible on grid view.Shouldn't display any product quantities on the grid view" +
                                            "<br>Failed to verify Job BOM.</font></br>");
            }
        }

        private static void LogFailedToGetExpandOption(Button expandOptionBtn)
        {
            if (expandOptionBtn.IsDisplayed(false)) return;
            ExtentReportsHelper.LogFail($"<font color='red'>Can't find Expand Option button on grid view.Failed to verify Job BOM.</font>");
        }

        private void VerifyProductAndStyleQuantitiesInJobQuantities(ProductToOptionData item, ProductData expectedProduct)
        {

            // Verify Product and total quantities
            Label productLbl = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
            Label quantitiesLbl = new Label(FindType.XPath, $"//tr[contains(@id, 'Content_rgOptionConfigurationQuantities')]/td[7]/*[text()='{expectedProduct.Quantities}']");
            Label styleLbl = new Label(FindType.XPath, $"//tr[contains(@id, 'ctl00_CPH_Content_rgOptionConfigurationQuantities_ctl00')]/td[contains(.,'{expectedProduct.Name}')]/following-sibling::td/span[text() = '{expectedProduct.Style}']");

            if (VerifyElementLabelDisplay(productLbl) is false || VerifyElementLabelDisplay(styleLbl) is false || VerifyElementLabelDisplay(quantitiesLbl) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                    $"and Style <b>'{expectedProduct.Style} and product {productLbl} and Style {styleLbl}'</b> on grid view." +
                    $"and Quantities <b>'{expectedProduct.Quantities} and product {productLbl} and quantities {quantitiesLbl}'</b> on grid view." +
                    "<br>Failed to verify Job Quantities.</font></br> ");
            }
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                        $"and Style <b>'{expectedProduct.Style} and product {productLbl} and Style {styleLbl}'</b>" +
                        $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
        }

        private Button RetrieveExpandBuildingPhaseButton(string buildingPhase)
        {
            return new Button(FindType.XPath, $"//td[contains(text(), '{buildingPhase}')]/../td[@class='rgExpandCol']");
        }

        private void ExpandBuildingPhase(string buildingPhase)
        {
            Button expandPhaseBtn = new Button(FindType.XPath, $"//td[contains(text(), '{buildingPhase}')]/../td[@class='rgExpandCol']");
            expandPhaseBtn.RefreshWrappedControl();
            expandPhaseBtn.Click();
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        private void CheckBuildingPhaseName(string buildingPhase)
        {
            // Verify Building Phase
            Button expandPhaseBtn = RetrieveExpandBuildingPhaseButton(buildingPhase);
            if (expandPhaseBtn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{buildingPhase}'</b> on grid view." +
                    $"<br>Failed to verify House BOM.</font></br>");
            }
        }

        private void HandleNotVerifyingQuantities(bool isVerifyQuantities)
        {
            if (!isVerifyQuantities)
            {
                ExtentReportsHelper.LogInformation($"{JOB_QUANTITIES_NO_PRODUCT}");
            }
        }

        private void VerifyProductQuantitiesAndStyleQuantitiesOnCurrentBuildingPhase(ProductToOptionData item, Button expandPhaseBtn)
        {
            // Verify each product quantities on current building phase
            foreach (ProductData expectedProduct in item.ProductList)
            {
                VerifyProductAndStyleQuantitiesInJobQuantities(item, expectedProduct);
            }
            // Collapse building phase
            expandPhaseBtn.RefreshWrappedControl();
            expandPhaseBtn.Click();
            WaitingLoadingGifByXpath(loadingGrid_xpath);
        }

        private void ExpandBuildingPhaseAndVerifyProductQuantities(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            foreach (ProductToOptionData item in expectedData.productToOption)
            {
                CheckBuildingPhaseName(item.BuildingPhase);
                ExpandBuildingPhase(item.BuildingPhase);
                HandleNotVerifyingQuantities(isVerifyQuantities);
                VerifyProductQuantitiesAndStyleQuantitiesOnCurrentBuildingPhase(item, RetrieveExpandBuildingPhaseButton(item.BuildingPhase));
            }
        }
        public void VerifyItemWithStyleOnJobQuantitiesGrid(HouseQuantitiesData expectedData)
        {
            FilterItemInBOMGrid("Option Name", GridFilterOperator.Contains, expectedData.optionName);
            CheckOptionName(expectedData.optionName);
            ExpandOption(expectedData);
            ExpandBuildingPhaseAndVerifyProductQuantities(expectedData);
            ExpandOrCollapseOption(expectedData.optionName);
        }

        public void UpdateJobQuantities(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            // Filter option name
            FilterItemInBOMGrid("Option Name", GridFilterOperator.Contains, expectedData.optionName);

            if (IsItemInGrid("Option Name", expectedData.optionName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name '{expectedData.optionName}' on the BOM grid view." +
                    "<br>Failed to generate House BOM.</font></br>");
                return;
            }
            // Verify Expand Option
            Button expandOptionBtn = new Button(FindType.XPath, $"//*[normalize-space(text())='{expectedData.optionName}']/ancestor::tr/td[@class='rgExpandCol']/input");

            if (expectedData.productToOption.Count == 0)
            {
                if (expandOptionBtn.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'>There is no product quantiy item on the grid view." +
                        "<br>Successfully generated Job BOM.</b></font></br>");
                    return;
                }
                ExtentReportsHelper.LogFail($"<font color='red'>The Expand Option button should NOT be visible on grid view." +
                    $"<br>Shouldn't display any product quantities on the grid view" +
                    "<br>Failed to verify Job BOM.</font></br>");
            }
            if (expandOptionBtn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand Option button on grid view.Failed to verify Job BOM.</font>");
                return;
            }
            expandOptionBtn.Click();
            WaitingLoadingGifByXpath(loadingGrid_xpath);
            foreach (ProductToOptionData item in expectedData.productToOption)
            {
                if (isVerifyQuantities is false)
                {
                    ExtentReportsHelper.LogInformation($"{JOB_QUANTITIES_NO_PRODUCT}");
                }
                // Verify Building Phase
                Button expandPhaseBtn = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");
                if (expandPhaseBtn.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{item.BuildingPhase}'</b> on grid view." +
                        $"<br>Failed to verify Job BOM.</font></br>");
                    return;
                }
                expandPhaseBtn.Click();
                WaitingLoadingGifByXpath(loadingGrid_xpath, 2000, 2000);

                // Verify each product quantities on current building phase
                foreach (ProductData expectedProduct in item.ProductList)
                {
                    // Verify Product and total quantities
                    Label productLbl = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                    Button productBtn = new Button(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]" +
                        $"/../following-sibling::td/input[@title='Edit']");
                    Textbox updateQuantitiesTxt = new Textbox(FindType.XPath, $"//*[@class='rgEditForm']//td/input[contains(@id,'txtbxQuantities')]");
                    Button updateQuantitiesBtn = new Button(FindType.XPath, $"//*[@class='rgEditForm']//td/a[contains(@id,'UpdateButton')]");
                    if (VerifyElementLabelDisplay(productLbl) is false)
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                            "<br>Failed to verify Job BOM.</font></br>");
                    }
                    else
                    {
                        productBtn.Click();
                        updateQuantitiesTxt.SetText(expectedProduct.Quantities);
                        WaitingLoadingGifByXpath(loadingGrid_xpath);
                        updateQuantitiesBtn.Click();
                        WaitingLoadingGifByXpath(loadingGrid_xpath);
                    }
                }
                // Collapse building phase
                expandPhaseBtn.RefreshWrappedControl();
                expandPhaseBtn.Click();
                WaitingLoadingGifByXpath(loadingGrid_xpath);
            }
        }

        public int GetTotalNumberJobQuantitiesItem()
        {
            return QuantitiesPage_Grid.GetTotalItems;
        }

        private void ValidateExportJobQuantitiesLabel()
        {
            Label exportJobQuantitiesLbl = new Label(FindType.XPath, $"//*[@class='card-header']" +
                $"/h1[contains(text(),'{ExportJobQuantityLabelBySource}')]");
            if (!(exportJobQuantitiesLbl.IsDisplayed(false) is false) ||
                     !(IsExportJobQuantitiesLabelMatching(ExportJobQuantityLabelBySource) is false)) return;
            ExtentReportsHelper.LogFail($"<font color='red'>{CAN_NOT_OPEN_EXPORT_JOB_QUANTITIES}</font>");
        }
        private void ValidateExportXMLJobQuantitiesLabel()
        {
            Label exportXMLJobQuantitiesLbl = new Label(FindType.XPath, $"//*[@class='card-header']" +
                $"/h1[contains(text(),'{ExportJobQuantityLabelBySourceXML}')]");
            if (!(exportXMLJobQuantitiesLbl.IsDisplayed(false) is false) ||
                     !(IsExportJobXMLQuantitiesLabelMatching(ExportJobQuantityLabelBySourceXML) is false)) return;
            ExtentReportsHelper.LogFail($"<font color='red'>{CAN_NOT_OPEN_EXPORT_JOB_QUANTITIES}</font>");
        }
        private void HandleIsCaptured(string fileType, bool isCaptured)
        {
            if (isCaptured)
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(ExportQuantitiesBtn),
                    $"Click <font color='green'><b><i>{fileType:g}</i></b></font> button.");
        }

        private void SelectFileTypeAndViewStyle(string fileType, string viewStyle)
        {
            Button fileTypeBtn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rbExportType']//label[contains(text(),'{fileType}')]");
            Button viewStyleBtn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rbExportStyle']//label[contains(text(),'{viewStyle}')]");
            CheckAllChkBox.SetCheck(true);
            fileTypeBtn.Click();
            viewStyleBtn.Click();
        }

        private void ClickOnExportQuantities()
        {
            ExportQuantitiesBtn.Click();
        }

        private void HandleNonXMLTypeExport(string fileType, string viewStyle, bool isCaptured)
        {
            ExportQuantitiesBtn.WaitForElementIsVisible(EXPORT_QUANTITIES_WAITING_TIME);
            HandleIsCaptured(fileType, isCaptured);
            ClickOnExportQuantities();
            ValidateExportJobQuantitiesLabel();
            SelectFileTypeAndViewStyle(fileType, viewStyle);
            ExportBtn.Click();
            System.Threading.Thread.Sleep(DOWNLOAD_WAITING_TIME);
            CancelBtn.Click();
        }

        private void HandleXMLTypeExport(string fileType, bool isCaptured)
        {
            ExportXMLQuantitiesBtn.WaitForElementIsVisible(EXPORT_QUANTITIES_WAITING_TIME);
            HandleIsCaptured(fileType, isCaptured);
            ClickOnExportQuantities();
            ValidateExportXMLJobQuantitiesLabel();
            ExportXMLBtn.Click();
            System.Threading.Thread.Sleep(DOWNLOAD_WAITING_TIME);
            CancelExportXMLBtn.Click();
        }

        public void DownloadBaselineJobQuantitiesFile(string fileType, string viewStyle, string exportName)
        {
            try
            {
                if (fileType != XML_TYPE)
                {
                    HandleNonXMLTypeExport(fileType, viewStyle, true);
                }
                else
                {
                    HandleXMLTypeExport(fileType, true);
                }
            }

            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {fileType} on File Type"));
            }

            // Verify and move it to baseline folder
            ValidationEngine.DownloadBaseLineFile(fileType, exportName);
        }

        public void ExportJobQuantitiesFile(string fileType, string viewStyle, string exportName, int expectedTotalNumber, string expectedExportTitle)
        {
            try
            {
                if (fileType != XML_TYPE)
                {
                    HandleNonXMLTypeExport(fileType, viewStyle, true);

                }
                else
                {
                    HandleXMLTypeExport(fileType, true);
                }
            }

            catch (NoAlertPresentException)
            {
                throw new NoAlertPresentException(string.Format($"Could not button with name {fileType} on File Type"));
            }

            // Verify Download File (included total number and title only)
            ValidationEngine.ValidateExportFile(fileType, exportName, expectedExportTitle, expectedTotalNumber);
        }
    }
}

