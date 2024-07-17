using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.UserMenu.Setting;
using System;

namespace Pipeline.Testing.Script.Section_IV
{
    class B09_A_PIPE_19177 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        readonly string DIVISIONS_NAME = "Divisions";
        readonly string COMMUNITIES_NAME = "Communities";
        readonly string HOUSES_NAME = "Houses";
        readonly string JOBS_NAME = "Jobs";
        readonly string OPTION_NAME = "Options";

        readonly string EXPORT_PRODUCTCONVERSIONS_TO_CSV = "Export Product Conversions CSV";
        readonly string EXPORT_PRODUCTCONVERSIONS_TO_EXCEL = "Export Product Conversions Excel";
        readonly string IMPORT_PRODUCTSCONVERSION_TO_SPECSETGROUP_PRODUCTSCONVERSIONS = "Spec Sets Product Import";
        readonly string IMPORT = "Import";

        private SpecSetData specSetData;
        private SpecSetData productConversions;
        private SpecSetData styleConversions;
        private SpecSetData newProductConversions;
        private SpecSetData newStyleConversions;

        private static string NEWSPECSET_NAME_DEFAULT = "QA_RT_SpecSet1_Automation";

        private static string DIVISION_NAME_DEFAULT = "QA_RT_Divsion1_Automation";

        private static string OPTION_NAME_DEFAULT = "QA_RT_Option1_Automation";
        private static string OPTION_CODE_DEFAULT = "123";

        private readonly string COMMUNITY_CODE_DEFAULT = "456";
        private readonly string COMMUNITY_NAME_DEFAULT = "QA_RT_Community1_Automation";

        private readonly string HOUSE_CODE_DEFAULT = "456";
        private readonly string HOUSE_NAME_DEFAULT = "QA_RT_House1_Automation";

        private readonly string JOB_NAME_DEFAULT = "QA_RT_Job1_Automation";


        [SetUp]
        public void SetUpData()
        {
            specSetData = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_Automation",
                SpectSetName = "QA_RT_SpecSet_Automation",
            };
            productConversions = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup_Automation",
                SpectSetName = "QA_RT_SpecSet_Automation",
                OriginalPhase = "123-QA_RT_BuildingPhase1_Automation",
                OriginalCategory = "QA_RT_Category1_Automation",
                OriginalProduct = "QA_RT_Product1_Automation",
                OriginalProductStyle = "QA_RT_Style1_Automation",
                OriginalProductUse = "QA_RT_Use1_Automation",
                NewPhase = "124-QA_RT_BuildingPhase2_Automation",
                NewCategory = "QA_RT_Category2_Automation",
                NewProduct = "QA_RT_Product2_Automation",
                NewProductStyle = "QA_RT_Style2_Automation",
                NewProductUse = "QA_RT_Use2_Automation",
                ProductCalculation = "NONE"

            };
            styleConversions = new SpecSetData()
            {
                OriginalManufacture = "QA_RT_Manufacturer1_Automation",
                OriginalStyle = "QA_RT_Style1_Automation",
                OriginalUse = "QA_RT_Use1_Automation",
                NewManufacture = "QA_RT_Manufacturer2_Automation",
                NewStyle = "QA_RT_Style2_Automation",
                NewUse = "QA_RT_Use2_Automation",
                StyleCalculation = "NONE"
            };
            newProductConversions = new SpecSetData()
            {
                OriginalPhase = "126-QA_RT_BuildingPhase4_Automation",
                OriginalCategory = "QA_RT_Category4_Automation",
                OriginalProduct = "QA_RT_Product4_Automation",
                OriginalProductStyle = "QA_RT_Style4_Automation",
                OriginalProductUse = "QA_RT_Use4_Automation",
                NewPhase = "125-QA_RT_BuildingPhase3_Automation",
                NewCategory = "QA_RT_Category3_Automation",
                NewProduct = "QA_RT_Product3_Automation",
                NewProductStyle = "QA_RT_Style3_Automation",
                NewProductUse = "QA_RT_Use3_Automation",
                ProductCalculation = "NONE"
            };

            newStyleConversions = new SpecSetData()
            {
                OriginalManufacture = "QA_RT_Manufacturer4_Automation",
                OriginalStyle = "QA_RT_Style4_Automation",
                OriginalUse = "QA_RT_Use4_Automation",
                NewManufacture = "QA_RT_Manufacturer3_Automation",
                NewStyle = "QA_RT_Style3_Automation",
                NewUse = "QA_RT_Use3_Automation",
                StyleCalculation = "NONE"
            };



        }
        [Test]
        [Category("Section_IV")]
        public void B09_A_Estimating_DetailPages_SpecSets_SpecSetGroups_Assignments()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 2. Open a Spec Set Group assignment page </b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.ChangeSpecSetPageSize(20);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData.GroupName);
            }

            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(specSetData.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", specSetData.GroupName);
            string specSetDetail_url = SpecSetDetailPage.Instance.CurrentURL;

            // Step 3. Click add 
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 3. Add/Edit/Delete a Spec Set > Verify the spec set is added/edited/deleted successfully</b></font>");
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            Assert.That(SpecSetDetailPage.Instance.IsModalDisplayed(), "The add new spect set modal is not displayed");
            SpecSetDetailPage.Instance.CreateNewSpecSet(specSetData.SpectSetName);
            SpecSetDetailPage.Instance.VerifyCreateNewSpecSetIsDisplayedInGrid(specSetData.SpectSetName);
            SpecSetDetailPage.Instance.EditItemOnSpecSetGrid("Spec Set", specSetData.SpectSetName, NEWSPECSET_NAME_DEFAULT);
            SpecSetDetailPage.Instance.DeleteSpecSet(NEWSPECSET_NAME_DEFAULT);
            string actualDeleteMsg = SpecSetDetailPage.Instance.GetLastestToastMessage();
            string expectedDeleteSuccessfulMess = "Spec Set removed!";
            if (expectedDeleteSuccessfulMess.Equals(actualDeleteMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The SpecSet removed successfully!</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The SpecSet could not be deleted!</font>. Actual message: {actualDeleteMsg}");
            }


            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(specSetData.SpectSetName);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 4 .Filter a Spec Set > Verify the spec set is filtered correctly</b></font>");
            //Add New SpecSet
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(NEWSPECSET_NAME_DEFAULT);
            SpecSetDetailPage.Instance.FilterItemInGrid("Spec Set", GridFilterOperator.Contains, NEWSPECSET_NAME_DEFAULT);
            if (SpecSetDetailPage.Instance.IsItemInGrid("Spec Set", NEWSPECSET_NAME_DEFAULT) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Filter {NEWSPECSET_NAME_DEFAULT} is successfully in grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Filter {NEWSPECSET_NAME_DEFAULT} is unsuccessfully in grid </font>");
            }

            //Nofilter
            SpecSetDetailPage.Instance.FilterItemInGrid("Spec Set", GridFilterOperator.NoFilter, string.Empty);

            // Close current tab
            CommonHelper.CloseAllTabsExcludeCurrentOne();


            // Tab 1: Setting page
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 5. Add/Edit/Delete a Product Conversion > Verify the Product Conversion is added/edited/deleted successfully</b></font>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Show Category on Add Spec Set Product Conversion Modal - TURN ON</b></font>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings, true, true);
            CommonHelper.SwitchTab(1);

            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            EstimatingPage.Instance.VerifySettingEstimatingPageIsDisplayed();
            EstimatingPage.Instance.Check_Show_Category_On_Product_Conversion(true);
            CommonHelper.SwitchTab(0);

            // Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithCategory(productConversions);
            if ($"Created Spec Set Product ({productConversions.OriginalProduct}) In Spec Set ({productConversions.SpectSetName})" != SpecSetDetailPage.Instance.GetLastestToastMessage())
                Console.WriteLine(SpecSetDetailPage.Instance.GetLastestToastMessage());
            ExtentReportsHelper.LogInformation("Created the Product Conversation in Spec Set.");
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(productConversions);

            SpecSetDetailPage.Instance.EditItemProductConversionsInGrid(productConversions.OriginalProduct);
            SpecSetDetailPage.Instance.UpdateProductConversion(newProductConversions);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(newProductConversions);

            SpecSetDetailPage.Instance.DeleteItemOnProductConversionsInGrid(newProductConversions.OriginalProduct);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Show Category on Add Spec Set Product Conversion Modal - TURN OFF</b></font>");

            CommonHelper.SwitchTab(1);
            EstimatingPage.Instance.Check_Show_Category_On_Product_Conversion(false);


            CommonHelper.SwitchTab(0);

            // Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(productConversions);
            if ($"Created Spec Set Product ({productConversions.OriginalProduct}) In Spec Set ({productConversions.SpectSetName})" != SpecSetDetailPage.Instance.GetLastestToastMessage())
                Console.WriteLine(SpecSetDetailPage.Instance.GetLastestToastMessage());
            ExtentReportsHelper.LogInformation("Created the Product Conversation in Spec Set.");
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(productConversions);

            SpecSetDetailPage.Instance.EditItemProductConversionsInGrid(productConversions.OriginalProduct);
            SpecSetDetailPage.Instance.UpdateProductConversion(newProductConversions);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(newProductConversions);

            SpecSetDetailPage.Instance.DeleteItemOnProductConversionsInGrid(newProductConversions.OriginalProduct);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 6. Add/Edit/Delete a Style Conversion > Verify the Style Conversion is added/edited/deleted successfully</b></font>");

            // Add New Conversation Style
            SpecSetDetailPage.Instance.AddStyleConversion(styleConversions);
            string actualMsg = SpecSetDetailPage.Instance.GetLastestToastMessage();
            string expectedMsg = "Style Conversion Created";
            if (actualMsg.Equals(expectedMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Style Conversion created successfully.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Style Conversion is create unsuccessfully.</font>");
            }

            SpecSetDetailPage.Instance.VerifyAddStyleConversionInGrid(styleConversions);
            //Edit Style Conversion
            SpecSetDetailPage.Instance.EditItemOnStyleConversionsInGridWithoutProductConversion(styleConversions.OriginalStyle);
            SpecSetDetailPage.Instance.UpdateStyleConversion(newStyleConversions);

            string expectMsgUpdatedStyle = "Style Conversion Updated";
            string actualMsgUpdatedStyle = SpecSetDetailPage.Instance.GetLastestToastMessage();
            if (expectMsgUpdatedStyle.Equals(actualMsgUpdatedStyle))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Style Conversion is updated successfully.</b></font>");
                SpecSetDetailPage.Instance.CloseToastMessage();
            }
            else
            {
                Assert.Fail($"<font color='red'>The Style Conversion is not updateed successfully. Actual message: <i>{actualMsgUpdatedStyle}</i></font>");
            }

            SpecSetDetailPage.Instance.VerifyAddStyleConversionInGrid(newStyleConversions);
            SpecSetDetailPage.Instance.DeleteItemOnStyleConversionsInGrid(newStyleConversions.OriginalManufacture);

            string actualDeleteStyleMsg = SpecSetDetailPage.Instance.GetLastestToastMessage();
            string expectedDeleteStyleSuccessfulMess = "Spec Set Style removed!";
            if (expectedDeleteStyleSuccessfulMess.Equals(actualDeleteStyleMsg))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Style Conversion removed successfully!</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Style Conversion could not be deleted!</font>. Actual message: {actualDeleteStyleMsg}");
            }


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 8. Add a Division/Community/House/Job/Option to the Spec Set</b></font>");
            CommonHelper.RefreshPage();
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(DIVISIONS_NAME, DIVISION_NAME_DEFAULT, specSetData.SpectSetName, string.Empty);
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(COMMUNITIES_NAME, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT), specSetData.SpectSetName, string.Empty);
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(HOUSES_NAME, (HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT), specSetData.SpectSetName, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT));
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(JOBS_NAME, JOB_NAME_DEFAULT, specSetData.SpectSetName, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT));
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(OPTION_NAME, OPTION_NAME_DEFAULT, specSetData.SpectSetName, string.Empty);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step H: Change the Spec Set under the Divisions/Communities/Houses/Jobs/Options </b></font>");
            SpecSetDetailPage.Instance.EditNameItemAndCheckItemInGrid(DIVISIONS_NAME, "Name", DIVISION_NAME_DEFAULT, NEWSPECSET_NAME_DEFAULT);
            SpecSetDetailPage.Instance.EditNameItemAndCheckItemInGrid(COMMUNITIES_NAME, "Name", COMMUNITY_NAME_DEFAULT, NEWSPECSET_NAME_DEFAULT);
            SpecSetDetailPage.Instance.EditNameItemAndCheckItemInGrid(HOUSES_NAME, "House Name", HOUSE_NAME_DEFAULT, NEWSPECSET_NAME_DEFAULT);
            SpecSetDetailPage.Instance.EditNameItemAndCheckItemInGrid(JOBS_NAME, "Number", JOB_NAME_DEFAULT, NEWSPECSET_NAME_DEFAULT);
            SpecSetDetailPage.Instance.EditNameItemAndCheckItemInGrid(OPTION_NAME, "Option Name", OPTION_NAME_DEFAULT, NEWSPECSET_NAME_DEFAULT);


            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step I: Remove the Spec Set from the Division/Community/House/Job/Option</b></font>");
            SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(DIVISIONS_NAME, "Name", DIVISION_NAME_DEFAULT);
            SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(COMMUNITIES_NAME, "Name", COMMUNITY_NAME_DEFAULT);
            SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(HOUSES_NAME, "House Name", HOUSE_NAME_DEFAULT);
            SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(JOBS_NAME, "Number", JOB_NAME_DEFAULT);
            SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(OPTION_NAME, "Option Name", OPTION_NAME_DEFAULT);

            System.Threading.Thread.Sleep(3000);

            //J. Expand all Spec Sets
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 9.Expand all Spec Sets</b></font>");
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            SpecSetDetailPage.Instance.VerifyCreateNewSpecSetIsDisplayedInGrid(specSetData.SpectSetName);
            SpecSetDetailPage.Instance.VerifyCreateNewSpecSetIsDisplayedInGrid(NEWSPECSET_NAME_DEFAULT);

            //Delete Product Conversion
            CommonHelper.ScrollToBeginOfPage();
            System.Threading.Thread.Sleep(3000);
            SpecSetDetailPage.Instance.DeleteSpecSet(NEWSPECSET_NAME_DEFAULT);

            //  Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(productConversions);
            if ($"Created Spec Set Product ({productConversions.OriginalProduct}) In Spec Set ({productConversions.SpectSetName})" != SpecSetDetailPage.Instance.GetLastestToastMessage())
                Console.WriteLine(SpecSetDetailPage.Instance.GetLastestToastMessage());
            ExtentReportsHelper.LogInformation("Created the Product Conversation in Spec Set.");
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(productConversions);

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10: verify the Export/Import functions<b>");
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.1: Verify 'EXPORT CSV' function.</b></font>");
            // Scroll up to click utility button
            CommonHelper.ScrollToBeginOfPage();

            SpecSetDetailPage.Instance.ImportExporFromMoreMenu(EXPORT_PRODUCTCONVERSIONS_TO_CSV, specSetData.GroupName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.2: Verify 'EXPORT Excel' function.</b></font>");
            SpecSetDetailPage.Instance.ImportExporFromMoreMenu(EXPORT_PRODUCTCONVERSIONS_TO_EXCEL, specSetData.GroupName);
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Step 10.3: Verify IMPORT Product Conversion function.</b></font>");
            string importFileDir;
            // Open Import page
            importFileDir = $"\\DataInputFiles\\Import\\PIPE_19177\\Pipeline_SpecSetProducts.csv";
            // Click import from Utilities button
            SpecSetDetailPage.Instance.ImportExporFromMoreMenu(IMPORT);
            SpecSetDetailPage.Instance.ImportFile(IMPORT_PRODUCTSCONVERSION_TO_SPECSETGROUP_PRODUCTSCONVERSIONS, importFileDir);

            CommonHelper.OpenURL(specSetDetail_url);
            // Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(newProductConversions);
        }
        [TearDown]
        public void DeleteData()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b> Back to Spec Set page and delete it.</b></font>");
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData.GroupName);
                if (SpecSetPage.Instance.GetLastestToastMessage().ToLower().Contains("successful"))
                    ExtentReportsHelper.LogPass("<font color = 'green'><b>Spec Set Group deleted successfully!</b></font>");
                else
                    ExtentReportsHelper.LogFail("<font color = 'red'>Spec Set Group failed to delete!</font>");

            }

        }
    }
}

