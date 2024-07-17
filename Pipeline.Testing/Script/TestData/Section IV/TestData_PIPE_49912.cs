using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;

namespace Pipeline.Testing.Script.TestData.Section_IV
{
    class TestData_PIPE_49912 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);
        }
        SeriesData series;
        CommunityData community;
        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";

        [SetUp]
        public void GetData()
        {
            community = new CommunityData()
            {
                Name = "QA_RT_Community01_Automation",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "Automation_01",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Nothing to say v1",
                DrivingDirections = "Nothing to say v2",
                Slug = "R-QA-Only-Community-Auto"
            };

            series = new SeriesData()
            {
                Name = "QA_RT_Serie3_Automation",
                Code = "",
                Description = "Please no not remove or modify"
            };
        }

        [Test]
        public void SetUpTestData_A04_B_Assets_DetailPage_House_HouseQuantities_Parameter_field_is_truncated_incorrectly_when_user_exports_House_Quantities_to_XML_and_qty_has_2_parameters()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            OptionPage.Instance.ImportExporFromMoreMenu("Import");
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_IMPORT} grid view to import new Options.</font>");

            string importFile = $"\\DataInputFiles\\Import\\PIPE_48407\\Pipeline_Options.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Community Page.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {community.Name} is displayed in grid.</font>");
            }
            else
            {

                CommunityPage.Instance.CreateCommunity(community);
                string _expectedMessage = $"Could not create Community with name {community.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { community.Name}.");
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare to Series Page.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, series.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", series.Name) is false)
            {
                SeriesPage.Instance.CreateSeries(series);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StyleData styleData = new StyleData()
            {
                Name = "QA_RT_New_Style_Auto",
                Manufacturer = manuData.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                StylePage.Instance.CreateNewStyle(styleData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);

            BuildingGroupData buildingGroupData = new BuildingGroupData()
            {
                Code = "12111111",
                Name = "QA_RT_New_Building_Group_Auto_01"
            };

            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData.Code) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(BUILDING_GROUP_PHASE_VIEW, BUILDING_GROUP_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products.</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_49912\\Pipeline_BuildingPhases.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(PRODUCT_IMPORT_VIEW, PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {PRODUCT_IMPORT} grid view to import new products..</font>");

            importFile = "\\DataInputFiles\\Import\\PIPE_49912\\Pipeline_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importFile);
        }
    }
}
