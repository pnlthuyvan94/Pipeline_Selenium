using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes.MoveInReadyResource
{
    public partial class MoveInReadyResourceGrid : DetailsContentPage<MoveInReadyResourceGrid>
    {
        public MoveInReadyResourceGrid() : base()
        {

        }

        protected Label AddMoveInReadyResource_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlSpecResources']/div/article/section/article/header/h1");

        protected Button AddMoveInReadyResource_btn => new Button(FindType.XPath, "//*[@id='aAddResource']");

        protected IGrid MoveInReadyResourcePage_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgSpecResources_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgSpecResources']/div[1]");

        protected Textbox SelectImage_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_AsyncUpload1file0']");

        protected Button UploadImage_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lnkInsertResource']");

        protected Textbox Feature_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtTitle']");

        protected Textbox FeatureDescription_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtLink']");

        protected Button SaveFeature_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertFeature']");

        protected Textbox SelectDocument_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_RadAsyncUploadDocsfile0']");

        protected Button UploadDocument_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertDocs']");
    }
}
