namespace Pipeline.Testing.Pages.Costing.Vendor
{
    public class VendorData
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string Trade { get; set; }
        public string Contact { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
        public string Phone { get; set; }
        public string AltPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Fax { get; set; }
        public string Url { get; set; }
        public string TaxGroup { get; set; }
        public bool EnablePrecision { get; set; }
        public VendorData(VendorData data)
        {
            Name = data.Name;
            Code = data.Code;
            Address1 = data.Address1;
            City = data.City;
            State = data.State;
            Zip = data.Zip;
            Email = data.Email;
        }
        
        public VendorData()
        {
            Name = string.Empty;
            Code = string.Empty;
            Trade = string.Empty;
            Address1 = string.Empty;
            Address2 = string.Empty;
            Address3 = string.Empty;
            City = string.Empty;
            State = string.Empty;
            Zip = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
            AltPhone = string.Empty;
            MobilePhone = string.Empty;
            Fax = string.Empty;
            Url = string.Empty;
            TaxGroup = string.Empty;
            EnablePrecision = false;
        }
    }
}
