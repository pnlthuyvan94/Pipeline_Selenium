using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.CustomOptions;
using Pipeline.Testing.Pages.Assets.CustomOptions.CustomOptionDetail;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class A09_RT_01080 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        CustomOptionData _customOption;

        [SetUp]
        public void CreateTestData()
        {
            // Step 3: Populate all values
            _customOption = new CustomOptionData()
            {
                Code = "R-QA Only CO Auto",
                Description = "Regression test create Custom Option",
                Structural = bool.Parse("TRUE"),
                Price = double.Parse("100")
            };
        }

        [Test]
        [Category("Section_III")]
        public void A09_Assets_AddACustomOption()
        {
            // Step 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Builder/Options/CustomOptions.aspx
            CustomOptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.CustomOptions);

            // Step 2: click on "+" Add button
            CustomOptionPage.Instance.GetItemOnHeader(DashboardContentItems.Add).Click();
            var _expectedCreateCOURL = BaseDashboardUrl + BaseMenuUrls.CREATE_NEW_CUSTOM_OPTION_URL;
            if (CustomOptionDetailPage.Instance.IsPageDisplayed(_expectedCreateCOURL) is false)
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Custom Option create page isn't displayed.</font>");
            }
            else
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Custom Option create page is displayed</b></font>");
            }
            // Create CO - Click 'Save' Button
            CustomOptionDetailPage.Instance.AddCustomOption(_customOption);
            string _expectedMessage = $"Could not create Custom Option with name {_customOption.Code}.";
            string actualMsg = CustomOptionDetailPage.Instance.GetLastestToastMessage();
            if (_expectedMessage.Equals(actualMsg))
            {
                ExtentReportsHelper.LogFail($"Could not create Custom Option with name { _customOption.Code}.");
                CustomOptionDetailPage.Instance.CloseToastMessage();
            }
            else 
            {
                ExtentReportsHelper.LogInformation($"Actual message: {actualMsg}");
                CustomOptionDetailPage.Instance.CloseToastMessage();
            }

            BasePage.PageLoad();
            // Step 4. Verify new CO in header
            if (!CustomOptionDetailPage.Instance.IsSaveCustomOptionSuccessful(_customOption.Code))
            {
                ExtentReportsHelper.LogFail($"Create new Custom Option unsuccessfully.");
            }
            ExtentReportsHelper.LogPass($"Custom Option is created sucessfully with URL: {CustomOptionDetailPage.Instance.CurrentURL}");

            // Step 5. Verify data saved successfully
            Assert.That(CustomOptionDetailPage.Instance.IsSaveCustomOptionData(_customOption), "New Custom Option created with incorrect data.");
            ExtentReportsHelper.LogPass($"Custom Option is create sucessfully with valid data");

            // Step 6. Back to list of Division and verify new item in grid view
            CustomOptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.CustomOptions);

            // Insert name to filter and click filter by Contain value
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, _customOption.Code);

            if(CustomOptionPage.Instance.IsItemInGrid("Code", _customOption.Code) is false)
            {
                ExtentReportsHelper.LogFail($"New Custom Option \"{_customOption.Code} \" was not display on grid.");
            }
            else
            {
                ExtentReportsHelper.LogPass($"New Custom Option \"{_customOption.Code} \" is display on grid.");
            }
        }

        [TearDown]
        public void DeleteCustomOption()
        {
            CustomOptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.CustomOptions);
            CustomOptionPage.Instance.FilterItemInGrid("Code", GridFilterOperator.Contains, _customOption.Code);

            if (CustomOptionPage.Instance.IsItemInGrid("Code", _customOption.Code))
            {
                // 7. Select item and click deletete icon
                CustomOptionPage.Instance.DeleteItemInGrid("Code", _customOption.Code);
                CustomOptionPage.Instance.WaitGridLoad();
                string successfulMess = $"Custom Option {_customOption.Code} deleted successfully!";
                string actualMsg = CustomOptionPage.Instance.GetLastestToastMessage();
                if (successfulMess.Equals(actualMsg))
                {
                    ExtentReportsHelper.LogPass("Custom Option deleted successfully!");
                    CustomOptionPage.Instance.CloseToastMessage();
                }
                else 
                {
                    if (CustomOptionPage.Instance.IsItemInGrid("Code", _customOption.Code))
                        ExtentReportsHelper.LogPass("Custom Option could not be deleted!");
                    else
                        ExtentReportsHelper.LogPass("Custom Option deleted successfully!");
                }
            }
        }
    }
}
