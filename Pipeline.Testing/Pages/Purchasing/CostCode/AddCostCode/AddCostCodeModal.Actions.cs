namespace Pipeline.Testing.Pages.Purchasing.CostCode.AddCostCode
{
    public partial class AddCostCodeModal
    {
        public AddCostCodeModal EnterName(string name)
        {
            if (!string.IsNullOrEmpty(name))
                Name_txt.SetText(name);
            return this;
        }

        public AddCostCodeModal EnterDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Description_txt.SetText(description);
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
    }
}