using Pipeline.Common.Controls;
using Pipeline.Common.Enums;

namespace Pipeline.Testing.Pages.Estimating.Units.AddUnit
{
    public partial class AddUnitPage : UnitPage
    {
        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_Panel3']/h1");

        protected Textbox Abbreviation_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUnitAbbrInsert']");


        protected Textbox Name_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtUnitNameInsert']");

        protected Button PhaseSave_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbInsertUnit']");

        protected Button PhaseClose_btn
           => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_Panel3']/a");
    }

}
