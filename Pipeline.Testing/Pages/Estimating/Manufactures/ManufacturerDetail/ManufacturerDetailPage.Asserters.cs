using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Manufactures.ManufacturerDetail
{
    public partial class ManufacturerDetailPage
    {
        /*
         * Verify head title and id of new manufacturer
         */
        public bool IsSaveManufactureSuccessful(string manufacturerName)
        {
            return SubHeadTitle().Equals(manufacturerName) && !CurrentURL.EndsWith("mid=0");
        }

        public bool IsDisplayDataCorrectly(ManufacturerData data)
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
            if (data.Url != Url_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Url result: {data.Url}. Actual result: {Url_txt.GetValue()}");
                return false;
            }
            return true;
        }

        public bool IsDisplayAddButton()
        {
            return ManufacturerAdd_btn.WaitForElementIsVisible(5);
        }

    }
}
