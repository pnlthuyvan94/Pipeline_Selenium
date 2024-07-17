using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.House;
using Pipeline.Testing.Pages.Assets.House.HouseDetail;
using Pipeline.Testing.Pages.CustomField;
using Pipeline.Testing.Pages.Settings.Builder.House;
using Pipeline.Testing.Pages.Settings.CustomField;
using Pipeline.Testing.Script.TestData;
using System.Collections.Generic;

namespace Pipeline.Testing.Script.Section_IV
{
    class A04_G_PIPE_30937 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        //private const string X_BUTTONS_XPATH = "//a[@class = 'internal-button']";
        private const string TEST_NUMBER_INPUT = "5";
        private const string STATIC_TEST_TYPE = "TestStatic";
        private const string TEST_NUMBER = "TestNumber";
        private const string TEST_CHECKBOX = "TestCheckBox";

        CustomFieldData _customFieldData0;
        CustomFieldData _customFieldData1;
        CustomFieldData _customFieldData2;
        List<CustomFieldData> _listcustomFieldData;

        [SetUp]
        public void SetupData()
        {
            _customFieldData0 = new CustomFieldData()
            {
                Title = "TestNumber",
                Description = "Create new 1",
                Tag = "RTOne",
                FieldType = "Number",
                Default = true,
                Note = "Create new 1"
            };
            _customFieldData1 = new CustomFieldData()
            {
                Title = "TestCheckBox",
                Description = null,
                Tag = "RTTwo",
                FieldType = "Checkbox",
                Default = true,
                Note = "Create new 2"
            };
            _customFieldData2 = new CustomFieldData()
            {
                Title = "TestStatic",
                Description = "For static",
                Tag = "Forstatic",
                FieldType = "Static",
                Default = true,
                Note = "Create new 3",
            };
            _listcustomFieldData = new List<CustomFieldData>()
            {
                _customFieldData0,_customFieldData1,_customFieldData2
            };
        }


        [Test]
        [Category("Section_IV")]
        public void A04_G_Assets_Detail_Pages_Houses_Custom_Fields ()
        {
            //Step 0: Set House Page so that to appear custom fields
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 0: Set House Page so that to appear custom fields </font>");
            HouseSettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            HouseSettingPage.Instance.LeftMenuNavigation("Builder");
            HouseSettingPage.Instance.SwichingToHouseView();            
            //Create three custom fields if need
            if (!HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData0.Title))
            {
                // Filter item created
                HouseSettingPage.Instance.ShowAddCustomFieldModal();
                HouseSettingPage.Instance.CreateCustomField(_listcustomFieldData[0]);               
            }
            if (!HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData1.Title))
            {
                // Filter item created
                HouseSettingPage.Instance.ShowAddCustomFieldModal();
                HouseSettingPage.Instance.CreateCustomField(_listcustomFieldData[1]);              
            }
            if (!HouseSettingPage.Instance.IsItemInGrid("Title", _customFieldData2.Title))
            {
                // Filter item created
                HouseSettingPage.Instance.ShowAddCustomFieldModal();
                HouseSettingPage.Instance.CreateCustomField(_listcustomFieldData[2]);
            }
            
            //Step 1: Go to a house and stand at 'Customs field'
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 1: Go to a house and stand at 'Customs field'</font>");
            HousePage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Houses);
            HousePage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains,TestDataCommon.HOUSE_NAME_DEFAULT);
            HousePage.Instance.SelectItemInGridWithTextContains("Name", TestDataCommon.HOUSE_NAME_DEFAULT);
            HouseDetailPage.Instance.LeftMenuNavigation("Custom Fields");
            string CustomField_url = HouseDetailPage.Instance.CurrentURL;
            //Step 2: Click '+' to add custom field into and set 'Check All'
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 2: Click '+' to add custom field into and set 'Check All'</font>");
            CustomFieldDetailPage.Instance.ShowAddCustomFieldModal();
            CustomFieldDetailPage.Instance.SetHouseCustomFieldCheckAll(true);
            CustomFieldDetailPage.Instance.InsertTheSelectedCustomFieldToThisPage();
            //Step 3: Input values into testnumber and set check Testcheckbox
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 3: Input values into testnumber and set check Testcheckbox</font>");
            CustomFieldDetailPage.Instance.EnterValueToField(TEST_NUMBER, TEST_NUMBER_INPUT);
            CustomFieldDetailPage.Instance.EnterValueToField(TEST_CHECKBOX, true).Save();
            //Step 4: Verify toast message shows correctly
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 4: Verify toast message shows correctly</font>");
            if (CustomFieldDetailPage.Instance.GetLastestToastMessage().Trim().Equals("Successfully updated House Custom Fields."))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Toast message displays correctly, it is: '{CustomFieldDetailPage.Instance.GetLastestToastMessage()}'</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>Toast message does not display correctly, it is: '{CustomFieldDetailPage.Instance.GetLastestToastMessage()}'</b></font>");
            }

            //Back to custom field
            CommonHelper.OpenURL(CustomField_url);
            //Step 5: Verify values input successfully into custom field of the house
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'><b>Step 5: Verify values input successfully into custom field of the house</b></font>");
            CustomFieldDetailPage.Instance.VerifyHouseCustomFieldValues(TEST_NUMBER_INPUT, true, STATIC_TEST_TYPE);
            
            //Step 6: click 'Remove Custom Fields' button and click on these buttons to drop these x fields
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 6: click 'Remove Custom Fields' button and count three 'x' buttons and click on these buttons to drop these x fields</font>");
            CustomFieldDetailPage.Instance.ClickHouseRemoveCustomField();           
            //Click on three buttons            
            CustomFieldDetailPage.Instance.DeleteCustomeField("TestNumber");
            CustomFieldDetailPage.Instance.DeleteCustomeField("TestCheckBox");
            CustomFieldDetailPage.Instance.DeleteCustomeField("Static");
            CustomFieldDetailPage.Instance.Save();
           
            //Step 7: Verify toast message shows correctly
            ExtentReportsHelper.LogInformation(null, $"<font color='lavender'> Step 7: Verify toast message shows correctly</font>");
            if (CustomFieldDetailPage.Instance.GetLastestToastMessage().Trim().Equals("Successfully updated House Custom Fields."))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Toast message displays correctly, it is: {CustomFieldDetailPage.Instance.GetLastestToastMessage()}</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'><b>Toast message does not display correctly, it is: {CustomFieldDetailPage.Instance.GetLastestToastMessage()}</b></font>");
            }
            
        }
    }
}
