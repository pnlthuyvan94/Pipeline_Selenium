using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Utils;
using System.Linq;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Sales.ContractDocument.AddContractDocument
{
    public partial class ContractDocumentDetailPage : DetailsContentPage<ContractDocumentDetailPage>
    {
        private static IQueryable<Row> MetaData;

        public ContractDocumentDetailPage() : base()
        {
            // Sheet contains repository of Dashboard
            MetaData = ContractDocumentPage.Instance.MetaData;
        }

        private Row _title
         => ExcelFactory.GetRow(MetaData, 1);
        protected Label Title_lbl
            => new Label(_title);

        private Row _name
           => ExcelFactory.GetRow(MetaData, 2);
        protected Textbox Name_txt
            => new Textbox(_name);

        private Row _description
            => ExcelFactory.GetRow(MetaData, 3);
        protected Textbox Description_txt
            => new Textbox(_description);

        private Row _UploadFile
          => ExcelFactory.GetRow(MetaData, 4);
        protected Textbox UploadFile_txt
            => new Textbox(_UploadFile);

        private Row _sortOrder
            => ExcelFactory.GetRow(MetaData, 5);
        protected Textbox SortOrder_txt
            => new Textbox(_sortOrder);

        private Row _save
            => ExcelFactory.GetRow(MetaData, 6);
        protected Button Save_btn
            => new Button(_save);
    }

}
