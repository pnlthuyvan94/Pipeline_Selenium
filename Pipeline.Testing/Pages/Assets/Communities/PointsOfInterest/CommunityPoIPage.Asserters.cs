using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.Communities.PointsOfInterest
{
    public partial class CommunityPoIPage
    {
        public bool IsItemOnGrid(string columnName, string valueToFind)
        {
            return POI_Grid.IsItemOnCurrentPage(columnName, valueToFind);
        }

        public bool IsPointOfInterestPageDisplayed()
        {
            return HeaderNameOfPage_Lbl.WaitForElementIsVisible(5);
        }

        public bool IsBreadCrumbDisplayedWithName(string name)
        {
            if (SubHead_Lbl.GetText().Equals(name))
            {
                ExtentReportsHelper.LogPass(CommonHelper.CaptureScreen(SubHead_Lbl), $"The Points of Interest page is displayed with correct name <font color='green'><b>{name}</b></font>");
                return true;
            }
            ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(SubHead_Lbl), $"The Points of Interest page is <b>NOT</b> display with correct name <font color='green'><b>{name}</b></font>");
            return false;
        }
    }
}
