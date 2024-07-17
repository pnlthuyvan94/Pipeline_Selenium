using LinqToExcel;
using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Utils;
using System.Linq;

namespace Pipeline.Testing.Pages.Estimating.QuantityBuildingPhaseRule.AddBuildingPhaseRule
{
    public partial class AddBuildingPhaseRuleModal : QuantityBuildingPhaseRulePage
    {
        public IQueryable<Row> TestData_RT01024;

        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblNewTitle']");

        protected Textbox Priority_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtNewRulePriority']");

        protected Textbox LevelCondition_txt
            => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtBoxLevel']");

        protected DropdownList OriginalBuildingPhase_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlOldRulePhase']");

        protected DropdownList NewBuildingPhase_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlNewRulePhase']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");

    }

}
