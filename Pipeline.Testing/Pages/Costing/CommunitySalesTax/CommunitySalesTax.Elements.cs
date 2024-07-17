using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;


namespace Pipeline.Testing.Pages.Costing.CommunitySalesTax
{
    public partial class CommunitySalesTaxPage : DashboardContentPage<CommunitySalesTaxPage>
    {
        private string _gridLoading => "//*[@id='ctl00_CPH_Content_LoadingPanel1ctl00_CPH_Content_rgTaxes']/div[1]";
        protected IGrid CommunitySalesTaxPage_Grid => new Grid(FindType.Id, "ctl00_CPH_Content_rgTaxes_ctl00", _gridLoading);     
        protected DropdownList Community_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlCommunities");
        protected DropdownList TaxGroup_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlCommunityTaxGroup");
        protected Button BuildingPhaseArrow_btn => new Button(FindType.Id, "ctl00_CPH_Content_rgTaxes_ctl00_ctl02_ctl02_ddlBuildingPhases_Arrow");
        protected DropdownList TaxGroupOverride_ddl => new DropdownList(FindType.XPath, "//*[@id='ctl00_CPH_Content_rgTaxes_ctl00_ctl04_ddlTaxGroups']/select");
        protected Button UpdateValue_btn => new Button(FindType.Id, "ctl00_CPH_Content_rgTaxes_ctl00_ctl04_UpdateButton");
        
    }
    
}
