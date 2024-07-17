using NUnit.Framework;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;

namespace Pipeline.Testing.Pages.Settings.Builder.Option
{
    public partial class OptionSettingPage
    {
        public void VerifyTheCustomFieldSectionIsDisplay()
        {
            if (OptionCustomFieldTitle_lbl.WaitForElementIsVisible(5))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(OptionCustomFieldTitle_lbl), "The Section Option Custom Field is displayed as expectation.");
            }
            else
            {
                ExtentReportsHelper.LogFail("The Section Option Custom Field is NOT display as expectation.");
                Assert.Fail();
            }
        }

        public void VerifyCannotCreateWithDuplicateValue(CustomFieldData data)
        {
            CustomFieldModal.Instance.VerifyCannotCreateWithDuplicateValue(data, OptionCustomField_Grid, Xp_LoadingGifOptionCustomField);
        }

        public void VerifyCannotUpdateWithDuplicateValue(string oldTitle, CustomFieldData newData)
        {
            CustomFieldModal.Instance.UpdateCustomField(OptionCustomField_Grid, Xp_LoadingGifOptionCustomField, oldTitle, newData);
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
            return OptionCustomField_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool VerifyItemsInList(params CustomFieldData[] data)
        {
            bool isPassed = true;
            foreach (var item in data)
            {
                OptionCustomField_Grid.FilterByColumn("Title", GridFilterOperator.Contains, item.Title);
                WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFields']/div[1]",4000);
                if (!OptionCustomField_Grid.IsItemOnCurrentPage("Title", item.Title))
                    isPassed = false;
                if (!OptionCustomField_Grid.IsItemOnCurrentPage("Description", item.Description))
                    isPassed = false;
                if (!OptionCustomField_Grid.IsItemOnCurrentPage("Tag", item.Tag))
                    isPassed = false;
                if (!OptionCustomField_Grid.IsItemOnCurrentPage("Field Type", item.FieldType))
                    isPassed = false;
            }

            return isPassed;
        }
    }
}
