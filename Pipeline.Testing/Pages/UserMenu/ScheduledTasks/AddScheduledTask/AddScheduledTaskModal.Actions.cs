using OpenQA.Selenium;
using Pipeline.Common.Constants;
using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.UserMenu.ScheduledTasks.AddScheduledTask
{
    public partial class AddScheduledTaskModal
    {
        public AddScheduledTaskModal SelectTask(string task)
        {
            if (!string.IsNullOrEmpty(task))
            {
                Task_ddl.SelectItem(task);
                WaitingLoadingGifByXpath(ExcelFactory.GetRow(MetaData, 13)[BaseConstants.ValueToFind]);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Task_ddl), $"Select {task} on the dropdown list.");
            }
            return this;
        }

        public string GetDescription()
        {
            return Desription_lbl.GetText();
        }

        public string SelectCommunity(string community)
        {
            if (!string.IsNullOrEmpty(community) && Community_ddl.IsItemInList(community))
            {
                Community_ddl.SelectItem(community, false, false);
                WaitingLoadingGifByXpath(ExcelFactory.GetRow(MetaData, 14)[BaseConstants.ValueToFind], 2000);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(Community_ddl), $"Select: {community} on the dropdown list.");
            }
            else
            {
                Community_ddl.SelectItem(1);
                WaitingLoadingGifByXpath(ExcelFactory.GetRow(MetaData, 14)[BaseConstants.ValueToFind], 2000);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(House_ddl), $"Select Comunnity: {Community_ddl.SelectedItemName} on the dropdown list.");

            }
            return Community_ddl.SelectedValue;
        }

        public string SelectHouse(string house)
        {
            if (!string.IsNullOrEmpty(house) && House_ddl.IsItemInList(house))
            {
                House_ddl.SelectItem(house, false, false);
            }
            else
            {
                House_ddl.SelectItem(1);
                ExtentReportsHelper.LogInformation(CommonHelper.CaptureScreen(House_ddl), $"Select House: {House_ddl.SelectedItemName}");
            }
            return House_ddl.SelectedValue;
        }

        public AddScheduledTaskModal SetDate(string date)
        {
            if (!string.IsNullOrEmpty(date))
                Date_txt.SetText(date);
            return this;
        }

        public AddScheduledTaskModal SetTime(string time)
        {
            if (!string.IsNullOrEmpty(time))
                Time_txt.SetText(time);
            return this;
        }

        public AddScheduledTaskModal SelectFrequency(string frequency)
        {
            if (!string.IsNullOrEmpty(frequency))
                Frequency_ddl.SelectItem(frequency, true);
            return this;
        }

        public AddScheduledTaskModal CheckActive(string active)
        {
            if (!string.IsNullOrEmpty(active) && active.ToUpper().Equals("TRUE"))
                Active_chk.SetCheck(true);
            else
                Active_chk.SetCheck(false);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
        }

        public void Close()
        {
            Close_btn.Click();
        }
    }
}