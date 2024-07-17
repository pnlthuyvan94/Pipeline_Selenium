using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.Assets.House.HouseSpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.UserMenu.Setting;
using Pipeline.Testing.Script.TestData;


namespace Pipeline.Testing.Script.Section_IV
{
    class A04_K_PIPE_39504 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        readonly string HOUSES_NAME = "Houses";

        private SpecSetData specSetData1;
        private SpecSetData specSetData2;
        private SpecSetData specSetData3;
        private SpecSetData specSetData4;
        private SpecSetData productConversions;
        private SpecSetData styleConversions;

        public const string OPTION_NAME_DEFAULT = TestDataCommon.OPTION_NAME_DEFAULT;
        public const string OPTION_CODE_DEFAULT = TestDataCommon.OPTION_CODE_DEFAULT;

        private const string COMMUNITY_CODE_DEFAULT = TestDataCommon.COMMUNITY_CODE_DEFAULT;
        private const string COMMUNITY_NAME_DEFAULT = TestDataCommon.COMMUNITY_NAME_DEFAULT;

        private readonly string HOUSE_CODE_DEFAULT = TestDataCommon.HOUSE_CODE_DEFAULT;
        private readonly string HOUSE_NAME_DEFAULT = TestDataCommon.HOUSE_NAME_DEFAULT;

        [SetUp]
        public void GetTestData()
        {

            specSetData1 = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup1_Automation",
                UseDefault = "FALSE",
                SpectSetName = "QA_RT_SpecSet1_Automation",
            };
            specSetData2 = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup2_Automation",
                UseDefault = "TRUE",
                SpectSetName = "QA_RT_SpecSet2_Automation",
            };
            specSetData3 = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup3_Automation",
                UseDefault = "TRUE",
                SpectSetName = "QA_RT_SpecSet3_Automation",
            };
            specSetData4 = new SpecSetData()
            {
                GroupName = "QA_RT_SpecSetGroup4_Automation",
                UseDefault = "FALSE",
                SpectSetName = "QA_RT_SpecSet4_Automation",
            };

            productConversions = new SpecSetData()
            {
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

        }

        [Test]
        [Category("Section_IV")]
        public void A04_K_Assets_DetailPage_Houses_SpecSetsPage_SpecSet_page_on_a_House_needs_to_display_Community()
        {
            //I.  When there is no Spec Set. Go to House > Spec Sets
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step I. When there is no Spec Set. Go to House > Spec Sets.</font>");
            //Prepare data for House Data
            ExtentReportsHelper.LogInformation(null, "Prepare data for House Data.");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);

            // Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"Filter house with name {HOUSE_NAME_DEFAULT} and create if it doesn't exist.");
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, HOUSE_NAME_DEFAULT);
            if (HousePage.Instance.IsItemInGrid("Name", HOUSE_NAME_DEFAULT) is true)
            {

                ExtentReportsHelper.LogInformation($"House with Name {HOUSE_NAME_DEFAULT} is displayed in grid");
                HousePage.Instance.SelectItemInGridWithTextContains("Name", HOUSE_NAME_DEFAULT);
            }
            //Verify House Spec Set In Grid
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Verify House Spec Set In Grid.</font>");

            string houseDetail_url = HouseDetailPage.Instance.CurrentURL;
            HouseDetailPage.Instance.LeftMenuNavigation("Spec Sets");
            HouseSpecSetPage.Instance.VerifyJobSpecSetsPageIsDisplayed();

            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating");
            EstimatingPage.Instance.Check_Show_Category_On_Product_Conversion(false);

            //II. Add Spec Set to House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step II. Add Spec Set to House.</font>");
            //II.1 Spec Set have no Default column, have Product and Style conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.1 Spec Set have no Default column, have Product and Style conversion.</font>");
            //a. Create a new Spec Set Group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.1 a. Create a new Spec Set Group.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData1.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData1.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData1.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData1.GroupName);
            }

            SpecSetPage.Instance.CreateNewSpecSetGroup(specSetData1.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData1.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", specSetData1.GroupName);
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(specSetData1.SpectSetName);
            // Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            //b. Add Product and Style to Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.1b. Add Product and Style to Spec Set.</font>");
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(productConversions);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(productConversions);

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

            //c. Assign Spec Set to House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.1c. Assign Spec Set to House.</font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(HOUSES_NAME, (HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT), specSetData1.SpectSetName, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT));
            //d. Go to House detail > Spec Set

            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.1d. Go to House detail > Spec Set.</font>");
            //The Spec Set displays in grid view
            CommonHelper.OpenURL(houseDetail_url);
            HouseDetailPage.Instance.LeftMenuNavigation("Spec Sets");
            if (HouseSpecSetPage.Instance.IsItemInGrid("House Spec Set", specSetData1.SpectSetName))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>SpecSet With Name {specSetData1.SpectSetName} is displayed in House Spec Sets Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>SpecSet With Name {specSetData1.SpectSetName} is not display in House Spec Sets Grid.</font>");
            }



            //2. Spec Set have no Default column, no Product and no Style conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2. Spec Set have no Default column, no Product and no Style conversion.</font>");
            //a. Create new Spec Set and then left it blank
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2a. Create new Spec Set and then left it blank.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData2.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData2.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData2.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData2.GroupName);
            }
            SpecSetPage.Instance.CreateNewSpecSetGroup(specSetData2.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData2.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", specSetData2.GroupName);
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(specSetData2.SpectSetName);
            //b. Assign this Spec Set to House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2b. Assign this Spec Set to House.</font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(HOUSES_NAME, (HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT), specSetData2.SpectSetName, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT));
            //c. Go to House > Spec Sets
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 2c. Go to House > Spec Sets.</font>");
            CommonHelper.OpenURL(houseDetail_url);
            HouseDetailPage.Instance.LeftMenuNavigation("Spec Sets");
            if (HouseSpecSetPage.Instance.IsItemInGrid("House Spec Set", specSetData2.SpectSetName))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>SpecSet With Name {specSetData2.SpectSetName} is displayed in House Spec Sets Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>SpecSet With Name {specSetData2.SpectSetName} is not display in House Spec Sets Grid.</font>");
            }


            //3. Default checked and have Product, Style conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3. Default checked and have Product, Style conversion.</font>");
            //a. Create a new Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3a. Create a new Spec Set.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData3.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData3.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData3.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData3.GroupName);
            }

            SpecSetPage.Instance.CreateNewSpecSetGroup(specSetData3.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData3.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", specSetData3.GroupName);
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(specSetData3.SpectSetName);

            // Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            //b. Add Product and Style to Spec Set, check Default
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3b. Add Product and Style to Spec Set, check Default.</font>");
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(productConversions);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(productConversions);

            // Add New Conversation Style
            SpecSetDetailPage.Instance.AddStyleConversion(styleConversions);
            SpecSetDetailPage.Instance.VerifyAddStyleConversionInGrid(styleConversions);

            //c. Assign Spec Set to House with the default Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3c. Assign Spec Set to House with the default Spec Set.</font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(HOUSES_NAME, (HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT), specSetData3.SpectSetName, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT));

            //d. Go to House > Spec Set to check
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 3d. Go to House > Spec Set to check.</font>");
            CommonHelper.OpenURL(houseDetail_url);
            HouseDetailPage.Instance.LeftMenuNavigation("Spec Sets");
            if (HouseSpecSetPage.Instance.IsItemInGrid("House Spec Set", specSetData3.SpectSetName))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>SpecSet With Name {specSetData3.SpectSetName} is displayed in House Spec Sets Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>SpecSet With Name {specSetData3.SpectSetName} is not display in House Spec Sets Grid.</font>");
            }


            //4. Default not checked, have Product and Style conversion
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4. Default not checked, have Product and Style conversion.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData4.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData4.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData4.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData4.GroupName);
            }

            //a. Create a new Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4a. Create a new Spec Set.</font>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(specSetData4.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData4.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", specSetData4.GroupName);
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();
            SpecSetDetailPage.Instance.CreateNewSpecSet(specSetData4.SpectSetName);
            //b. Add Product and Style to Spec Set, do not check Default
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4b. Add Product and Style to Spec Set, do not check Default.</font>");
            // Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();
            //Add new Product 
            SpecSetDetailPage.Instance.AddProductConversionWithoutCategory(productConversions);
            SpecSetDetailPage.Instance.VerifyAddProductConversionInGrid(productConversions);


            // Add New Conversation Style
            SpecSetDetailPage.Instance.AddStyleConversion(styleConversions);
            SpecSetDetailPage.Instance.VerifyAddStyleConversionInGrid(styleConversions);

            //c. Assign Spec Set to House with the Spec Set is not default
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4c. Assign Spec Set to House with the Spec Set is not default.</font>");
            SpecSetDetailPage.Instance.AddNameItemAndCheckItemInGrid(HOUSES_NAME, (HOUSE_CODE_DEFAULT + "-" + HOUSE_NAME_DEFAULT), specSetData4.SpectSetName, (COMMUNITY_CODE_DEFAULT + "-" + COMMUNITY_NAME_DEFAULT));
            
            //d. Go to House > Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step 4d. Go to House > Spec Set.</font>");
            CommonHelper.OpenURL(houseDetail_url);
            HouseDetailPage.Instance.LeftMenuNavigation("Spec Sets");
            if (HouseSpecSetPage.Instance.IsItemInGrid("House Spec Set", specSetData4.SpectSetName))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>SpecSet With Name {specSetData4.SpectSetName} is displayed in House Spec Sets Grid</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>SpecSet With Name {specSetData4.SpectSetName} is not display in House Spec Sets Grid.</font>");
            }


            //III. Remove Spec Set from House
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III. Remove Spec Set from House.</font>");
            //1.Remove assign.Go to House > Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.1.Remove assign.Go to House > Spec Set.</font>");
            //2. Delete Spec Set
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.2. Delete Spec Set.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData1.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData1.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData1.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.SelectItemInGrid("Name", specSetData1.GroupName);

                SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(HOUSES_NAME, "House Name", HOUSE_NAME_DEFAULT);
                SpecSetDetailPage.Instance.DeleteSpecSet(specSetData1.SpectSetName);
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
            }
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData2.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData2.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData2.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.SelectItemInGrid("Name", specSetData2.GroupName);

                SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(HOUSES_NAME, "House Name", HOUSE_NAME_DEFAULT);
                SpecSetDetailPage.Instance.DeleteSpecSet(specSetData2.SpectSetName);
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
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData3.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData3.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData3.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.SelectItemInGrid("Name", specSetData3.GroupName);

                SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(HOUSES_NAME, "House Name", HOUSE_NAME_DEFAULT);
                SpecSetDetailPage.Instance.DeleteSpecSet(specSetData3.SpectSetName);
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
            }

            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData4.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData4.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData4.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.SelectItemInGrid("Name", specSetData4.GroupName);

                SpecSetDetailPage.Instance.DeleteNameItemAndCheckItemInGrid(HOUSES_NAME, "House Name", HOUSE_NAME_DEFAULT);
                SpecSetDetailPage.Instance.DeleteSpecSet(specSetData4.SpectSetName);
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
            }

            //3. Delete Spec Set Group
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step III.3. Delete Spec Set Group.</font>");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_SPECSETS_URL);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData1.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData1.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData1.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData1.GroupName);
            }

            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData2.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData2.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData2.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData2.GroupName);
            }

            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData3.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData3.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData3.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData3.GroupName);
            }

            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", specSetData4.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", specSetData4.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {specSetData4.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", specSetData4.GroupName);
            }

            //IV. Verify the paging displays correctly
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'>Step IV. Verify the paging displays correctly.</font>");
            CommonHelper.OpenURL(houseDetail_url);
            HouseDetailPage.Instance.LeftMenuNavigation("Spec Sets");

            HouseSpecSetPage.Instance.ChangeHouseSpecSetPageSize(20);
            HouseSpecSetPage.Instance.ChangeHouseSpecSetPageSize(10);
            HouseSpecSetPage.Instance.ChangeHouseSpecSetPageSize(50);
            HouseDetailPage.Instance.LeftMenuNavigation("Spec Sets");
        }
    }
}
