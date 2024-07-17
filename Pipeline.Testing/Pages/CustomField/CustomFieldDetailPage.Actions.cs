using OpenQA.Selenium;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Pages.CustomField
{
    public partial class CustomFieldDetailPage
    {
        // Textbox: span/input
        // Number: span/input
        // TextArea: span/textarea
        // Checkbox: span/[span/input]
        // Date: span/div
        // Static: span/span

        /// <summary>
        /// Return exact the xpath of item with Name & Type
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string GetXpath(CustomFieldData item)
        {
            string xpath = "//section[@class='information']/span/section[{0}[starts-with(text(),'{1}')] and {2}]";
            switch (item.FieldType)
            {
                case "Text Area":
                    return string.Format(xpath, "span", item.Title, "textarea");
                case "Date":
                    return string.Format(xpath, "span", item.Title, "div");
                case "Checkbox":
                    return string.Format(xpath, "span", item.Title, "span/input");
                case "Static":
                    return string.Format(xpath, "span", "Static", $"span[starts-with(text(),'{item.Title}')]");
                default: // Text + Number
                    return string.Format(xpath, "span", item.Title, "input");
            }
        }

        /// <summary>
        /// Get list custom fields on the Custom Fields Detail page
        /// </summary>
        /// <returns></returns>
        public IList<CustomFieldData> GetCurrentItems()
        {
            ExtentReportsHelper.LogInformation("Get current Custom Fields on this page.");
            var items = new List<CustomFieldData>();
            // Get list control
            var controls = FindElementHelper.FindElements(FindType.XPath, "//section[@class='information']/span/section");
            foreach (var item in controls)
            {
                string typeName = string.Empty;
                string value = string.Empty;
                var valueField = item.FindElements(By.XPath("./textarea"));
                if (valueField.Count == 0)
                {
                    valueField = item.FindElements(By.XPath("./div/table//input"));
                    if (valueField.Count == 0)
                    {
                        valueField = item.FindElements(By.XPath("./span/input"));
                        if (valueField.Count == 0)
                        {
                            valueField = item.FindElements(By.XPath("./input[@type='number']"));
                            if (valueField.Count == 0)
                            {
                                valueField = item.FindElements(By.XPath("./input[@type='text']"));
                                if (valueField.Count == 0)
                                {
                                    valueField = item.FindElements(By.XPath("./span[2]"));
                                    typeName = "Static";
                                }
                                else
                                    typeName = "Text";
                            }
                            else
                                typeName = "Number";
                        }
                        else
                            typeName = "Checkbox";
                    }
                    else
                        typeName = "Date";
                }
                else
                    typeName = "Text Area";

                string name = item.FindElement(By.XPath("./span[1]")).Text;
                name = name.Remove(name.Length - 1);
                value = valueField.First().GetAttribute("value");
                if (string.IsNullOrEmpty(value))
                    value = valueField.First().Text;
                if (name.Equals("Static"))
                    name = typeName;
                if (typeName.Equals("Checkbox"))
                    value = valueField.First().GetAttribute("checked");

                items.Add(new CustomFieldData()
                {
                    Title = name,
                    FieldType = typeName,
                    Value = value
                });
            }
            return items;
        }

        /// <summary>
        /// Click add button to show add custom field modal
        /// </summary>
        public void ShowAddCustomFieldModal()
        {
            Add_Btn.Click();
            WaitingLoadingGifByXpath(addLoadingGif_Xpath);
        }

        public bool IsModalDisplayedWithTitle(string title)
        {
            // Wait the title is displayed
            ModalTitle_lbl.WaitForElementIsVisible(5);
            return ModalTitle_lbl.GetText().Equals(title);
        }

        /// <summary>
        /// Select custom fields on Add Custom Field modal
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public CustomFieldDetailPage SelectCustomFieldsOnModal(IList<CustomFieldData> items)
        {
            if (ModalTitle_lbl.IsDisplayed())
            {
                foreach (var item in items)
                {
                    CheckBox x = new CheckBox(FindType.XPath, string.Format(listCustomField_Xpath, item.Title, item.Description));
                    if (x.IsNull())
                    {
                        ExtentReportsHelper.LogFail($"The Custom Field Title <font color='green'><b>{item.Title} - {item.Description}</b></font> with type <font color='green'><b>{item.FieldType}</b></font> is not display on List modal.");
                    }
                    else
                        x.Check();
                }
                return this;
            }
            throw new NotFoundException("The 'Add Modal' is not displayed");
        }

        public void InsertTheSelectedCustomFieldToThisPage()
        {
            // Click insert
            Insert_Btn.Click();

            // Wait cancel load
            WaitingLoadingGifByXpath(removeLoadingGif_Xpath);

            // Verify the modal is hidden
            ModalTitle_lbl.WaitForElementIsInVisible(10);
        }

        public void Save()
        {
            // Save click
            Save_Btn.Click();

            // loading
            WaitingLoadingGifByXpath(removeLoadingGif_Xpath);
        }

        public void CanceleAction()
        {
            // cancel click
            Cancel_Btn.Click();

            // Wait canncel loading
            WaitingLoadingGifByXpath(removeLoadingGif_Xpath);
        }

        public CustomFieldDetailPage RemovingCustomField(IList<CustomFieldData> items)
        {
            // Remove
            Remove_Btn.Click();

            // Loading 
            WaitingLoadingGifByXpath(removeLoadingGif_Xpath);

            // remove item
            foreach (var item in items)
            {
                var newXpath = GetXpath(item) + "/a";
                var control = new SpecificControls(FindType.XPath, newXpath);
                if (control.IsExisted())
                {
                    control.Click();
                    control.WaitForElementIsInVisible(5);
                    //TODO: after delete, check the pageload method
                    PageLoad();
                }
                else
                    ExtentReportsHelper.LogFail($"The Custom Field Title <font color='green'><b>{item.Title}</b></font> with type <font color='green'><b>{item.FieldType}</b></font> is not display on details page.");
            }

            return this;
        }

        public CustomFieldDetailPage EnterValueToField(string name, string value)
        {
            string xpath = $@"//section[@class='information']/span/section[span[text()='{name}:']]//table//input[1] |
                              //section[@class='information']/span/section[span[text()='{name}:']]/textarea | 
                              //section[@class='information']/span/section[span[text()='{name}:']]/input[@type='text'] |
                              //section[@class='information']/span/section[span[text()='{name}:']]/input[@type='number']";
            Textbox control = new Textbox(FindType.XPath, xpath);
            control.SetText(value);
            return this;
        }

        public CustomFieldDetailPage EnterValueToField(string name, bool isCheck)
        {
            string xpath = $@"//section[@class='information']/span/section[span[text()='{name}:']]/span/input";
            CheckBox control = new CheckBox(FindType.XPath, xpath);
            control.SetCheck(isCheck);
            return this;
        }

        public void SetHouseCustomFieldCheckAll(bool check)
        {
            if(check)
            {
                ModalCheckAll_ckb.Check();
            }    
            else
            {
                ModalCheckAll_ckb.UnCheck();
            }
        }

        public void ClickHouseRemoveCustomField()
        {
            RemoveCustomField_Btn.Click();
            WaitingLoadingGifByXpath("//div[@id = 'ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbRemove']/div[1]");
        }
        public void DeleteCustomeField(string CustomFieldName)
        {
            string private_XPath = $"//span[@id = 'ctl00_CPH_Content_ctl00_CPH_Content_PlaceHolder1Panel']//span[contains(.,'{CustomFieldName}')]/following-sibling::a";
            try
            {
                Button XButton = new Button(FindType.XPath, private_XPath);
                XButton.Click();
            }
            catch(StaleElementReferenceException)
            {
                Button XButton = new Button(FindType.XPath, private_XPath);
                System.Threading.Thread.Sleep(1000);
                XButton.Click();
            }
            
        }


    }
}
