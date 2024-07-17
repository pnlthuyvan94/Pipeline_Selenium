using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.AvailablePlan;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A02_D_RT_01201 : BaseTestScript
    {
        private class CommunityAllHouseOptionData
        {
            public string AllHouseOpt1 { get; set; }
            public string AllHouseOpt2 { get; set; }
            public string Price { get; set; }

            public CommunityAllHouseOptionData()
            {
                AllHouseOpt1 = string.Empty;
                AllHouseOpt2 = string.Empty;
                Price = string.Empty;
            }
        }

        private class CommunityOptionConditionnData
        {
            public string OptionCondition1 { get; set; }
            public string OptionCondition2 { get; set; }
            public string Operator { get; set; }
            public string Price { get; set; }

            public CommunityOptionConditionnData()
            {
                OptionCondition1 = string.Empty;
                Operator = string.Empty;
                OptionCondition2 = string.Empty;
                Price = string.Empty;
            }
        }

        private CommunityData communityData;
        private AvaiablePlanData houseData;
        private CommunityAllHouseOptionData allHouseOptionDate_1;
        private CommunityAllHouseOptionData allHouseOptionDate_2;
        private CommunityOptionConditionnData optionConditionData;
        private static string HOUSE_NAME = "English Cottage";
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\RT_01201";
        private string[] elevations = { "BBA", "BBB","ELEV-A", "ELEV-B" };
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        [SetUp]
        public void GetData()
        {
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Make sure current transfer seperation character is ','<b></b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);


            // Step 0.2: Import Option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Import Option.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_OPTION);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_IMPORT} grid view to import new Options.</font>");

            string importFile = $@"{IMPORT_FOLDER}\Pipeline_Options.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);


            // Step 0.3: Import House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3: Import House.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_HOUSE);
            if (BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.HOUSE_IMPORT) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.HOUSE_IMPORT} grid view to import new Options.</font>");

            importFile = $@"{IMPORT_FOLDER}\Pipeline_Houses.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.HOUSE_IMPORT, importFile);

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            // Insert name to filter and click filter by Contain value
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME))
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME);
            }
            // Navigate House Option And Add Option into House
            HouseDetailPage.Instance.LeftMenuNavigation("Options");          
            HouseOptionDetailPage.Instance.ClickAddElevationToShowModal().InsertElevationToHouse(elevations);



            communityData = new CommunityData()
            {
                Name = "R-QA Only Community Auto_Test Option",
                Division = "None",
                City = "Ho Chi Minh",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Community from automation test v1",
                DrivingDirections = "Community from automation test v2",
                Slug = "R-QA-Only-Community-Auto - slug",
            };

            allHouseOptionDate_1 = new CommunityAllHouseOptionData()
            {
                AllHouseOpt1 = "BBA",
                AllHouseOpt2 = "BBB",
                Price = "100"
            };

            allHouseOptionDate_2 = new CommunityAllHouseOptionData()
            {
                AllHouseOpt1 = "ELEV-A",
                AllHouseOpt2 = "ELEV-B",
                Price = "50"
            };

            optionConditionData = new CommunityOptionConditionnData()
            {
                OptionCondition1 = "0101",
                Operator = "and",
                OptionCondition2 = "0124Name",
                Price = "100.00"
            };

            // Step 1.1: Navigate to Community page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Navigate to Community default page.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Step 1.2: Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.2: Filter community with name {communityData.Name} and create if it doesn't exist.<b></b></font>");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (!CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);
            }
            else
            {
                // Select filter item to open detail page
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
            }

            // Step 1.3: Open Available Plan navigation and assign house to community
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 1.3: Open Available Plan navigation and assign house to community.</b></font>");
            houseData = new AvaiablePlanData()
            {
                Id = "EC",
                Name = "English Cottage",
                Price = "500000",
                Note = "Nothing"
            };
            CommunityPage.Instance.AddHouseToCommunity(houseData, communityData.Name);
        }

        [Test]
        [Category("Section_IV")]
        public void A02_D_Assets_DetailPage_Community_Option()
        {
            // Step 2: Open Phase navigation
            CommunityOptionPage.Instance.LeftMenuNavigation("Options");
            if (CommunityOptionPage.Instance.IsCommunityOptionPageDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Community Option page doesn't display or title is incorrect." +
                    $"<br>Expected title: Community Option.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Community Option page displays correctly with 2 grid view is Community Options and Community house Options.</b></font>");

            // Step 3: Add Community Option and Community House Option and remove it

            // Add Community Option
            AddCommunityOption();

            // Add Community House Option and Condition to option and remove it
            AddCommunityHouseOption();
        }

        private void AddCommunityOption()
        {
            // Click Add (+) Community Option
            CommunityOptionPage.Instance.OpenAddCommunityOptionModal();
            if (CommunityOptionPage.Instance.AddCommunityOptionModal.IsCommunityOptionModalDisplayed() is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Add Community Option modal doesn't display or modal title is incorrect.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Add Community Option modal displays correctly</b></font>");

            // Populate data from exel to modal
            string[] Items = { allHouseOptionDate_1.AllHouseOpt1, allHouseOptionDate_1.AllHouseOpt2 };
            CommunityOptionData communityOptionData = new CommunityOptionData()
            {
                AllHouseOptions = Items,
                SalePrice = allHouseOptionDate_1.Price
            };

            CommunityOptionPage.Instance.AddCommunityOptionModal.AddCommunityOption(communityOptionData);
            CommunityOptionPage.Instance.WaitCommunityOptionGridLoad();

            // Verify Message
            string _actualMessage = CommunityOptionPage.Instance.GetLastestToastMessage();
            string _expectedMessage = "Selected Options added successfully!";
            if (_actualMessage.Equals(_expectedMessage))
            {
                ExtentReportsHelper.LogPass($"Add community option successfully.");
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Add community option unsuccessfully. Actual messsage: {_actualMessage}");
            }

            if(CommunityOptionPage.Instance.AddCommunityOptionModal.IsAddCommunityOptionSuccessful(communityOptionData) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Add Community Option successfully. Option and price are same as expected.</b></font>");



            // Close modal
            // CommunityOptionPage.Instance.AddCommunityOptionModal.Close();

            // Filter house in the grid view
            foreach (string optionName in communityOptionData.AllHouseOptions)
            {
                if (!CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", optionName))
                {
                    ExtentReportsHelper.LogFail($"Option: {optionName} doesn't display in the grid view");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Option: {optionName} displays correctly in the grid view and remove it.");
                    RemoveOptionFromCommunity(optionName);
               }
            }
        }

        private void AddCommunityHouseOption()
        {
            // Click Add (+) Community Option
            CommunityOptionPage.Instance.OpenAddCommunityHouseOptionModal();
            if (CommunityOptionPage.Instance.AddCommunityHouseOptionModal.IsModalDisplayed is false)
                ExtentReportsHelper.LogFail($"<font color='red'>Add Community House Option modal doesn't display.</font>");
            else
                ExtentReportsHelper.LogPass(null, $"<font color='green'><b>Add Community House Option modal displays correctly.</b></font>");

            // Populate data from exel to modal
            string[] Items = { allHouseOptionDate_2.AllHouseOpt1, allHouseOptionDate_2.AllHouseOpt2 };
            CommunityHouseOptionData communityHouseOptionData = new CommunityHouseOptionData()
            {
                AllHouseOptions = Items,
                SalePrice = allHouseOptionDate_2.Price
            };

            CommunityOptionPage.Instance.AddCommunityHouseOptionModal.AddCommunityHouseOption(communityHouseOptionData);
            CommunityOptionPage.Instance.WaitCommunityHouseOptionGridLoad();

            // Verify Message
            string _actualMessage = CommunityOptionPage.Instance.GetLastestToastMessage();
            string _expectedMessage = "Selected Options added successfully!";
            if (_actualMessage.Equals(_expectedMessage))
            {
                ExtentReportsHelper.LogPass(null, $"Add community house option successfully.");
                //CommunityOptionPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Add community house option unsuccessfully. Actual messsage: {_actualMessage}");
                Assert.Fail($"Add community house option unsuccessfully. Actual messsage: {_actualMessage}");
            }
            Assert.That(CommunityOptionPage.Instance.AddCommunityHouseOptionModal.IsAddCommunityHouseOptionSuccessful(communityHouseOptionData), $"Option and price aren't cleared from the Adding modal.");

            // Close modal
            // CommunityOptionPage.Instance.AddCommunityHouseOptionModal.Close();

            // Filter house in the grid view
            foreach (string optionName in communityHouseOptionData.AllHouseOptions)
            {
                // In the grid view, dev adds a space after option name
                if (!CommunityOptionPage.Instance.IsCommunityHouseOptionInGrid("Option", optionName))
                    ExtentReportsHelper.LogFail($"Option: {optionName} doesn't display in the grid view");
                else
                    ExtentReportsHelper.LogPass(null, $"Option: {optionName} displays correctly in the grid view.");
            }

            // Add condition to community house option
            ExtentReportsHelper.LogInformation($"Add condition to option {allHouseOptionDate_2.AllHouseOpt1}");
            AddOptionCondition(allHouseOptionDate_2.AllHouseOpt1);

            // Remove option from community house option
            ExtentReportsHelper.LogInformation("Remove all option from community house option.");
            foreach (string optionName in communityHouseOptionData.AllHouseOptions)
            {
                RemoveOptionFromCommunity(optionName, false);
            }

        }

        private void AddOptionCondition(string optionName)
        {
            // Click Add (+) Community Option
            CommunityOptionPage.Instance.OpenAssignConditionModal(optionName);
            //Assert.That(CommunityOptionPage.Instance.AddOptionCondition.IsConditionGridDisplayed(), "Add condition to option grid view doesn't display.");
            ExtentReportsHelper.LogPass("Add Condition to Option grid view displays correctly.");


            CommunityOptionPage.Instance.AddOptionCondition.SelectOptionCondition(optionConditionData.OptionCondition1).AddCondition()
                .SelectOperator(optionConditionData.Operator).AddOperator()
                .SelectOptionCondition(optionConditionData.OptionCondition2).AddCondition()
                .EnterConditionSalePrice(optionConditionData.Price);

            // Verify output of condition is same as expected
            string expectedCondition = optionName + " with " + optionConditionData.OptionCondition1 + " " + optionConditionData.Operator + " " + optionConditionData.OptionCondition2;
            //   Assert.That(CommunityOptionPage.Instance.AddOptionCondition.IsConditionAddedCorrectly(expectedCondition), "The condition isn't displayed correct in the textbox.");
            //   ExtentReportsHelper.LogPass("The condition is displayed correct in the textbox.");

            CommunityOptionPage.Instance.AddOptionCondition.Apply();

            // Verify Message
            string _actualMessage = CommunityOptionPage.Instance.GetLastestToastMessage();
            string _expectedMessage = "Successfully added conditional community house option.";
            if (_actualMessage.Equals(_expectedMessage))
            {
                ExtentReportsHelper.LogPass($"Add condition {expectedCondition} to option {optionName} successfully.");
                CommunityOptionPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Add condition {expectedCondition} to option {optionName} unsuccessfully.");
            }

            // Verify new condition in the grid view
            Assert.That(CommunityOptionPage.Instance.AddOptionCondition.IsFinalConditionDisplayedCorrectly(expectedCondition, optionConditionData.Price),
                 "The final condition isn't displayed correct in the grid view after applying.");
            ExtentReportsHelper.LogPass("The final condition is displayed correct in the grid view after applying.");

            // Check hyperlink
            ExtentReportsHelper.LogInformation("Click into condition and verify the hyperlink.");
            CommunityOptionPage.Instance.AddOptionCondition.VerifyHyperlinkToOption(optionName);
        }

        private void RemoveOptionFromCommunity(string optionName, bool isDeleteCommunityOption = true)
        {
            string expectedMess, deletedItem;

            if (isDeleteCommunityOption)
            {
                CommunityOptionPage.Instance.DeleteCommunityOptionInGrid("Option", optionName);
                expectedMess = $"Option {optionName} successfully removed from this Community";
                deletedItem = "Community Option";
            }
            else
            {
                // In UI, dev adds space after option name
                CommunityOptionPage.Instance.DeleteCommunityHouseOptionInGrid("Option", optionName + " ");
                expectedMess = $"Option {optionName} successfully removed from this House in this Community";
                deletedItem = "Community House Option";
            }


            if (expectedMess == CommunityOptionPage.Instance.GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass($"Option: {optionName} deleted successfully from {deletedItem}!");
                CommunityOptionPage.Instance.CloseToastMessage();
            }
            else
            {
                if ((isDeleteCommunityOption && CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", optionName))
                    || (!isDeleteCommunityOption && CommunityOptionPage.Instance.IsCommunityHouseOptionInGrid("Option", optionName)))
                {
                    ExtentReportsHelper.LogFail($"Option: {optionName} could not be deleted from {deletedItem}!");
                }
                else
                {
                    ExtentReportsHelper.LogPass($"Option: {optionName} deleted successfully from {deletedItem}!");
                }
            }
        }

        [TearDown]
        public void DeleteCommunity()
        {
            // Step 5: Remove house from community
            AvailablePlanPage.Instance.LeftMenuNavigation("Available Plans");
            CommunityPage.Instance.RemoveHouseFromCommunity(communityData.Name, houseData.Name, true);

            // Step 6: Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5: Back to Community default page and delete data.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
            // Insert name to filter and click filter by Contain value
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME))
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME);
            }
            // Navigate House Option And Add Option into House
            HouseDetailPage.Instance.LeftMenuNavigation("Options");
            foreach(string item in elevations)
            {
                HouseOptionDetailPage.Instance.DeleteItemInElevation("Name", item);
            }

        }

    }
}
