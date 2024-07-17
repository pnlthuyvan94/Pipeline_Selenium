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
    public class StyleImportGrid : IGrid
    {
        private SpecificControls WrappedControl { get; set; }

        private string _xpathLoadingGrid { get; set; }

        public StyleImportGrid(FindType fintype, string valueToFind, string xpathGridLoad)
        {
            WrappedControl = new SpecificControls(fintype, valueToFind);
            _xpathLoadingGrid = xpathGridLoad;
        }
        public StyleImportGrid(Row row, string xpathGridLoad)
        {
            WrappedControl = new SpecificControls(row);
            _xpathLoadingGrid = xpathGridLoad;
        }

        protected int TotalColumns
        {
            get { return CellsHeader.Count; }
        }

        protected int TotalRows
        {
            get { return WrappedControl.FindElements(FindType.XPath, "./tbody/tr[position()>1]").Count; }
        }


        protected SpecificControls RowHeader
        {
            get { return new SpecificControls(WrappedControl.FindElement(FindType.XPath, "./tbody/tr[@class='GridViewHeaderStyle']")); }
        }

        protected IReadOnlyCollection<IWebElement> CellsHeader
        {
            get { return WrappedControl.FindElements(FindType.XPath, "./tbody/tr[@class='GridViewHeaderStyle']/th"); }
        }

        public int GetColumnHeaderIndexByName(string columnName)
        {
            foreach (var item in CellsHeader)
            {
                if (item.Text == columnName)
                {
                    return CellsHeader.ToList().IndexOf(item);
                }
            }
            return -1;
        }

        protected SpecificControls GetRowItem(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_gvRules']/tbody/tr/td", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                var item = WrappedControl.FindElement(FindType.XPath, "//tbody/tr[./td[position()=" + iColumn + " and (normalize-space(text())='" + valueToFind + "' or ./*[text()='" + valueToFind + "'])]]", 5);
                return ((item != null) ? new SpecificControls(item) : null);
            }
        }


        /// <summary>
        /// Find Item In Grid By Column In Current Page 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="grids"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public virtual bool IsItemOnCurrentPage(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_gvRules']/tbody/tr/td", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return false;
            }
            else
            {

                var isExist = WrappedControl.FindElement(FindType.XPath, "//tbody/tr/td[position()=" + iColumn + " and (normalize-space(text())='" + valueToFind + "' or ./*[text()='" + valueToFind + "'])]", 5);
                return ((isExist != null) ? true : false);
            }
        }

        /// <summary>
        /// Get Item In Grid By Column In Current Page 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public virtual IWebElement GetItemOnCurrentPage(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_gvRules']/tbody/tr/td", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                return WrappedControl.FindElement(FindType.XPath, "//tbody/tr/td[position()=" + iColumn + " and (normalize-space(text())='" + valueToFind + "' or ./*[text()='" + valueToFind + "'])]", 5);
            }
        }

        /// <summary>
        /// Get Item In Grid By Column In Current Page by index. the row and column start from 0
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public virtual IWebElement GetItemOnCurrentPage(int columnIndex, int rowIndex)
        {
            int iColumn = columnIndex + 1;
            int iRow = rowIndex + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_gvRules']/tbody/tr/td", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                return WrappedControl.FindElement(FindType.XPath, "//tbody/tr[" + iRow + " ]/td[" + iColumn + " ]", 5);
            }
        }

        /// <summary>
        /// Get Item In Grid By Column In Current Page by Column index. Column start from 0
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public virtual IWebElement GetItemOnCurrentPage(int columnIndex, string valueToFind)
        {
            int iColumn = columnIndex + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_gvRules']/tbody/tr/td", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                return WrappedControl.FindElement(FindType.XPath, "//tbody/tr/td[position()=" + iColumn + " and (normalize-space(text())='" + valueToFind + "' or ./*[text()='" + valueToFind + "'])]", 5);
            }
        }


        /// <summary>
        /// Click Item In Grid Current Page 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueToFind"></param>
        public virtual void ClickItemInGrid(string columnName, string valueToFind)
        {
            var _control = GetItemOnCurrentPage(columnName, valueToFind);
            if (_control == null)
                throw new NoSuchElementException("The item is not existed on current page.");
            SpecificControls controls = new SpecificControls(_control);
            controls.Click();
            BasePage.PageLoad();
        }

        /// <summary>
        /// Click Item In Grid Current Page by index. Column and Row start from 0
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        public virtual void ClickItemInGrid(int columnIndex, int rowIndex)
        {
            var _control = GetItemOnCurrentPage(columnIndex, rowIndex);
            if (_control == null)
                throw new NoSuchElementException("The item is not existed on current page.");
            SpecificControls controls = new SpecificControls(_control);
            controls.Click();
            BasePage.PageLoad();
        }

        /// <summary>
        /// Delete Item In Grid - Current Page 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="grids"></param>
        /// <param name="valueToFind"></param>
        public virtual void ClickDeleteItemInGrid(string columnName, string valueToFind)
        {
            SpecificControls item = GetRowItem(columnName, valueToFind);

            if (item == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not exist on current page.", valueToFind));
            SpecificControls del = new SpecificControls(item.FindElement(FindType.XPath, "//td/input[@title='Delete']"));
            if (del == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not contains Delete function.", valueToFind));
            del.JavaScriptClick();
        }

        /// <summary>
        /// Click delete 1st item on current page
        /// </summary>
        public virtual void ClickDeleteFirstItem()
        {
            var isNoRecord = FindElementHelper.Instance().FindElement(FindType.XPath, ".//td/input[@title='Delete']", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                throw new NoSuchElementException("No records to display.");
            }
            else
            {
                SpecificControls item = new SpecificControls(WrappedControl.FindElement(FindType.XPath, "./tbody/tr[1]//*/input[@title='Delete']"));
                if (item == null)
                    throw new NoSuchElementException("The 1st item do not contains Delete function.");
                item.JavaScriptClick();
            }

        }

        /// <summary>
        /// Get total pages on current filter
        /// </summary>
        public virtual int GetTotalPages
        {
            get
            {
                var total = WrappedControl.FindElement(FindType.Id, "ctl00_CPH_Content_lblTotalNumberOfPages");
                int.TryParse(total.Text.ToString().Trim(), out int iTotal);
                return iTotal;
            }
        }

        /// <summary>
        /// Get current page number
        /// </summary>
        public virtual int GetCurrentPageNumber
        {
            get
            {
                var currentPage = WrappedControl.FindElement(FindType.Id, "ctl00_CPH_Content_txtGoToPage");
                int.TryParse(currentPage.GetAttribute("value").ToString().Trim(), out int page);
                return page;
            }
        }

        public int GetPageSize => throw new NotImplementedException();

        public int GetTotalItems => throw new NotImplementedException();

        /// <summary>
        /// Wait Grid loading until complete. Be careful, it may make your app freeze!
        /// </summary>
        public virtual void WaitGridLoad(bool useLoadingElementXPath = false)
        {
            if (useLoadingElementXPath && !string.IsNullOrEmpty(_xpathLoadingGrid))
            {
                int iTime = 0;
                while (FindElementHelper.Instance().FindElement(FindType.XPath, _xpathLoadingGrid, 1) != null)
                {
                    // Wait until timeout
                    System.Threading.Thread.Sleep(500);
                    iTime++;
                    if (iTime > 120)
                        throw new TimeoutException("The grid timed out loading after 120s.");
                }
            }

            BasePage.JQueryLoad();
        }

        public void NavigateToPage(int pageNumber)
        {
            throw new NotImplementedException();
        }

        public void ChangePageSize(int sizeNumber)
        {
            MoveGridToCenterScreen();

            DropdownList pageSize_ddl = new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlPageSize']");

            if (pageSize_ddl.IsDisplayed() is true)
            {
                if (pageSize_ddl.SelectedValue == sizeNumber.ToString())
                {
                    ExtentReportsHelper.LogInformation(null, $"<font color='yellow'>The current page size is {sizeNumber}. Don't need to filter again.</font>");
                    return;
                }

                try
                {
                    pageSize_ddl.SelectItemByValue(sizeNumber.ToString());
                }
                catch (Exception exception)
                {
                    ExtentReportsHelper.LogWarning($"Could not locate item element {sizeNumber} in Dropdown List: {exception.Message}");
                }
            }
            else
                ExtentReportsHelper.LogWarning($"font color='yellow'>Can't find page size drop down list to select.</font>");
        }

        public void ClickEditFirstItem()
        {
            throw new NotImplementedException();
        }

        public void ClickEditItemInGrid(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }


        public void FilterByColumn(string columnName, GridFilterOperator grids, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public void ClickItemInGridWithTextContains(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public bool IsItemWithTextContainsOnCurrentPage(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_gvRules']/tbody/tr/td", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
                return false;
            else
            {
                var isExist = WrappedControl.FindElement(FindType.XPath, $"//tbody/tr/td[position()={iColumn} and (normalize-space(text())='{valueToFind}' or ./*[contains(text(),'{valueToFind}')])]", 5);
                return ((isExist != null) ? true : false);
            }
        }

        public void ClickEditItemInGridWithTextContains(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public IWebElement GetItemWithTextContainsOnCurrentPage(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }
        public void MoveGridToCenterScreen()
        {
            CommonHelper.MoveToElement(WrappedControl, true);
        }

        public bool IsLockedRoleOnItemInGrid(string valueToFind)
        {
            throw new NotImplementedException();
        }

        public IWebElement GetItemByRowAndColumn(string columnName, int rowIndex)
        {
            throw new NotImplementedException();
        }

        public void ClickEditItemInGridButton(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public virtual void IsDeleteFirstItem(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }
        public virtual void IsEditFirstItem(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public void IsColumnHeaderIndexByName(string columnName)
        {
            throw new NotImplementedException();
        }

        public void IsDeleteFirstItem()
        {
            throw new NotImplementedException();
        }

        public void IsEditFirstItem()
        {
            throw new NotImplementedException();
        }
        public void SelectItemOnGridViaCheckbox(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }
    }
}
