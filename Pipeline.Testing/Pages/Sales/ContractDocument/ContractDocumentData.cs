
namespace Pipeline.Testing.Pages.Sales.ContractDocument
{
    public class ContractDocumentData
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public string UploadedFile { get; set; }
        public string SortOrder { get; set; }

        public ContractDocumentData()
        {
            Name = string.Empty;
            Description = string.Empty;
            UploadedFile = string.Empty;
            SortOrder = string.Empty;
        }
    }
}