using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Import;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class A05_F_PIPE_49911 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string OPTION_PRODUCT_VIEW = "Option Product Import";

        private const string OPTION_NAME_DEFAULT = "QA_RT_Option_Automation";
        private const string STYLE_NAME_DEFAULT = "DEFAULT";

        private const string STYLE_NAME = "QA_RT_Style_Automation";
        string OptionProduct_url;

        [Test]
        [Category("Section_IV")]
        public void A05_F_Assets_Detail_Pages_Options_Products_Importing_Option_qty_with_DEFAULT_style_will_get_the_default_value_of_product_not_DEFAULT_value()
        {
            //1.Verify that user import Option qty with default style
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>1.Verify that user import Option qty with default style</font>");
            //1.1 Go to Option Products import
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>1.1 Go to Option Products import</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", OPTION_NAME_DEFAULT))
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", OPTION_NAME_DEFAULT);
            }

            //Navigate To Option Product
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Navigate To Option Product</font>");
            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            OptionProduct_url = ProductsToOptionPage.Instance.CurrentURL;
            ProductsToOptionPage.Instance.ImportExporFromMoreMenu("Import");
            CommonHelper.ScrollToEndOfPage();
            string expectedURL = BaseDashboardUrl + "/products/transfers/imports/products.aspx?oid=9053&view=Products";
            if (ProductsImportPage.Instance.IsPageDisplayed(expectedURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Import Product Option isn't displayed.</font>");
            }
            ExtentReportsHelper.LogPass("<font color='green'><b>Import Product Option page is displayed</b></font>");

            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, OPTION_PRODUCT_VIEW) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {OPTION_PRODUCT_VIEW} grid view to import new bulding phase.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_49911\\Pipeline_OptionProduct_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_PRODUCT_IMPORT, importFile);

            CommonHelper.OpenURL(OptionProduct_url);
            List<string> Product1s = new List<string>() { "QA_RT_New_Product01_Automation", "QA_RT_New_Product02_Automation", "QA_RT_New_Product03_Automation" };
            foreach (string product in Product1s)
            {
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", product) is true && ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Style", STYLE_NAME_DEFAULT) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Import Product Option with Style: {STYLE_NAME_DEFAULT} is successfully and displayed in grid</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Import Product Option with Style: {STYLE_NAME_DEFAULT} failed and isn't displayed in grid.</font>");
                }

            }

            List<string> Product2s = new List<string>() { "QA_RT_New_Product01_Automation", "QA_RT_New_Product02_Automation" };
            foreach (string product in Product2s)
            {
                if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", product) is true && ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Style", STYLE_NAME) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>Import Product Option with Style: {STYLE_NAME} is successfully and displayed in grid</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Import Product Option with Style: {STYLE_NAME} failed and isn't displayed in grid.</font>");
                }

            }
            //1.2 Import Qty with “DEFAULT” Style
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>1.2 Import Qty with “DEFAULT” Style</font>");

            ProductsToOptionPage.Instance.ImportExporFromMoreMenu("Import");
            string expectedErrorMess = "Style Name De fault not found on Line #1.";
            string ImportFile = "\\DataInputFiles\\Import\\PIPE_49911\\Pipeline_OptionProductErrorFile_Automation.xlsx";
            ProductsImportPage.Instance.ImportInvalidData(ImportGridTitle.BUILDING_PHASE_IMPORT, ImportFile, expectedErrorMess);

        }
        [TearDown]
        public void ClearData()
        {
            CommonHelper.OpenURL(OptionProduct_url);
            ProductsToOptionPage.Instance.DeleteAllOptionQuantities();
        }
    }
}
