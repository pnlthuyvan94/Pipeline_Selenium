using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.CommunityHouseBOM;
using Pipeline.Testing.Pages.Assets.Communities.Products;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;


namespace Pipeline.Testing.Script.Section_X
{
    class UAT_HOTFIX_PIPE_34678 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_VIII);
        }

        OptionData _option;
        CommunityData _community;
        SeriesData _series;
        HouseData _housedata;

        private int totalItems;
        private string exportFileName;

        private readonly int[] indexs = new int[] { };
        private const string EXPORT_CSV_MORE_MENU = "CSV";
        private const string EXPORT_Excel_MORE_MENU = "Excel";
        private const string ImportType = "Pre-Import Modification";
        private const string TYPE_DELETE_HOUSEQUANTITIES = "DeleteAll";

        private readonly string PARAMETER_DEFAULT = "SWG";

        private readonly string COMMUNITY_CODE_DEFAULT = "Automation_01";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House01_Automation";
        private readonly string OPTION_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private readonly string OPTION_CODE_DEFAULT = "0100";

        private const string FILTERED_TO_ALL = "ALL";

        private const string EXPORT_SELECTED_HOUSES = "Export Selected Houses";
        private const string EXPORT_ALL_HOUSES = "Export All Houses";


        //Default House Quantities 
        
        private ProductData productData_withParameter1;
        private ProductData productData_withParameter2;

        private ProductToOptionData productToOption_withParameter1;
        private ProductToOptionData productToOption_withParameter2;

        private ProductData productData_House_withParameter1;
        private ProductData productData_House_withParameter2;


        private ProductToOptionData productToHouse_withParameter1;
        private ProductToOptionData productToHouse_withParameter2;

        private ProductToOptionData productToHouseBOM_withParameter1;
        private ProductToOptionData productToHouseBOM_withParameter2;

        private HouseQuantitiesData houseQuantities_WithParameter_DefaultCommunity;
        private HouseQuantitiesData houseQuantities_HouseBOM_WithParameter_DefaultCommunity;


        private ProductData productData;


        private ProductToOptionData productToOption;

        private ProductToOptionData productToHouseBOM;

        private HouseQuantitiesData houseQuantities_DefaultCommunity;
        private HouseQuantitiesData houseQuantities_HouseBOM_DefaultCommunity;



        //Specific House Quantities With Parameter
        private ProductData productData_withParameter_SpecificCommunity1;
        private ProductData productData_withParameter_SpecificCommunity2;


        private ProductToOptionData productToOption_withParameter_SpecificCommunity1;
        private ProductToOptionData productToOption_withParameter_SpecificCommunity2;


        private ProductData productData_House_withParameter_SpecificCommunity1;
        private ProductData productData_House_withParameter_SpecificCommunity2;


        private ProductToOptionData productToHouse_withParameter_SpecificCommunity1;
        private ProductToOptionData productToHouse_withParameter_SpecificCommunity2;


        private ProductToOptionData productToHouseBOM_withParameter_SpecificCommunity1;
        private ProductToOptionData productToHouseBOM_withParameter_SpecificCommunity2;


        private HouseQuantitiesData houseQuantities_withParameter_SpecificCommunity;
        private HouseQuantitiesData houseQuantities_withParameter_HouseBOM_SpecificCommunity;


        //Specific House Quantities
        private ProductData productData_SpecificCommunity;

        private ProductToOptionData productToOption_SpecificCommunity;

        private ProductData productData_House_SpecificCommunity;


        private ProductToOptionData productToHouse_SpecificCommunity;


        private ProductToOptionData productToHouseBOM_SpecificCommunity;

        private HouseQuantitiesData houseQuantities_SpecificCommunity;
        private HouseQuantitiesData houseQuantities_HouseBOM_SpecificCommunity;

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

            _housedata = new HouseData()
            {
                HouseName = "QA_RT_House01_Automation",
                SaleHouseName = "QA_RT_House01_Sales_Name",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "3000",
                BasePrice = "1000000",
                SQFTBasement = "1",
                SQFTFloor1 = "1",
                SQFTFloor2 = "2",
                SQFTHeated = "3",
                SQFTTotal = "7",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "1",
                Bathrooms = "1.5",
                Garage = "1 Car",
                Description = "Test"
            };

            _series = new SeriesData()
            {
                Name = "QA_RT_Serie3_Automation",
                Code = "",
                Description = "Please no not remove or modify"
            };

            //Default House Quantities 
            productData_withParameter1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE",
                Parameter = "SWG=ABC"
            };

            productData_withParameter2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "70.00",
                Unit = "NONE",
                Parameter = "SWG=EFG"
            };


            productToOption_withParameter1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_withParameter1 },
                ParameterList = new List<ProductData> { productData_withParameter1 }
            };

            productToOption_withParameter2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_withParameter2 },
                ParameterList = new List<ProductData> { productData_withParameter2 }
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_withParameter1 = new ProductData(productData_withParameter1) ;

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_withParameter2 = new ProductData(productData_withParameter2) ;


            productToHouse_withParameter1 = new ProductToOptionData(productToOption_withParameter1) { ProductList = new List<ProductData> { productData_House_withParameter1 } };
            productToHouse_withParameter2 = new ProductToOptionData(productToOption_withParameter2) { ProductList = new List<ProductData> { productData_House_withParameter2 } };

            // There is no House quantities 
            houseQuantities_WithParameter_DefaultCommunity = new HouseQuantitiesData()
            {
                houseName= HOUSE_NAME_DEFAULT,
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_withParameter1, productToHouse_withParameter2  }
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_withParameter1 = new ProductData(productData_withParameter1);
            ProductData productData_HouseBOM_withParameter2 = new ProductData(productData_withParameter2);

            productToHouseBOM_withParameter1 = new ProductToOptionData(productToOption_withParameter1) { ProductList = new List<ProductData> { productData_HouseBOM_withParameter1 } };

            productToHouseBOM_withParameter2 = new ProductToOptionData(productToOption_withParameter2) { ProductList = new List<ProductData> { productData_HouseBOM_withParameter2 } };



            houseQuantities_HouseBOM_WithParameter_DefaultCommunity = new HouseQuantitiesData(houseQuantities_WithParameter_DefaultCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_withParameter1, productToHouseBOM_withParameter2 }
            };


            productData = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
            };

            productToOption = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData }
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"


            ProductData productData_HouseBOM = new ProductData(productData) {Quantities="100.00" } ;


            productToHouseBOM = new ProductToOptionData(productToOption) { ProductList = new List<ProductData> { productData_HouseBOM } };


            houseQuantities_HouseBOM_DefaultCommunity = new HouseQuantitiesData(houseQuantities_HouseBOM_WithParameter_DefaultCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM },
            };


            //Specific Community 
            productData_withParameter_SpecificCommunity1 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "30.00",
                Unit = "NONE",
                Parameter = "SWG=ASD"
            };

            productData_withParameter_SpecificCommunity2 = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
                Quantities = "70.00",
                Unit = "NONE",
                Parameter = "SWG=FGH"
            };

            productToOption_withParameter_SpecificCommunity1 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_withParameter_SpecificCommunity1 },
                ParameterList = new List<ProductData> { productData_withParameter_SpecificCommunity1 }
            };

            productToOption_withParameter_SpecificCommunity2 = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_withParameter_SpecificCommunity2 },
                ParameterList = new List<ProductData> { productData_withParameter_SpecificCommunity2 }
            };



            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_withParameter_SpecificCommunity1 = new ProductData(productData_withParameter_SpecificCommunity1) ;

            // House quantities 2 will be same as option quantities 2 but diffirent 'Style' and 'Quantities' fields
            productData_House_withParameter_SpecificCommunity2 = new ProductData(productData_withParameter_SpecificCommunity2) ;


            productToHouse_withParameter_SpecificCommunity1 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity1) { ProductList = new List<ProductData> { productData_House_withParameter_SpecificCommunity1 } };
            productToHouse_withParameter_SpecificCommunity2 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity2) { ProductList = new List<ProductData> { productData_House_withParameter_SpecificCommunity2 } };

            // There is no House quantities 
            houseQuantities_withParameter_SpecificCommunity = new HouseQuantitiesData()
            {
                houseName = HOUSE_NAME_DEFAULT,
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_withParameter_SpecificCommunity1, productToHouse_withParameter_SpecificCommunity2}
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_withParameter_SpecificCommunit1 = new ProductData(productData_withParameter_SpecificCommunity1);
            ProductData productData_HouseBOM_withParameter_SpecificCommunit2 = new ProductData(productData_withParameter_SpecificCommunity2);


            productToHouseBOM_withParameter_SpecificCommunity1 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity1) { ProductList = new List<ProductData> { productData_HouseBOM_withParameter_SpecificCommunit1 } };

            productToHouseBOM_withParameter_SpecificCommunity2 = new ProductToOptionData(productToOption_withParameter_SpecificCommunity2) { ProductList = new List<ProductData> { productData_HouseBOM_withParameter_SpecificCommunit2 } };



            houseQuantities_withParameter_HouseBOM_SpecificCommunity = new HouseQuantitiesData(houseQuantities_withParameter_SpecificCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_withParameter_SpecificCommunity1, productToHouseBOM_withParameter_SpecificCommunity2 },
            };

            productData_SpecificCommunity = new ProductData()
            {
                Name = "QA_RT_New_Product_Automation_01",
                Style = "QA_RT_New_Style_Auto",
                Use = "NONE",
            };


            productToOption_SpecificCommunity = new ProductToOptionData()
            {
                BuildingPhase = "Au01-QA_RT_New_Building_Phase_01_Automation",
                ProductList = new List<ProductData> { productData_withParameter_SpecificCommunity1 },
            };


            /****************************** Create Product quantities on House ******************************/

            // House quantities 1 will be same as option quantities 1 but diffirent 'Quantities' field
            productData_House_SpecificCommunity = new ProductData(productData_SpecificCommunity);

            productToHouse_SpecificCommunity = new ProductToOptionData(productToOption_SpecificCommunity) { ProductList = new List<ProductData> { productData_House_SpecificCommunity } };
            

            // There is no House quantities 
            houseQuantities_SpecificCommunity = new HouseQuantitiesData()
            {
                houseName = HOUSE_NAME_DEFAULT,
                communityCode = COMMUNITY_CODE_DEFAULT,
                communityName = COMMUNITY_NAME_DEFAULT,
                optionName = OPTION_NAME_DEFAULT,
                productToOption = new List<ProductToOptionData> { productToHouse_SpecificCommunity }
            };


            /****************************** The expected data when verifing House BOM ******************************/
            //the quantities on the BOM grid view will combine all quantities from "Option/ quantities" and "House/ quantities"

            ProductData productData_HouseBOM_SpecificCommunity = new ProductData(productData_SpecificCommunity) { Quantities="100.00"};


            productToHouseBOM_SpecificCommunity = new ProductToOptionData(productToOption_SpecificCommunity) { ProductList = new List<ProductData> { productData_HouseBOM_SpecificCommunity } };



            houseQuantities_HouseBOM_SpecificCommunity = new HouseQuantitiesData(houseQuantities_SpecificCommunity)
            {
                productToOption = new List<ProductToOptionData> { productToHouseBOM_SpecificCommunity },
            };
            
        }
        [Test]
        [Category("Section_VIII")]
        public void UAT_HotFix_Community_HouseBom_Export_DoesNot_Match_The_Display_With_CustomerParameters()
        {
            //Make sure current transfer seperation character is ','
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to Settings > Group by Parameters settings is turned on.</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();

            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            string settingBOM_url = SettingPage.Instance.CurrentURL;
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change House BOM Product Orientation is turned false.</b></font>");
            BOMSettingPage.Instance.Check_House_BOM_Product_Orientation(false);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Group by Parameters settings is turned on.</b></font>");
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT);
            //1. Navigate to House Default page, select a House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1: Navigate to House Default page, select a House.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {_housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _housedata.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", _housedata.HouseName) is true)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(_housedata);
                string updateMsg = $"House {_housedata.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            else
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", _housedata.HouseName);

            }

            //Navigate House Communities And Check Community Data on grid
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");
            HouseCommunities.Instance.FillterOnGrid("Name", COMMUNITY_NAME_DEFAULT);
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT, indexs);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Community with Name {COMMUNITY_NAME_DEFAULT} is displayed in grid");
            }


            //2. Open House Quantities page then add product quantities with no parameter and custom parameter into Default Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2: Open House Quantities page then add product quantities with no parameter and custom parameter into Default Community.</b></font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            string HouseImport_url = HouseImportDetailPage.Instance.CurrentURL;
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Processing the import with specific community.</font>");
            
            //Import House Quantities
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Processing the import with default community
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_34678.xml");

            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the set up data for product quantities on House.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");

            foreach (ProductToOptionData housequantity in houseQuantities_WithParameter_DefaultCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_WithParameter_DefaultCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", item.Parameter) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_WithParameter_DefaultCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}" +
                            $"<br>The expected Parameter: {item.Parameter}</br></font>");
                }
            }

            //3. Open House BOM settings, set the “Group by Parameter:” is ON
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3 : Open House BOM settings, set the “Group by Parameter:” is ON.</b></font>");
            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT);

            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", _community.Name);
            }

            //4. Go to Community detail which relevant to this house, generate BOM 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4 : Go to Community detail which relevant to this house, generate BOM.</b></font>");
            CommunityDetailPage.Instance.LeftMenuNavigation("House BOM");
            string CommunityHouseBOM_url = CommunityHouseBOMDetailPage.Instance.CurrentURL;

            //Get the total items on the UI
            ExtentReportsHelper.LogInformation("<font color='lavender'>Get the total items on the UI.</font>");
            CommonHelper.ScrollToEndOfPage();
            
            totalItems = CommunityHouseBOMDetailPage.Instance.GetTotalNumberItem();

            CommunityHouseBOMDetailPage.Instance.GenerateHouseBOM();

            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGridWithParameter(houseQuantities_HouseBOM_WithParameter_DefaultCommunity);
            
            // Get export file name
            exportFileName = CommonHelper.GetExportFileName(ExportType.Community_HouseBOM.ToString(), COMMUNITY_NAME_DEFAULT);

            //Step 5a.1 : Export CSV House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5a.1 : Export CSV House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName, EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Step 5a.2 : Export EXCEL House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5a.2 : Export EXCEL House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_Excel_MORE_MENU, exportFileName, EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_Excel_MORE_MENU, exportFileName, 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);           

            //Step 5b.1 : Export CSV House BOM by “Export All Houses” option and verify the data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5b.1 : Export CSV House BOM by “Export All Houses” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (1)", EXPORT_ALL_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (1)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE, EXPORT_ALL_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Step 5b.2 : Export EXCEL House BOM by “Export All Houses” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5b.2 : Export EXCEL House BOM by “Export All Houses” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (1)", EXPORT_ALL_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (1)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE, EXPORT_ALL_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);


            //6.Open House BOM settings, set the “Group by Parameter:” is OFF
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6: Open House BOM settings, set the “Group by Parameter:” is OFF.</b></font>");
            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(false,string.Empty);


            //7. Go to Community detail which relevant to this house, generate BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 7: Go to Community detail which relevant to this house, generate BOM.</b></font>");
            CommonHelper.OpenURL(CommunityHouseBOM_url);
            CommunityHouseBOMDetailPage.Instance.GenerateHouseBOM();
            CommunityHouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_ALL);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_DefaultCommunity);

            //Step 8a.1 : Export CSV House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8a.1 : Export CSV House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (2)", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (2)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);


            //Step 8a.2 : Export EXCEL House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8a.2 : Export EXCEL House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (2)", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (2)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);


            //Step 8b.1 : Export CSV House BOM by “Export All Houses” option and verify the data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8b.1 : Export CSV House BOM by “Export All Houses” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (3)", EXPORT_ALL_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (3)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_ALL_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Step 8b.2 : Export EXCEL House BOM by “Export All Houses” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8.b2 : Export EXCEL House BOM by “Export All Houses” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (3)", EXPORT_ALL_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (3)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_ALL_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.XLSX);

            

            //9. Open House Quantities page then add product quantities with no parameter and custom parameter into Specific Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9 : Open House Quantities page then add product quantities with no parameter and custom parameter into Specific Community.</b></font>");
            CommonHelper.OpenURL(HouseImport_url);

            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity("Default House Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteSelectedHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            HouseQuantitiesDetailPage.Instance.LeftMenuNavigation("Import");

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Processing the import with default community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Processing the import with default community.</font>");
            //Import House Quantities
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_34678.xml");

            //Go to House quantities and check data
            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.3: Go to House quantities and check data.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_withParameter_SpecificCommunity.communityCode + "-" + houseQuantities_withParameter_SpecificCommunity.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities_withParameter_SpecificCommunity.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities_withParameter_SpecificCommunity.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities_withParameter_SpecificCommunity.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}" +
                            $"<br>The expected Quantities: {item.Quantities}</br></font>");
                }
            }

            //10. Open House BOM settings, set the “Group by Parameter:” is ON
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10 : Open House BOM settings, set the “Group by Parameter:” is ON.</b></font>");
            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(true, PARAMETER_DEFAULT);

            //11. Go to Community detail which relevant to this house, generate BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 11 : Go to Community detail which relevant to this house, generate BOM.</b></font>");
            CommonHelper.OpenURL(CommunityHouseBOM_url);
            CommunityHouseBOMDetailPage.Instance.GenerateHouseBOM();
            CommunityHouseBOMDetailPage.Instance.SelectCollection(FILTERED_TO_ALL);
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGridWithParameter(houseQuantities_withParameter_HouseBOM_SpecificCommunity);

            //Step 12a.1 : Export CSV House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12a.1 : Export CSV House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (4)", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (4)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Step 12a.2 : Export EXCEL House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12a.2 : Export EXCEL House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (4)", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (4)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.EXCEL);

            //Step 12b.1 : Export CSV House BOM by “Export All Houses” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12b.1 : Export CSV House BOM by “Export All Houses” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (5)", EXPORT_ALL_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (5)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE, EXPORT_ALL_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Step 12b.2 : Export EXCEL House BOM by “Export All Houses” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 12b.2 : Export EXCEL House BOM by “Export All Houses” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (5)", EXPORT_ALL_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (5)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCTWITHPARAMETER_TITLE, EXPORT_ALL_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.EXCEL);

            //13. Open House BOM settings, set the “Group by Parameter:” is OFF
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 13: Open House BOM settings, set the “Group by Parameter:” is OFF.</b></font>");
            CommonHelper.OpenURL(settingBOM_url);
            BOMSettingPage.Instance.SelectGroupByParameter(false,string.Empty);

            //14. Go to Community detail which relevant to this house, generate BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 14: Go to Community detail which relevant to this house, generate BOM.</b></font>");
            CommonHelper.OpenURL(CommunityHouseBOM_url);
            CommunityHouseBOMDetailPage.Instance.GenerateHouseBOM();
            CommunityHouseBOMDetailPage.Instance.VerifyItemOnHouseBOMGrid(houseQuantities_HouseBOM_SpecificCommunity);

            //Step 15a.1 : Export CSV House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 15a.1 : Export CSV House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (6)", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (6)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Step 15a.2 : Export EXCEL House BOM by “Export Selected House” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 15a.2 : Export EXCEL House BOM by “Export Selected House” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (6)", EXPORT_SELECTED_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (6)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_SELECTED_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.EXCEL);

            //Step 15b.1 : Export CSV House BOM by “Export All Houses” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 15b.1 : Export CSV House BOM by “Export All Houses” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (7)", EXPORT_ALL_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_CSV_MORE_MENU, $"{exportFileName} (7)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_ALL_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.CSV);

            //Step 15b.2 : Export EXCEL House BOM by “Export All Houses” option and verify the data.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 15b.2 : Export EXCEL House BOM by “Export All Houses” option and verify the data.</b></font>");
            CommunityHouseBOMDetailPage.Instance.DownloadBaseLineHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (7)", EXPORT_ALL_HOUSES, string.Empty);
            CommunityHouseBOMDetailPage.Instance.ExportHouseBOMFile(EXPORT_Excel_MORE_MENU, $"{exportFileName} (7)", 0, ExportTitleFileConstant.COMMUNITY_HOUSEBOMPRODUCT_TITLE, EXPORT_ALL_HOUSES, string.Empty);
            //CommunityHouseBOMDetailPage.Instance.CompareExportFile(exportFileName, TableType.EXCEL);
            
        }

        [TearDown]
        public void DeleteData()
        {
            
            //Delete File House Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {_housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _housedata.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", _housedata.HouseName) is true)
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", _housedata.HouseName);

            }
            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true)
            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", OPTION_NAME_DEFAULT);
            }

            //Delete All House Quantities In Default Specific Community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete All House Quantities In Default Specific Community .</font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);

            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities_SpecificCommunity.communityCode + '-' + houseQuantities_SpecificCommunity.communityName);
            HouseQuantitiesDetailPage.Instance.DeleteAllHouseQuantities(TYPE_DELETE_HOUSEQUANTITIES);
            
        }
    }
}
