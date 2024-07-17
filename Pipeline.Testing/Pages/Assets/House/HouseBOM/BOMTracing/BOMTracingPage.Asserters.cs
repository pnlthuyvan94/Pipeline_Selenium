

using Pipeline.Common.Utils;

namespace Pipeline.Testing.Pages.Assets.House.HouseBOM.BOMTracing
{
    public partial class BOMTracingPage
    {
        public bool VerifyMiniBtnFunction()
        {
            if (Mini_Btn.IsDisplayed())
            {
                ClickMiniBtn();
                return (MiniDetailedFinal_Lbl.IsDisplayed() && MiniDetailedKeyMeasures_Lbl.IsDisplayed());
            }
            else
            {
                return false;
            }
        }
        public bool VerifyDetailedBtnFunction()
        {
            if (Detailed_Btn.IsDisplayed())
            {
                ClickDetailedBtn();
                return (DetailedFinalBOMProduct_Lbl.IsDisplayed() && DetailedSubFinal_Lbl.IsDisplayed() && DetailedTotalSum_Lbl.IsDisplayed()
                    && MiniDetailedFinal_Lbl.IsDisplayed() && MiniDetailedKeyMeasures_Lbl.IsDisplayed());
            }
            else
            {
                return false;
            }
        }
        public bool VerifyNormalBtnPage()
        {
            if(Normal_Btn.IsDisplayed())
            {              
                ExtentReportsHelper.LogPass($"<font color='green'> BOM trace tables are shown </font>");
                return true;              
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> BOM trace tables are not shown </font>");
                return false;
            }
        }
        public bool VerifyKeyMeasureBOMTraceModal()
        {
            if(KeyMeasureBomTrace.IsDisplayed())
            {
                ExtentReportsHelper.LogPass($"<font color='green'> BOM trace Key measure tables are shown </font>");
                return true;
            }
            else
            {
                ExtentReportsHelper.LogFail($"<font color='red'> BOM trace Key measure tables are not shown </font>");
                return false;
            }
        }
    }
}
