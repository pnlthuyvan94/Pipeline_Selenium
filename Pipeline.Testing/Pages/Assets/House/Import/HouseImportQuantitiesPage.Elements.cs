using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.House.Import
{
    public partial class HouseImportQuantitiesPage : DetailsContentPage<HouseImportQuantitiesPage>
    {
        protected Button UploadFile_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbOpenUploadModal' and text() = 'Upload']");
        protected Button GenerateReportView_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbGenerateReportView']");
        protected CheckBox SelectAllHouseMaterialFile_ckb => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgFiles_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");
        protected Label ImportQuantities_lbl => new Label(FindType.XPath, "//*[@class='card-header clearfix']/h1[text() = 'Import Quantities']");
        protected Button Import_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbImport']");
        protected Button FinishImport_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbFinalize']");
        protected string loadingIcon = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgFiles']";
        protected Grid HouseMaterialFile_grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgFiles_ctl00']", loadingIcon);

        protected Button BulkAction_btn => new Button(FindType.XPath, "//*[@id='bulk-actions']");
        protected Button DeleteSelectedFiles_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbDeleteSelectedFiles']");


        /*********************** Upload House Material Files *******************************/

        protected Label Upload_lbl => new Label(FindType.XPath, "//*[@class='card-header']/h1[text() = 'Upload House Material Files']");
        protected DropdownList ImportType_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlImportTypes']");
        protected Textbox UploadFile_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_RadAsyncUpload1file0']");
        protected CheckBox SelectAllFile_ckb => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPendingFiles_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");
        protected Button UploadConfirm_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbUpload']");
        protected Button CancelConfirm_btn => new Button(FindType.XPath, "//*[@id='lbCancelUpload']");



    }

}
