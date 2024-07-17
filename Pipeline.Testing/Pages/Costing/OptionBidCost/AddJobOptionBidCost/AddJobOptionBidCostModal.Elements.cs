using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Costing.OptionBidCost.AddJobOptionBidCost
{
    public partial class AddJobOptionBidCostModal : OptionBidCostPage
    {
        public AddJobOptionBidCostModal() : base()
        {
        }

        protected Label JobModalTitle_lbl => new Label(FindType.XPath, "//*[@id='job-bidcost-modal']/section/header/h1");

        protected Button JobClose_Btn => new Button(FindType.XPath, "//*[@id='job-bidcost-modal']/section/header/a");

        protected DropdownList ModalJob_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddJob']");

        protected DropdownList JobOption_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddJobOption']");

        protected Button JobToggleConditionFields_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCondition2']");

        protected DropdownList JobConditionOption_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddNewOption2']");

        protected Button JobAddOptionToFinalCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewOption2']");

        protected DropdownList JobOperator_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddNewOperator2']");

        protected Button JobAddOperatorToFinalCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewOperator2']");

        protected Textbox JobFinalCondition_Textbox => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddNewDependentCondition2']");

        protected Button JobSaveCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddDependentCondition2']");

        protected Button JobClearCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCancelDependentCondition2']");

        protected Button JobCancelCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbClearDependentCondition2']");

        protected DropdownList JobBuildingPhase_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddJobBuildingPhases']");

        protected Textbox JobBidCost_Textbox => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddJobBidCost']");

        protected Button JobSaveBidCost_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveJobBidCost']");
    }
}
