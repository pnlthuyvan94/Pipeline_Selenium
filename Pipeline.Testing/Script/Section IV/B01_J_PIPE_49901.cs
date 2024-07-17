using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.Products;

namespace Pipeline.Testing.Script.Section_IV
{
    class B01_J_PIPE_49901 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string PRODUCT_IMPORT_PARAGRAPH_ELEMENT_ID = "ctl00_CPH_Content_lblMessageSubcomponent";
        private const string INVALID_DATA_FILE_PATH = @"\DataInputFiles\Import\PIPE_49901\ImportInvalidProductData.csv";
        private const string VALID_DATA_FILE_PATH = @"\DataInputFiles\Import\PIPE_49901\ImportValidProductData.csv";
        private const string BASELINE_INVALID_ERROR_FILE_PATH = @"\DataInputFiles\Import\PIPE_49901\ErrorMessage.txt";
        private void ImportProductData(string productDataFileDirectory, string categoryOfImport, string elementID)
        {
            ProductPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.ImportExportProduct("Import");
            CommonHelper.ScrollToElement(elementID);
            ProductPage.Instance.ImportFile("Products", categoryOfImport, productDataFileDirectory);
        }
        private void CheckImportErrorMessage(string paragraphElementId, string baselineFileDir)
        {
            bool isImportProductErrorMessageMatched = CommonHelper.CompareContentElementAndFile(paragraphElementId, baselineFileDir);
            if (isImportProductErrorMessageMatched)
            {
                ExtentReportsHelper.LogPass("<font color='green'> The error message shown correct </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> The error message shown not correct! </font>");
            }
        }
        [Test]
        [Category("Section_IV")]
        public void B01_J_Product_Subcomponent_Invalid_Error_Exist()
        {
            //Step 1: Go to Product tab and import invalid data
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 1: Go to Product tab and import invalid data.</font>");
            ImportProductData(INVALID_DATA_FILE_PATH, "Subcomponents Import", PRODUCT_IMPORT_PARAGRAPH_ELEMENT_ID);
            CheckImportErrorMessage(PRODUCT_IMPORT_PARAGRAPH_ELEMENT_ID, BASELINE_INVALID_ERROR_FILE_PATH);
            //Step 2: Import with valid data
            ExtentReportsHelper.LogInformation(null,
                "<font color='lavender'> Step 2: Import with valid data.</font>");
            ImportProductData(VALID_DATA_FILE_PATH, "Subcomponents Import", PRODUCT_IMPORT_PARAGRAPH_ELEMENT_ID);
        }
    }
}
