using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Settings.Builder.Product
{
    public partial class ProductSettingPage
    {

        public ProductSettingPage ShowAddCustomFieldModal()
        {
            ProductCustomField_Grid.MoveGridToCenterScreen();
            Add_btn.Click();
            CustomFieldModal.Instance.WaitForModalDisplay();
            return this;
        }

        public void CreateCustomField(CustomFieldData data)
        {
            CustomFieldModal.Instance.CreateNewCustomField(data, Xp_LoadingGifProductCustomField);
        }

        public void UpdateCustomField(string oldTitle, CustomFieldData newData)
        {
            CustomFieldModal.Instance.UpdateCustomField(ProductCustomField_Grid, Xp_LoadingGifProductCustomField, oldTitle, newData);
        }

        public void CloseModal()
        {
            CustomFieldModal.Instance.CloseModal();
        }

        public IList<CustomFieldData> GetAllDefaultCustomField()
        {
            ProductCustomField_Grid.MoveGridToCenterScreen();
            return CustomFieldModal.Instance.GetAllDefaultCustomField(ProductCustomField_Grid);
        }

        public ProductSettingPage EnterDataForCustomField(CustomFieldData data)
        {
            CustomFieldModal.Instance.EnterDataForCustomField(data);
            return this;
        }

        public void DragAndDrop()
        {
            CustomFieldModal.Instance.DragAndDrop(ProductCustomField_Grid, Xp_LoadingGifProductCustomField);
        }

        public void DeleteItemInGrid(string TitleItem)
        {
            ProductCustomField_Grid.FilterByColumn("Title", GridFilterOperator.Contains, TitleItem);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFields']/div[1]", 5000);
            ProductCustomField_Grid.ClickDeleteItemInGrid("Title", TitleItem);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFields']/div[1]");
        }

        public void ApplyDefault()
        {
            ApplyDefault_Btn.Click();
            WaitingLoadingGifByXpath("/*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCustomFields']/div[1]");
            ExtentReportsHelper.LogInformation("Log the information after appling default.");
            CloseToastMessage();
        }
    }
}
