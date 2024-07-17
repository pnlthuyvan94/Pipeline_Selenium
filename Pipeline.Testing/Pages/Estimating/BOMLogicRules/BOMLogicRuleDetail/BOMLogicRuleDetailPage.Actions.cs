using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.BOMLogicRules.BOMLogicRuleDetail
{
    public partial class BOMLogicRuleDetailPage
    {

        public bool IsItemInGrid(string columnName, string value)
        {
            return Condition_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            Condition_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMLogicRules']", 1000);
        }
        public void DeleteItemInGrid(string columnName, string value)
        {
            Condition_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMLogicRuleConditions']/div[1]",5000);
        }

        public void ClickEditItemGrid(string columnName, string value)
        {
            Condition_Grid.ClickEditItemInGrid(columnName, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMLogicRuleConditions']/div[1]");
        }

        public void ClickAddToShowCreateACondition()
        {
            Condition_btn.Click(false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbConditionModal']/div[1]");

            if (!IsModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Condition\" modal doesn't display or title is incorrect.");
            }

        }

        public void SelectCondition(BOMLogicRuleDetailData BOMLogicRuleDetailData)
        {
            //Condition Key
            ConditionKey_ddl.SelectItem(BOMLogicRuleDetailData.ConditionKey);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ConditionModal']/div[1]");

            if (ConditionKey_ddl.SelectedItemName == BOMLogicRuleDetailData.ConditionKey)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Condition Key is selected {BOMLogicRuleDetailData.ConditionKey} correctly</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Condition Key is select {BOMLogicRuleDetailData.ConditionKey} incorrectly</font color>");
            }

            //Condition Key Attribute
            ConditionKeyAttribute_ddl.SelectItem(BOMLogicRuleDetailData.ConditionKeyAttribute);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ConditionModal']/div[1]");

            if (ConditionKeyAttribute_ddl.SelectedItemName == BOMLogicRuleDetailData.ConditionKeyAttribute)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Condition Key Attribute is selected {BOMLogicRuleDetailData.ConditionKeyAttribute} correctly</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Condition Key Attribute is select {BOMLogicRuleDetailData.ConditionKeyAttribute} incorrectly</font color>");
            }

        }

        public void SelectOperator(string Operator)
        {
            //Operator
            Operator_ddl.SelectItem(Operator);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ConditionModal']/div[1]");
            if (Operator_ddl.SelectedItemName == Operator)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Operator is selected {Operator} correctly</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Operator is select {Operator} incorrectly</font color>");
            }
        }


        public void SelectConditionValueForProduct(string searchProduct , List<string> listitem)
        {
            Button OpenArrow_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_radProductControl_Arrow']");
            OpenArrow_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_radProductControl_LoadingDiv']");
            // Select Product
            Textbox ProductValue_element = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_radProductControl_Input']");

            ProductValue_element.SetText(searchProduct);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_radProductControl_LoadingDiv']");

            foreach (string item in listitem)
            {
                CheckBox Product_chk = new CheckBox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_radProductControl_DropDown']//ul//li/label[contains(text(),'{item}')]/input | //*[@id='ctl00_CPH_Content_radProductControl_DropDown']//ul//li[contains(@title,'{item}')]/label/input");

                if (Product_chk.IsDisplayed() is true)
                {
                    Product_chk.SetCheck(true);
                }
            }
        }

        public void SelectConditionValueForBuildingPhase(string BuildingPhase)
        {
            Button OpenArrow_btn = new Button(FindType.XPath, $"//*[@aria-labelledby='select2-ctl00_CPH_Content_dllAddDropdownControl-container']");
            OpenArrow_btn.Click();

            Textbox BuildingPhaseValue_element = new Textbox(FindType.XPath, "//*[@id='searchDropDown']");

            BuildingPhaseValue_element.SetText(BuildingPhase);
            // Select BuidingPhase
            Button BuildingPhase_btn = new Button(FindType.XPath, $"//*[@id='select2-ctl00_CPH_Content_dllAddDropdownControl-results']/li[contains(text(),'{BuildingPhase}')]");
            BuildingPhase_btn.Click();

        }
        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ConditionModal']/div[1]");
        }

        public void CloseModal()
        {
            Cancel_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ConditionModal']/div[1]");
        }

        public void UpdateConditionValueForProduct(string searchProduct, List<string> listitem)
        {
            IWebElement ProductValue_txt = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBOMLogicRuleConditions_ctl00']//span/input[contains(@id,'radProductEditControl_Input')]");
            Button OpenArrow_btn = new Button(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgBOMLogicRuleConditions_ctl00']//span/button/span[contains(@id,'radProductEditControl_Arrow')]");

            OpenArrow_btn.Click();
            WaitingLoadingGifByXpath("//*[contains(@id,'radProductEditControl_LoadingDiv')]");
            ProductValue_txt.SendKeys(searchProduct);
            WaitingLoadingGifByXpath("//*[contains(@id,'radProductEditControl_LoadingDiv')]");
            foreach (string item in listitem)
            {
                CheckBox Product_chk = new CheckBox(FindType.XPath, $"//*[contains(@id,'radProductEditControl_DropDown')]//ul//li[contains(@title,'{item}')]/label/input");

                if (Product_chk.IsDisplayed() is true)
                {
                    Product_chk.SetCheck(true);
                }
            }

            UpdateSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgBOMLogicRuleConditions']/div[1]");

        }
        public void CheckFunctionalProductModal()
        {

            //verify Product dropdown field
            Textbox ProductControl_Input = new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_radProductControl_Input']");
            if (ProductControl_Input.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product field is displayed</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product field is not display</font color>");
            }

            //Product field then verify the list of products that is shown   
            Button product_btn = new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_radProductControl_Arrow']");
            product_btn.Click(false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_radProductControl_LoadingDiv']");

            IWebElement ProductControl_ArrrProduct = FindElementHelper.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_radProductControl_DropDown']");
            if (ProductControl_ArrrProduct.Displayed)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Product dropdown field is displayed</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Product dropdown field is not display</font color>");
            }

            //The product description and distinguish the “Product Name” and “Product Description” by a dash “-” character
            Button Product_btn = new Button(FindType.XPath, $"(//*[@id='ctl00_CPH_Content_radProductControl_DropDown']//li/label)[1]");
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

        }

        public void ClickAddToActions()
        {
            Action_btn.Click(false);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbActionModal']/div[1]");

            if (!IsActionModalDisplayed())
            {
                ExtentReportsHelper.LogFail("\"Add Action\" modal doesn't display or title is incorrect.");
            }

        }

        public void SelectAction(BOMLogicRuleDetailData BOMLogicRuleDetailData)
        {
            //Condition Key
            ActionKey_ddl.SelectItem(BOMLogicRuleDetailData.ActionKey);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ActionModal']/div[1]");

            if (ActionKey_ddl.SelectedItemName == BOMLogicRuleDetailData.ActionKey)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'>Action Key is selected {BOMLogicRuleDetailData.ActionKey} correctly</font color>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Action Key is select {BOMLogicRuleDetailData.ActionKey} incorrectly</font color>");
            }
        }

        public void SelectActionValue(string buildingPhase, string product,string style,string option)
        {
            Button openBuildingPhase_btn = new Button(FindType.XPath, $"//*[@title='Select Building Phase']");
            openBuildingPhase_btn.Click();

            // Select BuidingPhase
            Textbox BuildingPhaseValue_element = new Textbox(FindType.XPath, "//*[@placeholder='Search Building Phase']");

            BuildingPhaseValue_element.SetText(buildingPhase);

            Button BuildingPhase_btn = new Button(FindType.XPath, $"//*[@id='select2-ctl00_CPH_Content_ddlBuildingPhase-results']/li[contains(text(),'{buildingPhase}')]");
          
            BuildingPhase_btn.Click();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ActionModal']/div[1]");

            // Select Product

            Button openProduct_btn = new Button(FindType.XPath, $"//*[@title='Select Product']");
            openProduct_btn.Click();

            Textbox ProductValue_element = new Textbox(FindType.XPath, "//*[@placeholder='Search Product']");

            ProductValue_element.SetText(product);

            Button Product_btn = new Button(FindType.XPath, $"//*[@id='select2-ctl00_CPH_Content_ddlProduct-results']/li[contains(text(),'{product}')]");

            Product_btn.Click();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ActionModal']/div[1]");

            // Select Style
            Button openStyle_btn = new Button(FindType.XPath, $"//*[@title='Select Product Style']");
            openStyle_btn.Click();

            Textbox StyleValue_element = new Textbox(FindType.XPath, "//*[@placeholder='Search Product Style']");

            StyleValue_element.SetText(style);

            Button Style_btn = new Button(FindType.XPath, $"//*[@id='select2-ctl00_CPH_Content_ddlStyle-results']/li");

            Style_btn.Click();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ActionModal']/div[1]");

            // Select Product Option
            Button openOption_btn = new Button(FindType.XPath, $"//*[@title='Select Product Option']");
            openOption_btn.Click();

            Textbox OptionValue_element = new Textbox(FindType.XPath, "//*[@placeholder='Search Product Option']");

            OptionValue_element.SetText(option);

            Button Option_btn = new Button(FindType.XPath, $"//*[@id='select2-ctl00_CPH_Content_ddlOption-results']/li[contains(text(),'{option}')]");

            Option_btn.Click();

            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ActionModal']/div[1]");

        }

        public void SaveCreateAction()
        {
            ActionSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_ActionModal']/div[1]");
        }
    }
}
