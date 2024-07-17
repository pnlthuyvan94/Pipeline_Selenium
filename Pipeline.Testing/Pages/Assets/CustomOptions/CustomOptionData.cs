namespace Pipeline.Testing.Pages.Assets.CustomOptions
{
    public class CustomOptionData
    {
        public CustomOptionData()
        {
            Code = string.Empty;
            Description = string.Empty;
            Structural = false;
            Price = 0.00;
        }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool Structural { get; set; }
        public double Price { get; set; }
    }
}