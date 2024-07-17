namespace Pipeline.Testing.Pages.Settings.BuildPro
{
    public class BuildProData
    {
        public BuildProSetting BuildProSetting { get; set; }
        public BuildProData()
        {
            BuildProSetting = new BuildProSetting();
        }
    }

    public class BuildProSetting
    {
        public string RootURI { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Company { get; set; }
        public string Division { get; set; }
        public bool Status { get; set; }

        public BuildProSetting()
        {
            RootURI = "https://uatxml.hyphensolutions.com/httpreceive.aspx";
            Username = "intuatcgi";
            Password = "int3gr8ti0n";
            Company = "CGI";
            Division = "2";
            Status = true;
        }

    }
}
