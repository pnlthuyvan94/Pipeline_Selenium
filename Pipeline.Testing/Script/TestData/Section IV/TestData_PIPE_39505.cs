using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.TestData_Section_IV
{
    class TestData_PIPE_39505 : BaseTestScript
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
        public void SetUpTestData_B09_C_Jobs_Infinitive_loop_generate_HouseBOM_in_case_configure_the_original_style_and_new_style_are_the_same()
        {

            ExtentReportsHelper.LogInformation(null, "Prepare a new Manufacturer to import Product.");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);

            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "QA_RT_New_Manu_Auto"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData1 = new ManufacturerData()
            {
                Name = "QA_RT_New_Manu_Auto1"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData1.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData1.Name) is false)
            {
                ManufacturerPage.Instance.CreateNewManufacturer(manuData1);
            }

            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData2 = new ManufacturerData()
            {
                Name = "QA_RT_New_Manu_Auto2"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData2.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData2.Name) is false)
            {
                ManufacturerPage.Instance.CreateNewManufacturer(manuData2);
            }


            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData3 = new ManufacturerData()
            {
                Name = "QA_RT_New_Manu_Auto3"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData3.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData3.Name) is false)
            {
                ManufacturerPage.Instance.CreateNewManufacturer(manuData3);
            }

            ExtentReportsHelper.LogInformation(null, "Prepare a new Style to import Product.");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "QA_RT_New_Style_Auto",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            StylePage.Instance.FilterItemInGrid("Manufacturer", GridFilterOperator.Contains, styleData.Manufacturer);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                StylePage.Instance.CreateNewStyle(styleData);
            }
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData1 = new StyleData()
            {
                Name = "QA_RT_New_Style_Auto1",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData1.Name);
            StylePage.Instance.FilterItemInGrid("Manufacturer", GridFilterOperator.Contains, styleData1.Manufacturer);
            if (StylePage.Instance.IsItemInGrid("Name", styleData1.Name) is false)
            {
                StylePage.Instance.CreateNewStyle(styleData1);
            }
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData2 = new StyleData()
            {
                Name = "QA_RT_New_Style_Auto1",
                Manufacturer = manuData1.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData2.Name);
            StylePage.Instance.FilterItemInGrid("Manufacturer", GridFilterOperator.Contains, styleData2.Manufacturer);
            if (StylePage.Instance.IsItemInGrid("Name", styleData2.Name) is false)
            {
                StylePage.Instance.CreateNewStyle(styleData2);
            }

            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData3 = new StyleData()
            {
                Name = "QA_RT_New_Style_Auto3",
                Manufacturer = manuData3.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData3.Name);
            StylePage.Instance.FilterItemInGrid("Manufacturer", GridFilterOperator.Contains, styleData3.Manufacturer);
            if (StylePage.Instance.IsItemInGrid("Name", styleData3.Name) is false)
            {
                StylePage.Instance.CreateNewStyle(styleData3);
            }

            ExtentReportsHelper.LogInformation(null, "Prepare a new Building Group to import Product.");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "12111111",
                Name = "QA_RT_New_Building_Group_Auto_01"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            ExtentReportsHelper.LogInformation(null, "Prepare data: Import Building Phase to import Product.");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            string importFile = "\\DataInputFiles\\Import\\PIPE_39505\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_39505\\Pipeline_Products_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);

        }
    }
}
