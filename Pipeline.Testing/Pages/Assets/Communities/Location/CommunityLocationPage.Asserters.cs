
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Communities.Location
{
    public partial class CommunityLocationPage
    {
        public bool IsUpdatedLocationSuccessfully(string latitude, string longitude)
        {
            bool result = true;
            if (!Latitude_txt.GetValue().Equals(latitude))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result for Latitude: {latitude}. <br>Actual result: {Latitude_txt.GetValue()}");
                result = false;
            }
            if (!Longitude_txt.GetValue().Equals(longitude))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result for Longitude: {longitude}. <br>Actual result: {Longitude_txt.GetValue()}");
                result = false;
            }

            return result;
        }

        public bool IsLocationPageDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (Map_lbl == null || Map_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("Set Divisional Google Map modal is not displayed.");
                    }
                }
                return true;
            }
        }
    }
}
