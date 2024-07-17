using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Jobs.DocumentTypes.AddDocumentTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.DocumentTypes
{
    public partial class DocumentTypes : DashboardContentPage<DocumentTypes>
    {
        public AddDocumentTypeModal AddDocumentTypeModal { get; private set; }
        protected Button JobDocuments_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbBackToJobDocuments']");
        protected Grid DocumentTypes_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgDocumentTypes_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgDocumentTypes']/div[1]");
    }
}
