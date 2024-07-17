namespace Pipeline.Testing.Pages.Estimating.Worksheet.WorksheetDetail
{
    public partial class WorksheetDetailPage
    {
        /*
         * Verify head title and id of new manufacturer
         */
        public bool IsSaveWorksheetSuccessful(string worksheetName)
        {
            System.Threading.Thread.Sleep(1000);
            return SubHeadTitle().Equals(worksheetName) && !CurrentURL.EndsWith("hid=0");
        }
    }
}
