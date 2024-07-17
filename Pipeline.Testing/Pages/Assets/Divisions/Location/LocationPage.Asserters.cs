
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Assets.Divisions.Location
{
    public partial class LocationPage
    {
        public bool IsUpdatedLocationSuccessfully(string latitude, string longitude)
        {
            if (!Latitude_txt.GetValue().Equals(latitude))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result for Latitude: {latitude}. \n Actual result: {Latitude_txt.GetValue()}");
                return false;
            }
            if (!Longitude_txt.GetValue().Equals(longitude))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected result for Longitude: {longitude}. \n Actual result: {Longitude_txt.GetValue()}");
                return false;
            }

            return true;
        }

        public bool IsLocationPageDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (LocationModelTilte_lbl == null || LocationModelTilte_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("Set Divisional Google Map modal is not displayed.");
                    }
                }
                return (LocationModelTilte_lbl.GetText() == "Set Divisional Google Map");
            }
        }

    }
}
