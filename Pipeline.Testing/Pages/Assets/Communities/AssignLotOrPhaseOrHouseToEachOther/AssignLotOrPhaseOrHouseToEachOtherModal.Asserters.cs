using Pipeline.Common.Controls;
using System;

namespace Pipeline.Testing.Pages.Assets.Communities.AssignLotOrPhaseOrHouseToEachOther
{
    public partial class AssignLotOrPhaseOrHouseToEachOtherModal
    {
        public bool IsModalDisplayed(bool isAssignedToPlan, bool isAssignedToPhase, bool isAssignedLot)
        {
            //WaitingItemDisplay(AssignedTitle_lbl);
            AssignedTitle_lbl.WaitForElementIsVisible(30);
            if (isAssignedToPlan)
            {
                return (isAssignedLot ? AssignedTitle_lbl.GetText() == "Assign Lots to Plan"
               : AssignedTitle_lbl.GetText() == "Assign Community Phases to Plan");

            }
            else if (isAssignedToPhase)
            {
                return isAssignedLot ? AssignedTitle_lbl.GetText() == "Assign Lots to Community Phase"
               : AssignedTitle_lbl.GetText() == "Assign Plans to Community Phase";
            }
            else
            {
                // Assign to lot
                return AssignedTitle_lbl.GetText() == "Assign Plans to Lot";
            }
        }

        public bool IsAssignedSuccessful(bool isAssignedToPlan, bool isAssignedToPhase, bool isAssignedLot)
        {
            var message = GetMessageLabel();
            if (!string.Empty.Equals(message))
            {
                if (isAssignedToPlan)
                {
                    return (isAssignedLot ? message == "Successfully assigned Lots to Plan"
                    : message == "Successfully assigned Community Phases to Plan");
                }
                else if (isAssignedToPhase)
                {
                    return (isAssignedLot ? message == "Successfully assigned Lot(s) to Community Phase"
                   : message == "Successfully assigned Plan(s) to Community Phase");
                }
                else
                {
                    // Assign to lot
                    return message == "Successfully assigned Plans to Lot";
                }
            }
            return false;
        }

        private void WaitingItemDisplay(Label item)
        {
            int iTimeOut = 0;
            while (item == null || item.IsDisplayed() == false)
            {
                System.Threading.Thread.Sleep(500);
                iTimeOut++;
                if (iTimeOut == 10)
                {
                    throw new TimeoutException($"Label /'{item}/' is not displayed.");
                }
            }
        }
    }
}
