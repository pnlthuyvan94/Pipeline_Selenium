

using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.House.HouseSpecSet
{
    public partial class HouseSpecSetPage
    {
        public bool VerifyJobSpecSetsPageIsDisplayed()
        {
            SpecSetsHeaderTitle_Lbl.WaitForElementIsVisible(5);
            if (SpecSetsHeaderTitle_Lbl.IsDisplayed(false))
            {
                ExtentReportsHelper.LogPass("<font color='green'><b>The House Spec Sets Page is displayed correctly</b></font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail("<font color='red'>The House Spec Sets Page is displayed wrong</font>");
                return false;
            }
        }
    }
}
