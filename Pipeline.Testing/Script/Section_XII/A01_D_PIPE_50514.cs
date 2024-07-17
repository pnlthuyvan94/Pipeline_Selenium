using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseComparisonGroups;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.House.Options;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_XII
{
    class A01_D_PIPE_50514 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_XII);
        }
        private const string ImportType = "Pre-Import Modification";
        private static string COMMUNITY_NAME_DEFAULT = "QA_RT_Community01_Automation";

        private readonly int[] indexs = new int[] { };
        HouseData housedata;
        private static string OPTION1_NAME_DEFAULT = "QA_RT_Option01_Automation";
        private static string OPTION1_CODE_DEFAULT = "0100";

        private static string OPTION2_NAME_DEFAULT = "QA_RT_Option02_Automation";
        private static string OPTION2_CODE_DEFAULT = "0200";
        private static string OPTION3_NAME_DEFAULT = "QA_RT_Option03_Automation";
        private static string OPTION3_CODE_DEFAULT = "0300";
        private static string OPTION4_NAME_DEFAULT = "QA_RT_Option04_Automation";
        private static string OPTION4_CODE_DEFAULT = "0400";
        private static string OPTION5_NAME_DEFAULT = "QA_RT_Option05_Automation";
        private static string OPTION5_CODE_DEFAULT = "0500";
        private static string OPTION6_NAME_DEFAULT = "QA_RT_Option06_Automation";
        private static string OPTION6_CODE_DEFAULT = "0600";
        private static string OPTION7_NAME_DEFAULT = "QA_RT_Option07_Automation";
        private static string OPTION7_CODE_DEFAULT = "0700";
        private static string OPTION8_NAME_DEFAULT = "QA_RT_Option08_Automation";
        private static string OPTION8_CODE_DEFAULT = "0800";
        private static string OPTION9_NAME_DEFAULT = "QA_RT_Option09_Automation";
        private static string OPTION9_CODE_DEFAULT = "0900";
        private static string OPTION10_NAME_DEFAULT = "QA_RT_Option10_Automation";
        private static string OPTION10_CODE_DEFAULT = "1000";

        List<string> ListOptions = new List<string>() { OPTION1_NAME_DEFAULT, OPTION2_NAME_DEFAULT, OPTION3_NAME_DEFAULT, OPTION4_NAME_DEFAULT,
        OPTION5_NAME_DEFAULT,OPTION6_NAME_DEFAULT,OPTION7_NAME_DEFAULT,OPTION8_NAME_DEFAULT,OPTION9_NAME_DEFAULT,OPTION10_NAME_DEFAULT};
        string HouseImport_url;

        [SetUp]
        public void GetData()
        {
            housedata = new HouseData()
            {
                HouseName = "QA_RT_House_50514_Automation",
                SaleHouseName = "QA_RT_House_50514_Automation",
                Series = "QA_RT_Serie3_Automation",
                PlanNumber = "0514",
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
        }


        [Test]
        [Category("Section_XII")]
        public void Epic_Workflow_Need_To_Add_A_View_Comparison_Setup_In_House_Due_To_New_Workflow_With_The_Pre_Compared_Files_From_Connect()
        {
            //I. Verify navigation
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I. Verify navigation.</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

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

            //Add Option Into House
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION1_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION1_NAME_DEFAULT + " - " + OPTION1_CODE_DEFAULT);
            }

            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION2_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION2_NAME_DEFAULT + " - " + OPTION2_CODE_DEFAULT);
            }

            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION3_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION3_NAME_DEFAULT + " - " + OPTION3_CODE_DEFAULT);
            }
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION4_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION4_NAME_DEFAULT + " - " + OPTION4_CODE_DEFAULT);
            }
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION5_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION5_NAME_DEFAULT + " - " + OPTION5_CODE_DEFAULT);
            }
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION6_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION6_NAME_DEFAULT + " - " + OPTION6_CODE_DEFAULT);
            }
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION7_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION7_NAME_DEFAULT + " - " + OPTION7_CODE_DEFAULT);
            }
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION8_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION8_NAME_DEFAULT + " - " + OPTION8_CODE_DEFAULT);
            }
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION9_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION9_NAME_DEFAULT + " - " + OPTION9_CODE_DEFAULT);
            }
            if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", OPTION10_NAME_DEFAULT) is false)
            {
                HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(OPTION10_NAME_DEFAULT + " - " + OPTION10_CODE_DEFAULT);
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Navigate to House Communities page.</font>");
            HouseDetailPage.Instance.LeftMenuNavigation("Communities");

            //Verify the Communities in grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify the Communities in grid.</font>");
            if (HouseCommunities.Instance.IsValueOnGrid("Name", COMMUNITY_NAME_DEFAULT) is false)
            {
                HouseCommunities.Instance.AddButtonCommunities();
                HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(COMMUNITY_NAME_DEFAULT, indexs);
            }
            //Click on menu Comparison Groups
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.1 Click on menu Comparison Groups.</font>");
            //Click Comparison Groups in the menu side.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.1 Click Comparison Groups in the menu side.</font>");
            //Navigate to the Comparison Groups page successfully
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.1 Navigate to the Comparison Groups page successfully.</font>");
            HouseCommunities.Instance.LeftMenuNavigation("Comparison Groups");
            string ComparisonGroups_url = HouseComparisonGroups.Instance.CurrentURL;
            HouseComparisonGroups.Instance.VerifyComparisonGroupsIsDisplayedWithNoImport();

            //2. Click on View Comparison button
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2. Click on View Comparison button.</font>");
            //Go to House > Quantities Import
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2.1 Go to House > Quantities Import.</font>");
            //Click on Dropdown Comparison Setup
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2.2 Click on Dropdown Comparison Setup</font>");
            //Click on View Comparison 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2.3 Click on View Comparison </font>");
            //Open the new tab.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2.4 Open the new tab.</font>");
            HouseComparisonGroups.Instance.LeftMenuNavigation("Import");
            HouseImport_url = HouseComparisonGroups.Instance.CurrentURL;
            HouseImportDetailPage.Instance.ViewComparison();

            //Message “No Import Comparison Groups to display.” displayed when there is no comparison group.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2.5 Message “No Import Comparison Groups to display.” displayed when there is no comparison group.</font>");
            //User cannot edit anything.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2.6 User cannot edit anything.</font>");
            HouseComparisonGroups.Instance.VerifyComparisonGroupsIsDisplayedWithNoImport();

            //II. Verify Comparison Groups page update to the latest information
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II. Verify Comparison Groups page update to the latest information</font>");
            //1. User add un-modified Option
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1. User add un-modified Option</font>");
            //In House Quantities import page, click Upload
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1.2 In House Quantities import page, click Upload</font>");

            CommonHelper.SwitchTab(0);
            string Opions = OPTION1_NAME_DEFAULT + ", " + OPTION2_NAME_DEFAULT + ", " + OPTION3_NAME_DEFAULT
                + ", " + OPTION4_NAME_DEFAULT + ", " + OPTION5_NAME_DEFAULT + ", " + OPTION6_NAME_DEFAULT + ", " +
                OPTION7_NAME_DEFAULT + ", " + OPTION8_NAME_DEFAULT + ", " + OPTION9_NAME_DEFAULT + ", " + OPTION10_NAME_DEFAULT;

            if (HouseImportDetailPage.Instance.IsItemInGridHouseMaterialFiles("Option(s)", Opions) is true)

            {
                HouseImportDetailPage.Instance.DeleteFileImportQuantities("Option(s)", Opions);
            }

            //Import House Quantities
            HouseImportDetailPage.Instance.ImportHouseQuantities(ImportType, string.Empty, Opions, "ImportHouseQuantities_DefaultCommunity_PIPE_50514.xml");

            //Upload the file above, Uncheck Skip/Ignore Modifications and click Upload
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1.3. Upload the file above, Uncheck Skip/Ignore Modifications and click Upload</font>");
            //Choose uploaded file and Click Start Comparison
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1.4. Choose uploaded file and Click Start Comparison</font>");
            //There is no comparison group.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1.5. There is no comparison group.</font>");
            //Refresh Comparison Groups page, there is also no comparison groups.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1.6 Refresh Comparison Groups page, there is also no comparison groups.</font>");
            //In Import process, tick the checkbox to choose all Un-Modified Options and click Add
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1.7 In Import process, tick the checkbox to choose all Un-Modified Options and click Add.</font>");
            //Un-modified Options are added to Comparison Groups successfully.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1.8 Un-modified Options are added to Comparison Groups successfully.</font>");
            //Refresh Comparison Groups page, the grid is updated to the latest.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1.9 Refresh Comparison Groups page, the grid is updated to the latest.</font>");

            HouseImportDetailPage.Instance.StartComparion();
            HouseImportDetailPage.Instance.VerifyComparisonGroups(ListOptions);
            CommonHelper.SwitchTab(1);
            CommonHelper.OpenURL(ComparisonGroups_url);
            HouseComparisonGroups.Instance.VerifyComparisonGroupsIsDisplayedWithOptions(OPTION1_NAME_DEFAULT, OPTION2_NAME_DEFAULT, OPTION3_NAME_DEFAULT, OPTION4_NAME_DEFAULT,
            OPTION5_NAME_DEFAULT, OPTION6_NAME_DEFAULT, OPTION7_NAME_DEFAULT, OPTION8_NAME_DEFAULT, OPTION9_NAME_DEFAULT, OPTION10_NAME_DEFAULT);

            //2. User add Comparison Groups
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.2. User add Comparison Groups</font>");
            //In the Import process, click Add Comparison Group
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.2.1 In the Import process, click Add Comparison Group</font>");
            //Insert these fields and click Insert.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.2.2 Insert these fields and click Insert.</font>");
            //New Comparison Group is added successfully
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.2.3 New Comparison Group is added successfully</font>");
            CommonHelper.SwitchTab(0);
            HouseImportDetailPage.Instance.AddComparisonGroups();
            HouseImportDetailPage.Instance.InsertItemComparsion(OPTION1_NAME_DEFAULT, "and", OPTION1_NAME_DEFAULT + " and " + OPTION2_NAME_DEFAULT, OPTION8_NAME_DEFAULT, OPTION9_NAME_DEFAULT);
            
            //Refresh Comparison Groups page, there is also no comparison groups.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.2.4 Refresh Comparison Groups page, there is also no comparison groups.</font>");
            CommonHelper.SwitchTab(1);
            CommonHelper.OpenURL(ComparisonGroups_url);
            HouseComparisonGroups.Instance.VerifyItemComparisonGroups(OPTION1_NAME_DEFAULT, string.Empty, OPTION1_NAME_DEFAULT + " and " + OPTION2_NAME_DEFAULT, OPTION8_NAME_DEFAULT + ", " + OPTION9_NAME_DEFAULT);

            //3. User edit Comparison Group
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.3. User edit Comparison Group</font>");
            //Click on the edit icon of any Comparison Group.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.3.1 Click on the edit icon of any Comparison Group.</font>");
            //Update the Condition and Included Import Configuration.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.3.2 Update the Condition and Included Import Configuration.</font>");
            //Open new tab to reach House detail page, click on menu Comparison Groups.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.3.3 Open new tab to reach House detail page, click on menu Comparison Groups.</font>");


            CommonHelper.SwitchTab(0);
            HouseImportDetailPage.Instance.UpdateItemComparsion(OPTION6_NAME_DEFAULT, "not", "not " + OPTION7_NAME_DEFAULT, OPTION3_NAME_DEFAULT, OPTION5_NAME_DEFAULT);
            
            //Comparison Groups page loaded successfully and updated to the latest.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.3.4 Comparison Groups page loaded successfully and updated to the latest.</font>");
            CommonHelper.SwitchTab(1);
            CommonHelper.OpenURL(ComparisonGroups_url);
            HouseComparisonGroups.Instance.VerifyItemComparisonGroups(OPTION6_NAME_DEFAULT, string.Empty, "not " + OPTION7_NAME_DEFAULT, OPTION3_NAME_DEFAULT + ", " + OPTION5_NAME_DEFAULT + ", " + OPTION6_NAME_DEFAULT);
            
            //4. User add child level. Need to expand all in initial view.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.4. User add child level. Need to expand all in initial view.</font>");
            //Refresh Comparison Groups page, the grid is updated to the latest. 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.4.1 Refresh Comparison Groups page, the grid is updated to the latest.</font>");
            CommonHelper.SwitchTab(0);

            HouseImportDetailPage.Instance.OpenInsertItemComparison(OPTION1_NAME_DEFAULT);
            HouseImportDetailPage.Instance.InsertItemComparsion(OPTION3_NAME_DEFAULT, "or", OPTION3_NAME_DEFAULT + " or " + OPTION2_NAME_DEFAULT, OPTION3_NAME_DEFAULT, OPTION4_NAME_DEFAULT);
            CommonHelper.SwitchTab(1);
            CommonHelper.OpenURL(ComparisonGroups_url);
            HouseComparisonGroups.Instance.ExpandComparisonGroups(OPTION1_NAME_DEFAULT);
            HouseComparisonGroups.Instance.VerifyItemComparisonGroups(OPTION3_NAME_DEFAULT, OPTION1_NAME_DEFAULT, OPTION3_NAME_DEFAULT + " or " + OPTION2_NAME_DEFAULT, OPTION3_NAME_DEFAULT + ", " + OPTION4_NAME_DEFAULT);

            //5. User copy comparison groups
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.5. User copy comparison groups</font>");
            //In the Import process, click on copy button of any comparison group.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.5.1 In the Import process, click on copy button of any comparison group.</font>");
            //Comparison Group is copied successfully.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.5.2 Comparison Group is copied successfully.</font>");
            CommonHelper.SwitchTab(0);
            HouseImportDetailPage.Instance.ExpandComparisonGroups(OPTION1_NAME_DEFAULT);
            HouseImportDetailPage.Instance.CopyItemComparsionGroups(OPTION3_NAME_DEFAULT);
            HouseImportDetailPage.Instance.VerifyItemComparisonGroups(OPTION3_NAME_DEFAULT, OPTION1_NAME_DEFAULT, OPTION3_NAME_DEFAULT + " or " + OPTION2_NAME_DEFAULT, OPTION3_NAME_DEFAULT + ", " + OPTION4_NAME_DEFAULT);

            //The Comparison Groups page is opened in the new tab and the grid is updated to the latest.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.5.3 The Comparison Groups page is opened in the new tab and the grid is updated to the latest.</font>");
            HouseImportDetailPage.Instance.ViewComparison();
            CommonHelper.SwitchTab(1);
            HouseComparisonGroups.Instance.VerifyItemComparisonGroups(OPTION3_NAME_DEFAULT, OPTION1_NAME_DEFAULT, OPTION3_NAME_DEFAULT + " or " + OPTION2_NAME_DEFAULT, OPTION3_NAME_DEFAULT + ", " + OPTION4_NAME_DEFAULT);

            //6. User delete comparison Group
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.6. User delete comparison Group</font>");
            //In the import process, delete Comparison Group.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.6. In the import process, delete Comparison Group.</font>");

            CommonHelper.SwitchTab(0);
            HouseImportDetailPage.Instance.DeleteItemComparsion(OPTION5_NAME_DEFAULT);
            //Refresh Comparison Groups page, the Comparison Group deleted is disappeared.
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.6.1 Refresh Comparison Groups page, the Comparison Group deleted is disappeared.</font>");
            HouseComparisonGroups.Instance.VerifyComparisonGroupsIsNotDisplayWithOptions(OPTION5_NAME_DEFAULT);

            //III. Verify functionality of Comparison Groups page
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III. Verify functionality of Comparison Groups page.</font>");
            //1. Paging
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.1. Paging.</font>");
            //Change page size to 10
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.2. Change page size to 10.</font>");
            HouseComparisonGroups.Instance.PageSize(10);
            //Change page index to 3
            //Navigate to the last page
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.3 Navigate to the last page.</font>");
            //Change page size to 50
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.4 Change page size to 50.</font>");
            HouseComparisonGroups.Instance.PageSize(50);
            //2. Sort
            HouseComparisonGroups.Instance.SortOptionInComparisonGroups();

        }

        [TearDown]
        public void DeleteData()
        {
            //Delete Option In Comparison Group
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>Delete Option In Comparison Group</font>");
            CommonHelper.OpenURL(HouseImport_url);
            HouseImportDetailPage.Instance.CheckALLFileImport();
            HouseImportDetailPage.Instance.OpenStartComparisonSetup();
            List<string> ListOptions = new List<string>() { OPTION1_NAME_DEFAULT, OPTION2_NAME_DEFAULT, OPTION3_NAME_DEFAULT, OPTION4_NAME_DEFAULT,
            OPTION6_NAME_DEFAULT,OPTION7_NAME_DEFAULT,OPTION8_NAME_DEFAULT,OPTION9_NAME_DEFAULT,OPTION10_NAME_DEFAULT};
            foreach (string Option in ListOptions)
            {
                HouseImportDetailPage.Instance.DeleteItemComparsion(Option);
            }

            HouseImportDetailPage.Instance.DeleteItemComparsion(OPTION1_NAME_DEFAULT);
            HouseImportDetailPage.Instance.DeleteItemComparsion(OPTION3_NAME_DEFAULT);
        }
    }
}
