using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityHouseOption
{
    public partial class AddCommunityHouseOptionModal
    {
        private AddCommunityHouseOptionModal SelectOptionGroup(string optionGroup)
        {
            if (!string.Empty.Equals(optionGroup))
                OptionGroup_ddl.SelectItem(optionGroup, false);
            return this;
        }

        private AddCommunityHouseOptionModal SelectCostGroup(string costGroup)
        {
            if (!string.Empty.Equals(costGroup))
                CostGroup_ddl.SelectItem(costGroup, false);
            return this;
        }

        private AddCommunityHouseOptionModal EnterSalePrice(string price)
        {
            if (!string.IsNullOrEmpty(price))
                SalePrice_txt.SetText(price);
            return this;
        }

        private AddCommunityHouseOptionModal SelectHouseOptions(string[] houseOptionList)
        {
            if (houseOptionList.Length != 0)
                AllHouseOptions_lst.SetChecked(GridFilterOperator.Contains, houseOptionList);
            return this;
        }

        private AddCommunityHouseOptionModal SelectOtherMasterOptions(string[] otherMasterOptionList)
        {
            if (otherMasterOptionList.Length != 0)
                OtherMasterOptions_lst.Select(GridFilterOperator.StartsWith, otherMasterOptionList);
            return this;
        }

        private AddCommunityHouseOptionModal IsStandard(bool value)
        {
            if (value && !Standard_btn.IsClickable())
            {
                Standard_btn.Click();
            }
            return this;
        }

        private AddCommunityHouseOptionModal IsAvailableToAllHouse(bool value)
        {
            if (value && !Standard_btn.IsClickable())
            {
                AvailableToAllHouse_btn.Click();
            }
            return this;
        }

        private void Add()
        {
            AddCommunityHouseOptionToCommunity_btn.Click();
        }

        public void Close()
        {
            Close_btn.Click();
        }

        public void AddCommunityHouseOption(CommunityHouseOptionData data)
        {
            SelectOptionGroup(data.OptionGroup).SelectCostGroup(data.CostGroup)
                .SelectHouseOptions(data.AllHouseOptions).SelectOtherMasterOptions(data.OtherMasterOptions)
                .IsStandard(data.IsStandard).IsAvailableToAllHouse(data.IsAvailableToAllHouse)
                .EnterSalePrice(data.SalePrice)
                .Add();
        }
    }
}