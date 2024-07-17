using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.DocumentTypes.AddDocumentTypes
{
    public partial class AddDocumentTypeModal
    {
        public AddDocumentTypeModal EnterDocumentTypeName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                DocumentTypeName_txt.SetText(data);
            return this;
        }

        public AddDocumentTypeModal EnterDocumentTypeDescription(string data)
        {
            if (!string.IsNullOrEmpty(data))
                DocumentTypeDescription_txt.SetText(data);
            return this;
        }
        public AddDocumentTypeModal SelectAccessibleTrades(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                Trades_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }
        public void Save()
        {
            Save_btn.Click(false);
        }
    }
}
