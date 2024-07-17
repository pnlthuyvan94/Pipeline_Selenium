using Pipeline.Common.Controls;
using Pipeline.Common.Pages;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Calculations.CalculationDetail
{
    public partial class CalculationDetailPage : DetailsContentPage<CalculationDetailPage>
    {
        // Calculation Detail

        protected Textbox Description_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtDescription']");

        protected Textbox Calculation_txt => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtCalculation']");

        protected Button Save_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContinue']");

    }
}
