using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddCommunityHouseOption
{
    public partial class AddCommunityHouseOptionModal
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (AddCommunityHouseOption_lbl == null || AddCommunityHouseOption_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("/'Add Options/' modal is not displayed.");
                    }
                }
                return (AddCommunityHouseOption_lbl.GetText() == "Add Options" && OptionGroup_ddl != null && IsAddOption_btn.IsClickable());
            }
        }

        public bool IsAddCommunityHouseOptionSuccessful(CommunityHouseOptionData data)
        {
            // Successful in case sale price is empty and option don't display anymore
            if (string.Empty != SalePrice_txt.GetText())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result: Sales price field should be empty.");
                return false;
            }
            if (AllHouseOptions_lst.IsItemExisted(Common.Enums.GridFilterOperator.EndsWith, data.AllHouseOptions))
            {
                return false;
            }
            return true;
        }
    }
}
