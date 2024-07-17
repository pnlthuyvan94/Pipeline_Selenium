using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityOption
{
    public partial class AddCommunityOptionModal
    {
        public bool IsCommunityOptionModalDisplayed()
        {
            return AddCommunityOptionTitle_lbl.WaitForElementIsVisible(5);
        }

        public bool IsAddCommunityOptionSuccessful(CommunityOptionData data)
        {
            bool result = true;
            // Successful in case sale price is empty and option don't display anymore
            if (!string.IsNullOrEmpty(SalePrice_txt.GetText()))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: Sales price field should be empty. Current value: {SalePrice_txt.GetText()}");
                result = false;
            }
            if (AllHouseOptions_lst.IsItemExisted(Common.Enums.GridFilterOperator.EndsWith, data.AllHouseOptions))
                result = false;
            return result;
        }
    }
}
