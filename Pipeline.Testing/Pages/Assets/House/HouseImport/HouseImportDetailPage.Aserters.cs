using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.House.Import
{
    public partial class HouseImportDetailPage
    {
        public void VerifyItemComparisonGroups(string Option, string ComparedTo, string Condition, string ListIncludedOption)
        {
            Label Option_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]");
            Label ComparedTo_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblComparedTo') and contains(text(),'{ComparedTo}')]");
            Label Condition_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblCondition') and contains(text(),'{Condition}')]");
            Label IncludedImportOption_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblIncludedOptions') and contains(text(),'{ListIncludedOption}')]");


            if (Option_lbl.IsDisplayed() is true && Option_lbl.GetText().Equals(Option) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Option Name column with {Option} is displayed in Comparison Groups</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Option Name column with {Option} is displayed in Comparison Groups</font>");
            }
            if (!string.IsNullOrEmpty(ComparedTo))
            {
                if (ComparedTo_lbl.IsDisplayed() is true && ComparedTo_lbl.GetText().Equals(ComparedTo) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Compared To column with {ComparedTo} is displayed in Comparison Groups</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The Compared To column with {ComparedTo} is displayed in Comparison Groups</font>");
                }

                if (Condition_lbl.IsDisplayed() is true && Condition_lbl.GetText().Equals(Condition) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Condition column with {Condition} is displayed in Comparison Groups</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The Condition column with {Condition} is displayed in Comparison Groups</font>");
                }
            }


            if (IncludedImportOption_lbl.IsDisplayed() is true && IncludedImportOption_lbl.GetText().Equals(ListIncludedOption) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Included Option Name column with {ListIncludedOption} is displayed in Comparison Groups</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Included Option Name column with {ListIncludedOption} is displayed in Comparison Groups</font>");

            }
        }

        public void CheckProductWithStyleNotImportAndNoExistInTheSystem(HouseImportQuantitiesData HouseImportQuantitiesData)
        {
            foreach (string item in HouseImportQuantitiesData.Products)
            {
                Label ProductStatusLbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_pnlFinalize']//p[contains(text(),'{item}')]");
                if (ProductStatusLbl.IsDisplayed())
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Product With Name {item} is displayed with message this Product does not exist in the system.</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogInformation($"<font color='yellow'>The Product With Name  {item} is not display with message this Product does not exist in the system.</font>");
                }
            }
        }
    }
}
