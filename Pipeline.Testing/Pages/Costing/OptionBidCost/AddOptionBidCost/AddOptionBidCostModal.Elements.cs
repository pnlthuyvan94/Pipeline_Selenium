using Pipeline.Common.Controls;
using Pipeline.Common.Enums;


namespace Pipeline.Testing.Pages.Costing.OptionBidCost.AddOptionBidCost
{
    public partial class AddOptionBidCostModal : OptionBidCostPage
    {
        public AddOptionBidCostModal() : base()
        {
        }

        protected Label ModalTitle_lbl => new Label(FindType.XPath, "//*[@id='bidcost-modal']/section/header/h1");
        protected Button Close_Btn => new Button(FindType.XPath, "//*[@id='bidcost-modal']/section/header/a");
        protected DropdownList CostTier_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlCostTiers']");

        protected DropdownList Tier1Option_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTier1Options']");

        protected DropdownList Tier2House_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTier2Houses']");

        protected DropdownList Tier2Option_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTier2Options']");

        protected DropdownList Tier3Community_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTier3Communities']");

        protected DropdownList Tier3Option_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTier3Options']");

        protected DropdownList Tier4Community_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTier4Communities']");

        protected DropdownList Tier4House_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTier4Houses']");
        protected DropdownList Tier4Option_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlTier4Options']");

        protected Button ToggleConditionFields_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCondition']");

        protected DropdownList Option_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddNewOption']");

        protected Button AddOptionToFinalCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewOption']");

        protected DropdownList Operator_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddNewOperator']");

        protected Button AddOperatorToFinalCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddNewOperator']");

        protected Textbox FinalCondition_Textbox => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddNewDependentCondition']");

        protected Button SaveCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbAddDependentCondition']");

        protected Button ClearCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbClearDependentCondition']");

        protected Button CancelCondition_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCancelDependentCondition']");

        protected DropdownList BuildingPhase_Dropdown => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_ddlAddBuildingPhases']");

        protected Textbox BidCost_Textbox => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAddBidCost']");

        protected Button SaveBidCost_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveBidCost']");

        protected Label savebtn_loading => new Label(FindType.XPath, "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_lbSaveBidCost']/div[1]");

        protected Button AddAllowance_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbUpdateAllowance']");

        protected Textbox Allowance_Textbox => new Textbox(FindType.XPath, "//*[@id='ctl00_CPH_Content_txtAllowance']");

        protected Button SaveAllowance_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSaveAllowance']");
        protected Button Cancel_Btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbCancelABC']");

    }
}
