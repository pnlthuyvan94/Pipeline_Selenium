using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Settings.MSNAV;
using Pipeline.Testing.Pages.Settings.Sage300CRE;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Integrations
{
    class Integrations_PIPE_33250 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Integrations);
        }
        private bool status = true;
        private Sage300CREPageData _sage300CREPageData1;
        private Sage300CREPageData _sage300CREPageData2;
        private Sage300CREPageData _sage300CREPageData3;
        private Sage300CREPageData _sage300CREPageData4;
        private Sage300CREPageData _sage300CREPageData5;
        private Sage300CREPageData _sage300CREPageData6;

        private const string Configure_Job_Number_Mask_Label = "Job Number Mask Breakdown";

        [SetUp] // Pre-condition
        public void GetTestData()
        {
            _sage300CREPageData1 = new Sage300CREPageData()
            {
                Section = 3,
                Character1 = 2,
                Character2 = 2,
                Character3 = 4,
                Connection1 = "Community Code (C)",
                Connection2 = "Filler (X)",
                Connection3 = "Lot Number (L)"
            };
            _sage300CREPageData2 = new Sage300CREPageData()
            {
                Section = 2,
                Character1 = 2,
                Character2 = 2,
                Connection1 = "Community Code (C)",
                Connection2 = "Filler (X)"
            };

            _sage300CREPageData3 = new Sage300CREPageData()
            {
                Section = 2,
                Character1 = 2,
                Character2 = 2,
                Connection1 = "Select Connection",
                Connection2 = "Select Connection"
            };

            _sage300CREPageData4 = new Sage300CREPageData()
            {
                Section = 2,
                Character1 = 2,
                Character2 = 2,
                Connection1 = "Community Code (C)",
                Connection2 = "Lot Number (L)"
            };

            _sage300CREPageData5 = new Sage300CREPageData()
            {
                Section = 3,
                Character1 = 2,
                Character2 = 2,
                Character3 = 1,
                Connection1 = "Community Code (C)",
                Connection2 = "Filler (X)",
                Connection3 = "Select Connection"
            };
            _sage300CREPageData6 = new Sage300CREPageData()
            {
                Section = 3,
                Character1 = 2,
                Character2 = 2,
                Character3 = 1,
                Connection1 = "Community Code (C)",
                Connection2 = "Filler (X)",
                Connection3 = "Lot Number (L)"
            };
        }
        [Test]
        [Category("Integrations")]
        public void Pipeline_Integrations_Sage_Job_Number_Mask()
        {
            //Step 1: Access https://pipeline-dev45.sstsandbox.com/develop/Dashboard/default.aspx using your credentials.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 1: Access https://pipeline-dev45.sstsandbox.com/develop/Dashboard/default.aspx using your credentials.</b></font>");
            // Open Setting page
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            //Step 2: From the Dashboard, click the User Menu > Settings.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 2: From the Dashboard, click the User Menu > Settings.</b></font>");
            // Set Sage 300 status
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Open Sage 300 CRE on the left navigation and verify it.</b></font>");

            //Step 3: Select Sage300CRE settings from the left navigation.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 3: Select Sage300CRE settings from the left navigation.</b>");
            Sage300CREPage.Instance.LeftMenuNavigation("Sage300CRE");

            //Step 4:In Sage300CRE settings page, click the Configure button besides the Job Number Mask
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 4:In Sage300CRE settings page, click the Configure button besides the Job Number Mask.</b></font>");
            Sage300CREPage.Instance.SetSage300Status(status);

            //Step 5: Verify if Configure Job Number Mask modal UI all items.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 5: Verify if Configure Job Number Mask modal UI all items.</b></font>");
            //Create New Config Job Number
            ExtentReportsHelper.LogInformation("<b><font color='lavender'> Create New Config Job Number.</font>");
            Sage300CREPage.Instance.CreateConfigJobNumberMaskModal(_sage300CREPageData1);
            //Verify New Config Is Correct 
            Sage300CREPage.Instance.VerifyJobNumberMaskPreview(_sage300CREPageData1);
            Sage300CREPage.Instance.SaveConfigure();
            string actual_SaveConfigure1 = Sage300CREPage.Instance.GetLastestToastMessage(4);
            string expected_SaveConfigure1 = "Changes saved!";
            if (actual_SaveConfigure1.Equals(expected_SaveConfigure1) is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b>Save Configure is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Save Configure is unsuccessfully.</font>");
            }
            Sage300CREPage.Instance.CloseConfigureModal();

            //Step 6 : Hover mouse over information icon.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 6 : Hover mouse over information icon.</b></font>");
            Sage300CREPage.Instance.OpenConfigJobNumberMaskModal();
            Sage300CREPage.Instance.VerifyJobNumberMaskBreakdown(Configure_Job_Number_Mask_Label);
            Sage300CREPage.Instance.CloseConfigureModal();


            //Step 7 : Click Number of Sections and select 2.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 7 : Hover mouse over information icon.</b></font>");
            //Create New Config Job Number
            ExtentReportsHelper.LogInformation("<b><font color='lavender'> Create New Config Job Number.</font>");
            Sage300CREPage.Instance.CreateConfigJobNumberMaskModal(_sage300CREPageData2);
            //Verify New Config Is Correct 
            ExtentReportsHelper.LogInformation("<b><font color='lavender'>Verify New Config Is Correct.</font>");
            Sage300CREPage.Instance.VerifyJobNumberMaskPreview(_sage300CREPageData2);

            //Step 8 : Don’t make a Section Connection and click save. 
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 8 : Don’t make a Section Connection and click save.</b></font>");
            //Create New Config Job Number
            ExtentReportsHelper.LogInformation("<b><font color='lavender'> Create New Config Job Number.</font>");
            Sage300CREPage.Instance.CreateConfigJobNumberMaskModal(_sage300CREPageData3);
            Sage300CREPage.Instance.SaveConfigure();

            string actual_SaveConfigureWithoutConection = Sage300CREPage.Instance.GetLastestToastMessage();
            string expect_SaveConfigureWithoutConection = "Section connection is required. Please select a Connection.";

            if (actual_SaveConfigureWithoutConection.Equals(expect_SaveConfigureWithoutConection) is true)
            {
                ExtentReportsHelper.LogInformation($"<font color ='green'><b>Section connection is required. Please select a Connection.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogInformation($"<font color ='red'>The Toast Message with Text: {actual_SaveConfigureWithoutConection}.</font>");
            }

            Sage300CREPage.Instance.VerifyErrorConectionIsDisplayed("Change to Community Code or Lot", "Change to Community Code or Lot", string.Empty);
            
            //CloseConfigureModal
            Sage300CREPage.Instance.CloseToastMessage();
            Sage300CREPage.Instance.CloseConfigureModal();

            //Step 9: Make a Section Connection and click save.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 9: Make a Section Connection and click save.</b></font>");
            CommonHelper.RefreshPage();
            //Create New Config Job Number
            ExtentReportsHelper.LogInformation("<b><font color='lavender'> Create New Config Job Number.</font>");
            Sage300CREPage.Instance.CreateConfigJobNumberMaskModal(_sage300CREPageData4);
            //Verify New Config Is Correct 
            ExtentReportsHelper.LogInformation("<b><font color='lavender'>Verify New Config Is Correct.</font>");
            Sage300CREPage.Instance.VerifyJobNumberMaskPreview(_sage300CREPageData4);
            Sage300CREPage.Instance.SaveConfigure();

            string actual_SaveConfigure4 = Sage300CREPage.Instance.GetLastestToastMessage(4);
            string expected_SaveConfigure4 = "Changes saved!";
            if (actual_SaveConfigure4.Equals(expected_SaveConfigure4) is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b>Save Configure is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Save Configure is unsuccessfully.</font>");
            }
            Sage300CREPage.Instance.CloseToastMessage();
            Sage300CREPage.Instance.CloseConfigureModal();
            CommonHelper.RefreshPage();
            Sage300CREPage.Instance.VerifyJobNumberMask("CC-LL");

            //Step 10: Click Number of Sections and select 3
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 10: Click Number of Sections and select 3.</b></font>");


            //Step 11: Don’t make a Section Connection and click save.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 11: Don’t make a Section Connection and click save.</b>");
            //Create New Config Job Number
            ExtentReportsHelper.LogInformation("<b><font color='lavender'> Create New Config Job Number.</font>");
            Sage300CREPage.Instance.CreateConfigJobNumberMaskModal(_sage300CREPageData5);
            //Verify New Config Is Correct 
            ExtentReportsHelper.LogInformation("<b><font color='lavender'>Verify New Config Is Correct.</font>");
            Sage300CREPage.Instance.VerifyJobNumberMaskPreview(_sage300CREPageData5);
            Sage300CREPage.Instance.SaveConfigure();
            string actual_SaveConfigureWithoutConection1 = Sage300CREPage.Instance.GetLastestToastMessage();
            string expect_SaveConfigureWithoutConection1 = "Section connection is required. Please select a Connection.";

            if (actual_SaveConfigureWithoutConection1.Equals(expect_SaveConfigureWithoutConection1) is true)
            {
                ExtentReportsHelper.LogInformation($"<font color ='green'><b>Section connection is required. Please select a Connection.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogInformation($"<font color ='red'>The Toast Message with Text: {actual_SaveConfigureWithoutConection1}.</font>");
            }
            Sage300CREPage.Instance.VerifyErrorConectionIsDisplayed(string.Empty, string.Empty, "Please select a Connection");
            Sage300CREPage.Instance.CloseToastMessage();
            Sage300CREPage.Instance.CloseConfigureModal();

            //Step 12 : Make a Section Connection and click save.
            ExtentReportsHelper.LogInformation("<b><font color='lavender'><b>Step 12: Make a Section Connection and click save.</b></font>");
            //Refresh Page
            CommonHelper.RefreshPage();
            //Create New Config Job Number
            ExtentReportsHelper.LogInformation("<b><font color='lavender'> Create New Config Job Number.</font>");
            Sage300CREPage.Instance.CreateConfigJobNumberMaskModal(_sage300CREPageData6);
            //Verify New Config Is Correct 
            ExtentReportsHelper.LogInformation("<b><font color='lavender'>Verify New Config Is Correct.</font>");
            Sage300CREPage.Instance.VerifyJobNumberMaskPreview(_sage300CREPageData6);
            Sage300CREPage.Instance.SaveConfigure();
            string actual_SaveConfigure6 = Sage300CREPage.Instance.GetLastestToastMessage(4);
            string expected_SaveConfigure6 = "Changes saved!";
            if (actual_SaveConfigure6.Equals(expected_SaveConfigure6) is true)
            {
                ExtentReportsHelper.LogPass($"<font color ='green'><b>Save Configure is successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>Save Configure is unsuccessfully.</font>");
            }
            Sage300CREPage.Instance.CloseConfigureModal();
            //Refresh Page
            CommonHelper.RefreshPage();
            Sage300CREPage.Instance.VerifyJobNumberMask("CC-XX-L");       
        }

    }
}
