using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Jobs.JobDocuments.Upload;
using Pipeline.Testing.Pages.Purchasing.Trades.AddTrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.JobDocuments
{
    public partial class JobDocuments : DashboardContentPage<JobDocuments>
    {
        public UploadDocumentsModal UploadDocumentsModal { get; private set; }
        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgJobDocument']/div[1]";

        protected Button DocumentTypes_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbDocumentTypes']");
        protected Button AddDocument_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypAddJobDocument']");
        protected Textbox JobSearch_Txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_tb_SearchJobDocument']");
        protected Button JobSearch_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_btn_SearchJobDocument']");
        protected Button ViewByFilter_btn => new Button(FindType.Id, "filter-actions");
        protected CheckBox ViewByAddress_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_cbViewSearchAddress");
        protected CheckBox DisplayOnlyCloseJobs_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_cbViewClosedJobs");
        protected Button Reset_btn => new Button(FindType.Id, "btn_FilterRest");
        protected Button Filter_btn => new Button(FindType.Id, "ctl00_CPH_Content_btn_FilterOK");
        protected Button Upload_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbUploadDocument");
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='upload-doc-modal']/section/header/h1");
        protected Image JobInformationTooltip => new Image(FindType.XPath, "//*[name()='svg']/*[name()='g']/*[name()='path']");
        protected Grid Documents_grid => new Grid(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_rgJobDocument_ctl00\"]", _gridLoading);
        protected Grid UploadFiles_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgUploadedJobDocumentPreview_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUploadedJobDocumentPreview']/div[1]");
        protected CheckBox SelectAll_chk => new CheckBox(FindType.Id, "ctl00_CPH_Content_rgJobDocument_ctl00_ctl02_ctl02_ClientSelectColumnSelectCheckBox");
        protected Button DeleteAll_lnk => new Button(FindType.Id, "ctl00_CPH_Content_lbDeleteSelectedJobDocuments");
        protected Button BulkAction_Btn => new Button(FindType.Id, "bulk-actions");
    }
}
