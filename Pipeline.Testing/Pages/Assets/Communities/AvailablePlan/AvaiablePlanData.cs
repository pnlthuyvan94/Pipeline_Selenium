namespace Pipeline.Testing.Pages.Assets.Communities.AvailablePlan
{
    public class AvaiablePlanData
    {
        public AvaiablePlanData()
        {
            Id = string.Empty;
            Name = string.Empty;
            Price = string.Empty;
            Note = string.Empty;
        }

        public AvaiablePlanData(AvaiablePlanData data)
        {
            Id = data.Id;
            Name = data.Name;
            Price = data.Price;
            Note = data.Note;
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Note { get; set; }
    }
}