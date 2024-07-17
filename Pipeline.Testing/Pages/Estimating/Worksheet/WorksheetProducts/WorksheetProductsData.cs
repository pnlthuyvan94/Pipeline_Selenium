using System.Collections.Generic;


namespace Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetProducts
{
    public class WorksheetProductsData
    {
        public List<string> BuildingPhase;
        public List<string> Products;
        public string Style;
        public string Code;
        public string Use;
        public List<string> Quantity;
        public WorksheetProductsData()
        {
            BuildingPhase = new List<string>();
            Products = new List<string>();
            Style = string.Empty;
            Code = string.Empty;
            Use = string.Empty;
            Quantity = new List<string>();
        }
    }
}
