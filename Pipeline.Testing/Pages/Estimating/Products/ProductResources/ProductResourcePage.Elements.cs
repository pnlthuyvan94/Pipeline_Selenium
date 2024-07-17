using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.Products.ProductResources.AddProductResource;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Products.ProductResources
{
    public partial class ProductResourcePage : DetailsContentPage<ProductResourcePage>
    {
        public AddProductResourceModal AddProductResourceModal { get; private set; }

        protected IGrid Resource_Grid => new Grid(FindType.XPath,"//*[@id='ctl00_CPH_Content_rgProductResources_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgProductResources']/div[1]");

        protected Button InsertResource_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddResource']");
    }
}
