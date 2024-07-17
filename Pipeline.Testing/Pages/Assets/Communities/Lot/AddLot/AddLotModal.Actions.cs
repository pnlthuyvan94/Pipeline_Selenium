namespace Pipeline.Testing.Pages.Assets.Communities.Lot.AddLot
{
    public partial class AddLotModal
    {
        public AddLotModal EnterLotNumber(string number)
        {
            if (!string.IsNullOrEmpty(number))
                LotNumber_txt.SetText(number);
            return this;
        }

        public AddLotModal SelectStatus(string status)
        {
            if (!string.Empty.Equals(status))
            {
                Status_ddl.SelectItem(status, false);
            }
            return this;
        }

        public AddLotModal SelectCommunitypPhase(string communityPhase)
        {
            if (!string.Empty.Equals(communityPhase))
            {
                CommunityPhase_ddl.SelectItem(communityPhase, false);
            }
            return this;
        }

        public AddLotModal EnterAcre(string acre)
        {
            if (!string.IsNullOrEmpty(acre))
                Acre_txt.SetText(acre);
            return this;
        }

        public AddLotModal EnterCost(string cost)
        {
            if (!string.IsNullOrEmpty(cost))
                Cost_txt.SetText(cost);
            return this;
        }

        public AddLotModal EnterMarkup(string markup)
        {
            if (!string.IsNullOrEmpty(markup))
                Markup_txt.SetText(markup);
            return this;
        }

        public AddLotModal EnterPremium(string premium)
        {
            if (!string.IsNullOrEmpty(premium))
                premium_txt.SetText(premium);
            return this;
        }

        public AddLotModal EnterWidth(string width)
        {
            if (!string.IsNullOrEmpty(width))
                Width_txt.SetText(width);
            return this;
        }

        public AddLotModal EnterDepth(string depth)
        {
            if (!string.IsNullOrEmpty(depth))
                Depth_txt.SetText(depth);
            return this;
        }

        public AddLotModal EnterAddress(string address)
        {
            if (!string.IsNullOrEmpty(address))
                Address_txt.SetText(address);
            return this;
        }

        public AddLotModal EnterCity(string city)
        {
            if (!string.IsNullOrEmpty(city))
                City_txt.SetText(city);
            return this;
        }

        public AddLotModal EnterState(string state)
        {
            if (!string.IsNullOrEmpty(state))
                State_txt.SetText(state);
            return this;
        }

        public AddLotModal EnterZip(string zip)
        {
            if (!string.IsNullOrEmpty(zip))
                Zip_txt.SetText(zip);
            return this;
        }

        public AddLotModal EnterLatitude(string latitude)
        {
            if (!string.IsNullOrEmpty(latitude))
                Latitude_txt.SetText(latitude);
            return this;
        }

        public AddLotModal EnterLongitude(string longitude)
        {
            if (!string.IsNullOrEmpty(longitude))
                Longitude_txt.SetText(longitude);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
        }

        public void Cancel()
        {
           if (Close_btn.WaitForElementIsVisible(5)==true)
                Close_btn.Click();
        }

        public void AddLot(LotData data)
        {
            EnterLotNumber(data.Number).SelectStatus(data.Status).SelectCommunitypPhase(data.CommunityPhase)
                .EnterAcre(data.Acre).EnterCost(data.Cost).EnterMarkup(data.Markup)
                .EnterPremium(data.Premium).EnterWidth(data.Width).EnterDepth(data.Depth)
                .EnterAddress(data.Address).EnterCity(data.City).EnterState(data.State)
                .EnterZip(data.Zip).EnterLatitude(data.Latitude).EnterLongitude(data.Longitude)
                .Save();
        }
    }
}