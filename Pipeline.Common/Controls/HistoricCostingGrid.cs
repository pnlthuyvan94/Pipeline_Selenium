using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Common.Controls
{
    public class HistoricCostingGrid : IGrid
    {
        private SpecificControls WrappedControl { get; set; }
        private string _xpathLoadingGrid { get; set; }
        public HistoricCostingGrid(FindType fintype, string valueToFind)
        {
            WrappedControl = new SpecificControls(fintype, valueToFind);
        }
        public HistoricCostingGrid(Row row, string xpathGridLoad)
        {
            WrappedControl = new SpecificControls(row);
            _xpathLoadingGrid = xpathGridLoad;
        }
        protected IReadOnlyCollection<IWebElement> CellsHeader
        {
            get { return WrappedControl.GetWrappedControl().FindElements(By.XPath("./thead/tr[2]/th")); }
        }
        public int GetCurrentPageNumber => throw new NotImplementedException();

        public int GetPageSize => throw new NotImplementedException();

        public int GetTotalPages => throw new NotImplementedException();

        public int GetTotalItems => throw new NotImplementedException();

        public void ChangePageSize(int sizeNumber)
        {
            throw new NotImplementedException();
        }

        public void ClickDeleteFirstItem()
        {
            throw new NotImplementedException();
        }

        public virtual void ClickDeleteItemInGrid(string columnName, string valueToFind)
        {
            SpecificControls item = GetRowItemWithTextContains(columnName, valueToFind);

            if (item.IsNull())
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not exist on current page.", valueToFind));

            CommonHelper.MoveToElementWithoutCapture(item);

            var deleteButton = item.FindElement(FindType.XPath, "./td/input[@title='Delete' or contains(@src,'delete.png')]");
            if (deleteButton is null)
                deleteButton = item.FindElement(FindType.XPath, ".//*[@title='Delete' or contains(@src,'delete.png')]");

            CommonHelper.MoveToElementWithoutCaptureAndCenter(deleteButton);

            SpecificControls del = new SpecificControls(deleteButton);
            if (del.IsNull())
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not contains Delete function.", valueToFind));

            del.Click();
            //del.JavaScriptClick();
            System.Threading.Thread.Sleep(667);
        }
        protected SpecificControls GetRowItem(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td/div", 2);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                CommonHelper.ScrollToBeginOfPage();

                var item = WrappedControl.FindElement(FindType.XPath, $@"./tbody/tr[./td[position()={iColumn} 
                                                and (normalize-space(text())='{valueToFind}' or ./*[text()='{valueToFind}'])]]", 8);

                CommonHelper.MoveToElement(item, true);

                return ((item != null) ? new SpecificControls(item) : null);
            }
        }
        public void ClickEditFirstItem()
        {
            throw new NotImplementedException();
        }

        public void ClickEditItemInGrid(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public void ClickEditItemInGridWithTextContains(string columnName, string valueToFind)
        {
            SpecificControls item = GetRowItemWithTextContains(columnName, valueToFind);

            if (item == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not exist on current page.", valueToFind));

            CommonHelper.MoveToElement(item, true);

            var editButton = item.FindElement(FindType.XPath, "./td/a[./img[@alt='Edit']]");
            if (editButton is null)
                editButton = item.FindElement(FindType.XPath, ".//*[@alt='Edit']");
            SpecificControls edit = new SpecificControls(editButton);
            if (edit.GetWrappedControl() == null)
                edit = new SpecificControls(item.FindElement(FindType.XPath, "//*[@alt='Edit']"));
            if (edit.GetWrappedControl() == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not contains Edit function.", valueToFind));

            edit.Click();
            //edit.JavaScriptClick();
            WaitGridLoad();
            System.Threading.Thread.Sleep(667);
        }
        protected SpecificControls GetRowItemWithTextContains(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td/div", 2);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                CommonHelper.ScrollToBeginOfPage();

                var item = WrappedControl.FindElement(FindType.XPath, $@"./tbody/tr[./td[position()={iColumn} 
                                                and (contains(normalize-space(text()),'{valueToFind}') or ./*[contains(text(),'{valueToFind}')])]]", 8);

                CommonHelper.MoveToElement(item, true);

                return ((item != null) ? new SpecificControls(item) : null);
            }
        }
        public void ClickItemInGrid(int columnIndex, int rowIndex)
        {
            throw new NotImplementedException();
        }

        public virtual void ClickItemInGrid(string columnName, string valueToFind)
        {
            var _control = GetItemOnCurrentPage(columnName, valueToFind);
            if (_control == null)
                throw new NoSuchElementException($"The item with name {columnName} and value {valueToFind} is not existed on current page.");

            CommonHelper.MoveToElement(_control, true);

            var temp = _control.FindElement(By.TagName("a"));
            SpecificControls controls;
            if (temp is null)
                controls = new SpecificControls(_control);
            else
                controls = new SpecificControls(temp);

            controls.WaitForElementIsVisible(10);
            controls.Click(false);
            WaitGridLoad();
            System.Threading.Thread.Sleep(500);
        }

        public void ClickItemInGridWithTextContains(string columnName, string valueToFind)
        {
            var _control = GetItemOnCurrentPageWithTextContains(columnName, valueToFind);
            if (_control == null)
                throw new NoSuchElementException($"The item with name {columnName} and value {valueToFind} is not existed on current page.");

            CommonHelper.MoveToElement(_control, true);

            var temp = _control.FindElement(By.TagName("a"));
            SpecificControls controls;
            if (temp is null)
                controls = new SpecificControls(_control);
            else
                controls = new SpecificControls(temp);

            controls.WaitForElementIsVisible(10);
            controls.Click();
            WaitGridLoad();
        }
        public bool IsItemWithTextContainsOnCurrentPage(string columnName, string valueToFind)
        {
            var item = GetItemOnCurrentPageWithTextContains(columnName, valueToFind);
            if (item is null)
            {
                ExtentReportsHelper.LogInformation($"The item <font color='green'><b>{valueToFind}</b></font> on the column <font color='green'><b>{columnName}</b></font> is <b>NOT</b> displayed on grid.");
                return false;
            }
            else
            {
                CommonHelper.MoveToElement(item, true);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"The item <font color='green'><b>{valueToFind}</b></font> on the column <font color='green'><b>{columnName}</b></font> is displayed on grid.");
                return true;
            }
        }
        public virtual IWebElement GetItemOnCurrentPageWithTextContains(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td/div", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                var item = WrappedControl.FindElement(FindType.XPath, $"./tbody/tr/td[position()={iColumn} and (normalize-space(text())='{valueToFind}' or ./*[contains(text(),'{valueToFind}')])]", 5);
                CommonHelper.MoveToElement(item, true);
                return item;
            }
        }
        public void FilterByColumn(string columnName, GridFilterOperator grids, string valueToFind)
        {
            throw new NotImplementedException();
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

        public IWebElement GetItemByRowAndColumn(string columnName, int rowIndex)
        {
            throw new NotImplementedException();
        }

        public IWebElement GetItemOnCurrentPage(int columnIndex, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public IWebElement GetItemOnCurrentPage(int columnIndex, int rowIndex)
        {
            throw new NotImplementedException();
        }

        public IWebElement GetItemOnCurrentPage(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public IWebElement GetItemWithTextContainsOnCurrentPage(string columnName, string valueToFind)
        {
            throw new NotImplementedException();
        }

        public virtual bool IsItemOnCurrentPage(string columnName, string valueToFind)
        {
            WaitGridLoad();

            System.Threading.Thread.Sleep(1000);

            MoveGridToCenterScreen();

            WrappedControl.RefreshWrappedControl();

            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            IWebElement isNoRecord = null;
            try
            {
                isNoRecord = WrappedControl.GetWrappedControl().FindElement(By.XPath("./tbody/tr/td/div"));
            }
            catch (Exception ex)
            {
                //Do nothing, there must be elements in the grid
            }

            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return false;
            }
            else
            {
                System.Threading.Thread.Sleep(1500);

                IWebElement isExist = null;
                try
                {
                    isExist = WrappedControl.GetWrappedControl().FindElement(By.XPath("(./tbody/tr/td[position()=" + iColumn + " and (normalize-space(text())='" + valueToFind + "' or ./*[text()='" + valueToFind + "'])] | //tbody/tr/td[position()=" + iColumn + " and (normalize-space(text())='" + valueToFind + "' or ./*[text()='" + valueToFind + "'])])[1]"));
                }
                catch (Exception ex)
                { //item doesn't exist on the grid
                }

                if (isExist != null)
                {
                    CommonHelper.MoveToElement(isExist, true);
                    ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(isExist), $"The item <font color='green'><b>{valueToFind}</b></font> on the column <font color='green'><b>{columnName}</b></font> is displayed on grid.");
                    return true;
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"The item <font color='green'><b>{valueToFind}</b></font> on the column <font color='green'><b>{columnName}</b></font> is <b>NOT</b> displayed on grid.");
                    return false;
                }
            }
        }

        public bool IsLockedRoleOnItemInGrid(string valueToFind)
        {
            throw new NotImplementedException();
        }

        public void MoveGridToCenterScreen()
        {
            CommonHelper.MoveToElementWithoutCapture(WrappedControl);
        }

        public void NavigateToPage(int pageNumber)
        {
            throw new NotImplementedException();
        }

        public virtual void WaitGridLoad(bool useLoadingElementXPath = false)
        {
            if (useLoadingElementXPath && !string.IsNullOrEmpty(_xpathLoadingGrid))
            {
                Label loading = new Label(FindType.XPath, _xpathLoadingGrid);
                if (loading.WaitUntilExist(1))
                    if (!loading.WaitForElementIsInVisible(20, false))
                        ExtentReportsHelper.LogWarning("The loading gif is loading over 120s.");
            }

            BasePage.JQueryLoad();
            System.Threading.Thread.Sleep(500);
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
