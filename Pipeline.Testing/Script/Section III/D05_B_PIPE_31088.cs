using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Assets.Communities.Options;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.Communities;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.Options;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Costing.CostingEstimate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_III
{
    public class D05_B_PIPE_31088 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private OptionData newOption;
        private const string NewOptionName = "RT_QA_New_Option_D05B";
        private const string NewOptionNumber = "D05B";


        private HouseData newHouse;
        private const string NewHouseName = "RT_QA_New_House_D05B";
        private const string NewHousePlanNo = "D05B";

        private CommunityData newCommunity;
        private const string NewCommunityName = "RT_QA_New_Community_D05B";

        private const string ExportHouseEstimateCSV = "Export House Estimate to CSV";
        private const string ExportHouseEstimateXls = "Export House Estimate to Excel";
        private string exportFileName = "";

        [SetUp]
        public void Setup()
        {
            var optionType = new List<bool>()
            {
                false, false, false
            };
            newOption = new OptionData()
            {
                Name = NewOptionName,
                Number = NewOptionNumber,
                Types = optionType
            };
            newHouse = new HouseData()
            {
                HouseName = NewHouseName,
                PlanNumber = NewHousePlanNo
            };

            newCommunity = new CommunityData()
            {
                Name = NewCommunityName
            };
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Create new Option.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewOptionName);
            System.Threading.Thread.Sleep(2000);
            if (OptionPage.Instance.IsItemInGrid("Name", NewOptionName) is false)
            {
                OptionPage.Instance.ClickAddToOpenCreateOptionModal();
                OptionPage.Instance.AddOptionModal.AddOption(newOption);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2 Add new House.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewHouseName);
            System.Threading.Thread.Sleep(2000);
            if (HousePage.Instance.IsItemInGrid("Name", NewHouseName) is false)
            {
                HousePage.Instance.CreateHouse(newHouse);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new Community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is false)
            {
                CommunityPage.Instance.CreateCommunity(newCommunity);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add house to Community.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewHouseName);
            System.Threading.Thread.Sleep(2000);
            if (HousePage.Instance.IsItemInGrid("Name", NewHouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", NewHouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Communities", true);
                HouseCommunities.Instance.FillterOnGrid("Name", NewCommunityName);
                System.Threading.Thread.Sleep(2000);
                if (HouseCommunities.Instance.IsValueOnGrid("Name", NewCommunityName) is false)
                {
                    HouseCommunities.Instance.AddButtonCommunities();
                    HouseCommunities.Instance.AddAndVerifyCommunitiesToHouse(NewCommunityName, null);
                }
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.6 Add option to House.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewHouseName);
            System.Threading.Thread.Sleep(2000);
            if (HousePage.Instance.IsItemInGrid("Name", NewHouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", NewHouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Options", true);
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", NewOptionName) is false)
                {
                    HouseOptionDetailPage.Instance.ClickAddOptionToShowModal().InsertOptionToHouse(NewOptionName + " - " + NewOptionNumber);
                }
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.5 Add option to Community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName);
                CommunityDetailPage.Instance.LeftMenuNavigation("Options", true);
                CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.EqualTo, NewOptionName);
                if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", NewOptionName) is false)
                {
                    string[] optionData = { NewOptionName + " - " + NewOptionNumber };
                    CommunityOptionPage.Instance.AddCommunityOption(optionData);
                }
            }
        }

        [Test]
        public void D05_B_Costing_Estimate_House_Estimates()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0: Go to Costing Estimates Page.</b></font>");

            CostingEstimatesPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.CostingEstimate);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Click House Estimates button.</b></font>");
            CostingEstimatesPage.Instance.ClickHouseEstimates();
            CommonHelper.CaptureScreen();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.1: Select House " + newHouse + ".</b></font>");
            CostingEstimatesPage.Instance.SelectHouse(NewHousePlanNo + "-" + NewHouseName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.2: Select COmmunity " + NewCommunityName + ".</b></font>");
            CostingEstimatesPage.Instance.SelectCommunity(NewCommunityName + "-" + NewCommunityName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3: Validate if new option " + NewOptionName + " is displayed on the grid.</b></font>");
            CommonHelper.CaptureScreen();
            if (CostingEstimatesPage.Instance.IsItemInHouseGrid("Option", NewOptionName) is true)
            {
                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4: Export House Estimates to CSV File.</b></font>");
                exportFileName = $"{CommonHelper.GetExportFileName(ExportType.HouseEstimate.ToString())}_{NewHouseName}";
                CostingEstimatesPage.Instance.ExportFile2(ExportHouseEstimateCSV, exportFileName, 1, ExportTitleFileConstant.HOUSE_ESTIMATES_TITLE);

                ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5: Export House Estimates to Excel File.</b></font>");
                exportFileName = $"{CommonHelper.GetExportFileName(ExportType.HouseEstimate.ToString())}_{NewHouseName}";
                CostingEstimatesPage.Instance.ExportFile2(ExportHouseEstimateXls, exportFileName, 1, ExportTitleFileConstant.HOUSE_ESTIMATES_TITLE, false);
                CommonHelper.RefreshPage();
            }
            else
            {
                ExtentReportsHelper.LogFail(null, "<font color='lavender'><b>No House Estimates data found.</b></font>");

            }
        }

        [TearDown]
        public void ClearData()
        {
            //delete option from house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.1 Delete option from House.</b></font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewHouseName);
            System.Threading.Thread.Sleep(2000);
            if (HousePage.Instance.IsItemInGrid("Name", NewHouseName) is true)
            {
                HousePage.Instance.SelectItemInGridWithTextContains("Name", NewHouseName);
                HouseDetailPage.Instance.LeftMenuNavigation("Options", true);
                if (HouseOptionDetailPage.Instance.IsItemInOptionGrid("Name", NewOptionName) is true)
                {
                    HouseOptionDetailPage.Instance.DeleteItemInOption("Name", NewOptionName);
                }
            }
            //delete option from community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.2 Delete option from Community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is true)
            {
                CommunityPage.Instance.SelectItemInGrid("Name", NewCommunityName);
                CommunityDetailPage.Instance.LeftMenuNavigation("Options", true);
                CommunityOptionPage.Instance.FilterItemInGrid("Option", GridFilterOperator.EqualTo, NewOptionName);
                if (CommunityOptionPage.Instance.IsCommunityOptionInGrid("Option", NewOptionName) is true)
                {
                    CommunityOptionPage.Instance.DeleteCommunityOptionInGrid("Option", NewOptionName);
                }
            }
            //delete option
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.3 Delete new Option.</b></font>");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.DeleteOption(NewOptionName);

            //delete community
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.4 Delete new Community.</b></font>");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.EqualTo, NewCommunityName);
            System.Threading.Thread.Sleep(2000);
            if (CommunityPage.Instance.IsItemInGrid("Name", NewCommunityName) is true)
            {
                CommunityPage.Instance.DeleteCommunity(NewCommunityName);
            }

            //delete house
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.5 Delete new House.</b></font>");
            HousePage.Instance.DeleteHouse(NewHouseName);
        }
    }
}
