
namespace Pipeline.Testing.Pages.Pathway.Assets
{
    public class AssetsData
    {

        public string Name { get; set; }
        public string AssetType { get; set; }
        public string AssetGroup { get; set; }
        public string Length { get; set; }
        public string Width { get; set; }

        public AssetsData()
        {
            Name = string.Empty;
            AssetType = string.Empty;
            AssetGroup = string.Empty;
            Length = string.Empty;
            Width = string.Empty;
        }
    }
}