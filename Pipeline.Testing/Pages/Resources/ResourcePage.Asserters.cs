using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Resources
{
    public partial class ResourcePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            ResourcePage_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return ResourcePage_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            ResourcePage_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void WaitGridLoad()
        {
            WaitingLoadingGifByXpath("//*[contains(@id,'ctl00_CPH_Content_LoadingPanel1ctl00')]/div[1]");
        }

        public void SelectItemInGrid(string columnName, string value)
        {
            ResourcePage_Grid.ClickItemInGrid(columnName, value);
            PageLoad();
        }

        public bool IsUploadSectionDisplay()
        {
            Add_btn.Click();

            // Reloading page
            PageLoad();

            // True: Type, Tite and Link item isn't null
            if (Type_ddl.IsNull() || Title_txt.IsNull() || Source_txt.IsNull())
            {
                return false;
            }

            return true;
        }
    }
}
