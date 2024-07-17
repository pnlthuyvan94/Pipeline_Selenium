using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Settings.Builder.Option
{
    public partial class OptionSettingPage
    {
        public void SwichingToOptionView()
        {
            SwitchView_ddl.SelectItem("Option", false, false);
            WaitingLoadingGifByXpath(Xp_LoadingWhenSwitchView);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(SwitchView_ddl), $"Select <font color='green'><b>\"Option\"</b></font> on the dropdown list.");
        }

        public OptionSettingPage ShowAddCustomFieldModal()
        {
            OptionCustomField_Grid.MoveGridToCenterScreen();
            Add_btn.Click();
            CustomFieldModal.Instance.WaitForModalDisplay();
            return this;
        }

        public void CreateCustomField(CustomFieldData data)
        {
            CustomFieldModal.Instance.CreateNewCustomField(data, Xp_LoadingGifOptionCustomField);
        }

        public void UpdateCustomField(string oldTitle, CustomFieldData newData)
        {
            CustomFieldModal.Instance.UpdateCustomField(OptionCustomField_Grid, Xp_LoadingGifOptionCustomField, oldTitle, newData);
        }

        public void CloseModal()
        {
            CustomFieldModal.Instance.CloseModal();
        }

        public IList<CustomFieldData> GetAllDefaultCustomField()
        {
            OptionCustomField_Grid.MoveGridToCenterScreen();
            return CustomFieldModal.Instance.GetAllDefaultCustomField(OptionCustomField_Grid);
        }

        public OptionSettingPage EnterDataForCustomField(CustomFieldData data)
        {
            CustomFieldModal.Instance.EnterDataForCustomField(data);
            return this;
        }

        public void DragAndDrop()
        {
            CustomFieldModal.Instance.DragAndDrop(OptionCustomField_Grid, Xp_LoadingGifOptionCustomField);
        }

        public void DeleteItemInGrid(string TitleItem)
        {
           // OptionCustomField_Grid.FilterByColumn("Title", GridFilterOperator.Contains, TitleItem);
            OptionCustomField_Grid.ClickDeleteItemInGrid("Title", TitleItem);
            ConfirmDialog(ConfirmType.OK);
            OptionCustomField_Grid.WaitGridLoad();
        }

        public void ApplyDefault()
        {
            ApplyDefault_Btn.Click();
            OptionCustomField_Grid.WaitGridLoad();
            ExtentReportsHelper.LogInformation("Log the information after appling default.");
            CloseToastMessage();
        }

        public IList<CustomFieldData> GetAllCustomField()
        {
            return CustomFieldModal.Instance.GetAllCustomField(OptionCustomField_Grid);
        }

        public void SelectOptionDisplayFormat(OptionDisplayFormat format)
        {
            OptionDisplayFormat_ddl.SelectItemByValue(format.ToString("g"));
            SaveOptionSetting_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_secOption']/div[1]");
        }
    }
}
