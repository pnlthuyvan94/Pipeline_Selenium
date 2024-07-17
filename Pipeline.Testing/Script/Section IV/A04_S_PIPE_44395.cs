using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_S_PIPE_44395 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        CommunityData community;
        HouseData houseData;
        HouseImportQuantitiesData _houseImportQuantitiesData1;
        HouseImportQuantitiesData _houseImportQuantitiesData2;

        private const string OPTION_NAME_DEFAULT = "QA_RT_Option_44395_Automation";
        private const string OPTION_CODE_DEFAULT = "4395";

        private readonly string HOUSE_CODE_DEFAULT = "4395";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House_44395_Automation";

        private const string PRODUCT_IMPORT = "Products Import";
        private const string BUILDING_GROUP_PHASE_IMPORT = "Building Group/Phases Import";

        private const string PRODUCT_IMPORT_VIEW = "Products";
        private const string BUILDING_GROUP_PHASE_VIEW = "Building Groups and Phases";


        private const string TYPE_FILE_DEFAULT = "Processed Data";

        private const string ImportType1 = "Pre-Import Modification";
        private const string ImportType2 = "Delta (As-Built)";

        [SetUp]

        public void SetupData()
        {
            var optionType = new List<bool>()
            {
                false, false, false
            };


            houseData = new HouseData()
            {
                HouseName = "QA_RT_House_44395_Automation",
                SaleHouseName = "QA_RT_House_44395_Sales_Name",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "4395",
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

            community = new CommunityData()
            {
                Name = "QA_RT_Community_44395_Automation",
                Code = "Automation_44395"
            };

            _houseImportQuantitiesData1 = new HouseImportQuantitiesData()
            {
                OptionName= OPTION_NAME_DEFAULT,
                Products = new List<string>() { "QA_RT_New_Product_Style_Automation_01" },
                BuildingPhases = new List<string>() { "123-QA_RT_BuildingPhase1_Automation" },
            };

            _houseImportQuantitiesData2 = new HouseImportQuantitiesData()
            {
                OptionName = OPTION_NAME_DEFAULT,
                Products = new List<string>() { "QA_RT_New_Product_Style_Automation_01" },
                BuildingPhases = new List<string>() { "123-QA_RT_BuildingPhase1_Automation" },
            };

            
        }

        [Test]
        [Category("Section_IV")]
        public void A04_S_Assets_DetailPage_Houses_ImportPage_HouseQuantitiesImport_Does_Not_Prompt_to_Add_a_BuildingPhase_to_a_Product()
        {
            //I. Pre-Import Modification + Comparison Setup
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I. Pre-Import Modification + Comparison Setup.</font><b>");
            //1. Go to Assets > House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.1  Go to Assets > House.</font><b>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            // Hover over Assets  > click Houses then click a House that will be used for testing.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Hover over Assets  > click Houses then click a House that will be used for testing..</font><b>");
            
            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData.HouseName);
            if (!HousePage.Instance.IsItemInGrid("Name", houseData.HouseName) is true)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(houseData);
                string updateMsg = $"House {houseData.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            else
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData.HouseName);

            }
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseOptionDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", community.Name) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(community.Name);
            }

            //2. Click on Upload Button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.2 Click on Upload Button.</font><b>");
            HouseDetailPage.Instance.LeftMenuNavigation("Import");
            string HouseImport_url = HouseImportDetailPage.Instance.CurrentURL;
            //Import House Quantities
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();

            //3. In Import Type dropdown, choose “Pre-Import Modification”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.3 In Import Type dropdown, choose “Pre-Import Modification”.</font><b>");
            //4. Click on Select File and choose Plus minus XML file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.4 Click on Select File and choose Plus minus XML file.</font><b>");
            //5. Click on Upload button at the right bottom of the modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.5 Click on Upload button at the right bottom of the modal.</font><b>");
            //6. In the House Material Files grid, click on checkbox to select the uploaded file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.6 In the House Material Files grid, click on checkbox to select the uploaded file.</font><b>");
            //7. Click on Start Comparison Setup button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.7 Click on Start Comparison Setup button.</font><b>");
            //8. In Un-Modified Option to Import panel, click on checkbox to select the option, then click on Add button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.8 In Un-Modified Option to Import panel, click on checkbox to select the option, then click on Add button.</font><b>");
            //9. Click on Generate Comparisons
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.9 Click on Generate Comparisons.</font><b>");
            //10. Click on Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.10 Click on Import button.</font><b>");
            //11.Click on Finish Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.11 Click on Finish Import</font><b>");
            
            HouseImportDetailPage.Instance.UploadFileAndComparisonSetup(ImportType1, _houseImportQuantitiesData1, false, "ImportHouseQuantities_DefaultCommunity_PIPE_44395_1.xml");
            
            //12. Go to House > Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>I.12 Go to House > Quantities</font><b>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.VerifyHouseQuantitiesIsNotDisplay();
            
            //II. Pre-Import Modification + Generate Report View
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II. Pre-Import Modification + Generate Report View</font><b>");
            //1. Navigate to House Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.1. Navigate to House Import page</font><b>");

            CommonHelper.OpenURL(HouseImport_url);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();

            //2. Click on Upload Button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.2. Click on Upload Button</font><b>");
            //3. In Import Type dropdown, choose “Pre-Import Modification”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.3. In Import Type dropdown, choose “Pre-Import Modification”</font><b>");
            //4. Click on Select File and choose Plus minus XML file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.4. Click on Select File and choose Plus minus XML file</font><b>");
            //5. Check on checkbox
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.5. Check on checkbox</font><b>");
            //6. In the House Material Files grid, click on checkbox to select the uploaded file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.6. In the House Material Files grid, click on checkbox to select the uploaded file</font><b>");
            
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(ImportType1, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_44395_2.xml");

            //7. Click on Start Generate Report View button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.7. Click on Start Generate Report View button</font><b>");
            //8. Click on Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.8. Click on Import button</font><b>");
            //9. Click on Finish Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.9. Click on Finish Import</font><b>");
            HouseImportDetailPage.Instance.ImportFileWithBuildingPhaseIsNotAssigned();

            //10. Go to House > Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>II.10. Go to House > Quantities</font><b>");           
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.VerifyHouseQuantitiesIsNotDisplay();
            
            //III. Delta (As-Built) + Comparison Setup
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III. Delta (As-Built) + Comparison Setup</font><b>");
            //1.Navigate to House Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.1 Navigate to House Import page</font><b>");

            CommonHelper.OpenURL(HouseImport_url);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();

            //2. Click on Upload Button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.2. Click on Upload Button</font><b>");
            //3. In Import Type dropdown, choose “Delta (As-Built)”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.3. In Import Type dropdown, choose “Delta (As-Built)”</font><b>");
            //4. Click on Select File and choose As-built XML file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.4. Click on Select File and choose As-built XML file</font><b>");
            //5. Click on Upload button at the right bottom of the modal
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.5. Click on Upload button at the right bottom of the modal</font><b>");
            //6. In the House Material Files grid, click on checkbox to select the uploaded file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.6. In the House Material Files grid, click on checkbox to select the uploaded file</font><b>");
            //7. Click on Start Comparison Setup button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.7. Click on Start Comparison Setup button</font><b>");
            //8. Click on Generate Comparisons
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.8. Click on Generate Comparisons</font><b>");
            //9. Click on Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.9. Click on Import button</font><b>");
            //10. Click on Finish Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.10. Click on Finish Import</font><b>");

            HouseImportDetailPage.Instance.UploadFileAndComparisonSetup(ImportType2, _houseImportQuantitiesData2, false, "ImportHouseQuantities_DefaultCommunity_PIPE_44395_3.xml");          

            //11. Go to House > Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>III.11. Go to House > Quantities</font><b>");
            HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.VerifyHouseQuantitiesIsNotDisplay();

            //IV. Delta (As-Built) + Generate Report View
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV. Delta (As-Built) + Generate Report View</font><b>");
            
            //1. Navigate to House Import page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.1. Navigate to House Import page</font><b>");
            CommonHelper.OpenURL(HouseImport_url);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
            
            //2. Click on Upload Button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.2. Click on Upload Button</font><b>");
            //3. In Import Type dropdown, choose “Delta (As-Built)”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.3. In Import Type dropdown, choose “Delta (As-Built)”</font><b>");
            //4. Click on Select File and choose As-built XML file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.4. Click on Select File and choose As-built XML file</font><b>");
            //5. Check on the checkbox
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.5. Check on the checkbox</font><b>");
            //6. In the House Material Files grid, click on checkbox to select the uploaded file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.6. In the House Material Files grid, click on checkbox to select the uploaded file</font><b>");
            
            HouseImportDetailPage.Instance.ImportHouseQuantitiesAndGenerationReportView(ImportType2, string.Empty, OPTION_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_44395_4.xml");
            
            //7. Click on Start Generate Report View button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.7. Click on Start Generate Report View button</font><b>");
            //8. Click on Import button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.8. Click on Import button</font><b>");
            //9. Click on Finish Import
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.9. Click on Finish Import</font><b>");
            HouseImportDetailPage.Instance.ImportFileWithBuildingPhaseIsNotAssigned();

            //10. Go to House > Quantities
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>IV.10. Go to House > Quantities</font><b>");
            HouseCommunities.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.VerifyHouseQuantitiesIsNotDisplay();

        }
    }
}
