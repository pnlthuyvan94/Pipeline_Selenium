
using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Estimating.Calculations.CalculationDetail
{
    public partial class CalculationDetailPage
    {
        public bool IsDisplayDataCorrectly(CalculationData data)
        {
            if (!SubHeadTitle().Equals(data.Description))
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color='red'>Expected Sub Head title: {data.Description}. Actual Sub Head title: {SubHeadTitle()}</font>");
                return false;
            }
            if (data.Description != Description_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color='red'>Expected Description: {data.Description}. Actual result: {Description_txt.GetValue()}</font>");
                return false;
            }
            if ("QTY " + data.Calculation != Calculation_txt.GetValue())
            {
                ExtentReportsHelper.LogFail(CommonHelper.CaptureScreen(), $"<font color='red'>Expected Calculation: {"QTY " + data.Calculation}. Actual result: {Calculation_txt.GetValue()}</font>");
                return false;
            }
            ExtentReportsHelper.LogPass(null, $"<font color='green'><b>The Calculation detail page with description: '{data.Description}' " +
                       $"and calculation: '{data.Calculation}' displays correctly.</b></font>");
            return true;
        }
    }
}
