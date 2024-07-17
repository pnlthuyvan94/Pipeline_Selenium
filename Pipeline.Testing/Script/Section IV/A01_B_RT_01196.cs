using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Divisions;
using Pipeline.Testing.Pages.Assets.Divisions.DivisionDetail;
using Pipeline.Testing.Pages.Assets.Divisions.Location;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A01_B_RT_01196 : BaseTestScript
    {
        private class DivisionLocationData
        {
            public string Latitude { get; set; }
            public string Longitude { get; set; }
            public string Area { get; set; }

            public DivisionLocationData()
            {
                Latitude = string.Empty;
                Longitude = string.Empty;
                Area = string.Empty;
            }
        }

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private DivisionData _division;
        private DivisionLocationData locationData_Update;

        [SetUp]
        public void CreateTestData()
        {
            _division = new DivisionData()
            {
                Name = "R-QA Only Division Auto - Location",
                Address = "3990 IN 38",
                City = "Lafayette",
                State = "IN",
                Zip = "47905",
                Description = "Create a new Division by automation",
            };

            locationData_Update = new DivisionLocationData()
            {
                Latitude = "40.3835653294955",
                Longitude = "-86.8325550651511",
                Area = "Vista Point Estates"
            };
        }


        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A01_B_Assets_DetailPage_Division_Location()
        {
            // Step 1: navigate to this URL: http://beta.bimpipeline.com/Dashboard/Builder/Divisions/Default.aspx
            DivisionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Divisions);

            // Step 2: Filter and verify Division on the grid
            DivisionPage.Instance.FilterItemInGrid("Division", GridFilterOperator.Contains, _division.Name);
            if (DivisionPage.Instance.IsItemInGrid("Division", _division.Name) is false)
            {
                // if the division doesn't exist then create a new one
                DivisionPage.Instance.CreateDivision(_division);
            }
            else
            {
                // Select filter item to open detail page
                DivisionPage.Instance.SelectItemInGrid("Division", _division.Name);
                System.Threading.Thread.Sleep(2000);
            }

            //Verify the updated Division in header
            if (DivisionDetailPage.Instance.IsSaveDivisionSuccessful(_division.Name) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The updated Division '{_division.Name}' displays unsuccessfully on the Subtitle.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Division detail page is displayed sucessfully with URL: {DivisionDetailPage.Instance.CurrentURL}</b></font>");


            // Step 3: Open Location navigation
            LocationPage.Instance.LeftMenuNavigation("Location");
            if (LocationPage.Instance.IsLocationPageDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Set Divisional Google Map modal isn't displayed.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Set Divisional Google Map page is displayed sucessfully.</b></font>");

            CommonHelper.RefreshPage();
            // Step 4: Click + - and zoom icon (capture and manual test)
            VerifyMapByCaptureImage();

            // Step 5: Populate new longtitude and lingitude values
            LocationPage.Instance.UpdateLocation(locationData_Update.Latitude, locationData_Update.Longitude);
            if (LocationPage.Instance.IsUpdatedLocationSuccessfully(locationData_Update.Latitude, locationData_Update.Longitude) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Location updated successfully.</b></font>");

            // Verify message
            var expectedMessage = $"Division {_division.Name} location saved successfully!";
            if (!expectedMessage.Equals(LocationPage.Instance.GetLastestToastMessage()))
            {
                ExtentReportsHelper.LogFail($"Could not update location with Latitude {locationData_Update.Latitude} and Longitude {locationData_Update.Longitude}.");
            }

            // Step 6: Delete Latitude or Longitude
            SaveWithEmptyValue(locationData_Update.Latitude, locationData_Update.Longitude);

        }

        private void VerifyMapByCaptureImage()
        {
            LocationPage.Instance.ZoomInLocation();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Capture and log for manual testing purpose, Zoom in location");

            LocationPage.Instance.ZoomOutLocation();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Capture and log for manual testing purpose, Zoom out location");

            LocationPage.Instance.FullSceenLocation();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Capture and log for manual testing purpose, Full screen map");
            LocationPage.Instance.ExitFullSceenLocation();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Capture and log for manual testing purpose, Exit full screen map");
        }

        private void SaveWithEmptyValue(string latitude, string longitude)
        {
            // Empty Latitude
            LocationPage.Instance.UpdateLocation(string.Empty, longitude);

            var expectedNoLatitudeMessage = "Please enter a numeric value for Latitude.";
            if (expectedNoLatitudeMessage != LocationPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Shouldn't update location with empty Latitude and Longitude {longitude} successfully." +
                    $"<br>Expected: {expectedNoLatitudeMessage}" +
                    $"<br>Actual: {LocationPage.Instance.GetLastestToastMessage()}</br></font>");
            }
            LocationPage.Instance.CloseToastMessage();

            // Empty Longitude
            LocationPage.Instance.UpdateLocation(latitude, string.Empty);

            var expectedNoLongtitudeMessage = "Please enter a numeric value for Longitude.";
            if (expectedNoLongtitudeMessage != LocationPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Shouldn't update location with Latitude {latitude} and empty Longitude successfully." +
                    $"<br>Expected: {expectedNoLongtitudeMessage}" +
                    $"<br>Actual: {LocationPage.Instance.GetLastestToastMessage()}</br></font>");
            }
            LocationPage.Instance.CloseToastMessage();

        }

        #endregion

        [TearDown]
        public void DeleteData()
        {
            // Back to Division page and delete it
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Back to Division page and delete it.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_DIVISION_URL);
            DivisionPage.Instance.DeleteDivision(_division);
        }
    }
}


