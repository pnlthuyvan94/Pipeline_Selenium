using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pipeline.Testing.Pages.UserMenu.ScheduledTasks
{
    public class ScheduledTaskData
    {
        public string Task;
        public string Description;
        public string Community;
        public string CommunityId;
        public string House;
        public string HouseId;
        public string Date;
        public string Time;
        public string Frequency;
        public string Active;
        public ScheduledTaskData()
        {
            Task = string.Empty;
            Description = string.Empty;
            Community = string.Empty;
            CommunityId = string.Empty;
            House = string.Empty;
            HouseId = string.Empty;
            Date = string.Empty;
            Time = string.Empty;
            Frequency = string.Empty;
            Active = string.Empty;
        }
        public ScheduledTaskData(ScheduledTaskData data)
        {
            Task = data.Task;
            Description = data.Description;
            Community = data.Community;
            CommunityId = data.CommunityId;
            House = data.House;
            HouseId = data.HouseId;
            Date = data.Date;
            Time = data.Time;
            Frequency = data.Frequency;
            Active = data.Active;
        }
    }
}
