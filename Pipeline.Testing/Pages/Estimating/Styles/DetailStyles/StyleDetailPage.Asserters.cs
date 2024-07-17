using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Styles.DetailStyles
{
    public partial class StyleDetailPage
    {

        // Verify manufacturer combo box display new item correct or not
        public bool DisplayCorrectManufacturer(string expectedManunfacturerName)
        {
            return Manufacturer_ddl != null && Manufacturer_ddl.SelectedItemName.Equals(expectedManunfacturerName);
        }

        public bool IsUpdateDataCorrectly(StyleData data)
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
            if (data.Manufacturer != Manufacturer_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Manufacturer result: {data.Manufacturer}. Actual result: {Manufacturer_ddl.GetValue()}");
                return false;
            }
            if (data.Url != Url_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Url result: {data.Url}. Actual result: {Url_txt.GetValue()}");
                return false;
            }
            if (data.Description != Description_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Description result: {data.Description}. Actual result: {Description_txt.GetValue()}");
                return false;
            }
            return true;
        }

        public bool IsDisplayAddButton()
        {
            return AddStyle_btn.IsDisplayed();
        }

        public bool IsSubHeaderCorrect(string styleName)
        {
            if (!SubHeadTitle().Equals(styleName))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Subheader result: {styleName}. Actual result: {SubHeadTitle()}");
                return false;
            }
            return true;
        }
        public bool IsManufacturerInList(string manufacturer)
        {
            if (Manufacturer_ddl.IsItemInList(manufacturer))
            {
                ExtentReportsHelper.LogInformation($"Items {manufacturer} is displayed in list");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Items {manufacturer} is not displayed in list");
                return false;
            }
        }
    }
}
