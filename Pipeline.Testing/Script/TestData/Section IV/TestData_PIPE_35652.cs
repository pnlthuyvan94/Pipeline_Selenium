using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using NUnit.Framework;
namespace Pipeline.Testing.Script.TestData
{
    class TestData_PIPE_35652 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);
        }
        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        [Test]
        public void SetUpTestData_A04_Q_Assets_DetailPage_Houses_ImportPage_HouseQuantitiesImport_Descriptions_of_the_key_measures_and_phases_on_house_import_process()
        {
            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer_Automation"
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
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "QA_RT_Style_Automation",
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
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "1222222",
                Name = "QA_RT_BuildingGroup_Automation"
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
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_35652\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_35652\\Pipeline_Products_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
        }
        }
}
