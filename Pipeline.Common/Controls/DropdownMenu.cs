using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Common.Controls
{
    public class DropdownMenu : SpecificControls
    {

        public DropdownMenu(IWebElement control) : base(control)
        {
        }

        public DropdownMenu(FindType findType, string valueToFind) : base(findType, valueToFind) { }

        public DropdownMenu(Row row) : base(row)
        {
        }

        /// <summary>
        /// Select item on menu by name
        /// </summary>
        /// <param name="itemName"></param>
        public void SelectItem(string itemName, Boolean isOpenNewTab = false)
        {
            CommonHelper.MoveToCenterOfElement(this.GetWrappedControl(), true);

            if (!IsMenuDisplayed)
            {
                IWebElement parent_control = CommonHelper.find_element_weak(this.GetWrappedControl(), By.XPath("./../a"));
                
                //click_element can throw an exception here because the test will not proceed properly in that case
                CommonHelper.click_element(parent_control, false);
            }

            SpecificControls control = new SpecificControls(CommonHelper.find_element_weak(this.GetWrappedControl(), By.XPath(string.Format("./../ul/li/a[text()='{0}' or ./span[text()='{0}']]", itemName))));
            if (!CommonHelper.is_valid(control.GetWrappedControl())) 
                throw new ElementNotVisibleException(string.Format("Could not found element with name '{0}' on your Dropdown Menu", itemName));

            if (isOpenNewTab)
                CommonHelper.OpenLinkInNewTab(control.GetAttribute("href"));
            else
                CommonHelper.click_element(control.GetWrappedControl());
        }

        /// <summary>
        /// Select item on dropdown menu by index, the start number is 0
        /// </summary>
        /// <param name="itemIndex"></param>
        public void SelectItem(int itemIndex)
        {
            CommonHelper.MoveToCenterOfElement(this.GetWrappedControl(), true);

            if (!IsMenuDisplayed)
            {
                IWebElement parent_control = CommonHelper.find_element_weak(this.GetWrappedControl(), By.XPath("./../a"));
                
                //click_element can throw an exception here because the test will not proceed properly in that case
                CommonHelper.click_element(parent_control);
            }

            int iPosition = itemIndex + 1;
            SpecificControls controls = new SpecificControls(CommonHelper.find_element_weak(this.GetWrappedControl(), By.XPath(string.Format("./../ul/li/a[" + iPosition + "]"))));
            if (!CommonHelper.is_valid(controls.GetWrappedControl()))
                throw new ElementNotVisibleException(string.Format("Could not find visible element with index '{0}' on your Dropdown Menu", iPosition));

            CommonHelper.click_element(controls.GetWrappedControl());
        }

        public IList<string> GetListItemName
        {
            get
            {
                if (this is null)
                    return null;
                else
                {
                    List<IWebElement> _list = new List<IWebElement>();
                    var listItem = this.FindElements(FindType.XPath, "./../ul/li/a").ToList();
                    return CommonHelper.CastListControlsToListString(listItem);
                }
            }
        }

        private bool IsMenuDisplayed => CommonHelper.is_valid(CommonHelper.find_element_weak(this.GetWrappedControl(), By.XPath("./../ul/li")));
    }
}
