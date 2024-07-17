namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase.AddCutoffPhase
{
    public partial class AddCutoffPhaseModal
    {
        public AddCutoffPhaseModal EnterName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Name_txt.SetText(data);
            return this;
        }

        public AddCutoffPhaseModal EnterCode(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Code_txt.SetText(data);
            return this;
        }

        public AddCutoffPhaseModal EnterSortOrder(string data)
        {
            if (!string.IsNullOrEmpty(data))
                SortOrder_txt.SetAttribute("value", data);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitGridLoad();
        }

        public void CreateNewCutoffPhase(CutoffPhaseData data)
        {
            EnterCode(data.Code)
                .EnterName(data.Name)
                .EnterSortOrder(data.SortOrder)
                .Save();
        }
    }
}
