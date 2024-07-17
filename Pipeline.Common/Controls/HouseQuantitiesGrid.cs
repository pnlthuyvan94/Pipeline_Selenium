using OpenQA.Selenium;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;


namespace Pipeline.Common.Controls
{
    public class HouseQuantitiesGrid : IGrid
    {
        private SpecificControls WrappedControl { get; set; }

        public HouseQuantitiesGrid(FindType fintype, string valueToFind)
        {
            WrappedControl = new SpecificControls(fintype, valueToFind);
        }

        protected IReadOnlyCollection<IWebElement> CellsHeader
        {
            get { return WrappedControl.GetWrappedControl().FindElements(By.XPath("//thead/tr[2]/th")); }
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
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_gvRules']/tbody/tr/td", 2);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return false;
            }
            else
            {
                var isExist = WrappedControl.FindElement(FindType.XPath, $"(//tbody/tr/td[position()="+ iColumn+" and (contains(text(),'"+ valueToFind+ "') or ./*[contains(text(), '" + valueToFind + "')])])[1]", 5);
                if (isExist != null)
                    return ((isExist.Text.Contains(valueToFind)) ? true : false);
                else
                    return false;
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

        public bool IsItemWithTextContainsOnCurrentPage(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgProductsToHouses_ctl00']/tbody/tr/td", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
                return false;
            else
            {
                var isExist = WrappedControl.FindElement(FindType.XPath, $"//tbody/tr/td[position()={iColumn}]//*[normalize-space(text())='{valueToFind}'  or contains(text(),'{valueToFind}')]", 5);
                return ((isExist != null) ? true : false);
            }
        }

        public virtual void WaitGridLoad(bool useLoadingElementXPath = false) => throw new NotImplementedException();

        public void ClickEditItemInGridWithTextContains(string columnName, string valueToFind) => throw new NotImplementedException();

        public IWebElement GetItemWithTextContainsOnCurrentPage(string columnName, string valueToFind) => throw new NotImplementedException();

        public bool IsLockedRoleOnItemInGrid(string valueToFind) => throw new NotImplementedException();
        public void NavigateToPage(int pageNumber) => throw new NotImplementedException();

        public void ChangePageSize(int sizeNumber) => throw new NotImplementedException();

        public void ClickEditFirstItem() => throw new NotImplementedException();

        public void ClickEditItemInGrid(string columnName, string valueToFind) => throw new NotImplementedException();
        public virtual int GetTotalPages => throw new NotImplementedException();

        public virtual int GetCurrentPageNumber => throw new NotImplementedException();

        public int GetPageSize => throw new NotImplementedException();

        public virtual int GetTotalItems
        {
            get
            {
                WrappedControl.RefreshWrappedControl();
                var total = WrappedControl.FindElement(FindType.XPath, "./tfoot/tr/td/table/tbody/tr/td/div[5]/strong[1]");
                int.TryParse(total.Text.ToString().Trim(), out int iTotal);
                return iTotal;
            }
        }

        public virtual void ClickItemInGrid(string columnName, string valueToFind) => throw new NotImplementedException();

        public virtual void ClickItemInGrid(int columnIndex, int rowIndex) => throw new NotImplementedException();

        public virtual void ClickDeleteItemInGrid(string columnName, string valueToFind) => throw new NotImplementedException();

        public virtual void ClickDeleteFirstItem() => throw new NotImplementedException();

        public void ClickItemInGridWithTextContains(string columnName, string valueToFind) => throw new NotImplementedException();

        public void MoveGridToCenterScreen() => throw new NotImplementedException();

        public virtual void FilterByColumn(string columnName, GridFilterOperator filter_operator, string valueToFind) => throw new NotImplementedException();

        public IWebElement GetItemByRowAndColumn(string columnName, int rowIndex)
        {
            throw new NotImplementedException();
        }

        public void ClickEditItemInGridButton(string columnName, string valueToFind)
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
