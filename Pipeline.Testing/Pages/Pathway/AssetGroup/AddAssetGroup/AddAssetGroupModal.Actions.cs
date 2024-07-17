namespace Pipeline.Testing.Pages.Pathway.AssetGroup.AddAssetGroup
{
    public partial class AddAssetGroupModal
    {
        public AddAssetGroupModal EnterName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }


        public void Save()
        {
            Save_btn.Click();
            WaitGridLoad();
        }


        public void CloseModal()
        {
            Close_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
