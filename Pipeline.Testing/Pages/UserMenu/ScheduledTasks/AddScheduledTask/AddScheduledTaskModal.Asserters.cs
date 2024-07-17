using System;

namespace Pipeline.Testing.Pages.UserMenu.ScheduledTasks.AddScheduledTask
{
    public partial class AddScheduledTaskModal
    {
        public bool IsModalDisplayed
        {
            get
            {
                int iTimeOut = 0;
                while (ModalTitle_lbl == null || ModalTitle_lbl.IsDisplayed() == false)
                {
                    System.Threading.Thread.Sleep(500);
                    iTimeOut++;
                    if (iTimeOut == 10)
                    {
                        throw new TimeoutException("Add New Task modal is not displayed.");
                    }
                }
                return (ModalTitle_lbl.GetText() == "Add New Task");
            }
        }

    }
}
