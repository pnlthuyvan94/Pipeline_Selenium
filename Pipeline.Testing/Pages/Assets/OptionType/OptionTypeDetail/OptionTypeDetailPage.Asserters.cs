
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.OptionType.OptionTypeDetail
{
    public partial class OptionTypeDetailPage
    {
        public bool IsTitleOptionType(string title)
        {
            if (OptionTypeTitle_lbl == null || OptionTypeTitle_lbl.IsDisplayed() == false
                || OptionTitle_lbl == null || OptionTitle_lbl.IsDisplayed() == false)
            {
                throw new Exception("Not found " + OptionTypeTitle_lbl.GetText() + " element or" + OptionTitle_lbl.GetText() + " element.");
            }
            return SubHeadTitle() == title;
        }

        public bool IsUpdateOptionTypeSuccessful(OptionTypeData data)
        {
            return SubHeadTitle().Equals(data.Name) && !CurrentURL.EndsWith("cid=0");
        }

        public bool IsUpdateDataCorrectly(OptionTypeData data)
        {
            bool inputFlexPlan = data.IsFlexPlan.ToString().ToLower() == "true" ? true : false;
            bool inputIsPathwayVisible = data.IsPathwayVisible.ToString().ToLower() == "true" ? true : false;
            if (!SubHeadTitle().Equals(data.Name))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Sub Head title: {data.Name}. Actual Sub Head title: {SubHeadTitle()}");
                return false;
            }
            if (data.Name != Name_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Name result: {data.Name}. Actual result: {Name_txt.GetValue()}");
                return false;
            }
            if (data.SortOrder != SortOrder_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Sort Order result: {data.SortOrder}. Actual result: {SortOrder_txt.GetValue()}");
                return false;
            }
            if (data.DisplayName != DisplayName_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Display Name result: {data.DisplayName}. Actual result: {DisplayName_txt.GetValue()}");
                return false;
            }
            if (inputIsPathwayVisible != IsPathwayVisible_ckb.IsChecked)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Is Pathway visible result: {inputIsPathwayVisible}. Actual result: {IsPathwayVisible_ckb.IsChecked}");
                return false;
            }
            if (inputFlexPlan != IsFlexPlan_ckb.IsChecked)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Flex Plan result: {inputFlexPlan}. Actual result: {IsFlexPlan_ckb.IsChecked}");
                return false;
            }
            return true;
        }
    }
}
