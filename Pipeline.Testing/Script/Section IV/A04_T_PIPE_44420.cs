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
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Settings.BOMSetting;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_T_PIPE_44420 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        OptionData option;
        CommunityData community;
        CommunityData community1;
        HouseData housedata;

        private readonly int[] indexs = new int[] { };

        private const string ImportType1 = "Pre-Import Modification";
        private const string ImportType2 = "Delta (As-Built)";
        private const string ImportType3 = "CSV";

        private static string COMMUNITY_CODE_DEFAULT = "Automation_44420";
        private static string COMMUNITY_NAME_DEFAULT = "QA_RT_Community_44420_Automation";

        private static string HOUSE_NAME_DEFAULT = "QA_RT_House_44420_Automation";

        private static string OPTION_NAME_DEFAULT = "QA_RT_Option_44420_Automation";
        private static string OPTION_CODE_DEFAULT = "4420";

        private static string FILE1_NAME_DEFAULT = "4420_01";

        private static string FILE2_NAME_DEFAULT = "4420_02";

        private static string FILE3_NAME_DEFAULT = "4420_03";

        private static string FILE4_NAME_DEFAULT = "4420_04";

        private static string FILE5_NAME_DEFAULT = "4420_05";

        private static string FILE6_NAME_DEFAULT = "4420_06";

        private static string FILE7_NAME_DEFAULT = "4420_07";

        private static string FILE8_NAME_DEFAULT = "4420_08";


        private static string TYPE1_NAME_DEFAULT = "Input - XML";
        private static string TYPE2_NAME_DEFAULT = "Input - CSV";
        private static string SOURCE1_NAME_DEFAULT = "Pipeline";
        private static string SOURCE2_NAME_DEFAULT = "Bim";
        private static string SOURCE3_NAME_DEFAULT = "Planswift";
        [SetUp]

        public void SetupData()
        {


            var optionType = new List<bool>()
            {
                false, false, true
            };

            option = new OptionData()
            {
                Name = "QA_RT_Option_44420_Automation",
                Number = "4420",
                SquareFootage = 0,
                Description = "Please do not remove or modify",
                OptionGroup = "NONE",
                OptionRoom = string.Empty,
                CostGroup = "NONE",
                OptionType = "NONE",
                Price = 0.00,
                Types = optionType
            };

            community = new CommunityData()
            {
                Name = "QA_RT_Community_44420_Automation",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "Automation_44420",
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

            community1 = new CommunityData()
            {
                Name = "QA_RT_Community_44420_IV_Automation",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "Automation_44420_IV",
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

            housedata = new HouseData()
            {
                HouseName = "QA_RT_House_44420_Automation",
                SaleHouseName = "QA_RT_House_44420_Automation",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "4420",
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

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to Option Page.</font>");
            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, option.Name);

            if (!OptionPage.Instance.IsItemInGrid("Name", option.Name))
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                if (OptionPage.Instance.AddOptionModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Create Option modal is not displayed.</font>");
                }
                // Create Option - Click 'Save' Button
                OptionPage.Instance.AddOptionModal.AddOption(option);
                string _expectedMessage = $"Option Number is duplicated.";
                string actualMsg = OptionPage.Instance.GetLastestToastMessage();
                if (_expectedMessage.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogFail($"Could not create Option with name { option.Name} and Number {option.Number}.");
                    Assert.Fail($"Could not create Option.");
                }
                BasePage.PageLoad();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Option with name { option.Name} is displayed in grid.</font>");
            }

            //Prepare Community Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Community Page.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {community.Name} is displayed in grid.</font>");
            }
            else
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(community);
                string _expectedMessage = $"Could not create Community with name {community.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { community.Name}.");
                }

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Community Page.</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, community1.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", community1.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {community1.Name} is displayed in grid.</font>");
            }
            else
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(community1);
                string _expectedMessage = $"Could not create Community with name {community1.Name}.";
                if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogFail($"Could not create Community with name { community1.Name}.");
                }
            }


        }
        [Test]
        [Category("Section_IV")]
        public void A04_T_Assets_DetailPage_House_ImportPage_Only_house_files_shown_on_the_screen_import()
        {

            //I. Verify the conditions option of themselves doesn't show on the export BOM
           
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I. Verify the conditions option of themselves doesn't show on the export BOM.</font>");
            //1. Prepare the data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>I.1. Prepare the data.</font>");
            //Make sure current transfer seperation character is ','
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();

            //Verify ability to turn on Group by parameters setting
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);
            SettingPage.Instance.LeftMenuNavigation("BOM");
            string settingBOM_url = SettingPage.Instance.CurrentURL;
            BOMSettingPage.Instance.SelectGroupByParameter(false, string.Empty);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Select Default House BOM View is Basic.</b></font>");
            BOMSettingPage.Instance.SelectDefaultHouseBOMView(true);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change House BOM Product Orientation is turned false.</b></font>");
            BOMSettingPage.Instance.Check_House_BOM_Product_Orientation(false);

            //Navigate to House default page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House default page.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);

            //Insert name to filter and click filter by House Name
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Filter house with name {housedata.HouseName} and create if it doesn't exist.</font>");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, housedata.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", housedata.HouseName) is false)
            {
                //Create a new house
                HousePage.Instance.CreateHouse(housedata);
                string updateMsg = $"House {housedata.HouseName} saved successfully!";
                if (updateMsg.Equals(HouseDetailPage.Instance.GetLastestToastMessage()))
                    ExtentReportsHelper.LogPass(updateMsg);
            }
            else
            {
                //Select filter item to open detail page
                HousePage.Instance.SelectItemInGridWithTextContains("Name", housedata.HouseName);

            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", community.Name) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(community.Name, indexs);
            }

            //Add 3 options in the house option and community option page:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Add options in the house option and community option page</font>");
            //Navigate to House Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Option page.font>");
            HouseCommunities.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION_NAME_DEFAULT + " - " + OPTION_CODE_DEFAULT);
            }

            HouseOptionDetailPage.Instance.LeftMenuNavigation("Import");
            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.NoFilter, string.Empty);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
            //Step 1: Verify can upload and import all files with import Type is “Pre-Import Modification”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 1: Verify can upload and import all files with import Type is Pre-Import Modification.</font>");
            //Step 2: Select the Import Type as “Pre-Import Modification” > click on the “Select File” => open the select files popup
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2: Select the Import Type as “Pre-Import Modification” > click on the “Select File” => open the select files popup.</font>");
            //Step 3: Select more than 10 files with below template > “Open” button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3: Select more than 10 files with below template > “Open” button.</font>");
            //Step 4: Check the imported files data, all data are uploaded successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4: Check the imported files data, all data are uploaded successfully.</font>");
            // 1.a. FileName
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.a. FileName.</font>");
            // 1.b. Options
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.b. Options.</font>");
            // 1.c. Source
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.c. Source.</font>");
            // 1.d. Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.d. Community.</font>");
            // 1.i. If the import community is not linked to the House yet, show the warning popup
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.i. If the import community is not linked to the House yet, show the warning popup.</font>");
            // 1.ii. If the import community is linked to the House, show the Community name in the row
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.ii. If the import community is linked to the House, show the Community name in the row.</font>");
            // 1.e. Using paging to check the data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.e. Using paging to check the data.</font>");

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.5.b. The successful Upload toast message is shown:.</font>");
            HouseImportDetailPage.Instance.OpenUploadFile();
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType1, string.Empty, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_44420_I.xml", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_I_1.xml", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_I_2.xml");
            
            // 1.5. Click on the “Upload” button =>
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.5. Click on the “Upload” button =>.</font>");
            // 1.5.a. The Processing Upload popup is shown with total all the upload files
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1.5.a. The Processing Upload popup is shown with total all the upload files.</font>");
            // 1.5.b. The successful Upload toast message is shown:

            HouseImportDetailPage.Instance.UploadAllFileImport();
            // 1.6. Check the upload data in the “House Material Files” list is corresponding with the content in the imported file
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'> 1.6. Check the upload data in the “House Material Files” list is corresponding with the content in the imported file</font>");
            List<string> listFilenames = new List<string>() { FILE1_NAME_DEFAULT, FILE2_NAME_DEFAULT, FILE3_NAME_DEFAULT };

            foreach (string FileName1 in listFilenames)
            {
                HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FileName1);
                if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FileName1+".xml") is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FileName1}'</b> is displayed correct.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected File Name: {FileName1}" +
                    $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                    $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                    $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                    $"<br>The expected Community: Default House Quantities</br></font>");
                }
            }

            // II. Verify can upload and import all files with import Type is “Delta (As-Built)”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II. Verify can upload and import all files with import Type is “Delta (As-Built)”</font>");
            // 2.1. Go to “ASSETS” > Houses > [a specific house] > “Import” > “Upload” button => The “Upload House Material Files” popup is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.1. Go to “ASSETS” > Houses > [a specific house] > “Import” > “Upload” button => The “Upload House Material Files” popup is displayed</font>");
            // 2.2. Select the Import Type as “Delta (As-Built)” > click on the “Select File” => open the select files popup
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.2. Select the Import Type as “Delta (As-Built)” > click on the “Select File” => open the select files popup</font>");
            // 2.3. Select more than 10 files with below template > “Open” button:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.3. Select more than 10 files with below template > “Open” button:</font>");
            // 2.4. Check the imported files data, all data are uploaded successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4. Check the imported files data, all data are uploaded successfully</font>");
            // 2.4.a.FileName
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.a.FileName</font>");
            // 2.4.b.Options
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.b.Options</font>");
            // 2.4.c.Source
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.c.Source</font>");
            // 2.4.d.Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.d.Community</font>");
            // 2.4.i. If the import community is not linked to the House yet, show the warning popup
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.i. If the import community is not linked to the House yet, show the warning popup</font>");
            // 2.4.ii. If the import community is linked to the House, show the Community name in the row
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.ii. If the import community is linked to the House, show the Community name in the row</font>");
            // 2.4.e. Using paging to check the data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.4.e. Using paging to check the data</font>");
            // 2.5. Click on the “Upload” button =>
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.5. Click on the “Upload” button =></font>");
            // 2.5.a. The Processing Upload popup is shown with total all the upload files
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.5.a. The Processing Upload popup is shown with total all the upload files</font>");
            // 2.5.b. The successful Upload toast message is shown
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.5.b. The successful Upload toast message is shown</font>");
            CommonHelper.RefreshPage();
            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.NoFilter, string.Empty);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
            HouseImportDetailPage.Instance.OpenUploadFile();
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType1, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_44420_II.xml","ImportHouseQuantities_SpecificCommunity_PIPE_44420_II_1.xml", "ImportHouseQuantities_SpecificCommunity_PIPE_44420_II_2.xml");
            HouseImportDetailPage.Instance.UploadAllFileImport();

            // 2.6. Close the Processing Upload popup and check the “House Material Files” list data which is shown properly    
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2.6. Close the Processing Upload popup and check the “House Material Files” list data which is shown properly  </font>");
            foreach (string FileName2 in listFilenames)
            {
                HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FileName2);
                if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FileName2 + ".xml") is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", COMMUNITY_NAME_DEFAULT) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FileName2}'</b> is displayed correct.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected File Name: {FileName2}" +
                    $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                    $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                    $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                    $"<br>The expected Community: {COMMUNITY_NAME_DEFAULT}</br></font>");
                }
            }

            // III. Verify can upload and import all files with import Type is “CSV”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III. Verify can upload and import all files with import Type is “CSV”</font>");
            // 3.1 Go to “ASSETS” > Houses > [a specific house] > “Import” > “Upload” button => The “Upload House Material Files” popup is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.1 Go to “ASSETS” > Houses > [a specific house] > “Import” > “Upload” button => The “Upload House Material Files” popup is displayed</font>");
            // 3.2 Select the Import Type as “CSV” > click on the “Select File” => open the select files popup
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.2 Select the Import Type as “CSV” > click on the “Select File” => open the select files popup</font>");
            // 3.3 Select more than 10 files with below template > “Open” button:
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.3 Select more than 10 files with below template > “Open” button</font>");
            // 3.4 Check the imported files data, all data are uploaded successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 Check the imported files data, all data are uploaded successfully</font>");
            // a.FileName
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 a.FileName</font>");
            // b. Options
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 b. Options</font>");
            // c. Source
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 c. Source</font>");
            // d.Community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 d.Community</font>");
            // i. If the import community is not linked to the House yet, show the warning popup
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 i. If the import community is not linked to the House yet, show the warning popup</font>");
            // ii.If the import community is not linked to the House yet OR the community is not existed in the app => show the warning popup and “Default House Quantities” is set in the the Community column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 ii.If the import community is not linked to the House yet OR the community is not existed in the app => show the warning popup and “Default House Quantities” is set in the the Community column</font>");
            // e.Using paging to check the data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3.4 e.Using paging to check the data</font>");

            CommonHelper.RefreshPage();
            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.NoFilter, string.Empty);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
            HouseImportDetailPage.Instance.OpenUploadFile();
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType3, string.Empty, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_CSV_ImportFile_PIPE_44420_III.csv" );
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType3, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_CSV_ImportFile_PIPE_44420_III_1.csv");
            // 5.Click on the “Upload” button =>
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 5.Click on the “Upload” button =></font>");
            // a.The Processing Upload popup is shown with total all the upload files
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step a.The Processing Upload popup is shown with total all the upload files</font>");
            // b. The successful Upload toast message is shown
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step b. The successful Upload toast message is shown</font>");

            HouseImportDetailPage.Instance.UploadAllFileImport();
            // 6. Close the Processing Upload popup and observe the “House Material Files” list => all files is shown successfully
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 6. Close the Processing Upload popup and observe the “House Material Files” list => all files is shown successfully</font>");
                HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE1_NAME_DEFAULT);
                if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE1_NAME_DEFAULT + ".csv") is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE2_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected File Name: {FILE1_NAME_DEFAULT}" +
                    $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                    $"<br>The expected Type: {TYPE2_NAME_DEFAULT}" +
                    $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                    $"<br>The expected Community: Default House Quantities</br></font>");
            }

            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE2_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE2_NAME_DEFAULT + ".csv") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE2_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", COMMUNITY_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE2_NAME_DEFAULT}" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE2_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                $"<br>The expected Community: {COMMUNITY_NAME_DEFAULT}</br></font>");
            }


            //IV. Verify can upload all files with various file types {“Pre-Import Modification”, “Delta (As-Built)”, “CSV”}
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step IV. Verify can upload all files with various file types {“Pre-Import Modification”, “Delta (As-Built)”, “CSV”}</font>");
            // Test data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Test data</font>");
            // The import file having
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>The import file having</font>");
            // community is linked to the house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Community is linked to the house</font>");
            // The community is not linked to the house AND existing in the app
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>The community is not linked to the house AND existing in the app</font>");
            // The community is not existed in the app
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>The community is not existed in the app</font>");
            //1. Go to “ASSETS” > Houses > [a specific house] > “Import” > “Upload” button => The “Upload House Material Files” popup is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1. Go to “ASSETS” > Houses > [a specific house] > “Import” > “Upload” button => The “Upload House Material Files” popup is displayed</font>");
            //2. Select the Import Type as “Pre-Import Modification” > click on the “Select File” => open the select files popup > Select multiple files (the file has content as test data) > “Open” button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>2. Select the Import Type as “Pre-Import Modification” > click on the “Select File” => open the select files popup > Select multiple files (the file has content as test data) > “Open” button</font>");
            //a. If the community is linked to the house, the community is shown in the Community column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>a. If the community is linked to the house, the community is shown in the Community column</font>");
            CommonHelper.RefreshPage();
            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.NoFilter, string.Empty);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
            HouseImportDetailPage.Instance.OpenUploadFile();
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType1, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_44420_IV.xml", "ImportHouseQuantities_SpecificCommunity_PIPE_44420_IV_1.xml", "ImportHouseQuantities_SpecificCommunity_PIPE_44420_IV_2.xml");

            //b. If the community is not linked to the house AND existing in the app => the Community column shows “Default House Quantities”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>b. If the community is not linked to the house AND existing in the app => the Community column shows “Default House Quantities”</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType1, string.Empty, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_44420_IV.xml", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_IV_1.xml", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_IV_2.xml");

            //c. If the community is not existed in the app => the Community column shows “Default House Quantities”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>c. If the community is not existed in the app => the Community column shows “Default House Quantities”</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType1, string.Empty, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_44420_IV_3.xml", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_IV_4.xml", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_IV_5.xml");
            HouseImportDetailPage.Instance.UploadAllFileImport();

            CommonHelper.RefreshPage();

            List<string> listFilenames_IV_1 = new List<string>() { FILE1_NAME_DEFAULT, FILE2_NAME_DEFAULT, FILE3_NAME_DEFAULT };
            List<string> listFilenames_IV_2 = new List<string>() { FILE4_NAME_DEFAULT, FILE5_NAME_DEFAULT, FILE5_NAME_DEFAULT };
            List<string> listFilenames_IV_3 = new List<string>() { FILE6_NAME_DEFAULT, FILE7_NAME_DEFAULT, FILE8_NAME_DEFAULT };

            foreach (string FileName in listFilenames_IV_1)
            {
                HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FileName);
                if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FileName + ".xml") is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", COMMUNITY_NAME_DEFAULT) is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FileName}'</b> is displayed correct.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected File Name: {FileName}" + ".xml" +
                    $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                    $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                    $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                    $"<br>The expected Community: {COMMUNITY_NAME_DEFAULT}</br></font>");
                }
            }

            foreach (string FileName in listFilenames_IV_2)
            {
                HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FileName);
                if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FileName + ".xml") is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FileName}'</b> is displayed correct.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected File Name: {FileName}" + ".xml" +
                    $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                    $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                    $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                    $"<br>The expected Community: Default House Quantities</br></font>");
                }
            }

            foreach (string FileName in listFilenames_IV_3)
            {
                HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FileName);
                if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FileName + ".xml") is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                    && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is true)
                {
                    ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FileName}'</b> is displayed correct.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                    "The result after generating a BOM can be incorrect." +
                    $"<br>The expected File Name: {FileName}" + ".xml" +
                    $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                    $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                    $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                    $"<br>The expected Community: Default House Quantities</br></font>");
                }
            }

            //3 .Select the Import Type as “Delta (As-Built)” > click on the “Select File” => open the select files popup > Select multiple files (the file has content as test data) > “Open” button
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>3 .Select the Import Type as “Delta (As-Built)” > click on the “Select File” => open the select files popup > Select multiple files (the file has content as test data) > “Open” button</font>");
            //a .If the community is linked to the house, the community is shown in the Community column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>a .If the community is linked to the house, the community is shown in the Community column</font>");
            CommonHelper.RefreshPage();
            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.NoFilter, string.Empty);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
            HouseImportDetailPage.Instance.OpenUploadFile();
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType2, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, SOURCE3_NAME_DEFAULT, "ImportHouseQuantities_As_built_ImportFile_PIPE_44420_IV.xml");
            //b. If the community is not linked to the house AND existing in the app => the Community column shows “Default House Quantities”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>b. If the community is not linked to the house AND existing in the app => the Community column shows “Default House Quantities”</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType2, string.Empty, OPTION_NAME_DEFAULT, SOURCE3_NAME_DEFAULT, "ImportHouseQuantities_As_built_ImportFile_PIPE_44420_IV_1.xml");
            //c. If the community is not existed in the app => the Community column shows “Default House Quantities”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>c. If the community is not existed in the app => the Community column shows “Default House Quantities”</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType2, string.Empty, OPTION_NAME_DEFAULT, SOURCE3_NAME_DEFAULT, "ImportHouseQuantities_As_built_ImportFile_PIPE_44420_IV_2.xml");
            HouseImportDetailPage.Instance.UploadAllFileImport();

            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE1_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE1_NAME_DEFAULT + ".xml") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE3_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", COMMUNITY_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE1_NAME_DEFAULT}" + ".xml" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE3_NAME_DEFAULT}" +
                $"<br>The expected Community: {COMMUNITY_NAME_DEFAULT}</br></font>");
            }

            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE2_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE2_NAME_DEFAULT + ".xml") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE3_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE2_NAME_DEFAULT}" + ".xml" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE3_NAME_DEFAULT}" +
                $"<br>The expected Community: Default House Quantities</br></font>");
            }

            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE3_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE3_NAME_DEFAULT + ".xml") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE3_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE3_NAME_DEFAULT}" + ".xml" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE3_NAME_DEFAULT}" +
                $"<br>The expected Community: Default House Quantities</br></font>");
            }

            CommonHelper.RefreshPage();
            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.NoFilter, string.Empty);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
            HouseImportDetailPage.Instance.OpenUploadFile();
            //4. Select import type = “CSV” then import csv files (the same as step3)
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>4. Select import type = “CSV” then import csv files (the same as step3)</font>");
            //a .If the community is linked to the house, the community is shown in the Community column
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>a .If the community is linked to the house, the community is shown in the Community column</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType3, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_CSV_ImportFile_PIPE_44420_IV.csv");
            //b. If the community is not linked to the house AND existing in the app => the Community column shows “Default House Quantities”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>b. If the community is not linked to the house AND existing in the app => the Community column shows “Default House Quantities”</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType3, string.Empty, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_CSV_ImportFile_PIPE_44420_IV_1.csv");
            //c. If the community is not existed in the app => the Community column shows “Default House Quantities”
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>c. If the community is not existed in the app => the Community column shows “Default House Quantities”</font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType3, string.Empty, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_CSV_ImportFile_PIPE_44420_IV_2.csv");
            //5. Click on the “Upload” button => The successful Upload toast message is shown. Check the list in the “House Material Files” => the data is shown properly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>5. Click on the “Upload” button => The successful Upload toast message is shown. Check the list in the “House Material Files” => the data is shown properly</font>");
            HouseImportDetailPage.Instance.UploadAllFileImport();
            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE1_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE1_NAME_DEFAULT + ".csv") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE2_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", COMMUNITY_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE1_NAME_DEFAULT}" + ".csv" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE2_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                $"<br>The expected Community: {COMMUNITY_NAME_DEFAULT}</br></font>");
            }

            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE2_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE2_NAME_DEFAULT + ".csv") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE2_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE2_NAME_DEFAULT}" + ".csv" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE2_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                $"<br>The expected Community: Default House Quantities</br></font>");
            }

            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE3_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE3_NAME_DEFAULT + ".csv") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE2_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", "Default House Quantities") is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE3_NAME_DEFAULT}" + ".csv" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE2_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                $"<br>The expected Community: Default House Quantities</br></font>");
            }

            //V. Verify can upload and import all files after editing/deleting some rows 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>V. Verify can upload and import all files after editing/deleting some rows </font>");
            //1. Go to “ASSETS” > Houses > [a specific house] > “Import” > “Upload” button => The “Upload House Material Files” popup is displayed
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>1. Go to “ASSETS” > Houses > [a specific house] > “Import” > “Upload” button => The “Upload House Material Files” popup is displayed</font>");
            CommonHelper.RefreshPage();
            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.NoFilter, string.Empty);
            HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
            HouseImportDetailPage.Instance.OpenUploadFile();
            
            //2. Select the Import Type as “Pre-Import Modification” (or Delta (As-Built) or CSV) > click on the “Select File” => open the select files popup > Select multiple files > “Open” button 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>2. Select the Import Type as “Pre-Import Modification” (or Delta (As-Built) or CSV) > click on the “Select File” => open the select files popup > Select multiple files > “Open” button </font>");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType1, string.Empty, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_44420_V.xml", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_V_1.xml", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_V_2.xml");
            HouseImportDetailPage.Instance.ImportHouseQuantitiesWithFiles(ImportType1, COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, OPTION_NAME_DEFAULT, SOURCE1_NAME_DEFAULT, "ImportHouseQuantities_SpecificCommunity_PIPE_44420_V.xml", "ImportHouseQuantities_SpecificCommunity_PIPE_44420_V_1.xml", "ImportHouseQuantities_SpecificCommunity_PIPE_44420_V_2.xml");


            //3 .Try to delete some rows 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>3 .Try to delete some rows </font>");
            HouseImportDetailPage.Instance.FilterItemInGridHouseUpload("NoFilter", string.Empty);
            HouseImportDetailPage.Instance.DeleteItemInGridHouseUpload("FileName", "ImportHouseQuantities_DefaultCommunity_PIPE_44420_V_2.xml");
            HouseImportDetailPage.Instance.DeleteItemInGridHouseUpload("FileName", "ImportHouseQuantities_SpecificCommunity_PIPE_44420_V_2.xml");
            
            //4 .Change the community 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>4 .Change the community </font>");
            HouseImportDetailPage.Instance.ChangeSpecificCommunityInHouseUpload(COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_44420_V.xml");
            HouseImportDetailPage.Instance.ChangeSpecificCommunityInHouseUpload(COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT, "ImportHouseQuantities_DefaultCommunity_PIPE_44420_V_1.xml");
            //5. Click on the “Select File(s) to Skip/Ignore Modification” checkbox randomly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>5. Click on the “Select File(s) to Skip/Ignore Modification” checkbox randomly</font>");
            HouseImportDetailPage.Instance.CheckSpecificFileInHouseUpload("ImportHouseQuantities_DefaultCommunity_PIPE_44420_V.xml", true);
            HouseImportDetailPage.Instance.CheckSpecificFileInHouseUpload("ImportHouseQuantities_DefaultCommunity_PIPE_44420_V_1.xml", true);

            //6. Click on the “Upload” button => all the files are inserted to the “House Material Files” list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>6. Click on the “Upload” button => all the files are inserted to the “House Material Files” list</font>");
            HouseImportDetailPage.Instance.UploadAllFileImport();

            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE1_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE1_NAME_DEFAULT + ".xml") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", COMMUNITY_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE1_NAME_DEFAULT}" + ".xml" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                $"<br>The expected Community: {COMMUNITY_NAME_DEFAULT} </br></font>");
            }

            HouseImportDetailPage.Instance.FilterItemInGridHouseMaterialFiles("File Name", GridFilterOperator.Contains, FILE2_NAME_DEFAULT);
            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", OPTION_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("File Name", FILE2_NAME_DEFAULT + ".xml") is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Type", TYPE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Source", SOURCE1_NAME_DEFAULT) is true
                && HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Community", COMMUNITY_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The data for House Material Files <b>'{FILE1_NAME_DEFAULT}'</b> is displayed correct.</font>");
            }
            else
            {
                ExtentReportsHelper.LogWarning("<font color='yellow'>The data for House Material Files on this page is NOT same as expectation. " +
                "The result after generating a BOM can be incorrect." +
                $"<br>The expected File Name: {FILE2_NAME_DEFAULT}" + ".xml" +
                $"<br>The expected Option: {OPTION_NAME_DEFAULT}" +
                $"<br>The expected Type: {TYPE1_NAME_DEFAULT}" +
                $"<br>The expected Source: {SOURCE1_NAME_DEFAULT}" +
                $"<br>The expected Community: {COMMUNITY_NAME_DEFAULT}</br></font>");
            }
        }
    }
}
