using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.Settings.CustomField;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Settings.Builder.House
{
    public partial class HouseSettingPage
    {
        public void SwichingToHouseView()
        {
            SwitchView_ddl.SelectItem("House", false, false);
            WaitingLoadingGifByXpath(Xp_LoadingWhenSwitchView);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(SwitchView_ddl), $"Select <font color='green'><b>\"House\"</b></font> on the dropdown list.");
        }

        public HouseSettingPage ShowAddCustomFieldModal()
        {
            HouseCustomField_Grid.MoveGridToCenterScreen();
            Add_btn.Click();
            CustomFieldModal.Instance.WaitForModalDisplay();
            return this;
        }

        public void CreateCustomField(CustomFieldData data )
        {
            CustomFieldModal.Instance.CreateNewCustomField(data, Xp_LoadingGifHouseCustomField);
        }

        public void UpdateCustomField(string oldTitle, CustomFieldData newData)
        {
            CustomFieldModal.Instance.UpdateCustomField(HouseCustomField_Grid, Xp_LoadingGifHouseCustomField, oldTitle, newData);
        }

        public void CloseModal()
        {
            CustomFieldModal.Instance.CloseModal();
        }

        public IList<CustomFieldData> GetAllDefaultCustomField()
        {
            HouseCustomField_Grid.MoveGridToCenterScreen();
            return CustomFieldModal.Instance.GetAllDefaultCustomField(HouseCustomField_Grid);
        }

        public HouseSettingPage EnterDataForCustomField(CustomFieldData data)
        {
            CustomFieldModal.Instance.EnterDataForCustomField(data);
            return this;
        }

        public void DragAndDrop()
        {
            CustomFieldModal.Instance.DragAndDrop(HouseCustomField_Grid, Xp_LoadingGifHouseCustomField);
        }

        public void DeleteItemInGrid(string TitleItem)
        {
            //HouseCustomField_Grid.FilterByColumn("Title", GridFilterOperator.Contains, TitleItem);
            HouseCustomField_Grid.ClickDeleteItemInGrid("Title", TitleItem);
            ConfirmDialog(ConfirmType.OK);
            HouseCustomField_Grid.WaitGridLoad();
        }

        public void ApplyDefault()
        {
            ApplyDefault_Btn.Click();
            HouseCustomField_Grid.WaitGridLoad();
            ExtentReportsHelper.LogInformation("Log the information after appling default.");
            CloseToastMessage();
        }
    }
}
