namespace Pipeline.Testing.Pages.Jobs.Job
{
    public class JobData
    {
        public string Name { get; set; }
        public string Community { get; set; }
        public string House { get; set; }
        public string Lot { get; set; }
        public string Draft { get; set; }
        public string Orientation { get; set; }
        public string Customer { get; set; }
        public JobData()
        {
            Name = string.Empty;
            Community = string.Empty;
            House = string.Empty;
            Lot = string.Empty;
            Draft = string.Empty;
            Orientation = string.Empty;
            Customer = string.Empty;
        }
        public JobData(JobData data)
        {
            Name = data.Name;
            Community = data.Community;
            House = data.House;
            Lot = data.Lot;
            Draft = data.Draft;
            Orientation = data.Orientation;
            Customer = data.Customer;
        }
    }
}
