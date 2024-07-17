using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Estimating.BuildingPhaseType.AddType
{
    public partial class AddTypeModal : BuildingPhaseTypePage
    {
        public AddTypeModal() : base()
        {
        }

        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='options-modal']/section/header/h1");

        protected Textbox BuildingPhaseTypeName_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewBuildingPhaseTypeName']");

        protected Button PhaseSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");

        protected Button PhaseClose_btn
           => new Button(FindType.XPath, "//*[@id='options-modal']/section/header/a");

    }

}
