using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Divisions.DivisionCommunity.AddCommunity
{
    public partial class DivisionCommunityModal
    {
        public DivisionCommunityModal SelectDivisionCommunity(string[] listDivsionCommunity)
        {
            if (listDivsionCommunity.Length != 0)
                DivisionCommunity_lst.SetChecked(GridFilterOperator.Contains, listDivsionCommunity);
            return this;
        }

        /// <summary>
        /// Select community by name
        /// </summary>
        /// <param name="communityName"></param>
        /// <returns></returns>
        public DivisionCommunityModal SelectDivisionCommunityByName(string communityName)
        {
            DivisionCommunity_lst.SetChecked(GridFilterOperator.Contains, communityName);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath(_loadingGifApplyingCommunity, 2);
        }

        public void Cancel()
        {
            Close_btn.Click();
        }
    }
}