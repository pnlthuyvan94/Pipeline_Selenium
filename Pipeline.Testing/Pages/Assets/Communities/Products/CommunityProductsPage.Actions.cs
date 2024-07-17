using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Products
{
    public partial class CommunityProductsPage
    {

        public bool IsItemInCommunityQuantitiesGrid(string buildingphase, string product)
        {
            Label Buildingphase_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgCommunityQuantities_ctl00']//tbody//td/span[contains(text(),'{buildingphase}')]");
            Label Product_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgCommunityQuantities_ctl00']//tbody//td/a[contains(text(),'{product}')]");
            if (Buildingphase_lbl.IsDisplayed() && Product_lbl.GetText().Equals(product))
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The Product with Name {product} is displayed in grid.</font>");
                return true;
            }
            else
            {
                return false;
            }
        }

        public void AddCommunityQuantities(CommunityQuantitiesData communityPhaseData)
        {
            Button Add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewCommunityQuantity']");
            if (Add_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Can't find Add new Community Quantities button. Stop this step.</font>");
                return;
            }
            Add_btn.Click(false);

            Label title_lbl = new Label(FindType.XPath, "//h1[text()='Add Product']");
            if (title_lbl.IsDisplayed(false) is false)
                ExtentReportsHelper.LogFail("<font color='red'>Can't Open Add Product modal or the title is incorrect.</font>");

            // Populate the data to modal and save
            PoppulateQuantitiesToModal(communityPhaseData);
        }

        public void PoppulateQuantitiesToModal(CommunityQuantitiesData communityPhaseData)
        {
            // Select Building Phase
            DropdownList phase_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlBuildingPhases']");
            if (phase_ddl.IsDisplayed(false) is true && communityPhaseData.BuildingPhase != string.Empty)
            {
                phase_ddl.SelectItem(communityPhaseData.BuildingPhase, true);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddCommunityQuantity']/div[1]", 1000);
            }


            Textbox selectedProduct = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId']");
            selectedProduct.RefreshWrappedControl();

            // If product is selected then don't need to select again
            if (selectedProduct.GetValue().StartsWith(communityPhaseData.ProductName) is false)
            {
                // Select Product
                Button product_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId']/span/button");
                product_btn.Click(false);

                // Check if the dropdown panel doesn't display then clicking again.
                string productPanel_Xpath = "//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']/div/ul[@class='rcbList']";
                IWebElement ProductPanel = FindElementHelper.FindElement(FindType.XPath, productPanel_Xpath);
                if (ProductPanel.Enabled == false)
                {
                    product_btn.JavaScriptClick(false);
                    CommonHelper.WaitUntilElementVisible(5, productPanel_Xpath);
                }

                string productXpath = productPanel_Xpath + $"/li[contains(text(), '{communityPhaseData.ProductName}')]";
                // Wait to product display and get it
                CommonHelper.WaitUntilElementVisible(5, productXpath);

                IWebElement value = FindElementHelper.FindElement(FindType.XPath, productXpath);
                if (value.Enabled is false)
                    ExtentReportsHelper.LogFail($"The Product <font color='red'><b>{communityPhaseData.ProductName + "-" + communityPhaseData.ProductDescription }</b></font> is not displayed on Product list.");
                else
                {
                    value.Click();
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddCommunityQuantity']/div[1]", 1000);
                }

                // Select Style
                DropdownList style_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlStyles']");
                if (!style_ddl.IsNull() && communityPhaseData.Style != string.Empty)
                {
                    style_ddl.SelectItem(communityPhaseData.Style, true);
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddCommunityQuantity']/div[1]", 1000);
                }

                // Select Use
                DropdownList use_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlUses']");
                if (!use_ddl.IsNull() && communityPhaseData.Use != string.Empty)
                    use_ddl.SelectItem(communityPhaseData.Use, true);

                // Set Quantity
                Textbox quantity_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtQuantity']");
                if (!quantity_txt.IsNull() && Convert.ToDouble(communityPhaseData.Quantity) > 0)
                    quantity_txt.SetText(communityPhaseData.Quantity.ToString());

                //Select Option
                Textbox option_ddl = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_cbOptions_Input']");
                if (!option_ddl.IsNull() && communityPhaseData.OptionName != string.Empty)
                {
                    option_ddl.SetText(communityPhaseData.OptionName);
                    WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlAddCommunityQuantity']/div[1]", 1000);
                }

                // Select Source
                DropdownList source_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlSourceTypes']");
                if (!source_ddl.IsNull() && communityPhaseData.Source != string.Empty)
                    source_ddl.SelectItem(communityPhaseData.Source, true);


                // Click save button
                Button add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertCommunityQuantity']");
                if (!add_btn.IsNull())
                    add_btn.Click();

                // Verify toast message
                string _expectedMessage = $"Product'{communityPhaseData.ProductName}'was added successfully.";
                string actualMsg = GetLastestToastMessage();
                if (actualMsg == _expectedMessage)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Community quantities with phase {communityPhaseData.BuildingPhase} added successfully!</b></font>");
                }
                else
                {
                    if (IsItemInCommunityQuantitiesGrid(communityPhaseData.BuildingPhase, communityPhaseData.ProductName))
                        ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Community quantities with phase {communityPhaseData.BuildingPhase} added successfully!</b></font>");
                    else
                        ExtentReportsHelper.LogFail($"<font color = 'red'>Community quantities with phase {communityPhaseData.BuildingPhase} could not be added - Possible constraints preventing additional.</font>");

                }
            }
        }

        public void DeleteAllCommunityQuantities()
        {
            Button DeleteData_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbDeleteAllCommunityQuantities']");
            BulkActions_btn.Click();
            DeleteData_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCommunityQuantities']/div[1]");
        }

        public void CloseModal()
        {
            CloseModal_btn.Click();
        }

        public void CheckFunctionalProductModal()
        {
            Button Add_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbNewCommunityQuantity']");
            if (Add_btn.IsDisplayed(false) is false)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Can't find Add new Community Quantities button. Stop this step.</font>");
                return;
            }
            Add_btn.Click(false);

            //verify Product dropdown field
            Textbox ProductControl_Input = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Input']");
            if (ProductControl_Input.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product field is displayed</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product field is not display</font color>");
            }

            //Product field then verify the list of products that is shown   
            Button product_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId']/span/button");
            product_btn.Click(false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']",3000);
            System.Threading.Thread.Sleep(5000);
            IWebElement ProductControl_ArrrProduct = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']");
            if (ProductControl_ArrrProduct.Displayed)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product dropdown field is displayed</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product dropdown field is not display</font color>");
            }

            //The product description and distinguish the “Product Name” and “Product Description” by a dash “-” character
            Button Product_btn = new Button(FindType.XPath, $"(//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']//li)[1]");
            Product_btn.WaitForElementIsVisible(5);
            Product_btn.RefreshWrappedControl();
            if (Product_btn.IsDisplayed() && Product_btn.GetText().Contains("-") is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product Name field is contain {"-"} character </font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product Name field is not contain {"-"} character </font color>");
            }

            //Close Modal
            CloseModal();

        }
    }
}
