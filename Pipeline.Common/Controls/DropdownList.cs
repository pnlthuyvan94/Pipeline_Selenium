using LinqToExcel;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Common.Controls
{
    public class DropdownList : BaseControl
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public DropdownList(IWebElement webElement) : base(webElement) { }

        public DropdownList(Row row) : base(row)
        {
            Select = new SelectElement(GetWrappedControl());
        }

        public DropdownList(FindType findType, string valueToFind) : base(findType, valueToFind)
        {
            Select = new SelectElement(GetWrappedControl());
        }

        public DropdownList(FindType findType, string valueToFind, int timeoutSeconds) : base(findType, valueToFind, timeoutSeconds)
        {
            Select = new SelectElement(GetWrappedControl());
        }

        private SelectElement Select { get; set; }

        /// <summary>
        /// Select an item on Dropdown-List by text displayed
        /// </summary>
        /// <param name="itemName"></param>
        /// <param name="partialMatch"></param>
        public void SelectItem(string itemName, bool partialMatch = false, bool capture = true)
        {
            System.Threading.Thread.Sleep(667);
            CommonHelper.MoveToElement(Select.WrappedElement, true);
            Select.SelectByText(itemName, partialMatch);
            if (capture)
                CaptureAndLog(string.Format(BaseConstants.SelectItemMessage, itemName));
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        public void SelectItems(params string[] itemNames)
        {
            if (!Select.IsMultiple)
                throw new NotSupportedException("The list does not support multi select.");
            foreach (var item in itemNames)
            {
                CommonHelper.MoveToElement(Select.WrappedElement, true);
                Select.SelectByText(item, true);
                CaptureStepAndLogInfo(SelectedItem, string.Format(BaseConstants.SelectItemMessage, item));
                BasePage.JQueryLoad();
                System.Threading.Thread.Sleep(667);
            }
        }


        /// <summary>
        /// Select an item on Dropdown-List by Index
        /// </summary>
        /// <param name="itemName"></param>
        public void SelectItem(int itemName)
        {
            CommonHelper.MoveToElement(Select.WrappedElement, true);
            Select.SelectByIndex(itemName);
            CaptureAndLog(string.Format(BaseConstants.SelectItemMessage, itemName));
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// Select an item on Dropdown-List by Value(ID)
        /// </summary>
        /// <param name="itemValue"></param>
        public void SelectItemByValue(string itemValue)
        {
            CommonHelper.MoveToElement(Select.WrappedElement, true);
            Select.SelectByValue(itemValue);
            CaptureAndLog(string.Format(BaseConstants.SelectItemMessage, itemValue));
            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(667);
        }

        /// <summary>
        /// Get selected item name 
        /// </summary>
        public string SelectedItemName
        {
            get { return Select.SelectedOption.Text; }
        }

        public List<string> AllSelectedItemName
        {
            get
            {
                return Select.AllSelectedOptions.Select(q => q.Text).ToList();
            }
        }

        /// <summary>
        /// Get selected index
        /// </summary>
        public int SelectedIndex
        {
            get
            {
                int.TryParse(Select.SelectedOption.GetAttribute("index"), out int index);
                return index;
            }
        }

        /// <summary>
        /// Get selected value
        /// </summary>
        public string SelectedValue
        {
            get { return Select.SelectedOption.GetAttribute("value"); }
        }

        /// <summary>
        /// Get total items on dropdown-list
        /// </summary>
        public int GetTotalItems
        {
            get { return Select.Options.Count(); }
        }

        /// <summary>
        /// Get all items from dropdown-list
        /// </summary>
        public IList<IWebElement> GetItems
        {
            get { return Select.Options; }
        }

        /// <summary>
        /// Get Selected Item
        /// </summary>
        public IWebElement SelectedItem
        {
            get { return Select.SelectedOption; }
        }

        public bool IsItemInList(string itemname, string attribute = "")
        {
            foreach (var item in Select.Options)
            {
                if (item.GetAttribute(attribute) == itemname || item.Text == itemname)
                {
                    CaptureAndLog($"Item <font color='green'><b>{itemname}</b></font> is existed on this list.");
                    return true;
                }
            }
            CaptureAndLog($"Item <font color='green'><b>{itemname}</b></font> is NOT exist on this list.");
            return false;

        }

        public string SelectItemByIndexAndGetValue(int index, bool capture = true)
        {
            if(index<= Select.Options.Count)
            {                  
            Select.SelectByIndex(index);
                if (capture)
                    CaptureAndLog(string.Format(BaseConstants.SelectItemMessage, index));
            }
            return Select.SelectedOption.Text.ToString();
        }
        public string SelectItemByValueOrIndex(string data, int index)
        {
            if (!string.IsNullOrEmpty(data) && IsItemInList(data))
            {
                SelectItem(data, true,true);
                return data;
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Items <font color ='green'><b>{SelectItemByIndexAndGetValue(index)}</b ></font> is selected in options");
                return SelectItemByIndexAndGetValue(index);
            }
        }
    }
}
