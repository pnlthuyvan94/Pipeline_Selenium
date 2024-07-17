using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Remotion.FunctionalProgramming;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.Settings.CustomField
{
    public partial class CustomFieldModal
    {
        public void WaitForModalDisplay()
        {
            Title_lbl.WaitForElementIsVisible(5);
        }

        public void ClickSaveButton()
        {
            Save_btn.Click();
        }

        public void CreateNewCustomField(CustomFieldData data, string LoadingGrid_xpath)
        {
            ExtentReportsHelper.LogInformation(null, "<b>***Create new Custom Field.***</b>");
            EnterDataForCustomField(data);
            Save_btn.Click();
            WaitingLoadingGifByXpath(LoadingGrid_xpath);
        }

        public CustomFieldModal EnterDataForCustomField(CustomFieldData data)
        {
            Title_Txt.SetText(data.Title + Keys.Tab);
            Description_Txt.SetText(data.Description + Keys.Tab);
            Tag_Txt.SetText(data.Tag + Keys.Tab);
            FieldType_Dll.SelectItem(data.FieldType);
            IsDefaultTrue(data.Default);
            return this;
        }

        public void CloseModal()
        {
            CloseModal_btn.Click();
            Title_lbl.WaitForElementIsInVisible(10);
        }

        protected CustomFieldModal IsDefaultTrue(bool IsDefaultTrue = true)
        {
            var items = FindElementHelper.FindElements(FindType.XPath, defaultField_Xpath);
            IList<Button> defaultBtn = CommonHelper.CreateList<Button>(items);
            if (IsDefaultTrue)
                defaultBtn[0].JavaScriptClick();
            else
                defaultBtn[1].JavaScriptClick();
            return this;
        }

        public void UpdateCustomField(IGrid grid, string LoadingGrid_xpath, string oldTitle, CustomFieldData newData)
        {
            ExtentReportsHelper.LogInformation(null, "<b>***Update the Custom Field.***</b>");
            grid.ClickEditItemInGrid("Title", oldTitle);
            WaitingLoadingGifByXpath(LoadingGrid_xpath);
            TitleEdit_Txt.SetText(newData.Title);
            DescriptionEdit_Txt.SetText(newData.Description);
            TagEdit_Txt.SetText(newData.Tag);
            FieldTypeEdit_Dll.SelectItemByValueOrIndex(newData.FieldType,1);
            DefaultEdit_Chk.SetCheck(newData.Default);
            Update_Btn.Click();
            WaitingLoadingGifByXpath(LoadingGrid_xpath);
        }

        public void FilterAllDefaultCustomField(IGrid grid)
        {
            DefaultHeader_Chk.Check();
            FilterPanel_Btn.JavaScriptClick();
            // Wait until the panel display
            Button item = new Button(FindType.XPath, "//*[contains(@id,'rfltMenu_detached')]//a[starts-with(.,'EqualTo')]");
            // Find item on panel and click
            item.JavaScriptClick();
            grid.WaitGridLoad();
        }

        public void ClearFilterDefaultCustomField(IGrid grid)
        {
            DefaultHeader_Chk.UnCheck();
            grid.WaitGridLoad();
            FilterPanel_Btn.JavaScriptClick();
            grid.WaitGridLoad();
            // Wait until the panel display
            Button item = new Button(FindType.XPath, "//*[contains(@id,'rfltMenu_detached')]//a[starts-with(.,'EqualTo')]");
            // Find item on panel and click
            item.JavaScriptClick();
            grid.WaitGridLoad();
        }

        public IList<CustomFieldData> GetAllDefaultCustomField(IGrid grid)
        {
            FilterAllDefaultCustomField(grid);
            IList<string> items = new List<string>();
            IList<CustomFieldData> itemNames = new List<CustomFieldData>();
            string Xp_DefaultItems = "//table[contains(@id,'rgCustomFields')]/tbody/tr[.//input[contains(@id,'IsRequired') and @checked='checked']]/td[2]/span";
            string Xp_FieldType = "//table[contains(@id,'rgCustomFields')]/tbody/tr[.//input[contains(@id,'IsRequired') and @checked='checked']]/td[5]/span";
            var names = FindElementHelper.FindElements(FindType.XPath, Xp_DefaultItems);
            var types = FindElementHelper.FindElements(FindType.XPath, Xp_FieldType);
            
            for (int i = 0; i < names.Count; i++)
            {
                try
                {
                    itemNames.Add(new CustomFieldData
                    {
                        Title = names[i].Text,
                        FieldType = types[i].Text
                    });
                    items.Add($"Title is <font color='green'><b>{names[i].Text}</b></font> with Type <font color='green'><b>{types[i].Text}</b></font>.");
                } catch (Exception)
                {
                    ExtentReportsHelper.LogWarning($"Default custom fields could not be verified - text element may not exist");
                }
            }
            try
            {
                CommonHelper.MoveToElement(names.Last());
            } catch (Exception)
            {
                ExtentReportsHelper.LogWarning($"Default custom field (during move_to_element) may not exist");
            }

            ExtentReportsHelper.LogInformation($"We have {itemNames.Count} default items. {CommonHelper.CastListToString(items)}");
           
            System.Threading.Thread.Sleep(5000);
            ClearFilterDefaultCustomField(grid);
            return itemNames;
        }

        public void DragAndDrop(IGrid grid, string XpathLoading)
        {
            grid.FilterByColumn("Title", GridFilterOperator.NoFilter, "");
            WaitingLoadingGifByXpath(XpathLoading, 2000);
            string dragItem = "//table[contains(@id,'rgCustomFields')]/tbody/tr[.//input[contains(@id,'IsRequired')]]/td[1]/input";
            var totalItems = FindElementHelper.FindElements(FindType.XPath, dragItem);
            var toEle = totalItems.First();
            var fromEle = totalItems.Last();
            CommonHelper.DragAndDrop(fromEle, toEle, () => { WaitingLoadingGifByXpath(XpathLoading, 2000); });
        }

        public IList<CustomFieldData> GetAllCustomField(IGrid grid)
        {
            ExtentReportsHelper.LogInformation("Get all Custom Fields on Setting Page");
            IList<CustomFieldData> items = new List<CustomFieldData>();
            string xpath = "//table[contains(@id,'rgCustomFields')]/tbody/tr/td[{0}]/span";

            int.TryParse(FindElementHelper.FindElement(FindType.XPath, "//*[contains(@id,'rgCustomField')]/tfoot/tr/td/table/tbody/tr/td/div[5]/strong[2]").Text, out int totalPage);
            for (int currentPage = 1; currentPage <= totalPage; currentPage++)
            {
                var titles = FindElementHelper.FindElements(FindType.XPath, string.Format(xpath, "2"));
                var descriptions = FindElementHelper.FindElements(FindType.XPath, string.Format(xpath, "3"));
                var tags = FindElementHelper.FindElements(FindType.XPath, string.Format(xpath, "4"));
                var fieldTypes = FindElementHelper.FindElements(FindType.XPath, string.Format(xpath, "5"));
                var defaults = FindElementHelper.FindElements(FindType.XPath, string.Format(xpath + "/input", "6"));

                for (int i = 0; i < titles.Count; i++)
                {
                    items.Add(new CustomFieldData
                    {
                        Title = titles[i].Text,
                        Description = descriptions[i].Text,
                        Tag = tags[i].Text,
                        FieldType = fieldTypes[i].Text,
                        Default = defaults[i].Selected
                    });
                }
                if (currentPage != totalPage)
                    grid.NavigateToPage(currentPage + 1);
            }

            return items;
        }
    }
}
