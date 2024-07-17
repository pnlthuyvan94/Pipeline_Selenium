using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities.Options.AddOptionCondition
{
    public partial class AddOptionConditionGrid
    {
        public bool IsConditionGridDisplayed()
        {
            return AssignConditionGrid.WaitForElementIsVisible(5);
        }

        public bool IsFinalConditionDisplayedCorrectly(string expectedCondition, string price)
        {
            string expectedPrice = "$" + price;
            FinalCondition_txt.WaitForElementIsVisible(5);
            // Successful in case the condition is displayed correctly after applying
            if (FinalCondition_txt.GetText() != expectedCondition || FinalConditionPrice_txt.GetText() != expectedPrice)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<br> The final condition or price aren't correct. </br> Expected: Condition: {expectedCondition}, Sale Price: {price} </br> "
                    + $"Actual: Condition: {FinalCondition_txt}, Sale Price: {FinalConditionPrice_txt}");
                return false;
            }
            return true;
        }
    }
}
