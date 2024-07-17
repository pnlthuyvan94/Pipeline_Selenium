using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_III
{
    class B17_B_PIPE_30286 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        SpecSetData specSet, specSetProductConversion;    
        private string exportFileName;
        private const string EXPORT_CSV_MORE_MENU = "Export Product Conversions CSV";        
        readonly string IMPORT_PRODUCTSCONVERSION_TO_SPECSETGROUP_PRODUCTSCONVERSIONS = "Spec Sets Product Import";
        public string urlSpecset = "";
        [SetUp]
        public void GetData()
        {

            specSet = new SpecSetData
            {
                GroupName = "QA_RT_SpecSetGroup123_Automation",
                SpectSetName = "QA_RT_SpecSet123_Automation",
            };


            specSetProductConversion = new SpecSetData
            {
                OriginalPhase = "RT01-QA_RT_BuildingPhase01_Auto",
                OriginalProduct = "QA_RT_Product03_Auto",
                OriginalProductStyle = "QA_RT_Style_Automation",
                OriginalProductUse = "QA Use 2",
                NewPhase = "RT01-QA_RT_BuildingPhase01_Auto",
                NewProduct = "QA_RT_Product01_Auto",
                NewProductStyle = "QA_RT_Style_Automation",
                NewProductUse = "NONE",
                ProductCalculation = "NONE",
            };

        }

        [Test]
        [Category("Section_III")]
        public void B17_B_Estimating_Spec_Sets_Export_Import_Spec_Sets()
        {
            //Step1: 0. Prepare specset - Create specset Group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change Show Category On SpecSet Product Conversion is turned false.</font><b>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            EstimatingPage.Instance.Check_Show_Category_On_Product_Conversion(false);

            //1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/ProductAssemblies/SpecSets/Default.aspx
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            //2. Insert name to filter and click filter by Contain value and open Spec Set Group page
            SpecSetPage.Instance.ChangeSpecSetPageSize(20);
            SpecSetPage.Instance.Navigatepage(1);


            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSet.GroupName);

            if (SpecSetPage.Instance.IsItemInGrid("Name", specSet.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSet.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.SelectItemInGrid("Name", specSet.GroupName);
                SpecSetDetailPage.Instance.ExpandAndCollapseSpecSet(specSet.SpectSetName, "Expand");
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step 1: Verify the information on product conversion.</font><b>");
                SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(specSetProductConversion);
            }
            //Step 2: Export CSV file
            string exportname = $"{specSet.GroupName}";
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2 Export CSV file.</b></font>");
            string urlSpecSet = SpecSetDetailPage.Instance.CurrentURL;
            SpecSetDetailPage.Instance.SelectItemInUtiliestButton("Export Product Conversions CSV");
            exportFileName = CommonHelper.GetExportFileName(ExportType.SpecSetProducts.ToString() + "_" + exportname);
            SpecSetPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);

            //Step 3: Verify delete product conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step 3: Verify Delete the Product conversion.</font><b>");
            SpecSetDetailPage.Instance.DeleteItemOnProductConversionsInGrid(specSetProductConversion.OriginalProduct);

            //Step 4: Now import product conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step 4: Import Product conversion.</font><b>");
            string importFileDir = "\\DataInputFiles\\Import\\PIPE_30286\\Pipeline_SpecSetProducts.csv";
            SpecSetDetailPage.Instance.ImportExporFromMoreMenu("Import", specSet.GroupName);
            SpecSetDetailPage.Instance.ImportFile(IMPORT_PRODUCTSCONVERSION_TO_SPECSETGROUP_PRODUCTSCONVERSIONS, importFileDir);

            //Step 5: Verify the import function
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Step 5: Verify Data created from import product conversion.</font><b>");
            CommonHelper.OpenURL(urlSpecSet);
            SpecSetDetailPage.Instance.ExpandAndCollapseSpecSet(specSet.SpectSetName, "Expand");
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(specSetProductConversion);

            //Step 6: Import product conversion as excel extension file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: Import Product Conversion by excel format.</b></font>");
            string importFileDirExcel = "\\DataInputFiles\\Import\\PIPE_30286\\Pipeline_QA_RT_SpecSetGroup_Automation.xlsx";
            SpecSetDetailPage.Instance.ImportExporFromMoreMenu("Import", specSet.GroupName);
            SpecSetDetailPage.Instance.ImportFile(IMPORT_PRODUCTSCONVERSION_TO_SPECSETGROUP_PRODUCTSCONVERSIONS, importFileDirExcel);

            //Step 7. Verify if the import is not successful with the correct error text in red font: Failed to import file due to wrong file format. File must be .csv format.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7: Verify error message with wrong extension file.</b></font>");
            string expectedErrorMess = "Failed to import file due to wrong file format. File must be .csv format.";
            string ImportFile = "\\DataInputFiles\\Import\\PIPE_30286\\Pipeline_QA_RT_SpecSetGroup_Automation.xlsx";
            SpecSetDetailPage.Instance.ImportInvalidData(ImportGridTitle.SPEC_SET_PRODUCT_IMPORT, ImportFile, expectedErrorMess);

        }

    }
}
