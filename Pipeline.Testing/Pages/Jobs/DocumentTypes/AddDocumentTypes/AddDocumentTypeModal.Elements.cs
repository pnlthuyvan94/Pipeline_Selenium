using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.DocumentTypes.AddDocumentTypes
{
    public partial class AddDocumentTypeModal : DocumentTypes
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='document-types-modal']/section/header/h1");

        protected Textbox DocumentTypeName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDocumentTypeName']");
        protected Textbox DocumentTypeDescription_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDocumentTypeDescription']");
        protected DropdownList Trades_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_lstBuildingTrades']");
        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");
    }
}
