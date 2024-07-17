using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductDetail
{
    public partial class ProductDetailPage
    {
        public ProductDetailPage EnterName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }

        public string SelectManufacture(string manufactureName)
        {
            if (Manufactur_ddl.IsItemInList(manufactureName))
            {
                Manufactur_ddl.SelectItem(manufactureName, false, false);
                PageLoad();
                return manufactureName;
            }
            else
            {
                Manufactur_ddl.SelectItem(0);
                PageLoad();
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Manufactur_ddl), $"Select Manufacturer {Manufactur_ddl.SelectedItemName}");
                return Manufactur_ddl.SelectedItemName;
            }
        }

        public string SelectStyle(string stylename)
        {
            return Style_ddl.SelectItemByValueOrIndex(stylename, 0);
        }

        public ProductDetailPage EnterProductCode(string code)
        {
            ProductCode_txt.SetText(code);
            return this;
        }

        public ProductDetailPage EnterDescription(string desc)
        {
            Description_txt.SetText(desc);
            return this;
        }

        public ProductDetailPage EnterNotes(string note)
        {
            Note_txt.SetText(note);
            return this;
        }

        public string SelectUnit(string unit)
        {
            return Unit_ddl.SelectItemByValueOrIndex(unit, 1);
        }

        public ProductDetailPage EnterSKU(string sku)
        {
            SKU_txt.SetText(sku);
            return this;
        }

        public ProductDetailPage EnterRoundingUnit(string roundingUnit)
        {
            RoundingUnit_txt.SetText(roundingUnit);
            return this;
        }

        public ProductDetailPage EnterWaste(string waste)
        {
            Waste_txt.SetText(waste);
            return this;
        }

        public ProductDetailPage SelectRoundingRule(string ruleName = "")
        {
            switch (ruleName)
            {
                case "Always Round Up":
                    RoundingRule.AlwaysRoundUp_chk.Check();
                    break;
                case "Always Round Down":
                    RoundingRule.AlwaysRoundDown_chk.Check();
                    break;
                default:
                    RoundingRule.StandardRounding_chk.Check();
                    break;
            }
            return this;
        }

        public string SelectBuildingPhase(string phase)
        {
            return BuildingPhase_ddl.SelectItemByValueOrIndex(phase, 10);
        }

        public ProductDetailPage IsSupplemental(bool supplementalStatus)
        {
            // This function can't apply to current supplemental checkbox
            //Supplemental_chk.SetCheck(supplementalStatus);

            // Get supplement element
            CheckBox supplemental = new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_ckbSupToBid']");

            if (supplementalStatus is true && supplemental.IsChecked is false)
                supplemental.JavaScriptClick();
            else if (supplementalStatus is false && supplemental.IsChecked is true)
                supplemental.JavaScriptClick();

            return this;
        }

        public ProductData CreateAProduct(ProductData product)
        {
            EnterName(product.Name);
            product.Manufacture = SelectManufacture(product.Manufacture);
            product.Style = SelectStyle(product.Style);
            EnterProductCode(product.Code)
            .EnterDescription(product.Description);
            product.Unit = SelectUnit(product.Unit);
            EnterNotes(product.Notes)
                .EnterRoundingUnit(product.RoundingUnit)
                .SelectRoundingRule(product.RoundingRule)
                .EnterWaste(product.Waste);
            product.BuildingPhase = SelectBuildingPhase(product.BuildingPhase);
            Save();
            ProductData newproduct = new ProductData(product)
            {
                Manufacture = product.Manufacture,
                Style = product.Style,
                Unit = product.Unit,
                BuildingPhase = product.BuildingPhase
            };
            return newproduct;
        }

        public void Save()
        {
            CommonHelper.ScrollToBeginOfPage();
            Save_btn.Click();
            // WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]");
            int iTime = 0;
            while (FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]".ToString(), 1) != null)
            {
                // Wait until timeout
                System.Threading.Thread.Sleep(500);
                iTime++;
                if (iTime > 120)
                    throw new TimeoutException("The grid is loaded over 60s.");
            }
            PageLoad();
        }
        public void SaveProductDetail()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbLoadingAnimation']/div[1]");

        }

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

        public void AddBuildingPhases(string buildingPhases, bool check, string taxStatus)
        {
            AddBuildingPhase_btn.Click();

            BuildingPhases_ddl.SelectItem(buildingPhases);
            if (check == true)
            {
                NewPhaseDefault_cb.Check();
            }
            else
            {
                NewPhaseDefault_cb.UnCheck();
            }

            TaxStatus_ddl.SelectItem(taxStatus);
            SaveBuildingPhase_btn.Click();
            //System.Threading.Thread.Sleep(3000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");
            BuildingPhase_grid.WaitGridLoad();

        }
        public void AddManufacturersStyles(string manufacturer, bool check, string style, string productCode)
        {
            //Manufacturer_ddl.WaitForElementIsVisible(1, false);

            AddManufacturer_btn.Click();
            Manufacturer_ddl.SelectItem(manufacturer);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewStyles']/div[1]");
            StyleManufacturer_ddl.SelectItem(style);

            if (check == true)
            {
                NewManufacturerDefault_cb.Check();
            }
            else
            {
                NewManufacturerDefault_cb.UnCheck();
            }

            ProductCodeManufacturer_txt.SetText(productCode);

            SaveManufacturer_btn.Click();
            //System.Threading.Thread.Sleep(3000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewStyles']/div[1]");
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyles']/div[1]");
            Manufacture_grid.WaitGridLoad();
            // WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewManufacturers']/div[1]");
            //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyles"]/div[1]
            //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewManufacturers"]/div[1]
            //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewStyles"]/div[1]

        }
        public float GetTotalItemOnStyle()
        {
            System.Threading.Thread.Sleep(1000);
            return Manufacture_grid.GetTotalItems;
        }
        public float GetTotalItemOnCatelogy()
        {
            CommonHelper.ScrollToBeginOfPage();
            System.Threading.Thread.Sleep(1000);
            return Categories_grid.GetTotalItems;
        }

        public void AddCategories(string catelogires)
        {
            CommonHelper.ScrollToBeginOfPage();
            AddCatelories_btn.Click();
            Button Catelogires_cb = new Button(FindType.XPath, $"//span[text()='{catelogires}']//preceding-sibling::input");
            Catelogires_cb.Click();
            SaveCatelories_btn.Click();
            System.Threading.Thread.Sleep(1000);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rlbCategories']/div[1]");
            Categories_grid.WaitGridLoad();


        }
        public void AddHouseNotes(string houses, string options, string buildPhase, string productStyles, string houseNotes)
        {
            AddHouseNote_btn.Click();

            Button House_cb = new Button(FindType.XPath, $"//span[text()='{houses}']//preceding-sibling::input");

            Button Options_cb = new Button(FindType.XPath, $"//span[text()='{options}']//preceding-sibling::input");
            Button BuildPhase_cb = new Button(FindType.XPath, $"//span[text()='{buildPhase}']//preceding-sibling::input");
            Button ProductStyles_cb = new Button(FindType.XPath, $"//span[text()='{productStyles}']//preceding-sibling::input");
            Button SaveHouse_btn = new Button(FindType.XPath, $"//a[contains(@id,'ctl00_CPH_Content_lbSaveHouseNoteOverride')]");
            if (House_cb.IsDisplayed() is true)
            {
                House_cb.Click();
            }
            else
            {
                CheckBox HouseIndex_chk = new CheckBox(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rlbHousesHouse']//li//input)[1]");
                HouseIndex_chk.Check(true);
            }

            if (Options_cb.IsDisplayed() is true)
            {
                Options_cb.Click();
            }
            else
            {
                CheckBox OptionIndex_chk = new CheckBox(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rlbOptionsHouse']//li//input)[1]");
                OptionIndex_chk.Check(true);
            }

            if (BuildPhase_cb.IsDisplayed() is true)
            {
                BuildPhase_cb.Click();
            }
            else
            {
                CheckBox BuildingPhaseIndex_chk = new CheckBox(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rlbHPBPHouse']//li//input)[1]");
                BuildingPhaseIndex_chk.Check(true);
            }

            if (ProductStyles_cb.IsDisplayed() is true)
            {
                ProductStyles_cb.Click();
            }
            else
            {
                CheckBox ProductStylesIndex_chk = new CheckBox(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rlbStyleHouse']//li//input)[1]");
                ProductStylesIndex_chk.Check(true);
            }

            HouseNotes_txt.SetText(houseNotes);
            System.Threading.Thread.Sleep(20000);
            SaveHouse_btn.Click();
            CommonHelper.ScrollToEndOfPage();
            HouseNoteOverrides_Grid.WaitGridLoad();
            System.Threading.Thread.Sleep(30000);
            //WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesHouse']/div[1]");

        }
        public void AddJobNotes(string option, string customOption, string buildPhase, string productStyles, string jobNotes)
        {
            CommonHelper.ScrollToEndOfPage();
            JobNotes_btn.Click();
            Button Option_cb = new Button(FindType.XPath, $"//div[contains(@id,'rlbOptionsJob')]//following-sibling::span[text()='{option}']//preceding-sibling::input");
            Button CustompOptions_cb = new Button(FindType.XPath, $"//span[text()='{customOption}']//preceding-sibling::input");
            Button BuildPhase_cb = new Button(FindType.XPath, $"//div[contains(@id,'rlbHPBPJob')]//span[text()='{buildPhase}']//preceding-sibling::input");
            Button ProductStyles_cb = new Button(FindType.XPath, $"//div[contains(@id,'rlbStyleJob')]//span[text()='{productStyles}']//preceding-sibling::input");
            Button SaveJob_btn = new Button(FindType.XPath, $"//a[@id='ctl00_CPH_Content_lbSaveJobNoteOverride' and @class ='long-button']");

            if (Option_cb.IsDisplayed() is true)
            {
                Option_cb.Click();
            }
            else
            {
                CheckBox OptionIndex_chk = new CheckBox(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rlbOptionsJob']//li//input)[1]");
                OptionIndex_chk.Check(true);
            }

            if (CustompOptions_cb.IsDisplayed() is true)
            {
                CustompOptions_cb.Click();
            }
            else
            {
                CheckBox CustompOptionIndex_chk = new CheckBox(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rlbCOptionsJob']//li//input)[1]");
                CustompOptionIndex_chk.Check(true);
            }

            if (BuildPhase_cb.IsDisplayed() is true)
            {
                BuildPhase_cb.Click();
            }
            else
            {
                CheckBox BuildPhaseIndex_chk = new CheckBox(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rlbHPBPJob']//li//input)[1]");
                BuildPhaseIndex_chk.Check(true);
            }


            if (ProductStyles_cb.IsDisplayed() is true)
            {
                ProductStyles_cb.Click();
            }
            else
            {
                CheckBox ProductStyleIndex_chk = new CheckBox(FindType.XPath, "(//*[@id='ctl00_CPH_Content_rlbStyleJob']//li//input)[1]");
                ProductStyleIndex_chk.Check(true);
            }

            JobNotes_txt.SetText(jobNotes);

            System.Threading.Thread.Sleep(20000);

            SaveJobNotes_btn.Click();
            CommonHelper.ScrollToEndOfPage();
            JobNoteOverrides_Grid.WaitGridLoad();
            System.Threading.Thread.Sleep(30000);
            //WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesJob']/div[1]");
            //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesJob"]/div[1]
            //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesJob"]/div[1]

        }
        public bool IsItemOnBuildPhaseGrid(string columnName, string valueToFind)
        {
            return BuildingPhase_grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        public void FilterItemInBuildPhaseGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            BuildingPhase_grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']", 2000);
            //BuildingPhase_grid.WaitGridLoad();
        }
        public void DeleteItemOnBuildPhaseGrid(string columnName, string valueToFind)
        {
            CommonHelper.ScrollToBeginOfPage();
            BuildingPhase_grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']", 3000);
        }
        public bool IsItemOnManufacturerGrid(string columnName, string valueToFind)
        {
            return Manufacture_grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        public void EditItemOnManufacturerGrid(string columnName, string valueToFind)
        {
            Manufacture_grid.ClickEditItemInGrid(columnName, valueToFind);
            Manufacture_grid.WaitGridLoad();
        }

        public void DeleteItemOnManufacturersGrid(string columnName, string valueToFind)
        {
            CommonHelper.ScrollToBeginOfPage();
            Manufacture_grid.ClickDeleteItemInGrid(columnName, valueToFind);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgStyles']/div[1]");
        }
        public bool IsItemOnCategoryGrid(string columnName, string valueToFind)
        {
            return Category_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        public void DeleteItemOnCategoriesGrid(string item)
        {
            Button Delete = new Button(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgCategories_ctl00']/tbody/tr/td/a[contains(text(),'{item}')]/../../td/input[contains(@src,'delete')]");
            CommonHelper.ScrollToBeginOfPage();
            //Category_Grid.ClickDeleteFirstItem();
            //Categories_grid.ClickDeleteItemInGrid(columnName, valueToFind);
            Delete.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCategories']/div[1]");
        }

        public bool IsItemOnHouseNoteGrid(string columnName, string valueToFind)
        {
            return HouseNoteOverrides_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        public void DeleteItemOnHouseNoteGrid(string item)
        {
            Button Delete = new Button(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgNotesHouse_ctl00']/tbody/tr/td/span[contains(text(),'{item}')]/../../td/input[contains(@src,'delete')]");
            CommonHelper.ScrollToEndOfPage();
            // HouseNoteOverrides_Grid.ClickDeleteFirstItem();
            Delete.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesHouse']/div[1]");
        }

        public bool IsItemOnJobNoteGrid(string columnName, string valueToFind)
        {
            return JobNoteOverrides_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }
        public void DeleteItemOnJobNoteGrid(string item)
        {
            ////table[@id="ctl00_CPH_Content_rgNotesJob_ctl00"]/tbody/tr/td/span[contains(text(),"testing notes")]/../../td/input[contains(@src,"delete")]
            Button Delete = new Button(FindType.XPath, $"//table[@id='ctl00_CPH_Content_rgNotesJob_ctl00']/tbody/tr/td/span[contains(text(),'{item}')]/../../td/input[contains(@src,'delete')]");
            CommonHelper.ScrollToEndOfPage();
            // JobNoteOverrides_Grid.ClickDeleteFirstItem();
            Delete.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgNotesJob']/div[1]");
        }
        public void ClickManufacturerStyleAddBtn()
        {
            AddManufacturer_btn.Click();
            WaitingLoadingGifByXpath("//div[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewManufacturers']/div[1]");
        }
        public void ClickAddManufacturerButton()
        {
            AddNewManufacturerButton_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewManufacturers']/div[1]");

        }
        public void InputTextToManufacturer(string textContent)
        {
            ManufacturerInputText_tbx.Clear();
            ManufacturerInputText_tbx.SetText(textContent);
        }
        public void ClickInsertManufacturerButton()
        {
            ManufacInsert_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlInsertMfg2']/div[1]");
        }
        public void ClickManufactCancelButton()
        {
            ManufacCancel_btn.Click();
            WaitingLoadingGifByXpath("//div[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlInsertMfg2']/div[1]");
        }
        public void ClickAddStyleButton()
        {
            AddStyle_btn.Click();
            WaitingLoadingGifByXpath("//div[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlNewStyles']/div[1]");
        }
        public void ClickStyleCancelButton()
        {
            StyleCancel_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlInsertStyle2']/div[1]");
        }
        public void InputTextToStyle(string textContent)
        {
            StyleInputText_tbx.Clear();
            StyleInputText_tbx.SetText(textContent);
        }
        public void ClickInsertStyleButton()
        {
            StyleInsert_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlInsertStyle2']/div[1]");
        }
    }

}
