using LinqToExcel;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Sales.ContractDocument.AddContractDocument;

namespace Pipeline.Testing.Pages.Sales.ContractDocument
{
    public partial class ContractDocumentPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            ContractDocument_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            ContractDocument_Grid.WaitGridLoad();
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return ContractDocument_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            ContractDocument_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
            ContractDocument_Grid.WaitGridLoad();
        }

        public void ClickAddContractDocumentButton()
        {
            PageLoad();
            GetItemOnHeader(DashboardContentItems.Add).Click();
            // Open Detail User page
            ContractDocumentDetail = new ContractDocumentDetailPage();
        }

        public void CreateNewContractDocument()
        {
            //  Populate data from excel file
            Row TestData = ExcelFactory.GetRow(TestData_RT_01104, 1);
            ContractDocumentData _data = new ContractDocumentData()
            {
                Name = TestData["Name"],
                Description = TestData["Description"],
                UploadedFile = _pathUploadedFile,
                SortOrder = TestData["Sort Order"],
            };
            ContractDocumentDetail.CreateContractDocument(_data);
        }
    }

}
