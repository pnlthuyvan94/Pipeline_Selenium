using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductResources.AddProductResource
{
    public partial class AddProductResourceModal
    {
        private AddProductResourceModal EnterResourceTitle(string title)
        {
            Title_txt.SetText(title);
            return this;
        }

        private AddProductResourceModal EnterSource(string source)
        {
            Source_txt.SetText(source);
            return this;
        }

        private AddProductResourceModal SetPrimaryResource(bool status)
        {
            PrimaryResource_ckb.SetCheck(status);
            return this;
        }


        public void Save()
        {
            Save_btn.Click();
            PageLoad();
        }

        public void CloseModal()
        {
            Close_btn.Click();
        }

        public void UploadResourceToProduct(ProductResourceData data, string sourceName)
        {
            // Select and upload file
            data.Source = sourceName;

            // Can't upload resource with empty title
            if (data.Title == string.Empty || data.Source == string.Empty)
            {
                ExtentReportsHelper.LogFail("Can't upload resource with empty title or source.");
            }

            string sourcePath = $"{data.Source}";

            // Enter title, source and primary resource
            EnterResourceTitle(data.Title).EnterSource(data.Source).SetPrimaryResource(data.PrimaryResource).Save();
            PageLoad();

        }
    }
}
