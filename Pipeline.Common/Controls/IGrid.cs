using OpenQA.Selenium;
using Pipeline.Common.Enums;

namespace Pipeline.Common.Controls
{
    public interface IGrid
    {
        int GetCurrentPageNumber { get; }
        int GetPageSize { get; }
        int GetTotalPages { get; }
        int GetTotalItems { get; }
        int GetColumnHeaderIndexByName(string columnName);
        void IsColumnHeaderIndexByName(string columnName);
        void WaitGridLoad(bool useLoadingElementXPath = false);
        void NavigateToPage(int pageNumber);
        void ChangePageSize(int sizeNumber);

        void ClickDeleteFirstItem();
        void IsDeleteFirstItem();
        void ClickDeleteItemInGrid(string columnName, string valueToFind);
        void ClickEditFirstItem();
        void IsEditFirstItem();
        void ClickEditItemInGrid(string columnName, string valueToFind);
        void ClickEditItemInGridButton(string columnName, string valueToFind);
        void ClickEditItemInGridWithTextContains(string columnName, string valueToFind);
        void ClickItemInGrid(int columnIndex, int rowIndex);
        void ClickItemInGrid(string columnName, string valueToFind);
        void ClickItemInGridWithTextContains(string columnName, string valueToFind);
        void MoveGridToCenterScreen();

        bool IsLockedRoleOnItemInGrid(string valueToFind);

        bool IsItemOnCurrentPage(string columnName, string valueToFind);
        bool IsItemWithTextContainsOnCurrentPage(string columnName, string valueToFind);
        void FilterByColumn(string columnName, GridFilterOperator grids, string valueToFind);
        void SelectItemOnGridViaCheckbox(string columnName, string valueToFind);
        IWebElement GetItemOnCurrentPage(int columnIndex, string valueToFind);
        IWebElement GetItemOnCurrentPage(int columnIndex, int rowIndex);
        IWebElement GetItemOnCurrentPage(string columnName, string valueToFind);
        IWebElement GetItemWithTextContainsOnCurrentPage(string columnName, string valueToFind);
        IWebElement GetItemByRowAndColumn(string columnName, int rowIndex);
    }
}
