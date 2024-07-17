using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Jobs.DocumentTypes.AddDocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.DocumentTypes
{
    public partial class DocumentTypes
    {
        public void OpenAddDocumentTypesModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddDocumentTypeModal = new AddDocumentTypeModal();
            System.Threading.Thread.Sleep(500);
        }
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            DocumentTypes_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgDocumentTypes']");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return DocumentTypes_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            DocumentTypes_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public string EditDocumentType(string columnName, string value, string newName, string newDescription, string newTrade)
        {
            DocumentTypes_Grid.ClickEditItemInGrid(columnName, value);
            System.Threading.Thread.Sleep(4000);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Enter document type name.</b></font>");
            Textbox name_txt = new Textbox(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgDocumentTypes_ctl00']/tbody/tr/td/input[contains(@id,'txtEditDocumentTypesName')]");
            name_txt.SetText(newName);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Enter document type description.</b></font>");
            Textbox description_txt = new Textbox(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgDocumentTypes_ctl00']/tbody/tr/td/textarea[contains(@id,'txtEditDocumentTypesDescription')]");
            description_txt.SetText(newDescription);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select building trade.</b></font>");
            DropdownList trade_dll = new DropdownList(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgDocumentTypes_ctl00']/tbody/tr/td/div/div/select[contains(@id,'lstEditBuildingTrades')]");
            trade_dll.SelectItem(newTrade);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Click update button.</b></font>");
            Button updateBtn = new Button(FindType.XPath, "//table[@id='ctl00_CPH_Content_rgDocumentTypes_ctl00'" +
              "]/tbody/tr/td/input[contains(@id,'UpdateButton')]");
            updateBtn.Click();

            return GetLastestToastMessage();
        }

        public void GoBackToJobDocumentsPage()
        {
            JobDocuments_btn.Click();
        }
    }
}
