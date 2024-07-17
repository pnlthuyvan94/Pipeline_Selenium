using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Purchasing.CostCategory.CostCategoryDetail
{
    public partial class CostCategoryDetailPage
    {
        /// <summary>
        /// Verify the title of Cost Category
        /// </summary>
        /// <param name="title">The expected title name</param>
        /// <returns></returns>
        public bool IsTitleCostCategoryDisplayed()
        {
            return CostCategoryTitle_lbl != null && CostCategoryTitle_lbl.IsDisplayed(false) == true;
        }

        /// <summary>
        /// Verify the Cost Category on the grid view
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsCostCategoryDisplayedCorrect(CostCategoryData data)
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
            if (data.CostType != CostType_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Cost Type: {data.CostType}.<br>Actual Cost Type: {CostType_ddl.SelectedItemName}");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Verify the Building Phase modal
        /// </summary>
        /// <returns></returns>
        public bool IsAddBuildingPhaseDisplayed()
        {
            AddBuildingPhase_lbl.WaitForElementIsVisible(5, false);
            return AddBuildingPhase_lbl != null && AddBuildingPhase_lbl.IsDisplayed(false) is true;
        }
    }
}
