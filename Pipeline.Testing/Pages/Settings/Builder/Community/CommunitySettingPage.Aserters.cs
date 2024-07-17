using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;

namespace Pipeline.Testing.Pages.Settings.Builder.Community
{
    public partial class CommunitySettingPage
    {
        public void VerifyTheCustomFieldSectionIsDisplay()
        {
            if (CommunityCustomFieldTitle_lbl.WaitForElementIsVisible(5))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(CommunityCustomFieldTitle_lbl), "The Section Community Custom Field is displayed as expectation.");
            }
            else
            {
                ExtentReportsHelper.LogFail("The Section Community Custom Field is NOT display as expectation.");
                //Assert.Fail();
            }
        }

        public void VerifyCannotCreateWithDuplicateValue(CustomFieldData data)
        {
            CustomFieldModal.Instance.VerifyCannotCreateWithDuplicateValue(data, CommunityCustomField_Grid, Xp_LoadingGifCommunityCustomField);
        }

        public void VerifyCannotUpdateWithDuplicateValue(string oldTitle, CustomFieldData newData)
        {
            CustomFieldModal.Instance.VerifyCannotUpdateWithDuplicateValue(oldTitle, newData, CommunityCustomField_Grid, Xp_LoadingGifCommunityCustomField);
        }

        public bool VerifyTitleFieldWithMessage(string expected)
        {
            return CustomFieldModal.Instance.VerifyTitleFieldWithMessage(expected);
        }

        public bool VerifyTagFieldWithMessage(string expected)
        {
            return CustomFieldModal.Instance.VerifyTagFieldWithMessage(expected);
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return CommunityCustomField_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool VerifyItemsInList(params CustomFieldData[] data)
        {
            bool isPassed = true;
            foreach (var item in data)
            {
                CommunityCustomField_Grid.FilterByColumn("Title", GridFilterOperator.Contains, item.Title);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFieldsToCommunity']/div[1]",4000);
                if (!CommunityCustomField_Grid.IsItemOnCurrentPage("Title", item.Title))
                    isPassed = false;
                if (!CommunityCustomField_Grid.IsItemOnCurrentPage("Description", item.Description))
                    isPassed = false;
                if (!CommunityCustomField_Grid.IsItemOnCurrentPage("Tag", item.Tag))
                    isPassed = false;
                if (!CommunityCustomField_Grid.IsItemOnCurrentPage("Field Type", item.FieldType))
                    isPassed = false;
            }

            return isPassed;
        }
    }
}
