using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Documents;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.IO;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class A04_F_RT_01216 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private DocumentData newData;
        private IList<string> fileList;
        private HouseData houseData;

        [SetUp]
        public void GetData()
        {
            newData = new DocumentData()
            {
                Name = "DocumentDOC.doc;DocumentPDF.pdf;DocumentTXT.txt;DocumentDOCX.docx;DocumentXML.xml;DocumentXLS.xls;DocumentXLSX.xlsx;DocumentCSV.csv;DocumentZIP.zip;DocumentPPTX.pptx;",
                Description = "description",
                Upload = ""
            };
            fileList = CommonHelper.CastValueToList(newData.Name);

            houseData = new HouseData()
            {
                HouseName = "QA_RT_Auto_House_RT_01216",
                SaleHouseName = "RegressionTest_House_Sales_Name",
                Series = "RT_Series_DoNot_Delete",
                PlanNumber = "1216",
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

            // Go to default page
            // Step 0: Navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Houses/Default.aspx
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1: Navigate to House default page</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.2: Filter house with name {houseData.HouseName} and create if it doesn't exist.</b></font>");
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
        }

        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void A04_F_Assets_DetailPage_House_Documents()
        {
            // Navigate to this URL: http://beta.bimpipeline.com/Dashboard/Builder/Divisions/Default.aspx
            // Step 1: Navigate Asserts > Communities and open scussess Documents page
            ExtentReportsHelper.LogInformation(null, " Step 1: Navigate Asserts > Communities and open scussess Documents page");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            //Click a Community to which you like to select
            ExtentReportsHelper.LogInformation("Click a Community to which you like to select");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            bool isFoundOldItem = HousePage.Instance.IsItemInGrid("Name", houseData.HouseName);
            //Assert.That(isFoundOldItem, string.Format($"The updated community: \"{houseData.HouseName}\" was not display on grid."));
            if (isFoundOldItem)
            {
                ExtentReportsHelper.LogPass(string.Format($"The updated community: \"{houseData.HouseName}\" was display on grid."));
            }
            else
            {
                ExtentReportsHelper.LogFail(string.Format($"The updated community: \"{houseData.HouseName}\" was not display on grid."));
            }

            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
            //Verify the updated Community in header
            //Assert.That(HouseDetailPage.Instance.IsHouseNameDisplaySuccessfullyOnBreadScrumb(houseData.HouseName, houseData.PlanNumber), "The updated community displays unsuccessfully.");
            if (HouseDetailPage.Instance.IsHouseNameDisplaySuccessfullyOnBreadScrumb(houseData.HouseName, houseData.PlanNumber))
            {
                ExtentReportsHelper.LogPass($"The updated community displays sucessfully with URL: {HouseDetailPage.Instance.CurrentURL}");
            }
            else
            {
                ExtentReportsHelper.LogFail("The updated community displays unsuccessfully.");
            }

            // Step 2: In Side Navigation, click the Document to open the Document page
            ExtentReportsHelper.LogInformation(" Step 2: In Side Navigation, click the Document to open the Document page");
            HouseDetailPage.Instance.LeftMenuNavigation("Documents");
            // Verify opened successfully the Document page
            //Assert.That(DocumentPage.Instance.IsDocumentsPageDisplay(), "Opened unsuccessfully the Document page");
            if (DocumentPage.Instance.IsDocumentsPageDisplay())
            {
                ExtentReportsHelper.LogPass("Opened successfully the Document page");
            }

            // Step 3: The successful open/input/save of valid input value (along with the appropriate toast message to indicate the success)
            ExtentReportsHelper.LogInformation(" Step 3: The successful open/input/save of valid input value (along with the appropriate toast message to indicate the success)");
            // Add all types of Document
            ExtentReportsHelper.LogInformation(" a. Add all types of Document");
            DocumentPage.Instance.UploadDocumentsAndVerify(fileList[0], fileList[1], fileList[2], fileList[3], fileList[4], fileList[5], fileList[6], fileList[7], fileList[8], fileList[9]);
            //Verify Drag &drop the Documents. (Can not implement automation)

            // The successful filter the newly created item
            ExtentReportsHelper.LogInformation(" The successful filter the newly created item");
            ExtentReportsHelper.LogInformation(" Enter the name of a Document on the 'Name' column; selected the 'Filter' button on modal and selected the 'Contains' function.");
            DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.Contains, fileList[7]);
            System.Threading.Thread.Sleep(5000);

            // Verify filter successfully
            if (DocumentPage.Instance.IsItemInGridOption("Name", fileList[7]) is true && DocumentPage.Instance.IsNumberItemInGrid(1) is true)
            {
                ExtentReportsHelper.LogPass($"The document {fileList[7]} file filtered successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail($"The document {fileList[7]} file filtered unsuccessfully");
            }

            // Step 4: Edit successfully the Document
            ExtentReportsHelper.LogInformation("Step 4: Edit successfully the Document");
            DocumentPage.Instance.EditAndVerifyDocumentFile(fileList[7], newData.Description);

            // Step 5. Click the Resource; verify the Document is downloaded
            ExtentReportsHelper.LogInformation("Step 5: Click the Document; verify the hyperlink");
            DocumentPage.Instance.DownloadFile(DocumentPage.Instance.IsFileHref(fileList[7]), pathReportFolder + fileList[7]);
            System.Threading.Thread.Sleep(5000);
            if (File.Exists(pathReportFolder + fileList[7]))
            {
                ExtentReportsHelper.LogPass($"The document <font color='green'><b>{fileList[7]}</b></font> file is downloaded");
            }
            else
                ExtentReportsHelper.LogFail($"The document <font color='green'><b>{fileList[7]}</b></font> file download unsuccessfully");

            // Step 6: Select and delete item is successfully
            ExtentReportsHelper.LogInformation("Step 6: Select and delete item is successfully");
            DocumentPage.Instance.DeleteDocumentFile(fileList[7]);

        }
        #endregion
        [TearDown]
        public void ClearData()
        {
            // clear filter
            DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.NotIsEmpty, "");
            System.Threading.Thread.Sleep(5000);

            ExtentReportsHelper.LogInformation("============<b>Clear Data</b>============");
            DocumentPage.Instance.DeleteDocumentFile(fileList[0], fileList[1], fileList[2], fileList[3], fileList[4], fileList[5], fileList[6], fileList[8], fileList[9]);

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
