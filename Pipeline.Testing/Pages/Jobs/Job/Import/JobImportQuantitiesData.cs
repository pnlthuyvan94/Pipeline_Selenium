namespace Pipeline.Testing.Pages.Jobs.Job
{
    public class JobImportQuantitiesData
    {
        public string Option { get; set; }
        public string BuildingPhaseCode { get; set; }
        public string BuildingPhaseName { get; set; }
        public string ProductName { get; set; }
        public float Quantities { get; set; }
        public string Now { get; set; }
        public string Future { get; set; }

        public JobImportQuantitiesData()
        {
            this.Option = string.Empty;
            this.BuildingPhaseCode = string.Empty;
            this.BuildingPhaseName = string.Empty;
            this.ProductName = string.Empty;
            this.Quantities = 0;
            this.Now = string.Empty;
            this.Future = string.Empty;
        }
    }
}
