using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Settings.Sage300CRE
{
    public partial class Sage300CREPage
    {
        /// <summary>
        /// Change Sage status to Running or Pause
        /// </summary>
        /// <param name="isRunning"></param>
        public void SetSage300Status(bool isRunning)
        {
            bool isCaptured = false;
            if (isRunning is true)
            {
                // Set Sage 300 to running
                Running_btn.Click(isCaptured);
            }
            else
            {
                // Set Sage 300 to pause
                Paused_btn.Click(isCaptured);
            }

            Save_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSave']");

            string actualMess = GetLastestToastMessage();
            string expectedMess = "Changes saved!";
            if(actualMess.ToLower().Equals(expectedMess.ToLower()))
                ExtentReportsHelper.LogPass($"<font color ='green'><b>MS Sage 300 Integration Settings saved successfully.</b></font>");

            else
                ExtentReportsHelper.LogInformation($"<font color ='red'>MS Sage 300 Integration Settings failed to save." +
                    $"<br>Expected message: {expectedMess}" +
                    $"<br>Actual message: {actualMess}</br></font>");

        }
        
        public void OpenConfigJobNumberMaskModal()
        {
            Configure_btn.Click();
        }
         public void SaveConfigure()
        {
            SaveConfigure_btn.Click();
        }
        public void CloseConfigureModal()
        {
            CloseConfigure_btn.Click();
        }

        /// <summary>
        /// Create New Config In Job Number Mask
        /// </summary>
        /// <param name="Sage300CREPageData"></param>
        public void CreateConfigJobNumberMaskModal(Sage300CREPageData Sage300CREPageData)
        {
            OpenConfigJobNumberMaskModal();
            if (TitleModal_lbl.IsDisplayed() && TitleModal_lbl.GetText().Equals("Configure Job Number Mask"))
            {
            ExtentReportsHelper.LogInformation($"<font color ='green'>The Configure Number is displayed.</font>");
            Section_ddl.SelectItemByValue(Sage300CREPageData.Section.ToString());
            if(Section_ddl.SelectedValue.ToString() == "3")
                {

                    Character1_ddl.SelectItemByValue(Sage300CREPageData.Character1.ToString());
                    Character2_ddl.SelectItemByValue(Sage300CREPageData.Character2.ToString());
                    Character3_ddl.SelectItemByValue(Sage300CREPageData.Character3.ToString());
                    Connection1_ddl.SelectItem(Sage300CREPageData.Connection1);
                    Connection2_ddl.SelectItem(Sage300CREPageData.Connection2);
                    Connection3_ddl.SelectItem(Sage300CREPageData.Connection3);
                }
                else if (Section_ddl.SelectedValue.ToString() == "2")
                {
                    Character1_ddl.SelectItemByValue(Sage300CREPageData.Character1.ToString());
                    Character2_ddl.SelectItemByValue(Sage300CREPageData.Character2.ToString());
                    Connection1_ddl.SelectItem(Sage300CREPageData.Connection1);
                    Connection2_ddl.SelectItem(Sage300CREPageData.Connection2);
                }
                else
                {
                    Character1_ddl.SelectItemByValue(Sage300CREPageData.Character1.ToString());
                    Connection1_ddl.SelectItem(Sage300CREPageData.Connection1);
                }


            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color ='red'>The Configure Number is not displayed.</font>");
            }

        }

        public bool IsSageRunning()
        {
            var isChecked = Running_btn.GetAttribute("checked");
            if(isChecked != null && isChecked.Equals("true"))
            {
                return true;
            }
            return false;
        }

    }
}
