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
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Common.Utils;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Enums;
using Pipeline.Common.Constants;

namespace Pipeline.Testing.Script.Section_III
{
    class A04_C_PIPE_36148 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        private const string QtyParam1 = "11.00";
        private const string QtyParam2 = "22.00";
        private const string QtyParam3 = "33.00";
        private const string ImportType = "Pre-Import Modification";
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\PIPE_36148";
        OptionData optionData;
        CommunityData communityData;
        SeriesData seriesData;
        HouseData houseData, houseSaleHouseName;
        BuildingGroupData buildingGroupData;
        BuildingPhaseData buildingPhaseData, buildingPhaseDataDefault;
        ManufacturerData manuData1, manuData2;
        StyleData styleData, styleDataDefault;
        ProductData productData, productDataDefault;
        QuantitiesDetailData quantitiesDefaultDetailData, quantitiesDetailData2, quantitiesDetailData1;

        [SetUp]
        public void SetupData()
        {
            var optionTypeGlobalF = new List<bool>()
            {
                false, false, false
            };
            optionData = new OptionData()
            {
                Name = "RT_QA_Automation_Option_36148",
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
                Name = "RT_QA_Automation_Community_36148",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "ComAu5",
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
                Name = "RT_QA_Automation_SeriesData_36148",
                Code = "6148",
                Description = "For 190 sprint"
            };

            //Create house (2 houses)
            houseData = new HouseData()
            {
                HouseName = "RT_QA_Automation_House_36148",
                SaleHouseName = "RT_QA_Automation_House_36148",
                Series = seriesData.Name,
                PlanNumber = "0918",
                BasePrice = string.Empty,
                SQFTBasement = string.Empty,
                Description = "Testsp190"
            };
            houseSaleHouseName = new HouseData()
            {
                SaleHouseName = "RT_QA_Automation_House_36148"
            };
            
            //Create building group, building phase
            buildingGroupData = new BuildingGroupData()
            {
                Code = "190",
                Name = "SPA_190"
            };

            buildingPhaseData = new BuildingPhaseData()
            {
                Code = "1901",
                Name = "RT_QA_Automation_BuildingPhase_36148",
                BuildingGroupName = buildingGroupData.Name,
                BuildingGroupCode = buildingGroupData.Code,
                Taxable = false,
            };
            buildingPhaseDataDefault = new BuildingPhaseData()
            {
                Code = "1902",
                Name = "RT_QA_Automation_BuildingPhase_36148_D",
                BuildingGroupName = buildingGroupData.Name,
                BuildingGroupCode = buildingGroupData.Code,
                Taxable = false,
            };
            //Create Manufacturer
            manuData1 = new ManufacturerData()
            {
                Name = "RT_QA_Manufacturer_36148_D"
            };
            manuData2 = new ManufacturerData()
            {
                Name = "RT_QA_Manufacturer_36148"
            };
            //Create Style
            styleDataDefault = new StyleData()
            {
                Name = "RT_QA_Style_36148_D",
                Manufacturer = manuData1.Name
            };
            styleData = new StyleData()
            {
                Name = "RT_QA_Style_36148",
                Manufacturer = manuData2.Name
            };
            productDataDefault = new ProductData()
            {
                Name = "RT_QA_Automation_Production_36148_D",
                Description = "Pro_190",
                Supplemental = false,
                Manufacture = manuData1.Name,
                Style = styleData.Name,
                BuildingPhase = buildingPhaseData.Name,
                Use = string.Empty,
                Quantities = "11.00",
            };
            productData = new ProductData()
            {
                Name = "RT_QA_Automation_Production_36148",
                Description = "Pro_190",
                Supplemental = false,
                Manufacture = manuData1.Name,
                Style = styleData.Name,
                BuildingPhase = buildingPhaseData.Name,
                Use = string.Empty,
                Quantities = "22.00",
            };

            quantitiesDefaultDetailData = new QuantitiesDetailData()
            {
                Option = optionData.Name + "-" + optionData.Description,
                DependentCondition = string.Empty,
                BuildingPhase = buildingPhaseDataDefault.Code + "-" + buildingPhaseDataDefault.Name,
                Products = productDataDefault.Name,
                Description = string.Empty,
                Style = styleDataDefault.Name,
                Use = "NONE",
                Parameters = string.Empty,
                Quantity = QtyParam1,
                Source = "Pipeline"
            };
            quantitiesDetailData1 = new QuantitiesDetailData()
            {
                Option = optionData.Name + "-" + optionData.Description,
                DependentCondition = string.Empty,
                BuildingPhase = buildingPhaseData.Code + "-" + buildingPhaseData.Name,
                Products = productData.Name,
                Description = string.Empty,
                Style = styleData.Name,
                Use = "NONE",
                Parameters = string.Empty,
                Quantity = QtyParam2,
                Source = "Pipeline"
            };
            quantitiesDetailData2 = new QuantitiesDetailData()
            {
                Option = optionData.Name + "-" + optionData.Description,
                DependentCondition = string.Empty,
                BuildingPhase = buildingPhaseDataDefault.Code + "-" + buildingPhaseDataDefault.Name,
                Products = productDataDefault.Name,
                Description = string.Empty,
                Style = "DEFAULT",
                Use = "NONE",
                Parameters = string.Empty,
                Quantity = QtyParam3,
                Source = "Pipeline"
            };

            //*************ACTION TO SETUP DATA ****************//

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
            //6' Add option1 to community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Add option to community</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_COMMUNITY);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.COMMUNITY_OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.COMMUNITY_OPTION_IMPORT} grid view to import Options to Community.</font>");
            string importFile = $@"{IMPORT_FOLDER}\Community_Option.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.COMMUNITY_OPTION_IMPORT, importFile);

            //7. Create new building group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a building phase</font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingGroupData.Name);
            if (BuildingGroupPage.Instance.IsItemInGrid("Name", buildingGroupData.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Building group has been existed</b></font>");
            }
            else
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(buildingGroupData);
            }

            //8. Create 2 building phases
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create a building phase </font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingPhaseDataDefault.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", buildingPhaseDataDefault.Code) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPhaseCode(buildingPhaseDataDefault.Code).EnterPhaseName(buildingPhaseDataDefault.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(buildingGroupData.Code + "-" + buildingGroupData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }
            //Second BP
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingPhaseData.Code);
            if (BuildingPhasePage.Instance.IsItemInGrid("Code", buildingPhaseData.Code) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPhaseCode(buildingPhaseData.Code).EnterPhaseName(buildingPhaseData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(buildingGroupData.Code + "-" + buildingGroupData.Name);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
            }
            //9. Create new manufacturer
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Create Manufacturer to import Product.</font>");
            ManufacturerPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Manufacturers);
            ManufacturerPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, manuData1.Name);
            if (ManufacturerPage.Instance.IsItemInGrid("Name", manuData1.Name) is false)
            {
                // If Manu doesn't exist then create a new one
                ManufacturerPage.Instance.CreateNewManufacturer(manuData1);
                ManufacturerPage.Instance.CreateNewManufacturer(manuData2);
            }

            //10. Create a style
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare a new Style to import Product.</font>");
            StylePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Styles);
            StylePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, styleDataDefault.Name);
            if (StylePage.Instance.IsItemInGrid("Name", styleDataDefault.Name) is false)
            {
                // If Style doesn't exist then create two new ones
                StylePage.Instance.CreateNewStyle(styleDataDefault);
                StylePage.Instance.CreateNewStyle(styleData);
            }

            //11.Prepare Data: Import Product, before building phase has been created by UI
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Prepare Data: Import Product.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.PRODUCTS_IMPORT_URL_VIEW_PRODUCT);
            string importProductFile = "\\DataInputFiles\\Import\\PIPE_36148\\Import_Products.csv";
            ProductsImportPage.Instance.ImportValidData(ImportGridTitle.PRODUCT_IMPORT, importProductFile);

            //12. Prepare data: Import 1 House Quantities to house1 for default 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> Add house quantities that have given parameters</font>");
            HouseCommunities.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Import");
                HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, string.Empty, "HouseQuantities_DefaultCom_PIPE_36148.xml");
                HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
                HouseDetailPage.Instance.LeftMenuNavigation("Import");
                HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, communityData.Code + "-" + communityData.Name, optionData.Name, "HouseQuantities_SpecificCom_PIPE_36148.xml");

            }

        }

        [Test]
        [Category("Section_III")]
        public void A04_C_Assets_House_Some_Issues_Copying_House()
        {

            //I. SETUP DATA
            //Step 1: Verify house has "Sales House Name:" value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b> **** I. SETUP DATA **** </b></font>");
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  I.1 Verify house has 'Sales house name' value </font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
            HouseDetailPage.Instance.IsSavedWithCorrectValue(houseSaleHouseName);
            //Step 2: Verify in default house quantities, the house has one quantity value
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  I.2 With default house community the house has one quantity value </font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");
            if (HouseQuantitiesDetailPage.Instance.IsHouseQuantitiesValueShownCorrect(1, quantitiesDefaultDetailData))
            {
                ExtentReportsHelper.LogPass("<font color='green'> There is one quantities row that show values in the default community</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> There is one quantities row that not show values in the default community</font>");
            }
            //Step 3: Verify in specific house quantities, the house has two quantities
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  I.3 With specific house community the house has two quantities</font>");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            if (HouseQuantitiesDetailPage.Instance.IsHouseQuantitiesValueShownCorrect(1, quantitiesDetailData1) &&
                HouseQuantitiesDetailPage.Instance.IsHouseQuantitiesValueShownCorrect(2, quantitiesDetailData2) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'> There are two quantities rows shown values in specific community</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> There are two quantities rows are not shown values in specific community</font>");
            }

            //II. Process Copy House
            //Step 4: Go to the house to click copy button the house to new "Copy" house
            ExtentReportsHelper.LogInformation($"<font color='lavender'><b> ***** II. PROCESS COPY HOUSE ***** </b> </font>");
            ExtentReportsHelper.LogInformation($"<font color='lavender'> II.1 Click onto the house to copy as an another house as {houseData.HouseName + "_Copy"} </font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            HousePage.Instance.ClickCopyFromHouse(houseData.HouseName);

            //HousePage.Instance.ClickCopyInCopyHouseModal();
            //Step 5: Enter new name for copied house and set as required to proceed
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  II.2 Enter new name for copied house and check checked properties on new modal</font>");
            HousePage.Instance.EnterNewHouseName(houseData.HouseName + "_Copy");
            if (HousePage.Instance.IsIncludeAllCommunitiesChecked() &&
                HousePage.Instance.IsIncludeAllHousesOptionsChecked() &&
                HousePage.Instance.IsIncludeAllQuantitiesChecked() &&
                HousePage.Instance.IsIncludeAllSalesOptionsChecked() &&
                HousePage.Instance.IsIncludeAllSalesOptionsLogicChecked())
            {
                ExtentReportsHelper.LogPass("<font color='green'> All five properties are checked </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> All five properties are not set checked </font>");
            }

            //Step 6: Verify toast message
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  II.3 Verify toast message </font>");
            HousePage.Instance.ClickCopyInCopyHouseModal();

            if (HousePage.Instance.GetLastestToastMessage().Equals("House copied successfully."))
            {
                ExtentReportsHelper.LogPass("<font color='green'> Toast message shown correctly </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> Toast message is not shown correctly </font>" +
                    $"<font> <b>The real toast message is {HousePage.Instance.GetLastestToastMessage()} </b></font>");
            }
            //Step 6': Verify new copied house now appears in the housepage grid
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  II.4 Verify new name of copied house </font>");
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName + "_Copy"))
            {
                ExtentReportsHelper.LogPass($"<font color='green'> The copied house has name as '{houseData.HouseName + "_Copy"}'</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> The copied house name has name not correct </font>");
            }
            //III. Check data
            //Step 7: Sale house name display in the name is correct
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  III.1 Verify in House Page has name of newly copied house </font>");
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName + "_Copy"))
            {
                ExtentReportsHelper.LogPass("<font color='green'> Sale house name displayed correctly </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> Salse house name displayed not correctly </font>");
            }
            //Step 8: House quantities in both default and specific community are correct
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  III.2 Verify house has 'Sales house name' value </font>");
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName + "_Copy");
            if (HouseDetailPage.Instance.IsSavedWithCorrectValue(houseSaleHouseName) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Sales House Name of copy house '{houseData.HouseName + "_Copy"}' shown correctly </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Sales House Name of copy house '{houseData.HouseName + "_Copy"}' is not shown correctly </font>");
            }

            //Step 9: Move to tab "Quantities", at default house quantities
            ExtentReportsHelper.LogInformation($"<font color='lavender'>  III.3 At default house quantities, the copied house  </font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");
            if (HouseQuantitiesDetailPage.Instance.IsHouseQuantitiesValueShownCorrect(1, quantitiesDefaultDetailData))
            {
                ExtentReportsHelper.LogPass("<font color='green'> Quantities values of the copied house on Default House Quantities shown correctly </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'> Quantities values of the copied house on Default House Quantities shown not correctly </font>");
            }
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            if (HouseQuantitiesDetailPage.Instance.IsHouseQuantitiesValueShownCorrect(1, quantitiesDetailData1) &&
                HouseQuantitiesDetailPage.Instance.IsHouseQuantitiesValueShownCorrect(2, quantitiesDetailData2))
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantities values of the copied house on {communityData.Name} shown correctly</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Quantities values of the copied house on {communityData.Name} shown not correctly </font>");
            }

        }

        [TearDown]
        public void ClearData()
        {

            //Delete all imported files in House Quantities in house1 and house2 respectively
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseDetailPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);
                HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
                HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
                CommonFuncs.SwitchToAnotherOne(houseData.HouseName + "_Copy");
                HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();

                //Delete all quantities files in Quantities in house2 and house1 respectively
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete all quantities files in Quantities in house2 and house1 respectively.</font>");
                HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
                HouseQuantitiesDetailPage.Instance.FilterByCommunity("Default House Quantities");
                HouseImportDetailPage.Instance.DeleteAllOptionQuantities();
                HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
                CommonFuncs.SwitchToAnotherOne(houseData.HouseName);
                HouseQuantitiesDetailPage.Instance.FilterByCommunity("Default House Quantities");
                HouseImportDetailPage.Instance.DeleteAllOptionQuantities();
                HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
                HouseImportDetailPage.Instance.DeleteAllOptionQuantities();

            }
            //Delete Product - 2 products
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Products.</font>");
            HouseDetailPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.Products);
            ProductPage.Instance.FilterItemInGrid("Product Name", GridFilterOperator.Contains, productData.Name);
            if (ProductPage.Instance.IsItemInGrid("Product Name", productData.Name) is true)
            {
                ProductPage.Instance.DeleteProduct(productData.Name);
                ProductPage.Instance.DeleteProduct(productDataDefault.Name);
            }

            //Delete BP - 2BP
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete Buidling Phase</font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, buildingPhaseData.Name);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", buildingPhaseData.Name) is true)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Name", buildingPhaseData.Name);
                if (BuildingPhasePage.Instance.IsItemInGrid("Name", buildingPhaseDataDefault.Name) is true)
                {
                    BuildingPhasePage.Instance.DeleteItemInGrid("Name", buildingPhaseDataDefault.Name);
                }
            }

            //Delete Option, to de-assignment with house and community           
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

            //Go to house to delete copy house          
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Delete the Copied House Copy </font>");
            HousePage.Instance.DeleteHouse(houseData.HouseName + "_Copy");

        }
    }
}
