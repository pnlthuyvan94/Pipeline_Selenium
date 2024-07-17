using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Calculations.CalculationDetail
{
    public partial class CalculationDetailPage
    {
        private CalculationDetailPage EnterDescription(string description)
        {
            if (!string.IsNullOrEmpty(description))
                Description_txt.SetText(description);
            return this;
        }

        private CalculationDetailPage EnterCalculation(string calculation)
        {
            if (!string.IsNullOrEmpty(calculation))
                Calculation_txt.SetText(calculation);
            return this;
        }

        private void Save()
        {
            Save_btn.Click();
        }

        public CalculationDetailPage UpdateCalculation(CalculationData data)
        {
            EnterDescription(data.Description).EnterCalculation(data.Calculation).Save();
            string loadingIcon = "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveContinue']/div[1]";
            WaitingLoadingGifByXpath(loadingIcon);
            return this;
        }

        public bool IsIemInProductSubcomponentGrid(string columnName, string value)
        {
            string productSubcomponent_Xpath = "//*[@id='ctl00_CPH_Content_rgCalcSub_ctl00']";
            IGrid ProductSubcomponent_Grid = new Grid(FindType.XPath, productSubcomponent_Xpath, string.Empty);

            return ProductSubcomponent_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsIemInStyleConversionGrid(string columnName, string value)
        {
            string styleConversion_Xpath = "//*[@id='ctl00_CPH_Content_rgCalcStyle_ctl00']";
            IGrid StyleConversion_Grid = new Grid(FindType.XPath, styleConversion_Xpath, string.Empty);

            return StyleConversion_Grid.IsItemOnCurrentPage(columnName, value);
        }

        public bool IsIemInProductConversionGrid(string columnName, string value)
        {
            string productConversion_Xpath = "//*[@id='ctl00_CPH_Content_rgCalcProd_ctl00']";
            IGrid ProductConversion_Grid = new Grid(FindType.XPath, productConversion_Xpath, string.Empty);

            return ProductConversion_Grid.IsItemOnCurrentPage(columnName, value);
        }
    }
}
