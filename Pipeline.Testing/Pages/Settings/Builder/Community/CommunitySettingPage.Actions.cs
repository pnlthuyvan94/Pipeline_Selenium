using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Settings.Builder.Community
{
    public partial class CommunitySettingPage
    {
        public void SwichingToCommunityView()
        {
            SwitchView_ddl.SelectItem("Community", false, false);
            WaitingLoadingGifByXpath(Xp_LoadingWhenSwitchView);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(SwitchView_ddl), $"Select <font color='green'><b>\"Community\"</b></font> on the dropdown list.");
        }

        public CommunitySettingPage ShowAddCustomFieldModal()
        {
            CommunityCustomField_Grid.MoveGridToCenterScreen();
            Add_btn.Click();
            CustomFieldModal.Instance.WaitForModalDisplay();
            return this;
        }

        public void CreateCustomField(CustomFieldData data)
        {
            CustomFieldModal.Instance.CreateNewCustomField(data, Xp_LoadingGifCommunityCustomField);
        }

        public void UpdateCustomField(string oldTitle, CustomFieldData newData)
        {
            CustomFieldModal.Instance.UpdateCustomField(CommunityCustomField_Grid, Xp_LoadingGifCommunityCustomField, oldTitle, newData);
        }

        public void CloseModal()
        {
            CustomFieldModal.Instance.CloseModal();
        }

        public IList<CustomFieldData> GetAllDefaultCustomField()
        {
            CommunityCustomField_Grid.MoveGridToCenterScreen();
            return CustomFieldModal.Instance.GetAllDefaultCustomField(CommunityCustomField_Grid);
        }

        public CommunitySettingPage EnterDataForCustomField(CustomFieldData data)
        {
            CustomFieldModal.Instance.EnterDataForCustomField(data);
            return this;
        }

        public void DragAndDrop()
        {
            CustomFieldModal.Instance.DragAndDrop(CommunityCustomField_Grid, Xp_LoadingGifCommunityCustomField);
        }

        public void DeleteItemInGrid(string TitleItem)
        {
            //CommunityCustomField_Grid.FilterByColumn("Title", GridFilterOperator.Contains, TitleItem);
            CommunityCustomField_Grid.WaitGridLoad();

            CommunityCustomField_Grid.ClickDeleteItemInGrid("Title", TitleItem);
            ConfirmDialog(ConfirmType.OK);
            CommunityCustomField_Grid.WaitGridLoad();
        }

        public void ApplyDefault()
        {
            ApplyDefault_Btn.Click();
            CommunityCustomField_Grid.WaitGridLoad();
            ExtentReportsHelper.LogInformation("Log the information after appling default.");
            CloseToastMessage();
        }

        public IList<CustomFieldData> GetAllCustomField()
        {
            return CustomFieldModal.Instance.GetAllCustomField(CommunityCustomField_Grid);
        }
    }
}
