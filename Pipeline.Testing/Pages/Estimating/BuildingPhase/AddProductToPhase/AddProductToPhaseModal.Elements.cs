using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.AddProductToPhase
{
    public partial class AddProductToPhaseModal : DetailsContentPage<BuildingPhaseDetailPage>
    {
        public AddProductToPhaseModal() : base()
        {
        }


        //Elements below are for Purchasing Module
        protected DropdownList Product_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlProducts");
        protected DropdownList TaxStatus_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlPhaseTaxStatus");
        protected CheckBox SetDefault_chkbox => new CheckBox(FindType.Id, "ctl00_CPH_Content_ckbSetAsDefault");

        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertProduct");
    }
}
