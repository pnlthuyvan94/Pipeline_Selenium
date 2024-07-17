using Pipeline.Common.Utils;
using System;

namespace Pipeline.Testing.Pages.Purchasing.CostType.CostTypeDetail
{
    public partial class CostTypeDetailPage
    {
        /// <summary>
        /// Verify the title of Cost Type
        /// </summary>
        /// <param name="title">The expected title name</param>
        /// <returns></returns>
        public bool IsTitleCostTypeDisplay()
        {
            return CostTypeTitle_lbl != null && CostTypeTitle_lbl.IsDisplayed() == true;
        }

        /// <summary>
        /// Verify the Category Type on the grid view
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool IsCostTypeDisplayCorrect(CostTypeData data)
        {
            bool result = true;
            if (data.Name != Name_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Name: {data.Name}.<br>Actual Name: {Name_txt.GetValue()}");
                result = false;
            }
            if (data.Description != Description_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Description: {data.Description}.<br>Actual Description: {Description_txt.GetValue()}");
                result = false;
            }
            if (data.TaxGroup != TaxGroup_ddl.SelectedItemName)
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"Expected Tax Group: {data.TaxGroup}.<br>Actual Tax Group: {TaxGroup_ddl.SelectedItemName}");
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Verify the CostCategory modal
        /// </summary>
        /// <returns></returns>
        public bool IsAddCostCategoryDisplayed()
        {
            AddCostCategory_lbl.WaitForElementIsVisible(5);
            return AddCostCategory_lbl != null && AddCostCategory_lbl.IsDisplayed() is true;
        }
    }
}
