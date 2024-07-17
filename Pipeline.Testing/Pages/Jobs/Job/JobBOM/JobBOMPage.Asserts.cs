using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Estimating.Products;

namespace Pipeline.Testing.Pages.Jobs.Job.JobBOM
{
    public partial class JobBOMPage
    {
        public void VerifyItemOnJobBOMGrid(HouseQuantitiesData expectedData, bool isVerifyQuantities = true)
        {
            // Filter option name
            FilterItemInBOMGrid("Option", GridFilterOperator.EqualTo, expectedData.optionName);

            if (IsItemInGrid("Option", expectedData.optionName) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name <b>'{expectedData.optionName}'</b>on the BOM grid view." +
                    "<br>Failed to generate House BOM.</font></br>");
                return;
            }


            // Verify Expand Option
            Button expandOption = new Button(FindType.XPath, $"//*[normalize-space(text())='{expectedData.optionName}']/ancestor::tr/td[@class='rgExpandCol']/input");
            
            // Expectation: There is no product quantities
            if (expectedData.productToOption.Count == 0)
            {
                if (expandOption.IsDisplayed(false) is false)
                {
                    // There is NO product quantities on the grid view
                    ExtentReportsHelper.LogPass($"<font color='green'><b>There is no product quantiy item on the grid view." +
                        "<br>Successfully generated Job BOM.</b></font></br>");
                    return;
                }
                else
                {
                    // There are any product quantities on the grid view
                    ExtentReportsHelper.LogFail($"<font color='red'>The <b>Expand Option button </b> should NOT be visible on grid view." +
                        $"<br>Shouldn't display any product quantities on the grid view" +
                        "<br>Failed to verify Job BOM.</font></br>");
                    return;
                }
            }
            // Expectation: There is any product quantities
            if (expandOption.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand Option</b> button on grid view." +
                    "<br>Failed to verify Job BOM.</font></br>");
                return;
            }
            expandOption.Click();
            WaitingLoadingGifByXpath(loadingGrid_xpath);


            foreach (ProductToOptionData item in expectedData.productToOption)
            {
                // Verify Phase Bid
                Label phaseBid = new Label(FindType.XPath, $"//td[normalize-space(text())= '{item.BuildingPhase}']/following-sibling::td[1]");
                if (phaseBid.IsExisted() is true && phaseBid.GetText() == item.PhaseBid)
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Phase Bid column displays with correct value '{item.PhaseBid}' on grid view.</b></font>");
                else
                    ExtentReportsHelper.LogFail($"<font color='red'>Phase Bid column SHOULD display with value <b>'{item.PhaseBid}'</b> on grid view." +
                        $"<br>Failed to display Phase Bid.</font></br>");

                if (isVerifyQuantities is true)
                {
                    // Verify Building Phase
                    Button expandPhase = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");
                    if (expandPhase.IsDisplayed(false) is false)
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{item.BuildingPhase}'</b> on grid view." +
                            $"<br>Failed to verify Job BOM.</font></br>");
                        return;
                    }

                    expandPhase.Click();
                    WaitingLoadingGifByXpath(loadingGrid_xpath, 2000, 2000);

                    // Verify each product quantities on current building phase
                    foreach (ProductData expectedProduct in item.ProductList)
                    {
                        // Verify Product and total quantities
                        Label product = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                        Label quantities = new Label(FindType.XPath, $"//tr[contains(@id, 'Content_rgOptionsView')]/td[10]/*[text()='{expectedProduct.Quantities}']");

                        if (product.IsDisplayed(false) is false || quantities.IsDisplayed(false) is false)
                        {
                            ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                                $"and Quantities <b>'{expectedProduct.Quantities}'</b> on grid view." +
                                "<br>Failed to verify Job BOM.</font></br>");
                        }
                        else
                            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                                    $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                    }

                    // Collapse building phase
                    expandPhase.RefreshWrappedControl();
                    expandPhase.Click();
                    WaitingLoadingGifByXpath(loadingGrid_xpath);
                }
                else
                {
                    // If there is no quantity to verify then check the text "	No non-zero quantities were found for the selected Option."
                    // Expand all phase
                    Button expandAllPhase = new Button(FindType.XPath, $"//th[text()='Phases']/preceding-sibling::th/input");
                    if (expandAllPhase.IsDisplayed(false) is false)
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>'Expand All Phase button'</b> on grid view.</font></br>");
                        return;
                    }

                    expandAllPhase.Click();
                    WaitingLoadingGifByXpath(loadingGrid_xpath);

                    Label noQuantities = new Label(FindType.XPath, $"//td[text() = 'No non-zero quantities were found for the selected Option.']");
                    if (noQuantities.IsDisplayed(false) is false)
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find text with name <b>'	No non-zero quantities were found for the selected Option.'</b> on grid view." +
                            "<br>Failed to verify Job BOM.</font></br>");
                    }
                    else
                        ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Text with name 'No non-zero quantities were found for the selected Option.'" +
                            $" display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");

                    // Collapse it
                    expandAllPhase.RefreshWrappedControl();
                    expandAllPhase.Click();
                    WaitingLoadingGifByXpath(loadingGrid_xpath);
                }
            }
        }

        public void VerifySupplementalByPhase(HouseQuantitiesData expectedData)
        {
            foreach (ProductToOptionData item in expectedData.productToOption)
            {
                // Get Phase Bid by option and phase
                Label phaseBid = new Label(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/following-sibling::td/a[text() = '{item.PhaseBid}']");
                if (phaseBid.IsDisplayed(false) is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Phase Bid value <b>'{item.PhaseBid}'</b> of phase '{item.BuildingPhase}' on grid view.</font>");
                    return;
                }
                phaseBid.JavaScriptClick();
                WaitingLoadingGifByXpath(loadingGrid_xpath);


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
        public bool VerifyJobBomPageIsDisplayed(string title)
        {
            JobBomHeader_lbl.WaitForElementIsVisible(5);
            if (JobBomHeader_lbl.GetText().Equals(title) && CurrentURL.Contains("Dashboard/BuilderBom/JobBom/JobBom.aspx"))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(JobBomHeader_lbl), "<font color='green'><b>The Job Bom Page is displayed correctly</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(JobBomHeader_lbl), "<font color='red'>The Job Bom Page is displayed wrong</font>");
                return false;
            }
        }
        public void VerifyViewByDropDownList(HouseQuantitiesData expectedData, string ViewType, bool isVerifyQuantities = true)
        {
            SwitchJobBomView(ViewType);
            switch (ViewType)
            {
                case "Phase/Product":

                    foreach (ProductToOptionData item in expectedData.productToOption)
                    {

                        if (isVerifyQuantities is true)
                        {
                            // Verify Building Phase
                            Button expandPhase = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");
                            if (expandPhase.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{item.BuildingPhase}'</b> on grid view." +
                                    $"<br>Failed to verify Job BOM.</font></br>");
                                return;
                            }

                            expandPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath, 2000, 2000);

                            // Verify each product quantities on current building phase
                            foreach (ProductData expectedProduct in item.ProductList)
                            {
                                // Verify Product and total quantities
                                Label product = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                                Label quantities = new Label(FindType.XPath, $"//tr[contains(@id, 'Content_rgPhasesView')]/td/a[contains(text(),'{expectedProduct.Quantities}')]");

                                if (product.IsDisplayed(false) is false || quantities.IsDisplayed(false) is false)
                                {
                                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                                        $"and Quantities <b>'{expectedProduct.Quantities}'</b> on grid view." +
                                        "<br>Failed to verify Job BOM.</font></br>");
                                }
                                else
                                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                                            $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                            }

                            // Collapse building phase
                            expandPhase.RefreshWrappedControl();
                            expandPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);
                        }
                        else
                        {
                            // If there is no quantity to verify then check the text "	No non-zero quantities were found for the selected Option."
                            // Expand all phase
                            Button expandAllPhase = new Button(FindType.XPath, $"//th[text()='Phases']/preceding-sibling::th/input");
                            if (expandAllPhase.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>'Expand All Phase button'</b> on grid view.</font></br>");
                                return;
                            }

                            expandAllPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);

                            Label noQuantities = new Label(FindType.XPath, $"//td[text() = 'No non-zero quantities were found for the selected Option.']");
                            if (noQuantities.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find text with name <b>'	No non-zero quantities were found for the selected Option.'</b> on grid view." +
                                    "<br>Failed to verify Job BOM.</font></br>");
                            }
                            else
                                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Text with name 'No non-zero quantities were found for the selected Option.'" +
                                    $" display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");

                            // Collapse it
                            expandAllPhase.RefreshWrappedControl();
                            expandAllPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);
                        }
                    }
                    break;
                case "Phase/Product/Use":
                    foreach (ProductToOptionData item in expectedData.productToOption)
                    {

                        if (isVerifyQuantities is true)
                        {
                            // Verify Building Phase
                            Button expandPhase = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");
                            if (expandPhase.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{item.BuildingPhase}'</b> on grid view." +
                                    $"<br>Failed to verify Job BOM.</font></br>");
                                return;
                            }

                            expandPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath, 2000, 2000);

                            // Verify each product quantities on current building phase
                            foreach (ProductData expectedProduct in item.ProductList)
                            {
                                // Verify Product and total quantities
                                Label product = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                                Label quantities = new Label(FindType.XPath, $"//tr[contains(@id, 'Content_rgPhasesUsesView')]/td/a[contains(text(),'{expectedProduct.Quantities}')]");

                                if (product.IsDisplayed(false) is false || quantities.IsDisplayed(false) is false)
                                {
                                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                                        $"and Quantities <b>'{expectedProduct.Quantities}'</b> on grid view." +
                                        "<br>Failed to verify Job BOM.</font></br>");
                                }
                                else
                                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                                            $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                            }

                            // Collapse building phase
                            expandPhase.RefreshWrappedControl();
                            expandPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);
                        }
                        else
                        {
                            // If there is no quantity to verify then check the text "	No non-zero quantities were found for the selected Option."
                            // Expand all phase
                            Button expandAllPhase = new Button(FindType.XPath, $"//th[text()='Phases']/preceding-sibling::th/input");
                            if (expandAllPhase.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>'Expand All Phase button'</b> on grid view.</font></br>");
                                return;
                            }

                            expandAllPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);

                            Label noQuantities = new Label(FindType.XPath, $"//td[text() = 'No non-zero quantities were found for the selected Option.']");
                            if (noQuantities.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find text with name <b>'	No non-zero quantities were found for the selected Option.'</b> on grid view." +
                                    "<br>Failed to verify Job BOM.</font></br>");
                            }
                            else
                                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Text with name 'No non-zero quantities were found for the selected Option.'" +
                                    $" display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");

                            // Collapse it
                            expandAllPhase.RefreshWrappedControl();
                            expandAllPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);
                        }
                    }
                    break;
                case "Option/Phase/Product":
                    VerifyItemOnJobBOMGrid(expectedData);
                    break;

                case "Option/Phase/Product/Use":
                    // Filter option name
                    FilterItemInPhaseUseBOMGrid("Option", GridFilterOperator.EqualTo, expectedData.optionName);

                    if (IsItemInPhaseUseGrid("Option", expectedData.optionName) is false)
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find Option with name <b>'{expectedData.optionName}'</b>on the BOM grid view." +
                            "<br>Failed to generate House BOM.</font></br>");
                        return;
                    }


                    // Verify Expand Option
                    Button expandOption = new Button(FindType.XPath, $"//*[normalize-space(text())='{expectedData.optionName}']/ancestor::tr/td[@class='rgExpandCol']/input");

                    // Expectation: There is no product quantities
                    if (expectedData.productToOption.Count == 0)
                    {
                        if (expandOption.IsDisplayed(false) is false)
                        {
                            // There is NO product quantities on the grid view
                            ExtentReportsHelper.LogPass($"<font color='green'><b>There is no product quantiy item on the grid view." +
                                "<br>Successfully generated Job BOM.</b></font></br>");
                            return;
                        }
                        else
                        {
                            // There are any product quantities on the grid view
                            ExtentReportsHelper.LogFail($"<font color='red'>The <b>Expand Option button </b> should NOT be visible on grid view." +
                                $"<br>Shouldn't display any product quantities on the grid view" +
                                "<br>Failed to verify Job BOM.</font></br>");
                            return;
                        }
                    }
                    // Expectation: There is any product quantities
                    if (expandOption.IsDisplayed(false) is false)
                    {
                        ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>Expand Option</b> button on grid view." +
                            "<br>Failed to verify Job BOM.</font></br>");
                        return;
                    }
                    expandOption.Click();
                    WaitingLoadingGifByXpath(loadingGrid_xpath);


                    foreach (ProductToOptionData item in expectedData.productToOption)
                    {
                        // Verify Phase Bid
                        Label phaseBid = new Label(FindType.XPath, $"//td[normalize-space(text())= '{item.BuildingPhase}']/following-sibling::td[1]");
                        if (phaseBid.IsExisted() is true && phaseBid.GetText() == item.PhaseBid)
                            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Phase Bid column displays with correct value '{item.PhaseBid}' on grid view.</b></font>");
                        else
                            ExtentReportsHelper.LogFail($"<font color='red'>Phase Bid column SHOULD display with value <b>'{item.PhaseBid}'</b> on grid view." +
                                $"<br>Failed to display Phase Bid.</font></br>");

                        if (isVerifyQuantities is true)
                        {
                            // Verify Building Phase
                            Button expandPhase = new Button(FindType.XPath, $"//td[contains(text(), '{item.BuildingPhase}')]/../td[@class='rgExpandCol']");
                            if (expandPhase.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find Building Phase with name <b>'{item.BuildingPhase}'</b> on grid view." +
                                    $"<br>Failed to verify Job BOM.</font></br>");
                                return;
                            }

                            expandPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath, 2000, 2000);

                            // Verify each product quantities on current building phase
                            foreach (ProductData expectedProduct in item.ProductList)
                            {
                                // Verify Product and total quantities
                                Label product = new Label(FindType.XPath, $"//a[contains(@rel, 'Products/Details') and contains(text(), '{expectedProduct.Name}')]");
                                Label quantities = new Label(FindType.XPath, $"//tr[contains(@id, 'Content_rgOptionsPhasesUsesView')]/td/a[contains(text(),'{expectedProduct.Quantities}')]");

                                if (product.IsDisplayed(false) is false || quantities.IsDisplayed(false) is false)
                                {
                                    ExtentReportsHelper.LogFail($"<font color='red'>Can't find Product with name <b>'{expectedProduct.Name}'</b> " +
                                        $"and Quantities <b>'{expectedProduct.Quantities}'</b> on grid view." +
                                        "<br>Failed to verify Job BOM.</font></br>");
                                }
                                else
                                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Product with name '{expectedProduct.Name}' " +
                                            $"and Quantities '{expectedProduct.Quantities}' display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");
                            }

                            // Collapse building phase
                            expandPhase.RefreshWrappedControl();
                            expandPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);
                        }
                        else
                        {
                            // If there is no quantity to verify then check the text "	No non-zero quantities were found for the selected Option."
                            // Expand all phase
                            Button expandAllPhase = new Button(FindType.XPath, $"//th[text()='Phases']/preceding-sibling::th/input");
                            if (expandAllPhase.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find <b>'Expand All Phase button'</b> on grid view.</font></br>");
                                return;
                            }

                            expandAllPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);

                            Label noQuantities = new Label(FindType.XPath, $"//td[text() = 'No non-zero quantities were found for the selected Option.']");
                            if (noQuantities.IsDisplayed(false) is false)
                            {
                                ExtentReportsHelper.LogFail($"<font color='red'>Can't find text with name <b>'	No non-zero quantities were found for the selected Option.'</b> on grid view." +
                                    "<br>Failed to verify Job BOM.</font></br>");
                            }
                            else
                                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Text with name 'No non-zero quantities were found for the selected Option.'" +
                                    $" display correctly on Building Phase '{item.BuildingPhase}' grid view.</b>");

                            // Collapse it
                            expandAllPhase.RefreshWrappedControl();
                            expandAllPhase.Click();
                            WaitingLoadingGifByXpath(loadingGrid_xpath);
                        }
                    }
                    break;
                default:

                    ExtentReportsHelper.LogInformation(null, $"No View type is selected.");
                    break;
            }
        }
    }
}
