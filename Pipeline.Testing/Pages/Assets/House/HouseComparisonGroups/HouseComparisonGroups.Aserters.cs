

using AventStack.ExtentReports.Utils;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Collections.Generic;

namespace Pipeline.Testing.Pages.Assets.House.HouseComparisonGroups
{
    public partial class HouseComparisonGroups
    {
        public void VerifyComparisonGroupsIsDisplayedWithNoImport()
        {
            if (NoImportComparion_lbl.IsDisplayed() && NoImportComparion_lbl.GetText().Equals("No Import Comparison Groups to display.") is true)
            {

                ExtentReportsHelper.LogPass($"<font color='green'><b>Comparison Groups is displayed With No Import Comparison</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>Comparison Groups is not display With No Import Comparison</font>");
            }
        }

        public void VerifyComparisonGroupsIsDisplayedWithOptions(params string[] ListOption)
        {
            foreach (string Option in ListOption)
            {
                Label Option_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]");
                if (Option_lbl.IsDisplayed() is true && Option_lbl.GetText().Equals(Option) is true)
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Option Name column with {Option} is displayed in Comparison Groups</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The Option Name column with {Option} is displayed in Comparison Groups</font>");
                }
            }
        }

        public void VerifyComparisonGroupsIsNotDisplayWithOptions(params string[] ListOption)
        {
            foreach (string Option in ListOption)
            {
                Label Option_lbl = new Label(FindType.XPath, $"//*[@id='ctl00_CPH_Content_rtlComparisonGroups']//span[contains(@id,'lblOptionName') and contains(text(),'{Option}')]");
                if (Option_lbl.IsDisplayed() is false )
                {
                    ExtentReportsHelper.LogPass($"<font color='green'><b>The Option Name column with {Option} is not display in Comparison Groups</b></font>");
                }
                else
                {
                    ExtentReportsHelper.LogFail($"<font color='red'>The Option Name column with {Option} is displayed in Comparison Groups</font>");
                }
            }
        }

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
            }

            if (Condition_lbl.IsDisplayed() is true && Condition_lbl.GetText().Equals(Condition) is true)
            {
                ExtentReportsHelper.LogPass($"<font color='green'><b>The Condition column with {Condition} is displayed in Comparison Groups</b></font>");
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'>The Condition column with {Condition} is displayed in Comparison Groups</font>");
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

    }
}
