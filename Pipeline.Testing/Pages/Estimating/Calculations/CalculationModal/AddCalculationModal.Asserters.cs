
namespace Pipeline.Testing.Pages.Estimating.Calculations.CalculationModal
{
    public partial class AddCalculationModal
    {
        public bool IsModalDisplayed()
        {
            ModalTitle_lbl.WaitForElementIsVisible(10);
            return (ModalTitle_lbl.GetText() == "Add Calculation");
        }

    }
}
