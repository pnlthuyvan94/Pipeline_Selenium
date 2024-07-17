using NUnit.Framework;
using NUnit.Framework.Legacy;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.Costing.Vendor;
using Pipeline.Testing.Pages.Costing.Vendor.VendorDetail;
using Pipeline.Testing.Pages.Estimating.BuildingPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;
using Pipeline.Testing.Pages.Jobs.Job;
using Pipeline.Testing.Pages.Jobs.Job.JobDetail;
using Pipeline.Testing.Pages.Settings.BuildPro;
using Pipeline.Testing.Pages.Settings.Costing;
using RestSharp;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;

namespace Pipeline.Testing.Script.Integrations
{
    public class RT_01166 : BaseTestScript
    {
        // BuildPro infor
        readonly string buildProApplicationURL = "https://uat.hyphensolutions.com/Build/LogIn.asp";
        readonly string buildProUsername = "system.admin.CG";
        readonly string buildProPassword = "1[CGVisions3990";
        readonly string xmlImportFile = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\DataInputFiles\hn-02.xml";


        // log in with super admin
        // super admin
        readonly string _superUserName = "sysadmin";
        readonly string _superPass = "shoreline";

        // Buildpro data
        BuildProData buildProData;
        CommunityData communityCreated, communityUpdated;
        BuildingPhaseData buildingPhaseCreated, buildingPhaseUpdated;
        VendorData vendorCreated, vendorUpdated;

        string dataCreatedURL;

        [SetUp]
        public void SetUp()
        {
            UpdateRandomQuantities();
            buildProData = new BuildProData();

            communityCreated = new CommunityData()
            {
                Name = "BuildPro Community",
                Code = "BPCT",
                Division = "CG Visions",
                City = "Hyphen City",
                State = "TX",
                Zip = "39615",
                Status = "Closed",
                Description = "Description"
            };
            communityUpdated = new CommunityData(communityCreated)
            {
                Name = communityCreated.Name + " Edit",
                City = communityCreated.City + " Edit",
                State = "AL",
                Zip = "35242"
            };
            buildingPhaseCreated = new BuildingPhaseData()
            {
                Code = "9420",
                Name = "Sample BuildPro Phase",
                AbbName = "Sample BP Phase",
                Description = "Test BuildPro",
                BuildingGroupCode = "620",
                BuildingGroupName = "QA Only Building Group",
                PercentBilled = "100",
                Taxable = true
            };
            buildingPhaseUpdated = new BuildingPhaseData(buildingPhaseCreated)
            {
                AbbName = buildingPhaseCreated.AbbName + " Edit"
            };
            vendorCreated = new VendorData()
            {
                Name = "BuildProAdd1",
                Code = "BP1T",
                Email = "hainguyen@email.com",
                Address1 = "399 SR 38 E",
                City = "Lafayette",
                State = "IN",
                Zip = "47905"
            };
            vendorUpdated = new VendorData(vendorCreated)
            {
                Name = "BuildProAdd2"
            };
        }

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Integrations);
        }

        [Test]
        [Category("Integrations")]
        [Order(1)]
        [Ignore("Integration test scripts will be ignored at this time and be implemented in the future.")]
        public void IT02_Pipeline_BuildPro()
        {
            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>BuildPro Setting</b></font>************************************");
            IT02_Pipeline_BuildPro_Setting();
            CommonFuncs.LogOut();
            CommonFuncs.LoginToPipeline(ConfigurationManager.GetValue(BaseConstants.UserName), ConfigurationManager.GetValue(BaseConstants.Password));

            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>BuildPro Community</b></font>************************************");
            IT02_Pipeline_BuildPro_Community();
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>BuildPro Building Phase</b></font>************************************");
            IT02_Pipeline_BuildPro_BuildingPhase();
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>BuildPro Vendor</b></font>************************************");
            IT02_Pipeline_BuildPro_Vendor();
            CommonHelper.RefreshPage();

            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>BuildPro Job</b></font>************************************");
            IT02_Pipeline_BuildPro_Job();
        }

        private void IT02_Pipeline_BuildPro_Setting()
        {
            // Step 1: navigate to this setting page
            BuildProPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);

            BuildProPage.Instance.LeftMenuNavigation("BuildPro");

            // Log out and re-log in with super admin user to modified
            ExtentReportsHelper.LogInformation(null, "Log out and re-log in with super admin user to update the system information");
            CommonFuncs.LogOut();
            // Go to the Build pro in the setting page
            CommonFuncs.LoginToPipeline(_superUserName, _superPass);

            ExtentReportsHelper.LogInformation(null, "Input invalid credential and test the connection");

            // Edit username and pass and test connection
            BuildProPage.Instance.UpdateRootURI(buildProData.BuildProSetting.RootURI + "_invalid_ROOT_URL").UpdateUserName(buildProData.BuildProSetting.Username + " invalid user name").UpdatePassword(buildProData.BuildProSetting.Password).TestConnection();

            System.Threading.Thread.Sleep(500);

            // Get the message and verify
            string _expectedMessage = "Pipeline was not able to communicate with BuildPro®.";
            string _actualMessage = BuildProPage.Instance.GetListMessage()[0];
            BuildProPage.Instance.GetListMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                BuildProPage.Instance.CloseToastMessage();
                System.Threading.Thread.Sleep(1000);
                BuildProPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail("The message is NOT dispaly as expected. Actual results: " + _actualMessage);
                BuildProPage.Instance.CloseToastMessage();
                Assert.Fail("The message is NOT dispaly as expected. Actual results: " + _actualMessage);
            }

            ExtentReportsHelper.LogInformation(null, "Reupdate the correct value and test connection again");
            // Reupdate correct value
            ExtentReportsHelper.LogInformation(null, "Input valid credential and re-test the connection");

            BuildProPage.Instance.UpdateRootURI(buildProData.BuildProSetting.RootURI)
                                 .UpdateUserName(buildProData.BuildProSetting.Username)
                                 .UpdatePassword(buildProData.BuildProSetting.Password)
                                 .UpdateCompany(buildProData.BuildProSetting.Company)
                                 .UpdateDivision(buildProData.BuildProSetting.Division)
                                 .UpdateStatus(buildProData.BuildProSetting.Status)
                                 .TestConnection();

            // Test connection and verify message
            _expectedMessage = "You are connected to https://uatxml.hyphensolutions.com/httpreceive.aspx.";
            _actualMessage = BuildProPage.Instance.GetLastestToastMessage(20);
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                BuildProPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail("The message is NOT dispaly as expected. Actual results: " + _actualMessage);
                BuildProPage.Instance.CloseToastMessage();
                Assert.Fail("The message is NOT dispaly as expected. Actual results: " + _actualMessage);
            }

            // Turn of build pro
            ExtentReportsHelper.LogInformation(null, "PAUSE BuildPro on the setting");

            BuildProPage.Instance.UpdateStatus(false).Save();
            _actualMessage = BuildProPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
                BuildProPage.Instance.CloseToastMessage();

            // No item on comunity
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            Assert.That(CommunityPage.Instance.VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage());

            // NO item on Building Phase
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            Assert.That(BuildingPhasePage.Instance.VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage());

            // No item on vendor
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            Assert.That(VendorPage.Instance.VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage());

            // Go to the 1st job page and verify the item not display
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.GoToDetailPageOfThe1stJob();

            Assert.That(JobDetailPage.Instance.VerifyTheScheduleTemplateIsNOTDisplayedOnCurrentPage());

            // Go to View PO
            JobDetailPage.Instance.LeftMenuNavigation("View Purchase Orders");

            // No Item on view PO
            Assert.That(JobDetailPage.Instance.VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage());
            Assert.That(JobDetailPage.Instance.VerifyTheViewBuildProEPOButtonIsNOTDisplayedOnCurrentPage());

            // Go to Vendor change
            JobDetailPage.Instance.LeftMenuNavigation("Vendor Change Requests");

            // No item on vendor change
            Assert.That(JobDetailPage.Instance.VerifyTheSyncToBuildProButtonIsNOTDisplayedOnCurrentPage());

            // back to setting page 
            BuildProPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            BuildProPage.Instance.LeftMenuNavigation("BuildPro");

            // Turn on buildpro and save
            ExtentReportsHelper.LogInformation(null, "RUNNING BuildPro on the setting");
            BuildProPage.Instance.UpdateStatus(true).Save();
            _actualMessage = BuildProPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
                BuildProPage.Instance.CloseToastMessage();

            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            Assert.That(CommunityPage.Instance.VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage());

            // Item on Building Phase
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            Assert.That(BuildingPhasePage.Instance.VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage());

            // Item on vendor
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            Assert.That(VendorPage.Instance.VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage());

            // Go to the 1st job page and verify the item not display
            JobPage.Instance.SelectMenu(MenuItems.JOBS).SelectItem(JobsMenu.AllJobs);
            JobPage.Instance.GoToDetailPageOfThe1stJob();

            Assert.That(JobDetailPage.Instance.VerifyTheScheduleTemplateIsDisplayedOnCurrentPage());

            // Go to View PO
            JobDetailPage.Instance.LeftMenuNavigation("View Purchase Orders");

            // Item on view PO
            Assert.That(JobDetailPage.Instance.VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage());
            Assert.That(JobDetailPage.Instance.VerifyTheViewBuildProEPOButtonIsDisplayedOnCurrentPage());

            // Go to Vendor change
            JobDetailPage.Instance.LeftMenuNavigation("Vendor Change Requests");

            // Item on vendor change
            Assert.That(JobDetailPage.Instance.VerifyTheSyncToBuildProButtonIsDisplayedOnCurrentPage());

        }

        private void IT02_Pipeline_BuildPro_Community()
        {
            // Go to the community
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            ExtentReportsHelper.LogInformation(null, "************************************Create new commmunity and Sync to BuildPro************************************");
            // Create new community
            CreateCommunity(communityCreated);

            SyncToBuildProAndVerifyCommunity(communityCreated);

            // go to build pro
            CommonHelper.OpenLinkInNewTab(buildProApplicationURL);
            CommonHelper.SwitchLastestTab();
            BasePage.PageLoad();

            VerifyCommunityOnBuildPro(communityCreated);

            // back to community and update name
            ExtentReportsHelper.LogInformation(null, "************************************Update the commmunity and Re-Sync to BuildPro************************************");
            CommonHelper.SwitchTab(0);
            CommonHelper.OpenURL(dataCreatedURL);
            BasePage.PageLoad();

            UpdateCommunity(communityUpdated);

            SyncToBuildProAndVerifyCommunity(communityUpdated);

            // go to build pro
            CommonHelper.SwitchLastestTab();
            CommonHelper.RefreshPage();
            BasePage.PageLoad();

            VerifyCommunityOnBuildPro(communityUpdated);
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }

        private void IT02_Pipeline_BuildPro_BuildingPhase()
        {
            // go to build pro
            CommonHelper.OpenLinkInNewTab(buildProApplicationURL);
            CommonHelper.SwitchLastestTab();
            BasePage.PageLoad();

            // Log in if need
            BuildProPage.Instance.SignInToBuildProIfNeeded(buildProUsername, buildProPassword);

            // Navigate to Manger>Cost Code List in Buildpro to view current list
            CommonHelper.OpenURL("https://uat.hyphensolutions.com/Build/System/AcctsetList.asp");

            // Verify the Building Phase does not exist
            ClassicAssert.IsFalse(BuildProPage.Instance.VerifyTheBuildingPhaseExistedOnBuildPro(buildingPhaseCreated));

            // Back to Pipeline Builing phase
            CommonHelper.SwitchTab(0);

            // Create new building phase
            ExtentReportsHelper.LogInformation(null, "************************************Create the Building Phase and Sync to BuildPro************************************");

            CreateBuildingPhase(buildingPhaseCreated);

            // Sync to build Pro
            BuildingPhasePage.Instance.SyncBuildingPhaseToBuildPro();
            string _actualMessage = BuildProPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                BuildProPage.Instance.CloseToastMessage();
            }

            // Verify the New building phase sync successfully
            Assert.That(BuildingPhasePage.Instance.VerifyTheBuildingPhaseSyncedSuccessfully(buildingPhaseCreated));

            // Go back to buildpro
            CommonHelper.SwitchLastestTab();
            CommonHelper.RefreshPage();

            // Verify
            Assert.That(BuildProPage.Instance.VerifyTheBuildingPhaseExistedOnBuildPro(buildingPhaseCreated));

            // Back to pipeline and edit
            ExtentReportsHelper.LogInformation(null, "************************************Update the Building Phase and Re-Sync to BuildPro************************************");
            CommonHelper.SwitchTab(0);
            CommonHelper.RefreshPage();
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.EqualTo, buildingPhaseCreated.Code);
            BuildingPhasePage.Instance.ClickItemInGrid("Code", buildingPhaseCreated.Code);
            UpdateBuildingPhase(buildingPhaseUpdated);

            // Re-sync again
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.SyncBuildingPhaseToBuildPro();
            _actualMessage = BuildProPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                BuildProPage.Instance.CloseToastMessage();
            }

            // Verify the New building phase sync successfully
            Assert.That(BuildingPhasePage.Instance.VerifyTheBuildingPhaseSyncedSuccessfully(buildingPhaseUpdated));

            // Go back to buildpro
            CommonHelper.SwitchLastestTab();
            CommonHelper.RefreshPage();

            // Verify
            Assert.That(BuildProPage.Instance.VerifyTheBuildingPhaseExistedOnBuildPro(buildingPhaseUpdated));
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);
        }

        private void IT02_Pipeline_BuildPro_Vendor()
        {
            // go to build pro
            CommonHelper.OpenLinkInNewTab(buildProApplicationURL);
            CommonHelper.SwitchLastestTab();
            BasePage.PageLoad();
            // Log in if need
            BuildProPage.Instance.SignInToBuildProIfNeeded(buildProUsername, buildProPassword);

            // Navigate to Suplier
            CommonHelper.OpenURL("https://uat.hyphensolutions.com/Build/Supply/Supplier/OrgSupplierRelationship_2.aspx");

            // Verify the Building Phase does not exist
            BuildProPage.Instance.VerifyTheVendorExistedOnBuildPro(vendorCreated);

            // Back to Pipeline vendor
            CommonHelper.SwitchTab(0);

            // Create new vendor
            ExtentReportsHelper.LogInformation(null, "************************************Create Vendor and Sync to BuildPro************************************");

            CreateTheVendor(vendorCreated);
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);

            // Sync to build Pro
            VendorPage.Instance.SyncVendorToBuildPro();
            string _actualMessage = VendorPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                VendorPage.Instance.CloseToastMessage();
            }

            // Verify the New building phase sync successfully
            Assert.That(VendorPage.Instance.VerifyTheVendorSyncedSuccessfully(vendorCreated));

            // Go back to buildpro
            CommonHelper.SwitchLastestTab();
            CommonHelper.RefreshPage();

            // Verify
            Assert.That(BuildProPage.Instance.VerifyTheVendorExistedOnBuildPro(vendorCreated));

            // Back to pipeline and edit
            ExtentReportsHelper.LogInformation(null, "************************************Update Vendor and Re-Sync to BuildPro************************************");

            CommonHelper.SwitchTab(0);
            CommonHelper.OpenURL(dataCreatedURL);
            UpdateTheVendor(vendorUpdated);

            // Re-sync again
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.SyncVendorToBuildPro();
            _actualMessage = VendorPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                VendorPage.Instance.CloseToastMessage();
            }

            // Verify the update vendor sync successfully
            Assert.That(VendorPage.Instance.VerifyTheVendorSyncedSuccessfully(vendorUpdated));

            // Go back to buildpro
            CommonHelper.SwitchLastestTab();
            CommonHelper.RefreshPage();

            // Verify
            Assert.That(BuildProPage.Instance.VerifyTheVendorExistedOnBuildPro(vendorUpdated));
            CommonHelper.CloseCurrentTab();
            CommonHelper.SwitchTab(0);

        }

        private void IT02_Pipeline_BuildPro_Job()
        {
            // Back to pipeline and open the job
            CommonHelper.OpenURL("http://dev.bimpipeline.com/Dashboard/Jobs/Job.aspx?jid=1119".ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain")));

            JobDetailPage.Instance.SelectComunity("HN-hai nguyen community");
            JobDetailPage.Instance.SelectHouse("0011-hai nguyen house");
            JobDetailPage.Instance.SelectLot("001A");
            JobDetailPage.Instance.Save();
            string _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                if (_actualMessage.Equals("Job hn-02 saved successfully!"))
                    ExtentReportsHelper.LogPass(_actualMessage);
                else
                    ExtentReportsHelper.LogInformation(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
            }

            // go to quantities page and delete all quantities
            JobDetailPage.Instance.LeftMenuNavigation("Quantities");
            JobDetailPage.Instance.DeleteAllQuantities();

            // import
            JobDetailPage.Instance.LeftMenuNavigation("Import");
            JobDetailPage.Instance.DeleteAllOldFile();
            JobDetailPage.Instance.ImportJobQuantities(xmlImportFile, false);
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
            }

            JobDetailPage.Instance.ProcessedImportFile().FinishImportFile();
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                if (_actualMessage.Equals("The quantities imported successfully from the file!"))
                    ExtentReportsHelper.LogPass(_actualMessage);
                else
                    ExtentReportsHelper.LogInformation(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
            }
            JobDetailPage.Instance.DeleteAllOldFile();

            // bom & estimate
            JobDetailPage.Instance.LeftMenuNavigation("Estimate");
            JobDetailPage.Instance.GenerateBomAndEstimate();

            // Create PO
            CreatePOAndSyncAndVerify("_001", "HN-Pro-01", "Hai Nguyen vender");

            //********************************************* TBD ***************************************//
            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>Sync a PO with TBD Vendor</b></font>************************************");

            CommonHelper.SwitchTab(0);
            // Verify TBD vendor
            CommonHelper.OpenLinkInNewTab("http://dev.bimpipeline.com/Dashboard/Costing/Settings/Default.aspx".ToString().Replace("dev", ConfigurationManager.GetValue("ApplicationDomain")));
            CommonHelper.SwitchLastestTab();
            BasePage.PageLoad();
            CostingPage.Instance.AppendTBDCode("_001").Save();
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
            }
            CommonHelper.CloseCurrentTab();
            // Back to Pipeline vendor
            CommonHelper.SwitchTab(0);
            CreatePOAndSyncAndVerify("_002", "HN-Pro-02", "HN-QA-Vender");

            // Call API
            CommonHelper.SwitchTab(0);

            // create po
            JobDetailPage.Instance.LeftMenuNavigation("Create Purchase Orders");
            JobDetailPage.Instance.ExpandCreatePO();
            JobDetailPage.Instance.SelectPOByBuildingPhaseAndProduct("_003", "HN-Pro-03").ProcessCreatePO();
            System.Threading.Thread.Sleep(500);
            JobDetailPage.Instance.ExpandCreatePO();
            ExtentReportsHelper.LogInformation($"After create PO with Building Phase Code <font color='green'><b><i>_003</i></b></font> and Product Name <font color='green'><b><i>HN-Pro-03</i></b></font>");
            string createdDate = DateTime.Now.ToString("MM/dd/yyyy");

            // sync to build pro
            JobDetailPage.Instance.LeftMenuNavigation("View Purchase Orders");
            string POName = JobDetailPage.Instance.ExpandAllPOOnViewPOPage()
                                     .SortDateCreatedAsc("_003")
                                     .SelectPOOnViewPOPage("_003", "Created", createdDate, ConfigurationManager.GetValue("UserName"));

            // Vendor change
            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>Create Vendor Change</b></font>************************************");

            int.TryParse(POName.Replace("hn-02", string.Empty), out int vendorChangePONumber);
            VendorOnTheFly(POName.Replace("hn-02", string.Empty));

            // Change vendor
            CommonHelper.SwitchTab(0);
            JobDetailPage.Instance.LeftMenuNavigation("Vendor Change Requests");
            JobDetailPage.Instance.ChangeVendor(POName, "Hai Nguyen vender");
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
            }

            JobDetailPage.Instance.SelectAndProcessVendorChange(POName);
            string expected = "Successfully processed selected purchase order vendor change(s)!";
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (!expected.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogFail(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
                Assert.Fail();
            }
            else
            {
                ExtentReportsHelper.LogPass(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
            }

            JobDetailPage.Instance.SyncVendorChangeToBuildPro(POName);
            _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogPass(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
            }

            JobDetailPage.Instance.IsPOSyncToBuildProSuccessfully(POName, "_003");
            JobDetailPage.Instance.RefreshPage();
            // sync to Build Pro
            VerifyPOOnBuildPro(POName, "Hai Nguyen vender");

            // EPO
            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>Create EPO</b></font>************************************");

            CommonHelper.SwitchTab(0);
            EPO((vendorChangePONumber + 1).ToString());
            JobDetailPage.Instance.LeftMenuNavigation("View Purchase Orders");
            JobDetailPage.Instance.ExpandAllPOOnViewPOPage();
            Assert.That(JobDetailPage.Instance.IsPOOnViewPOPage("hn-02" + (vendorChangePONumber + 1), "Created", createdDate, "EPO"));
        }

        private void VendorOnTheFly(string pONumber)
        {
            string domain = "http://beta.bimpipeline.com:6510";
            if (ConfigurationManager.GetValue("ApplicationDomain").Contains("dev"))
                domain = "https://api-pipeline-dev.sstsandbox.com";
            string endpointURL = domain + "/buildpro/externalevent/invoke";

            var client = new RestClient(endpointURL)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/xml");
            string requestBody = $"<?xml version=\"1.0\"?>\r\n<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:xsd=\"http://www.w3.org/1999/XMLSchema\" xmlns:xsi=\"http://www.w3.org/1999/XMLSchema-instance\" SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n    <SOAP-ENV:Header xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n        <MH2SoapHeader>\r\n            <Authentication>\r\n                <UserName>webuser</UserName>\r\n                <Password>webuser</Password>\r\n            </Authentication>\r\n        </MH2SoapHeader>\r\n    </SOAP-ENV:Header>\r\n    <SOAP-ENV:Body>\r\n        <JDEOW_VendorOnTheFly>\r\n            <VendorChange>\r\n                <DocCompanyNumber>45F</DocCompanyNumber>\r\n                <DocNumber>{pONumber}</DocNumber>\r\n                <DocType>TO</DocType>\r\n                <DocSuffix>000</DocSuffix>\r\n                <OldVendorAddress>\r\n                    <AddressBookEntry>\r\n                        <AddressBookNumber>101</AddressBookNumber>\r\n                    </AddressBookEntry>\r\n                </OldVendorAddress>\r\n                <NewVendorAddress>\r\n                    <AddressBookEntry>\r\n                        <AddressBookNumber>_001</AddressBookNumber>\r\n                    </AddressBookEntry>\r\n                </NewVendorAddress>\r\n                <UserID>0</UserID>\r\n                <UserName>carvermcchangey</UserName>\r\n                <DateSent>11/14/2018</DateSent>\r\n            </VendorChange>\r\n        </JDEOW_VendorOnTheFly>\r\n    </SOAP-ENV:Body>\r\n</SOAP-ENV:Envelope>";
            request.AddParameter("application/xml", requestBody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
        }

        private void EPO(string PONumber)
        {
            string domain = "http://beta.bimpipeline.com:6510";
            if (ConfigurationManager.GetValue("ApplicationDomain").Contains("dev"))
                domain = "https://api-pipeline-dev.sstsandbox.com";
            string endpoint = domain + "/buildpro/externalevent/Invoke";

            var client = new RestClient(endpoint)
            {
                Timeout = -1
            };
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/xml");
            string requestBody = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n<SOAP-ENV:Envelope xmlns:SOAP-ENV=\"http://schemas.xmlsoap.org/soap/envelope/\" xmlns:SOAP-ENC=\"http://schemas.xmlsoap.org/soap/encoding/\" xmlns:xsd=\"http://www.w3.org/1999/XMLSchema\" xmlns:xsi=\"http://www.w3.org/1999/XMLSchema-instance\" SOAP-ENV:encodingStyle=\"http://schemas.xmlsoap.org/soap/encoding/\">\r\n    <SOAP-ENV:Header>\r\n        <MH2SoapHeader>\r\n            <Authentication>\r\n                <UserName>webuser</UserName>\r\n                <Password>webuser</Password>\r\n            </Authentication>\r\n        </MH2SoapHeader>\r\n    </SOAP-ENV:Header>\r\n    <SOAP-ENV:Body>\r\n        <JDEOW_EPO>\r\n            <EPOItem>\r\n                <LotJob>hn-02</LotJob>\r\n                <CommunityNumber>hn</CommunityNumber>\r\n                <LotNumber>001A</LotNumber>\r\n                <CostType />\r\n                <CostCode>_003</CostCode>\r\n                <Vendor>\r\n                    <AddressBookEntry>\r\n                        <AddressBookNumber>101</AddressBookNumber>\r\n                    </AddressBookEntry>\r\n                </Vendor>\r\n                <ShortItemNumber>0001</ShortItemNumber>\r\n                <ItemDescription />\r\n                <SequenceNumber>1</SequenceNumber>\r\n                <BuildProDocument>\r\n                    <Document>\r\n                        <DocCompanyNumber />\r\n                        <DocNumber>{PONumber}</DocNumber>\r\n                        <DocType>UA</DocType>\r\n                        <DocSuffix />\r\n                        <DocLineNumber>1</DocLineNumber>\r\n                    </Document>\r\n                </BuildProDocument>\r\n                <Quantity>3.40</Quantity>\r\n                <UnitOfMeasure>EA</UnitOfMeasure>\r\n                <UnitPrice>1.00</UnitPrice>\r\n                <ExtendedPrice />\r\n                <Tax>0.00</Tax>\r\n                <Taxable />\r\n                <TaxRateAreaCode />\r\n                <TaxExplanationCode />\r\n                <UserID>1978</UserID>\r\n                <UserName>snishtala</UserName>\r\n                <DateChanged>02/04/2016</DateChanged>\r\n                <PromisedDeliveryDate>05/21/2020</PromisedDeliveryDate>\r\n                <AutopayEligible>N</AutopayEligible>\r\n                <ReasonCode>V10</ReasonCode>\r\n                <NTEIndicator>N</NTEIndicator>\r\n                <OptionNumber />\r\n                <LongItemNumber>03HN_Product</LongItemNumber>\r\n                <EPONote>No Notes Given</EPONote>\r\n            </EPOItem>\r\n        </JDEOW_EPO>\r\n    </SOAP-ENV:Body>\r\n</SOAP-ENV:Envelope>";
            request.AddParameter("application/xml", requestBody, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var results = response.Content;
            ExtentReportsHelper.LogInformation(null, "The result after sent an API:<br>Status:" + response.StatusCode + "<br>Content:" + results);
        }

        private void CreatePOAndSyncAndVerify(string buildingPhaseCode, string productName, string vendorName)
        {
            // create po
            JobDetailPage.Instance.LeftMenuNavigation("Create Purchase Orders");
            JobDetailPage.Instance.ExpandCreatePO();
            JobDetailPage.Instance.SelectPOByBuildingPhaseAndProduct(buildingPhaseCode, productName).ProcessCreatePO();
            System.Threading.Thread.Sleep(500);
            JobDetailPage.Instance.ExpandCreatePO();
            ExtentReportsHelper.LogInformation($"After create PO with Building Phase Code <font color='green'><b><i>{buildingPhaseCode}</i></b></font> and Product Name <font color='green'><b><i>{productName}</i></b></font>");
            string createdDate = DateTime.Now.ToString("MM/dd/yyyy");

            // sync to build pro
            JobDetailPage.Instance.LeftMenuNavigation("View Purchase Orders");
            string POName = JobDetailPage.Instance.ExpandAllPOOnViewPOPage()
                                     .SortDateCreatedAsc(buildingPhaseCode)
                                     .SelectPOOnViewPOPage(buildingPhaseCode, "Created", createdDate, ConfigurationManager.GetValue("UserName"));

            // Sync to BuildPro
            JobDetailPage.Instance.SyncPO();
            string _actualMessage = JobDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                JobDetailPage.Instance.CloseToastMessage();
            }
            JobDetailPage.Instance.IsPOSyncToBuildProSuccessfully(POName, buildingPhaseCode);
            JobDetailPage.Instance.RefreshPage();
            JobDetailPage.Instance.ExpandAllPOOnViewPOPage().VerifyPOOnViewPOPageWithStatus(POName, "Sent to Scheduling", createdDate);

            // go to build pro and verify
            VerifyPOOnBuildPro(POName, vendorName);
        }

        private void VerifyPOOnBuildPro(string POName, string vendorName)
        {
            // go to build pro
            CommonHelper.OpenLinkInNewTab(buildProApplicationURL);
            CommonHelper.SwitchLastestTab();
            BasePage.PageLoad();
            // Log in if need
            BuildProPage.Instance.SignInToBuildProIfNeeded(buildProUsername, buildProPassword);

            // Go to Job Page
            CommonHelper.OpenURL("https://uat.hyphensolutions.com/Build/Jobs/JME3.aspx?job_id=-1");
            BuildProPage.Instance.FilterAndShowTask("hn-02").VerifyTheJobSyncToBuildPro(POName.Replace("hn-02", string.Empty), vendorName);
            CommonHelper.CloseCurrentTab();
        }

        private void SyncToBuildProAndVerifyCommunity(CommunityData data)
        {
            // Back to community
            if (!CommunityPage.Instance.CurrentURL.EndsWith("Communities/Default.aspx"))
                CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Sync community to buildpro
            CommunityPage.Instance.SyncCommunityToBuildPro();

            // verify message
            string _actualMessage = BuildProPage.Instance.GetLastestToastMessage();
            ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
            BuildProPage.Instance.CloseToastMessage();

            // Verify The Community Sync To BuildPro Successfully
            ClassicAssert.True(BuildProPage.Instance.VerifyTheCommunitySyncToBuildProSuccessfully(data.Name));
        }

        private void VerifyCommunityOnBuildPro(CommunityData data)
        {
            // log in and verify
            BuildProPage.Instance.SignInToBuildProIfNeeded(buildProUsername, buildProPassword);

            // Navigate to Job admin page
            CommonHelper.OpenURL("https://uat.hyphensolutions.com/Build/Subdivs/Default.asp");
            BasePage.PageLoad();

            // Verify the community sync successfully
            Assert.That(BuildProPage.Instance.VerifyCommunitySyncedSuccessfully(data));

            // Go to the Org list page
            CommonHelper.OpenURL("https://uat.hyphensolutions.com/Build/System/Orgs/OrgList.asp");
            BasePage.PageLoad();

            // Verify the community sync successfully
            Assert.That(BuildProPage.Instance.VerifyCommunitySyncedSuccessfullyOnOrgListPage(data));
        }

        private void UpdateCommunity(CommunityData _community)
        {
            // Create community - Click 'Save' Button
            CommunityDetailPage.Instance.AddOrUpdateCommunity(_community, true);
            string _actualMessage = CommunityDetailPage.Instance.GetLastestToastMessage();
            string _expectedMessage = $"Community {_community.Name} saved successfully!";
            if (_actualMessage.Equals(_expectedMessage))
            {
                ExtentReportsHelper.LogPass($"Updated Community with name { _community.Name} successfully.");
                CommunityDetailPage.Instance.CloseToastMessage();
            }
            else if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogFail($"Could not update Community with name { _community.Name}.");
                CommunityDetailPage.Instance.CloseToastMessage();
                Assert.Fail($"Could not Update Community with name { _community.Name}.");
            }

            // Step 4. Verify updated community in header
            Assert.That(CommunityDetailPage.Instance.IsSaveCommunitySuccessful($"{_community.Name} ({_community.Code})"), "Create new Community unsuccessfully.");
            ExtentReportsHelper.LogPass($"Community is updated sucessfully with URL: {CommunityDetailPage.Instance.CurrentURL}");

            // Step 5. Verify data saved successfully
            Assert.That(CommunityDetailPage.Instance.IsSaveCommunityData(_community), "The Community updated with incorrect data.");
            ExtentReportsHelper.LogPass($"Community is updated sucessfully with valid data");

        }

        // Create community
        private void CreateCommunity(CommunityData _community)
        {
            // Step 2: click on "+" Add button
            CommunityPage.Instance.GetItemOnHeader(DashboardContentItems.Add).Click();
            var _expectedCreateCommunityURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_COMMUNITY_URL;
            Assert.That(CommunityDetailPage.Instance.IsPageDisplayed(_expectedCreateCommunityURL), "Community create page isn't displayed");

            // Create Community - Click 'Save' Button
            CommunityDetailPage.Instance.AddOrUpdateCommunity(_community);

            string _expectedMessage = $"Could not create Community with name {_community.Name}.";
            if (CommunityDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
            {
                ExtentReportsHelper.LogFail($"Could not create Community with name { _community.Name}.");
                Assert.Fail($"Could not create Community with name { _community.Name}.");
            }

            // Step 4. Verify new House in header
            Assert.That(CommunityDetailPage.Instance.IsSaveCommunitySuccessful($"{_community.Name} ({_community.Code})"), "Create new Community unsuccessfully.");
            dataCreatedURL = CommunityDetailPage.Instance.CurrentURL;

            ExtentReportsHelper.LogPass($"Community is created sucessfully with URL: {dataCreatedURL}");
            // Step 5. Verify data saved successfully
            Assert.That(CommunityDetailPage.Instance.IsSaveCommunityData(_community), "New Community created with incorrect data.");
            ExtentReportsHelper.LogPass($"Community is create sucessfully with valid data");

            // Step 6. Back to list of Community and verify new item in grid view
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Insert name to filter and click filter by Contain value
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _community.Name);

            bool isFound = CommunityPage.Instance.IsItemInGrid("Name", _community.Name);
            Assert.That(isFound, string.Format($"New Community \"{_community.Name} \" was not display on grid."));

        }

        // create building phase
        private void CreateBuildingPhase(BuildingPhaseData data)
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Products/BuildingPhases/Default.aspx
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);

            // Step 2: click on "+" Add button
            BuildingPhasePage.Instance.ClickAddToBuildingPhaseModal();

            Assert.That(BuildingPhasePage.Instance.AddBuildingPhaseModal.IsModalDisplayed(), "Add Building Phase modal is not displayed.");

            // Step 3: Populate all values
            BuildingPhasePage.Instance.AddBuildingPhaseModal
                                      .EnterPhaseCode(data.Code)
                                      .EnterPhaseName(data.Name)
                                      .EnterAbbName(data.AbbName)
                                      .EnterDescription(data.Description);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectGroup(data.BuildingGroup);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectType(data.Type);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.SelectParent(data.Parent);
            BuildingPhasePage.Instance.AddBuildingPhaseModal.EnterPercentBilled(data.PercentBilled)
                                      .IsTaxable(data.Taxable);

            // 4. Select the 'Save' button on the modal;
            BuildingPhasePage.Instance.AddBuildingPhaseModal.Save();

            // Verify successful save and appropriate success message.
            string _expectedMessage = $"Building Phase {data.Code} {data.Name} added successfully!";
            string _actualMessage = BuildingPhasePage.Instance.AddBuildingPhaseModal.GetLastestToastMessage();
            if (_expectedMessage == _actualMessage)
            {
                ExtentReportsHelper.LogPass("The message is dispalyed as expected. Actual results: " + _actualMessage);
                BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseToastMessage();
            }
            // Verify the modal is displayed with default value ()
            if (!BuildingPhasePage.Instance.AddBuildingPhaseModal.IsDefaultValues)
                ExtentReportsHelper.LogWarning("The modal of Add Building Phase is not displayed with default values.");

            // Close modal
            BuildingPhasePage.Instance.AddBuildingPhaseModal.CloseModal();
        }

        private void UpdateBuildingPhase(BuildingPhaseData data)
        {
            BuildingPhaseDetailPage.Instance.EnterAbbName(data.AbbName).Save();
            string actualMsg = BuildingPhaseDetailPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(actualMsg))
            {
                ExtentReportsHelper.LogInformation(actualMsg);
                BuildingPhaseDetailPage.Instance.CloseToastMessage();
            }
        }

        // Create the vendor
        private void CreateTheVendor(VendorData data)
        {
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);

            // Step 2: click on "+" Add button
            VendorPage.Instance.ClickAddToVendorIcon();
            string expectedURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_VENDOR_URL;
            Assert.That(VendorDetailPage.Instance.IsPageDisplayed(expectedURL), "Vendor detail page isn't displayed");

            // 4. Select the 'Save' button on the modal;
            VendorDetailPage.Instance.CreateOrUpdateAVendor(data);

            // 5. Verify new Vendor in header
            ExtentReportsHelper.LogPass("Create successful Vendor");
            dataCreatedURL = VendorDetailPage.Instance.CurrentURL;
        }

        private void UpdateTheVendor(VendorData data)
        {
            // 4. Select the 'Save' button on the modal;
            VendorDetailPage.Instance.CreateOrUpdateAVendor(data);
            string _actualMessage = VendorPage.Instance.GetLastestToastMessage();
            if (!string.IsNullOrEmpty(_actualMessage))
            {
                ExtentReportsHelper.LogInformation(_actualMessage);
                VendorPage.Instance.CloseToastMessage();
            }
            VendorPage.Instance.RefreshPage();
            // 5. Verify new Vendor in header
            Assert.That(VendorDetailPage.Instance.IsCreateSuccessfully(data), "Create new Vendor unsuccessfully");
            ExtentReportsHelper.LogPass("Create successful Vendor");
            dataCreatedURL = VendorDetailPage.Instance.CurrentURL;
        }

        private void UpdateRandomQuantities()
        {
            var xdoc = XDocument.Load(xmlImportFile);
            var products = xdoc.Root.Elements()
                                    .Where(p => p.Name.LocalName == "customitems")
                                    .FirstOrDefault();

            var productQuantites = products.Descendants().Where(p => p.Name.LocalName == "product_qnt").ToList();
            Random qtt = new Random();
            foreach (var item in productQuantites)
            {
                string oldvalue = item.Value;
                string newValue = qtt.Next(1, 10).ToString();
                if (oldvalue.Equals(newValue))
                    newValue = qtt.Next(11, 20).ToString();
                item.SetValue(newValue);
            }
            xdoc.Save(xmlImportFile);
        }

        [TearDown]
        public void DeleteAllItem()
        {
            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>Clean Up Data</b></font>************************************");
            // Delete Community
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityUpdated.Name);

            bool isFound = CommunityPage.Instance.IsItemInGrid("Name", communityUpdated.Name);
            if (isFound)
            {
                CommunityPage.Instance.DeleteItemInGrid("Name", communityUpdated.Name);
                string actual = CommunityPage.Instance.GetLastestToastMessage();
                if (!string.IsNullOrEmpty(actual))
                    ExtentReportsHelper.LogPass(actual);
                CommunityPage.Instance.CloseToastMessage();
            }

            // Delete Building Phase
            BuildingPhasePage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.BuildingPhases);
            BuildingPhasePage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, buildingPhaseUpdated.Code);
            isFound = BuildingPhasePage.Instance.IsItemInGrid("Code", buildingPhaseUpdated.Code);
            if (isFound)
            {
                BuildingPhasePage.Instance.DeleteItemInGrid("Code", buildingPhaseUpdated.Code);
                string actual = BuildingPhasePage.Instance.GetLastestToastMessage();
                if (!string.IsNullOrEmpty(actual))
                    ExtentReportsHelper.LogPass(actual);
                BuildingPhasePage.Instance.CloseToastMessage();
            }

            // Delete vendor
            VendorPage.Instance.SelectMenu(MenuItems.COSTING).SelectItem(CostingMenu.Vendors);
            VendorPage.Instance.EnterVendorNameToFilter("Name", vendorUpdated.Name);
            isFound = VendorPage.Instance.IsItemInGrid("Name", vendorUpdated.Name);
            if (isFound)
            {
                VendorPage.Instance.DeleteItemInGrid("Name", vendorUpdated.Name);
                string actual = VendorPage.Instance.GetLastestToastMessage();
                if (!string.IsNullOrEmpty(actual))
                    ExtentReportsHelper.LogPass(actual);
                VendorPage.Instance.CloseToastMessage();
            }
            ExtentReportsHelper.LogInformation(null, "************************************<font color='green'><b>End of Clean Up Data</b></font>************************************");
        }

    }
}
