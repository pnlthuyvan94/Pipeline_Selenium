namespace Pipeline.Testing.Pages.Purchasing.Trades.AddTrade
{
    public partial class AddTradeModal
    {
        public AddTradeModal EnterTradeName(string data)
        {
            if (!string.IsNullOrEmpty(data))
                TradeName_txt.SetText(data);
            return this;
        }

        public AddTradeModal EnterCode(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Code_txt.SetText(data);
            return this;
        }

        public AddTradeModal EnterTradeDescription(string data)
        {
            if (!string.IsNullOrEmpty(data))
                TradeDescription_txt.SetText(data);
            return this;
        }
        public AddTradeModal ClickQualifyVendor()
        {
            QualifyVendor_rbtn.Click();
            return this;
        }

        public AddTradeModal ClickQualifyBuilderVendor()
        {
            QualifyBuilderVendor_rbtn.Click();
            return this;
        }
        private AddTradeModal SelectCompanyVendor(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                CompanyVendor_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }
        private AddTradeModal SelectVendor(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                Vendor_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }

        private AddTradeModal SelectBuilderVendor(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                BuilderVendor_dll.SelectItemByValueOrIndex(data, index);
            return this;
        }

        private AddTradeModal SelectBuildingPhases(string data, int index = 1)
        {
            if (!string.Empty.Equals(data))
                BuildingPhases_ddl.SelectItemByValueOrIndex(data, index);
            return this;
        }

        private AddTradeModal SelectSchedulingTasks(string data, int index = 1)
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

        public void AddTrade(TradesData data, bool isOldModal = false)
        {
            EnterTradeName(data.TradeName)
            .EnterCode(data.Code)
            .EnterTradeDescription(data.TradeDescription);

            if (isOldModal == false)
            {
                if (data.IsBuilderVendor)
                {
                    ClickQualifyBuilderVendor();
                    SelectBuilderVendor(data.BuilderVendor);
                }
                else
                {
                    ClickQualifyVendor();
                    SelectVendor(data.Vendor)
                        .SelectBuildingPhases(data.BuildingPhases);
                }
            }
            else
            {
                SelectCompanyVendor(data.Vendor)
                    .SelectBuildingPhases(data.BuildingPhases);
            }

            SelectSchedulingTasks(data.SchedulingTasks)
                .Save();
        }
    }
}