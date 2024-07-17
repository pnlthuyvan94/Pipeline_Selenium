using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Costing.Vendor.VendorProduct
{
    public partial class VendorProductPage
    {
        /*
         * Verify head title and id of new Job
         */
       

        public bool IsHeaderBreadscrumbCorect(string _name)
        {
            System.Threading.Thread.Sleep(1000);
            string expectedName = $"{_name}";
            return SubHeadTitle().Equals(expectedName) && !CurrentURL.EndsWith("vid=0");
        }

        public void VerifyVendorProductIsDisplayed(string title)
        {
            Product_lbl.WaitForElementIsVisible(5);
            if (Product_lbl.IsDisplayed(false) && Product_lbl.GetText().Equals(title) && CurrentURL.Contains("/Dashboard/Costing/Vendors/VendorsToProducts.aspx?"))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Vendor To Product page is displayed successfully with title{Product_lbl.GetText().Equals(title)} </b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Vendor To Product page is displayed wrong with title{Product_lbl.GetText().Equals(title)}</font>");
            }
        }

        public void VerifyBuildingPhaseAddedInProductGrid(string BuildingPhase)
        {
            Label BuildingPhase_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_ctl00_CPH_Content_rgBuildingPhasesPanel']//span[contains(text(),'{BuildingPhase}')]");
            BuildingPhase_lbl.WaitForElementIsVisible(5);
            if (BuildingPhase_lbl.IsDisplayed(false) && BuildingPhase_lbl.GetText().Equals(BuildingPhase))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(BuildingPhase_lbl), "<font color='green'><b>The BuildingPhase Added in Product grid is displayed correctly</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(BuildingPhase_lbl), "<font color='red'>The BuildingPhase Added in Product grid is displayed incorrectly</font>");
            }
        }
        public void VerifyBaseMateriaAndLaborCostIsDisplayedCorrectly(string productName, string materialCost, string laborCost)
        {
            {
                Textbox MaterialCost_lbl = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgBuildingPhases']//tbody/tr/td/a[contains(text(),'{productName}')]/../following-sibling::td/a[contains(@id,'aCost1') and contains(text(),'{materialCost}')]");
                Textbox LaborCost_lbl = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgBuildingPhases']//tbody/tr/td/a[contains(text(),'{productName}')]/../following-sibling::td/a[contains(@id,'aCost2') and contains(text(),'{laborCost}')]");
                if (MaterialCost_lbl.GetText().Equals(materialCost) && LaborCost_lbl.GetText().Equals(laborCost))
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The base material {MaterialCost_lbl.GetText()} and labor {LaborCost_lbl.GetText()} cost in grid is displayed correctly.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>Actual Result: The base material {MaterialCost_lbl.GetText()} and labor {LaborCost_lbl.GetText()} cost in grid is displayed wrong.</font>");
                }
            }
        }
    }
}
