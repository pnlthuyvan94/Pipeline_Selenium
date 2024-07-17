using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.DocumentType.AddDocument;

namespace Pipeline.Testing.Pages.UserMenu.DocumentType
{
    public partial class DocumentTypePage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            DocumentType_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTypes']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return DocumentType_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            DocumentType_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTypes']/div[1]");
        }

        public void OpenDocumentTypeModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            // Wait loading grid
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTypes']/div[1]");
            AddDocumentType = new AddDocumentType();
        }

    }

}
