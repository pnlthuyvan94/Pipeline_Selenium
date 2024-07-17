using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.ReleaseGroup.ReleaseGroupDetail
{
    public partial class ReleaseGroupDetailPage
    {
        /// <summary>
        /// Verify the title of Release Group
        /// </summary>
        /// <param name="title">The expected title name</param>
        /// <returns></returns>
        public bool IsTitleReleaseGroupDisplay()
        {
            return ReleaseGroupTitle_lbl != null && ReleaseGroupTitle_lbl.IsDisplayed() == true;
        }

        /// <summary>
        /// Verify the Release Group on the grid view
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsReleaseGroupDisplayCorrect(ReleaseGroupData data)
        {
            bool result = true;
            if (data.Name != Name_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color = 'red'>Expected Name: {data.Name}.<br>Actual Name: {Name_txt.GetValue()}</font>");
                result = false;
            }
            if (data.Description != Description_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color = 'red'>Expected Description: {data.Description}.<br>Actual Description: {Description_txt.GetValue()}</font>");
                result = false;
            }
            if (data.SortOrder != SortOrder_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color = 'red'>Expected Sort Order: {data.Description}.<br>Actual Description: {SortOrder_txt.GetValue()}</font>");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Verify Select Building Phase modal
        /// </summary>
        /// <returns></returns>
        public bool IsAddBuildingPhaseDisplayed()
        {
            AddBuildingPhase_lbl.WaitForElementIsVisible(5);
            return AddBuildingPhase_lbl != null && AddBuildingPhase_lbl.IsDisplayed() is true;
        }
    }
}
