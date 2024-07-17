
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.OptionSelectionGroup.OptionSelectionGroupDetail
{
    public partial class OptionSelectionGroupDetailPage
    {
        public bool IsTitlOptionSelectionGroup(string title)
        {
            if (OptionSelectionGroupTitle_lbl == null || OptionSelectionGroupTitle_lbl.IsDisplayed() == false
                || OptionSelectionTitle_lbl == null || OptionSelectionTitle_lbl.IsDisplayed() == false)
            {
                throw new Exception("Not found " + OptionSelectionGroupTitle_lbl.GetText() + " element");
            }
            return SubHeadTitle() == title;
        }

        public bool IsUpdateOptionSelectionGroupSuccessful(OptionSelectionGroupData data)
        {
            return SubHeadTitle().Equals(data.Name) && !CurrentURL.EndsWith("cid=0");
        }

        public bool IsUpdateDataCorrectly(OptionSelectionGroupData data)
        {
            if (!SubHeadTitle().Equals(data.Name))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Sub Head title: {data.Name}. Actual Sub Head title: {SubHeadTitle()}");
                return false;
            }
            if (data.Name != OptionSelectionGroupName_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Name result: {data.Name}. Actual result: {OptionSelectionGroupName_txt.GetValue()}");
                return false;
            }
            if (data.SortOrder != SortOrder_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Sort Order result: {data.SortOrder}. Actual result: {SortOrder_txt.GetValue()}");
                return false;
            }
            return true;
        }
    }
}
