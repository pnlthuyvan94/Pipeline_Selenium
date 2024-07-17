using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Purchasing.Trades.AddTrade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.Jobs.JobDocuments.Upload
{
    public partial class UploadDocumentsModal : DashboardContentPage<UploadDocumentsModal>
    {
        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='upload-doc-modal']/section/header/h1");
        protected Button BackToJobDocuments_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_hypBack']");
        protected Button Upload_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_btnUpload']");
        protected Grid Documents_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgUploadedFiles_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUploadedFiles']/div[1]");
        protected Grid UploadedJobDocumentPreview_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgUploadedJobDocumentPreview_ctl00", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUploadedJobDocumentPreview']/div[1]");
        protected Link uploadLink => new Link(FindType.Id, "lBrowseFile");
        protected Button CancelUpload_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbCancelDocUpload");
        protected Button Step2ContinueUpload_btn => new Button(FindType.Id, "ctl00_CPH_Content_btnDocUploadStep2");
        protected Button BackToStep1_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbBreadCrumbsLblS1");
        protected Button SaveUpload_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbDocUploadSaveTrigger");
    }
}
