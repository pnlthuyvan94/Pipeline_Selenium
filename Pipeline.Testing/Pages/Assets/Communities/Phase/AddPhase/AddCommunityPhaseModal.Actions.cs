namespace Pipeline.Testing.Pages.Assets.Communities.Phase.AddPhase
{
    public partial class AddCommunityPhaseModal
    {
        private AddCommunityPhaseModal EnterPhaseName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        private AddCommunityPhaseModal EnterPhaseCode(string code)
        {
            if (!string.IsNullOrEmpty(code))
                Code_txt.SetText(code);
            return this;
        }

        private AddCommunityPhaseModal SelectStatus(string status)
        {
            if (!string.Empty.Equals(status))
                Status_ddl.SelectItem(status, false);
            return this;
        }


        private AddCommunityPhaseModal EnterSortOrder(string sortOrder)
        {
            if (!string.IsNullOrEmpty(sortOrder))
                SortOrder_txt.SetText(sortOrder);
            return this;
        }

        private AddCommunityPhaseModal EnterDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Description_txt.SetText(description);
            return this;
        }

        private void Add()
        {
            AddPhase_btn.Click();
        }

        public void Close()
        {
            if(Close_btn.WaitForElementIsVisible(15))
                Close_btn.Click();
        }

        public void AddPhase(CommunityPhaseData data)
        {
            EnterPhaseName(data.Name).EnterPhaseCode(data.Code).SelectStatus(data.Status)
                .EnterSortOrder(data.SortOrder).EnterDescription(data.Description)
                .Add();
        }
    }
}