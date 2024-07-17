using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.Calculations.CalculationModal;


namespace Pipeline.Testing.Pages.Estimating.Calculations
{
    public partial class CalculationPage : DashboardContentPage<CalculationPage>
    {
       public AddCalculationModal AddCalculationModal { get; private set; }
        protected IGrid Calculation_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgCalcs_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCalcs']/div[1]");

    }
}
