namespace Pipeline.Testing.Pages.Assets.Communities.AvailablePlan.AddHouseToCommunity
{
    public partial class AddHouseToCommunityModal
    {
        private AddHouseToCommunityModal SelectHouse(string house)
        {
            if (!string.Empty.Equals(house))
            {
                House_ddl.SelectItem(house);
                WaitingLoadingGifByXpath(_loadingGiftSelectHouse, 2);
            }
            return this;
        }

        private AddHouseToCommunityModal EnterHousePrice(string price)
        {
            if (!string.IsNullOrEmpty(price))
                Price_txt.SetText(price);
            return this;
        }

        private AddHouseToCommunityModal EnterHouseNote(string note)
        {
            if (!string.IsNullOrEmpty(note))
                Note_txt.SetText(note);
            return this;
        }

        private void Save()
        {
            Save_btn.Click();
        }

        public void Cancel()
        {
            if (Close_btn.WaitForElementIsVisible(10) == true)
                Close_btn.Click();
        }

        public void AddHouseToCommunity(AvaiablePlanData houseData)
        {
            SelectHouse(houseData.Id + "-" + houseData.Name).EnterHousePrice(houseData.Price).EnterHouseNote(houseData.Note)
                .Save();
        }
    }
}