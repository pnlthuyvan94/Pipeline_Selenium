using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.BuildingGroup.AddBuildingGroup
{
    public partial class AddBuildingGroupModal
    {
        public AddBuildingGroupModal EnterBuildingGroupCode(string code)
        {
            if (string.IsNullOrEmpty(code) is false)
                BuildingGroupCode_txt.SetText(code);
            return this;
        }

        public AddBuildingGroupModal EnterBuildingGroupName(string name)
        {
            if (string.IsNullOrEmpty(name) is false)
                BuildingGroupName_txt.SetText(name);
            return this;
        }

        public AddBuildingGroupModal EnterBuildingGroupDescription(string description)
        {
            if (string.IsNullOrEmpty(description) is false)
                BuildingGroupDescription_txt.SetText(description);
            return this;
        }
        public AddBuildingGroupModal SelectTheBuildingTrade(string item)
        {
            BuildingTrade_ddl.SelectItem(item, true);
            return this;
        }

        public void Save()
        {
            BuildingGroupSave_btn.Click();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_pnlNewBuildingGroup']");
        }

        public void CloseModal()
        {
            CloseModal_btn.Click();
            ModalTitle_lbl.WaitForElementIsInVisible(5);
        }

        /// <summary>
        /// Check Iteam Name Functional In Building Group Modal
        /// </summary>
        public void IsItemNameInBuildingGroupModal()
        {
            if (BuildingGroupCode_txt.IsDisplayed() is true)
                ExtentReportsHelper.LogPass("<font color ='green'><b>Code Field is displayed in Building Group Page.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color ='red'>Building Group Code Field  is not display in Building Group Page.</font>");

            if (BuildingGroupName_txt.IsDisplayed() is true)
                ExtentReportsHelper.LogPass("<font color ='green'><b>Building Group Name Field is displayed in Building Group Page.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color ='red'>Building Group Name Field  is not display in Building Group Page.</font>");

            if (BuildingGroupDescription_txt.IsDisplayed() is true)
                ExtentReportsHelper.LogPass("<font color ='green'><b>Building Group Description Field is displayed in Building Group Page.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color ='red'>Building Group Description Field  is not display in Building Group Page.</font>");


            if (BuildingGroupSave_btn.IsDisplayed() is true)
                ExtentReportsHelper.LogPass("<font color ='green'><b>Save Button is displayed in Building Group Page.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color ='red'>Add Button is not display in Building Group Page.</font>");

            if (BuildingGroupCancel_btn.IsDisplayed() is true)
                ExtentReportsHelper.LogPass("<font color ='green'><b>Cancel Button is displayed in Building Group Page.</b></font>");
            else
                ExtentReportsHelper.LogFail("<font color ='red'>Cancel Button is not display in Building Group Page.</font>");

        }
    }
}

