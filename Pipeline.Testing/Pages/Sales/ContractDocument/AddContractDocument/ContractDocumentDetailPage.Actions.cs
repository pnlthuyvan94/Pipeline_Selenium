using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Sales.ContractDocument.AddContractDocument
{
    public partial class ContractDocumentDetailPage
    {
        public ContractDocumentDetailPage EnterUserName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        public ContractDocumentDetailPage EnterDescription(string city)
        {
            if (!string.IsNullOrEmpty(city))
                Description_txt.SetText(city);
            return this;
        }

        public ContractDocumentDetailPage EnterUploadedFile(string file)
        {
            if (!string.IsNullOrEmpty(file))
                UploadFile_txt.SetText(file);
            return this;
        }

        public ContractDocumentDetailPage EnterSortOrder(string state)
        {
            if (!string.IsNullOrEmpty(state))
                SortOrder_txt.SetText(state);
            return this;
        }

        public void UploadFile(string pathUploadedFile)
        {
            if (!string.IsNullOrEmpty(pathUploadedFile))
            {
                UploadFile_txt.SetAttribute("value", pathUploadedFile);
            }
        }

        public void Save()
        {
            Save_btn.Click();
            PageLoad();
        }

        public void CreateContractDocument(ContractDocumentData data)
        {
            EnterUserName(data.Name).EnterDescription(data.Description)
                                .EnterUploadedFile(data.UploadedFile)
                                .EnterSortOrder(data.SortOrder)
                                .Save();
        }
    }
}
