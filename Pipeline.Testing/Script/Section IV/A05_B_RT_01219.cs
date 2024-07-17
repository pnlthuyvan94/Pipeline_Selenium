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
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.Assigments;
using Pipeline.Testing.Pages.Assets.Series;
using Pipeline.Testing.Pages.Settings.BuilderMT;
using Pipeline.Testing.Pages.Settings.HomeFront;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A05_B_RT_01219 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        readonly string HOUSENAME = "hai nguyen house";
        readonly string CODENAME = "0011";
        readonly string COMMUNITYNAME = "RT_Community_Auto_Dont_Edit_Delete";
        readonly string prodName = "QA_RT_Option_Automation_01";

        string optionName;
        OptionData _option;
        CommunityData _community;
        SeriesData _series;
        HouseData _housedata;

        [SetUp]
        public void GetData()
        {
            _community = new CommunityData()
            {
                Name = "RT_Community_Auto_Dont_Edit_Delete",
                Division = "None",
                City = "Ho Chi Minh",
                Code = "Automation_Edit_Delete_01",
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
                Slug = "RT_Community_Auto_Dont_Edit_Delete"
            };

            _series = new SeriesData()
            {
                Name = "Visions",
                Code = "",
                Description = ""
            };

            _housedata = new HouseData()
            {
                HouseName = "hai nguyen house",
                SaleHouseName = "Sweet home",
                Series = "Visions",
                PlanNumber = "0011",
                BasePrice = "1000000",
                SQFTBasement = "100",
                SQFTFloor1 = "250",
                SQFTFloor2 = "200",
                SQFTHeated = "50",
                SQFTTotal = "600",
                Style = "Single Family",
                Stories = "0",
                Bedrooms = "0",
                Bathrooms = "0",
                Garage = "1 Car",
                Description = "hai nguyen house"
            };

            // Get old data
            optionName = "R-QA Only Option Auto";

            // Open setting page and turn on Builder MT
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            BuilderMTPage.Instance.LeftMenuNavigation("BuilderMT");
            BuilderMTPage.Instance.SelectGroupByParameter(true);

            // Open setting page and turn on HomeFront
            HomeFrontPage.Instance.LeftMenuNavigation("HomeFront");
            HomeFrontPage.Instance.SetStatus(true);


            //Prepare Series Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare to Series Page.</font>");
            // Go to the Series default page
            //SeriesPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Series);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SERIES_URL);
            // Filter the created series 
            SeriesPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _series.Name);

            // Verify the item is display in list
            if (!SeriesPage.Instance.IsItemInGrid("Name", _series.Name))
            {
                // Create new series to test
                SeriesPage.Instance.ClickAddToSeriesModal();

                if (SeriesPage.Instance.AddSeriesModal.IsModalDisplayed() is false)
                {
                    ExtentReportsHelper.LogFail(null, $"<font color='red'>Add Series modal is displayed.</font>");
                }
                else 
                {
                    SeriesPage.Instance.AddSeriesModal
                                             .EnterSeriesName(_series.Name)
                                             .EnterSeriesCode(_series.Code)
                                             .EnterSeriesDescription(_series.Description);

                    // Select the 'Save' button on the modal;
                    SeriesPage.Instance.AddSeriesModal.Save();

                    // Verify successful save and appropriate success message.
                    string _expectedMessage = "Series " + _series.Name + " created successfully!";
                    string _actualMessage = SeriesPage.Instance.AddSeriesModal.GetLastestToastMessage();
                    if (_expectedMessage.Equals(_actualMessage))
                    {
                        ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                        SeriesPage.Instance.CloseToastMessage();
                    }
                    else
                        ExtentReportsHelper.LogFail($"The message does not as expected. \nActual results: {_actualMessage}\nExpected results: {_expectedMessage} ");
                }
                // Close modal
                //SeriesPage.Instance.AddSeriesModal.CloseModal();
            }
            else
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Serires with name {_series.Name} is displayed in grid.</font>");
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Navigate to House default page.</font><b>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_HOUSE_URL);
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
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>The House With Name : {_housedata.HouseName} is displayed in grid.</font><b>");

            }

            //Prepare Community Data
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Prepare Community Page.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", _community.Name) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<font color='lavender'>The Communtity with name {_community.Name} is displayed in grid.</font>");
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

            // Go to Option default page
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            System.Threading.Thread.Sleep(5000);

            // Filter
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, optionName);

            var optionType = new List<bool>()
            {
                true, true, true
            };
            _option = new OptionData()
            {
                Name = "R-QA Only Option Auto",
                Number = "13579",
                SquareFootage = 10000,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "Windows",
                OptionRoom = string.Empty,
                CostGroup = "All Rooms",
                OptionType = "Design",
                Price = 100,
                Types = optionType
            };

            if (OptionPage.Instance.IsItemInGrid("Name", optionName) is false)
            {
                // If option isn't existing then create a new one
                OptionPage.Instance.CreateNewOption(_option);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", optionName);
            }

            // Go to Assigment page
            OptionPage.Instance.LeftMenuNavigation("Assignments");
        }

        [Test]
        [Category("Section_IV")]
        public void A05_B_Assets_DetailPage_Options_Assignments()
        {
            // Switch to other page and verify
            foreach (AssigmentView viewBy in Enum.GetValues(typeof(AssigmentView)))
            {
                AssignmentDetailPage.Instance.SwitchAssignmentView(viewBy);
                AssignmentDetailPage.Instance.VerifyItemInPage(viewBy);
            }

            // Back to Builder page
            AssignmentDetailPage.Instance.SwitchAssignmentView(AssigmentView.Builder);

            // Add House
            string expectedMsg = "";
            if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HOUSENAME))
            {
                AssignmentDetailPage.Instance.ClickAddHouseToShowModal().AddHousesToOption(CODENAME + "-" + HOUSENAME);
                expectedMsg = "House(s) added to house successfully";
                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("House(s) added to house successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }

                // Verify item in House grid
                if (!AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HOUSENAME))
                {
                    ExtentReportsHelper.LogFail($"House with name {HOUSENAME} is NOT add to this Option successfully");
                }
            }

            // Add Community
            if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITYNAME))
            {
                AssignmentDetailPage.Instance.ClickAddCommunityToShowModal().AddCommunityToOption("-" + COMMUNITYNAME);
                expectedMsg = "Option(s) added to community successfully";
                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Option(s) added to community successfully");
                    //AssignmentDetailPage.Instance.CloseToastMessage();
                }

                // Verify item in Community grid
                if (!AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITYNAME))
                {
                    ExtentReportsHelper.LogFail($"Community with name {COMMUNITYNAME} is NOT add to this Option successfully");
                }
            }

            // Add Product
            // Verify item in Product grid
            if (AssignmentDetailPage.Instance.IsItemInProductGrid("Name", prodName) is false)
            {
                AssignmentDetailPage.Instance.ClickAddProductQuantityOptionToShowModal().AddProductQuantityToOption(prodName);
                expectedMsg = "Product Quantity Child Option(s) successfully added";
                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass("Product Quantity Child Option(s) successfully added");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
            }
            //AssignmentDetailPage.Instance.CloseAddProductQuantityOptionModal();
            // Verify item in Product grid
            if (!AssignmentDetailPage.Instance.IsItemInProductGrid("Name", prodName))
            {
                ExtentReportsHelper.LogFail($"Product with name {prodName} is NOT add to this Option successfully");
                //Assert.Fail();
            }

            // Delete House items
            if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HOUSENAME) is true)
            {
                AssignmentDetailPage.Instance.DeleteItemInHouseGrid("Name", HOUSENAME);
                System.Threading.Thread.Sleep(3000);
                expectedMsg = $"Option {optionName} successfully removed from this House";
                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass($"House with name {HOUSENAME} removed successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
                // Verify item in House grid
                if (AssignmentDetailPage.Instance.IsItemInHouseGrid("Name", HOUSENAME) is true)
                {
                    ExtentReportsHelper.LogFail($"House with name {HOUSENAME} is NOT remove to this Option successfully");
                    //Assert.Fail();
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"House with name {HOUSENAME} is Not display in Grid.");
                }

                ExtentReportsHelper.LogPass($"===================== Remove items ======================");
                // Delete Community items

                if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                {
                    ExtentReportsHelper.LogPass($"Community with name {COMMUNITYNAME} removed successfully");
                    AssignmentDetailPage.Instance.CloseToastMessage();
                }
                // Verify item in Community grid
                if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITYNAME) is true)
                {
                    AssignmentDetailPage.Instance.DeleteItemInCommunityGrid("Name", COMMUNITYNAME);
                    expectedMsg = $"Option {optionName} successfully removed from this Community";
                    if (AssignmentDetailPage.Instance.IsItemInCommunityGrid("Name", COMMUNITYNAME) is true)
                    {
                        ExtentReportsHelper.LogFail($"Community with name {COMMUNITYNAME} is NOT remove to this Option successfully");
                    }
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Community with name {COMMUNITYNAME} is NOT display grid");
                }

                // Delete Product items

                // Verify item in Product grid
                if (AssignmentDetailPage.Instance.IsItemInProductGrid("Name", prodName) is true)
                {
                    AssignmentDetailPage.Instance.DeleteItemInProductGrid("Name", prodName);
                    expectedMsg = $"Product Quantity Child Option(s) succssfully removed.";
                    if (expectedMsg.Equals(AssignmentDetailPage.Instance.GetLastestToastMessage()))
                    {
                        ExtentReportsHelper.LogPass($"Product Quantity Child Option(s) succssfully removed.");
                        AssignmentDetailPage.Instance.CloseToastMessage();
                    }
                    if (AssignmentDetailPage.Instance.IsItemInProductGrid("Name", prodName) is true)
                    {
                        ExtentReportsHelper.LogFail($"Product with name {prodName} is NOT remove from this Option successfully");
                    }
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"Product with name {prodName} is NOT display in grid");
                    //Assert.Fail();
                }
            }
        }
    }
}
