namespace Pipeline.Testing.Pages.Jobs.Job.Import
{
    public partial class JobImportPage
    {
        public bool IsImportPageDisplayed
        {
            get
            {
                return Import_lbl.WaitForElementIsVisible(5, false);
            }
        }
    }
}
