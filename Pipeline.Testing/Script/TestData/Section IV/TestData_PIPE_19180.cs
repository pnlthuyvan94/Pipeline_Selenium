using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Uses;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;
using Pipeline.Testing.Pages.Estimating.Worksheet;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.TestData.Section_IV
{
    class TestData_PIPE_19180 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);   
        }

        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\PIPE_19180";
        private const string WORKSHEET_NAME = "QA_RT_Worksheet_PIPE_19180";
        private readonly string WORKSHEET_CODE = "WS19180";


        ProductData productDataDelete;
        BuildingPhaseData buildingPhaseData;
        BuildingGroupData buildingGroupData;
        UsesData use_Data;
        ManufacturerData manuData;
        StyleData styleData;
        private WorksheetData worksheetData;
        [SetUp]

        public void SetupData()
        {
            //Worksheet data info
            worksheetData = new WorksheetData()
            {
                Name = WORKSHEET_NAME,
                Code = WORKSHEET_CODE
            };


            buildingGroupData = new BuildingGroupData()
            {
                Code = "1980",
                Name = "BG_19181"
            };
            buildingPhaseData = new BuildingPhaseData()
            {
                Code = "1981",
                Name = "RT_QA_Automation_BuildingPhase_19180",
                BuildingGroupName = buildingGroupData.Name,
                BuildingGroupCode = buildingGroupData.Code,
                Taxable = false,
            };

            //Create style, manufacturer, use

            manuData = new ManufacturerData()
            {
                Name = "RT_QA_Manufacturer_19180"
            };

            styleData = new StyleData()
            {
                Name = "RT_QA_Style_19180",
                Manufacturer = manuData.Name
            };

            use_Data = new UsesData()
            {
                Name = "RT_QA_Automation_19180",
                SortOrder = "1"
            };

            productDataDelete = new ProductData()
            {
                Name = "RT_QA_Pro_19180",
            };

        }
        [Test]
        public void SetUpTestData_B10_B_Estimating_Detail_Pages_Worksheets_Products()
        {

            //-------------Create Data----------------//

            //Prepare a building group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a building group</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingGroupData.Name);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", buildingGroupData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Building group has been existed</font>");
            }
            else
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            // Prepare a building phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a building phase</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", buildingPhaseData.Code) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPhaseCode(buildingPhaseData.Code).EnterPhaseName(buildingPhaseData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(buildingGroupData.Code + "-" + buildingGroupData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }

            //Prepare a new Manufacturer to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Manufacturer to import Product</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (!ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name))
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }


            //Prepare a style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (!StylePage.Instance.IsItemInGrid("Name", styleData.Name))
            {
                //If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            //Prepare Use
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare Use.</font>");
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, use_Data.Name);
            if (UsesPage.Instance.IsItemInGrid("Name", use_Data.Name) is false)
            {
                UsesPage.Instance.CreateNewUse(use_Data);
            }

            //Prepare Data: Import Product, before building phase has been created by UI
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Import Product to House.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            string importProductFile = $@"{IMPORT_FOLDER}\Import_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);


            //Prepare worksheet
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>*************** Prepare DATA FOR WORKSHEET **************</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> With the house prepared data, go to its House BOM</font>");
            HousePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", WORKSHEET_NAME);
            if (WorksheetPage.Instance.IsItemInGrid("Name", WORKSHEET_NAME) is false)
            {
                WorksheetPage.Instance.CreateNewWorksheet(worksheetData);
            }
        }
        }
}
