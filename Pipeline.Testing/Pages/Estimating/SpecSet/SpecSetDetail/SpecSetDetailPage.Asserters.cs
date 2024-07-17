
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.SpecSet.SpecSetDetail
{
    public partial class SpecSetDetailPage
    {
        public bool IsModalDisplayed()
        {
            return ModalTitle_lbl.WaitForElementIsVisible(10);
        }
        public void VerifyAddProductConversionInGrid(SpecSetData SpecSetData)
        {
            IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "OriginalBuildingPhase_Name", SpecSetData.OriginalPhase);
            IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "OriginalProductStyle", SpecSetData.OriginalProductStyle);
            IsItemProductOnProductConversionGrid(SpecSetData.SpectSetName, SpecSetData.OriginalProduct);
            IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "OriginalProductUse", SpecSetData.OriginalProductUse);
            IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "NewBuildingPhase_Name", SpecSetData.NewPhase);
            IsItemProductOnProductConversionGrid(SpecSetData.SpectSetName, SpecSetData.NewProduct);
            IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "NewProductStyle", SpecSetData.NewProductStyle);
            IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "NewProductUse", SpecSetData.NewProductUse);
            IsIemOnProductConversionGrid(SpecSetData.SpectSetName, "Calculation", SpecSetData.ProductCalculation);
        }

        public void VerifyCreateNewSpecSetIsDisplayedInGrid(string SpecSetName)
        {
            Label SpecSetName_txt = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgSets']//span[contains(text(),'{SpecSetName}')]");
            SpecSetName_txt.WaitForElementIsVisible(5);
            if (SpecSetName_txt.IsDisplayed(false) && SpecSetName_txt.GetText().Equals(SpecSetName))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The SpecSet Name  in grid is displayed correctly with Name: {SpecSetName_txt.GetText()}.</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The SpecSet SpecSet Name in grid is displayed wrong with Name: {SpecSetName_txt.GetText()}.</font>");
            }
        }

        public void VerifyAddStyleConversionInGrid(SpecSetData SpecSetData)
        {
            if (SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "OriginalMfg_Name", SpecSetData.OriginalManufacture) is true
               && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "OriginalStyle_Name", SpecSetData.OriginalStyle) is true
               && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "OriginalUse", SpecSetData.OriginalUse) is true
               && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "NewMfg_Name", SpecSetData.NewManufacture) is true
               && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "NewStyle_Name", SpecSetData.NewStyle) is true
               && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "NewUse", SpecSetData.NewUse) is true
               && SpecSetDetailPage.Instance.IsIemOnStyleConversionGrid(SpecSetData.SpectSetName, "Calculation", SpecSetData.StyleCalculation) is true)
                ExtentReportsHelper.LogPass(null, $"<font color='green'>The set up Style Conversion on Spec Set Group <b>'{SpecSetData.SpectSetName}'</b> is correct.</font>");

            else
                ExtentReportsHelper.LogWarning("<font color='yellow'>The set up Style Conversion on this page is NOT same as expectation. " +
                            "The next step and the  result after generating a BOM can be incorrect." +
                            $"<br>The expected Original Manufacturer/Style: {SpecSetData.OriginalManufacture}" +
                            $"<br>The expected Original Use: {SpecSetData.OriginalUse}" +
                            $"<br>The expected New Manufacturer/Style: {SpecSetData.NewManufacture}" +
                            $"<br>The expected New Use: {SpecSetData.NewUse}" +
                            $"<br>The expected Calculation: {SpecSetData.StyleCalculation}</br></font>");
        }
    }
}
