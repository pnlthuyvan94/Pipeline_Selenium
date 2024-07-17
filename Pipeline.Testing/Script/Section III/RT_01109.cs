using LinqToExcel;
using NUnit.Framework;
using Pipeline.Common.BaseClass;
using Pipeline.Common.Enums;
using Pipeline.Common.Enums.MenuEnums;
using Pipeline.Common.Utils;
using Pipeline.Testing.Based;
using Pipeline.Testing.Pages.UserMenu.ScheduledTasks;

namespace Pipeline.Testing.Script.Section_III
{
    public partial class I05_RT_01109 : BaseTestScript
    {
        public override void SetupTestSectionName()
        {
            SetupTestSectionName(TestSets.Section_III);
        }

        ScheduledTaskData _scheduledtask;
        ScheduledTaskData _getscheduledtask;
        [SetUp]
        public void CreateTestData()
        {
            _scheduledtask = new ScheduledTaskData()
            {
                Task = "Update House Option BOM",
                Description = "This task updates the BOM for all options on the specified house in a community.",
                Community = "Acorn Two",
                CommunityId = "320",
                House = "The Goose",
                HouseId = "63",
                Date = "6/1/2020",
                Time = "6/01/1020 12:00:00 AM",
                Frequency = "Once",
                Active = "TRUE"
            };
        }
        #region"Test case"
        [Test]
        [Category("Section_III")]
        public void I05_UserMenu_AddScheduledTask()
        {
            // 1: navigate to this URL: http://dev.bimpipeline.com/Dashboard/Tasks/Default.aspx
            ScheduledTaskPage.Instance.SelectMenu(MenuItems.PROFILE).SelectItem(ProfileMenu.ScheduledTask);

            // 2: Add new Task
            _getscheduledtask = ScheduledTaskPage.Instance.AddTask(_scheduledtask);

            // 6: Verify the new task create successfully
            ScheduledTaskPage.Instance.FilterItemInGrid("Parameters", GridFilterOperator.NoFilter, string.Empty);
            ScheduledTaskPage.Instance.FilterItemInGrid("Name", GridFilterOperator.Contains, _getscheduledtask.Task);
            //ScheduledTaskPage.Instance.FilterItemInGrid("Parameters", GridFilterOperator.StartsWith, $"community={_getscheduledtask.CommunityId}&house={_getscheduledtask.HouseId}");
            bool isFound = ScheduledTaskPage.Instance.IsItemInGrid("Name", _getscheduledtask.Task);
            Assert.That(isFound, string.Format("New Task \"{0}\" was not display on grid.", _getscheduledtask.Task));

            // 8: Delete Contract Document
            ScheduledTaskPage.Instance.DeleteTask(_getscheduledtask);
        }
    }
}
#endregion