using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Resources;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_A_RT_01198 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CommunityData communityData;
        private CommunityData communityData_Update;

        [SetUp]
        public void CreateTestData()
        {
            communityData = new CommunityData()
            {
                Name = "R-QA Only Community Auto - Community Detail",
                Division = "None",
                Code = "Community_Code_Detail",
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
                Slug = "R-QA-Only-Community-Auto-slug",
            };

            communityData_Update = new CommunityData()
            {
                Name = "R-QA Only Community Auto- Community Detail Update",
                Division = "CG Visions",
                Code = "Community_Code_Detail Update",
                City = "Ho Chi Minh_Update",
                CityLink = "https://hcmUpdate.com",
                Township = "Tan Binh_Update",
                County = "VN_Update",
                State = "IN_Update",
                Zip = "01011",
                SchoolDistrict = "Hoang hoa tham_Update",
                SchoolDistrictLink = "http://hht.com",
                Status = "Coming Soon",
                Description = "Community from automation test v1_Update",
                DrivingDirections = "Community from automation test v2_Update",
                Slug = "R-QA-Only-Community-Auto-slug_Update",
            };

            // Step 1: Navigate to Community page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1: Navigate to Community default page.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);


            // Step 2.1 Filter the updated community name and delete it before updating on step 3
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.1 Filter the updated community name '{communityData_Update.Name}' and delete it before updating on step 3.<b></b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData_Update.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData_Update.Name) is true)
                CommunityPage.Instance.DeleteCommunity(communityData_Update.Name);

            // Step 2.2: Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 2.2: Filter community with name {communityData.Name} and create if it doesn't exist.<b></b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (!CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);
            }
            else
            {
                //Delete data before create new data
                CommunityPage.Instance.DeleteCommunity(communityData.Name);
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);
            }
        }


        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A02_A_Assets_DetailPage_Community_CommunityDetail()
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 3: Update community.<b></b></font>");
            // Update community - Click 'Save' Button
            CommunityDetailPage.Instance.AddOrUpdateCommunity(communityData_Update, true);

            // Step 4. Verify updated community in header
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 4. Verify updated community in header.<b></b></font>");

            if (CommunityDetailPage.Instance.IsSaveCommunitySuccessful(communityData_Update.Name) is false)
                ExtentReportsHelper.LogFail($"Create new Community unsuccessfully. The title isn't updated with new value '{communityData_Update.Name}'.");
            else
                ExtentReportsHelper.LogPass($"Community is updated sucessfully with URL: {CommunityDetailPage.Instance.CurrentURL}");

            // Step 5. Verify data saved successfully
            if (CommunityDetailPage.Instance.IsSaveCommunityData(communityData_Update) is true)
                ExtentReportsHelper.LogPass($"Community is updated sucessfully with valid data");

            // Step 6. Upload community image
            // UploadCommunityImageAndVeriy();

            // Back to detail page 
            ExtentReportsHelper.LogInformation("Back to Community detail page and add lot status.");
            ResourcePage.Instance.LeftMenuNavigation("Community Details");

            // Step 7. Add lot Status button
            //   ExtentReportsHelper.LogInformation("Step7");
            //   AddLotStatus(TestData_RT01198);
        }

        private void UploadCommunityImageAndVeriy()
        {
            // Upload community image
            ExtentReportsHelper.LogInformation($"Community is uploading image to resource");
            string[] imageName = { "CommunityImage.jpg", "gifFile.gif", "jpegFile.jpeg", "pngFile.png", "bmpFile.bmp", "mpegFile.mpeg", "tifFile.tif" };

            foreach (string item in imageName)
            {
                bool isUploadSuccessful = CommunityDetailPage.Instance.UploadCommunityImage($@"\DataInputFiles\Resources\{item}");
                if (item.EndsWith(".bmp") || item.EndsWith(".mpeg") || item.EndsWith(".tif"))
                {
                    if (!isUploadSuccessful)
                    {
                        // Expected: Cannot upload with file ".bmp", ".mpeg", ".tif" files
                        ExtentReportsHelper.LogPass($"Can't upload image: {item} with extension is '.bmp', '.mpeg', '.tif'.");
                    }
                    else
                    {
                        // Actual: upload successful => log fail and delete it
                        ExtentReportsHelper.LogFail($"the image: {item} should be uploaded unsuccessful. Remove image in the resource page.");

                        // Navigate to resource page and delete it
                        NavigateToResourcePage();
                        FilterAndRemoveResource(item);

                        // Back to community detail 
                        ResourcePage.Instance.LeftMenuNavigation("Community Details");

                    }
                }
                else // The Other files
                {
                    if (isUploadSuccessful)
                    {
                        // Expected: Upload file successful => log pass and delete it
                        ExtentReportsHelper.LogPass($"the image: {item} uploaded successful then remove it in the resource page.");

                        // Navigate to resource page and delete it
                        NavigateToResourcePage();

                        FilterAndRemoveResource(item);

                        // Back to community detail 
                        ResourcePage.Instance.LeftMenuNavigation("Community Details");
                    }
                    else
                    {
                        // Actual: can't upload file => log fail
                        ExtentReportsHelper.LogFail($"The image: {item} can't be uploaded.");
                    }
                }
            }
        }

        public static void NavigateToResourcePage()
        {
            // Verify new image in the Resource page
            ResourcePage.Instance.LeftMenuNavigation("Resources");
            string expectedResourcePage = "/Dashboard/Builder/Communities/Resources.aspx?cid=";
            Assert.That(ResourcePage.Instance.CurrentURL.Contains(expectedResourcePage), "Resource page isn't displayed");
        }

        public static void FilterAndRemoveResource(string deletedItem)
        {
            // Filter item in the grid view and delete it
            if (!ResourcePage.Instance.IsItemInGrid("Source", deletedItem))
            {
                ExtentReportsHelper.LogFail($"Resource name: {deletedItem} doesn't display in the grid view");
            }
            else
            {
                ExtentReportsHelper.LogPass($"Resource name: {deletedItem} display in the grid view then delete it");

                ResourcePage.Instance.DeleteItemInGrid("Source", deletedItem);
                ResourcePage.Instance.WaitGridLoad();

                string expectedMess = $"{deletedItem.Split('.')[0]} was deleted successfully.";
                if (expectedMess == ResourcePage.Instance.GetLastestToastMessage())
                {
                    ExtentReportsHelper.LogPass($"Resource name: {deletedItem} deleted successfully!");
                    ResourcePage.Instance.CloseToastMessage();
                }
                else
                {
                    if (ResourcePage.Instance.IsItemInGrid("Source", deletedItem))
                        ExtentReportsHelper.LogFail($"Resource name: {deletedItem} could not be deleted!");
                    else
                        ExtentReportsHelper.LogPass($"Resource name: {deletedItem} deleted successfully!");
                }
            }
        }



        [TearDown]
        public void DeleteCommunity()
        {
            // Step 5: Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5: Back to Community default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            // Filter updated community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData_Update.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData_Update.Name))
                CommunityPage.Instance.DeleteCommunity(communityData_Update.Name);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);

        }

        #endregion
    }
}