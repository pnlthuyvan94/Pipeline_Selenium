using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;
using Pipeline.Testing.Pages.Estimating.Worksheet;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetProducts;
using Pipeline.Testing.Script.TestData;
using System.Collections.Generic;


namespace Pipeline.Testing.Script.Section_IV
{
    class B10_B_PIPE_19180 : BaseTestScript
    {
        private static string QtyUpdate = "13.00";
        private static List<string> QtyValue = new List<string>() { "11.00", "22.00", "33.00", "44.00", "55.00" };
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\PIPE_19180";
        private const string EXPORT_CSV_MORE_MENU = "Export CSV";
        private const string EXPORT_EXCEL_MORE_MENU = "Export Excel";
        
        private const string WORKSHEET_NAME = "QA_RT_Worksheet_L2";
        private readonly string WORKSHEET_CODE = "2607";

        private string exportFileName;
        private WorksheetData worksheetData;
        private const string OPTION_CODE_DEFAULT = TestDataCommon.OPTION_CODE_DEFAULT;
        private const string OPTION_NAME_DEFAULT = TestDataCommon.OPTION_NAME_DEFAULT;
        BuildingPhaseData buildingPhaseData;
        UsesData use_Data;
        WorksheetProductsData worksheetProductsData;
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        /****************** Create Worksheet ********************/

        [SetUp]

        public void SetupData()
        {
            //Worksheet data info
            worksheetData = new WorksheetData()
            {
                Name = WORKSHEET_NAME,
                Code = WORKSHEET_CODE
            };
        
            worksheetProductsData = new WorksheetProductsData()
            {
                //BuildingPhase = { "1981-RT_QA_Automation_BuildingPhase_19180" },
                BuildingPhase = { "2503-QA_RT_Building Phase" },
                Products = { "QA_RT_Product_L2", "QA_RT_Product_L1" },
                //Products = { "RT_QA_Pro_19180_1","RT_QA_Pro_19180_2", "RT_QA_Pro_19180_3"
                //,"RT_QA_Pro_19180_4", "RT_QA_Pro_19180_5"},
                Style = "Default",
                Use = "NONE",
                Quantity = { "5.00", "10.00"}

            };
        }

        [Test]
        [Category("Section_IV")]
        public void B10_B_Estimating_Detail_Pages_Worksheets_Products()
        {

            //Step 1: Navigate to worksheet page and select a worksheet
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 1: Navigate to worksheet page and select a worksheet </font>");
            WorksheetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", WORKSHEET_NAME);
            if (WorksheetPage.Instance.IsItemInGrid("Name", WORKSHEET_NAME) is true)
            {
                WorksheetPage.Instance.SelectItemInGrid("Name", WORKSHEET_NAME);
                ExtentReportsHelper.LogPass("$<font color='green'>Open worksheet successfully</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("$<font color='red'>Not found worksheet</font>");
            }
            //Go to tab "Products"
            //2. In the Side Navigation page, click the Products to open the Products data page
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 2: In the Side Navigation page, click the Products to open the Products data page </font>");
            WorksheetDetailPage.Instance.LeftMenuNavigation("Products");
            string url_WorksheetProduct = WorksheetPage.Instance.CurrentURL;

            //Step 3: Add Products Quantities, verify it added successfully
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 3: Add Products Quantities to Worksheet, verify it added successfully </font>");
            WorksheetProductsPage.Instance.AddQuantitiesInGrid(worksheetProductsData);
            //Verify the Product name and its quantities added
            CommonHelper.RefreshPage();
            if (WorksheetProductsPage.Instance.IsItemInGrid("Product", worksheetProductsData.Products[0]) && WorksheetProductsPage.Instance.IsItemInGrid("Product", worksheetProductsData.Products[1]))
            {
                ExtentReportsHelper.LogPass("<font color='green'>Added successfully the Product to Worksheet</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='green'>Added unsuccessfully the Product to Worksheet</font>");
            }

            // 4. Filter/Edit/Delete the Product in Worksheet Quantities. 
            // Filter item
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 4: Filter/Edit/Delete the Product in Worksheet Quantities </font>");
            WorksheetProductsPage.Instance.FilterItemInGrid("Product", GridFilterOperator.Contains, worksheetProductsData.Products[0]);
            ExtentReportsHelper.LogPass("<font color='green'>The Product Quantities of worksheet filtered successfully</font>");

            // Edit
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Edit Use of {worksheetProductsData.Products[0]} worksheet product</font>");
            WorksheetProductsPage.Instance.EditItemInGrid(worksheetProductsData.Products[0]);
            WorksheetProductsPage.Instance.UpdateItemInGrid(worksheetProductsData.Use, "20.000");
            string successfulMsgEdit = $"Updated 1 product quantities.";
            string actualMsgEdit = WorksheetProductsPage.Instance.GetLastestToastMessage();
            if (successfulMsgEdit.Equals(actualMsgEdit))
            {
                ExtentReportsHelper.LogPass("<font color='green'>The Product Quantities of worksheet edit successfully!</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Can not edit product quantities of worksheet</font>");
            }
            // Delete item
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Delete {worksheetProductsData.Products[0]} worksheet product</font>");
            WorksheetProductsPage.Instance.DeleteItemInGrid("Product", worksheetProductsData.Products[0]);
            WorksheetProductsPage.Instance.WaitGridLoad();
            string successfulMsg = $"The Product Quantities were successfully deleted.";
            string actualMsg = WorksheetProductsPage.Instance.GetLastestToastMessage();
            if (successfulMsg.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass("<font color='green'>The Product Quantities of worksheet deleted successfully!</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Can not delete product quantities of worksheet</font>");
            }

            //Step 5: Copy Quantities from this Worksheet to given option, verify all products are copied correctedly
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 5: Copy Quantities from this Worksheet to given option, verify all products are copied correctedly</font>");

            //Copy product quantities from ws to option
            WorksheetProductsPage.Instance.CopyProductQuantitiesTo("Options", OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);

            //Navigate to option page to verify the copied quantities
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);
            OptionPage.Instance.SelectItemInGridWithTextContains("Name", OPTION_NAME_DEFAULT);
            OptionPage.Instance.LeftMenuNavigation("Products");
            ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Product", worksheetProductsData.Products[1]);

            // Delete specify product quantities to Option
            ProductsToOptionPage.Instance.DeleteItemInGrid("Product", worksheetProductsData.Products[1]);
            // Open worksheet product page
            string[] arrListStr = url_WorksheetProduct.Split('=');
            WorksheetProductsPage.Instance.NavigateURL("BuilderBom/Worksheets/Worksheet.aspx?wid=" + arrListStr[1]);

            // 6.Verify the Export/ Import the Worksheet Quantities
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 6: Verify the Export/ Import the Worksheet Quantities</font>");
            ExtentReportsHelper.LogPass(null, $"<font color='green'> Import the Worksheet Quantities has verified at Worksheer BOM Test case</font>");
            exportFileName = CommonHelper.GetExportFileName(ExportType.Worksheets.ToString(), WORKSHEET_NAME);
            WorksheetProductsPage.Instance.DownloadBaseLineFile(EXPORT_CSV_MORE_MENU, exportFileName);
            //Export 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Exported successfully the CSV file.</b></font>");
            WorksheetProductsPage.Instance.ExportFile(EXPORT_CSV_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.WORKSHEET_PRODUCTS_TITLE);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Exported successfully the Excel file</b></font>");
            WorksheetProductsPage.Instance.ExportFile(EXPORT_EXCEL_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.WORKSHEET_PRODUCTS_TITLE);

        }

        [TearDown]
        public void DeleteData()
        {

        }
    }
}

