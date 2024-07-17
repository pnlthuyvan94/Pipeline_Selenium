using NUnit.Framework;
using NUnit.Framework.Legacy;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.CustomOptions;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionProduct;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.Assets.Options.Products;
using Pipeline.Testing.Pages.Estimating.Products;
using Pipeline.Testing.Pages.Settings.Builder.Option;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A11_RT_01227 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CustomOptionData customOption;

        [SetUp]
        public void GetData()
        {
            customOption = new CustomOptionData()
            {
                Code = "R-QA Only CO Auto Update",
                Description = "Regression test create Custom Option Update",
                Structural = bool.Parse("FALSE"),
                Price = double.Parse("999")
            };

            ExtentReportsHelper.LogInformation(null, "Step 0: Go to the setting page and setting the option view by");
            OptionSettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            OptionSettingPage.Instance.LeftMenuNavigation("Builder");
            OptionSettingPage.Instance.SwichingToOptionView();
            OptionSettingPage.Instance.SelectOptionDisplayFormat(OptionDisplayFormat.OptionNameOptionNumber);
            OptionSettingPage.Instance.GetLastestToastMessage();
            ExtentReportsHelper.LogInformation("Update setting.");
            OptionSettingPage.Instance.CloseToastMessage();

            // Close all tab exclude the current one
            CommonHelper.CloseAllTabsExcludeCurrentOne();
        }

        [Test]
        [Category("Section_IV")]
        public void A11_B_Assets_DetailPage_CustomOption_Product()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Options/CustomOptions.aspx
            ExtentReportsHelper.LogInformation(null, "Step 1: From ASSETS/CUSTOM OPTIONS, click the Custom Option to which you would like to select");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL);
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, customOption.Code);

            if (CustomOptionPage.Instance.IsItemInGrid("Code", customOption.Code) is false)
            {
                CustomOptionPage.Instance.CreateCustomOption(customOption);
                ExtentReportsHelper.LogInformation(null, "Step 2: Current is the custom option detail page, so ignore step 2.");
            }
            else
            {
                // Step 2: In Side Navigation, click the Details to open the Details page
                ExtentReportsHelper.LogInformation(null, "Step 2: In Side Navigation, click the Details to open the Details page");
                CustomOptionPage.Instance.SelectItemInGrid("Code", customOption.Code);
            }

            CustomOptionDetailPage.Instance.LeftMenuNavigation("Products");
            ClassicAssert.True(CustomOptionDetailPage.Instance.IsSaveCustomOptionSuccessful(customOption.Code));
            ExtentReportsHelper.LogPass("Opened successfully the Custom Option Details page ");

            // Step 3: Click the '+' button on page to open 'Add Product' modal; Add Product to Custom Option
            ExtentReportsHelper.LogInformation(null, "Step 3: Click the '+' button on page to open 'Add Product' modal; Add Product to Custom Option");
            CustomOptionProduct.Instance.Click_AddButton();
            IList<string> ListBuildPhase = CustomOptionProduct.Instance.GetListItem("//select[contains(@id,'ddlBuildingPhases')]/option");
            CustomOptionProduct.Instance.AddProduct(ListBuildPhase[3]);
            ExtentReportsHelper.LogInformation($"Add product with {ListBuildPhase[3]}");
            string _expectedMessage = "Custom Option Product created successfully!";
            string actualMsg = CustomOptionProduct.Instance.GetLastestToastMessage();
            if (_expectedMessage.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Added successfully the Product to Custom Option");
                CustomOptionProduct.Instance.CloseToastMessage();
            }
            else 
            {
                ExtentReportsHelper.LogFail("Add unsucessfully the Product to Custom Option");
            }
            CustomOptionProduct.Instance.CloseAddProduct();

            // Step 4: Verify the 'Copy Product Quantities' button
            ExtentReportsHelper.LogInformation(null, "Step 4: Verify the 'Copy Product Quantities' button");
            ExtentReportsHelper.LogInformation(null, "Clicked the 'Copy Quantities to Selected Option' button on modal; the Product is copied successfully");
            CustomOptionProduct.Instance.Click_CopyProductQuantities();
            IList<string> ListQuantities = CustomOptionProduct.Instance.GetListItem("//div[@id='ctl00_CPH_Content_rlbOptionsTo']/div/ul/li/label/span");

            string OPTION_NAME_DEFAULT = "QA_RT_Option_Automation"; 
            CustomOptionProduct.Instance.SelectOptionByName(OPTION_NAME_DEFAULT);
            actualMsg = CustomOptionProduct.Instance.Click_CopyQuantities();
            string _expectedCopyQuantities = $"Quantities successfully copied from custom option {customOption.Code} to following option(s):\r\n{OPTION_NAME_DEFAULT}";
            if (actualMsg.Contains(_expectedCopyQuantities))
            {
                ExtentReportsHelper.LogPass($"Quantities sucessfully copied from custom option {customOption.Code} to following option(s): {OPTION_NAME_DEFAULT}");
                CustomOptionProduct.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"Copy quantities unsucessfully. <br>Actual message:<br>{actualMsg}.<br>Expected:<br>{_expectedCopyQuantities}");
            }
            CustomOptionProduct.Instance.CloseCopyProductQuantities();


            ExtentReportsHelper.LogInformation(null, $"Switch to {OPTION_NAME_DEFAULT} option for check");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);

            // Clear option name filter


            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, OPTION_NAME_DEFAULT);

            OptionPage.Instance.SelectItemInGridWithTextContains("Name", OPTION_NAME_DEFAULT);
            OptionDetailPage.Instance.LeftMenuNavigation("Products");
            ProductPage.Instance.ChangePageSize(100);
            System.Threading.Thread.Sleep(5000);
            // Verify Product option
            if (ProductsToOptionPage.Instance.IsOptionQuantitiesInGrid("Building Phase", ListBuildPhase[3].Trim()))
            {
                ExtentReportsHelper.LogPass($"Quanities  be coppied display in Grid of Option");

                ProductsToOptionPage.Instance.DeleteItemInGrid("Building Phase", ListBuildPhase[3].Trim());
                ProductsToOptionPage.Instance.WaitOptionQuantitiesLoadingIcon();
            }
            else
                ExtentReportsHelper.LogFail($"Quanities  be coppied no display in Grid of Option");

            // Comback to Custom Option page
            CustomOptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.CustomOptions);
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, customOption.Code);
            CustomOptionPage.Instance.SelectItemInGrid("Code", customOption.Code);
            CustomOptionDetailPage.Instance.LeftMenuNavigation("Products");

            // Step 5:  Verify the 'Generate BOM' button
            ExtentReportsHelper.LogInformation(null, "Step 5:  Verify the 'Generate BOM' button");
            ExtentReportsHelper.LogInformation(null, "Select the Community; Click the 'Generate BOM & Estimate' button");
            CustomOptionProduct.Instance.Click_GenerateBOMEstimate();
            string _expectedGenerate = $"Report generated";
            actualMsg = CustomOptionProduct.Instance.GetLastestToastMessage();
            if (_expectedGenerate.Equals(actualMsg))
            {
                ExtentReportsHelper.LogPass($"Generate BOM successfully");
                CustomOptionProduct.Instance.CloseToastMessage();
            }
            else
            {
                ExtentReportsHelper.LogFail($"Generate BOM unsuccessfully. Actual message {actualMsg}");
            }

            if (!CustomOptionProduct.Instance.IsItemGird("Option", customOption.Code))
                ExtentReportsHelper.LogFail($"The item doesn't display {customOption.Code} on Gird");
            else
                ExtentReportsHelper.LogPass($"The item display {customOption.Code} on Gird");

            // Step 6: Delete the Product out of Custom Option
            ExtentReportsHelper.LogInformation(null, "Step 6: Delete the Product out of Custom Option");
            ExtentReportsHelper.LogInformation(null, "Selected Product; click the 'Delete' button");
            CustomOptionProduct.Instance.DeleteItemOnProductGird("Building Phase", ListBuildPhase[3].Trim());

            if (CustomOptionProduct.Instance.IsItemGird("Building Phase", ListBuildPhase[3].Trim()) is true)
                ExtentReportsHelper.LogFail("Can't delete product on the grid view");
            else
                ExtentReportsHelper.LogPass(null, "Deleted product successfully");
        }

        [TearDown]
        public void DeleteCustomOption()
        {
            CustomOptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.CustomOptions);
            ExtentReportsHelper.LogInformation(null, $"Step 6.1: Filter the Custom option with Code {customOption.Code} then delete it.");
            CustomOptionPage.Instance.DeleteCustomOption(customOption);
        }
    }
}
