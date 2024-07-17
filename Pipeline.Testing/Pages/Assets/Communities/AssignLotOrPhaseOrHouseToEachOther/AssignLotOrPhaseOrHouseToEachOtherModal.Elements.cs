using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther
{
    public partial class AssignLotOrPhaseOrHouseToEachOtherModal : CommunityPage
    {
        public AssignLotOrPhaseOrHouseToEachOtherModal() : base()
        {
        }

        protected Label AssignedTitle_lbl => new Label(FindType.XPath, "//*[@id='RadWindowWrapper_ctl00_CPH_Content_RadWindow1']//span[@class='rwTitle']");

        protected Label Message_lbl =>
            new Label(FindType.XPath, "//*[@id='SaveStatus']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='lbSave']");

        protected Button Close_btn
            => new Button(FindType.XPath, "//*[@id='RadWindowWrapper_ctl00_CPH_Content_RadWindow1']/div[1]/div/ul/li[2]/span");

        private string _loadingSave = "//*[@id='lp1lbSave']/div[1]";

        protected CheckBox AllLotToPlan_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='rgLotsToPlan_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");

        protected CheckBox AllPhaseToPlan_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='rgCommunityPhasesToPlan_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");

        protected CheckBox AllPlanToLot_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='rgPlansToLot_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");

        protected CheckBox AllPlanToCommunityPhase_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='rgPlanToCommunityPhases_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");

        protected CheckBox AllLotToCommunityphase_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='rgLotToCommunityPhases_ctl00_ctl02_ctl01_ClientSelectColumnSelectCheckBox']");
    }

}
