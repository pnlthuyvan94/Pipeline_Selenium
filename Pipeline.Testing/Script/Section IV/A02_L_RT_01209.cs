using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Constants;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.Assets.Communities;
using Pipeline.Testing.Pages.Assets.Communities.CommunityDetail;
using Pipeline.Testing.Pages.CustomField;
using Pipeline.Testing.Pages.Settings.Builder.Community;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;
using System.Linq;

namespace Pipeline.Testing.Script.Section_IV
{
    public class A02_L_RT_01209 : BaseTestScript
    {
        private IList<CustomFieldData> datas;
        private IList<CustomFieldData> newData;
        private CommunityData communityData;

        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_IV);
        }

        [SetUp]
        public void GetData()
        {
            // Custom Field will added to this page
            newData = new List<CustomFieldData>(){
                    new CustomFieldData
                    {
                        Title ="Create CF Com",
                        Description="Create new Custom Field - Community",
                        Tag="CCFC",
                        FieldType="Text Area",
                        Default=false,
                        Value=""
                    }
            };

            // Get all Custom field from setting page
            ExtentReportsHelper.LogInformation(null, "*********** Go to Community Setting page and add new Custom Field ***********");

            CommunitySettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            CommunitySettingPage.Instance.LeftMenuNavigation("Builder");
            CommunitySettingPage.Instance.SwichingToCommunityView();
            AddNewCustomField();

            datas = CommunitySettingPage.Instance.GetAllCustomField();

            communityData = new CommunityData()
            {
                Name = "R-QA Only Community Auto_Test_CustomField",
                Division = "None",
                City = "Ho Chi Minh",
                CityLink = "https://hcm.com",
                Township = "Tan Binh",
                County = "VN",
                State = "IN",
                Zip = "01010",
                SchoolDistrict = "Hoang hoa tham",
                SchoolDistrictLink = "http://hht.com",
                Status = "Open",
                Description = "Community from automation test v1",
                DrivingDirections = "Community from automation test v2",
                Slug = "R-QA-Only-Community-Auto - slug",
            };
        }

        [Test]
        [Category("Section_IV")]
        public void A02_L_Assets_DetailPage_Communities_CustomFields()
        {
            // Step 1: Navigate to Community page
            ExtentReportsHelper.LogInformation(null, "*********** Navigate to Community default page.***********");
            CommunityPage.Instance.SelectMenu(MenuItems.ASSETS).SelectItem(AssetsMenu.Communities);

            // Step 2: Insert name to filter and click filter by Contain value
            ExtentReportsHelper.LogInformation(null, $"*********** Filter community with name {communityData.Name} and create if it doesn't exist.***********");
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (!CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
            {
                // Create a new community
                CommunityPage.Instance.CreateCommunity(communityData);
            }
            else
            {
                // Select filter item to open detail page
                CommunityPage.Instance.SelectItemInGrid("Name", communityData.Name);
            }


            ExtentReportsHelper.LogInformation(null, "*********** Go to Custom Field Detail page ***********");
            // Go to Custom Field page
            CommunityDetailPage.Instance.LeftMenuNavigation("Custom Fields");

            ExtentReportsHelper.LogInformation(null, "*********** Verify the header title of Custom Field Detail ***********");
            // Verify page displayed
            CustomFieldDetailPage.Instance.VerifyTheTitleOnCustomFieldCorrect("Community Custom Fields");

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
            if (items.Count.Equals(0) is false)
                ExtentReportsHelper.LogFail($"The document <font color='red'><b>The number custom fields are incorrect.</b></font>");


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
            string _expectedMessage = "Successfully updated Community Custom Fields.";
            if (CustomFieldDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Successfully updated Community Custom Fields.");
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
            ExtentReportsHelper.LogPass($"Successfully added Community Custom Fields <br><font color='green'>{newData.First().Title}</font>");

            string valueToUpdate = "Add a value to this field";

            // Added value of Custom field
            ExtentReportsHelper.LogInformation(null, "*********** Try to add the value to Custom Field. ***********");
            CustomFieldDetailPage.Instance.EnterValueToField(newData.First().Title, valueToUpdate).Save();

            _expectedMessage = "Successfully updated Community Custom Fields.";
            if (CustomFieldDetailPage.Instance.GetLastestToastMessage() == _expectedMessage)
            {
                ExtentReportsHelper.LogPass($"Successfully updated Community Custom Fields.");
                CustomFieldDetailPage.Instance.CloseToastMessage();
            }

            // Refresh page and verify no item in this page
            ExtentReportsHelper.LogInformation(null, "*********** Refresh page and verify the item updated value successfully on detail page. ***********");
            CustomFieldDetailPage.Instance.RefreshPage();
            // Get the item in list
            items = CustomFieldDetailPage.Instance.GetCurrentItems();

            // Verify the value saved successfully
            if (items.First().Value.Equals(valueToUpdate))
                ExtentReportsHelper.LogPass($"Successfully added value <b><font color='green'>{items.First().Value}</font></b> to Community Custom Fields.");
            else
                ExtentReportsHelper.LogFail($"Could not add the value to Community Custom Fields <br>Actual: <font color='green'>{items.First().Value}</font>.<br>Expected:  <font color='green'>{valueToUpdate}</font>");

            // Remove the item and verify
            CustomFieldDetailPage.Instance.RemovingCustomField(newData).Save();
            CustomFieldDetailPage.Instance.CloseToastMessage();

            ExtentReportsHelper.LogInformation(null, "*********** Refresh page and verify the item removed successfully on detail page. ***********");
            // Refresh page and verify no item in this page
            CustomFieldDetailPage.Instance.RefreshPage();
            // Verify the items does not exist on this page
            CustomFieldDetailPage.Instance.VerifyCustomFieldIsNOTDisplayedInPage(newData.First());
        }


        [TearDown]
        public void RemoveDataOnSetting()
        {
            ExtentReportsHelper.LogInformation(null, "*********** Remove added Custom Field on Setting page for the Next run ***********");
            // Get all Custom field from setting page
            CommunitySettingPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.Settings);
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.ADMIN_SETTINGS_URL);
            CommunitySettingPage.Instance.LeftMenuNavigation("Builder");
            CommunitySettingPage.Instance.SwichingToCommunityView();
            CommunitySettingPage.Instance.DeleteItemInGrid(newData.First().Title);
            CommunitySettingPage.Instance.GetLastestToastMessage();
            ExtentReportsHelper.LogInformation("================= End of this test case =================");
            CommunitySettingPage.Instance.CloseToastMessage();

            // Back to Community default page and delete data
            ExtentReportsHelper.LogInformation(null, $"<=================  Back to Community default page and delete data.================= ");
            CommonHelper.OpenURL(BaseDashboardUrl + BaseMenuUrls.VIEW_COMMUNITY_URL);

            // Filter community then delete it
            CommunityPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, communityData.Name);
            if (CommunityPage.Instance.IsItemInGrid("Name", communityData.Name))
                CommunityPage.Instance.DeleteCommunity(communityData.Name);
        }

        private void AddNewCustomField()
        {
            CommunitySettingPage.Instance.ShowAddCustomFieldModal().CreateCustomField(newData.First());
            CommunitySettingPage.Instance.CloseToastMessage();
            CommunitySettingPage.Instance.CloseModal();
        }
    }
}
