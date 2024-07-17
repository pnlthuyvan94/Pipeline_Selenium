using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Manufactures;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Estimating.Styles;
using Pipeline.Testing.Pages.Import;
using System.Collections.Generic;
using Pipeline.Common.Utils;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Enums;
using Pipeline.Common.Constants;
using Pipeline.Testing.Pages.Estimating.Uses.AddUses;
using Pipeline.Testing.Pages.Estimating.Uses;
using Pipeline.Testing.Pages.Assets.Options.Products;
using System;
using Pipeline.Testing.Pages.Assets.House.HouseBOM;
using Pipeline.Testing.Pages.Assets.House.HouseBOM.BOMTracing;
using Pipeline.Testing.Pages.Settings.BOMSetting;

namespace Pipeline.Testing.Script.Section_IV
{
    class A05_E_PIPE_42429 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string QtyParam1 = "11.00";
        private const string QtyParam2 = "22.00";       
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\PIPE_42429";
        OptionData optionData;
        CommunityData communityData;
        SeriesData seriesData;
        HouseData houseData;
        BuildingGroupData buildingGroupData;
        BuildingPhaseData buildingPhaseData;
        ManufacturerData manuData;
        StyleData styleData;
        ProductData productData, productData_Option_1, productData_Option_2;
        QuantitiesDetailData quantitiesDetailDefaultData;
        UsesData Use_Data;
        OptionQuantitiesData optionQuantitiesData, valuesOnOptionHouseQuantity, ValuesOptionQuantitiesDefaultComData, optionQuantitiesDefaultComData;
        HouseQuantitiesData houseQuantities_HouseBOM, houseQuantities_HouseBOMDefault;
        ProductToOptionData productToOption1, productToOption2;
        static string houseId = string.Empty;
        static string rootLink = string.Empty;

        private const string BUILDINGPHASE_DEFAULT = "2429" + "-" + "RT_QA_Automation_BuildingPhase_42429";

        [SetUp]
        public void SetupData()
        {
            //--PART 1 Create information

            var optionTypeGlobalF = new List<bool>()
            {
                false, false, false
            };
            optionData = new OptionData()
            {
                Name = "RT_QA_Automation_Option1_42429",
                Number = string.Empty,
                SquareFootage = 0,
                Description = "FOR 191",
                SaleDescription = "191_1",
                OptionGroup = "",
                OptionRoom = string.Empty,
                CostGroup = "All Rooms",
                OptionType = "Design",
                Price = 191.00,
                Types = optionTypeGlobalF
            };
            //Create Community 
            communityData = new CommunityData()
            {
                Name = "RT_QA_Automation_Community_42429",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "ComAu6",
                Status = "Open",
                Description = "for 190",
                DrivingDirections = "for 190",
                Slug = "Community-Auto",
            };
            seriesData = new SeriesData()
            {
                Name = "RT_QA_Automation_SeriesData_42429",
                Code = "2429",
                Description = "For 191 sprint"
            };
            //Create house
            houseData = new HouseData()
            {
                HouseName = "RT_QA_Automation_House1_42429",
                SaleHouseName = "Name191_1",
                Series = seriesData.Name,
                PlanNumber = "0919"
            };

            Use_Data = new UsesData()
            {
                Name = "RT_QA_Automation_42429",
                SortOrder = "1"
            };
            buildingGroupData = new BuildingGroupData()
            {
                Code = "191",
                Name = "SPA_191"
            };
            buildingPhaseData = new BuildingPhaseData()
            {
                Code = "2429",
                Name = "RT_QA_Automation_BuildingPhase_42429",
                BuildingGroupName = buildingGroupData.Name,
                BuildingGroupCode = buildingGroupData.Code,
                Taxable = false
            };

            //Create Manufacturer
            manuData = new ManufacturerData()
            {
                Name = "RT_QA_Manufacturer_42429"
            };
            //Create Style
            styleData = new StyleData()
            {
                Name = "RT_QA_Style_42429",
                Manufacturer = manuData.Name
            };
            productData = new ProductData()
            {
                Name = "RT_QA_Automation_Production_42429",
                Description = "Pro_191",
                Manufacture = manuData.Name,
                Style = styleData.Name,
                Category = string.Empty,
                BuildingPhase = buildingPhaseData.Name,
                Use = string.Empty,
                Quantities = QtyParam1,
                Parameter = string.Empty
            };
            optionQuantitiesData = new OptionQuantitiesData
            {
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.HouseName,
                OptionName = optionData.Name,
                BuildingPhase = buildingPhaseData.Name,
                Category = "NONE",
                ProductName = productData.Name,
                ProductDescription = productData.Description,
                Style = styleData.Name,
                Condition = false,
                Use = Use_Data.Name,
                Quantity = QtyParam1,
                Source = "Pipeline"
            };

            optionQuantitiesDefaultComData = new OptionQuantitiesData
            {
                Community = "Default House Quantities",
                House = houseData.HouseName,
                OptionName = optionData.Name,
                BuildingPhase = buildingPhaseData.Name,
                ProductName = productData.Name,
                ProductDescription = productData.Description,
                Category = "NONE",
                Style = styleData.Name,
                Condition = false,
                Use = Use_Data.Name,
                Quantity = QtyParam2,
                Source = "Pipeline"
            };
            valuesOnOptionHouseQuantity = new OptionQuantitiesData
            {
                Community = communityData.Code + "-" + communityData.Name,
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Dependent_Condition = string.Empty,
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                ProductName = productData.Name,
                Style = styleData.Name,
                Use = Use_Data.Name,
                Parameters = string.Empty,
                Quantity = QtyParam1,
                Source = "Pipeline"
            };
            ValuesOptionQuantitiesDefaultComData = new OptionQuantitiesData
            {
                Community = "Default House Quantities",
                House = houseData.PlanNumber + "-" + houseData.HouseName,
                Dependent_Condition = string.Empty,
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                ProductName = productData.Name,
                Style = styleData.Name,
                Use = Use_Data.Name,
                Parameters = string.Empty,
                Quantity = QtyParam2,
                Source = "Pipeline"
            };


            productData_Option_1 = new ProductData()
            {
                Name = productData.Name,
                Style = styleData.Name,
                Use = Use_Data.Name,
                Quantities = QtyParam1,
                Unit = "NONE"
            };

            productToOption1 = new ProductToOptionData()
            {
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                ProductList = new List<ProductData> { productData_Option_1 }
            };

            houseQuantities_HouseBOM = new HouseQuantitiesData()
            {
                communityCode = communityData.Code,
                communityName = communityData.Name,
                optionName = optionData.Name,
                productToOption = new List<ProductToOptionData> { productToOption1 },
            };

            productData_Option_2 = new ProductData()
            {
                Name = productData.Name,
                Style = styleData.Name,
                Use = Use_Data.Name,
                Quantities = QtyParam2,
                Unit = "NONE"
            };
            productToOption2 = new ProductToOptionData()
            {
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                ProductList = new List<ProductData> { productData_Option_2 }
            };
            houseQuantities_HouseBOMDefault = new HouseQuantitiesData()
            {
                communityCode = communityData.Code,
                communityName = communityData.Name,
                optionName = optionData.Name,
                productToOption = new List<ProductToOptionData> { productToOption2 },
            };

            quantitiesDetailDefaultData = new QuantitiesDetailData()
            {
                Option = optionData.Name + "-" + optionData.Description,
                DependentCondition = string.Empty,
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                Products = productData.Name,
                Description = string.Empty,
                Style = styleData.Name,
                Use = Use_Data.Name,
                Parameters = string.Empty,
                Quantity = QtyParam2,
                Source = "Pipeline"
            };

            //ACTION TO SETUP DATA
            //1. Create Option

            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>  Create {optionData.Name} as normal</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData.Name) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(optionData);
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option {optionData.Name} is displayed in grid.</font>");
            }
            //2. Create a community (specific)
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Create Specific Community with name as {communityData.Name}.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {communityData.Name} is displayed in grid.</font>");
            }
            else
            {
                //Create a new community (specific one)
                CommunityPage.Instance.CreateCommunity(communityData);
                string _expectedMessage = $"Could not create Community with name {communityData.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { communityData.Name}.");
                }
            }
            //3. Create series
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create Series.</font>");
            SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, seriesData.Name);
            if (!SeriesPage.Instance.IsItemInGrid("Name", seriesData.Name))
            {
                //Create a new series
                SeriesPage.Instance.CreateSeries(seriesData);
            }
            //4.Create a house
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Create two houses as {houseData.HouseName} </font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {houseData.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is false)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(houseData);
                string updateMsg = $"House {houseData.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            //5. Add option to house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Add two options to house1</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
                string url = HousePage.Instance.CurrentURL;
                string subString = "Builder";
                string houseIdRaw = url.Split(new string[] { subString }, StringSplitOptions.None)[1];
                houseId = houseIdRaw.Split('?')[1];
                rootLink = url.Split(new string[] { subString }, StringSplitOptions.None)[0];
                HouseDetailPage.Instance.LeftMenuNavigation("Options");
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", optionData.Name) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(optionData.Name + " - " + optionData.Number);
                    HouseOptionDetailPage.Instance.WaitAddOptionModalLoadingIcon();
                    ExtentReportsHelper.LogInformation(null, $"{optionData.Name} has been added to {houseData.HouseName}");
                }
            }

            //6. Add house into the specific community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Add house to the specific community </font>");
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
            //7 Add option to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Add option to community</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_OPTION_IMPORT} grid view to import Options to Community.</font>");
            string importFile = $@"{IMPORT_FOLDER}\Community_Option.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_OPTION_IMPORT, importFile);

            //8. Create new building group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a building group</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.NoFilter, string.Empty);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingGroupData.Name);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", buildingGroupData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Building group has been existed</b></font>");
            }
            else
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //9. Create building phase
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a building phase </font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.NoFilter, string.Empty);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", buildingPhaseData.Code) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPhaseCode(buildingPhaseData.Code).EnterPhaseName(buildingPhaseData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(buildingGroupData.Code + "-" + buildingGroupData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }

            //10. Create new manufacturer
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData);
            }

            //11. Create a style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleData.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleData.Name) is false)
            {
                //If Style doesn't exist then create new ones
                StylePage.Instance.CreateNewStyle(styleData);
            }

            //12.Import products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            string importProductFile = "\\DataInputFiles\\Import\\PIPE_42429\\Import_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);
            //13. Create Use
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create Use.</font>");
            UsesPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Uses);
            UsesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, Use_Data.Name);
            if (UsesPage.Instance.IsItemInGrid("Name", Use_Data.Name) is false)
            {
                UsesPage.Instance.CreateNewUse(Use_Data);
            }

        }
        [Test]
        [Category("Section_IV")]
        public void A05_E_Assets_Detail_Pages_Options_Products_Add_House_Quantity()
        {

            //Step 0: Set the House BOM with parameter is False
            ExtentReportsHelper.LogInformation("<font color='lavender'>------------- 0. SETTING HOUSE BOM PARAMETERS and GROUP USE ------------</font>");
            HouseBOMDetailPage.Instance.NavigateURL("/BuilderBom/Settings/Default.aspx");
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);
            BOMSettingPage.Instance.SelectHouseBOMGroupByUse(true);

            //Step 1: Go to option/Products, add Option House Product modal 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> -------- I. Setup BOM Trace with specific House Community------</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>  I.1 Add House Option Quantities for the Option</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData.Name);
            OptionPage.Instance.SelectItemInGridWithTextContains("Name", optionData.Name);
            OptionPage.Instance.LeftMenuNavigation("Products");
            string urlOption = HousePage.Instance.CurrentURL;
            ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionQuantitiesData);
            //Step 2: Verify the toast message of creating a house option quantities success
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> I.2 Verify toast message of create a successful House Option quantities</font>");
            if (ProductsToOptionPage.Instance.GetLastestToastMessage().Equals("House Quantity created successfully!"))
            {
                ExtentReportsHelper.LogPass($"<font color='green'> House Quantity created a success </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> House Quantity created not a success </font>");
            }
            //Step 3: Verify the row of data shown is correct and verify toast message
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> I.3 Verify data shown on House Option Quantities</font>");
            // Verify items in the grid view are same as the expected setting data or not.
            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Products", valuesOnOptionHouseQuantity.ProductName) is true
                && ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Building Phase", BUILDINGPHASE_DEFAULT) is true
                && ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Style", valuesOnOptionHouseQuantity.Style) is true
                && ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Use", valuesOnOptionHouseQuantity.Use) is true
                && ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Quantity", valuesOnOptionHouseQuantity.Quantity.ToString()) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up data for Option quantities on product <b>'{valuesOnOptionHouseQuantity.ProductName}'</b> is correct.</font>");
            else
                ExtentReportsHelper.LogFail("<font color='yellow'>The set up data for Option quantities on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected Building phase: {valuesOnOptionHouseQuantity.BuildingPhase}" +
                    $"<br>The expected Product: {valuesOnOptionHouseQuantity.ProductName}" +
                    $"<br>The expected Style: {valuesOnOptionHouseQuantity.Style}" +
                    $"<br>The expected Use: {valuesOnOptionHouseQuantity.Use}" +
                    $"<br>The expected Quantities: {valuesOnOptionHouseQuantity.Quantity}</br></font>");

            //Step 4: Move to house bom, select specific community and generate House BOM, and expand 4 layers
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> I.4 Generate House BOM </font>");
            CommonHelper.OpenURL(rootLink + "BuilderBom/HouseBom/HouseBom.aspx?" + houseId);
            HouseBOMDetailPage.Instance.SelectCommunity(communityData.Code + "-" + communityData.Name);
            HouseBOMDetailPage.Instance.ClickOnBasicHouseBOMView();
            HouseBOMDetailPage.Instance.GenerateHouseBOM(communityData.Code + "-" + communityData.Name);
            //Step 5: Verify house quantity and click on quantity to go to BOM trace
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.5 Expand to Use layer to check quantity </font>");
            HouseBOMDetailPage.Instance.ViewBOMtrace(houseQuantities_HouseBOM);
            CommonHelper.SwitchLastestTab();           
            //Step 6: On BOM trace, verify values of BOM trace values
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.6 Verify that BOM tracing page in new tab</font>");
            string urlHouseBom = HousePage.Instance.CurrentURL;
            if (urlHouseBom.Contains("BuilderBom/Tracing/Default") == true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantity is correct and navigated to tracing page </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Quantity is not correct and/or not navigated to tracing page </font>");
            }
            if (BOMTracingPage.Instance.VerifyNormalBtnPage())
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Tracing page appears with its proper UI </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Tracing page does not appears with its proper UI </font>");
            }

            //Step 7: Delete house option quantities
            ExtentReportsHelper.LogInformation($"<font color='lavender'>I.7 Delete House option quantity and verify its delete toast message</font>");
            CommonHelper.SwitchTab(0);
            CommonHelper.OpenURL(urlOption);
            ProductsToOptionPage.Instance.DeleteAllProductToOptionInHouseOptionQuantity();
            string houseOptionQuantityTM = ProductsToOptionPage.Instance.GetLastestToastMessage();
            if (houseOptionQuantityTM.Contains("house quantities deleted."))
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Delete the house quantities a success </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Delete the house quantities not a success </font>");
            }

            //Step 8: Now add House Option Quantities with Default House Community
            ExtentReportsHelper.LogInformation($"<font color='lavender'>II. Verify BOM trace towards Default House Community Delete House option quantity </font>");
            ExtentReportsHelper.LogInformation($"<font color='lavender'>II.1 Add House Option Quantities </font>");
            ProductsToOptionPage.Instance.AddHouseOptionQuantities(optionQuantitiesDefaultComData);

            //Step 9: Verify the shown values for default community
            ExtentReportsHelper.LogInformation($"<font color='lavender'>II.2 Verify shown values for House Option Quantities </font>");
            if (ProductsToOptionPage.Instance.IsOptionHouseQuantitiesValueShownCorrect(1, ValuesOptionQuantitiesDefaultComData))
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Values shown in the Option house quantity row are corrects </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Values shown in the Option house quantity row are not corrects </font>");
            }

            //Step 10 Go to House/Quantities, pick Default House Quantities 
            ExtentReportsHelper.LogInformation($"<font color='lavender'>II.3 Verify shown values for House Quantities </font>");
            CommonHelper.OpenURL(rootLink + "BuilderBom/HouseBom/HouseQuantities.aspx?" + houseId);
            if (HouseQuantitiesDetailPage.Instance.IsHouseQuantitiesValueShownCorrect(1, quantitiesDetailDefaultData))
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Values shown in the house quantity row are corrects </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Values shown in the house quantity row are not corrects </font>");
            }

            //Step 11 Go to House BOM, select specific community and generate BOM
            ExtentReportsHelper.LogInformation($"<font color='lavender'>II.4 Generate BOM for specific community </font>");
            HouseBOMDetailPage.Instance.LeftMenuNavigation("House BOM");
            //Select specific community
            HouseBOMDetailPage.Instance.SelectCommunity(communityData.Code + "-" + communityData.Name);
            HouseBOMDetailPage.Instance.ClickOnBasicHouseBOMView();
            HouseBOMDetailPage.Instance.GenerateHouseBOM(communityData.Code + "-" + communityData.Name);

            //Step 12: Verify house quantity and click on quantity to go to BOM trace
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.4. View BOM trace, check quantity and expand to Use layer</font>");
            HouseBOMDetailPage.Instance.ViewBOMtrace(houseQuantities_HouseBOMDefault);
            CommonHelper.SwitchLastestTab();
            //Step 13: On BOM trace, verify values of BOM trace values
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.5. Verify that BOM tracing page in new tab</font>");
            string urlHouseBomCheck = HousePage.Instance.CurrentURL;
            if (urlHouseBomCheck.Contains("BuilderBom/Tracing/Default") == true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantity is correct and navigated to tracing page </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Quantity is not correct not navigated to tracing page </font>");
            }
            BOMTracingPage.Instance.VerifyNormalBtnPage();
          
            //Step 14: Delete house option quantities
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.6. Delete this House option quantity</font>");
            CommonHelper.OpenURL(urlOption);
            ProductsToOptionPage.Instance.DeleteAllProductToOptionInHouseOptionQuantity();

        }
        [TearDown]
        public void ClearData()
        {
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
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_BUILDING_PHASES_URL);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingPhaseData.Name);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", buildingPhaseData.Name) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", buildingPhaseData.Name);
            }

            //Delete Option, to de-assignment with house and community
            //Go to option
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Delete assignment to options {optionData.Name}.</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData.Name) is true)
            {
                OptionPage.Instance.SelectItemInGrid("Name", optionData.Name);
                OptionPage.Instance.LeftMenuNavigation("Assignments");
                OptionDetailPage.Instance.DeleteAllAssignmentToOption();
            }    
            //Go to community to deassign it
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Deassign community {communityData.Name}</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
                CommunityPage.Instance.DeletAllAssignments();
            }

        }
    }
}
