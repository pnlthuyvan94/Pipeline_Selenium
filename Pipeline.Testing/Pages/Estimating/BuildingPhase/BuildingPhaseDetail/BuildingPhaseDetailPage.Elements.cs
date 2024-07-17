using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddProductToPhase;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.AddVendorToPhase;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail
{
    public partial class BuildingPhaseDetailPage : DetailsContentPage<BuildingPhaseDetailPage>
    {
        public BuildingPhaseDetailPage() : base() { }

        public AddProductToPhaseModal AddProductToPhaseModal { get; private set; }
        public AddVendorToPhaseModal AddVendorToPhaseModal { get; private set; }

        protected Textbox Code_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtPhaseCode");
        protected Textbox Name_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtPhaseName");
        protected Textbox Abbreviated_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtPhaseAbbName");
        protected Textbox Description_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtPhaseDesc");
        protected DropdownList BuildingGroup_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlPhaseBuildingGroup");
        protected DropdownList Type_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlPhaseType");
        protected DropdownList Parent_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlPhaseParent");
        //protected DropdownList CutOffPhase_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlCutoffPhaseToBuildingPhase");


        //Elements below are for Purchasing Module
        protected DropdownList SchedulingTaskForPayment_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlSchedulingTaskForPayment");
        protected DropdownList TaskForPoDisplay_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlTaskForPODisplay");
        protected DropdownList Trade_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlBuildingTrade");
        protected DropdownList ReleaseGroup_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlReleaseGroups");
        protected DropdownList CostCode_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlCostCodes");
        protected DropdownList CostCategory_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlCostCategories");
        protected DropdownList POView_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlPOViewType");
        protected CheckBox BudgetOnly_chkbox => new CheckBox(FindType.Id, "ctl00_CPH_Content_chkBxBudgetOnly");
        protected Textbox Percent_Billed_txt => new Textbox(FindType.Id, "ctl00_CPH_Content_txtPercentBilled");
        protected RadioButton TaxableYes_rbtn => new RadioButton(FindType.Id, "ctl00_CPH_Content_rbTaxable_0");
        protected RadioButton TaxableNo_rbtn => new RadioButton(FindType.Id, "ctl00_CPH_Content_rbTaxable_1");

        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbSave");

        protected Button AddProductToBuildingPhase_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddProduct");
        protected Button AddVendorToBuildingPhase_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbAddVendor");

        protected Grid Products_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgProducts_ctl00", "");
        protected Grid Vendors_grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgVendors_ctl00", "");
    }
}
