using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Calculations.CalculationModal
{
    public partial class AddCalculationModal : CalculationPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='calcs-modal']/section/header/h1");

        protected Textbox Description_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewCalculations_Description']");

        protected Textbox Calculation_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewCalculations_Calculation']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");

    }

}
