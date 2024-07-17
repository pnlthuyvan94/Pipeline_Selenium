using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Documents;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Products.ProductDetail;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.IO;

namespace Pipeline.Testing.Script.Section_IV
{
    public partial class B01_C_RT_01246 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private string productName = "ProductDetail_AutomationTesting_DoNotDelete";
        private IList<string> fileList;
        private DocumentData newData;
        private ProductData product;
        [SetUp]
        public void GetData()
        {
            product = new ProductData()
            {
                Name = "ProductDetail_AutomationTesting_DoNotDelete",
                Manufacture = "Armstrong",
                Style = "carpet",
                Code = "5555",
                Description = "Description for testing",
                Notes = "Notes for testing",
                Unit = "NONE",
                SKU="1234",
                RoundingUnit = "1",
                RoundingRule = "Standard Rounding",
                Waste = "0.0",
                BuildingPhase = "004-HN-Phase-4"
            };

            newData = new DocumentData()
            {
                Name = "DocumentDOC.doc;DocumentPDF.pdf;DocumentTXT.txt;DocumentDOCX.docx;DocumentXML.xml;DocumentXLS.xls;DocumentXLSX.xlsx;DocumentCSV.csv;DocumentZIP.zip;DocumentPPTX.pptx;",
                Description = "description",
                Upload = ""
            };
            fileList = CommonHelper.CastValueToList(newData.Name);


            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers, true, true);
            CommonHelper.SwitchLastestTab();

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "Armstrong"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            // Prepare a new Style to import Product.
            // Can't import new Style then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
            StyleData styleData = new StyleData()
            {
                Name = "carpet",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "101",
                Name = "Hai Nguyen Building Group"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_01246\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_01246\\Pipeline_Products_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }

        #region"Test Case"
        [Test]
        [Category("Section_IV")]
        public void B01_C_Estimating_DetailPage_Products_Documents()
        {
            // Step 1: Navigate Estimating/ Product and open Product Detail page
            ExtentReportsHelper.LogInformation("<b>Step 1: Navigate Estimating/ Product.</b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_PRODUCT_URL);

            //  Click the Product to which you would like to select
            ExtentReportsHelper.LogInformation(null, "Click the Product to which you would like to select");
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productName);
            if(ProductPage.Instance.IsItemInGrid("Product Name", productName )is true)
            {
                ProductPage.Instance.SelectItemInGrid("Product Name", productName);
            }
            else
            {
                ProductPage.Instance.CreateNewProduct(product);
            }


            // Verify open the product detail
            if (ProductDetailPage.Instance.IsSaveProductSuccessful(productName) == false)
            {
                ExtentReportsHelper.LogFail(null, "<font color ='red'><b>Can't open the Product Details</b></font>");
            }
            ExtentReportsHelper.LogPass(null, "<b>Opened successfully the Product Details.</b>");


            // Step 2: In Side Navigation, click the Document to open the Document page
            ExtentReportsHelper.LogInformation(null, "<b>Step 2: click the Document on the left navigation.</b>");
            ProductDetailPage.Instance.LeftMenuNavigation("Documents");

            // Verify opened successfully the Document page
            Assert.That(DocumentPage.Instance.IsDocumentsPageDisplay(), "<font color ='red'><b>Opened unsuccessfully the Document page</b></font>");
            ExtentReportsHelper.LogPass(null, "<b>Opened successfully the Document page</b>");

            ExtentReportsHelper.LogInformation(null, "<b>Step 3.1: Upload Documents</b>");
            DocumentPage.Instance.UploadDocumentsAndVerify(fileList[0], fileList[1], fileList[2], fileList[3], fileList[4], fileList[5], fileList[6], fileList[7], fileList[8], fileList[9]);

            // The successful filter the newly created item
            ExtentReportsHelper.LogInformation(null, $"Filter document {fileList[7]} on the grid view.");
            DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.Contains, fileList[7]);
            // Verify filter successfully
            if (DocumentPage.Instance.IsItemInGridOption("Name", fileList[7]) is true) //&& DocumentPage.Instance.IsNumberItemInGrid(1) is true)
            {
                ExtentReportsHelper.LogPass(null, $"The document {fileList[7]} file filtered successfully");
            }
            else
            {
                ExtentReportsHelper.LogFail(null, $"The document {fileList[7]} file filtered unsuccessfully");
            }
            //DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.NoFilter, "");
            //System.Threading.Thread.Sleep(3000);

            ExtentReportsHelper.LogInformation(null, "<b>Step 3.2: Verify Drag &drop the Documents - Can not implement automation.</b>");


            // Step 4: Edit successfully the Document
            ExtentReportsHelper.LogInformation(null, "<b>Step 4: Edit successfully the Document</b>");
            DocumentPage.Instance.EditAndVerifyDocumentFile(fileList[7], newData.Description);


            // Step 5. Download document
            ExtentReportsHelper.LogInformation(null, "<b>Step 5: Click the Document and verify the hyperlink</b>");
            DocumentPage.Instance.DownloadFile(DocumentPage.Instance.IsFileHref(fileList[7]), pathReportFolder + fileList[7]);
            if (File.Exists(pathReportFolder + fileList[7]))
            {
                ExtentReportsHelper.LogPass(null, $"The document <font color='green'><b>{fileList[7]}</b></font> file is downloaded");
            }
            else
                ExtentReportsHelper.LogFail(null, $"The document <font color='red'><b>{fileList[7]}</b></font> file download unsuccessfully");


            // Step 6: Select and delete document
            ExtentReportsHelper.LogInformation(null, "<b>Step 6: Select and delete document</b>");
            DocumentPage.Instance.DeleteDocumentFile(fileList[7]);

        }

        #endregion

        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "============<b>Clear Data</b>============");
            DocumentPage.Instance.FilterItemInGridOption("Name", GridFilterOperator.NoFilter, string.Empty);
            DocumentPage.Instance.DeleteDocumentFile(fileList[0], fileList[1], fileList[2], fileList[3], fileList[4], fileList[5], fileList[6], fileList[8], fileList[9]);
        }

    }
}
