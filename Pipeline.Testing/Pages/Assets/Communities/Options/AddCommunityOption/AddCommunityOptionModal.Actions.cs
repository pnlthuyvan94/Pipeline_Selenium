using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityOption
{
    public partial class AddCommunityOptionModal
    {
        public AddCommunityOptionModal EnterOption(string option)
        {
            Option_txt.SetText(option);
            return this;
        }

        private AddCommunityOptionModal EnterSalePrice(string price)
        {
            if (!string.IsNullOrEmpty(price))
                SalePrice_txt.SetText(price);
            return this;
        }

        private AddCommunityOptionModal SelectHouseOptions(string[] houseOptionList)
        {
            if (houseOptionList.Length != 0)
                AllHouseOptions_lst.SetChecked(GridFilterOperator.Contains, houseOptionList);
            return this;
        }

        private AddCommunityOptionModal SelectOtherMasterOptions(string[] otherMasterOptionList)
        {
            if (otherMasterOptionList.Length != 0)
                OtherMasterOptions_lst.SetChecked(GridFilterOperator.Contains, otherMasterOptionList);
            return this;
        }

        private AddCommunityOptionModal IsStandard(bool value)
        {
            if (value && !Standard_btn.IsClickable())
            {
                Standard_btn.Click();
            }
            return this;
        }

        private AddCommunityOptionModal IsAvailableToAllHouse(bool value)
        {
            if (value == true)
            {
                AvailableToAllHouse_btn.Click();
            }
            return this;
        }

        private void Add()
        {
            AddCommunityOptionToCommunity_btn.Click();
        }

        public void Close()
        {
            Close_btn.Click();
        }

        public void AddCommunityOption(CommunityOptionData data)
        {
            SelectHouseOptions(data.AllHouseOptions).SelectOtherMasterOptions(data.OtherMasterOptions)
                .IsStandard(data.isStandard).IsAvailableToAllHouse(data.isAvailableToAllHouse)
                .EnterSalePrice(data.SalePrice)
                .Add();
        }
    }
}