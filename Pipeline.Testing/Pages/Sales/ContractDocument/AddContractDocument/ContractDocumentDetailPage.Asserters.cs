using System;

namespace Pipeline.Testing.Pages.Sales.ContractDocument.AddContractDocument
{
    public partial class ContractDocumentDetailPage
    {
        public bool IsSaveContractSuccessful(string username)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedUserName = $"{username}";
            return SubHeadTitle().Equals(expectedUserName) && !CurrentURL.EndsWith("ctrid=0");
        }
    }
}
