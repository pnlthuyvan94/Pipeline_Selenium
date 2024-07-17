using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Purchasing.Trades
{
    public partial class TradesExportModal : TradesPage
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id=\"TradesExportModal\"]/section/header/h1");
        protected DropdownList ExportOptions_ddl => new DropdownList(FindType.XPath, "//*[@id=\"exportOption\"]");

        protected Button ExportContinue_btn => new Button(FindType.XPath, "//*[@id=\"exportContinue\"]");


    }
}
