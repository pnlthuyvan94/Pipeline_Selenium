using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductSubcomponent
{
    public partial class ProductSubcomponentPage
    {
        public bool IsSubcomponentInGrid()
        {
            if (NoSubcomponent.IsDisplayed(false))
            {
                return false;
            }
            return true;
        }

        public void WaitConvertFromGridLoad()
        {
            ConvertFrom_Grid.WaitGridLoad();
        }

        public void ClickAdd_btn()
        {
            AddSubcomponent_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddSubcomponent']/div[1]");
        }

        public void Close_Modal(string modalTitle)
        {
            Button close = new Button(FindType.XPath, $"//span[contains(text(),'{modalTitle}')]/parent::h1/following-sibling::a");
            close.Click();
            System.Threading.Thread.Sleep(1000);
        }

        public ProductSubcomponentPage ClickSaveProductSubcomponent()
        {
            SaveSubcomponent_btn.Click();
            //*[@id="ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveAddSubcomponent"]/div[1]
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveAddSubcomponent']/div[1]");
            return this;
        }


        public ProductSubcomponentPage SelectBasicORAdvanced(string value)
        {
            if (value == "Advanced")
            {
                Advanced_btn.Click();
            }
            else
                Basic_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlAddParentBuildingPhase']/div[1]");
            System.Threading.Thread.Sleep(3000);
            return this;
        }

        public ProductSubcomponentPage SelectBuildingPhaseOfProduct(string buidingphase)
        {
            BuildingPhaseofProduct.SelectItem(buidingphase);
            System.Threading.Thread.Sleep(1000);
            return this;
        }
        public ProductSubcomponentPage SelectStyleOfProduct(string style)
        {
            StylelofProduct.SelectItem(style);
            return this;
        }
        public ProductSubcomponentPage SelectCatelogy(string catelogy)
        {
            CatelogyOfSubcomponent.SelectItem(catelogy);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlProductWithCategory']/div[1]");
            System.Threading.Thread.Sleep(1000);
            return this;
        }
        public ProductSubcomponentPage InputProductSubcomponent(string productSubcomponent)
        {

            ProductWithCategorySubcomponentName.SetText(productSubcomponent);
            System.Threading.Thread.Sleep(5000);
            Button ddlProductSubcomponent = new Button(FindType.XPath, $"//div[@id='ctl00_CPH_Content_ddlProductWithCategory_DropDown']//li");
            ddlProductSubcomponent.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlBuildingPhaseWithCategory']/div[1]");
            System.Threading.Thread.Sleep(3000);
            return this;
        }
        public ProductSubcomponentPage SelectBuildingPhaseOfSubComponent(string buidingphase)
        {
            BuildingPhaseWithCategory_ddl.SelectItem(buidingphase);
            System.Threading.Thread.Sleep(1000);
            return this;
        }
        public ProductSubcomponentPage SelectChildBuildingPhaseOfSubComponent(string buidingphase)
        {
            ChildBuildingPhaseOfSubcomponent.SelectItem(buidingphase);
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        public ProductSubcomponentPage SelectStyleOfSubComponent(string buidingphase)
        {
            StyleOfSubcomponent.SelectItem(buidingphase);
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        public ProductSubcomponentPage SelectChildStyleOfSubComponent(string buidingphase)
        {
            ChildStyleOfSubcomponent.SelectItem(buidingphase);
            System.Threading.Thread.Sleep(1000);
            return this;
        }
        public ProductSubcomponentPage SelectChildUseOfSubComponent(string use)
        {
            ChildUseOfSubcomponent_ddl.SelectItem(use);
            System.Threading.Thread.Sleep(1000);
            return this;
        }
        public ProductSubcomponentPage UpdateChildUseOfSubComponent(string use)
        {
            UpdateChildUseOfSubcomponent_ddl.SelectItem(use);
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        public void ClickEnableDisable_SubComponent(string name)
        {
            Button EnableORDisable = new Button(FindType.XPath, $"//span[contains(text(),'{name}')]//..//ancestor::tr[1]//td//input[contains(@id,'ibEditEnabled')]");
            EnableORDisable.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlSubcomponents']/div[1]");
        }

        public void ClickEditInGird(string name)
        {
            Button Edit = new Button(FindType.XPath, $"//span[contains(text(),'{name}')]//..//ancestor::tr[1]//td//input[contains(@src,'edit')]");
            Edit.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlSubcomponents']/div[1]");
            System.Threading.Thread.Sleep(4000);

            Label EditSubcomponent_lbl = new Label(FindType.XPath, "//*[@id='edit-modal-productsubcomponent']//h1");
            if (EditSubcomponent_lbl.GetText().Equals("Edit Subcomponent") is true)
            {
                ExtentReportsHelper.LogPass("<font color ='green'><b>Edit subcomponent Modal is displayed</b></font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color ='red'><b>Edit subcomponent Modal is displayed</b></font color>");
            }
        }

        public void ClickDeleteInGird(string name)
        {
            Button Delete = new Button(FindType.XPath, $"//span[contains(text(),'{name}')]//..//ancestor::tr[1]//td//input[contains(@src,'delete')]");
            if(Delete.IsDisplayed())
            {
                Delete.Click();
                ConfirmDialog(ConfirmType.OK);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlSubcomponents']/div[1]");
            }            
        }
        public void SelectUseEitSubcomponent(string use)
        {
            UseEditSubcomponent.SelectItem(use);
            System.Threading.Thread.Sleep(2000);
        }
        public void ClickSaveEditSubcomponent()
        {
            SaveEditSubComponent.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveEditSubcomponent']/div[1]");
        }

        public ProductSubcomponentPage AssignProductModal_ClickSave()
        {
            AssignProductModal_Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveAssignShowProduct']/div[1]");
            //System.Threading.Thread.Sleep(1000);
            return this;
        }
        public ProductSubcomponentPage AssignShowProductButton_Click()
        {
            AssignShowProduct_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAssignShowProduct']/div[1]");
            System.Threading.Thread.Sleep(2000);
            return this;
        }
        public ProductSubcomponentPage AssignProductModal_SelectBuildingPhase(string buildingPhase)
        {
            CheckBox BuildingPhase = new CheckBox(FindType.XPath, $"//div[@id='assign-showproduct-modal']//span[contains(text(),'{buildingPhase}')]//..//input");
            BuildingPhase.Check();
            return this;
        }

        public ProductSubcomponentPage CopySubComponentButton_Click()
        {
            CopySubcomponent_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbCopySubcomponents']/div[1]");
            System.Threading.Thread.Sleep(2000);
            return this;
        }
        public ProductSubcomponentPage SelectiveCopyOrBatchCopy(string value)
        {
            if (value == "SelectiveCopy")
            {
                SelectiveCopy_btn.Click();
            }
            else
                BatchCopy_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rbModeCopy']/div[1]");
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        public ProductSubcomponentPage ProductToCopyFrom(string ProductName)
        {
            // Select Product
            Textbox value_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbFromProduct_Input']");
            Button Product_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rcbFromProduct_DropDown']//ul//li[contains(text(),'{ProductName}')]");
            Button FromProduct_ddl = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbFromProduct_Arrow']");

            FromProduct_ddl.Click();
            //FIll in product name
            value_txt.SetText(ProductName);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_rcbFromProduct_LoadingDiv']");

            Product_btn.WaitForElementIsVisible(2);
            Product_btn.JavaScriptClick();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSubToCopy']/div[1]");
            return this;
        }

        public ProductSubcomponentPage CopySubcomponentModal_SelectBuildingPhaseInBatchCopy(string BuildingPhase)
        {
            Button OpenBuildingArrow_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlToBuildingPhaseBatch_Arrow']");
            CheckBox BuildingPhase_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ddlToBuildingPhaseBatch_DropDown']//label[contains(text(),'{BuildingPhase}')]/input");
            CheckBox AllBuildingPhase_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ddlToBuildingPhaseBatch_DropDown']//label[contains(text(),'Check All')]/input");
            OpenBuildingArrow_btn.Click();
            if (BuildingPhase != string.Empty && BuildingPhase_chk.GetText().Equals(BuildingPhase))
            {
                BuildingPhase_chk.Check(true);
            }
            else
            {
                AllBuildingPhase_chk.Check(true);
            }
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_txtSearchProductTo']/div[1]", 5000);

            //Collapse BuildingPhase
            OpenBuildingArrow_btn.Click();
            return this;
        }
        public ProductSubcomponentPage CopySubcomponentModal_SelectProductToInBatchCopy(string product)
        {
            Textbox ProductName_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtSearchProductTo']");
            CheckBox Product_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rlbToProductBatch']//ul//li//span/em[contains(text(),'{product}')]//ancestor::li/label/input");
            Label ProductChecked_lbl = new Label(FindType.XPath, "//*[@id='countBatchProduct']");
            //FIll in product name
            ProductName_txt.SetText(product);
            Product_chk.WaitForElementIsVisible(10);
            Product_chk.SetCheck(true);
            if (ProductChecked_lbl.GetText().Contains(product))
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b>The Product with Name {product} is selected in ProductTo Batch Copy</b></font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>The Product with Name {product} is not select in ProductTo Batch Copy</font color>");
            }

            return this;
        }

        public ProductSubcomponentPage CopySubcomponentModal_ClickSaveInBatchCopy()
        {
            CopySubcomponentBatchCopyModal_Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveCopySubcomponents']/div[1]");
            //System.Threading.Thread.Sleep(1000);
            return this;
        }

        public ProductSubcomponentPage CopySubcomponentModal_SelectStyleInBatchCopy(string style)
        {
            Button OpenStyleArrow_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlToStyleBatch_Arrow']");
            CheckBox Style_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ddlToStyleBatch_DropDown']//label[contains(text(),'{style}')]/input");
            CheckBox AllStyle_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ddlToStyleBatch_DropDown']//label[contains(text(),'Check All')]/input");
            OpenStyleArrow_btn.Click();
            if (style != string.Empty && Style_chk.GetText().Equals(style))
            {
                Style_chk.Check(true);
            }
            else
            {
                AllStyle_chk.Check(true);
            }
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_txtSearchProductTo']/div[1]", 5000);
            //Collap Building Phase
            OpenStyleArrow_btn.Click();
            return this;
        }



        public ProductSubcomponentPage CopySubcomponentModal_SelectBuildingPhaseTo(string buildingPhase)
        {
            CopySubcomponentModal_BuildingPhaseTo_ddl.SelectItem(buildingPhase);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rlbToProduct']/div[1]");
            System.Threading.Thread.Sleep(2000);
            return this;
        }

        public ProductSubcomponentPage CopySubcomponentModal_SelectProduct(string Product)
        {
            Textbox Product_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtCopySearch']");
            CheckBox Product_chk = new CheckBox(FindType.XPath, $"//span[@id='ctl00_CPH_Content_ctl00_CPH_Content_rlbToProductPanel']//span[contains(text(),'{Product}')]//..//input | //span[@id='ctl00_CPH_Content_ctl00_CPH_Content_rlbToProductPanel']//span/em[contains(text(),'{Product}')]//ancestor::li/label/input");
            Product_txt.SetText(Product);
            Product_chk.RefreshWrappedControl();
            Product_chk.SetCheck(true);
            return this;
        }
        public ProductSubcomponentPage CopySubcomponentModal_SelectBuildingPhaseFrom(string buildingPhase)
        {
            CopySubcomponentModal_BuildingPhaseFrom_ddl.SelectItem(buildingPhase);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rlbSubcomponentsToCopy']/div[1]");
            System.Threading.Thread.Sleep(2000);
            return this;
        }
        public ProductSubcomponentPage CopySubcomponentModal_SelectSubcomponentToCopyFrom(string buildingPhase)
        {
            CheckBox CopySubcomponentModal_SelectSubcomponentToCopyFrom_ddl = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSubToCopy_ctl00']//td[contains(text(),'{buildingPhase}')]//ancestor::tr//td[1]");
            CopySubcomponentModal_SelectSubcomponentToCopyFrom_ddl.Check();
            System.Threading.Thread.Sleep(3000);
            return this;
        }
        public ProductSubcomponentPage CopySubcomponentModal_ClickSave()
        {
            CopySubcomponentModal_Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveCopySubcomponents']/div[1]");
            //System.Threading.Thread.Sleep(1000);
            return this;
        }
        public ProductSubcomponentPage FilterSubcomponentInGird(string Subcomponent)
        {
            FilterSubComponent_ddl.SelectItem(Subcomponent);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlSubcomponents']/div[1]");
            System.Threading.Thread.Sleep(2000);
            return this;
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

        public bool VerifyItemInGrid(string columnName, string value)
        {
            string findItem_Xpath = $"//*[contains(@id,'lbl{columnName}') and contains(@id, 'Subcomponent') and text() = '{value}']";
            IWebElement calculation_On_Row = driver.FindElement(By.XPath($"{findItem_Xpath}"));

            if (calculation_On_Row.Displayed is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>{columnName} with value {value} displayed correctly on the Subcomponent grid.</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Can't find {columnName} with value {value} on the Subcomponent grid.</font>");
                return false;
            }

        }

        public ProductSubcomponentPage SelectOptionSubcomponent(string option)
        {
            CheckBox OptionSubcomponent = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rlbAddOptionPanel']//span/em[contains(text(),'{option}')]//ancestor::label/input");
            OptionSubcomponent_txt.SetText(option);
            OptionSubcomponent.SetCheck(true);
            return this;
        }

        public ProductSubcomponentPage InputProductSubcomponentWithoutCategory(string productSubcomponent)
        {

            Textbox ChildProductSubcomponentName_txt = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rcbAddChildProductName_Input']");
            ChildProductSubcomponentName_txt.SetText(productSubcomponent);
            System.Threading.Thread.Sleep(5000);
            Button ddlProductSubcomponent = new Button(FindType.XPath, $"//div[@id='ctl00_CPH_Content_rcbAddChildProductName_DropDown']//li");
            ddlProductSubcomponent.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ddlAddChildProductStyle']/div[1]");
            System.Threading.Thread.Sleep(1000);
            return this;
        }

        public ProductSubcomponentPage SelectCalculationSubcomponent(string calculation)
        {
            CalculationSubcomponent_ddl.SelectItem(calculation);
            System.Threading.Thread.Sleep(1000);
            return this;
        }
        public void DeleteAllProductSubcomponent()
        {
            AllSubcomponent_chk.Check();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlSubcomponents']/div[1]");
            Button DeleteData_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_lbDeleteSelectedSubcomponents']");
            SubcomponentBulkActions_btn.Click();
            DeleteData_btn.Click();
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rtlSubcomponents']/div[1]");
        }

        public void OpenStyleModal()
        {
            Button openStyleModal_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddStyle']");
            if (openStyleModal_btn.IsDisplayed())
            {
                openStyleModal_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddStyle']/div[1]");
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Add Style Button is not display in Product Subcomponent");
            }
        }

        public void AddNewStyleInSubcomponent(string style, string manufacture, string productcode, bool IsCreated)
        {
            OpenStyleModal();

            Button AddNewManufacturer_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewManufacturer']");
            AddNewManufacturer_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddNewManufacturer']/div[1]");

            InputManufacturer_txt.SetText(manufacture);
            InsertManufacturer_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbInsertManufacturer']/div[1]");

            Button AddNewStyle_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewStyle']");
            AddNewStyle_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddNewStyle']/div[1]");

            InputStyle_txt.SetText(style);
            InsertStyle_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbInsertStyle']/div[1]");


            SelectManufacture_ddl.SelectItem(manufacture);
            SelectStyle_ddl.SelectItem(style);
            ProductCode_txt.SetText(productcode);
            if (IsCreated == true)
            {
                SaveStyle_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewStyle']/div[1]");

                if (ChildStyleOfSubcomponent.IsItemInList(style) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Style with name {style} is add new in list.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Style with name {style} is not add new in list.</font>");
                }
            }
            else
            {
                CloseStyle_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewStyle']/div[1]");

                if (ChildStyleOfSubcomponent.IsItemInList(style) is false)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Style with name {style} is cancel successfully.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Style with name {style} is not cancel successfully and still in List.</font>");
                }
            }
        }
        public void AddNewStyleInSubcomponentWithInvalid(string style, string manufacture)
        {
            OpenStyleModal();

            if (SelectManufacture_ddl.IsItemInList(manufacture) is true)
            {
                Button AddNewManufacturer_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewManufacturer']");
                AddNewManufacturer_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddNewManufacturer']/div[1]");

                InputManufacturer_txt.SetText(manufacture);
                InsertManufacturer_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbInsertManufacturer']/div[1]");
                if(ErrorManufacturer_lbl.GetText().Equals("**The Manufacturer Name must be unique.") is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Manufacturer Error Message is display as expect.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>The Manufacturer Error Message is not display as expect.</font>");
                }
                
                CancelManufacturer_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbCancelManufacturer']/div[1]");
            }

            if (SelectStyle_ddl.IsItemInList(style) is true)
            {
                Button AddNewStyle_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewStyle']");
                AddNewStyle_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbAddNewStyle']/div[1]");

                InputStyle_txt.SetText(style);
                InsertStyle_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbInsertStyle']/div[1]");
                if (ErrorStyle_lbl.GetText().Equals("**The Style Name in the same Manufacturer must be unique.") is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Style Error Message is display as expect.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>The Style Error Message is not display as expect.</font>");
                }

                CancelStyle_btn.Click();
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbCancelStyle']/div[1]");
            }

        }
    }

}
