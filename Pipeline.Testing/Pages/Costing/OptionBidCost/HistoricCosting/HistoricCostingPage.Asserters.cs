

namespace Pipeline.Testing.Pages.Costing.OptionBidCost.HistoricCosting
{
    public partial class HistoricCostingPage
    {
        public bool IsColumnFoundInGrid(string columnName)
        {
            try
            {
                return HistoricCost_Grid.IsColumnFoundInGrid(columnName);
            }
            catch
            {
                return false;
            }
        }
    }
}
