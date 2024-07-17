using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.CostCode.CostCodeDetail
{
    public partial class CostCodeDetailPage
    {
        /// <summary>
        /// Verify the title of Cost Type
        /// </summary>
        /// <param name="title">The expected title name</param>
        /// <returns></returns>
        public bool IsTitleCostCodeDisplay()
        {
            return CostCodeTitle_lbl != null && CostCodeTitle_lbl.IsDisplayed() == true;
        }

        /// <summary>
        /// Verify the Category Type on the grid view
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsCostTypeDisplayCorrect(CostCodeData data)
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
            return result;
        }

        /// <summary>
        /// Verify the CostCategory modal
        /// </summary>
        /// <returns></returns>
        public bool IsAddBuildingPhaseDisplayed()
        {
            AddBuildingPhase_lbl.WaitForElementIsVisible(5);
            return AddBuildingPhase_lbl != null && AddBuildingPhase_lbl.IsDisplayed() is true;
        }
    }
}
