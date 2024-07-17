namespace Pipeline.Testing.Pages.Estimating.Uses.AddUses
{
    public partial class AddUsesModal
    {
        public AddUsesModal EnterUsesName(string name)
        {
            UsesName_txt.SetText(name);
            return this;
        }

        public AddUsesModal EnterUsesDescription(string description)
        {
            UsesDescription_txt.SetText(description);
            return this;
        }

        public AddUsesModal EnterUseSortOrder(string sortOrder)
        {
            UsesSortOrder_txt.SetText(sortOrder);
            return this;
        }

        public void FillDataAndSave(UsesData data)
        {
            EnterUsesName(data.Name).EnterUsesDescription(data.Description).EnterUseSortOrder(data.SortOrder).Save();
        }

        public void Save()
        {
            UsesSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgUses']");
        }

        public void CloseModal()
        {
            UsesClose_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
