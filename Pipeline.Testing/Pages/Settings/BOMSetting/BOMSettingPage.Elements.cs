using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;

namespace Pipeline.Testing.Pages.Settings.BOMSetting
{
    public partial class BOMSettingPage : DashboardContentPage<BOMSettingPage>
    {
        protected Button SaveJobBOMSetting => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveJobBom']");
        protected Button SaveHouseBOMSetting => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveHouseBOM']");
        protected CheckBox EnableHouseBOMProductOrientation_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_chkEnableHouseBomProductOrientation']");
        protected Button SaveGeneralSettings => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveGeneral']");
        protected Textbox Paramter_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtParameter']");
        protected CheckBox EnablePromptBuildingPhase_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_chkBuildingPhaseAddToProduct']");
        protected CheckBox RoundingNegativeValueTowardsZero_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_chkRoundNegativeValuesTowardsZero']");
        protected CheckBox AssignedOptionsOnHouseBOM_chk => new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_chkAssignedOption']");
    }
}
