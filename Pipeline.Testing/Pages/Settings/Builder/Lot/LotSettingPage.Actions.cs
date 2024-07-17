using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.Builder.Lot
{
    public partial class LotSettingPage
    {

        public void SwichingToLotView()
        {
            SwitchView_ddl.SelectItem("Lot", false, false);
            WaitingLoadingGifByXpath(loadingXpathWhenSwitch);
            ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(SwitchView_ddl), $"Select \"Lot\" on the dropdown list.");
        }

        public LotSettingPage ShowAddLotStatusModal()
        {
            LotStatus_Grid.MoveGridToCenterScreen();
            AddNewLotStatus_Btn.Click();
            TitleModal_lbl.WaitForElementIsVisible(5);
            return this;
        }

        public LotSettingPage EnterLotStatusValue(string value)
        {
            LotStatus_txt.SetText(value);
            return this;
        }

        public void CloseModal()
        {
            CloseModal_Btn.Click();
            System.Threading.Thread.Sleep(500);
        }

        public void AddNewLotStatus()
        {
            Save_btn.Click();
            WaitingLoadingGifByXpath(lotStatusLoading);
        }

        public void EditLotStatus(string oldValue, string newValue)
        {
            LotStatus_Grid.ClickEditItemInGrid("Lot Status", oldValue);
            LotStatus_Grid.WaitGridLoad();
            // Find element
            Textbox textbox = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgStatuses_ctl00']/tbody/tr/td[1]/input[@value='{oldValue}']");
            textbox.SetText(newValue);

            AcceptChange_Btn.Click();
            WaitingLoadingGifByXpath(lotStatusLoading, 2000);
        }

        public void DeleteItemInGrid(string lotStatusName)
        {
            LotStatus_Grid.ClickDeleteItemInGrid("Lot Status", lotStatusName);
            ConfirmDialog(ConfirmType.OK);
            WaitingLoadingGifByXpath(lotStatusLoading);
        }

        public void NavigateBackToFirstPapge()
        {
            LotStatus_Grid.NavigateToPage(1);
        }

        public void CreateSettingLotStatus(string originalLotStatusName)
        {
            ShowAddLotStatusModal()
                .EnterLotStatusValue(originalLotStatusName)
                .AddNewLotStatus();

            // Verify the message
            string successfulMess = $"Status name successfully added.";
            if (successfulMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass(null, "<font color='green'><b>Status name successfully added.</b></font>");
                CloseToastMessage();
            }

            // Verify the new Lot status is displayed on grid
            if (!IsItemDisplayOnScreen(originalLotStatusName))
            {
                ExtentReportsHelper.LogFail("<font color='red'>Status name does not add.</font>");
            }
        }

        public void DeleteSettingStatus(string lotStatusName)
        {
            // Click delete item on grid
            DeleteItemInGrid(lotStatusName);

            // Verify the delete successfully
            string expect_successfulMess = $"Status successfully removed.";
            string actual_successfulMess = GetLastestToastMessage();
            if (actual_successfulMess == expect_successfulMess)
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>Status successfully removed.</b></font>");
            }

            // Verify the items is existing on the grid or not

            if (IsItemDisplayOnScreen(lotStatusName) is true)
            {
                ExtentReportsHelper.LogFail("<font color='red'>Status name can't be removed.</font>");
            }
        }
    }

}
