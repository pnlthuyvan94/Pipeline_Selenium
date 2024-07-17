using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;

namespace Pipeline.Testing.Pages.Estimating.Uses.UseDetail
{
    public partial class UseDetailPage
    {

        public bool IsDisplayDataCorrectly(UsesData data)
        {
            if (!SubHeadTitle().Equals(data.Name))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Subheader result: {data.Name}. Actual result: {SubHeadTitle()}");
                return false;
            }
            if (data.Name != Name_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Name result: {data.Name}. Actual result: {Name_txt.GetValue()}");
                return false;
            }
            if (data.Description != Description_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Description result: {data.Description}. Actual result: {Description_txt.GetValue()}");
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
