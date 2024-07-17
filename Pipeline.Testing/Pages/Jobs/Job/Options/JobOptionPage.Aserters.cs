namespace Pipeline.Testing.Pages.Jobs.Job.Options
{
    public partial class JobOptionPage
    {
        public bool IsOptionCardDisplayed
        {
            get
            {
                return OptionTitle_lbl.WaitForElementIsVisible(5, false);
            }
        }

        public bool IsAddConfigEnabled
        {
            get
            {
                string elementClass = AddConfig_btn.GetAttribute("class");
                if (elementClass.Contains("aspNetDisabled"))
                    return false;
                else
                    return true;
            }
        }
    }
}
