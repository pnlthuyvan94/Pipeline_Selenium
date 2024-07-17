using LinqToExcel;
using LinqToExcel.Extensions;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Resources;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_H_RT_01205 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        readonly string sourcePathDocument = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\DataInputFiles\Documents\";
        readonly string sourcePathImage = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + $@"\DataInputFiles\Resources\";
        private IList<string> linkList;
        private IList<string> dirArrayDoc = new List<string>();
        private readonly IList<string> listNameDoc = new List<string>();
        private IList<string> dirArrayImage = new List<string>();
        private readonly IList<string> listNameImage = new List<string>();
        private CommunityData communityData;


        [SetUp]
        public void GetData()
        {
            Row newdata = ExcelFactory.GetRow(ResourcePage.Instance.TestData_RT_01205, 3);
            linkList = CommonHelper.CastValueToList(newdata["Link"]);
            // Get list documents
            dirArrayDoc = Directory.GetFiles(sourcePathDocument);
            foreach (var nameDoc in dirArrayDoc)
            {
                string nameItem = Path.GetFileName(nameDoc);
                listNameDoc.Add(nameItem);
            }
            // Get list images
            dirArrayImage = Directory.GetFiles(sourcePathImage);
            foreach (var nameDoc in dirArrayImage)
            {
                string nameItem = Path.GetFileName(nameDoc);
                listNameImage.Add(nameItem);
            }

            communityData = new CommunityData()
            {
                //Name = TestData["Name"],
                Name = "R-QA Only Community Auto_Test_Resource",
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
        public void A02_H_Assets_DetailPage_Community_Resource()
        {
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

            // Open Resource navigation
            CommunityDetailPage.Instance.LeftMenuNavigation("Resources");

            // Step 3: Click Add button and upload resource
            ExtentReportsHelper.LogInformation(" Step 3: Click Add button and updload resource");
            Assert.That(ResourcePage.Instance.IsUploadSectionDisplay(), "The upload Resource section displays unsuccessfully.");
            // Upload image
            UploadResourceData(dirArrayImage.ToArray(), 1);
            //Upload documents
            UploadResourceData(dirArrayDoc.ToArray(), 2);
            // Upload link
            UploadResourceData(linkList.ToArray(), 3);

            // Step 4: Edit to update Resource information
            ExtentReportsHelper.LogInformation(" Step 4:  Edit to update Resource information");
            UpdateResourceData(Path.GetFileName(dirArrayDoc[1]), 1);
            ExtentReportsHelper.LogInformation(" Step 5:  Click resource and verify hyperlink");

            // Step 5: Click resource and verify hyperlink
            ResourcePage.Instance.DownloadFile(ResourcePage.Instance.IsFileHref(listNameDoc[1]), pathReportFolder + listNameDoc[1]);

            // Step 6: Delete resource item
            ExtentReportsHelper.LogInformation(" Step 6:  Delete resource item");
            ResourcePage.Instance.FilterAndRemoveResource(linkList.ToArray());
            ResourcePage.Instance.FilterAndRemoveResource(listNameImage.ToArray());
            ResourcePage.Instance.FilterAndRemoveResource(listNameDoc.ToArray());

        }

        private void UploadResourceData(string[] ResourceData, int index)
        {
            Row TestData_RT_01205 = ExcelFactory.GetRow(ResourcePage.Instance.TestData_RT_01205, index);
            ResourceData resourceData = new ResourceData()
            {
                Type = TestData_RT_01205["Type"],
                Title = TestData_RT_01205["Title"],
                UpdatedTitle = TestData_RT_01205["Update Title"],
                Link = TestData_RT_01205["Link"],
            };

            ResourcePage.Instance.UploadResourceAndVerify(resourceData, ResourceData.ToArray());
        }

        private void UpdateResourceData(string resourceUpdate, int index)
        {
            Row TestData_RT_01205 = ExcelFactory.GetRow(ResourcePage.Instance.TestData_RT_01205, index);
            ResourceData resourceData = new ResourceData()
            {
                Type = TestData_RT_01205["Type"],
                Title = TestData_RT_01205["Title"],
                UpdatedTitle = TestData_RT_01205["Update Title"],
                Link = TestData_RT_01205["Link"],
            };
            ResourcePage.Instance.UpdateResource(resourceUpdate, resourceData.UpdatedTitle);
        }
        #endregion

        [TearDown]
        public void DeleteCommunity()
        {
            // Step 5: Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5: Back to Community default page and delete data.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);
        }
    }
}
