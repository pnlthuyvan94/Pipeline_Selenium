using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Pages.Assets.Communities.Sitemap
{
    public partial class SitemapPage
    {
        public void UploadSitemapFile(string sitemapFileName)
        {
            DisplayFileName_txt.SendKeysWithoutClear(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + sitemapFileName);
            PageLoad();
            Save();
        }

        public void Add()
        {
            Add_btn.Click();
        }

        private void Save()
        {
            Save_btn.Click();
        }

        public void Cancel()
        {
            Cancel_btn.Click();
        }

        public void RemoveUploadFile()
        {
            Remove_btn.WaitForElementIsVisible(3);
            Remove_btn.Click();
        }

    }
}
