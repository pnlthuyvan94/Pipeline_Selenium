using OpenQA.Selenium;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;

namespace Pipeline.Testing.Pages.Assets.OptionRooms.OptionRoonDetail
{
    public partial class OptionRoomDetailPage
    {

        public bool IsDisplayDataCorrectly(OptionRoomData data)
        {
            if (!SubHeadTitle().Equals(data.Name))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Subheader result: {data.Name}. Actual result: {SubHeadTitle()}. <br> This issue will be fixed on this request R-02954 </br>");
                return false;
            }
            if (data.Name != Name_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Name result: {data.Name}. Actual result: {Name_txt.GetValue()}");
                return false;
            }
            if (data.SortOrder != SortOrder_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Abbreviation result: {data.SortOrder}. Actual result: {SortOrder_txt.GetValue()}");
                return false;
            }
            return true;
        }
    }
}
