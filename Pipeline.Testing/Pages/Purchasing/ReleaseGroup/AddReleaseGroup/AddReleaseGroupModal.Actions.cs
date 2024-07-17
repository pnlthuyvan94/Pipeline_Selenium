namespace Pipeline.Testing.Pages.Purchasing.ReleaseGroup.AddReleaseGroup
{
    public partial class AddReleaseGroupModal
    {
        public AddReleaseGroupModal EnterName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Name_txt.SetText(data);
            return this;
        }

        public AddReleaseGroupModal EnterDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Description_txt.SetText(description);
            return this;
        }

        public AddReleaseGroupModal EnterSortOrder(string order)
        {
            if (!string.IsNullOrEmpty(order))
                SortOrder_txt.SetAttribute("value", order);
            return this;
        }

        public void Save()
        {
            Save_btn.Click(false);
        }

        public void Close()
        {
            Close_btn.Click(false);
        }

        public void CreateNewReleaseGroup(ReleaseGroupData data)
        {
            EnterName(data.Name)
                .EnterDescription(data.Description)
                .EnterSortOrder(data.SortOrder)
                .Save();
        }
    }
}
