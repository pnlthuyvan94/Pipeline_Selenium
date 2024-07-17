using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Calculations.CalculationModal
{
    public partial class AddCalculationModal
    {
        public AddCalculationModal EnterDescription(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Description_txt.SetText(data);
            return this;
        }

        public AddCalculationModal EnterCalculation(string data)
        {
            if (!string.IsNullOrEmpty(data))
                Calculation_txt.SetText(data);
            return this;
        }

        public void Save()
        {
            Save_btn.Click();
            //Calculation_Grid.WaitGridLoad();
            WaitingLoadingGifByXpath("//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgCalcs']");
        }

        public void CloseModal()
        {
            FindElementHelper.FindElement(FindType.XPath, "//*[@id='calcs-modal']/section/header/a").Click();
            ModalTitle_lbl.WaitForElementIsInVisible(10);
        }
    }
}