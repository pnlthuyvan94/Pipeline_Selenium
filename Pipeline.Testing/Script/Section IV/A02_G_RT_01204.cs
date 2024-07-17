using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes;
using Pipeline.Testing.Pages.Assets.Communities.MoveInReadyHomes.MoveInReadyResource;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_G_RT_01204 : BaseTestScript
    {
        private class MoveInReadySourceData
        {
            public string UploadImageName { get; set; }
            public string FeatureName { get; set; }
            public string FeaturerDescription { get; set; }
            public string UploladDocumentName { get; set; }

            public MoveInReadySourceData()
            {
                UploadImageName = string.Empty;
                FeatureName = string.Empty;
                FeaturerDescription = string.Empty;
                UploladDocumentName = string.Empty;
            }
        }

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private MoveInReadyHomesData moveInReadyHomesData;
        private CommunityData communityData;
        private MoveInReadySourceData moveInReadySourceData;

        [SetUp] // Pre-condition
        public void GetOldTestData()
        {
            communityData = new CommunityData()
            {
                //Name = TestData["Name"],
                Name = "R-QA Only Community Auto",
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

            moveInReadyHomesData = new MoveInReadyHomesData()
            {
                House = "English Cottage",
                Lot = "RT-QA_Lot_1",
                Status = "Not Yet Started",
                Price = "100000",
                Address = "14 Tan Hai",
                Basement = "1",
                FirstFloor = "2",
                SecondFloor = "3",
                Heated = "4",
                Total = "10",
                Style = "Single Family",
                Story = "2",
                Bedroom = "2",
                Bathroom = "3.5",
                Garage = "1 Car",
                Note = "No Description",
                IsModalHome = "TRUE",
            };

            moveInReadySourceData = new MoveInReadySourceData()
            {
                UploadImageName = string.Empty,
                FeatureName = string.Empty,
                FeaturerDescription = string.Empty,
                UploladDocumentName = string.Empty
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



        [Test]
        [Category("Section_IV")]
        [Ignore("The MoveInReadyHomes was removed from Community detail page, so this test sript will be ignored.")]
        public void A02_G_Assets_DetailPage_Community_MoveInReadyHomes()
        {
            // Step 3: Open Move in Ready Home navigation
            MoveInReadyHomesPage.Instance.LeftMenuNavigation("Move-In Ready Homes");
            string expectedMoveInReadyHomePage = "/Dashboard/Builder/Communities/MoveIns";
            Assert.That(MoveInReadyHomesPage.Instance.CurrentURL.Contains(expectedMoveInReadyHomePage), "Move in Ready Home page isn't displayed");

            // Get upload information from excel

            // Step 4: Click Save button to add new Move in ready Home
            AddMoveInReadyHome(moveInReadyHomesData);

            // Step 5: Upload Move in Ready Resource
            UploadMoveInReadyResource(moveInReadySourceData);

            // Step 6: Remove Move in ready Resource
            DeleteMoveInReadyResource(moveInReadySourceData);

            // Step 7: Back to Move In Ready Home page and delete item
            MoveInReadyHomesPage.Instance.LeftMenuNavigation("Move-In Ready Homes");

            // Remove Move in ready home from community: Expecte: Delete successful
            RemoveMoveInReadyHomeFromCommunity(communityData.Name, moveInReadyHomesData.House);
        }

        private void AddMoveInReadyHome(MoveInReadyHomesData moveInReadyHomesData)
        {
            // Click Add button
            MoveInReadyHomesPage.Instance.PressAddNewMoveInReadyHome();

            // Verify: The the new view is displayed
            Assert.That(MoveInReadyHomesPage.Instance.IsAddMoveInReadyHomeGridDisplay(), "The Add move in ready resource home grid view should be displayes after click Add button.");

            MoveInReadyHomesPage.Instance.AddNewMoveInReadyHome(moveInReadyHomesData);

            // Verify new data
            if (MoveInReadyHomesPage.Instance.IsSaveMoveInReadyHomeSuccessful(moveInReadyHomesData))
                ExtentReportsHelper.LogPass("Move In Ready Homes is saves successfully with correct data.");
            else
                ExtentReportsHelper.LogFail("Move in Ready Home is saved with incorrect data.");

            // Verify Display Move-In Ready Resource grid view
            Assert.That(MoveInReadyResourceGrid.Instance.IsAddMoveInReadyResourceGridDisplay(), "The Move-In Ready Resource grid view doesn't display after saving a Move-In Resource Home.");
            ExtentReportsHelper.LogPass("Move in Ready Resource is displayed below Move in Ready Home grid view.");
        }

        private void UploadMoveInReadyResource(MoveInReadySourceData moveInReadySourceData)
        {
            // Scroll down and click Add button
            MoveInReadyResourceGrid.Instance.AddNewMoveInReadyResource();

            // Verify new upload grid was display
            Assert.That(MoveInReadyResourceGrid.Instance.IsUploadResourceGridDisplay(), "The upload Resource grid isn't displayed.");
            ExtentReportsHelper.LogPass("The upload Resource grid is displayes with current tab is Images.");

            /****************************************** Upload Images ******************************************/
            ExtentReportsHelper.LogInformation("Upload Imgae Home Resource.");

            string ImageFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\DataInputFiles\Resources\";
            string imagePath = ImageFolder + moveInReadySourceData.UploladDocumentName.ToString().Replace(",", " \n " + ImageFolder);
            MoveInReadyResourceGrid.Instance.SelectImage(imagePath).UploadImage();


            /****************************************** Upload Features ******************************************/
            // Scroll down and click Add button
            MoveInReadyResourceGrid.Instance.AddNewMoveInReadyResource();

            ExtentReportsHelper.LogInformation("Upload Features Home Resource.");
            MoveInReadyResourceGrid.Instance.SwitchResourceTab("Features").EnterFeatures(moveInReadySourceData.FeatureName)
                .EnterFeatureDescription(moveInReadySourceData.FeaturerDescription).SaveFeature();


            /****************************************** Upload Document ******************************************/
            ExtentReportsHelper.LogInformation("Upload Document Home Resource.");

            string DocumentFolder = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\DataInputFiles\Documents\";
            string documentPath = DocumentFolder + moveInReadySourceData.UploladDocumentName.ToString().Replace(",", " \n " + DocumentFolder);
            MoveInReadyResourceGrid.Instance.SwitchResourceTab("Documents").SelectDocument(documentPath).UploadDocument();
        }


        private void DeleteMoveInReadyResource(MoveInReadySourceData moveInReadySourceData)
        {
            /****************************************** Upload Images ******************************************/
            ExtentReportsHelper.LogInformation("Remove Image Home Resource.");
            string[] uploadImageFiles = moveInReadySourceData.UploadImageName.ToString().Split(',');

            // Scroll down
            MoveInReadyResourceGrid.Instance.ScrollDown();

            foreach (string ImageFile in uploadImageFiles)
            {
                FilterAndRemoveResource(ImageFile.Split('.')[0]);
            }


            /****************************************** Upload Features ******************************************/
            ExtentReportsHelper.LogInformation("Remove Features Home Resource.");
            FilterAndRemoveResource(moveInReadySourceData.FeatureName);


            /****************************************** Upload Document ******************************************/
            ExtentReportsHelper.LogInformation("Remove Document Home Resource.");
            string[] uploadDocumentFiles = moveInReadySourceData.UploladDocumentName.ToString().Split(',');

            // Scroll down
            MoveInReadyResourceGrid.Instance.ScrollDown();

            foreach (string documentFile in uploadDocumentFiles)
            {
                FilterAndRemoveResource(documentFile.Split('.')[0]);
            }
        }

        private void FilterAndRemoveResource(string resourceTitle)
        {
            // Filter item in the grid view and delete it

            MoveInReadyResourceGrid.Instance.FilterItemInGrid("Title", GridFilterOperator.EqualTo, resourceTitle);

            bool isFoundOldItem = MoveInReadyResourceGrid.Instance.IsItemInGrid("Title", resourceTitle);
            if (!isFoundOldItem)
            {
                ExtentReportsHelper.LogFail($"Can't find {resourceTitle} from Move-In Ready Resource!");
                return;
            }
            Assert.That(isFoundOldItem, string.Format($"The uploaded resource: \"{resourceTitle}\" was not display on grid."));

            MoveInReadyResourceGrid.Instance.DeleteItemInGrid("Title", resourceTitle);
            string expectedMess = "Deleted successfully";
            string actualMsg = MoveInReadyResourceGrid.Instance.GetLastestToastMessage();

            if (expectedMess.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Resource: {resourceTitle} deleted successfully from Move-In Ready Resource!");
                MoveInReadyResourceGrid.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogFail($"Resource: {resourceTitle} could not be deleted from Move-In Ready Resource! Actual message {actualMsg}");
                MoveInReadyResourceGrid.Instance.CloseToastMessage();
            }
        }

        private void RemoveMoveInReadyHomeFromCommunity(string communityName, string houseName)
        {
            MoveInReadyHomesPage.Instance.DeleteItemInGrid("Name", houseName);
            MoveInReadyHomesPage.Instance.WaitGridLoad();

            string expectedDeleteSuccessfulMess = $"Spec Home {houseName} deleted successfully!";

            // Expected: delete successful
            if (expectedDeleteSuccessfulMess == MoveInReadyHomesPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass($"Move In Ready Home with house name: {houseName} removed successfully from community {communityName}!");
                MoveInReadyHomesPage.Instance.CloseToastMessage();
            }
            else
            {
                if (MoveInReadyHomesPage.Instance.IsItemInGrid("Name", houseName))
                    ExtentReportsHelper.LogFail($"Move In Ready Home with house name:  {houseName} could not be deleted from community {communityName}!");
                else
                    ExtentReportsHelper.LogPass($"Move In Ready Home with house name: {houseName} deleted successfully from community {communityName}!");
            }
        }

        [TearDown]
        public void CleanUpData()
        {
            ExtentReportsHelper.LogInformation($"Try to clean up data to prepare next run");
            MoveInReadyHomesPage.Instance.LeftMenuNavigation("Move-In Ready Homes");
            if (MoveInReadyHomesPage.Instance.IsItemInGrid("Name", moveInReadyHomesData.House))
            {
                MoveInReadyHomesPage.Instance.OpenItemInGrid("Name", moveInReadyHomesData.House);
                MoveInReadyResourceGrid.Instance.DeleteAllItemInGrid();
                MoveInReadyHomesPage.Instance.LeftMenuNavigation("Move-In Ready Homes");
                MoveInReadyHomesPage.Instance.DeleteItemInGrid("Name", moveInReadyHomesData.House);
                MoveInReadyHomesPage.Instance.WaitGridLoad();
                MoveInReadyHomesPage.Instance.GetLastestToastMessage();
                MoveInReadyHomesPage.Instance.CloseToastMessage();
            }
            else
                ExtentReportsHelper.LogInformation($"All data has been clean up successfully.");


            // Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Back to Community default page and delete data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);
        }
    }
}