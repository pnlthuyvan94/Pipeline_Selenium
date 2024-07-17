using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Import;
using Pipeline.Testing.Pages.Settings.MainSetting;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A05_A_RT_01218 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        OptionData _optionNew;
        OptionData _optionOld;
        private static readonly string IMPORT_FOLDER = "\\DataInputFiles\\Import\\RT_01218";



        [SetUp]
        public void GettingData()
        {
            // Get old data
            _optionOld = new OptionData()
            {
                Name = "QA_RT_Auto_Option_RT_01218",
                Number = "01218"
            };

            // Get option name to update
            var optionType = new List<bool>()
            {
                false,false,false
            };
            _optionNew = new OptionData()
            {
                Name = "QA_RT_Auto_Option_RT_01218_Update",
                Number = "01219",
                SquareFootage = double.Parse("10000"),
                Description = "Regression Test Create Option update",
                SaleDescription = "Create Option Sale Description update",
                OptionGroup = "NONE",
                OptionRoom = "NONE",
                CostGroup = "All Rooms",
                OptionType = "NONE",
                Price = double.Parse("100.00"),
                Types = optionType
            };

            /****************************************** Setting *******************************************/

            // Update setting with : TransferSeparationCharacter, SetSage300AndNAV, Group by Parameter
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 0.1: Open setting page, Make sure current transfer seperation character is ','<b></b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            string seperationCharacter = ','.ToString();
            MainSettingPage.Instance.SetTransferSeparationCharactertatus(seperationCharacter);

            /****************************************** Import data *******************************************/

            // Step 0.4: Import Communities.
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 0.4: Import Communities.</b></font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.BUILDER_IMPORT_URL_VIEW_OPTION);
            if (!BuilderImportPage.Instance.IsImportGridDisplay(ImportGridTitle.OPTION_IMPORT))
                ExtentReportsHelper.LogFail($"<font color ='red'>Can't find {ImportGridTitle.OPTION_IMPORT} grid view to import new Communities.</font>");

            string importFile = $@"{IMPORT_FOLDER}\Pipeline_Options.csv";
            BuilderImportPage.Instance.ImportValidData(ImportGridTitle.OPTION_IMPORT, importFile);

            /*************************************************************************************************/

            // Go to Option default page
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);

            // Filter and delete if option has the same name and number with _optionNew
            OptionPage.Instance.DeleteOption(_optionNew.Name);

            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _optionOld.Name);
            if (!OptionPage.Instance.IsItemInGrid("Name", _optionOld.Name))
            {
                // Can't find community in the import file
                Assert.Ignore($"Can't find Option '{_optionOld.Name}' in the import file to continue testing. Recheck the import function. Stop this test script");
            }
            else
            {
                //Select Option with Name
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _optionOld.Name);
            }
        }

        [Test]
        [Category("Section_IV")]
        public void A05_A_Assets_DetailPage_Options_Details()
        {
            // Option Details
            ExtentReportsHelper.LogInformation(null, "========== Update option details page ========");
            OptionData optionUpdate_new = OptionDetailPage.Instance.UpdateOption(_optionNew);
            var expectedMsg = $"Option {_optionNew.Name} saved successfully!";
            if (expectedMsg.Equals(OptionDetailPage.Instance.GetLastestToastMessage()))
            {
                ExtentReportsHelper.LogPass();
                OptionDetailPage.Instance.CloseToastMessage();
            }
            OptionDetailPage.Instance.RefreshPage();

            //Verify data
            OptionDetailPage.Instance.IsSaveOptionData(optionUpdate_new);

        }
        [TearDown]
        public void CleanUpData()
        {
            //Delete Data
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            // 7. Select item and click deletete icon
            OptionPage.Instance.DeleteOption(_optionNew.Name);
            OptionPage.Instance.DeleteOption(_optionOld.Name);
        }
    }
}
