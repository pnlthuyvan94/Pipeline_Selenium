using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;


namespace Pipeline.Testing.Script.TestData.Section_IV
{
    class TestData_PIPE_45580 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);
        }     

        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\PIPE_45580";

        ProductData productData;
        BuildingPhaseData buildingPhaseData;
        BuildingGroupData buildingGroupData;
        
        ManufacturerData manuData;
        StyleData styleData;

        [SetUp]

        public void SetupData()
        {
            buildingGroupData = new BuildingGroupData()
            {
                Code = "BG_195",
                Name = "QA_RT_Automation_BG_19175"
            };
            buildingPhaseData = new BuildingPhaseData()
            {
                Code = "P195",
                Name = "QA_RT_Automation_BP_195",
                BuildingGroupName = buildingGroupData.Name,
                BuildingGroupCode = buildingGroupData.Code,
                Taxable = false,
            };

            //Create style, manufacturer, use

            manuData = new ManufacturerData()
            {
                Name = "QA_RT_Automation_Manufacturer_195"
            };

            styleData = new StyleData()
            {
                Name = "QA_RT_Automation_Style_195",
                Manufacturer = manuData.Name
            };
            productData = new ProductData()
            {
                Name = "QA_RT_Automation_Product_195"
            };
        }

        [Test]
        public void SetUpTestData_B01_I_Estimating_Detail_Page_Products_Add_Manufacturer_Add_Style_In_Products_Detail_Page()
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

            //Prepare a new Manufacturer 
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

            // Import Product, before building phase has been created by UI
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            string importProductFile = $@"{IMPORT_FOLDER}\Import_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);

        }
    }
}
