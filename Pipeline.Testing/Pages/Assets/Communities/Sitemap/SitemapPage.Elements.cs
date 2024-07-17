using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Assets.Communities.Sitemap
{
    public partial class SitemapPage : DetailsContentPage<SitemapPage>
    {

        protected Button Add_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddSitemap']");

        protected Textbox DisplayFileName_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_fuSitemap']");

        protected Label Description_lbl => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_pnlInsertSitemap']/section/section[2]");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbUploadSitemap']");

        protected Button Cancel_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCancelSitemap']");

        protected Label UploadFileName123_lbl => new Label(FindType.Id, "ctl00_CPH_Content_lblSitemapFile");

        protected Button Remove_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbRemoveSitemapFile']");

    }
}
