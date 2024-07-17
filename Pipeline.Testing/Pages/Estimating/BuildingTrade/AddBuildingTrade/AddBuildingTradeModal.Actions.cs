namespace Pipeline.Testing.Pages.Estimating.BuildingTrade.AddBuildingTrade
{
    public partial class AddBuildingTradeModal
    {
        public AddBuildingTradeModal EnterName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Name_txt.SetText(data);
            return this;
        }

        public AddBuildingTradeModal EnterCode(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Code_txt.SetText(data);
            return this;
        }

        public AddBuildingTradeModal EnterDescription(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Description.SetText(data);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
        }

        public void AddBuildingTrade(BuildingTradeData data)
        {
            EnterCode(data.Code)
                .EnterName(data.Name)
                .EnterDescription(data.Description)
                .Save();

        }
    }
}