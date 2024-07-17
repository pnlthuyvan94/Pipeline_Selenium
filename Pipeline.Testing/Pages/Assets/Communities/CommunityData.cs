namespace Pipeline.Testing.Pages.Assets.Communities
{
    public class CommunityData
    {
        public CommunityData()
        {
            Name = string.Empty;
            Division = "None";
            Code = string.Empty;
            City = string.Empty;
            CityLink = string.Empty;
            Township = string.Empty;
            County = string.Empty;
            State = string.Empty;
            Zip = string.Empty;
            SchoolDistrict = string.Empty;
            SchoolDistrictLink = string.Empty;
            Status = "Closed";
            Description = string.Empty;
            DrivingDirections = string.Empty;
        }

        public CommunityData(CommunityData data)
        {
            Name = data.Name;
            Division = data.Division;
            Code = data.Code;
            City = data.City;
            CityLink = data.CityLink;
            Township = data.Township;
            County = data.County;
            State = data.State;
            Zip = data.Zip;
            SchoolDistrict = data.SchoolDistrict;
            SchoolDistrictLink = data.SchoolDistrictLink;
            Status = data.Status;
            Description = data.Description;
            DrivingDirections = data.DrivingDirections;
        }

        public string Name { get; set; }
        public string Division { get; set; }
        public string Code { get; set; }
        public string City { get; set; }
        public string CityLink { get; set; }
        public string Township { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Slug { get; set; }
        public string SchoolDistrict { get; set; }
        public string SchoolDistrictLink { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string DrivingDirections { get; set; }

    }
}