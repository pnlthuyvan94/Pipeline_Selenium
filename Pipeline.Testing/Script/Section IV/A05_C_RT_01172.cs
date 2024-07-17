using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Options;
using Pipeline.Testing.Pages.Assets.Options.OptionDetail;
using Pipeline.Testing.Pages.CustomField;
using Pipeline.Testing.Pages.Settings.Builder.Option;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A05_C_RT_01172 : BaseTestScript
    {
        private IList<CustomFieldData> datas;
        private IList<CustomFieldData> newData;
        private OptionData _option;
        private readonly string TitleCustomField = "Option Custom Fields";

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        [SetUp]
        public void GetData()
        {

            var optionType = new List<bool>()
            {
                true, true, true
            };

            _option = new OptionData()
            {
                Name = "R-QA Only Option Auto",
                Number = "13579",
                SquareFootage = 10000,
                Description = "Regression Test Create Option",
                SaleDescription = "Create Option Sale Description",
                OptionGroup = "Windows",
                OptionRoom = string.Empty,
                CostGroup = "All Rooms",
                OptionType = "Design",
                Price = 100,
                Types = optionType
            };

            // Custom Field will added to this page
            newData = new List<CustomFieldData>(){
                    new CustomFieldData
                    {
                        Title ="Create CF Opt",
                        Description="Create new Custom Field - Option",
                        Tag="CCFO",
                        FieldType="Text Area",
                        Default=false,
                        Value=""
                    }
            };

            // Get all Custom field from setting page
            ExtentReportsHelper.LogInformation(null, "*********** Go to Option Setting page and add new Custom Field ***********");

            OptionSettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            OptionSettingPage.Instance.LeftMenuNavigation("Builder");
            OptionSettingPage.Instance.SwichingToOptionView();

            AddNewCustomField();

            datas = OptionSettingPage.Instance.GetAllCustomField();
        }

        [Test]
        [Category("Section_IV")]
        public void A05_C_Assets_DetailPage_Options_CustomFields()
        {
            ExtentReportsHelper.LogInformation(null, "*********** Go to the Option Default page and Navigate to Option Details Page ***********");
            // Go to Option page
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_OPTION_URL);
            System.Threading.Thread.Sleep(5000);

            // Filter item in grid
            OptionPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _option.Name);
            System.Threading.Thread.Sleep(2000);

            if (OptionPage.Instance.IsItemInGrid("Name", _option.Name) is false)
            {
                // Create a new option if it's not existing
                OptionPage.Instance.CreateNewOption(_option);
            }
            else
            {
                // Go to option detail page
                OptionPage.Instance.SelectItemInGridWithTextContains("Name", _option.Name);
            }


            ExtentReportsHelper.LogInformation(null, "*********** Go to Custom Field Detail page ***********");
            // Go to Custom Field page
            OptionDetailPage.Instance.LeftMenuNavigation("Custom Fields");

            ExtentReportsHelper.LogInformation(null, "*********** Verify the header title of Custom Field Detail ***********");
            // Verify page displayed
            Assert.That(CustomFieldDetailPage.Instance.VerifyTheTitleOnCustomFieldCorrect(TitleCustomField));
            CustomFieldDetailPage.Instance.VerifyTheTitleOnCustomFieldCorrect(TitleCustomField);

            // Get all item in current page
            ExtentReportsHelper.LogInformation(null, "*********** Get current Custom Fields on this page, and then remove all. ***********");
            var items = CustomFieldDetailPage.Instance.GetCurrentItems();
            if (items.Count > 0)
            {
                // Remove all items in Custom field page
                CustomFieldDetailPage.Instance.RemovingCustomField(items).Save();

                CustomFieldDetailPage.Instance.CloseToastMessage();
                // Refresh page and verify no item in this page
                CustomFieldDetailPage.Instance.RefreshPage();

                // Verify all item on custom field setting page is displayed on the list
                items = CustomFieldDetailPage.Instance.GetCurrentItems();
            }
            if (items.Count.Equals(0) is true)
            {
                ExtentReportsHelper.LogInformation(null, "*********** Open Add Custom Field Modal and verify the list items are displayed as expected. ***********");
                // Add new custom field to this page
                CustomFieldDetailPage.Instance.ShowAddCustomFieldModal();

                // Verify the list of custom field is displayed correctly
                CustomFieldDetailPage.Instance.VerifyCustomItemsInList(datas);

                ExtentReportsHelper.LogInformation(null, "*********** Select Custom Field in List and try to add to this page. ***********");
                // Select the custom field
                CustomFieldDetailPage.Instance.SelectCustomFieldsOnModal(newData).InsertTheSelectedCustomFieldToThisPage();

                // Save
                CustomFieldDetailPage.Instance.Save();
                string _expectedMessage = "Successfully updated Option Custom Fields.";
                if (CustomFieldDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogPass($"Successfully updated Option Custom Fields.");
                    CustomFieldDetailPage.Instance.CloseToastMessage();
                }

                ExtentReportsHelper.LogInformation(null, "*********** Refresh page and verify the item is added successfully. ***********");
                // Refresh page and verify no item in this page
                CustomFieldDetailPage.Instance.RefreshPage();

                // Verify all item on custom field setting page is displayed on the list
                bool isEqual = CustomFieldDetailPage.Instance.VerifyCustomFieldIsDisplayedWithCorrectItems(newData.First());
                if (!isEqual)
                {
                    IList<string> listActual = items.Select(c => c.Title).ToList();
                    ExtentReportsHelper.LogFail($"The actual item in list is not as expect <br>Actual: <font color='green'>{CommonHelper.CastListToString(listActual)}</font><br>Expected:<font color='green'>{newData.First().Title}</font>");
                }
                ExtentReportsHelper.LogPass($"Successfully added Option Custom Fields <br><font color='green'>{newData.First().Title}</font>");

                string valueToUpdate = "Add a value to this field";

                // Added value of Custom field
                ExtentReportsHelper.LogInformation(null, "*********** Try to add the value to Custom Field. ***********");
                CustomFieldDetailPage.Instance.EnterValueToField(newData.First().Title, valueToUpdate).Save();

                _expectedMessage = "Successfully updated Option Custom Fields.";
                if (CustomFieldDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
                {
                    ExtentReportsHelper.LogPass($"Successfully updated Option Custom Fields.");
                    CustomFieldDetailPage.Instance.CloseToastMessage();
                }

                // Refresh page and verify no item in this page
                ExtentReportsHelper.LogInformation(null, "*********** Refresh page and verify the item updated value successfully on detail page. ***********");
                CustomFieldDetailPage.Instance.RefreshPage();
                // Get the item in list
                items = CustomFieldDetailPage.Instance.GetCurrentItems();

                // Verify the value saved successfully
                if (items.First().Value.Equals(valueToUpdate))
                    ExtentReportsHelper.LogPass($"Successfully added value <b><font color='green'>{items.First().Value}</font></b> to Option Custom Fields.");
                else
                    ExtentReportsHelper.LogFail($"Could not add the value to Option Custom Fields <br>Actual: <font color='green'>{items.First().Value}</font>.<br>Expected:  <font color='green'>{valueToUpdate}</font>");

                // Remove the item and verify
                CustomFieldDetailPage.Instance.RemovingCustomField(newData).Save();
                CustomFieldDetailPage.Instance.CloseToastMessage();

                ExtentReportsHelper.LogInformation(null, "*********** Refresh page and verify the item removed successfully on detail page. ***********");
                // Refresh page and verify no item in this page
                CustomFieldDetailPage.Instance.RefreshPage();
                // Verify the items does not exist on this page
                CustomFieldDetailPage.Instance.VerifyCustomFieldIsNOTDisplayedInPage(newData.First());
            }
        }

        [TearDown]
        public void RemoveDataOnSetting()
        {
            ExtentReportsHelper.LogInformation(null, "============================== Tear Down ==============================");
            ExtentReportsHelper.LogInformation(null, "*********** Delete Option ***********");
            OptionPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Options);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.Contains, _option.Number);
            OptionPage.Instance.DeleteOption(_option.Name);
            OptionPage.Instance.FilterItemInGrid("Number", GridFilterOperator.NoFilter, string.Empty);
            ExtentReportsHelper.LogInformation(null, "*********** Remove added Custom Field on Setting page for the Next run ***********");
            // Get all Custom field from setting page
            OptionSettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            OptionSettingPage.Instance.LeftMenuNavigation("Builder");
            OptionSettingPage.Instance.SwichingToOptionView();
            OptionSettingPage.Instance.DeleteItemInGrid(newData.First().Title);
            OptionSettingPage.Instance.GetLastestToastMessage();
            ExtentReportsHelper.LogInformation("================= End of this test case =================");
            OptionSettingPage.Instance.CloseToastMessage();
            
        }

        private void AddNewCustomField()
        {
            if (OptionSettingPage.Instance.IsItemInGrid("Title", newData.First().Title) is false)
            {
                OptionSettingPage.Instance.ShowAddCustomFieldModal().CreateCustomField(newData.First());
                OptionSettingPage.Instance.CloseToastMessage();
                //OptionSettingPage.Instance.CloseModal();
            } else
                ExtentReportsHelper.LogInformation(null, $"Custom field with title {newData.First().Title} is existing on the grid." +
                    $" Don't need to create a new one.");
        }
    }
}
