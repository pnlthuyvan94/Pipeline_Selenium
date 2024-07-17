namespace Pipeline.Testing.Pages.Estimating.BuildingPhaseType.AddType
{
    public partial class AddTypeModal
    {
        public AddTypeModal EnterBuildingPhaseTypeName(string name)
        {
            BuildingPhaseTypeName_txt.SetText(name);
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
