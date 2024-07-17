using NUnit.Framework;
using OpenQA.Selenium;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Export;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorBuildingPhase;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Costing.Vendor.VendorProduct;
using Pipeline.Testing.Pages.Estimating.BuildingGroup;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Purchasing.CostCode;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Script.Section_III
{
    public class D01_C_PIPE_31048 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        private VendorData newVendor;
        private const string NewVendorName = "RT_QA_New_Vendor_D01C";
        private const string NewVendorCode = "D01C";

        private BuildingGroupData newBuildingGroup;
        private const string NewBuildingGroupName = "RT_QA_New_BuildingGroup_D01C";
        private const string NewBuildingGroupCode = "D01C";
        private const string NewBuildingGroupDescription = "RT_QA_New_BuildingGroup_D01C";

        private BuildingPhaseData newBuildingPhase;
        private const string NewBuildingPhaseName = "RT_QA_New_BuildingPhase_D01C";
        private const string NewBuildingPhaseCode = "D01C";

        private string exportFileName;
        private const string ExportCsv = "Export CSV";
        private const string ExportExcel = "Export Excel";

        private const string VendorToBuildingPhaseImport = "Vendors To Building Phases Import";

        [SetUp]
        public void Setup()
        {
            //Initialize test data 
            newVendor = new VendorData()
            {
                Name = NewVendorName,
                Code = NewVendorCode
            };

            newBuildingGroup = new BuildingGroupData()
            {
                Name = NewBuildingGroupName,
                Code = NewBuildingGroupCode,
                Description = NewBuildingGroupDescription
            };

            newBuildingPhase = new BuildingPhaseData()
            {
                Code = NewBuildingPhaseCode,
                Name = NewBuildingPhaseName,
                BuildingGroupCode = NewBuildingGroupCode,
                BuildingGroupName = NewBuildingGroupName
            };

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.1 Create new Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewVendorName);
            System.Threading.Thread.Sleep(2000);
            if (VendorPage.Instance.IsItemInGrid("Name", NewVendorName) is false)
            {
                VendorPage.Instance.ClickAddToVendorIcon();
                VendorDetailPage.Instance.CreateOrUpdateAVendor(newVendor);
            }

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.2: Add New Building Group.</b></font>");
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingGroups);
            BuildingGroupPage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, newBuildingGroup.Code);
            System.Threading.Thread.Sleep(2000);
            if (BuildingGroupPage.Instance.IsItemInGrid("Code", newBuildingGroup.Code) is false)
            {
                BuildingGroupPage.Instance.AddNewBuildingGroup(newBuildingGroup);
            }
            BuildingGroupPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.3 Add new Building Phase.</b></font>");
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (BuildingPhasePage.Instance.IsItemInGrid("Name", NewBuildingPhaseName) is false)
            {
                BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();
                BuildingPhasePage.Instance.AddBuildingPhaseModal
                                          .EnterPhaseCode(newBuildingPhase.Code)
                                          .EnterPhaseName(newBuildingPhase.Name)
                                          .EnterAbbName(newBuildingPhase.AbbName)
                                          .EnterDescription(newBuildingPhase.Description);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(newBuildingPhase.BuildingGroup);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
            }
            BuildingPhasePage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4 Add Building Phase to Vendor.</b></font>");
            VendorDetailPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", newVendor.Name);
            VendorPage.Instance.SelectVendor("Name", newVendor.Name);
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
            System.Threading.Thread.Sleep(2000);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is false)
            {
                System.Threading.Thread.Sleep(1500);
                VendorBuildingPhasePage.Instance.AddBuildingPhase(newBuildingPhase.Code);
                VendorBuildingPhasePage.Instance.WaitBuildingPhaseGird();
                System.Threading.Thread.Sleep(2000);
            }
        }

        [Test]
        public void D01_C_Costing_Vendors_Bulding_Phase_Export_Import()
        {
            //Navigate to Costing > Vendors 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 1.0 Navigate to Costing and select a Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);

            //Click Building Phases link from the left navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Navigate to Base Costs link from the left navigation under Product Cost section.</b></font>");
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);

            //Click on Utilities icon in the upper right part of the page and select Export CSV option from the dropdown list
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2.0 Click Utilities button.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.0: Export Vendor TO BuildingPhase.</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.1: Export CSV.</b></font>");
            exportFileName = $"{CommonHelper.GetExportFileName("")}VendorsToBuildingPhases_" + NewVendorName;
            VendorPage.Instance.ExportFile(ExportCsv, exportFileName, 1, ExportTitleFileConstant.VENDORS_TO_BUILDINGPHASES);
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3.2: Export Excel.</b></font>");
            VendorProductPage.Instance.ExportFile(ExportExcel, exportFileName, 1, ExportTitleFileConstant.VENDORS_TO_BUILDINGPHASES);
            CommonHelper.RefreshPage();

            //Delete all building phases listed in the table grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4. Delete all building phases listed in the table grid</b></font>");
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Filtered successfully<b>");
                VendorBuildingPhasePage.Instance.DeleteItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName);
                string actualdeleteMsg = VendorBuildingPhasePage.Instance.GetLastestToastMessage();
                string expecteddeleteMsg = "Vendor to Building Phase was successfully deleted.";
                if (actualdeleteMsg.Equals(expecteddeleteMsg))
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Vendor to Building Phase was successfully deleted.</b></font>");
                }
            }

            //Add a new entry on the exported .csv file and import
            string importFile = "";
            string expectedErrorMessage = "";
            CostingImportPage.Instance.OpenImportPage();
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.0: Import Vendor.</b></font>");
            if (CostingImportPage.Instance.IsImportLabelDisplay(VendorToBuildingPhaseImport) is false)
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {VendorToBuildingPhaseImport} grid view to import new Vendor.</font>");

            importFile = "\\DataInputFiles\\Costing\\VendorToBuildingPhaseImport\\Pipeline_VendorToBuildingPhase.csv";
            ImportValidData(VendorToBuildingPhaseImport, importFile);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1.1:  Import Vendor To BuildingPhase Wrong file type.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\VendorToBuildingPhaseImport\\Pipeline_VendorToBuildingPhase.txt";
            expectedErrorMessage = "Failed to import file due to wrong file format. File must be .csv format.";
            ImportInvalidData(VendorToBuildingPhaseImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1.2:  Import Vendor To BuildingPhase format import file.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\VendorToBuildingPhaseImport\\Pipeline_VendorToBuildingPhase_Wrong_Format.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(VendorToBuildingPhaseImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1.3:  Import Vendor To BuildingPhase File without header.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\VendorToBuildingPhaseImport\\Pipeline_VendorToBuildingPhase_No_Header.csv";
            expectedErrorMessage = "Failed to import file due to an error in the data format. Column headers do not match expected values.";
            ImportInvalidData(VendorToBuildingPhaseImport, importFile, expectedErrorMessage);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5.1.4:  Import Vendor To BuildingPhase File has the “character” between fields don’t match with the configuration.</b></font>");
            importFile = "\\DataInputFiles\\Costing\\VendorToBuildingPhaseImport\\Pipeline_VendorToBuildingPhase_Invalid_Separator.csv";
            expectedErrorMessage = "Failed to import file. The transfer separation character in the .csv file does not match the current transfer separation character in PL settings. Check your PL settings for the transfer separation character by clicking";
            ImportInvalidData(VendorToBuildingPhaseImport, importFile, expectedErrorMessage);

            //Navigate to Costing > Vendors 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0 Navigate to Costing and select a Vendor.</b></font>");
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", NewVendorName);
            VendorPage.Instance.SelectVendor("Name", NewVendorName);

            //Click Building Phases link from the left navigation
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.1 Navigate to Base Costs link from the left navigation under Product Cost section.</b></font>");
            VendorDetailPage.Instance.LeftMenuNavigation("Building Phases", true);
            VendorBuildingPhasePage.Instance.FilterItemInGrid("Building Phase", GridFilterOperator.Contains, NewBuildingPhaseName);
            if (VendorBuildingPhasePage.Instance.IsItemInGrid("Building Phase", NewBuildingPhaseCode + "-" + NewBuildingPhaseName) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Building phases added and displayed in the table grid<b>");
            }
            CommonHelper.RefreshPage();
        }


        [TearDown]
        public void ClearData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6.0 No Tear Down of Data because the existing data will re used.</b></font>");
        }

        private void ImportValidData(string importGridTitle, string fullFilePath)
        {
            string actualMessage = CostingImportPage.Instance.ImportFile(importGridTitle, fullFilePath);

            string expectedMessage = "Import complete.";
            if (expectedMessage.ToLower().Contains(actualMessage.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The valid file was NOT imported." +
                    $"<br>The toast message is: {actualMessage}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The valid file was imported successfully and the toast message indicated success.</b></font>");

        }

        private void ImportInvalidData(string importGridTitlte, string fullFilePath, string expectedFailedData)
        {
            string actualMessage = CostingImportPage.Instance.ImportFile(importGridTitlte, fullFilePath);

            if (actualMessage.ToLower().Contains(expectedFailedData.ToLower()) is false)
                ExtentReportsHelper.LogFail($"<font color='red'>The invalid file should fail to import.</font>" +
                    $"<br>The expected message is: {expectedFailedData}</br></font>");
            else
                ExtentReportsHelper.LogPass($"<font color='green'><b>The invalid file failed to import and the toast message indicated failure.</b></font>");

        }
    }
}
