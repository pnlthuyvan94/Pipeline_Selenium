using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.AvailablePlan;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.House.Quantities;
using Pipeline.Testing.Pages.Assets.House.Quantities.HouseOptionCopyQuantities;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Assets.House.Import;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Assets.House.Communities;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_R_PIPE_36132 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }
        OptionData optionData1, optionData2;
        CommunityData communityData;
        HouseData houseData1, houseData2;
        BuildingPhaseData buildingPhaseData;
        ProductData productData;
        private const string param1 = "SWG=QA";
        private const string param2 = "SWG=R";
        private const string param3 = "SWG=R_Update";
        private const string ImportType = "Pre-Import Modification";

        [Test]
        [Category("Section_IV")]
        public void A04_R_Assets_Detail_Pages_Houses_Quantities_Copy_House_Quantities_need_to_include_parameters()
        {
            //--PART 1 Create information

            optionData1 = new OptionData()
            {
                Name = "RT_QA_Automation_Option1_36132",
                Number = string.Empty,
                Description = "FOR 188"
            };
            optionData2 = new OptionData()
            {
                Name = "RT_QA_Automation_Option2_36132",
                Number = string.Empty,
                Description = "FOR 188",
                SaleDescription = "188_2"
            };
            //Create Community 
            communityData = new CommunityData()
            {
                Name = "RT_QA_Automation_Community_36132",
                Code = "ComAu2"
            };
            //Create house (2 houses)
            houseData1 = new HouseData()
            {
                HouseName = "RT_QA_Automation_House1_36132",
                Series = "RT_QA_Automation_SeriesData_36132",
                PlanNumber = "0912"
            };
            houseData2 = new HouseData()
            {
                HouseName = "RT_QA_Automation_House2_36132",
                Series = "RT_QA_Automation_SeriesData_36132",
                PlanNumber = "0913"
            };
            buildingPhaseData = new BuildingPhaseData()
            {
                Code = "1881",
                Name = "RT_QA_Automation_BuildingPhase_36132",
                BuildingGroupName = "SPA_188",
                BuildingGroupCode = "188",
                Taxable = false
            };
            //Create product
            productData = new ProductData()
            {
                Name = "RT_QA_Automation_Production_36132",
                Description = "Pro_188",
                Notes = string.Empty,
                Code = string.Empty,
                Unit = string.Empty,
                SKU = string.Empty,
                RoundingUnit = string.Empty,
                RoundingRule = string.Empty,
                Waste = string.Empty,
                Supplemental = false,
                Manufacture = "RT_QA_Manufacturer_36132",
                Style = "RT_QA_Style_36132",
                Category = string.Empty,
                BuildingPhase = buildingPhaseData.Name,
                Use = string.Empty,
                Quantities = "22",
                Parameter = string.Empty
            };

            //DELETA BEFORE START TEST SCRIPT 
            //Delete all imported files in House Quantities in house1 and house2 respectively
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete File House Quantities.</font>");
            HouseDetailPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData1.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData1.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
                HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
                HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
                CommonFuncs.SwitchToAnotherOne(houseData2.HouseName);
                HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();

                //Delete all quantities files in Quantities in house2 and house1 respectively
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Delete all quantities files in Quantities in house2 and house1 respectively.</font>");
                HouseImportDetailPage.Instance.LeftMenuNavigation("Quantities");
                HouseQuantitiesDetailPage.Instance.FilterByCommunity("Default House Quantities");
                HouseImportDetailPage.Instance.DeleteAllOptionQuantities();
                HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
                HouseImportDetailPage.Instance.DeleteAllOptionQuantities();
                CommonFuncs.SwitchToAnotherOne(houseData1.HouseName);
                HouseQuantitiesDetailPage.Instance.FilterByCommunity("Default House Quantities");
                HouseImportDetailPage.Instance.DeleteAllOptionQuantities();
                HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
                HouseImportDetailPage.Instance.DeleteAllOptionQuantities();
            }

            //I. SETUP DATA
            //Step 1: Verify that in the specific community that house1 and house2 added
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b> **** I. SETUP DATA **** </b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.1: Verify that in the {communityData.Name} has two houses are as {houseData1.HouseName} and {houseData2.HouseName}</font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
                CommunityDetailPage.Instance.LeftMenuNavigation("Available Plans");
                if (AvailablePlanPage.Instance.IsItemInGrid("Name", houseData1.HouseName) &&
                    AvailablePlanPage.Instance.IsItemInGrid("Name", houseData2.HouseName))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'>There are {houseData1.HouseName} and {houseData2.HouseName} in the community {communityData.Name}.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>There are not {houseData1.HouseName} and/or {houseData2.HouseName} in the community {communityData.Name}.</font>");
                }
            }
            //Step 2: Go to house1 to check there are 2 options inside it
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.2: Verify that in House1 as {houseData1.HouseName} there are two options inside as {optionData1.Name} and {optionData2.Name}</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData1.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData1.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Options");
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", optionData1.Name) &&
                    HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", optionData2.Name))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'>There are {optionData1.Name} and {optionData2.Name} in the {houseData1.HouseName}.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>There are not {optionData1.Name} and/or {optionData2.Name} in the {houseData1.HouseName}.</font>");
                }
            }
            //Step 3: Verify that option1 is normal, option2 is global
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.3: Verify that option1 {optionData1.Name} is normal, option2 {optionData2.Name} is global</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData1.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData1.Name) is true)
            {
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", optionData1.Name);
                //Verify that it is a normal option in the first option
                if (!OptionDetailPage.Instance.IsOptionTypeChecked("Elevation"))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'>The option '{optionData1.Name}' is the normal option.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The option '{optionData1.Name}'is not the normal option.</font>");
                }
                //Switch to second option to verify that this option is Global 
                CommonFuncs.SwitchToAnotherOne(optionData2.Name);
                if (!OptionDetailPage.Instance.IsOptionTypeChecked("Global"))
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The option '{optionData2.Name}' is not the Global option.</font>");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"<font color='green'>The option '{optionData2.Name}' is the Global option.</font>");
                }
            }
            //Step 3': Import 2 House Quantities to house1 for default and specific comms respectively
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>12. Add house quantities that have given parameters</font>");
            HouseCommunities.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData1.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData1.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Import");
                HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, string.Empty, string.Empty, "HouseQuantities_DefaultCom_PIPE_36132.xml");
                HouseImportDetailPage.Instance.DeleteAllHouseMaterialFiles();
                HouseImportDetailPage.Instance.LeftMenuNavigation("Import");
                HouseImportDetailPage.Instance.UploadFileAndImportHouseQuantities(ImportType, communityData.Code + "-" + communityData.Name, optionData1.Name, "HouseQuantities_SpecificCom_PIPE_36132.xml");

            }
            //Step 4: Go to house2, default comm, tab quan. Verify this house2 is total void, no option
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.4: Verify that in {houseData2.HouseName} before copying, it has no option at all</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData2.HouseName);

            if (HousePage.Instance.IsItemInGrid("Name", houseData2.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData2.HouseName);
                HousePage.Instance.LeftMenuNavigation("Quantities");
                HouseQuantitiesDetailPage.Instance.FilterByCommunity("Default House Quantities");
                if (HouseQuantitiesDetailPage.Instance.VerifyHouseQuantitiesIsNotDisplay())
                {
                    ExtentReportsHelper.LogPass($"<font color='green'>The option(s) in the {houseData2.HouseName} is blank</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The option(s) in the {houseData2.HouseName} is not blank</font>");
                }
            }
            //Step 5: Switch to house1, default comm, verify the option1 has SWG=QA and quantity = 11
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.5: Verify that in Default House Community, the House1 {houseData1.HouseName} has parameters as SWG = QA and Quantity = 11 </font>");
            CommonFuncs.SwitchToAnotherOne(houseData1.HouseName);
            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", param1) is true &&
                HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", "11") is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'>The quantities at {optionData1.Name} and in the {houseData1.HouseName} has parameter as {param1} and quantity = 11.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The quantities at {optionData2.Name} and in the {houseData1.HouseName} has not parameter as {param1} and not quantity = 11.</font>");
            }
            //Step 6. Still at this House1, switch to specific comm to see that parameters is SWG=R, quantity = 22
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>I.6: Verify that in Specific Community {communityData.Name}, at House1 {houseData1.HouseName} it has parameters as SWG=R and Quantity = 22.</font>");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", param2) is true &&
                HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", "22") is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> In quantity page, parameters is {param2} and quantity is 22 at {houseData1.HouseName}.</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>In quantity page, parameters is not {param2} and quantity is not 22 at {houseData1.HouseName}.</font>");
            }
            //COPY FROM HOUSE TO HOUSE
            //Step 7: At this house1, back to default comm (SWG=QA, 11), click at yellow copy quantities to houses/options button, set check at house2 and option 1, the middle comm is default by default
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>**** II. COPY PRODUCT QUANTITIES FROM HOUSE TO HOUSE ****</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><----DEFAULT HOUSE COMMUNITY----</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.1: Verify that in House1 {houseData1.HouseName} and Default House Community, click copy yellow button Copy Quantity To Other House Or Option, a list of houses and options to appear </font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData1.HouseName);
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity("Default House Quantities");
            HouseQuantitiesDetailPage.Instance.ClickCopyQuantitiesToHouseOrOption();
            HouseOptionCopyQuantities.Instance.SelectOptionInListOption(optionData1.Name + "-" + optionData1.Description);
            HouseOptionCopyQuantities.Instance.SelectHouseInListHouse(houseData2.HouseName);
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.2: Set checked at House2 {houseData2.HouseName} and {optionData1.Name} before copying </font>");
            //Verify the list of houses and options appear
            if (HouseOptionCopyQuantities.Instance.IsListHouseAndOptionExist())
            {
                ExtentReportsHelper.LogPass("<font color ='green'>After clicking button <b>Copy Quantities to other Houses/Options </b> , two tables of houses and options shown</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color ='red'>After clicking button <b>Copy Quantities to other Houses/Options</b>, two tables of houses and options are not shown</font>");
            }
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.3: Click on button CopyQuantitiesToSelectedHouse </font>");
            //Then click white buton "Copy Qtys to Selected Houses"
            HouseOptionCopyQuantities.Instance.ClickCopyQuantitiesHouseToHouse();
            //A browser modal with OK and Cancel buttons appear, click Cancel to see thing unchanged           
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.4: Click Cancel button on Alert then the Alert is gone </font>");
            HouseOptionCopyQuantities.Instance.ClickAlert(ConfirmType.Cancel);
            if (HouseOptionCopyQuantities.Instance.IsListHouseAndOptionExist())
            {
                ExtentReportsHelper.LogPass("<font color='green'>The <b>Cancel</b> button on the Alert works well</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The <b>Cancel</b> button on the Alert does not work well</font>");
            }
            //Step 8: Now click on white button copy option from house1 to house2 and OK on modal 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.5. Now click OK button on Alert again then a modal appears </font>");
            HouseOptionCopyQuantities.Instance.ClickCopyQuantitiesHouseToHouse();
            HouseOptionCopyQuantities.Instance.ClickAlert(ConfirmType.OK);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            //A new modal appears, click No or X button, it unchanges
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.6. Verify that click Cancel or X button(s) on modal, it keeps unchanges </font>");
            HouseOptionCopyQuantities.Instance.ClickModal(ConfirmType.No);
            if (HouseOptionCopyQuantities.Instance.IsListHouseAndOptionExist())
            {
                ExtentReportsHelper.LogPass("<font color='green'>The <b>No</b> button on the modal works well</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The <b>No</b> button on the modal does not work well</font>");
            }
            HouseOptionCopyQuantities.Instance.ClickCopyQuantitiesHouseToHouse();
            HouseOptionCopyQuantities.Instance.ClickAlert(ConfirmType.OK);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            HouseOptionCopyQuantities.Instance.ClickModal(ConfirmType.X);
            if (HouseOptionCopyQuantities.Instance.IsListHouseAndOptionExist())
            {
                ExtentReportsHelper.LogPass("<font color='green'>The <b>X</b> button on the modal works well</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The <b>X</b> button on the modal does not work well</font>");
            }
            //Click Yes button to proceed, then back to house2 to verify that the quantities are copied to it
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.7: Now click Yes on the modal a toast message appears and verify the copy is a success </font>");
            HouseOptionCopyQuantities.Instance.ClickCopyQuantitiesHouseToHouse();
            HouseOptionCopyQuantities.Instance.ClickAlert(ConfirmType.OK);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            bool checkToastMsgHH = HouseQuantitiesDetailPage.Instance.GetLastestToastMessage().Contains("Quantities successfully added to");
            HouseOptionCopyQuantities.Instance.ClickModal(ConfirmType.Yes);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            if (checkToastMsgHH is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantities successfully added to House2 as {houseData2.HouseName} </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Quantities is not successfully added to House2 as {houseData2.HouseName} </font>" +
                    $"<br>The actual result: {HouseQuantitiesDetailPage.Instance.GetLastestToastMessage()}</br></font>");
            }
            //Step 9: Switch to house2, default comm, tab quantity and verify that it is SWG=QA, quantity=11                        
            CommonFuncs.SwitchToAnotherOne(houseData2.HouseName);
            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", param1) &&
                HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", "11"))
            {
                ExtentReportsHelper.LogPass($"<font color='green'>Quantities in House2 {houseData2.HouseName} is now {param1} and Quantity = 11</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Quantities in House2 {houseData2.HouseName} is now not {param1} and Quantity = 11</font>");
            }
            //Step 10: Still at house2 in, switch to specific community, verify that it is blank
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>----SPECIFIC COMMUNITY----</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.8: Verify House quantities in House2 {houseData2.HouseName} is blank</font>");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            if (HouseQuantitiesDetailPage.Instance.VerifyHouseQuantitiesIsNotDisplay())
            {
                ExtentReportsHelper.LogPass($"<font color='green'> At {houseData2.HouseName} and at community {communityData.Name}, there is no option at all</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> At {houseData2.HouseName} and community {communityData.Name}, there is option</font>");
            }
            //Step 11, Back to house1, specific comm, (it is SWG=R, and quantity = 22) click yellow button
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.9: With specific community choice, click button Copy Qty To Other House Or Option</font>");
            CommonFuncs.SwitchToAnotherOne(houseData1.HouseName);
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            HouseQuantitiesDetailPage.Instance.ClickCopyQuantitiesToHouseOrOption();
            //Verify two tables of houses and options appear
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.10: Verify two tables of Houses and Options to appear</font>");
            if (HouseOptionCopyQuantities.Instance.IsListHouseAndOptionExist())
            {
                ExtentReportsHelper.LogPass("<font color='green'>After clicking button <b>Copy Qty To Other House Or Option</b>, it jumps to two tables of houses and options to appear</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>After clicking button to <b>Copy Qty To Other House Or Option</b>, it does not jump to two tables of houses and options to appear</font>");
            }
            //Select middle specific comm, set check at house2 and option1, then click white button to copy from house to house
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.11: After selecting the community in the middle of the screen, there are now House2 and Option1 appear, set checked on them </font>");
            HouseOptionCopyQuantities.Instance.SelectMiddleCommunity(communityData.Code + "-" + communityData.Name);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconSelectMiddleComm();
            HouseOptionCopyQuantities.Instance.SelectHouseInListHouse(houseData2.HouseName);
            HouseOptionCopyQuantities.Instance.SelectOptionInListOption(optionData1.Name + "-" + optionData1.Description);
            HouseOptionCopyQuantities.Instance.ClickCopyQuantitiesHouseToHouse();
            HouseOptionCopyQuantities.Instance.ClickAlert(ConfirmType.OK);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            bool checkToastMsgHH1 = HouseQuantitiesDetailPage.Instance.GetLastestToastMessage().Contains("Quantities successfully copied from option");
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.12: A toast message to appear as Quantities successfully added to {houseData2.HouseName}</font>");
            if (HouseQuantitiesDetailPage.Instance.GetLastestToastMessage().Contains("Quantities successfully added to") is true || checkToastMsgHH1 is true)
            {
                ExtentReportsHelper.LogInformation($"<font color='lavender'> Toast message appears to tell added to {houseData2.HouseName} a success</font>");
            }
            else
            {
                ExtentReportsHelper.LogInformation($"<font color='lavender'>Toast message does not appears to tell added to {houseData2.HouseName} a success</font>" +
                    $"<br>The actual result: {HouseQuantitiesDetailPage.Instance.GetLastestToastMessage()}</br></font>");
            }
            //Click yes on modal
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.13: Click Copy Quantities to Selected House and OK button on Alert</font>");
            HouseOptionCopyQuantities.Instance.ClickModal(ConfirmType.Yes);
            //Step 12: Back to house2, specific comm, tab quan to see that it is SWG=R, quan=22
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.14: Verify in House2 {houseData2.HouseName} now has parameters as SWG=R, quantity = 22</font>");
            CommonFuncs.SwitchToAnotherOne(houseData2.HouseName);
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", param2) is true &&
                HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Quantity", "22") is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantities successfully added to {houseData2.HouseName} as {param2}, and quantity = 22</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Quantities are not successfully added to {houseData2.HouseName} as {param2}, and quantity = 22</font>");
            }
            //Step 13: Update param for this house2, specific comm update at option1 as SWG=R to become SWG=R_Update, verify its update a success
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.15. Edit parameter in this House2 succeeded a toast message appears </font>");
            HouseQuantitiesDetailPage.Instance.ClickEditItemInQuantitiesGrid(buildingPhaseData.Code + "-" + buildingPhaseData.Name, productData.Name);
            HouseQuantitiesDetailPage.Instance.AddParameterInQuantitiesGrid(param3);
            if (HouseQuantitiesDetailPage.Instance.GetLastestToastMessage().Contains("The House Quantities were updated successfully") is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantities in {houseData2.HouseName} were updated a success </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Quantities in {houseData2.HouseName} were not updated a success </font>" +
                    $"<br>The actual result: {HouseQuantitiesDetailPage.Instance.GetLastestToastMessage()}</br></font>");
            }
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.16. Verify House2 to update as SWG=R_Update </font>");
            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", "SWG=R_Update") is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Parameters in {houseData2.HouseName} is now updated as {param3}font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Parameters in {houseData2.HouseName} is now not updated as {param3}</font>");
            }
            //Step 14: Now back to house1, middle specific comm, set check at house2 and option1 (SWG=R, 22) and click white button, OK, and Yes
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.17. Back to {houseData1.HouseName}, at {communityData.Name} set check at {houseData2.HouseName} and {optionData1.Name} and click at Copy again </font>");
            CommonFuncs.SwitchToAnotherOne(houseData1.HouseName);
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            HouseQuantitiesDetailPage.Instance.ClickCopyQuantitiesToHouseOrOption();
            HouseOptionCopyQuantities.Instance.SelectMiddleCommunity(communityData.Code + "-" + communityData.Name);
            HouseOptionCopyQuantities.Instance.SelectHouseInListHouse(houseData2.HouseName);
            HouseOptionCopyQuantities.Instance.SelectOptionInListOption(optionData1.Name + "-" + optionData1.Description);
            HouseOptionCopyQuantities.Instance.ClickCopyQuantitiesHouseToHouse();
            HouseOptionCopyQuantities.Instance.ClickAlert(ConfirmType.OK);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            HouseOptionCopyQuantities.Instance.ClickModal(ConfirmType.Yes);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            //Step 15: Back to house2, specific comm to verify that it is back to SWG=R
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>II.18. Check data again to see that in House2 {houseData2.HouseName}, at {communityData.Name} verify that it is now back as SWG=R</font>");
            CommonFuncs.SwitchToAnotherOne(houseData2.HouseName);
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            if (HouseQuantitiesDetailPage.Instance.IsItemInQuantitiesGrid("Parameters", param2) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Parameters in {houseData2.HouseName} at {communityData.Name} is now back as {param2}</font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> Parameters in {houseData2.HouseName} at {communityData.Name} is now not updated as {param2}</font>");
            }
            //------ Step: JUMP TO OPTION SECTION -------
            //Step 16: Go to assets, option 2, default comm,tab product, verify that at Option Quantity section, column BP is "No records to display." 
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b> **** III. COPY QUANTITIES FROM OPTION TO OPTION ****</b></font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>----DEFAULT HOUSE COMMUNITY----</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.1. Before copy, Option2 {optionData2.Name}, TAB Products, verify that in House Quantities it is now as No records to display </font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData2.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData2.Name) is true)
            {
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", optionData2.Name);
                OptionPage.Instance.LeftMenuNavigation("Products");
                if (!ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Community", "CheckingData"))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'> Option2 {optionData2.Name} has House Option Quantities is blank</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'> Option2 {optionData2.Name} has House Option Quantities is not blank</font>");
                }
            }
            //Step 17: Back to house/house1, tab quantities, has middle comm as default, in the 3rd and 4th tables it is for options
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.2. Back to {houseData1.HouseName}, click copy button and verify that there are two tables of options </font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.EnterHouseNameToFilter("Name", houseData1.HouseName);
            if (HousePage.Instance.IsItemInGrid("Name", houseData1.HouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
            }

            HousePage.Instance.LeftMenuNavigation("Quantities");
            //Click yellow button to copy
            HouseQuantitiesDetailPage.Instance.ClickCopyQuantitiesToHouseOrOption();
            //Verify it has two table of options
            if (HouseOptionCopyQuantities.Instance.IsCopyQtyToOptExist())
            {
                ExtentReportsHelper.LogPass($"<font color='green'> There are two lists of Options on the left and right</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass($"<font color='red'> There are not two lists of Options on the left and right<</font>");
            }
            //Step 18 Check at option1 on the left and option2 on the right next to it
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.3. Set checks at {optionData1.Name} on the left and {optionData2.Name} on the right and click copy Selected Option</font>");
            HouseOptionCopyQuantities.Instance.SelectLeftOptioninOptList(optionData1.Name + "-" + optionData1.Description);
            HouseOptionCopyQuantities.Instance.SelectOptionOptionToCpyQty(optionData2.Name + "-" + optionData2.Description);
            //Click white button for option
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.4. Verify a toast message as Quantities successfully copied from {optionData1.Name} to: {optionData2.Name} </font>");
            HouseOptionCopyQuantities.Instance.ClickCopyQuantitiesOptionToOption();
            HouseOptionCopyQuantities.Instance.ClickAlert(ConfirmType.OK);
            bool checkToastMsgOP = HouseQuantitiesDetailPage.Instance.GetLastestToastMessage().Contains("Quantities successfully copied from option");
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconClickCopyOptionToOption();
            if (checkToastMsgOP is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantities successfully copied from {optionData1.Name} to : {optionData2.Name} </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Quantities are not successfully copied from {optionData1.Name} to: {optionData2.Name} </font>" +
                    $"<br>The actual result: {HouseQuantitiesDetailPage.Instance.GetLastestToastMessage()}</br></font>");
            }
            //Step 19: Back to option2, tab products to verify that it is copied successfully to option2
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.5. Check Data on Option2 {optionData2.Name}:</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData2.Name);
            OptionPage.Instance.SelectItemInGridWithTextContains("Name", optionData2.Name);
            OptionPage.Instance.LeftMenuNavigation("Products");
            if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Community", "Default House Quantities") &&
                ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Houses", houseData1.PlanNumber + "-" + houseData1.HouseName) &&
                ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Products", productData.Name) &&
                ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Parameters", "SWG=QA") &&
                ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Quantity", "11.00"))
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantities in {optionData2.Name} now is 'Default House Quantities', {houseData1.PlanNumber + "-" + houseData1.HouseName}, {productData.Name}, 'SWG = QA', '11' </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"< font color='red' > Quantities in {optionData2.Name} now is NOT 'Default House Quantities', {houseData1.PlanNumber + "-" + houseData1.HouseName}, {productData.Name}, 'SWG = QA', '11' </font>");
            }
            //Step 20: Back to house1, specific comm, tab quantities, has option1 with SWG=R, 22, click yellow button
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>----SPECIFIC COMMUNITY----</font>");
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.6: Before copy Option2 has parameters as SWG=QA</font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData2.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData2.Name) is true)
            {
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", optionData2.Name);
                OptionPage.Instance.LeftMenuNavigation("Products");
                if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Parameters", param1))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'> Option {optionData2.Name} has parameters as {param1}</font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'> Option {optionData2.Name} has parameters is not {param1}</font>");
                }
            }
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.7: Then click to 'Copy to Selected Option'</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, houseData1.HouseName);
            HousePage.Instance.SelectItemInGridWithTextContains("Name", houseData1.HouseName);
            HouseDetailPage.Instance.LeftMenuNavigation("Quantities");
            HouseQuantitiesDetailPage.Instance.FilterByCommunity(communityData.Code + "-" + communityData.Name);
            HouseQuantitiesDetailPage.Instance.ClickCopyQuantitiesToHouseOrOption();
            //Select middle comm as specific, select option1 on the left and option2 on the right;
            HouseOptionCopyQuantities.Instance.SelectMiddleCommunity(communityData.Code + "-" + communityData.Name);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconSelectMiddleComm();
            HouseOptionCopyQuantities.Instance.SelectLeftOptioninOptList(optionData1.Name + "-" + optionData1.Description);
            HouseOptionCopyQuantities.Instance.SelectOptionOptionToCpyQty(optionData2.Name + "-" + optionData2.Description);
            //Click white button for option
            HouseOptionCopyQuantities.Instance.ClickCopyQuantitiesOptionToOption();
            HouseOptionCopyQuantities.Instance.ClickAlert(ConfirmType.OK);
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconCpyHouseToHouse();
            bool checkToastMsg = HouseQuantitiesDetailPage.Instance.GetLastestToastMessage().Contains("Quantities successfully copied from option");
            HouseOptionCopyQuantities.Instance.WaitingForLoadingIconClickCopyOptionToOption();
            //Verify a toast message as "Quantities successfully copied from option "option1" to: "option2""
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.8 Toast message to tell the copy from Option1 to Option2 a success</font>");
            if (HouseQuantitiesDetailPage.Instance.GetLastestToastMessage().Contains("Quantities successfully copied from option") is true || checkToastMsg is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'> Quantities successfully copied from {optionData1.Name} to : {optionData2.Name} </font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Quantities are not successfully copied from {optionData1.Name} to: {optionData2.Name} </font>" +
                    $"<br>The actual result: {HouseQuantitiesDetailPage.Instance.GetLastestToastMessage()}</br></font>");
            }
            //Step 21: Back to option2, tab products to verify that in "House Option Quantities" grid has specific comm, house1, SWG=R, quan = 22.00
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>III.9 Check Data: Verify now in Option2 {optionData2.Name} it now has SWG=R, quantity = 22</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionData2.Name);
            if (OptionPage.Instance.IsItemInGrid("Name", optionData2.Name) is true)
            {
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", optionData2.Name);
                OptionPage.Instance.LeftMenuNavigation("Products");
                if (ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Community", communityData.Code + "-" + communityData.Name) &&
                    ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Houses", houseData1.PlanNumber + "-" + houseData1.HouseName) &&
                    ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Products", productData.Name) &&
                    ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Parameters", param2) &&
                    ProductsToOptionPage.Instance.IsHouseOptionQuantitiesInGrid("Quantity", "22.00"))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'> Option {optionData2.Name} has parameters as {communityData.Code + "-" + communityData.Name}, {houseData1.PlanNumber + "-" + houseData1.HouseName}, {productData.Name}, {param2}, '22' </font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'> Option {optionData2.Name} has parameters are NOT as {communityData.Code + "-" + communityData.Name}, {houseData1.PlanNumber + "-" + houseData1.HouseName}, {productData.Name}, {param2}, '22' </font>");
                }
            }

        }
    }
}
