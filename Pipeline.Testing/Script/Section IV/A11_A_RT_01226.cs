using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.CustomOptions;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A11_RT_01226 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        private CustomOptionData _customOption;
        private CustomOptionData _newcustomOption;

        [SetUp]
        public void GetData()
        {

            _customOption = new CustomOptionData()
            {
                Code = "R-QA Only CO Auto",
                Description = "Regression test create Custom Option",
                Structural = bool.Parse("TRUE"),
                Price = double.Parse("100")
            };

            _newcustomOption = new CustomOptionData()
            {
                Code = "R-QA Only CO Auto Update",
                Description = "Regression test create Custom Option Update",
                Structural = bool.Parse("FALSE"),
                Price = double.Parse("999")
            };

            ExtentReportsHelper.LogInformation(null, "Step 1: From ASSETS/CUSTOM OPTIONS, click the Custom Option to which you would like to select");
            CustomOptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.CustomOptions);
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, _customOption.Code);
            if (CustomOptionPage.Instance.IsItemInGrid("Code", _customOption.Code) is false)
            {
                CustomOptionPage.Instance.CreateCustomOption(_customOption);
                ExtentReportsHelper.LogInformation(null, "Step 2: Current is the custom option detail page, so ignore step 2.");
            }
            else
            {
                // Custom Option is existing on the grid view
                // Step 2: In Side Navigation, click the Details to open the Details page
                ExtentReportsHelper.LogInformation(null, "Step 2: In Side Navigation, click the Details to open the Details page");
                CustomOptionPage.Instance.SelectItemInGrid("Code", _customOption.Code);
                if(CustomOptionDetailPage.Instance.IsSaveCustomOptionSuccessful(_customOption.Code) is true)
                {
                    ExtentReportsHelper.LogPass("<font color='green'><b>Opened successfully the Custom Option Details page</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail("<font color='red'>Opened insuccessfully the Custom Option Details page</font>");
                }
                
            }
        }

        [Test]
        [Category("Section_IV")]
        public void A11_A_Assets_DetailPage_CustomOption_Details()
        {
            // Step 3: Edit the valid and verify the update successfully
            ExtentReportsHelper.LogInformation(null, "Step 3: Edit the valid and verify the update successfully");
            CustomOptionDetailPage.Instance.AddCustomOption(_newcustomOption);
            if(CustomOptionDetailPage.Instance.IsSaveCustomOptionSuccessful(_newcustomOption.Code) is true)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Edit the valid and verify the update successfully</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>Edit the valid and verify the update insuccessfully</font>");
            }

            ExtentReportsHelper.LogInformation(null, "Step 4: Verify on PathWay. This function will be implemented later.");

        }

        [TearDown]
        public void DeleteCustomOption()
        {
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_CUSTOM_OPTION_URL);
            BasePage.PageLoad();

            ExtentReportsHelper.LogInformation(null, "Step 5.1: Filter the new custom option then delete it.");
            CustomOptionPage.Instance.DeleteCustomOption(_newcustomOption);

            ExtentReportsHelper.LogInformation(null, "Step 5.2: Filter the old custom option then delete it.");
            CustomOptionPage.Instance.DeleteCustomOption(_customOption);
        }
    }
}
