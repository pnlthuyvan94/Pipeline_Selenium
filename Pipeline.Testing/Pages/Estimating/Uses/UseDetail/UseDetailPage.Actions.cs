using Pipeline.Testing.Pages.Estimating.Uses.AddUses;

namespace Pipeline.Testing.Pages.Estimating.Uses.UseDetail
{
    public partial class UseDetailPage
    {
        public UseDetailPage EnterUseName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }

        public UseDetailPage EnterUseSortOrder(string url)
        {
            SortOrder_txt.SetText(url);
            return this;
        }

        public UseDetailPage EnterUseDescription(string description)
        {
            Description_txt.SetText(description);
            return this;
        }

        public void Save()
        {
            ManufacturerSave_btn.Click();

            // Wait loading hide
            WaitingLoadingGifByXpath(_gridLoading);
            PageLoad();
        }


        public void UpdateUse(UsesData data)
        {
            EnterUseName(data.Name).EnterUseDescription(data.Description).EnterUseSortOrder(data.SortOrder).Save();
        }

    }
}
