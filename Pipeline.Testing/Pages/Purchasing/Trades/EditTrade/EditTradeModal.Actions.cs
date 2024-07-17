namespace Pipeline.Testing.Pages.Purchasing.Trades.EditTrade
{
    public partial class EditTradeModal
    {
        public EditTradeModal EnterTradeName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                TradeName_txt.SetText(data);
            return this;
        }

        public EditTradeModal EnterCode(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Code_txt.SetText(data);
            return this;
        }

        public EditTradeModal EnterTradeDescription(string data)
        {
            if (!string.IsNullOrEmpty(data))
                TradeDescription_txt.SetText(data);
            return this;
        }

        private EditTradeModal SelectCompanyVendor(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                CompanyVendor_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }

        private EditTradeModal SelectBuildingPhases(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                BuildingPhases_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }

        private EditTradeModal SelectSchedulingTasks(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                SchedulingTasks_ddl.SelectItemByValueOrIndex(data, index);
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

        public void SaveUpdatedTrade(TradesData data)
        {
            EnterTradeName(data.TradeName)
            .EnterCode(data.Code)
            .EnterTradeDescription(data.TradeDescription)
            .SelectCompanyVendor(data.Vendor)
            .SelectBuildingPhases(data.BuildingPhases)
            .SelectSchedulingTasks(data.SchedulingTasks)
            .Save();
        }
    }
}