

namespace Pipeline.Testing.Pages.Estimating.SpecSet
{
    public class SpecSetData
    {
        public string GroupName { get; set; }
        public string UseDefault { get; set; }
        public string SpectSetName { get; set; }
        public string OriginalManufacture { get; set; }
        public string OriginalStyle { get; set; }
        public string OriginalUse { get; set; }
        public string NewManufacture { get; set; }
        public string NewStyle { get; set; }
        public string NewUse { get; set; }
        public string StyleCalculation { get; set; }
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
        public string ProductCalculation { get; set; }
        public SpecSetData()
        {
            GroupName = string.Empty;
            SpectSetName = string.Empty;
            OriginalManufacture = string.Empty;
            OriginalStyle = string.Empty;
            OriginalUse = string.Empty;
            NewManufacture = string.Empty;
            NewStyle = string.Empty;
            NewUse = string.Empty;
            StyleCalculation = string.Empty;
            OriginalPhase = string.Empty;
            OriginalCategory = string.Empty;
            OriginalProduct = string.Empty;
            OriginalProductStyle = string.Empty;
            OriginalProductUse = string.Empty;
            NewPhase = string.Empty;
            NewCategory = string.Empty;
            NewProduct = string.Empty;
            NewProductStyle = string.Empty;
        }

        public SpecSetData(SpecSetData SpecSetData)
        {
            GroupName = SpecSetData.GroupName;
            SpectSetName = SpecSetData.SpectSetName;
            OriginalManufacture = SpecSetData.OriginalManufacture;
            OriginalStyle = SpecSetData.OriginalStyle;
            OriginalUse = SpecSetData.OriginalUse;
            NewManufacture = SpecSetData.NewManufacture;
            NewStyle = SpecSetData.NewStyle;
            NewUse = SpecSetData.NewUse;
            StyleCalculation = SpecSetData.StyleCalculation;
            OriginalPhase = SpecSetData.OriginalPhase;
            OriginalCategory = SpecSetData.OriginalCategory;
            OriginalProduct = SpecSetData.OriginalProduct;
            OriginalProductStyle = SpecSetData.OriginalProductStyle;
            OriginalProductUse = SpecSetData.OriginalProductUse;
            NewPhase = SpecSetData.NewPhase;
            NewCategory = SpecSetData.NewCategory;
            NewProduct = SpecSetData.NewProduct;
            NewProductStyle = SpecSetData.NewProductStyle;
        }
    }
}
