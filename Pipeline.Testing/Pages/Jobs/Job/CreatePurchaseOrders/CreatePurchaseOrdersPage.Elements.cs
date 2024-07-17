using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders.Variance;
using Pipeline.Testing.Pages.Jobs.Job.JobBOM.OneTimeItem;

namespace Pipeline.Testing.Pages.Jobs.Job.CreatePurchaseOrders
{
    public partial class CreatePurchaseOrdersPage :DashboardContentPage<CreatePurchaseOrdersPage>
    {
        public VarianceModal VarianceModal { get; private set; }

        protected Button CreatePO_ConfirmChanges_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_btnConfirmChanges']");
        protected Button CreatePO_btn => new Button(FindType.XPath, "//*[@id='ctl00_CPH_Content_lbSelected']");
        protected Grid ViewPhase_Grid => new Grid(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00']", "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgPhasesView']/div[1]");
        protected Image POInformationTooltip => new Image(FindType.XPath, "//*[@id=\"ctl00_CPH_Content_rgPhasesView_ctl00\"]/tbody/tr[1]/td[7]/div[1]/svg");
        protected DropdownList DefaultView_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlViewByTypes");
        protected CheckBox SelectAll_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgPhasesView_ctl00_ctl02_ctl00_IBP_BuildingPhases_NameSelectCheckBox']");
        protected CheckBox SelectAllBG_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgBuildingGroupsView_ctl00_ctl02_ctl00_BuildingGroups_ClientSelectColumnSelectCheckBox']");
        protected CheckBox SelectAllRG_chk =>
                   new CheckBox(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgReleaseGroupsView_ctl00_ctl02_ctl00_ReleaseGroups_ClientSelectColumnSelectCheckBox']");

    }
}
