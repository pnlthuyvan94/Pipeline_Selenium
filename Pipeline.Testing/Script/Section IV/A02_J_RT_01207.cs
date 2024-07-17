using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Documents;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.IO;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A02_J_RT_01207 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private DocumentData newData;
        private IList<string> fileList;
        private CommunityData communityData;

        [SetUp]
        public void GetData()
        {
            newData = new DocumentData()
            {
                Name = "DocumentDOC.doc;DocumentPDF.pdf;DocumentTXT.txt;DocumentDOCX.docx;DocumentXML.xml;DocumentXLS.xls;DocumentXLSX.xlsx;DocumentCSV.csv;DocumentZIP.zip;DocumentPPTX.pptx;",
                Description = "description",
                Upload = string.Empty
            };
            fileList = CommonHelper.CastValueToList(newData.Name);

            communityData = new CommunityData()
            {
                Name = "R-QA Only Community_Auto_Test_Documents",
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
        public void A02_J_Assets_DetailPage_Comunities_Document()
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

            // Step 2: In Side Navigation, click the Document to open the Document page
            ExtentReportsHelper.LogInformation(" Step 2: In Side Navigation, click the Document to open the Document page");
            CommunityDetailPage.Instance.LeftMenuNavigation("Documents");
            // Verify opened successfully the Document page
            if (DocumentPage.Instance.IsDocumentsPageDisplay() is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Opened successfully the Document page.</b></font>");


            // Step 3: The successful open/input/save of valid input value (along with the appropriate toast message to indicate the success)
            ExtentReportsHelper.LogInformation(" Step 3: The successful open/input/save of valid input value (along with the appropriate toast message to indicate the success)");
            // Add all types of Document
            ExtentReportsHelper.LogInformation(" a. Add all types of Document");
            DocumentPage.Instance.UploadDocumentsAndVerify(fileList[0], fileList[1], fileList[2], fileList[3], fileList[4], fileList[5], fileList[6], fileList[7], fileList[8], fileList[9]);
            //Verify Drag &drop the Documents. (Can not implement automation)

            //Step 4: The successful filter the newly created item
            ExtentReportsHelper.LogInformation(" Step 4: The successful filter the newly created item");
            ExtentReportsHelper.LogInformation(" Enter the name of a Document on the 'Name' column; selected the 'Filter' button on modal and selected the 'Contains' function.");
            DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.Contains, fileList[7]);
            // Verify filter successfully
            if (DocumentPage.Instance.IsItemInGridOption("Name", fileList[7]) is true && DocumentPage.Instance.IsNumberItemInGrid(1) is true)
            {
                ExtentReportsHelper.LogPass($"The document {fileList[7]} file filtered successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail($"The document {fileList[7]} file filtered unsuccessfully");
            }

            // Step 5: Edit successfully the Document
            ExtentReportsHelper.LogInformation("Step 5: Edit successfully the Document");
            DocumentPage.Instance.EditAndVerifyDocumentFile(fileList[7], newData.Description);

            // Step 6: Select and delete item is successfully
            ExtentReportsHelper.LogInformation("Step 6: Select and delete item is successfully");
            DocumentPage.Instance.DeleteDocumentFile(fileList[7]);

            // Clear filter
            ExtentReportsHelper.LogInformation("Clear filter");
            DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.NotIsEmpty, string.Empty);

            //7. Click the Resource; verify the Document is downloaded
            ExtentReportsHelper.LogInformation("Step 7: Click the Resource; verify the Document is downloaded");
            DocumentPage.Instance.DownloadFile(DocumentPage.Instance.IsFileHref(fileList[2]), pathReportFolder + fileList[2]);
            if (File.Exists(pathReportFolder + fileList[2]))
            {
                ExtentReportsHelper.LogPass($"The document <font color='green'><b>{fileList[2]}</b></font> file is downloaded");
            }
            else
                ExtentReportsHelper.LogFail($"The document <font color='red'><b>{fileList[2]}</b></font> file download unsuccessfully");
        }
        #endregion
        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation("============<b>Clear Data</b>============");
            DocumentPage.Instance.DeleteDocumentFile(fileList[0], fileList[1], fileList[2], fileList[3], fileList[4], fileList[5], fileList[6], fileList[8], fileList[9]);

            // Step 8: Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 8: Back to Community default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);
        }

    }
}
