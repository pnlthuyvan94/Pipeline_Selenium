using NUnit.Framework;
using NUnit.Framework.Legacy;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Pages.UserMenu.ScheduledTasks.AddScheduledTask;

namespace Pipeline.Testing.Pages.UserMenu.ScheduledTasks
{
    public partial class ScheduledTaskPage
    {
        public void FilterItemInGrid(string columnName, GridFilterOperator gridFilterOperator, string value)
        {
            ScheduledTask_Grid.FilterByColumn(columnName, gridFilterOperator, value);
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTasks']/div[1]");
        }

        public bool IsItemInGrid(string columnName, string value)
        {
            return ScheduledTask_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public void DeleteItemInGrid(string columnName, string value)
        {
            ScheduledTask_Grid.ClickDeleteItemInGrid(columnName, value);
            ConfirmDialog(ConfirmType.OK);
        }

        public void OpenScheduledTaskModal()
        {
            GetItemOnHeader(DashboardContentItems.Add).Click();
            AddScheduledTaskModal = new AddScheduledTaskModal();
        }
        public ScheduledTaskData AddTask(ScheduledTaskData _scheduledtask)
        {
            OpenScheduledTaskModal();
            Assert.That(AddScheduledTaskModal.IsModalDisplayed, "Add New Task modal isn't displayed");

            // 3: Populate all values 
            // Select Name
            AddScheduledTaskModal.SelectTask(_scheduledtask.Task);

            // Verify the description
            ClassicAssert.IsTrue(_scheduledtask.Description.Equals(AddScheduledTaskModal.GetDescription()), "The description is displayed not same as expected.\n Expected: {0}\n Actual: {1}", _scheduledtask.Description, AddScheduledTaskModal.GetDescription());

            _scheduledtask.CommunityId = AddScheduledTaskModal.SelectCommunity(_scheduledtask.Community);
            _scheduledtask.HouseId = AddScheduledTaskModal.SelectHouse(_scheduledtask.House);
            AddScheduledTaskModal.SetDate(_scheduledtask.Date)
            .SetTime(_scheduledtask.Time).SelectFrequency(_scheduledtask.Frequency).CheckActive(_scheduledtask.Active)
                .Save();
            ScheduledTaskData _getscheduledtask = new ScheduledTaskData(_scheduledtask)
            {
                CommunityId = _scheduledtask.CommunityId,
                HouseId = _scheduledtask.HouseId
            };

            // Verify message
            string _actualMessage = ScheduledTaskPage.Instance.GetLastestToastMessage();
            string _expectedMessage = "Task Added";
            if (!string.IsNullOrEmpty(_actualMessage) && _expectedMessage.Equals(_actualMessage))
            {
                ExtentReportsHelper.LogPass($"Create new scheduled task successfully.");
            }
            else
            {
                ExtentReportsHelper.LogFail($"Don't display any message");
                Assert.Fail($"Fail to create new role.");
            }
            return _getscheduledtask;
        }
        public void DeleteTask(ScheduledTaskData _scheduledtask)
        {
            // Select OK to confirm; verify successful delete and appropriate success message.
            DeleteItemInGrid("Name", _scheduledtask.Task);

            string expectedMess = "Task Deleted";
            if (expectedMess == GetLastestToastMessage())
            {
                ExtentReportsHelper.LogPass("New Task deleted successfully!");
                ScheduledTaskPage.Instance.CloseToastMessage();
            }
            else
            {
                if (IsItemInGrid("Role", _scheduledtask.Task))
                    ExtentReportsHelper.LogFail("Task could not be deleted!");
                else
                    ExtentReportsHelper.LogPass("Task deleted successfully!");

            }

        }
    }
}
