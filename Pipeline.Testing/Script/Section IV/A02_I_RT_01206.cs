using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.Sitemap;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_I_RT_01206 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CommunityData communityData;

        [SetUp] // Pre-condition
        public void GetOldTestData()
        {
            communityData = new CommunityData()
            {
                //Name = TestData["Name"],
                Name = "R-QA Only Community Auto_Test SiteMap",
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

        }


        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A02_I_Assets_DetailPage_Community_Sitemap()
        {
            // Step 1.1: Navigate to Community page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Community default page.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Step 1.2: Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Filter community with name {communityData.Name} and create if it doesn't exist.<b></b></font>");
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

            // Step 2: Open Sitemap navigation
            SitemapPage.Instance.LeftMenuNavigation("Sitemap");
            if (SitemapPage.Instance.IsSitemapPageDisplay() is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Sitemap page displays correctly with URL: {SitemapPage.Instance.CurrentURL}</b></font>");


            // Step 3: Click Add button and verify  
            SitemapPage.Instance.Add();

            // Verify: The upload grid view is displayed
            if (SitemapPage.Instance.IsUploadGridDisplay() is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The upload grid view should be displayes after click Add button.</font>");


            // Step 4: Upload pdf, jpg, gif files
            string[] uploadFileName = { "Test_01.pdf", "Test_02.jpg", "Test_03.gif" };
            foreach (string item in uploadFileName)
            {
                ExtentReportsHelper.LogInformation($"Select file: {item} to upload.");
                SitemapPage.Instance.UploadSitemapFile($@"\DataInputFiles\Assets\Sitemap\{item}");

                // Verify file is uploaded successfully
                if (SitemapPage.Instance.IsUploadFileSuccessful(item) is false)
                    ExtentReportsHelper.LogFail($"<font color='red'>Sitemap file {item} is failed to uploaded.</br></font>");
                else
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Sitemap file {item} is uploaded successfully.</b></font>");


                // Remove file
                ExtentReportsHelper.LogInformation($"Remove upload file: {item}.");
                SitemapPage.Instance.RemoveUploadFile();

                // Verify upload file is removed successfully
                if (SitemapPage.Instance.IsRemoveFileSuccessful() is false)
                    ExtentReportsHelper.LogFail($"<font color='red'>Sitemap file {item} is failed to removed.</font>");
                else
                    ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Sitemap file {item} is removed successfully.</b></font>");
            }
        }
        #endregion


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