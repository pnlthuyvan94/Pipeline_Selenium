

using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Estimating.Uses.UseAssignment
{
    class UseAssignmentData
    {
        public List<string> Assignments_Header { get; set; }
        public List<string> Conversion_Header { get; set; }
        public string Product { get; set; }
        public string Houses { get; set; }
        public string BuildingPhase { get; set; }
        public string Subcomponent { get; set; }
        public string ActiveJobs { get; set; }
        public string ClosedJobs { get; set; }
        public string Options { get; set; }
        public string CustomOptions { get; set; }
        public string Worksheets { get; set; }
        public string SpectSetName { get; set; }
        public string OriginalManufacture { get; set; }
        public string OriginalStyle { get; set; }
        public string OriginalUse { get; set; }
        public string NewManufacture { get; set; }
        public string NewStyle { get; set; }
        public string NewUse { get; set; }
        public string OriginalPhase { get; set; }
        public string OriginalCategory { get; set; }
        public string OriginalProduct { get; set; }
        public string OriginalProductStyle { get; set; }
        public string OriginalProductUse { get; set; }
        public string NewPhase { get; set; }
        public string NewCategory { get; set; }
        public string NewProduct { get; set; }
        public string NewProductStyle { get; set; }
        public string NewProductUse { get; set; }
        public UseAssignmentData()
        {
            Assignments_Header = new List<string>();
            Conversion_Header = new List<string>();
            Product = string.Empty;
            Houses = string.Empty;
            Subcomponent = string.Empty;
            ActiveJobs = string.Empty;
            ClosedJobs = string.Empty;
            Options = string.Empty;
            CustomOptions = string.Empty;
            Worksheets = string.Empty;
            SpectSetName = string.Empty;
            OriginalManufacture = string.Empty;
            OriginalStyle = string.Empty;
            OriginalUse = string.Empty;
            NewManufacture = string.Empty;
            NewStyle = string.Empty;
            NewUse = string.Empty;
            OriginalPhase = string.Empty;
            OriginalCategory = string.Empty;
            OriginalProduct = string.Empty;
            OriginalProductStyle = string.Empty;
            OriginalProductUse = string.Empty;
            NewPhase = string.Empty;
            NewCategory = string.Empty;
            NewProduct = string.Empty;
            NewProductStyle = string.Empty;
            NewProductUse = string.Empty;
        }
    }
}
