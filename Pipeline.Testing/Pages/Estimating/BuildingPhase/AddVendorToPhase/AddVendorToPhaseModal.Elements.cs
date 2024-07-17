using Pipeline.Common.Controls;
using Pipeline.Common.Enums;
using Pipeline.Common.Pages;
using Pipeline.Testing.Pages.Estimating.BuildingPhase.BuildingPhaseDetail;

namespace Pipeline.Testing.Pages.Estimating.BuildingPhase.AddVendorToPhase
{
    public partial class AddVendorToPhaseModal : DetailsContentPage<BuildingPhaseDetailPage>
    {
        public AddVendorToPhaseModal() : base()
        {
        }


        //Elements below are for Purchasing Module
        protected DropdownList Vendor_ddl => new DropdownList(FindType.Id, "ctl00_CPH_Content_ddlVendors");

        protected Button Save_btn => new Button(FindType.Id, "ctl00_CPH_Content_lbInsertVendor");
    }
}
