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

namespace Pipeline.Testing.Script.TestData.Section_IV
{
    class TestData_PIPE_49910 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);
        }

        [Test]
        public void SetUpTestData_C02_C_Jobs_Details_Pages_All_Jobs_BOM_Quantities_The_Style_Manufacture_tag_column_do_not_show_on_the_Job_quantities_export_for_XML_EXCEL_CSV_file()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerData manuData = new ManufacturerData()
            {
                Name = "RT_QA_New_Manu_Auto"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "RT_QA_New_Style_Auto",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                StylePage.Instance.CreateNewStyle(styleData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group " +
                "to import Product.</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "12111111",
                Name = "QA_RT_New_Building_Group_Auto_01"
            };
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {

                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase " +
                "to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string pipelineBuildingPhaseFileCsv = "\\DataInputFiles\\Import\\PIPE_49910\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, pipelineBuildingPhaseFileCsv);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view " +
                    $"to import new products..</font>");

            string pipelineProductFileCsv = "\\DataInputFiles\\Import\\PIPE_49910\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, pipelineProductFileCsv);

            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }
    }
}
