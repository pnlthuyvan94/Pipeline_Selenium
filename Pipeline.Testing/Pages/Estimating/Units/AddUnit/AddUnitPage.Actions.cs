namespace Pipeline.Testing.Pages.Estimating.Units.AddUnit
{
    public partial class AddUnitPage
    {
        public AddUnitPage AddAbbreviation(string abbreviation)
        {
            Abbreviation_txt.SetText(abbreviation);
            return this;
        }

        public AddUnitPage AddName(string name)
        {
            Name_txt.SetText(name);
            return this;
        }

        public void Save()
        {
            PhaseSave_btn.Click();
            WaitGridLoad();
        }


        public void CloseModal()
        {
            PhaseClose_btn.Click();
            System.Threading.Thread.Sleep(500);
        }
    }
}
