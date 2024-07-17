using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.Location;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_B_RT_01199 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CommunityData communityData;
        private const string LATITUDE_UPDATE = "40.3835653294955";
        private const string LONGITUDE_UPDATE = "Longitude_Update";


        [SetUp] // Pre-condition
        public void GetOldTestData()
        {
            communityData = new CommunityData()
            {
                Name = "R-QA Only Community Auto_Test_Location",
                Division = "None",
                City = "Ho Chi Minh",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Community from automation test v1",
                DrivingDirections = "Community from automation test v2",
                Slug = "R-QA-Only-Community-Auto - slug",
            };

            // Step 1: Navigate to Community page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Community default page.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Step 2: Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2: Filter community with name {communityData.Name} and create if it doesn't exist.<b></b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (!CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);
            }
            else
            {
                // Select filter item to open detail page
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
            }
        }


        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A02_B_Assets_DetailPage_Community_Location()
        {
            // Step 3: Open Location navigation
            CommunityLocationPage.Instance.LeftMenuNavigation("Location");
            if (CommunityLocationPage.Instance.IsLocationPageDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The Community Location displays unsuccessfully or title is incorrect.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Community Location page displays correctly with URL: {CommunityLocationPage.Instance.CurrentURL}</b></font>");


            // Step 4: Click + - and zoom icon (capture and manual test)
            VerifyCommunityLocationMapByCaptureImage();

            // Step 5: Populate new longtitude and lingitude values
            CommunityLocationPage.Instance.UpdateLocation(LATITUDE_UPDATE, LONGITUDE_UPDATE);
            Assert.That(CommunityLocationPage.Instance.IsUpdatedLocationSuccessfully(LATITUDE_UPDATE, LONGITUDE_UPDATE), "The Location updated unsuccessfully.");
            if (CommunityLocationPage.Instance.IsUpdatedLocationSuccessfully(LATITUDE_UPDATE, LONGITUDE_UPDATE) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Location updated successfully.</b></font>");


            // Step 6: Delete Latitude or Longitude
            SaveCommunityLocationWithEmptyValue(LATITUDE_UPDATE, LONGITUDE_UPDATE);
        }

        #endregion

        private void VerifyCommunityLocationMapByCaptureImage()
        {
            CommunityLocationPage.Instance.ZoomInLocation();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Capture and log for manual testing purpose, Zoom in location");

            CommunityLocationPage.Instance.ZoomOutLocation();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Capture and log for manual testing purpose, Zoom out location");

            CommunityLocationPage.Instance.FullSceenLocation();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Capture and log for manual testing purpose, Full screen map");

            CommunityLocationPage.Instance.ExitFullSceenLocation();
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(), "Capture and log for manual testing purpose, Exit full screen map");
        }

        private void SaveCommunityLocationWithEmptyValue(string latitude, string longitude)
        {
            // Latitude is 0 
            CommunityLocationPage.Instance.UpdateLocation("0", longitude);
            if (CommunityLocationPage.Instance.IsUpdatedLocationSuccessfully("0", longitude) is true)
                ExtentReportsHelper.LogPass($"The Location updated unsuccessfully with Latitude = 0 and Longtitude = {longitude}");

            // Longitude is 0
            CommunityLocationPage.Instance.UpdateLocation(latitude, "0");
            if (CommunityLocationPage.Instance.IsUpdatedLocationSuccessfully(latitude, "0") is true)
                ExtentReportsHelper.LogPass($"The Location updated unsuccessfully with Longitude = 0 and latitude = {latitude}");

            // Latitude and Longitude are 0
            CommunityLocationPage.Instance.UpdateLocation("0", "0");
            if (CommunityLocationPage.Instance.IsUpdatedLocationSuccessfully("0", "0") is true)
                ExtentReportsHelper.LogPass($"The Location updated unsuccessfully with Longitude = 0 and latitude = 0");
        }

        /// <summary>
        /// This function is used after  the request R-02774 fixed
        /// </summary>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        //private void SaveCommunityLocationWithEmptyValue(string latitude, string longitude)
        //{
        //    // Empty Latitude 
        //    CommunityLocationPage.Instance.UpdateLocation(string.Empty, longitude);

        //    var expectedNoLatitudeMessage = "Please enter a numeric value for Latitude.";
        //    if (expectedNoLatitudeMessage != CommunityLocationPage.Instance.LastestToastMessage)
        //    {
        //        ExtentReportsHelper.LogFail($"Shouldn't update location with empty Latitude and Longitude {longitude} successfully.");
        //        Assert.Fail($"Shouldn't update location with empty Latitude and Longitude {longitude} successfully \n Expected: {expectedNoLatitudeMessage} \n Actual: {LocationPage.Instance.LastestToastMessage}");
        //    }
        //    CommunityLocationPage.Instance.CloseToastMessage();

        //    // Empty Longitude 
        //    LocationPage.Instance.UpdateLocation(latitude, string.Empty);

        //    var expectedNoLongtitudeMessage = "Please enter a numeric value for Longitude.";
        //    if (expectedNoLongtitudeMessage != LocationPage.Instance.LastestToastMessage)
        //    {
        //        ExtentReportsHelper.LogFail($"Shouldn't update location with Latitude {latitude} and empty Longitude successfully.");
        //        Assert.Fail($"Shouldn't update location with Latitude {latitude} and empty Longitude successfully \n Expected: {expectedNoLongtitudeMessage} \n Actual: {LocationPage.Instance.LastestToastMessage}");
        //    }
        //    LocationPage.Instance.CloseToastMessage();
        //}

        [TearDown]
        public void DeleteCommunity()
        {
            // Step 5: Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5: Back to Community default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);
        }
    }
}
