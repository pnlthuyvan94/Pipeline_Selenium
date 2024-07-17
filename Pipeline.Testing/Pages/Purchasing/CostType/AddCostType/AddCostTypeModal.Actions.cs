namespace Pipeline.Testing.Pages.Purchasing.CostType.AddCostType
{
    public partial class AddCostTypeModal
    {
        public AddCostTypeModal EnterName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Name_txt.SetText(data);
            return this;
        }

        public AddCostTypeModal EnterDescription(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Description_txt.SetText(data);
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

        public void AddCostType(CostTypeData data)
        {
            EnterName(data.Name).EnterDescription(data.Description).Save();
        }
    }
}