using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther
{
    public partial class AssignLotOrPhaseOrHouseToEachOtherModal
    {
        public void SelectOrUnSelectAllItems(bool status, bool isAssignedToPlan, bool isAssignedToPhase, bool isAssignedLot = true)
        {
            // True to check all and False to uncheck all
            CommonHelper.SwitchFrameAction("RadWindow1", () =>
            {
                if (isAssignedToPlan)
                {
                    if (isAssignedLot)
                        AllLotToPlan_chk.SetCheck(status);
                    else
                        AllPhaseToPlan_chk.SetCheck(status);
                }
                else if (isAssignedToPhase)
                {
                    if (isAssignedLot)
                        AllLotToCommunityphase_chk.SetCheck(status);
                    else
                        AllPlanToCommunityPhase_chk.SetCheck(status);
                }
                else
                {
                    // Assign to lot
                    AllPlanToLot_chk.SetCheck(status);

                }
                Save();
            });

            // Waiting loading grid
            WaitingLoadingGifByXpath(_loadingSave);
        }


        public string GetMessageLabel()
        {
            var message = string.Empty;
            CommonHelper.SwitchFrameAction("RadWindow1", () =>
            {
                message = Message_lbl.GetText();
                
            });
            return message;
        }

        private void Save()
        {
            Save_btn.Click();
        }

        public void Cancel()
        {
            Close_btn.Click();
        }
    }
}