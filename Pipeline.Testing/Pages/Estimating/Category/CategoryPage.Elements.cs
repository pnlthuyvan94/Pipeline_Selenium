using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.Category.AddCategory;


namespace Pipeline.Testing.Pages.Estimating.Category
{
    public partial class CategoryPage : DashboardContentPage<CategoryPage>
    {
        public AddCategoryPage AddCategoryModal { get; private set; }
        protected IGrid Category_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCategories_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCategories']/div[1]");
    }
}
