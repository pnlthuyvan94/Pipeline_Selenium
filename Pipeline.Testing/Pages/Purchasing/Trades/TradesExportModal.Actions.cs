using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades
{
    public partial class TradesExportModal
    {
        public TradesExportModal SelectExportOption(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                ExportOptions_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }

        public void ContinueExport(string exportType, string exportFileName, string expectedTitle)
        {
            ExportContinue_btn.Click(true);
            System.Threading.Thread.Sleep(5000);
            ValidationEngine.ValidateExportFile(exportType, exportFileName, expectedTitle, 0);
        }
    }
}
