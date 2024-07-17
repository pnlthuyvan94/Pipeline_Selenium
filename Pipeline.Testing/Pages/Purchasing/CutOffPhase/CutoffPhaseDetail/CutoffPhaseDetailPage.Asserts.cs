using Pipeline.Common.Controls;
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.CutoffPhase.CutoffPhaseDetail
{
    public partial class CutoffPhaseDetailPage
    {
        /// <summary>
        /// Verify the title of Cutoff Phase
        /// </summary>
        /// <param name="title">The expected title name</param>
        /// <returns></returns>
        public bool IsCutoffPhaseTitleDisplayed()
        {
            return CutoffPhaseTitle_lbl != null && CutoffPhaseTitle_lbl.IsDisplayed(false);
        }

        /// <summary>
        /// Verify the Cutoff Phase on the grid view
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsCutoffPhaseDisplayedCorrect(CutoffPhaseData data)
        {
            bool result = true;
            if (data.Code != Code_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color = 'red'>Expected Description: {data.Code}.<br>Actual Code: {Code_txt.GetValue()}</font>");
                result = false;
            }
            if (data.Name != Name_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color = 'red'>Expected Name: {data.Name}.<br>Actual Name: {Name_txt.GetValue()}</font>");
                result = false;
            }
            if (data.SortOrder != SortOrder_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Cost Type: {data.SortOrder}.<br>Actual Cost Type: {SortOrder_txt.GetValue()}");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Verify the attribute modal is displayed. Attribute can be Building Phases/ Option Groups/ Options
        /// </summary>
        /// <returns></returns>
        public bool IsAddAttributeModalDisplayed(string attributeName)
        {
            Label modal_lbl;
            switch (attributeName)
            {
                case "Building Phases":
                    modal_lbl = AddBuildingPhase_lbl;
                    break;
                case "Option Groups":
                    modal_lbl = AddOptionGroup_lbl;
                    break;
                default:
                    // Default is "Options"
                    modal_lbl = AddOption_lbl;
                    break;
            }

            modal_lbl.WaitForElementIsVisible(5, false);
            return modal_lbl.IsDisplayed(false) is true;
        }
    }
}
