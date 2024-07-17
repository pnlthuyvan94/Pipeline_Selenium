namespace Pipeline.Testing.Pages.Pathway.Assets.AssetsDetail
{
    public partial class AssetsDetailPage
    {
        public AssetsDetailPage EnterName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        public AssetsDetailPage SelectAssetType(string type)
        {
            AssetType_ddl.SelectItem(type, true);
            return this;
        }

        public AssetsDetailPage SelectAssetGroup(string group)
        {
            AssetGroup_ddl.SelectItem(group, true);
            return this;
        }

        public AssetsDetailPage EnterMeasurementInFeet(string length, string width)
        {
            Length_txt.SetText(length);
            Width_txt.SetText(width);
            return this;
        }


        public void Save()
        {
            Save_btn.Click();
            PageLoad();
        }

        public void CreateAnAsset(AssetsData data)
        {
            EnterName(data.Name).SelectAssetType(data.AssetType)
                                .SelectAssetGroup(data.AssetGroup)
                                .EnterMeasurementInFeet(data.Length, data.Width)
                                .Save();
        }
    }
}
