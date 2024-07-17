using Pipeline.Common.BaseClass;


namespace Pipeline.Testing.Pages.Assets.House.HouseBOM.BOMTracing
{
    public partial class BOMTracingPage
    {
        public void ClickMiniBtn()
        {
            Mini_Btn.Click();
            BasePage.JQueryLoad();
        }

        public void ClickDetailedBtn()
        {
            Detailed_Btn.Click();
            BasePage.JQueryLoad();
        }
        
    }

}
