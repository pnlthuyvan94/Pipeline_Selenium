using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using RestSharp.Contrib;
using System;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A02_RT_01074 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        CommunityData _community;

        [SetUp]
        public void CreateTestData()
        {
            // Step 3: Populate all values
            _community = new CommunityData()
            {
                Name = "R-QA Only Community Auto Test",
                Division = "None",
                City = "Ho Chi Minh",
                Code= "R-QA Only Community Auto Test",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Nothing to say v1",
                DrivingDirections = "Nothing to say v2",
                Slug = "R-QA-Only-Community-Auto",
            };

        }

        [Test]
        [Category("Section_III")]
        public void A02_Assets_AddACommunity()
        {
            // Step 1: Navigate to community menu
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Navigate to community menu</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Step 2: Check if community exist
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0: Check if community exist</b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is false)
            {

                // Step 3: 
                //Click on "+" Add button
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Click on + Add button Navigate to community menu</b></font>");
                CommunityPage.Instance.GetItemOnHeader(DashboardContentItems.Add).Click();
                var _expectedCreateCommunityURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_COMMUNITY_URL;
                if (CommunityDetailPage.Instance.IsPageDisplayed(_expectedCreateCommunityURL) is false)
                {
                    ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>Community create page isn't displayed</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Community create page is displayed</b></font>");
                }

                //Create Community - Click 'Save' Button
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4.0: Create Community - Click 'Save' Button</b></font>");
                CommunityDetailPage.Instance.AddOrUpdateCommunity(_community);

                string _expectedMessage = $"Could not create Community with name {_community.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name {_community.Name}.");
                }

                // Step 5. Verify new Community in header
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: Verify new Community in header</b></font>");
                if (CommunityDetailPage.Instance.IsSaveCommunitySuccessful($"{_community.Name} ({_community.Code})") is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create new Community unsuccessfully..</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Community is created sucessfully with URL: {CommunityDetailPage.Instance.CurrentURL}");
                }

                // Step 6. Verify data saved successfully
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0: Verify data saved successfully.</b></font>");
                if (CommunityDetailPage.Instance.IsSaveCommunityData(_community) is true)
                    ExtentReportsHelper.LogPass($"Community is create sucessfully with valid data");
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7.0: Community exist</b></font>");
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);
            }


            // Step 8. Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.0: Insert name to filter and click filter by Contain value.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is false)
                ExtentReportsHelper.LogFail($"New Community \"{_community.Name} \" was not display on grid.");
            else 
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);

            //Step 9 Verify if vendor link is no longer displayed under costing section above Bid Costs link
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.0: Verify if vendor link is no longer displayed under costing section above Bid Costs link.</b></font>");
            CommonHelper.CaptureScreen();

            //Step 10.When directly pasting the URL to the browser, the page redirects to Pipeline’s dashboard
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.0: When directly pasting the URL to the browser, the page redirects to Pipeline’s dashboard.</b></font>");
            string currentURL = BaseFullUrl;
            Uri theUri = new Uri(currentURL);
            string cid = HttpUtility.ParseQueryString(theUri.Query).Get("cid");
            string view_community_vendor_URL = "/Costing/Builder/Communities/VendorsToCommunities.aspx?cid=" + cid;
            CommonHelper.OpenURL(BaseDashboardUrl + view_community_vendor_URL);
            CommonHelper.CaptureScreen();
        }


        [TearDown]
        public void DeleteCommunity()
        {
            // Back to community default page and delete data
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Insert name to filter and click filter by Contain value
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);

            bool isFound = CommunityPage.Instance.IsItemInGrid("Name", _community.Name);
            if (isFound)
            {
                CommunityPage.Instance.DeleteItemInGrid("Name", _community.Name);
                string successfulMess = $"Community {_community.Name} deleted successfully!";
                string actual = CommunityPage.Instance.GetLastestToastMessage();
                if (successfulMess.Equals(actual))
                {
                    ExtentReportsHelper.LogPass("Updated Community deleted successfully!");
                    CommunityPage.Instance.CloseToastMessage();
                }
                else
                {
                    if (!CommunityPage.Instance.IsItemInGrid("Name", _community.Name))
                        ExtentReportsHelper.LogPass("The Updated community deleted successfully!");
                    else
                        ExtentReportsHelper.LogFail($"The updated community could not be deleted. Actual message <i>{actual}</i>");
                }
            }
        }
    }
}
