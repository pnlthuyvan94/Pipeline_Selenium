namespace Pipeline.Testing.Pages.Assets.Divisions
{
    public class DivisionData
    {
        public DivisionData()
        {
            Name = string.Empty;
            Address = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Zip = string.Empty;
            Phone = string.Empty;
            Fax = string.Empty;
            MainEmail = string.Empty;
            ServicesEmail = string.Empty;
            Description = string.Empty;
        }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string MainEmail { get; set; }
        public string ServicesEmail { get; set; }
        public string Slug
        {
            get
            {
                return Name.Replace(" ", "-");
            }
            set
            {
                Slug = value.Replace(" ", "-");
            }
        }
        public string Description { get; set; }
    }
}