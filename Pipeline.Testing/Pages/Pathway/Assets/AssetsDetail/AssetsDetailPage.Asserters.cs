

namespace Pipeline.Testing.Pages.Pathway.Assets.AssetsDetail
{
    public partial class AssetsDetailPage
    {
        public bool IsSaveAssetsSuccessful(string assetName)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedUserName = $"{assetName}";
            return SubHeadTitle().Equals(expectedUserName) && !CurrentURL.EndsWith("aid=0");
        }
    }
}
