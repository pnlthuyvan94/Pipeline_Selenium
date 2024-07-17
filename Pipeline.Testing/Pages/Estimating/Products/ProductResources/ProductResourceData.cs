namespace Pipeline.Testing.Pages.Estimating.Products.ProductResources
{
    public class ProductResourceData
    {
        public ProductResourceData()
        {
            Title = string.Empty;
            UpdatedTitle = string.Empty;
            Source = string.Empty;
            PrimaryResource = false;
        }

        public string Title { get; set; }
        public string UpdatedTitle { get; set; }
        public string Source { get; set; }
        public bool PrimaryResource { get; set; }
    }
}