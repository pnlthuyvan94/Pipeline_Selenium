using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;

namespace Pipeline.Testing.Pages.Settings.Builder.House
{
    public partial class HouseSettingPage
    {
        public void VerifyTheCustomFieldSectionIsDisplay()
        {
            if (HouseCustomFieldTitle_lbl.WaitForElementIsVisible(5))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(HouseCustomFieldTitle_lbl), "The Section House Custom Field is displayed as expectation.");
            }
            else
            {
                ExtentReportsHelper.LogFail("The Section House Custom Field is NOT display as expectation.");
                //Assert.Fail();
            }
        }

        public void VerifyCannotCreateWithDuplicateValue(CustomFieldData data)
        {
            CustomFieldModal.Instance.VerifyCannotCreateWithDuplicateValue(data, HouseCustomField_Grid, Xp_LoadingGifHouseCustomField);
        }

        public void VerifyCannotUpdateWithDuplicateValue(string oldTitle, CustomFieldData newData)
        {
            CustomFieldModal.Instance.VerifyCannotUpdateWithDuplicateValue(oldTitle, newData, HouseCustomField_Grid, Xp_LoadingGifHouseCustomField);
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
            return HouseCustomField_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool VerifyItemsInList(params CustomFieldData[] data)
        {
            bool isPassed = true;
            foreach (var item in data)
            {
                HouseCustomField_Grid.FilterByColumn("Title", GridFilterOperator.Contains, item.Title);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFieldsToHouse']/div[1]",4000);
                if (!HouseCustomField_Grid.IsItemOnCurrentPage("Title", item.Title))
                    isPassed = false;
                if (!HouseCustomField_Grid.IsItemOnCurrentPage("Description", item.Description))
                    isPassed = false;
                if (!HouseCustomField_Grid.IsItemOnCurrentPage("Tag", item.Tag))
                    isPassed = false;
                if (!HouseCustomField_Grid.IsItemOnCurrentPage("Field Type", item.FieldType))
                    isPassed = false;
            }

            return isPassed;
        }
    }
}
