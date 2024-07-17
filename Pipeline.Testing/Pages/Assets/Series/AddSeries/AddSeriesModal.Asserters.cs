namespace Pipeline.Testing.Pages.Assets.Series.AddSeries
{
    public partial class AddSeriesModal
    {
        public bool IsDefaultValues
        {
            get
            {
                if (!string.IsNullOrEmpty(SeriesCode_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(SeriesName_txt.GetText()))
                    return false;
                if (!string.IsNullOrEmpty(SeriesDescription_txt.GetText()))
                    return false;
                return true;
            }
        }

        public bool IsModalDisplayed()
        {
            return ModalTitle_lbl.WaitForElementIsVisible(5);
        }

        public string IsModalNameIsDisplayed()
        {
            return ModalName_lbl.GetText();
        }
    }
}
