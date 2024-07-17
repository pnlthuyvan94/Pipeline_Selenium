using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseBOM.BOMTracing;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Estimating.Worksheet;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail;
using Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetProducts;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using System;
using System.Collections.Generic;


namespace Pipeline.Testing.Script.Section_IV
{
    class A04_U_PIPE_36138 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        OptionData optionData1;
        CommunityData communityData;
        SeriesData seriesData;
        HouseData houseData1;
        BuildingGroupData buildingGroupData;
        BuildingPhaseData buildingPhaseData;
        ManufacturerData manuData;
        StyleData styleData;
        ProductData productData;
        private const string ImportType = "Pre-Import Modification";     
        private const string QtyParam1 = "33.00";
        private const string QtyParam2 = "66.00";
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\PIPE_36138";
        private const string WORKSHEET_NAME = "QA_RT_Worksheet_PIPE_36138";
        private readonly string WORKSHEET_CODE = "WS36138";       
        static string houseId = string.Empty;
        static string rootLink = string.Empty;

        private const string OPTION_NAME = "RT_QA_Automation_Option1_36138";
        private const string COMMUNITY_NAME = "RT_QA_Automation_Community_36138";
        private const string COMMUNITY_CODE = "ComAu4";

        private HouseQuantitiesData houseQuantities;
        private HouseQuantitiesData houseQuantities_HouseBOM;
        private ProductToOptionData productToOption1;
        private ProductData productData_Option_1;
        private ProductData worksheetProductData;
        private WorksheetData worksheetData;


        [SetUp]
       
        public void SetupData()
        {
            
            //--PART 1 Create information

            var optionTypeGlobalF = new List<bool>()
            {
                false, false, false
            };

            optionData1 = new OptionData()
            {
                Name = "RT_QA_Automation_Option1_36138",
                Number = string.Empty,
                SquareFootage = 0,
                Description = "FOR 190",
                SaleDescription = "190_1",
                OptionGroup = "",
                OptionRoom = string.Empty,
                CostGroup = "All Rooms",
                OptionType = "Design",
                Price = 190.00,
                Types = optionTypeGlobalF
            };

            //Create Community 
            communityData = new CommunityData()
            {
                Name = "RT_QA_Automation_Community_36138",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "ComAu4",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "for 190",
                DrivingDirections = "for 190",
                Slug = "Community-Auto",
            };

            //Create Series
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Automation_SeriesData_36138",
                Code = "6138",
                Description = "For 190 sprint"
            };

            //Create house (2 houses)
            houseData1 = new HouseData()
            {
                HouseName = "RT_QA_Automation_House1_36138",
                SaleHouseName = "Name190_1",
                Series = seriesData.Name,
                PlanNumber = "0915",
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
                Description = "Testsp190"
            };

            //Create building group, building phase
            buildingGroupData = new BuildingGroupData()
            {
                Code = "190",
                Name = "SPA_190"
            };

            buildingPhaseData = new BuildingPhaseData()
            {
                Code = "BP38",
                Name = "RT_QA_Automation_BuildingPhase_36138",
                BuildingGroupName = buildingGroupData.Name,
                BuildingGroupCode = buildingGroupData.Code,
                Taxable = false,
            };

            //Create Manufacturer
            manuData = new ManufacturerData()
            {
                Name = "RT_QA_Manufacturer_36138"
            };
            //Create Style
            styleData = new StyleData()
            {
                Name = "RT_QA_Style_36138",
                Manufacturer = manuData.Name
            };

            //Create product
            productData = new ProductData()
            {
                Name = "RT_QA_Automation_Production_36138",
                Description = "Pro_190",
                Supplemental = false,
                Manufacture = manuData.Name,
                Style = styleData.Name,
                BuildingPhase = buildingPhaseData.Name,
                Quantities = QtyParam1,
            };

            worksheetProductData = new ProductData()
            {
                Name = "RT_QA_Automation_Production_36138",
                Code = "WS_190",
                Style = styleData.Name,
                Unit = string.Empty,
                Quantities = QtyParam2,
            };

            //Worksheet data info
            worksheetData = new WorksheetData()
            {
                Name = WORKSHEET_NAME,
                Code = WORKSHEET_CODE
            };

            productData_Option_1 = new ProductData()
            {
                Name = productData.Name,
                Style = styleData.Name,
                Use = "NONE",
                Quantities = QtyParam1,
                Unit = "NONE"
            };

            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                ProductList = new List<ProductData> { productData_Option_1 }

            };
            houseQuantities= new HouseQuantitiesData()
            {
                communityCode = communityData.Code,
                communityName = communityData.Name,
                optionName = optionData1.Name,
                productToOption = new List<ProductToOptionData> { productToOption1 }
            };

            houseQuantities_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = communityData.Code,
                communityName = communityData.Name,
                optionName = optionData1.Name,
                productToOption = new List<ProductToOptionData> { productToOption1 }
            };

            /********************** Create Worksheet BOM ****************************/

            // -----ACTIONS TO CREATE DATA from INFORMATION ABOVE ------
            // 1.Setup 1 option for normal
            
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> ******* CREATE DATA FOR HOUSE BOM ********</font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Setup option for normal </font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData1.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData1.Name))
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> The Option {optionData1.Name} is displayed in grid.</font>");                
            }
            else
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(optionData1);
            }

            //2. Create a community (specific)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create community specific</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {communityData.Name} is displayed in grid.</font>");
                goto SeriesPart;
            }
            else
            {
                //Create a new community (specific one)
                CommunityPage.Instance.CreateCommunity(communityData);
                string _expectedMessage = $"Could not create Community with name {communityData.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name {communityData.Name}.");
                }
            }

            //3. Create Series
            SeriesPart: ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create series</font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
            if (!SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name))
            {
                //Create a new series
                SeriesPage.Instance.CreateSeries(seriesData);
            }

            //4.Create a house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a house</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {houseData1.HouseName} and create if it doesn't exist.</font color>");
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData1.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData1.HouseName))
            {
                //Create a new house
                HousePage.Instance.CreateHouse(houseData1);
                string updateMsg = $"House {houseData1.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }

            //5. Add options 1 to house  
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Add option to house</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData1.HouseName);
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
            string url = HousePage.Instance.CurrentURL;
            string subString = "Builder";
            string houseIdRaw = url.Split(new string[] {subString}, StringSplitOptions.None)[1];
            houseId = houseIdRaw.Split('?')[1];
            rootLink = url.Split(new string[] { subString }, StringSplitOptions.None)[0];
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", optionData1.Name) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(optionData1.Name + " - " + optionData1.Number);
                HouseOptionDetailPage.Instance.WaitAddOptionModalLoadingIcon();
                ExtentReportsHelper.LogInformation(null, $"{optionData1.Name} has been added to {houseData1.HouseName}");
            }

            //6. Add house1, first standing at house1, into the specific community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Add house to community</font>");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");
            HouseCommunities.Instance.FillterOnGrid("Name", communityData.Name);
            if (HouseCommunities.Instance.IsValueOnGrid("Name", communityData.Name) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(communityData.Name);
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Community with Name {communityData.Name} is displayed in grid");
            }

            //6 Add option1 to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Add option to community</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_OPTION_IMPORT} grid view to import Options to Community.</font>");
            string importFile = $@"{IMPORT_FOLDER}\Community_Option.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_OPTION_IMPORT, importFile);

            //7. Create new building group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a building group</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingGroupData.Name);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", buildingGroupData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Building group has been existed</font>");
            }
            else
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //8. Create building phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a building phase</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", buildingPhaseData.Code) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPhaseCode(buildingPhaseData.Code).EnterPhaseName(buildingPhaseData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(buildingGroupData.Code + "-" + buildingGroupData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }

            //9. Create new manufacturer
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a new Manufacturer to import Product</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (!ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name))
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            //10. Create a style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (!StylePage.Instance.IsItemInGrid("Name", styleData.Name))
            {
                // If Style doesn't exist then create a new one
                StylePage.Instance.CreateNewStyle(styleData);
            }

            //11.Prepare Data: Import Product, before building phase has been created by UI
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Import Product to House.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            string importProductFile = "\\DataInputFiles\\Import\\PIPE_36138\\Import_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);

            //12. Prepare data: Import House Quantities to house1 for specific comms 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Import house quantities.</font>");
            HouseCommunities.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData1.HouseName);
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, COMMUNITY_CODE + "-" + COMMUNITY_NAME, OPTION_NAME, "HouseQuantities_SpecificCom_PIPE_36138.xml");

            //Verify the set up data for product quantities on House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the set up data for product quantities on House.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(houseQuantities.communityCode + "-" + houseQuantities.communityName);

            foreach (ProductToOptionData housequantity in houseQuantities.productToOption)
            {
                foreach (ProductData item in housequantity.ProductList)
                {

                    // Verify items in the grid view are same as the expected setting data or not.
                    if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Option", houseQuantities.optionName) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Building Phase", housequantity.BuildingPhase) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Products", item.Name) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Style", item.Style) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Use", item.Use) is true
                        && HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", item.Quantities) is true)
                        ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{item.Name}'</b> is correct.</font>");
                    else
                        ExtentReportsHelper.LogWarning("<font color='yellow'>The set up data for House quantities on this page is NOT same as expectation. " +
                            "The result after generating a BOM can be incorrect." +
                            $"<br>The expected Option: {houseQuantities.optionName}" +
                            $"<br>The expected Building phase: {housequantity.BuildingPhase}" +
                            $"<br>The expected Product: {item.Name}" +
                            $"<br>The expected Style: {item.Style}" +
                            $"<br>The expected Use: {item.Use}");
                }
            }
            /****************** Create Worksheet ********************/


            //13. Create worksheet
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>*************** CREATE DATA FOR WORKSHEET **************</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> With the house prepared data, go to its House BOM</font>");
            HousePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", WORKSHEET_NAME);
            if (WorksheetPage.Instance.IsItemInGrid("Name", WORKSHEET_NAME) is false)
            {
                WorksheetPage.Instance.CreateNewWorksheet(worksheetData);
                BasePage.PageLoad();
                WorksheetDetailPage.Instance.LeftMenuNavigation("Products");
            }
            else
            {
                WorksheetPage.Instance.SelectItemInGrid("Name", WORKSHEET_NAME);
            }

            WorksheetDetailPage.Instance.LeftMenuNavigation("Products");

            //14. Import product to worksheet
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Import product to worksheet</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDERBOM_IMPORT_URL_WORKSHEET);
            if (!BuilderBOMImportPage.Instance.IsImportGridDisplay(ImportGridTitle.WORKSHEET_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.WORKSHEET_IMPORT} grid view to import Worksheet Quantities.</font>");

            string importFile1 = $@"{IMPORT_FOLDER}\Import_Worksheet_Products.csv";
            BuilderBOMImportPage.Instance.ImportValidData(ImportGridTitle.WORKSHEET_IMPORT, importFile1);
            
        }

        [Test]
        [Category("Section_IV")]
        public void A04_U_AssetsDetail_Houses_ImportPage_BOMTracing_Mini_Detail()
        {
            //Step 0: Set the House BOM with parameter is False
            ExtentReportsHelper.LogInformation("<font color='lavender'>------------- 0. SETTING HOUSE BOM PARAMETERS AS FALSE ------------</font>");         
            HouseBOMDetailPage.Instance.NavigateURL("/BuilderBom/Settings/Default.aspx");       
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);
            //Step 1: With the house prepared data, go to its House BOM
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>------------- I. HOUSE BOM ------------</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.1. Go to House page</font>");
            CommonHelper.OpenURL(rootLink + "BuilderBom/HouseBom/HouseBom.aspx?" + houseId);                       

            //Step 2: Select its Community and then generate BOM and verify toast message as "Successfully processed selected BOM(s)"
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2. Select its community and then generate BOM</font>");
            HouseBOMDetailPage.Instance.ClickOnBasicHouseBOMView();
            HouseBOMDetailPage.Instance.GenerateHouseBOM(communityData.Code + "-" + communityData.Name);          

            //Step 3: Click on Total Quantity to move to page tracing, let Normal first, verify name and quantity on it
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.3. Check quantity and show BOM tracing in new tab</font>");
            HouseBOMDetailPage.Instance.SelectCommunity(communityData.Code + "-" + communityData.Name);
            HouseBOMDetailPage.Instance.ViewBOMtrace(houseQuantities_HouseBOM);
            CommonHelper.SwitchLastestTab();
            string BOMTrace_url = BOMTracingPage.Instance.CurrentURL;
            if(BOMTrace_url.Contains("BuilderBom/Tracing/Default") == true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantity is correct and navigated to tracing page </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Quantity is not correct not navigated to tracing page </font>");
            }             
                        
            //Step 4: Click on Mini, verify it show the quantity and images
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.4. Mini button shown and images shown thereof when click on it</font>");
            //Verify the function of Mini button
            BOMTracingPage.Instance.VerifyMiniBtnFunction();
            if (BOMTracingPage.Instance.VerifyMiniBtnFunction() == true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Mini button works correctly </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Mini button works not correct </font>");
            }
            //Step 5: Click on Detail, it breaks into five pictures
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.5. Detailed button shown and images shown thereof when click on it</font>");
            BOMTracingPage.Instance.VerifyDetailedBtnFunction();
            if (BOMTracingPage.Instance.VerifyDetailedBtnFunction() == true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Detailed button works correctly </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Detailed button works not correct </font>");
            }

            //Step 6: Now go to tab Worksheet, pick a name, go to left menu products
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>--------------------- II. WORKSHEET BOM -------------------</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1. Choose a worksheet and go to detail page </font>");
            HousePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.WorkSheets);
            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", WORKSHEET_NAME);
            WorksheetPage.Instance.SelectItemInGrid("Name", WORKSHEET_NAME);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.2. Go to tab Products</font>");
            WorksheetProductsPage.Instance.LeftMenuNavigation("Products");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.3. Select community and generate worksheet BOM </font>");
            WorksheetProductsPage.Instance.SelectCommunityBOM(communityData.Code + "-" + communityData.Name);
            WorksheetProductsPage.Instance.Click_GenerateBOM();
            if (WorksheetProductsPage.Instance.GetLastestToastMessage().Contains("Report generated"))
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Worksheet BOM generated successfully </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Worksheet BOM was not generated successfully </font>");
            }

            //Step 8: Notice section Worksheet BOM and select its community, then click Generate BOM
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.4. Expand worksheet and verify quantity</font>");
            WorksheetProductsPage.Instance.Click_ExpandWorksheet();
            WorksheetProductsPage.Instance.Click_ExpandSubWorksheet();
            WorksheetProductsPage.Instance.VerifyWorksheetBOMValues("Qty", worksheetProductData, QtyParam2, true);
            CommonHelper.SwitchLastestTab();
            string urlHouseBomWS = HousePage.Instance.CurrentURL;
            if (urlHouseBomWS.Contains("BuilderBom/Tracing/Default") == true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantity is matching and navigated to tracing page successfully </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Quantity is not matching and navigated to tracing page not successfully </font>");
            }
            
            //Move to View mode           
            //Verify the function of Mini button
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.5. Verify Mini button </font>");

            if (BOMTracingPage.Instance.VerifyMiniBtnFunction() == true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Mini button of Worksheet BOM tracing works correctly </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Mini button of Worksheet BOM tracing works not correctly </font>");
            }
            //Step 6: Click on Detail, it breaks into five pictures
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.6. Verify Detailed button </font>");

            if (BOMTracingPage.Instance.VerifyDetailedBtnFunction() == true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Detailed button of Worksheet BOM tracing works correctly </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Detailed button of Worksheet BOM tracing works not correctly </font>");
            }
            
        }

        [TearDown]
        public void ClearData()
        { 
            CommonHelper.SwitchTab(0);
            CommonHelper.CloseAllTabsExcludeCurrentOne();
            //Delete all imported files in House Quantities in the house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseDetailPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData1.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData1.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
                HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
                HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete all quantities files in Quantities in the house.</font>");
                HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
                HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
                HouseImportDetailPage.Instance.DeleteAllOptionQuantities();
            }

            //Delete Product
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Products.</font>");
            HouseDetailPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is true)
            {
                ProductPage.Instance.DeleteProduct(productData.Name);
            }

            //Delete BP
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Buidling Phase</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingPhaseData.Name);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", buildingPhaseData.Name) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", buildingPhaseData.Name);
            }

            //Delete Option, to de-assignment with house and community
            //Go to option1
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Delete assignment to options {optionData1.Name}.</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData1.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData1.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", optionData1.Name);
                OptionPage.Instance.LeftMenuNavigation("Assignments");
                OptionDetailPage.Instance.DeleteAllAssignmentToOption();
            }

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Delete Worksheets '{worksheetData.Name}'</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_WORKSHEETS_URL);

            WorksheetPage.Instance.EnterWorksheetNameToFilter("Name", worksheetData.Name);
            if (WorksheetPage.Instance.IsItemInGrid("Name", worksheetData.Name))
            {
                // Delete worksheet
                WorksheetPage.Instance.DeleteWorkSheet(worksheetData.Name);
            }
            
        }
    }
}

