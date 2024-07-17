using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.TestData.Section_VIII
{
    class TestData_PIPE_35568 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.SetupTestData);
        }

        OptionData _option;
        CommunityData _community;
        SeriesData _series;

        [SetUp]
        public void GetData()
        {
            var optionType = new List<bool>()
            {
                false, false, false
            };

            _option = new OptionData()
            {
                Name = "QA_RT_Option01_Automation",
                Number = "0100",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };

            _community = new CommunityData()
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
                Slug = "R-QA-Only-Community-Auto",
            };

            _series = new SeriesData()
            {
                Name = "QA_RT_Serie3_Automation",
                Code = "",
                Description = "Please no not remove or modify"
            };
        }
        [Test]
        public void SetUpTestData_UAT_HotFix_HouseQuanQty_Import_Custom_Parameter_Feature_Is_Broken_For_HouseQty_Imports()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);

            if (!OptionPage.Instance.IsItemInGrid("Name", _option.Name))
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed.</font>");
                }
                // Create Option - Click 'Save' Button
                OptionPage.Instance.AddOptionModal.AddOption(_option);
                string _expectedMessage = $"Option Number is duplicated.";
                string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Option with name { _option.Name} and Number {_option.Number}.");
                }
                BasePage.PageLoad();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option with name { _option.Name} is displayed in grid.</font>");
            }

            //Prepare Community Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Community Page.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {_community.Name} is displayed in grid.</font>");
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);
            }
            else
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(_community);
                string _expectedMessage = $"Could not create Community with name {_community.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { _community.Name}.");
                }

            }

            CommunityDetailPage.Instance.LeftMenuNavigation("Products");
            CommunityProductsPage.Instance.DeleteAllCommunityQuantities();

            //Prepare Series Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Create a new Series.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);

            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _series.Name);
            if (SeriesPage.Instance.IsItemInGrid("Name", _series.Name) is false)
            {
                // Create a new series
                SeriesPage.Instance.CreateSeries(_series);
            }


            //Prepare a new Manufacturer to import Product
            // Can't import new Manufacturer then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Manufacturer to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);

            ManufacturerData manuData1 = new ManufacturerData()
            {
                Name = "QA_Manu_Automation"
            };

            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData1.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData1.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData1);
            }

            ManufacturerData manuData2 = new ManufacturerData()
            {
                Name = "QA_RT_Manufacturer1_Automation"
            };
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_MANUFACTURERS_URL);
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData2.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData2.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData2);
            }

            // Prepare a new Style to import Product.
            // Can't import new Style then create a new one
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare a new Style to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
            StyleData styleData1 = new StyleData()
            {
                Name = "QA_Style_Automation",
                Manufacturer = manuData1.Name
            };
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData1.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData1.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData1);
            }

            StyleData styleData2 = new StyleData()
            {
                Name = "QA_RT_Style1_Automation",
                Manufacturer = manuData2.Name
            };
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_STYLES_URL);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData2.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData2.Name) is false)
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData2);
            }

            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);

            BuildingGroupData buildingGroupData1 = new BuildingGroupData()
            {
                Code = "_0001",
                Name = "QA_Building_Group_Automation"
            };

            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData1.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData1.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData1);
            }

            // Prepare a new Building Group to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Building Group to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_GROUP_URL);

            BuildingGroupData buildingGroupData2 = new BuildingGroupData()
            {
                Code = "123",
                Name = "QA_RT_BuildingGroup1_Automation"
            };

            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingGroupData2.Code);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", buildingGroupData2.Code) is false)
            {
                // Open a new tab and create a new Category
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData2);
            }

            //Prepare data: Import Building Phase to import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare data: Import Building Phase to import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_BUILDING_GROUP_AND_PHASE);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.BUILDING_GROUP_PHASE_VIEW, ImportGridTitle.BUILDING_PHASE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importBuildingPhaseFile = "\\DataInputFiles\\Import\\PIPE_35568\\ImportBuildingPhase\\Pipeline_BuildingPhases_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.BUILDING_PHASE_IMPORT, importBuildingPhaseFile);

            //Prepare Data: Import Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            if (ProductsImportPage.Instance.IsImportGridDisplay(ImportGridTitle.PRODUCT_IMPORT_VIEW, ImportGridTitle.PRODUCT_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.PRODUCT_IMPORT} grid view to import new products.</font>");

            string importProductFile = "\\DataInputFiles\\Import\\PIPE_35568\\ImportProduct\\Pipeline_Products_Automation.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);
        }
        }
}
