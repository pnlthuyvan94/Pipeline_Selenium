using LinqToExcel;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Pipeline.Common.Controls
{
    public class Grid : IGrid
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private SpecificControls WrappedControl { get; set; }

        private string _xpathLoadingGrid { get; set; }

        public Grid(FindType findType, string valueToFind, string xpathGridLoad)
        {
            WrappedControl = new SpecificControls(findType, valueToFind);
            _xpathLoadingGrid = xpathGridLoad;
        }

        public Grid(Row row, string xpathGridLoad)
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
            get { return WrappedControl.GetWrappedControl().FindElements(By.XPath("./tbody/tr")).Count; }
        }

        // Gets the "$" totals at the bottom of the grid
        // May need to scroll to the bottom of the page before calling this function.
        public List<double> GridTotals
        {
            get
            {
                var wrappedControl = WrappedControl.GetWrappedControl();
                if (wrappedControl != null)
                {
                    var trElements = wrappedControl.FindElements(By.XPath("./tfoot/tr"));
                    List<double> tdContents = new List<double>();

                    foreach (var tr in trElements)
                    {
                        var tdElements = tr.FindElements(By.XPath("./td"));
                        foreach (var td in tdElements)
                        {
                            double value;
                            // Parse the innerHTML as double and add to the list
                            string innerHTMLNoCommas = td.GetAttribute("innerHTML").TrimStart(new char[] { '$' }).Replace(",", "");
                            if (double.TryParse(innerHTMLNoCommas, NumberStyles.Any, CultureInfo.InvariantCulture, out value))
                            {
                                tdContents.Add(value);
                            }
                        }
                    }
                    // Return the last three items
                    return tdContents;
                }
                return null;
            }
        }

        protected SpecificControls RowFilter
        {
            get { return new SpecificControls(WrappedControl.GetWrappedControl().FindElement(By.XPath("./thead/tr[@class = 'rgFilterRow']"))); }
        }

        protected SpecificControls RowHeader
        {
            get { return new SpecificControls(WrappedControl.GetWrappedControl().FindElement(By.XPath("./thead/tr[1]"))); }
        }

        protected IReadOnlyCollection<IWebElement> CellsHeader
        {
            get { return WrappedControl.GetWrappedControl().FindElements(By.XPath("./thead/tr[1]/th")); }
        }

        protected IReadOnlyCollection<IWebElement> CellsHeader2
        {
            get { return WrappedControl.GetWrappedControl().FindElements(By.XPath("./thead/tr[2]/th")); }
        }
        protected IReadOnlyCollection<IWebElement> ListOfCellsFilter
        {
            get { return WrappedControl.GetWrappedControl().FindElements(By.XPath("./thead/tr[2]/td")); }
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
            foreach (var item in CellsHeader2)
            {
                if (item.Text == columnName)
                {
                    return CellsHeader2.ToList().IndexOf(item);
                }
            }
            return -1;
        }

        public virtual void IsColumnHeaderIndexByName(string columnName)
        {
            var item = WrappedControl.GetWrappedControl().FindElement(By.XPath($"./thead/tr[1]/th/a[contains(text(),'{columnName}')]"));
                if (item.Text == columnName)
                {
                    ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(item), $"<font color='green'><b>The Columm {columnName} is displayed in Table Grid.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(item), $"<font color='red'><b>The Columm {columnName} is not display in Table Grid.</b></font>");
                }
            
        }

        protected IWebElement GetFirstCellItemsFilterByColumnName(string columnName)
        {
            return RowFilter.FindElement(FindType.XPath, $"//td[{(GetColumnHeaderIndexByName(columnName)+1)}]/input[contains(@type, 'button')]");
        }

        protected IReadOnlyCollection<IWebElement> GetCellItemsFilterByColumnName(string columnName)
        {
            // RowFilter.FindElements(FindType.XPath, "(./td[" + (GetColumnHeaderIndexByName(columnName) + 1) + "]/input | //td[" + (GetColumnHeaderIndexByName(columnName) + 1) + "]/input)");
            return RowFilter.FindElements(FindType.XPath, "./td[" + (GetColumnHeaderIndexByName(columnName) + 1) + "]/input");

        }

        protected IReadOnlyCollection<IWebElement> GetCellItemsFilterByIndex(int index)
        {
            return RowFilter.FindElements(FindType.XPath, "(./td[" + (index + 1) + "]/input | //td[" + (index + 1) + "]/input)");
        }


        protected void SelectItemOnFilterPane(IWebElement buttonFilter, GridFilterOperator item, bool isCaptured = true)
        {

            // Using javascript to click for some casse
            //IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
            //executor.ExecuteScript("arguments[0].click();", buttonFilter);
            CommonHelper.click_element(buttonFilter, isCaptured);

            WaitGridLoad();
            System.Threading.Thread.Sleep(250);

            string filterPanelXPath = "//div[contains(@id, 'rfltMenu')]/ul[contains(@class, 'rmActive')]";

            // Wait until the panel display
            CommonHelper.WaitUntilElementVisible(8, filterPanelXPath, false);

            IWebElement pane = BaseValues.DriverSession.FindElement(By.XPath(filterPanelXPath));
            // Find item on panel and click

            CommonHelper.MoveToElement(pane, true);

            IWebElement filterItem = CommonHelper.find_element_weak(pane, By.XPath($"./li[.='{item}']"));

            CommonHelper.click_element(filterItem, isCaptured);
            //filterItem.Click();
            //executor.ExecuteScript("arguments[0].click();", filterItem);

            // Wait until page load complete
            WaitGridLoad();
            System.Threading.Thread.Sleep(250);
        }

        //protected void SelectItemOnFilterPane(IWebElement buttonFilter, GridFilterOperator item)
        //{
        //    // Using javascript to click for some casse
        //    IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
        //    executor.ExecuteScript("arguments[0].click();", buttonFilter);

        //    // Wait until the panel display
        //    SpecificControls pane = new SpecificControls(FindType.XPath, "//*[contains(@id,'rfltMenu_detached')]");
        //    // Find item on panel and click
        //    var filterItem = pane.FindElement(FindType.XPath, GetXpathItemFromPaneFilter(item));
        //    int iTimeOut = 0;
        //    while (!filterItem.Displayed)
        //    {
        //        System.Threading.Thread.Sleep(500);
        //        iTimeOut++;
        //        if (iTimeOut > 10)
        //            throw new TimeoutException("The menu filter does not displayed after 10s.");
        //    }

        //    if (BaseValues.IsCaptureEverything)
        //    {
        //        string text = filterItem.Text == "" ? filterItem.GetAttribute("value") : filterItem.Text;
        //        ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(filterItem), $"Element <font color ='green'><b>{text}</b></font> will be clicked after capturing the screenshot");
        //    }
        //    else
        //    {
        //        string text = filterItem.Text == "" ? filterItem.GetAttribute("value") : filterItem.Text;
        //        ExtentReportsHelper.LogInformation(null, $"Element <font color ='green'><b>{text}</b></font> will be clicked after capturing the screenshot");
        //    }
        //    executor.ExecuteScript("arguments[0].click();", filterItem);

        //    // Wait until page load complete
        //    WaitGridLoad();
        //    BasePage.JQueryLoad();
        //}

        protected string GetXpathItemFromPaneFilter(GridFilterOperator filterOperator)
        {
            return "./ul/li/a[./span[text()='" + filterOperator.ToString("g") + "']]";
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

        /// <summary>
        /// Filter by Column
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="grids"></param>
        /// <param name="valueToFind"></param>
        //public virtual void FilterByColumn(string columnName, GridFilterOperator grids, string valueToFind)
        //{
        //    if (RowFilter != null)
        //    {
        //        if (!grids.Equals(GridFilterOperator.NoFilter))
        //        {
        //            var cellColumn = GetCellItemsFilterByColumnName(columnName).ToList();
        //            if (cellColumn.Count == 0)
        //                throw new NotFoundException(string.Format("The column - {0} - is not exist on current Grid. ", columnName));
        //            // Set value to textbox
        //            Textbox Item1_txt = new Textbox(cellColumn[0]);
        //            Item1_txt.SetText(valueToFind);
        //            System.Threading.Thread.Sleep(500);
        //        }
        //        System.Threading.Thread.Sleep(5000);
        //        // Click on filter button and select option to filter
        //        SelectItemOnFilterPane(GetCellItemsFilterByColumnName(columnName).ToList()[1], grids);
        //    }
        //    else
        //        throw new NotSupportedException("This grid does not support filter function");

        //}


        public virtual void FilterByColumn(string columnName, GridFilterOperator filter_operator, string valueToFind)
        {
            if (RowFilter != null)
            {
                System.Threading.Thread.Sleep(500);

                //CommonHelper.ScrollToBeginOfPage();

                var cellColumn = GetCellItemsFilterByColumnName(columnName).ToList();
                if (cellColumn.Count == 0)
                    throw new NotFoundException(string.Format("The column - {0} - does not exist on current Grid. ", columnName));

                // Set value to textbox
                string message = $"Input value <font color='green'><b><i>'{valueToFind}'</i></b></font> to field <font color='green'><b>'{cellColumn[0].TagName}'</b></font>.";
                if (filter_operator.Equals(GridFilterOperator.NoFilter))
                {
                    cellColumn[0].Clear();
                    //WaitGridLoad();
                    //ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(cellColumn[0]), message);
                    ExtentReportsHelper.LogInformation(null, message);
                    //return;
                }
                else
                {
                    cellColumn[0].SendKeys(Keys.Control + "a");
                    cellColumn[0].SendKeys(Keys.Delete);
                    cellColumn[0].SendKeys(valueToFind);
                }
                /* if (gridFilterOperator.Equals(GridFilterOperator.Contains))
                {
                    cellColumn[0].SendKeys(Keys.Tab);
                    JQueryLoad();
                    ExtentReportsHelper.LogInformation(UtilsHelper.CaptureScreen(cellColumn[0]), message);
                }
                else
                {
                    // Click on filter button and select option to filter
                    SelectItemOnFilterPane(GetCellItemsFilterByColumnName(columnName).ToList()[0], gridFilterOperator);
                } */

                System.Threading.Thread.Sleep(667);

                IWebElement column_filter_element = GetFirstCellItemsFilterByColumnName(columnName);

                // When filter by contain (don't select option to filterr - use tab - loading icon will pin and doesn't stop
                // Click on filter button and select option to filter
                SelectItemOnFilterPane(column_filter_element, filter_operator, false);

            }
            else
                throw new NotSupportedException("This grid does not support filter function");
        }

        /// <summary>
        /// Filter by column using for drop down list
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueToFind_DropDown"></param>
        /// <param name="valueToFind_ListItem"></param>
        /// <param name="valueToFind"></param>
        public virtual void FilterByColumnDropDowwn(string columnName, string valueToFind_ListItem, string valueToFind)
        {
            IWebElement dropDownItem = RowFilter.FindElement(FindType.XPath, "./td[" + (GetColumnHeaderIndexByName(columnName) + 1) + "]//input[not(@type='hidden')]");
            if (dropDownItem.Displayed is true)
            {
                if (dropDownItem.GetAttribute("value") == valueToFind)
                {
                    ExtentReportsHelper.LogInformation(null, $"<font color='green'>The current column '{columnName}' is filtering by value '{valueToFind}'.</font>");
                    return;
                }

                try
                {
                    dropDownItem.Click();
                    CommonHelper.WaitUntilElementVisible(5, valueToFind_ListItem, false);
                    var list = FindElementHelper.Instance().FindElements(FindType.XPath, valueToFind_ListItem).Where(p => p.Displayed == true).FirstOrDefault();

                    // Throw if the column is not drop down list
                    if (list == null)
                    {
                        throw new NotSupportedException("This grid does not support filter function");
                    }

                    IWebElement selectedItem = list.FindElements(By.XPath("./li")).Where(item => item.Text == valueToFind).FirstOrDefault();
                    if (selectedItem != null && selectedItem.Displayed is true)
                        selectedItem.Click();
                    else
                    {
                        ExtentReportsHelper.LogInformation($"font color='yellow'>Can't find any value with name <b>'{valueToFind}'</b> on drop down column <b>'{columnName}'</b> to filter.</font>");
                        // Clear focus
                        dropDownItem.SendKeys(Keys.Tab);
                    }

                }
                catch (Exception exception)
                {
                    Log.Error($"Could not locate item element {valueToFind} in Dropdown List: {exception.Message}");
                }
            }
            else
                ExtentReportsHelper.LogWarning($"font color='yellow'>Can't find column with name <b>{columnName}</b> to filter.</font>");
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
            catch (Exception ex) { 
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

        public virtual bool IsItemOnCurrentPageV2(string columnName, string valueToFind)
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
                    isExist = WrappedControl.GetWrappedControl().FindElement(By.XPath("(./tbody/tr/td[" + iColumn + "]/div/a | //tbody/tr/td[" + iColumn + "]/div/a)[1]"));
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


        /// <summary>
        /// Get Item In Grid By Column In Current Page 
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="valueToFind"></param>
        /// <returns></returns>
        public virtual IWebElement GetItemOnCurrentPage(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td/div", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                var item = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td[position()=" + iColumn + " and (normalize-space(text())='" + valueToFind + "' or ./*[text()='" + valueToFind + "'])]", 5);
                CommonHelper.MoveToElement(item, true);
                return item;
            }
        }

        public virtual IWebElement GetItemOnCurrentPageV2(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td/div", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                var item = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td[" + iColumn + "]/div", 5);
                CommonHelper.MoveToElement(item, true);
                return item;
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
                var item = WrappedControl.FindElement(FindType.XPath, $"./tbody/tr/td[position()={iColumn} and (normalize-space(text())='{valueToFind}' or contains(.,'{valueToFind}')  or ./*[contains(text(),'{valueToFind}')])]", 5);
                CommonHelper.MoveToElement(item, true);
                return item;
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
            var item = WrappedControl.FindElement(FindType.XPath, $"./tbody/tr[{iRow}]/td[{iColumn}]", 5);
            CommonHelper.MoveToElement(item, true);
            return item;
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
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td/div", 1);
            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                var item = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td[position()=" + iColumn + " and (normalize-space(text())='" + valueToFind + "' or ./*[text()='" + valueToFind + "'])]", 5);
                CommonHelper.MoveToElement(item, true);
                return item;
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

        /// <summary>
        /// Click Item In Grid Current Page by index. Column and Row start from 0
        /// </summary>
        /// <param name="columnIndex"></param>
        /// <param name="rowIndex"></param>
        public virtual void ClickItemInGrid(int columnIndex, int rowIndex)
        {
            var _control = GetItemOnCurrentPage(columnIndex, rowIndex);
            if (_control == null)
            {
                ExtentReportsHelper.LogFail($"The item name {columnIndex} index and value {rowIndex} index is not existed on current page.");
                throw new NoSuchElementException($"The item name {columnIndex} and value {rowIndex} is not existed on current page.");
            }

            CommonHelper.MoveToElement(_control, true);

            Button item = new Button(_control.FindElement(By.XPath(".//*[@href]")));
            item.Click();
            WaitGridLoad();
            System.Threading.Thread.Sleep(500);
        }

        public virtual bool IsLockedRoleOnItemInGrid(string role_name)
        {
            WaitGridLoad();
            System.Threading.Thread.Sleep(2000);

            SpecificControls item = GetRowItem("Role", role_name);

            if (item == null) { 
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not exist on current page.", role_name));
            }

            CommonHelper.MoveToElement(item, true);

            var locked_role = item.FindElement(FindType.XPath, "./td/span[@class='lockedRole']");
            if (locked_role is null)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Click Edit Item In Current Page
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="grids"></param>
        /// <param name="valueToFind"></param>
        public virtual void ClickEditItemInGridButton(string columnName, string valueToFind)
        {
            WaitGridLoad();
            System.Threading.Thread.Sleep(1500);

            SpecificControls item = GetRowItem(columnName, valueToFind);

            if (item == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not exist on current page.", valueToFind));

            CommonHelper.MoveToElement(item, true);

            var editButton = item.FindElement(FindType.XPath, "./td[./input[@type='image']]");

            CommonHelper.MoveToElement(editButton, true);

            SpecificControls edit = new SpecificControls(editButton);
            if (edit.GetWrappedControl() == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not contains Edit function.", valueToFind));

            edit.Click();
            WaitGridLoad();
        }

        /// <summary>
        /// Click Edit Item In Current Page
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="grids"></param>
        /// <param name="valueToFind"></param>
        public virtual void ClickEditItemInGrid(string columnName, string valueToFind)
        {
            WaitGridLoad();
            System.Threading.Thread.Sleep(1500);

            SpecificControls item = GetRowItem(columnName, valueToFind);

            if (item == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not exist on current page.", valueToFind));

            CommonHelper.MoveToElement(item, true);

            var editButton = item.FindElement(FindType.XPath, "./td/a[./img[@alt='Edit' or @alt='Edit Permissions']]");
            if (editButton is null)
                editButton = item.FindElement(FindType.XPath, ".//*[@alt='Edit' or @alt='Edit Permissions' ]");

            CommonHelper.MoveToElement(editButton, true);

            SpecificControls edit = new SpecificControls(editButton);
            if (edit.GetWrappedControl() == null)
                edit = new SpecificControls(item.FindElement(FindType.XPath, "//*[@alt='Edit' or @alt='Edit Permissions']"));
            if (edit.GetWrappedControl() == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not contains Edit function.", valueToFind));

            edit.Click();
            //edit.JavaScriptClick();
            WaitGridLoad();
        }
        /// <summary>
        /// Check edit on 1st Item on current Page
        /// </summary>
        public virtual void IsEditFirstItem()
        {
            var isNoRecord = FindElementHelper.Instance().FindElement(FindType.XPath, "./tbody/tr/td/div", 1);

            CommonHelper.MoveToElement(isNoRecord, true);

            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                throw new NoSuchElementException("No records to display.");
            }
            else
            {
                MoveGridToCenterScreen();

                SpecificControls item = new SpecificControls(WrappedControl.FindElement(FindType.XPath, "./tbody/tr[1]//*/a[./img[@alt='Edit']]"));
                if (item == null)
                    throw new NoSuchElementException("The 1st item does not have Edit function.");
                else
                    ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(item), $"<font color='green'><b>The 1st Edit item is displayed in Table Grid.</b></font>");
            }
        }

        /// <summary>
        /// Click edit on 1st Item on current Page
        /// </summary>
        public virtual void ClickEditFirstItem()
        {
            var isNoRecord = FindElementHelper.Instance().FindElement(FindType.XPath, "./tbody/tr/td/div", 1);

            CommonHelper.MoveToElement(isNoRecord, true);

            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                throw new NoSuchElementException("No records to display.");
            }
            else
            {
                MoveGridToCenterScreen();

                SpecificControls item = new SpecificControls(WrappedControl.FindElement(FindType.XPath, "./tbody/tr[1]//*/a[./img[@alt='Edit']]"));
                if (item == null)
                    throw new NoSuchElementException("The 1st item does not have Edit function.");

                item.Click();
                //item.JavaScriptClick();
                WaitGridLoad();
            }
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

            if (item.IsNull())
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>The item with name - {valueToFind} - is not exist on current page.</font>");
                return;
            }

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
        /// <summary>
        /// Check delete on 1st Item on current Page
        /// </summary>
        public virtual void IsDeleteFirstItem()
        {
            var isNoRecord = FindElementHelper.Instance().FindElement(FindType.XPath, "./tbody/tr/td/div", 1);

            CommonHelper.MoveToElement(isNoRecord, true);

            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                throw new NoSuchElementException("No records to display.");
            }
            else
            {
                MoveGridToCenterScreen();

                SpecificControls item = new SpecificControls(WrappedControl.FindElement(FindType.XPath, "./tbody/tr[1]//*/input[@title='Delete']"));
                if (item == null)
                    throw new NoSuchElementException("The 1st item does not have Delete function.");
                else
                    ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(item), $"<font color='green'><b>The 1st Delete item is displayed in Table Grid.</b></font>");
            }
        }
        /// <summary>
        /// Click delete 1st item on current page
        /// </summary>
        public virtual void ClickDeleteFirstItem()
        {
            var isNoRecord = FindElementHelper.Instance().FindElement(FindType.XPath, "./tbody/tr/td/div", 1);

            CommonHelper.MoveToElement(isNoRecord, true);

            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                throw new NoSuchElementException("No records to display.");
            }
            else
            {
                MoveGridToCenterScreen();

                SpecificControls item = new SpecificControls(WrappedControl.FindElement(FindType.XPath, "./tbody/tr[1]//*/input[@title='Delete']"));
                if (item == null)
                    throw new NoSuchElementException("The 1st item do not contains Delete function.");

                item.Click();
                //item.JavaScriptClick();
                WaitGridLoad();
                System.Threading.Thread.Sleep(667);
            }

        }

        /// <summary>
        /// Get total items on current filter for all page
        /// </summary>
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

        /// <summary>
        /// Get total pages on current filter
        /// </summary>
        public virtual int GetTotalPages
        {
            get
            {
                var total = WrappedControl.FindElement(FindType.XPath, "./tfoot/tr/td/table/tbody/tr/td/div[5]/strong[2]");
                int.TryParse(total.Text.ToString().Trim(), out int iTotal);
                return iTotal;
            }
        }

        /// <summary>
        /// Get Page Size of Grid
        /// </summary>
        public virtual int GetPageSize
        {
            get
            {
                var pagesize = WrappedControl.FindElement(FindType.XPath, "./tfoot/tr/td/table/tbody/tr/td/div[4]//input[@type!='hidden']");
                int.TryParse(pagesize.GetAttribute("value").ToString().Trim(), out int size);
                return size;
            }
        }

        /// <summary>
        /// Change page size of Grid
        /// </summary>
        /// <param name="sizeNumber"></param>
        public virtual void ChangePageSize(int sizeNumber)
        {
            MoveGridToCenterScreen();

            SpecificControls pagesize = new SpecificControls(WrappedControl.FindElement(FindType.XPath, "//a[contains(@id,'PageSizeComboBox_Arrow')]"));

            if (pagesize == null || !pagesize.WaitUntilExist(8))
            { 
                ExtentReportsHelper.LogFail(string.Format("Page size controls do not exist on page."));
                throw new InvalidElementStateException("Page size controls do not exist on page.");
            }

            pagesize.Click();
            
            Label listSizeItem = new Label(FindType.XPath, "//div[@class='rcbSlide']");
            if (!listSizeItem.WaitUntilExist(8))
            {
                ExtentReportsHelper.LogFail(string.Format("The page size pane do not display on page."));
                throw new NoSuchElementException("The page size pane do not display on page.");
            }

            CommonHelper.MoveToElement(listSizeItem, true);

            var _item = WrappedControl.FindElement(FindType.XPath, $"//div[@class='rcbSlide']/.//li[text()='{sizeNumber}']");
            if (_item == null)
                throw new NotSupportedException("The grid does not support this size.");

            SpecificControls item = new SpecificControls(_item);

            item.Click();
            //item.JavaScriptClick();
            WaitGridLoad();
            System.Threading.Thread.Sleep(500);
        }

        /// <summary>
        /// Nagivate to other Page
        /// </summary>
        /// <param name="pageNumber"></param>
        public virtual void NavigateToPage(int pageNumber)
        {
            if (pageNumber <= 0 || pageNumber > GetTotalPages)
            {
                ExtentReportsHelper.LogFail(string.Format("This page only have {0} page(s).", GetTotalPages));
                throw new IndexOutOfRangeException(string.Format("This page only have {0} page(s).", GetTotalPages));
            }
            else if (pageNumber == GetCurrentPageNumber)
            {
                Console.WriteLine("You are stay at " + pageNumber + " page.");
            }
            else
            {
                var item = WrappedControl.FindElement(FindType.XPath, $"./tfoot/tr/td/table/tbody/tr/td/div[2]/a[./span[text()='{pageNumber}']]");

                CommonHelper.MoveToElementWithoutCaptureAndCenter(item);

                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(item), $"Navigating to page <font color='green'><b>{pageNumber}</b></font>.");

                CommonHelper.click_element(item);
                //IJavaScriptExecutor executor = (IJavaScriptExecutor)BaseValues.DriverSession;
                //executor.ExecuteScript("arguments[0].click();", item);
                WaitGridLoad();
                System.Threading.Thread.Sleep(500);
            }
        }

        /// <summary>
        /// Get current page number
        /// </summary>
        public virtual int GetCurrentPageNumber
        {
            get
            {
                var currentPage = WrappedControl.FindElement(FindType.XPath, "./tfoot/tr/td/table/tbody/tr/td/div[2]/a[@onclick='return false;']/span");

                CommonHelper.MoveToElementWithoutCaptureAndCenter(currentPage);

                int.TryParse(currentPage.Text.ToString().Trim(), out int page);
                return page;
            }
        }

        /// <summary>
        /// Wait Grid loading until complete. Be careful, it may make your app freeze!
        /// </summary>
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

        public IWebElement GetItemWithTextContainsOnCurrentPage(string columnName, string valueToFind)
        {
            int iColumn = GetColumnHeaderIndexByName(columnName) + 1;
            var isNoRecord = WrappedControl.FindElement(FindType.XPath, "./tbody/tr/td/div", 1);

            CommonHelper.MoveToElement(isNoRecord, true);

            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                return null;
            }
            else
            {
                MoveGridToCenterScreen();
                var item = WrappedControl.FindElement(FindType.XPath, $"./tbody/tr/td[position()={iColumn} and (contains(normalize-space(text()),'{valueToFind}') or ./*[contains(text(),'{valueToFind}')])]", 5);
                CommonHelper.MoveToElement(item, true);
                return item;
            }
        }

        public void MoveGridToCenterScreen()
        {
            CommonHelper.MoveToElementWithoutCapture(WrappedControl);
        }

        /// <summary>
        /// Get element by column name and row index
        /// </summary>
        /// <param name="columnName"></param>
        /// <param name="rowIndex"></param>
        /// <returns></returns>
        public virtual IWebElement GetItemByRowAndColumn(string columnName, int rowIndex)
        {
            var isNoRecord = FindElementHelper.Instance().FindElement(FindType.XPath, "./tbody/tr/td/div", 1);

            CommonHelper.MoveToElement(isNoRecord, true);

            if (isNoRecord != null && isNoRecord.Text == "No records to display.")
            {
                // No item in grid view
                ExtentReportsHelper.LogWarning($"<font color='yellow'>No records to display.</b></font>");
                return null;
            }
            else
            {
                MoveGridToCenterScreen();
                int iColumn = GetColumnHeaderIndexByName(columnName) + 1;

                // Get element by row and column
                IWebElement item = WrappedControl.FindElement(FindType.XPath, $"./tbody/tr[{rowIndex}]/td[{iColumn}]");
                return item;
            }
        }
        public bool IsColumnFoundInGrid(string columnName)
        {
            try
            {
                var cellColumn = GetCellItemsFilterByColumnName(columnName).ToList();
                if (cellColumn.Count == 0)
                    return false;
                return true;
            }
            catch
            {
                try
                {
                    var item = WrappedControl.GetWrappedControl().FindElement(By.XPath($"./thead/tr[1]/th/a[contains(text(),'{columnName}')]"));
                    if (item.Text == columnName)
                        return true;
                    else
                        return false;
                }
                catch
                {
                    var item2 = WrappedControl.GetWrappedControl().FindElement(By.XPath($"./thead/tr[1]/th[contains(text(),'{columnName}')]"));
                    if (item2.Text == columnName)
                        return true;
                    else
                        return false;
                }
            }
        }
        public virtual void SelectItemOnGridViaCheckbox(string columnName, string valueToFind)
        {
            WaitGridLoad();
            System.Threading.Thread.Sleep(1500);

            SpecificControls item = GetRowItem(columnName, valueToFind);

            if (item == null)
                throw new NoSuchElementException(string.Format("The item with name - {0} - is not exist on current page.", valueToFind));

            CommonHelper.MoveToElement(item, true);

            CheckBox checkbox = new CheckBox(item.FindElement(FindType.XPath, "./td[./input[@type='checkbox']]"));
           

            CommonHelper.MoveToElement(checkbox, true);
            checkbox.SetCheck(true);

            WaitGridLoad();
            System.Threading.Thread.Sleep(500);
        }
        public virtual void SelectGridRow(string columnName, string valueToFind)
        {
            var _control = GetItemOnCurrentPage(columnName, valueToFind);
            if (_control == null)
                throw new NoSuchElementException($"The item with name {columnName} and value {valueToFind} is not existed on current page.");

            CommonHelper.MoveToElement(_control, true);

            SpecificControls controls = new SpecificControls(_control);

            controls.WaitForElementIsVisible(10);
            controls.Click(false);
            WaitGridLoad();
            System.Threading.Thread.Sleep(500);
        }
        public virtual void SelectGridRowV2(string columnName, string valueToFind)
        {
            var _control = GetItemOnCurrentPageV2(columnName, valueToFind);
            if (_control == null)
                throw new NoSuchElementException($"The item with name {columnName} and value {valueToFind} is not existed on current page.");

            CommonHelper.MoveToElement(_control, true);

            SpecificControls controls = new SpecificControls(_control);

            controls.WaitForElementIsVisible(10);
            controls.Click(true);
            WaitGridLoad();
            System.Threading.Thread.Sleep(500);
            CommonHelper.CaptureScreen();
        }
    }
}
