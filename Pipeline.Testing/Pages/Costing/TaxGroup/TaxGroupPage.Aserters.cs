using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Costing.TaxGroup
{
    public partial class TaxGroupPage
    {
        public bool IsItemInGrid( string value)
        {
            CommonHelper.WaitUntilElementVisible(10, $"//*[@id='ctl00_CPH_Content_rgTaxGroups_ctl00']//span[contains(text(),'{value}')]");
            Textbox ItemInGrid = new Textbox(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rgTaxGroups_ctl00']//span[contains(text(),'{value}')]", 10);
            if (ItemInGrid.IsDisplayed() && ItemInGrid.GetText().Equals(value))
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>Tax Group with name {value} is displayed in grid</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogInformation($"Tax Group with name {value} is not displayed in grid");
                return false;
            }
        }

    }
}