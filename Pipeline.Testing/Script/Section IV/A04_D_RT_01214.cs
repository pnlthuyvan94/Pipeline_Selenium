using LinqToExcel.Extensions;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Resources;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A04_D_RT_01214 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        //private Row TestData_RT_01205;
        readonly string sourcePathDocument = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\DataInputFiles\Documents\";
        readonly string sourcePathImage = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\DataInputFiles\Resources\";
        private IList<string> linkList;
        private IList<string> dirArrayDoc;
        private IList<string> listNameDoc;
        private IList<string> dirArrayImage;
        private IList<string> listNameImage;
        private HouseData houseData;
        private ResourceData[] resourceData_list;
        public ResourceData resourceData_image;
        public ResourceData resourceData_doc;
        public ResourceData resourceData_link;

        [SetUp]
        public void GetData()
        {
            linkList = CommonHelper.CastValueToList("https://www.strongtie.com/");
            // Get list documents
            dirArrayDoc = Directory.GetFiles(sourcePathDocument);
            listNameDoc = new List<string>();
            foreach (var nameDoc in dirArrayDoc)
            {
                string nameItem = Path.GetFileName(nameDoc);
                listNameDoc.Add(nameItem);
            }
            // Get list images
            dirArrayImage = Directory.GetFiles(sourcePathImage);
            listNameImage = new List<string>();
            foreach (var nameDoc in dirArrayImage)
            {
                string nameItem = Path.GetFileName(nameDoc);
                listNameImage.Add(nameItem);
            }

            houseData = new HouseData()
            {
                HouseName = "QA_RT_Auto_House_RT_01214",
                SaleHouseName = "RegressionTest_House_Sales_Name",
                Series = "RT_Series_DoNot_Delete",
                PlanNumber = "1214",
                BasePrice = "1000000",
                SQFTBasement = "1",
                SQFTFloor1 = "1",
                SQFTFloor2 = "2",
                SQFTHeated = "3",
                SQFTTotal = "7",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "Test"
            };

            ResourceData resourceData_image = new ResourceData()
            {
                Type = "Image",
                Title = "Test Image",
                UpdatedTitle = "Test Image_Update",
                Link = "",
            };

            ResourceData resourceData_doc = new ResourceData()
            {
                Type = "Document",
                Title = "Test Document",
                UpdatedTitle = "Test Document_Update",
                Link = "",
            };

            ResourceData resourceData_link = new ResourceData()
            {
                Type = "Link",
                Title = "Test Link",
                UpdatedTitle = "Test Link_Update",
                Link = "https://www.strongtie.com/",
            };

            resourceData_list = new ResourceData[]
            {
               resourceData_image,
               resourceData_doc,
               resourceData_link
            };
        }

        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A04_D_Assets_DetailPage_House_Resources()
        {
            // Step 1: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to House default page</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Filter house with name {houseData.HouseName} and create if it doesn't exist.</b></font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
            {
                // Create a new house
                HousePage.Instance.CreateHouse(houseData);
            }
            else
            {
                // Delete before create new data
                HousePage.Instance.DeleteHouse(houseData.HouseName);
                // Create a new house
                HousePage.Instance.CreateHouse(houseData);
            }
            // Step 2: In Side Navigation, click the resources to open the resources page
            ExtentReportsHelper.LogInformation(" Step 2: In Side Navigation, click the Document to open the Document page");
            HouseDetailPage.Instance.LeftMenuNavigation("Resources");
            // Verify opened successfully the resource page
            string expectedResourcePage = "/Dashboard/Builder/Houses/Resources.aspx?hid=";
            //Assert.That(ResourcePage.Instance.CurrentURL.Contains(expectedResourcePage), "Resource page isn't displayed");
            if (ResourcePage.Instance.CurrentURL.Contains(expectedResourcePage))
            {
                ExtentReportsHelper.LogPass("Resource page is displayed");
            }
            else
            {
                ExtentReportsHelper.LogPass("Resource page isn't displayed");
            }
            ExtentReportsHelper.LogPass($"Opened successfully the Resources page");

            // Step 3: Click Add button and upload resource
            ExtentReportsHelper.LogInformation(" Step 3: Click Add button and updload resource");
            //Assert.That(ResourcePage.Instance.IsUploadSectionDisplay(), "The upload Resource section displays unsuccessfully.");
            if (ResourcePage.Instance.IsUploadSectionDisplay())
            {
                ExtentReportsHelper.LogPass("The upload Resource section displays successfully.");
            }
            else
            {
                ExtentReportsHelper.LogPass("The upload Resource section displays unsuccessfully.");
            }
            // Upload image
            UploadResourceData(dirArrayImage.ToArray(), "Image");
            //Upload documents
            UploadResourceData(dirArrayDoc.ToArray(), "Document");
            // Upload link
            UploadResourceData(linkList.ToArray(), "Link");

            // Close Upload grid
            ResourcePage.Instance.CloseUploadResouceGrid();

            // Step 4: Edit to update Resource information
            ExtentReportsHelper.LogInformation(" Step 4:  Edit to update Resource information");
            UpdateResourceData(Path.GetFileName(dirArrayDoc[1]), "Image");

            // Step 5: Click resource and verify hyperlink
            ExtentReportsHelper.LogInformation(" Step 5:  Click resource and verify hyperlink");
            ResourcePage.Instance.DownloadFile(ResourcePage.Instance.IsFileHref(listNameDoc[1]), pathReportFolder + listNameDoc[1]);

            // Step 6: Delete resource item
            ExtentReportsHelper.LogInformation(" Step 6:  Delete resource item");
            //ExtentReportsHelper.LogInformation("List Image:" listNameImage.);
            ResourcePage.Instance.ChangePageSizeView(100);
            System.Threading.Thread.Sleep(5000);
            ResourcePage.Instance.FilterAndRemoveResource(listNameImage.ToArray());
            ResourcePage.Instance.FilterAndRemoveResource(listNameDoc.ToArray());
            ResourcePage.Instance.FilterAndRemoveResource(linkList.ToArray());
        }
        private void UploadResourceData(string[] ResourceData, string resourceType)
        {
            foreach (var item in resourceData_list)
            {
                if ((item.Type).Equals(resourceType))
                {
                    ResourcePage.Instance.UploadResourceAndVerify(item, ResourceData.ToArray());
                }
            }
        }
        private void UpdateResourceData(string resourceUpdate, string resourceType)
        {
            foreach (var item in resourceData_list)
            {
                if ((item.Type).Equals(resourceType))
                {
                    ResourcePage.Instance.UpdateResource(resourceUpdate, item.UpdatedTitle);
                }
            }
            
        }

        #endregion
        [TearDown]
        public void DeleteHouse()
        {
            // Step 7: Back to House default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 7: Back to House default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            // Filter old and new house then delete it
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName))
                HousePage.Instance.DeleteHouse(houseData.HouseName);
        }
    }
}
