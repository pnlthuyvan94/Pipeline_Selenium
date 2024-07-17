using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionProduct
{
    public partial class CustomOptionProduct : DetailsContentPage<CustomOptionProduct>
    {
        public IList<string> GetListItem(string xpath)
        {
            IList<IWebElement> listElement = driver.FindElements(By.XPath($"{xpath}"));
            IList<string> listItem = new List<string>();
            foreach (var item in listElement)
            {
                string additem = item.Text;
                listItem.Add(additem);
            }
            return listItem;
        }

        public void Click_AddButton()
        {
            AddProduct_btn.Click();
            PageLoad();
            System.Threading.Thread.Sleep(1000);
        }
        public void Click_CopyProduct()
        {
            CoppyProduct.Click();
            PageLoad();
            System.Threading.Thread.Sleep(1000);
        }

        /// <summary>
        /// Click Generate BOM
        /// </summary>
        public void Click_GenerateBOMEstimate(bool isEstimate = true)
        {
            string button;
            if (isEstimate)
            {
                // Bom and estimate
                button = "//a[contains(text(),'Generate BOM & Estimate')]";
                CommonHelper.WaitUntilElementVisible(10, button);
                GenerateBOMEstimate_btn.Click();
            }
            else
            {

                // Bom only
                button = "//a[text()='Generate BOM']";
                CommonHelper.WaitUntilElementVisible(10, button);
                GenerateBOM_btn.Click();
            }           
            
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[1]");
        }

        public void SelectCommunityBOM(string community)
        {
            CommunityBOM_ddl.SelectItem(community, true, false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlRpt']/div[@class='raDiv']");
        }
        public void ClickExplainItemOnGridBOM()
        {
            ExpainItem_BOMGrid.Click();
            CustomOptionBOM_Grid.WaitGridLoad();
            ExpainSubItem_BOMGrid.Click();
            CustomOptionBOM_Grid.WaitGridLoad();
        }
        public void DeleteItemOnProductGird(string columnName, string valueToFind)
        {
            CustomOptionProduct_Grid.ClickDeleteItemInGrid(columnName, valueToFind);
            System.Threading.Thread.Sleep(1000);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl00']/div[1]", 3000);
            //CustomOptionProduct_Grid.WaitGridLoad();
        }

        public bool IsItemGird(string columnName, string valueToFind)
        {
            return CustomOptionBOM_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        public void AddProduct(string buildPhase)
        {
            try
            {
                BuildPhase_ddl.SelectItem(buildPhase, true, false);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rcbProductId']/div[1]");
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(BuildPhase_ddl), $"The page was selected item <font color='green'><b>{buildPhase}</b></font> from the dropdown list.");

                Button Product_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_Arrow']");
                Product_btn.Click();

                // Wait /loading' label on the Product ddd disappears
                CommonHelper.WaitUntilElementInvisible("//*[@id='ctl00_CPH_Content_rcbProductId_LoadingDiv']");
                System.Threading.Thread.Sleep(5000);
                Button ProductName_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']//li[1]");
                ProductName_btn.WaitForElementIsVisible(3);
                ProductName_btn.CoordinateClick();

                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlStyles']/div[1]");

                IList<string> ListStyle = GetListItem("//select[contains(@id,'ddlStyles')]/option");
                Style_ddl.SelectItem(ListStyle[0]);

                IList<string> ListUse = GetListItem("//select[contains(@id,'ddlUses')]/option");

                Use_ddl.SelectItem(ListUse[0]);

                Quantity_dll.SetText("20");

                IList<string> ListSource = GetListItem("//select[contains(@id,'ddlNewSourceTypes')]/option");
                Source_ddl.SelectItem(ListSource[2]);

                SaveProduct_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]");
            }
            catch (Exception e)
            {
                ExtentReportsHelper.LogFail($"<font color = 'red'>Can't add product to custom option. Error exception: {e}</font>");
            }
        }

        public void AddCustomOptionProduct(string buildPhase, string listUse, string quanity)
        {
            BuildPhase_ddl.SelectItem(buildPhase, true, false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rcbProductId']/div[1]", 2);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(BuildPhase_ddl), $"The page was selected item <font color='green'><b>{buildPhase}</b></font> from the dropdown list.");

            //IList<string> ListProduct = GetListItem("//select[contains(@id,'ddlProducts')]/option");
            product_addproduct_expand_btn.Click();
            System.Threading.Thread.Sleep(5000);
            IList<string> ListProduct = GetListItem("//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']/div[contains(@class,'rcbScroll')]/ul/li");

            Button producdll = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbProductId_DropDown']/div[contains(@class,'rcbScroll')]/ul/li[contains(text(),'{ListProduct[0]}')]");
            producdll.Click();
            //Product_ddl.SelectItem(ListProduct[0], true, false);

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlStyles']/div[1]", 1);
            //ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(BuildPhase_ddl), $"The page was selected item <font color='green'><b>{ListProduct}</b></font> from the dropdown list.");

            IList<string> ListStyle = GetListItem("//select[contains(@id,'ddlStyles')]/option");
            Style_ddl.SelectItem(ListStyle[0]);

            IList<string> ListUse = GetListItem("//select[contains(@id,'ddlUses')]/option");
            Use_ddl.SelectItem(listUse);

            Quantity_dll.SetText(quanity);

            // IList<string> ListSource = GetListItem("//select[contains(@id,'ddlNewSourceTypes')]/option");
            //Source_ddl.SelectItem(ListSource[0]);

            SaveProduct_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProducts']/div[1]");
        }
        public void CloseAddProduct()
        {
            if (Close_btn.WaitForElementIsVisible(5) == true)
                Close_btn.Click();
            PageLoad();
        }

        public void SelectOptionByName(params string[] optionName)
        {
            CheckBox option;
            foreach (var item in optionName)
            {
                option = new CheckBox(FindType.XPath, $"//div[@id='ctl00_CPH_Content_rlbOptionsTo']/div/ul/li/label/span[contains(text(),'{item}')]/../input");
                if (option.IsNull())
                    ExtentReportsHelper.LogFail($"The option with name <font color='green'><b>{item}</b></font> is not displayed on option list.");
                else
                    option.Check();
            }
        }
        public string Click_CopyQuantities()
        {
            CopyQuantities_btn.Click();
            return GetLastestToastMessage();
        }
        public void Click_CopyProductQuantities()
        {
            CopyProductQuantities_btn.Click();
            System.Threading.Thread.Sleep(1000);
        }
        public void CloseCopyProductQuantities()
        {
            if (CloseCopyProductQuantities_btn.WaitForElementIsVisible(5) == true)
                CloseCopyProductQuantities_btn.Click();
            PageLoad();
        }

        public bool IsItemCustomOptionQuantitiesGrid(string columnName, string valueToFind)
        {
            return CustomOptionProduct_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        public void EditItemOnProductGird(string columnName, string valueToFind)
        {
            CustomOptionProduct_Grid.ClickEditItemInGrid(columnName, valueToFind);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ctl00']/div[1]", 3000);
        }

        public void UpdateCustomOptionQuantities(string Style, string Use, string Quantity, string Source)
        {
            // Select Style
            DropdownList style_ddl = new DropdownList(FindType.XPath, "//*[contains(@id,'ddlEditStyles')]");
            if (!style_ddl.IsNull() && Style != string.Empty)
            {
                style_ddl.SelectItem(Style, true);
            }

            // Select Use
            DropdownList use_ddl = new DropdownList(FindType.XPath, "//*[contains(@id,'ddlEditUses')]");
            if (!use_ddl.IsNull() && Use != string.Empty)
                use_ddl.SelectItem(Use, true);

            // Set Quantity
            Textbox quantity_txt = new Textbox(FindType.XPath, "//*[contains(@id,'txtProductsToCustomOptions_Quantity')]");
            if (!quantity_txt.IsNull() && Convert.ToDouble(Quantity) > 0)
                quantity_txt.SetText(Quantity.ToString());


            // Select Source
            DropdownList source_ddl = new DropdownList(FindType.XPath, "//*[contains(@id,'ddlsourcetypes')]");
            if (!source_ddl.IsNull() && Source != string.Empty)
                source_ddl.SelectItem(Source, true);


            // Click save button
            Button add_btn = new Button(FindType.XPath, "//*[contains(@id,'UpdateButton')]");
            if (!add_btn.IsNull())
                add_btn.Click();

            // Verify toast message
            string _expectedMessage = $"Custom Option Product saved successfully!";
            string actualMsg = GetLastestToastMessage();
            if (actualMsg == _expectedMessage)
            {
                ExtentReportsHelper.LogPass(null, $"<font color = 'green'><b>Option Quantities updated successfully!</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color = 'red'>Option quantities could not be updated - Possible constraints preventing additional.</font>");
            }
        }
    }

}
