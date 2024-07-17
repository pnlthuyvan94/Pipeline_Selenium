using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.CommunityVendor.AddVendor
{
    public partial class CommunityVendorModal
    {
        public CommunityVendorModal SelectCommunityVendor(string[] listCommunityVendor)
        {
            if (listCommunityVendor.Length != 0)
                CommunityVendor_lst.SetChecked(GridFilterOperator.Contains, listCommunityVendor);
            return this;
        }

        /// <summary>
        /// Select Vendor by Name
        /// </summary>
        /// <param name="vendor"></param>
        /// <returns></returns>
        public CommunityVendorModal SelectCommunityVendorByName(string vendor)
        {
            CommunityVendor_lst.SetChecked(GridFilterOperator.Contains, vendor);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhases']/div[1]");
        }

        public void Cancel()
        {
            Close_btn.Click();
        }
    }
}