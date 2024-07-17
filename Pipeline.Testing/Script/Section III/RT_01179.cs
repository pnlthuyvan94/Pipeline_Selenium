using NUnit.Framework;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail;
using Pipeline.Testing.Pages.Estimating.SpecSet;
using System;
using Pipeline.Common.BaseClass;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Settings.Estimating;
using Pipeline.Testing.Pages.UserMenu.Setting;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class B17_RT_01179 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }
        SpecSetData SpecSetData;
        [SetUp]
        public void GetTestData()
        {
            SpecSetData = new SpecSetData()
            {
                GroupName= "RT_Automation_Purpose_DoNot_Delete",
                SpectSetName= "RT_SpecSet-Auto",
                OriginalManufacture= "Hai Nguyen Manufacturer",
                OriginalStyle= "Vintage",
                OriginalUse= "UseA",
                NewManufacture= "General",
                NewStyle= "General",
                NewUse= "UseA",
                StyleCalculation= "Block to Mortar (QTY/13.5)",
                OriginalPhase= "0003-HN-Phase-3",
                OriginalProduct= "HN-Trace-09",
                OriginalProductStyle= "Vintage",
                OriginalProductUse= "UseA",
                NewPhase= "1164-QA Only Phase-3",
                NewProduct= "QA_Product07",
                NewProductStyle= "DEFAULT",
                NewProductUse= "UseD",
                ProductCalculation= "Brick 500 to 464 (QTY/500*464)"

            };

        }

        [Test]
        [Category("Section_III")]
        public void B17_Estimating_AddSpecSet()
        {
            ExtentReportsHelper.LogInformation(null, "<font color='lavender'><b>Back to Setting Page to change Show Category On SpecSet Product Conversion is turned false.</font><b>");
            SettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            SettingPage.Instance.LeftMenuNavigation("Estimating", false);
            EstimatingPage.Instance.Check_Show_Category_On_Product_Conversion(false);

            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/ProductAssemblies/SpecSets/Default.aspx
            SpecSetPage.Instance.SelectMenu(MenuItems.ESTIMATING).SelectItem(EstimatingMenu.SpecSets);
            //TODO: user other page
            // Step 2. Insert name to filter and click filter by Contain value and open Spec Set Group page
            SpecSetPage.Instance.ChangeSpecSetPageSize(20);
            SpecSetPage.Instance.Navigatepage(1);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            if (SpecSetPage.Instance.IsItemInGrid("Name", SpecSetData.GroupName) is true)
            {
                ExtentReportsHelper.LogInformation(null, $"<b> {SpecSetData.GroupName} is exited in grid.</b>");
                SpecSetPage.Instance.DeleteItemInGrid("Name", SpecSetData.GroupName);
            }

            ExtentReportsHelper.LogInformation(null, "<b>Create new Spec Set group.</b>");
            SpecSetPage.Instance.CreateNewSpecSetGroup(SpecSetData.GroupName);
            SpecSetPage.Instance.FindItemInGridWithTextContains("Name", SpecSetData.GroupName);
            SpecSetPage.Instance.SelectItemInGrid("Name", SpecSetData.GroupName);

            // Step 3. Click add 
            SpecSetDetailPage.Instance.OpenCreateSpecSetModal();

            if (SpecSetDetailPage.Instance.IsModalDisplayed() is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The add new spect set modal is not displayed.</font>");
            }

            // Step 4. Create new spect set
            SpecSetDetailPage.Instance.CreateNewSpecSet(SpecSetData.SpectSetName);
            // Step 5. Expand all
            SpecSetDetailPage.Instance.ExpandAllSpecSet();

            // Step 6. Add New Conversation Style
            SpecSetDetailPage.Instance.ClickAddNewConversationStyle();
            SpecSetDetailPage.Instance.SelectOriginalManufacture(SpecSetData.OriginalManufacture);
            SpecSetDetailPage.Instance.SelectOriginalStyle(SpecSetData.OriginalStyle);
            SpecSetDetailPage.Instance.SelectOriginalUse(SpecSetData.OriginalUse);
            SpecSetDetailPage.Instance.SelectNewManufacture(SpecSetData.NewManufacture);
            SpecSetDetailPage.Instance.SelectNewStyle(SpecSetData.NewStyle);
            SpecSetDetailPage.Instance.SelectNewUse(SpecSetData.NewUse);
            SpecSetDetailPage.Instance.SelectStyleCalculation(SpecSetData.StyleCalculation);
            SpecSetDetailPage.Instance.PerformInsertStyle();

            // Step 7. Add new Product 
            SpecSetDetailPage.Instance.ClickAddNewProduct();
            SpecSetDetailPage.Instance.SelectOriginalBuildingPhase(SpecSetData.OriginalPhase);
            SpecSetDetailPage.Instance.SelectOriginalProduct(SpecSetData.OriginalProduct);
            SpecSetDetailPage.Instance.SelectOriginalProductStyle(SpecSetData.OriginalProductStyle);
            SpecSetDetailPage.Instance.SelectOriginalProductUse(SpecSetData.OriginalProductUse);
            SpecSetDetailPage.Instance.SelectNewBuildingPhase(SpecSetData.NewPhase);
            SpecSetDetailPage.Instance.SelectNewProduct(SpecSetData.NewProduct);
            SpecSetDetailPage.Instance.SelectNewProductStyle(SpecSetData.NewProductStyle);
            SpecSetDetailPage.Instance.SelectNewProductUse(SpecSetData.NewProductUse);
            SpecSetDetailPage.Instance.SelectProductCalculation(SpecSetData.ProductCalculation);
            SpecSetDetailPage.Instance.PerformInsertProduct();

            // Step 8. Capture information and delete for next run cycle
            if ($"Created Spec Set Product ({SpecSetData.OriginalProduct}) In Spec Set ({SpecSetData.SpectSetName})" != SpecSetDetailPage.Instance.GetLastestToastMessage())
                Console.WriteLine(SpecSetDetailPage.Instance.GetLastestToastMessage());

            ExtentReportsHelper.LogInformation("Created the Product Conversation in Spec Set.");

        }

        [TearDown]
        public void DeleteSpecSet()
        {
            try
            {
                SpecSetDetailPage.Instance.DeleteSpecSet(SpecSetData.SpectSetName);
                if ("Spec Set removed!" == SpecSetDetailPage.Instance.GetLastestToastMessage())
                    ExtentReportsHelper.LogInformation("Spec Set removed successfully!");
                System.Threading.Thread.Sleep(1000);
                ExtentReportsHelper.LogInformation("Information");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
