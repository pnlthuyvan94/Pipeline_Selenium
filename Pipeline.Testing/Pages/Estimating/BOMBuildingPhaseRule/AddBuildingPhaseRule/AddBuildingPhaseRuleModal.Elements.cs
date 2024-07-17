using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Estimating.BOMBuildingPhaseRule.AddBuildingPhaseRule
{
    public partial class AddBuildingPhaseRuleModal : BOMBuildingPhaseRulePage
    {
        public AddBuildingPhaseRuleModal() : base()
        {
        }

        protected Label ModalTitle_lbl
            => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_lblNewTitle']");

        protected DropdownList OriginalProductBuildingPhase_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlInsertParentPhase']");

        protected DropdownList OriginalSubBuildingPhase_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlInsertChildPhase']");

        protected DropdownList NewSubBuildingPhase_ddl
            => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlInsertNewChildPhase']");

        protected Button Save_btn
            => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveContent']");

    }

}
